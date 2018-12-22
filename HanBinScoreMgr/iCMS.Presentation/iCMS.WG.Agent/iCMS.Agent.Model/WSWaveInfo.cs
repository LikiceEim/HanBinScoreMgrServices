using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

using iCMS.WG.Agent.Common.Enum;

namespace iCMS.WG.Agent.Model
{
    [Serializable]
    public class WSWaveInfo : ICloneable
    {
        private int _WGNO;
        /// <summary>
        /// WGID
        /// </summary>
        public int WGNO
        {
            get { return _WGNO; }
            set { _WGNO = value; }
        }

        private int _WSNO;
        /// <summary>
        /// WSID
        /// </summary>
        public int WSNO
        {
            get { return _WSNO; }
            set { _WSNO = value; }
        }

        private EnumWaveType _SignalType;
        /// <summary>
        /// 波形类型：加速度、加速度包络、速度、位移、原始波形
        /// </summary>
        public EnumWaveType WaveType
        {
            get { return _SignalType; }
            set { _SignalType = value; }
        }

        private byte[] _WaveData;
        /// <summary>
        /// 波形数据
        /// </summary>
        public byte[] WaveData
        {
            get { return _WaveData; }
            set { _WaveData = value; }
        }

        private bool _IsFull;
        /// <summary>
        /// 波形是否完整：true：完整；false：不完整；
        /// </summary>
        public bool IsFull
        {
            get { return _IsFull; }
            set { _IsFull = value; }
        }

        DateTime _LastReceiveTime;
        /// <summary>
        /// 最后一次接受数据时间
        /// </summary>
        public DateTime LastReceiveTime
        {
            get { return _LastReceiveTime; }
            set { _LastReceiveTime = value; }
        }

        private int[] _ReceiveDataNumber;
        /// <summary>
        /// 收到的报文的号
        /// </summary>
        public int[] ReceiveDataNumber
        {
            get { return _ReceiveDataNumber; }
            set { _ReceiveDataNumber = value; }
        }

        private int _CurrentNoDataNumber;
        /// <summary>
        /// 当前没有波形的报文帧数
        /// </summary>
        public int CurrentNoDataNumber
        {
            get { return _CurrentNoDataNumber; }
            set { _CurrentNoDataNumber = value; }

        }

        private int _RepeatNumber;
        /// <summary>
        /// 重发次数
        /// </summary>
        public int RepeatNumber
        {
            get { return _RepeatNumber; }
            set { _RepeatNumber = value; }
        }

        private bool _IsRepeat;
        /// <summary>
        /// 是否发送接收未收到的波形数据报文
        /// </summary>
        public bool IsRepeat
        {
            set { _IsRepeat = value; }
            get { return _IsRepeat; }
        }


        private WaveInformation _WaveInfo;
        /// <summary>
        /// 波形信息
        /// </summary>
        public WaveInformation WaveDescInfo
        {
            get { return _WaveInfo; }
            set { _WaveInfo = value; }
        }

        public string MAC;

        //最后一条报文中波形数据的长度
        public int LastWaveLength = 0;

        public bool retry = false;

        public System.Threading.Timer checkFullTimer=null;


        public System.DateTime UpLoadDataTime { set; get; }

        /// <summary>
        /// 深度复制对象
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {

            WSWaveInfo waveDate = new WSWaveInfo()
            {
                WGNO = this.WGNO,


                WSNO = this.WSNO,


                WaveType = this.WaveType,

                WaveData = this.WaveData,


                IsFull = this.IsFull,


                LastReceiveTime = this.LastReceiveTime,


                ReceiveDataNumber = this.ReceiveDataNumber,


                CurrentNoDataNumber = this.CurrentNoDataNumber,


                RepeatNumber = this.RepeatNumber,


                IsRepeat = this.IsRepeat,

                UpLoadDataTime=this.UpLoadDataTime,

                WaveDescInfo = new WaveInformation()
                {
                          WSID = this.WaveDescInfo.WSID,
                          SamplingTime = this.WaveDescInfo.SamplingTime,
                          DAQStyle = this.WaveDescInfo.DAQStyle,
                          WaveLength = this.WaveDescInfo.WaveLength,
                          UpperLimit = this.WaveDescInfo.UpperLimit,
                          LowerLimit = this.WaveDescInfo.LowerLimit,
                          AmplitueScaler = this.WaveDescInfo.AmplitueScaler,
                          RMS = this.WaveDescInfo.RMS,
                          PK = this.WaveDescInfo.PK,
                          PPK = this.WaveDescInfo.PPK,
                          GPKC = this.WaveDescInfo.GPKC,
                          TotalWaveNum = this.WaveDescInfo.TotalWaveNum,
                          CurrentWaveNum = this.WaveDescInfo.CurrentWaveNum,
                },


                MAC = this.MAC,

                LastWaveLength = this.LastWaveLength,

                retry = this.retry
            };

            return waveDate;
           
        }

        ///// <summary>
        ///// 深度复制对象
        ///// </summary>
        ///// <returns></returns>
        //public object Clone()
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        object CloneObject;
        //        BinaryFormatter bf = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));
        //        bf.Serialize(ms, this);
        //        ms.Seek(0, SeekOrigin.Begin);
        //        // 反序列化至另一个对象(即创建了一个原对象的深表副本) 
        //        CloneObject = bf.Deserialize(ms);
        //        // 关闭流 
        //        ms.Close();
        //        return CloneObject;
        //    }
        //}

    }


    /// <summary>
    /// 波形信息
    /// </summary>
    [Serializable]
    public class WaveInformation
    {
        public int WSID;
        public DateTime SamplingTime;
        public int DAQStyle;
        public int WaveLength;
        public int UpperLimit;
        public int LowerLimit;
        public float AmplitueScaler;
        public float? RMS;
        public float? PK;
        public float? PPK;
        public float? GPKC;
        public int TotalWaveNum;
        public int CurrentWaveNum;
    }



}
