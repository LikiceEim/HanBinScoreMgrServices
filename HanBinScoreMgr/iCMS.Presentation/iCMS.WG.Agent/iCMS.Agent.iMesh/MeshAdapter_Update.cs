using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iMesh
{
    /// <summary>
    /// 固件分块数据信息
    /// </summary>
    public class tFwBlockDat
    {
        /// <summary>
        /// 当前分块固件大小
        /// </summary>
        public UInt16 size = 0;
        /// <summary>
        /// 固件分块数据
        /// </summary>
        public byte[] data = null;
    }

    /// <summary>
    /// 固件描述信息
    /// </summary>
    public class tFwDescInfo
    {
        /// <summary>
        /// 固件魔术字信息，防止无效固件
        /// </summary>
        public UInt32 u32MagicWord = 0;
        /// <summary>
        /// 更新固件版本号
        /// </summary>
        public tVer verFw = new tVer();
        /// <summary>
        /// 固件总大小
        /// </summary>
        public UInt32 u32Size = 0;
        /// <summary>
        /// 升级过程分块固件最大尺寸
        /// </summary>
        public byte u8BlockSize = 69;
        /// <summary>
        /// 升级过程分块固件个数
        /// </summary>
        public UInt16 u16BlockNum = 0;
        /// <summary>
        /// 固件执行进入点地址
        /// </summary>
        public UInt32 u32EntryPoint;
    }

    /// <summary>
    /// 固件数据信息
    /// </summary>
    public class tFirmware
    {
        /// <summary>
        /// [常量]Manager最大重启次数
        /// </summary>
        private const byte FW_HEADER_SIZE = 19;
        /// <summary>
        /// 固件头，固件描述信息
        /// </summary>
        public tFwDescInfo FwDesc = new tFwDescInfo();
        /// <summary>
        /// 固件分块数据查询字典
        /// UInt16：表示分块编号，从0开始编号
        /// FwBlockDat：对应编号的固件分块数据信息
        /// </summary>
        public Dictionary<UInt16, tFwBlockDat> DicFwBlockDat = null;

        /// <summary>
        /// 复制构造函数
        /// </summary>
        /// <param name="fw">复制的对象</param>
        public tFirmware(tFirmware fw)
        {
            FwDesc.u32MagicWord = fw.FwDesc.u32MagicWord;
            FwDesc.verFw.u8Main = fw.FwDesc.verFw.u8Main;
            FwDesc.verFw.u8Sub = fw.FwDesc.verFw.u8Sub;
            FwDesc.verFw.u8Rev = fw.FwDesc.verFw.u8Rev;
            FwDesc.verFw.u8Build = fw.FwDesc.verFw.u8Build;
            FwDesc.u32Size = fw.FwDesc.u32Size;
            FwDesc.u8BlockSize = fw.FwDesc.u8BlockSize;
            FwDesc.u16BlockNum = fw.FwDesc.u16BlockNum;
            FwDesc.u32EntryPoint = fw.FwDesc.u32EntryPoint;

            DicFwBlockDat = new Dictionary<ushort, tFwBlockDat>();
            for (ushort i = 0; i < fw.DicFwBlockDat.Count; i++)
            {
                tFwBlockDat fwBlkDat = new tFwBlockDat();
                fwBlkDat.size = fw.DicFwBlockDat[i].size;
                fwBlkDat.data = new byte[fwBlkDat.size];
                Array.Copy(fw.DicFwBlockDat[i].data, fwBlkDat.data, fwBlkDat.size);
                DicFwBlockDat.Add(i, fwBlkDat);
            }
        }

        /// <summary>
        /// 由固件文件路径生成固件对象的构造函数
        /// </summary>
        /// <param name="path">固件文件路径</param>
        public tFirmware(string path)
        {
            int bufSize = 300 * 1024;
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufSize);
            BinaryReader reader = new BinaryReader(stream);

            // 解析固件文件头信息
            byte[] data = reader.ReadBytes(FW_HEADER_SIZE);
            FwDesc.u32MagicWord = (UInt32)((data[0] << 24) | (data[1] << 16) | (data[2] << 8) | (data[3]));
            FwDesc.verFw.u8Main = data[4];
            FwDesc.verFw.u8Sub = data[5];
            FwDesc.verFw.u8Rev = data[6];
            FwDesc.verFw.u8Build = data[7];
            FwDesc.u32Size = (UInt32)((data[8] << 24) | (data[9] << 16) | (data[10] << 8) | (data[11]));
            //FwDesc.u16BlockNum != (UInt16)((data[12] << 8) | data[13]);
			FwDesc.u16BlockNum = (UInt16)(FwDesc.u32Size / FwDesc.u8BlockSize);
            if (FwDesc.u32Size % FwDesc.u8BlockSize != 0)
            {
                ++FwDesc.u16BlockNum;
            }
            //FwDesc.u8BlockSize = data[14];
            FwDesc.u32MagicWord = (UInt32)((data[15] << 24) | (data[16] << 16) | (data[17] << 8) | (data[18]));

            // 解析固件数据
            DicFwBlockDat = new Dictionary<ushort, tFwBlockDat>(FwDesc.u16BlockNum);
            data = reader.ReadBytes((int)FwDesc.u32Size);
            for (int i = 0; i < FwDesc.u16BlockNum; i++)
            {
                tFwBlockDat newFwBlockDat = new tFwBlockDat();
                if (i < FwDesc.u16BlockNum - 1)
                {
                    newFwBlockDat.size = FwDesc.u8BlockSize;
                }
                else
                {
                    newFwBlockDat.size = (UInt16)(FwDesc.u32Size - (FwDesc.u16BlockNum - 1) * FwDesc.u8BlockSize);
                }

                newFwBlockDat.data = new byte[newFwBlockDat.size];
                Array.Copy(data, i * FwDesc.u8BlockSize, newFwBlockDat.data, 0, newFwBlockDat.size);

                DicFwBlockDat.Add((UInt16)i, newFwBlockDat);
            }
        }

        /// <summary>
        /// 由固件文件数据生成固件对象的构造函数
        /// </summary>
        /// <param name="fileDat">固件文件数据</param>
        public tFirmware(byte[] fileDat)
        {
            if (fileDat == null || fileDat.Length <= FW_HEADER_SIZE)
                throw new Exception("Firmware data error");

            FwDesc.u32MagicWord = (UInt32)((fileDat[0] << 24) | (fileDat[1] << 16) | (fileDat[2] << 8) | (fileDat[3]));
            FwDesc.verFw.u8Main = fileDat[4];
            FwDesc.verFw.u8Sub = fileDat[5];
            FwDesc.verFw.u8Rev = fileDat[6];
            FwDesc.verFw.u8Build = fileDat[7];
            FwDesc.u32Size = (UInt32)((fileDat[8] << 24) | (fileDat[9] << 16) | (fileDat[10] << 8) | (fileDat[11]));
            //FwDesc.u16BlockNum != (UInt16)((data[12] << 8) | data[13]);
            FwDesc.u16BlockNum = (UInt16)(FwDesc.u32Size / FwDesc.u8BlockSize);
            if (FwDesc.u32Size % FwDesc.u8BlockSize != 0)
            {
                ++FwDesc.u16BlockNum;
            }
            //FwDesc.u8BlockSize = data[14];
            FwDesc.u32EntryPoint = (UInt32)((fileDat[15] << 24) | (fileDat[16] << 16) | (fileDat[17] << 8) | (fileDat[18]));

            // 解析固件数据
            DicFwBlockDat = new Dictionary<ushort, tFwBlockDat>(FwDesc.u16BlockNum);
            for (int i = 0; i < FwDesc.u16BlockNum; i++)
            {
                tFwBlockDat newFwBlockDat = new tFwBlockDat();
                if (i < FwDesc.u16BlockNum - 1)
                {
                    newFwBlockDat.size = FwDesc.u8BlockSize;
                }
                else
                {
                    newFwBlockDat.size = (UInt16)(FwDesc.u32Size - (FwDesc.u16BlockNum - 1) * FwDesc.u8BlockSize);
                }

                newFwBlockDat.data = new byte[newFwBlockDat.size];
                Array.Copy(fileDat, 19 + i * FwDesc.u8BlockSize, newFwBlockDat.data, 0, newFwBlockDat.size);

                DicFwBlockDat.Add((UInt16)i, newFwBlockDat);
            }
        }
    }

    /// <summary>
    /// 升级特定WS的通知事件类型定义
    /// </summary>
    /// <param name="wsMac"></param>
    public delegate void UpdateWsNotify(tMAC wsMac);
    /// <summary>
    /// 升级特定WS的失败事件类型定义
    /// </summary>
    /// <param name="wsMac"></param>
    public delegate void UpdateWsFailed(tMAC wsMac);
    /// <summary>
    /// 升级特定WS的成功事件类型定义
    /// </summary>
    /// <param name="wsMac"></param>
    public delegate void UpdateWsSucces(tMAC wsMac);
    /// <summary>
    /// 升级过程结束事件类型定义
    /// </summary>
    public delegate void UpdateProcessFinished();

    public partial class MeshAdapter
    {   
        /// <summary>
        /// 开始发送升级数据的时刻！
        /// </summary>
        private DateTime StartSendFwdateTime = DateTime.Now;
        /// <summary>
        /// 升级等待WS响应
        /// </summary>
        private int SendupdateSinglePacketGap = int.Parse(System.Configuration.ConfigurationManager.AppSettings["UpdateSinglePacketTimeout"].ToString());
        /// <summary>
        /// 升级等待WS响应
        /// </summary>
        private int waitFwUpdRspTolerance = int.Parse(System.Configuration.ConfigurationManager.AppSettings["UpdatewaitWsRepTimeout"].ToString());
        /// <summary>
        /// 最大重复发送升级次数
        /// </summary>
        private int maxFwResendReqCnt = int.Parse(System.Configuration.ConfigurationManager.AppSettings["UpdateResendCnt"].ToString());
        /// <summary>
        /// 等待WS升级加入网络的时间
        /// </summary>
        private int waitWsOnlineTolerance = int.Parse(System.Configuration.ConfigurationManager.AppSettings["UpdatewaitWsJoinTimeout"].ToString());
        /// <summary>
        /// 组件内部调用“配置网络”命令的响应同步事件
        /// </summary>
        private AutoResetEvent m_asigSetNwCfg = new AutoResetEvent(false);
        /// <summary>
        /// 组件内部调用“重启网络”命令的响应同步事件
        /// </summary>
        private AutoResetEvent m_asigResetSys = new AutoResetEvent(false);
        /// <summary>
        /// 组件内部调用“配置网络”命令的响应超时标志
        /// </summary>
        private volatile bool m_bSetNwCfgTimeout = false;
        /// <summary>
        /// 组件内部调用“重启网络”命令的响应超时标志
        /// </summary>
        private volatile bool m_bResetSysTimeout = false;
        /// <summary>
        /// 组件内部调用“禁言WS”命令的响应同步事件
        /// </summary>
        private AutoResetEvent m_asigMute = new AutoResetEvent(false);
        /// <summary>
        /// 组件内部调用“解言WS”命令的响应同步事件
        /// </summary>
        private AutoResetEvent m_asigDismute = new AutoResetEvent(false);
        /// <summary>
        /// true表示正在禁言，false表示正在使言
        /// </summary>
        private volatile bool m_bMuteOrDismute = false;
        /// <summary>
        /// 表示禁言控制超时
        /// </summary>
        private volatile bool m_bMuteTimeout = false;
        /// <summary>
        /// 表示解言控制超时
        /// </summary>
        private volatile bool m_bDismuteTimeout = false;
        /// <summary>
        /// 升级时期望进行禁言的WS列表
        /// </summary>
        private Dictionary<string, bool> dicExpectedMutedWs = new Dictionary<string, bool>();
        /// <summary>
        /// 升级时，禁言成功的WS列表
        /// </summary>
        private volatile List<string> m_listMutedWs = new List<string>();
        /// <summary>
        /// Manager发送网络重启事件信号
        /// </summary>
        private AutoResetEvent m_asigAutoNwResetEvt = new AutoResetEvent(false);
        /// <summary>
        /// 表示手动构造网络重启通知事件
        /// </summary>
        private volatile bool m_bManualNetworkResetEvent = false;
        /// <summary>
        /// 升级重启标志
        /// </summary>
        private volatile bool m_bUpdateReset = false;
        /// <summary>
        /// 正在升级
        /// </summary>
        private volatile bool m_bUpdating = false;
        /// <summary>
        /// 正在发送升级波形的数据退出升级态
        /// </summary>
        private volatile bool m_bExitUpdat = false;
        /// <summary>
        /// 记录当前正在禁言/解言的WS
        /// </summary>
        private tMAC m_bUpdateMoteMac = new tMAC();
        /// <summary>
        /// 请求升级的WS列表
        /// </summary>
        private List<tMAC> lstUpdWs = null;
        /// <summary>
        /// 上层传入的固件信息
        /// </summary>
        private tFirmware fw = null;
        /// <summary>
        /// 当前正在升级的WS地址
        /// </summary>
        private tMAC curUpdatingWs = null;

        /// <summary>
        /// 等待固件描述信息发送成功并接收到WS的响应
        /// </summary>
        private AutoResetEvent asigFwDescSent = new AutoResetEvent(false);
        /// <summary>
        /// 标识固件描述信息是否成功发送并接收到WS的响应
        /// </summary>
        private bool fwDescSentOK = false;

        /// <summary>
        /// 等待固件数据发送成功
        /// </summary>
        private AutoResetEvent asigFwDatSent = new AutoResetEvent(false);
        /// <summary>
        /// 等待固件升级最终WS的响应
        /// </summary>
        private AutoResetEvent asigFwUpdateResponse = new AutoResetEvent(false);
        /// <summary>
        /// 升级结果
        /// </summary>
        private tSetFwDataResult fwUpdateResult = null;

        private UpdateWsNotify updatingWs = null;
        /// <summary>
        /// 通知上层正在升级的WS
        /// </summary>
        public event UpdateWsNotify UpdatingWs
        {
            add { lock (objectLock) { updatingWs = value; } }
            remove { lock (objectLock) { updatingWs = null; } }
        }
        
        private UpdateWsFailed updatedWsFailed = null;
        /// <summary>
        /// 通知上层升级失败的WS
        /// </summary>
        public event UpdateWsFailed UpdatedWsFailed
        {
            add { lock (objectLock) { updatedWsFailed = value; } }
            remove { lock (objectLock) { updatedWsFailed = null; } }
        }
        
        private UpdateWsSucces updatedWsSucess = null;
        /// <summary>
        /// 通知上层升级成功的WS
        /// </summary>
        public event UpdateWsSucces UpdatedWsSucess
        {
            add { lock (objectLock) { updatedWsSucess = value; } }
            remove { lock (objectLock) { updatedWsSucess = null; } }
        }

        private UpdateProcessFinished updateFinished = null;
        /// <summary>
        /// 通知上层升级过程结束
        /// </summary>
        public event UpdateProcessFinished UpdateFinished
        {
            add { lock (objectLock) { updateFinished = value; } }
            remove { lock (objectLock) { updateFinished = null; } }
        }

        /// <summary>
        /// 下发固件描述信息成功回调函数
        /// </summary>
        /// <param name="result"></param>
        private void setFwDescSucess(tSetFwDescInfoResult result)
        {           
            fwDescSentOK = true;
            asigFwDescSent.Set();
        }

        /// <summary>
        /// 下发固件描述信息失败回调函数
        /// </summary>
        /// <param name="wsMac"></param>
        private void setFwDescFail(tMAC wsMac)
        {           
            fwDescSentOK = false;
            asigFwDescSent.Set();
        }

        /// <summary>
        /// 下发固件数据成功回调函数
        /// </summary>
        /// <param name="result"></param>
        private void setFwDatReault(tSetFwDataResult result)
        {
            fwUpdateResult = result;
            asigFwUpdateResponse.Set();           
        }

        /// <summary>
        /// 对特定WS下发固件描述信息
        /// </summary>
        /// <param name="moteMac">升级的WS</param>
        /// <returns>成功与否</returns>
        private bool SendFwDesc(tMAC moteMac)
        {
            tSetFwDescInfoParam FwDescInfoParam = new tSetFwDescInfoParam();
            FwDescInfoParam.u32MagicWord = fw.FwDesc.u32MagicWord;
            FwDescInfoParam.FwVer.u8Main = fw.FwDesc.verFw.u8Main;
            FwDescInfoParam.FwVer.u8Sub = fw.FwDesc.verFw.u8Sub;
            FwDescInfoParam.FwVer.u8Rev = fw.FwDesc.verFw.u8Rev;
            FwDescInfoParam.FwVer.u8Build = fw.FwDesc.verFw.u8Build;
            FwDescInfoParam.u32FwSize = fw.FwDesc.u32Size;
            FwDescInfoParam.u8MaxBlkSize = fw.FwDesc.u8BlockSize;
            FwDescInfoParam.u32EntryPoint = fw.FwDesc.u32EntryPoint;
            FwDescInfoParam.mac.Assign(moteMac);

            asigFwDescSent.Reset();
            fwDescSentOK = false;
            // 发送固件描述信息
            SetFwDescInfo(FwDescInfoParam, true);
            // 等待固件描述信息应用层响应
            asigFwDescSent.WaitOne();

            return fwDescSentOK;
        }

        /// <summary>
        /// 对特定WS下发固件数据
        /// </summary>
        /// <param name="moteMac">升级的WS</param>
        /// <returns>成功与否</returns>
        private bool SendFwData(tMAC moteMac)
        {
            tSetFwDataParam fwData = new tSetFwDataParam();
            fwData.mac.Assign(moteMac);
            // 等待WS升级完成响应的忍耐时间，单位是ms
            //int waitFwUpdRspTolerance = 50000;
            //int maxFwResendReqCnt = 6;
            double sendoverFwGap = 0;
            int waitFwDataTimeout = maxFwResendReqCnt * cfgAgtReq2MshRepTimeout;   
            StartSendFwdateTime = DateTime.Now;
            for (ushort i = 0; i < fw.DicFwBlockDat.Count; i++)
            {              
                if (m_bExitUpdat)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "WS(" + moteMac .ToHexString()+ ") lost,Update fail!");
                    return false;
                }
                fwData.u8DataPacketSize = (byte)fw.DicFwBlockDat[i].size;
                fwData.u8aData = fw.DicFwBlockDat[i].data;
                fwData.u16BlkIdx = i;

                asigFwDatSent.Reset();
                // 发送固件数据
                SetFwData(fwData, true);
                // 等待数据发送成功
                if (!asigFwDatSent.WaitOne(waitFwDataTimeout))
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "WS(" + moteMac .ToHexString()+ ")"+"SinglePacket Update fail!");
                    return false;
                }                
                TimeSpan tsWaitAppRsp = DateTime.Now - StartSendFwdateTime;
                sendoverFwGap = tsWaitAppRsp.TotalMilliseconds/(i+1);
                if (sendoverFwGap > SendupdateSinglePacketGap)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "WS(" + moteMac.ToHexString() + ")" + "SinglePacket Update Timeout!");
                    return false;
                }             
            }
            byte resendTime = 0;

        RESENT_FW_DAT:
            asigFwUpdateResponse.Reset();
            // 等待WS发送的升级数据响应
            if (!asigFwUpdateResponse.WaitOne(waitFwUpdRspTolerance))
            {
                CommStackLog.RecordInf(enLogLayer.eAdapter, "WS(" + moteMac.ToHexString() + ")" + "Update timeout fail!");
                return false;
            }
            if (fwUpdateResult == null)
            {
                CommStackLog.RecordInf(enLogLayer.eAdapter, "WS(" + moteMac.ToHexString() + ")" + "fwUpdateResult is null!");
                return false;
            }
            // 等到WS发送的升级数据响应
            // 升级成功
            if (fwUpdateResult.u8RC == 0)
            {
                return true;
            }
            // 升级重传
            else if (fwUpdateResult.u8RC == 1)
            {
                // 超过最大重试次数，则退出对本WS的升级
                if (resendTime++ >= maxFwResendReqCnt)
                {
                    CommStackLog.RecordInf(enLogLayer.eAdapter, "WS(" + moteMac.ToHexString() + ")" + "Update resendCnt Fail!");
                    return false;
                }

                for (UInt16 i = 0; i < fwUpdateResult.u16FwBlock.Length; i++)
                {
                    if (m_bExitUpdat)
                    {
                        CommStackLog.RecordInf(enLogLayer.eAdapter, "WS(" + moteMac.ToHexString() + ") lost,Update fail!");
                        return false;
                    }
                    fwData.u8DataPacketSize = (byte)fw.DicFwBlockDat[fwUpdateResult.u16FwBlock[i]].size;
                    fwData.u8aData = fw.DicFwBlockDat[fwUpdateResult.u16FwBlock[i]].data;
                    fwData.u16BlkIdx = fwUpdateResult.u16FwBlock[i];

                    asigFwDatSent.Reset();
                    // 发送固件数据
                    SetFwData(fwData, true);
                    // 等待数据发送成功
                    if (!asigFwDatSent.WaitOne(waitFwDataTimeout))
                    {
                        CommStackLog.RecordInf(enLogLayer.eAdapter, "SinglePacket Update fail!");
                        return false;
                    }
                }               
                goto RESENT_FW_DAT;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 从请求升级队列中删除特定的WS
        /// </summary>
        /// <param name="deleteMote">删除的WS地址</param>
        private void deleteReqUpdatedWs(tMAC deleteMote)
        {
            lock (lstUpdWs)
            {
                foreach (tMAC mote in lstUpdWs)
                {
                    if (mote.isEqual(deleteMote))
                    {
                        lstUpdWs.Remove(mote);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 升级过程处理函数
        /// </summary>
        private void UpdateHandler()
        {
            CommStackLog.RecordInf(enLogLayer.eAdapter, "UpdateHandler running");

            SetFwDescReaultArrived += setFwDescSucess;
            SetFwDescFailed += setFwDescFail;
            SetFwDataReaultArrived += setFwDatReault;

            // 等待WS重新加入网络的忍耐时间，单位是ms
            //int waitWsOnlineTolerance = 600000;
            int updateHandlerSleepTime = 1000;
            int noWsOnlineTime = 0;

            while (m_bUpdating)
            {
                try
                {
                    lock (lstUpdWs)
                    {
                        // 升级队列中无请求的WS则通知上层升级过程结束
                        if (lstUpdWs.Count <= 0)
                        {
                            dismuteWsUpstream();
                            if (updateFinished != null)
                                updateFinished();

                            return;
                        }

                        // 寻找可以升级的WS
                        foreach (tMAC mote in lstUpdWs)
                        {
                            lock (m_dicOnLineWs)
                            {
                                foreach (string onlineWS in m_dicOnLineWs.Keys)
                                {
                                    // 检查已经在线的WS
                                    if (m_dicOnLineWs[onlineWS])
                                    {
                                        tMAC checkWs = new tMAC(onlineWS);
                                        // 当前WS在线，且是上层请求升级中的一员
                                        if (checkWs.isEqual(mote))
                                        {
                                            curUpdatingWs = checkWs;
                                            CommStackLog.RecordInf(enLogLayer.eAdapter, "hit ws");
                                            noWsOnlineTime = 0;
                                            // 寻找到可升级的WS，则结束对在线WS的遍历
                                            break;
                                        }
                                    }
                                }
                            }

                            // 寻找到可升级的WS，则结束对升级队列的遍历
                            if (curUpdatingWs != null)
                                break;
                        }
                    }

                    if (curUpdatingWs != null)
                    {
                        CommStackLog.RecordInf(enLogLayer.eAdapter, "WS(" + curUpdatingWs.ToHexString()+ ")update!");
                        // 通知上层开始升级新的WS
                        if (updatingWs != null)
                            updatingWs(curUpdatingWs);
                        // 下发固件描述信息
                        if (!SendFwDesc(curUpdatingWs))
                        {
                            // 通知上层升级当前WS失败
                            if (updatedWsFailed != null)
                                updatedWsFailed(curUpdatingWs);
                            // 升级失败，从请求升级队列中删除当前WS
                            deleteReqUpdatedWs(curUpdatingWs);
                            curUpdatingWs = null;
                            // 升级当前WS失败，则转为升级下一个WS
                            continue;
                        }
                        // 下发固件数据
                        if (!SendFwData(curUpdatingWs))
                        {
                            m_bExitUpdat = false;
                            // 通知上层升级当前WS失败
                            if (updatedWsFailed != null)
                                updatedWsFailed(curUpdatingWs);
                            // 升级失败，从请求升级队列中删除当前WS
                            deleteReqUpdatedWs(curUpdatingWs);
                            curUpdatingWs = null;
                            // 升级当前WS失败，则转为升级下一个WS
                            continue;
                        }
                        // 通知上层升级当前WS成功
                        if (updatedWsSucess != null)
                            updatedWsSucess(curUpdatingWs);
                        // 升级成功，从请求升级队列中删除当前WS
                        deleteReqUpdatedWs(curUpdatingWs);
                        curUpdatingWs = null;
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Message:" + ex.Message);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "Source: " + ex.Source);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "StackTrace: " + ex.StackTrace);
                    CommStackLog.RecordErr(enLogLayer.eAdapter, "ToString: " + ex.ToString());
                }

                if (noWsOnlineTime >= waitWsOnlineTolerance)
                {
                    lock (lstUpdWs)
                    {
                        // 剩下的WS都是迟迟不能加入网络的WS，通知上层这些WS升级失败
                        foreach (tMAC mote in lstUpdWs)
                        {
                            if (updatedWsFailed != null)
                                updatedWsFailed(mote);
                        }

                        // 清空所有未升级完成的WS
                        lstUpdWs.Clear();
                        dismuteWsUpstream();
                        if (updateFinished != null)
                            updateFinished();

                        return;
                    }
                }

                Thread.Sleep(updateHandlerSleepTime);
                noWsOnlineTime += updateHandlerSleepTime;
            }
        }

        /// <summary>
        /// 在升级前先将网络中的所有WS禁言（禁止上传数据）
        /// </summary>
        /// <returns>禁止上传数据是否成功</returns>
        private bool muteWsUpstream()
        {
            int iWsCnt = 0;
            lock (m_dicOnLineWs) { iWsCnt = m_dicOnLineWs.Count; }
            if (iWsCnt == 0)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "No online WS");
                return false;
            }
            else
            {
                lock (m_dicOnLineWs)
                {
                    foreach (string ws in m_dicOnLineWs.Keys)
                    {
                        if (m_dicOnLineWs[ws])
                        {
                            dicExpectedMutedWs.Add(ws, true);
                            CommStackLog.RecordInf(enLogLayer.eAdapter, ws + " online");
                        }
                        else
                            CommStackLog.RecordInf(enLogLayer.eAdapter, ws + " offline");
                    }
                }

                // 进入禁言期
                m_bMuteOrDismute = true;
                // 清空已控制WS列表
                m_listMutedWs.Clear();
                lock (m_listMutedWs)
                {
                    foreach (string key in dicExpectedMutedWs.Keys)
                    {
                        tCtlWsUpstrParam param = new tCtlWsUpstrParam();
                        param.mac.Assign(new tMAC(key));
                        param.u8Control = 1;
                        m_asigMute.Reset();
                        m_bMuteTimeout = false;
                        // 通知WS暂不上传波形数据
                        CtlWsUpstr(param);
                        // 判定是否迷失
                        if (m_asigMute.WaitOne(SYC_WAIT_MAX_TIME))
                        {
                            if (m_bMuteTimeout)
                            {
                                m_asigMute.Reset();
                                m_bMuteTimeout = false;
                                CommStackLog.RecordErr(enLogLayer.eAdapter, "Muted(" + key + ") timeout");
                            }
                            else
                            {
                                m_asigMute.Reset();
                                m_bMuteTimeout = false;
                                // 记录已经禁言成功的WS MAC地址
                                m_listMutedWs.Add(key);
                                CommStackLog.RecordInf(enLogLayer.eAdapter, "Muted(" + key + ") ok");
                            }
                        }
                        else
                        {
                            m_asigMute.Reset();
                            m_bMuteTimeout = false;
                            // 迷失的情况也算是禁言成功
                            CommStackLog.RecordInf(enLogLayer.eAdapter, "Muted(" + key + ") lost");
                        }
                    }

                    dicExpectedMutedWs.Clear();
                }

                // 如果没有一个WS能够控制成功，则告诉上层不能升级
                if (m_listMutedWs.Count == 0)
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// 新的升级方案中，在升级后先将网络中的所有WS解言（允许上传数据）
        /// </summary>
        /// <returns>允许上传数据是否成功</returns>
        private bool dismuteWsUpstream()
        {
            int iWsCnt = 0;
            lock (m_dicOnLineWs) { iWsCnt = m_dicOnLineWs.Count; }
            if (iWsCnt == 0)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "No online WS");
                return false;
            }
            else
            {
                // 打印当前在线WS集合，方便问题定位
                lock (m_dicOnLineWs)
                {
                    foreach (string ws in m_dicOnLineWs.Keys)
                    {
                        if (m_dicOnLineWs[ws])
                            CommStackLog.RecordInf(enLogLayer.eAdapter, ws + " online");
                        else
                            CommStackLog.RecordInf(enLogLayer.eAdapter, ws + " offline");
                    }
                }

                // 进入使言期
                m_bMuteOrDismute = false;
                List<string> lstDismutedSuccess = new List<string>();
                lock (m_listMutedWs)
                {
                    foreach (string key in m_listMutedWs)
                    {
                        lock (m_dicOnLineWs)
                        {
                            try
                            {
                                if (!m_dicOnLineWs[key])
                                {
                                    continue;
                                }
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                        }

                        tCtlWsUpstrParam param = new tCtlWsUpstrParam();
                        tMAC mac = new tMAC(key);
                        param.mac.Assign(mac);
                        param.u8Control = 0;

                        m_asigDismute.Reset();
                        m_bDismuteTimeout = false;

                        CtlWsUpstr(param);
                        // 判定是否迷失
                        if (m_asigDismute.WaitOne(SYC_WAIT_MAX_TIME))
                        {
                            if (m_bDismuteTimeout)
                            {
                                m_asigDismute.Reset();
                                m_bDismuteTimeout = false;
                                CommStackLog.RecordErr(enLogLayer.eAdapter, "Dismuted(" + key + ") timeout");
                                continue;
                            }
                            else
                            {
                                m_asigDismute.Reset();
                                m_bDismuteTimeout = false;
                                CommStackLog.RecordInf(enLogLayer.eAdapter, "Dismuted(" + key + ") ok");
                                lstDismutedSuccess.Add(key);
                            }
                        }
                        else
                        {
                            m_asigDismute.Reset();
                            m_bDismuteTimeout = false;
                            CommStackLog.RecordInf(enLogLayer.eAdapter, "Dismuted(" + key + ") lost");
                        }
                    }

                    // 从禁言列表中删除已经成功解言的WS
                    foreach (string key in lstDismutedSuccess)
                    {
                        m_listMutedWs.Remove(key);
                    }
                    lstDismutedSuccess.Clear();

                    // 如果目前还有未解言的WS，则很有可能是最后一个升级的WS，最多再等待10分钟
                    if (m_listMutedWs.Count != 0)
                    {
                        int waitWsOnlineTolerance = 600000;
                        int waitWsOnlineCheckTime = 0;
                        while (waitWsOnlineCheckTime < waitWsOnlineTolerance)
                        {
                            // 每间隔1秒钟检查是否有新的WS加入
                            Thread.Sleep(1000);
                            waitWsOnlineCheckTime += 1000;
                            // 检查是否未解言的WS加入网络
                            foreach (string key in m_listMutedWs)
                            {
                                lock (m_dicOnLineWs)
                                {
                                    try
                                    {
                                        if (!m_dicOnLineWs[key])
                                        {
                                            continue;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        continue;
                                    }
                                }

                                // 一旦未解言成功的WS加入网络，则进行解言
                                tCtlWsUpstrParam param = new tCtlWsUpstrParam();
                                param.mac.Assign(new tMAC(key));
                                param.u8Control = 0;
                                m_asigDismute.Reset();
                                m_bDismuteTimeout = false;
                                CtlWsUpstr(param);
                                // 判定是否迷失
                                if (m_asigDismute.WaitOne(SYC_WAIT_MAX_TIME))
                                {
                                    if (m_bDismuteTimeout)
                                    {
                                        m_asigDismute.Reset();
                                        m_bDismuteTimeout = false;
                                        CommStackLog.RecordErr(enLogLayer.eAdapter, "Dismuted(" + key + ") timeout");
                                        continue;
                                    }
                                    else
                                    {
                                        m_asigDismute.Reset();
                                        m_bDismuteTimeout = false;
                                        CommStackLog.RecordInf(enLogLayer.eAdapter, "Dismuted(" + key + ") ok");
                                        lstDismutedSuccess.Add(key);
                                    }
                                }
                                else
                                {
                                    m_asigDismute.Reset();
                                    m_bDismuteTimeout = false;
                                    CommStackLog.RecordInf(enLogLayer.eAdapter, "Dismuted(" + key + ") lost");
                                }
                            }

                            // 从禁言列表中删除已经成功解言的WS
                            foreach (string key in lstDismutedSuccess)
                            {
                                m_listMutedWs.Remove(key);
                            }
                            lstDismutedSuccess.Clear();

                            // 所有该解言的WS都解言完成，退出解言过程
                            if (m_listMutedWs.Count == 0)
                                return true;
                        }

                        if (waitWsOnlineCheckTime >= waitWsOnlineTolerance)
                            return false;
                    }
                }

                return true;
            }
        }

        /// <summary>
        /// 进行WS固件升级前的“前奏”工作
        /// </summary>
        public bool PreludeUpdate()
        {
            m_asigSetNwCfg.Reset();
            m_asigResetSys.Reset();
            m_bSetNwCfgTimeout = false;
            m_bResetSysTimeout = false;

            CommStackLog.RecordInf(enLogLayer.eAdapter, "PreludeUpdate");

            m_bUpdating = true;
            // 暂停心跳
            if (!stopHeartbeat())
                return false;

            if (!muteWsUpstream())
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "muteWsUpstream failed");
                // 启动心跳
                startHeartbeat();
                return false;
            }

            // 设置双向主干网模式
            SetBackboneMode(enBBMode.Bidirectional, 2, true);
            m_asigSetNwCfg.WaitOne();
            // 设置主干网超时
            if (m_bSetNwCfgTimeout)
            {
                m_bSetNwCfgTimeout = false;
                CommStackLog.RecordErr(enLogLayer.eAdapter, "SetBackboneMode failed");
                // 通知上层升级失败
                startHeartbeat();
                return false;
            }

            // 重启网络，以使设置生效
            ResetSystem(true);
            m_asigResetSys.WaitOne();
            // 重启网络超时
            if (m_bResetSysTimeout)
            {
                m_bResetSysTimeout = false;
                CommStackLog.RecordErr(enLogLayer.eAdapter, "ResetSystem failed");
                // 启动心跳
                startHeartbeat();
                return false;
            }

            m_bUpdateReset = true;

            lock (m_dicOnLineWs)
            {
                for (int i = 0; i < m_dicOnLineWs.Count; i++)
                {
                    var item = m_dicOnLineWs.ElementAt(i);
                    if(item.Value)
                        m_dicOnLineWs[item.Key] = false;
                }
            }

            CommStackLog.RecordInf(enLogLayer.eAdapter, "PreludeUpdate finish");
            return true;
        }

        /// <summary>
        /// 进行WS固件升级
        /// </summary>
        /// <param name="macList">请求升级的WS列表</param>
        /// <param name="firmware">请求升级的固件信息</param>
        /// <returns>请求成功与否</returns>
        public bool Update(List<tMAC> macList, tFirmware firmware)
        {
            if (macList == null
                || macList.Count == 0
                || firmware == null
                || firmware.DicFwBlockDat == null
                || firmware.DicFwBlockDat.Count == 0)
            {
                return false;
            }

            try
            {
                // 复制一份升级mote列表
                lstUpdWs = new List<tMAC>();
                foreach (tMAC mote in macList)
                {
                    tMAC addMote = new tMAC();
                    addMote.Assign(mote);
                    lstUpdWs.Add(addMote);
                }

                // 复制一份固件信息
                fw = new tFirmware(firmware);

                // 构造升级线程
                Thread threadUpdateWorker = new Thread(UpdateHandler);
                threadUpdateWorker.Name = "UpdateHandler";
                threadUpdateWorker.Priority = ThreadPriority.AboveNormal;
                // 线程升级线程
                threadUpdateWorker.Start();

                return true;
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "Message:" + ex.Message);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "Source: " + ex.Source);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "StackTrace: " + ex.StackTrace);
                CommStackLog.RecordErr(enLogLayer.eAdapter, "ToString: " + ex.ToString());

                return false;
            }
        }

        /// <summary>
        /// 进行WS固件升级前的“终曲”工作
        /// </summary>
        public bool PostludeUpdate()
        {
            m_asigSetNwCfg.Reset();
            m_asigResetSys.Reset();
            m_bSetNwCfgTimeout = false;
            m_bResetSysTimeout = false;

            CommStackLog.RecordInf(enLogLayer.eAdapter, "PostludeUpdate");
            /*
            if (!dismuteWsUpstream())
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "allowUpstream failed");
                return false;
            }*/

            // 关闭双向主干网模式
            SetBackboneMode(enBBMode.Off, 1);
            m_asigSetNwCfg.WaitOne();
            // 关闭主干网超时
            if (m_bSetNwCfgTimeout)
            {
                m_bSetNwCfgTimeout = false;
                CommStackLog.RecordErr(enLogLayer.eAdapter, "SetBackboneMode failed");
                return false;
            }
            // 重启网络，以使设置生效
            ResetSystem(true);
            m_asigResetSys.WaitOne();

            // 重启网络超时
            if (m_bResetSysTimeout)
            {
                m_bResetSysTimeout = false;
                CommStackLog.RecordErr(enLogLayer.eAdapter, "ResetSystem failed");
                return false;
            }

            lock (m_dicOnLineWs)
            {
                for (int i = 0; i < m_dicOnLineWs.Count; i++)
                {
                    var item = m_dicOnLineWs.ElementAt(i);
                    if (item.Value)
                        m_dicOnLineWs[item.Key] = false;
                }
            }

            m_bUpdating = false;
            // 启动心跳
            startHeartbeat();

            return true;
        }
    }
}
