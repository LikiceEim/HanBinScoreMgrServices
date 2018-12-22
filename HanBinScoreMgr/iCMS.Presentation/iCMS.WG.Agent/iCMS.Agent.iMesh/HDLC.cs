using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iMesh
{
    internal delegate void DlgtRx1Frame(byte[] hdlcFrame, byte len);

    /// <summary>
    /// HDLC解析及组装层
    /// </summary>
    internal class HDLC : IStackLayer, IDisposable
    {
        public const byte HDLC_FLAG = 0x7E;
        public const byte HDLC_ESCAPE = 0x7D;
        public const byte HDLC_ESCAPE_MASK = 0x20;
        public const byte HDLC_MAX_BUFFER_SIZE = 128;
        public const ushort HDLC_CRCINIT = 0xFFFF;
        public const ushort HDLC_CRCGOOD = 0xF0B8;
        /// <summary>
        /// 上次接收到的最后一个字节数据
        /// </summary>
        private volatile byte m_u8LastRxByte = 0x00;
        /// <summary>
        /// 正在接受帧数据，只有接受非Flag数据此位才置位
        /// </summary>
        private volatile bool m_bBusyRevHdlcFrame = false;
        /// <summary>
        /// 表示当前接收到Escape字节数据0x7D
        /// </summary>
        private volatile bool m_bInputEscaping = false;
        /// <summary>
        /// HDLC接受帧数据CRC校验值
        /// </summary>
        private volatile ushort m_u16InputCrc = 0x0000;
        /// <summary>
        /// HDLC数据接受buffer写指针
        /// </summary>
        private volatile byte m_u8InputBufFill = 0;
        /// <summary>
        /// HDLC数据接受buffer，已经经过HDLC解码
        /// </summary>
        private byte[] m_u8aInputBuf = new byte[HDLC_MAX_BUFFER_SIZE];
        /// <summary>
        /// 输出HDLC帧的计算CRC值
        /// </summary>
        private volatile UInt16 m_u16OutputCrc = 0x0000;
        /// <summary>
        /// 输出帧长度
        /// </summary>
        private volatile byte m_u8OutputBufFill = 0;
        /// <summary>
        /// 输出帧暂存
        /// </summary>
        private byte[] m_u8aOutputBuf = new byte[2 * HDLC_MAX_BUFFER_SIZE];
        /// <summary>
        /// 用于记录输出帧，以便记录日志
        /// </summary>
        private volatile byte m_u8OutputPacketFill = 0;
        private byte[] m_u8aOutputPacketBuf = new byte[HDLC_MAX_BUFFER_SIZE];
        private object m_lockRx1Byte = new object();
        /// <summary>
        /// 底层通信设备
        /// </summary>
        private ComDevice m_Com = new ComDevice();
        /// <summary>
        /// 表示完整一帧HDLC报文接收事件的方法
        /// </summary>
        private DlgtRx1Frame m_evHDLCFrameArrived = null;
        /// <summary>
        /// 表示完整一帧HDLC报文接收事件的方法
        /// </summary>
        public event DlgtRx1Frame HDLCFrameArrived
        {
            add { m_evHDLCFrameArrived += value; }
            remove { m_evHDLCFrameArrived -= value; }
        }

        /// <summary>
        /// HDLC的构造函数
        /// </summary>
        public HDLC()
        {
            // 向底层通信设备注册字节数据到达回调函数
            m_Com.ByteArrived += Rx1Byte;
        }

        #region 接收HDLC数据
        /// <summary>
        /// HDLC层准备接收数据
        /// </summary>
        private void InputOpen()
        {
            // reset the input buffer index
            m_u8InputBufFill = 0;
            // initialize the value of the CRC
            m_u16InputCrc = HDLC_CRCINIT;
        }

        /// <summary>
        /// HDLC层接收数据
        /// </summary>
        /// <param name="b">字节数据</param>
        private void InputWrite(byte b)
        {
            try
            {
                if (b == HDLC_ESCAPE)
                    m_bInputEscaping = true;
                else
                {
                    if (m_bInputEscaping == true)
                    {
                        // Kous: 注意一下转化时候按思路执行的
                        b = (byte)(b ^ HDLC_ESCAPE_MASK);
                        m_bInputEscaping = false;
                    }
                    // add byte to input buffer
                    m_u8aInputBuf[m_u8InputBufFill] = b;
                    m_u8InputBufFill++;
                    // iterate through CRC calculator
                    m_u16InputCrc = FCS.Fcs16CalcByte(m_u16InputCrc, b);
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eHdlc, "InputBuf(" + m_u8InputBufFill + ") " + ex.ToString());
            }
        }

        /// <summary>
        /// HDLC层接收数据结束
        /// </summary>
        private void InputClose()
        {
            lock (CommStackLog.LockLog)
            {
                if (m_u16InputCrc == HDLC_CRCGOOD)
                {
                    // remove the CRC from the input buffer
                    m_u8InputBufFill -= 2;
                    CommStackLog.RecordInf(enLogLayer.eHdlc, "Rx: " + CommStackLog.ToHexString(m_u8aInputBuf, m_u8InputBufFill));
                }
                else
                {
                    CommStackLog.RecordErr(enLogLayer.eHdlc, "Rx crc error: " + m_u16InputCrc.ToString("X4"));
                    m_u8InputBufFill = 0;
                }
                // reset escaping
                m_bInputEscaping = false;
            }
        }

        /// <summary>
        /// HDLC注册给底层数据收发外设(UART)的字节接受回调函数
        /// </summary>
        /// <param name="rxByte">接受的新字节数据</param>
        private void Rx1Byte(byte rxByte)
        {
            // lock the module
            // start of frame
            if (m_bBusyRevHdlcFrame == false && m_u8LastRxByte == HDLC_FLAG && rxByte != HDLC_FLAG)
            {
                // I'm now receiving
                m_bBusyRevHdlcFrame = true;
                // create the HDLC frame
                InputOpen();
                // add the byte just received
                InputWrite(rxByte);
            }
            // middle of frame
            else if (m_bBusyRevHdlcFrame == true && rxByte != HDLC_FLAG)
            {
                // add the byte just received
                InputWrite(rxByte);
                if (m_u8InputBufFill + 1 > HDLC_MAX_BUFFER_SIZE)
                {
                    m_u8InputBufFill = 0;
                    m_bBusyRevHdlcFrame = false;
                    // input buffer overflow, drop the incoming fram
                    CommStackLog.RecordErr(enLogLayer.eHdlc, "Rx overflow frame: " + CommStackLog.ToHexString(m_u8aInputBuf, m_u8InputBufFill));
                }
            }
            // end of frame
            else if (m_bBusyRevHdlcFrame == true && rxByte == HDLC_FLAG)
            {
                // finalize the HDLC frame
                InputClose();
                if (m_u8InputBufFill >= 4)
                {
                    // hand over frame to upper layer
                    if (m_evHDLCFrameArrived != null)
                    {
                        m_evHDLCFrameArrived(m_u8aInputBuf, m_u8InputBufFill);
                        m_u8InputBufFill = 0;
                    }
                }

                m_bBusyRevHdlcFrame = false;
            }

            m_u8LastRxByte = rxByte;
        }
        #endregion 接收HDLC数据

        #region 发送HDLC数据
        /// <summary>
        /// HDLC层准备发送数据
        /// </summary>
        public void OutputOpen()
        {
            m_u8OutputBufFill = 0;
            m_u8OutputPacketFill = 0;
            m_u16OutputCrc = HDLC_CRCINIT;
            m_u8aOutputBuf[m_u8OutputBufFill++] = HDLC_FLAG;
        }

        /// <summary>
        /// HDLC层发送数据
        /// </summary>
        /// <param name="b"></param>
        public void OutputWrite(byte b)
        {
            m_u8aOutputPacketBuf[m_u8OutputPacketFill++] = b;
            m_u16OutputCrc = FCS.Fcs16CalcByte(m_u16OutputCrc, b);
            // write optional escape byte
            if (b == HDLC_FLAG || b == HDLC_ESCAPE)
            {
                m_u8aOutputBuf[m_u8OutputBufFill++] = HDLC_ESCAPE;
                b = (byte)(b ^ HDLC_ESCAPE_MASK);
            }
            // data byte
            m_u8aOutputBuf[m_u8OutputBufFill++] = b;
        }

        /// <summary>
        /// HDLC层发送数据结束
        /// </summary>
        public void OutputClose()
        {
            UInt16 u16finalCrc = 0;
            u16finalCrc = (UInt16)(~m_u16OutputCrc);
            // write the CRC value
            OutputWrite((byte)((u16finalCrc >> 0) & 0xFF));
            OutputWrite((byte)((u16finalCrc >> 8) & 0xFF));
            // write closing HDLC flag
            lock (CommStackLog.LockLog)
            {
                //m_Com.Send(HDLC_FLAG);
                m_u8aOutputBuf[m_u8OutputBufFill++] = HDLC_FLAG;
                m_Com.SendArr(m_u8aOutputBuf, m_u8OutputBufFill);
                CommStackLog.RecordInf(enLogLayer.eHdlc, "Tx: " + CommStackLog.ToHexString(m_u8aOutputPacketBuf, m_u8OutputPacketFill));
            }
        }
        #endregion 发送HDLC数据

        /// <summary>
        /// 检测CLI打印
        /// </summary>
        public void MoniterCli()
        {
            m_Com.MoniterCli();
        }

        /// <summary>
        /// 软件重启Manager
        /// </summary>
        public void ResetManager()
        {
            m_Com.ResetManager();
        }

        #region IDisposable 成员
        public void Dispose()
        {
            if (m_Com != null)
            {
                m_Com.Dispose();
            }
        }
        #endregion

        #region IStackLayer 实现
        public void Initialize()
        {
            m_Com.Initialize();

            m_u8LastRxByte = 0x00;
            m_bBusyRevHdlcFrame = false;
            m_bInputEscaping = false;
            m_u16InputCrc = 0x0000;
            m_u16OutputCrc = 0x0000;

            m_u8InputBufFill = 0;
            m_u8OutputBufFill = 0;

            Array.Clear(m_u8aInputBuf, 0, HDLC_MAX_BUFFER_SIZE);
            Array.Clear(m_u8aOutputBuf, 0, HDLC_MAX_BUFFER_SIZE);
        }
        public void Reset()
        {
            m_Com.Reset();

            m_u8LastRxByte = 0x00;
            m_bBusyRevHdlcFrame = false;
            m_bInputEscaping = false;

            m_u16InputCrc = 0x0000;
            m_u16OutputCrc = 0x0000;

            m_u8InputBufFill = 0;
            m_u8OutputBufFill = 0;

            Array.Clear(m_u8aInputBuf, 0, HDLC_MAX_BUFFER_SIZE);
            Array.Clear(m_u8aOutputBuf, 0, HDLC_MAX_BUFFER_SIZE);
        }
        #endregion
    }
}
