using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMesh
{
    /// <summary>
    /// 系统应用层主功能码定义
    /// </summary>
    public enum enAppMainCMD
    {
        /// <summary>
        /// 通知类报文主功能码
        /// </summary>
        eNotify = 0x00,
        /// <summary>
        /// 设置类报文主功能码
        /// </summary>
        eSet = 0x01,
        /// <summary>
        /// 获取类报文主功能码
        /// </summary>
        eGet = 0x02,
        /// <summary>
        /// 恢复出厂设置类报文主功能码
        /// </summary>
        eRestore = 0x03,
        /// <summary>
        /// 重启类报文主功能码
        /// </summary>
        eReset = 0x04,
        /// <summary>
        /// 固件更新类报文主功能码
        /// </summary>
        eUpdate = 0x05,
        /// <summary>
        /// 无效的应用层主功能码
        /// </summary>
        eNull = 0xFF,
    }
    /// <summary>
    /// 系统应用层通知类子功能码定义
    /// </summary>
    public enum enAppNotifySubCMD
    {
        /// <summary>
        /// WS自描述报告
        /// </summary>
        eSelfReport = 0x00,
        /// <summary>
        /// WS健康报告
        /// </summary>
        eHealthReport = 0x01,
        /// <summary>
        /// 波形数据描述信息
        /// </summary>
        eWaveDesc   = 0x03,
        /// <summary>
        /// 波形数据信息
        /// </summary>
        eWaveData   = 0x04,
        /// <summary>
        /// 特征值数据信息
        /// </summary>
        eEigenVal   = 0x06,
        /// <summary>
        /// 启停机
        /// </summary>
        eRevStop    = 0x08,
        /// <summary>
        /// 温度、电池电压数据
        /// </summary>
        eTmpVol     = 0x0A,
        /// <summary>
        /// LQ值
        /// </summary>
        eLQ         = 0x0B,

    }
    /// <summary>
    /// 系统应用层设置类子功能码定义
    /// </summary>
    public enum enAppSetSubCMD
    {
        /// <summary>
        /// 时间校准参数
        /// </summary>
        eTimeCali   = 0x01,
        /// <summary>
        /// WSID
        /// </summary>
        eWSID       = 0x02,
        /// <summary>
        /// Network ID
        /// </summary>
        eNetworkID  = 0x03,
        /// <summary>
        /// 测量定义
        /// </summary>
        eMeasDef    = 0x04,
        /// <summary>
        /// SN串码
        /// </summary>
        eSn         = 0x06,
        /// <summary>
        /// 传感器校准参数
        /// </summary>
        eCaliCoeff  = 0x07,
        /// <summary>
        /// AD采集截止电压
        /// </summary>
        eADCloseVolt= 0x08,
        /// <summary>
        /// 配置启停机
        /// </summary>
        eRevStop    = 0x09,
        /// <summary>
        /// 配置触发上传
        /// </summary>
        eTrigParam  = 0x0A,
        /// <summary>
        /// 配置ws路由模式
        /// </summary>
        eWsRouteMode= 0x0B,
        /// <summary>
        /// 配置ws路由模式
        /// </summary>
        eWsDebugMode = 0x0C,
    }
    /// <summary>
    /// 系统应用层获取类子功能码定义
    /// </summary>
    public enum enAppGetSubCMD
    {
        /// <summary>
        /// WS自描述报告
        /// </summary>
        eSelfReport = 0x00,
        /// <summary>
        /// 测量定义
        /// </summary>
        eMeasDef = 0x04,
        /// <summary>
        /// SN串码
        /// </summary>
        eSn          = 0x06,
        /// <summary>
        /// 传感器校准参数
        /// </summary>
        eCaliCoeff   = 0x07,
        /// <summary>
        /// AD采集截止电压
        /// </summary>
        eADCloseVolt = 0x08,
        /// <summary>
        /// 获取启停机
        /// </summary>
        eRevStop     = 0x09,
        /// <summary>
        /// 获取触发上传
        /// </summary>
        eTrigParam   = 0x0A,
        /// <summary>
        ///  获取ws路由模式
        /// </summary>
        eWsRouteMode = 0x0B,
    }
    /// <summary>
    /// 系统应用层恢复类子功能码定义
    /// </summary>
    public enum enAppRestoreSubCMD
    {
        eWS = 0x01,
        eWG = 0x02,
    }
    /// <summary>
    /// 系统应用层重启类子功能码定义
    /// </summary>
    public enum enAppResetSubCMD
    {
        eWS = 0x01,
        eWG = 0x02,
    }
    /// <summary>
    /// 系统应用层升级类子功能码定义
    /// </summary>
    public enum enAppUpdateSubCMD
    {
        /// <summary>
        /// 固件描述信息
        /// </summary>
        eFwDesc = 0x01,
        /// <summary>
        /// 固件数据信息
        /// </summary>
        eFwData = 0x02,
        /// <summary>
        /// 控制类信息
        /// </summary>
        eControl = 0x03,
    }
    /// <summary>
    /// WS运行状态
    /// </summary>
    public enum enMeshWsRunState
    {
        // 初始化状态
        eIni = 0x01,
        // 连接状态
        eCon = 0x02,
        // 会话状态
        eSess = 0x03,
        // 采集状态
        eDaq = 0x04,
        // 升级状态
        eUpt = 0x05,
    } 
    /// <summary>
    /// WS唤醒方式
    /// </summary>
    public enum enWsWkupMode
    {
        eReboot = 0x00, // 重启唤醒
        eRTCA   = 0x01,   // RTC定时唤醒
        eINT    = 0x02,    // 外部中断唤醒
    }
    /// <summary>
    /// Mote重启原因
    /// </summary>
    public enum enMoteBoot
    {
        eMoteOper       = 0x00,
        eMoteBootEvent  = 0x01,
        eMoteJoinFail   = 0x02,
        eSendToFail     = 0x03,
        eOther          = 0x04,
    }
    /// <summary>
    /// WS采集方式
    /// </summary>
    public enum enMeshWsDaqMode
    {
        eTiming                   = 0x00,  // 定时采集
        eImmediate                = 0x01,  // 即时采集
        eTimingSynchronization    = 0x02,  // 定时同步
        eTrigger                  = 0x03,  // 触发式采集
    }
    
    /// <summary>
    /// WS采集方式
    /// </summary>
    public enum enSensorType
    {
        eVib = 0x01,  // 振动
        eTmp = 0x02,  // 温度
    }

    /// <summary>
    /// 振动传感器其他功能定义
    /// </summary>
    public enum enVibFuncMap
    {
        eFacilitySync = 0x01,  // 局部同步采集
        eMeshSync     = 0x02,  // 全网同步采集       
    }

    /// <summary>
    /// 振动传感器支持的波形类型
    /// </summary>
    public enum enVibWvTMap
    {
        eAcc     = 0x01,  // 加速度波形
        eVel     = 0x02,  // 速度波形
        eDsp     = 0x04,  // 位移波形
        eAccEvp  = 0x08,  // 加速度包络波形
        eLQ      = 0x10,  // LQ波形
        eRevStop = 0x20,  // 启停机波形
    }

    /// <summary>
    /// 振动传感器支持的特征值类型定义
    /// </summary>
    public enum enVibEvTMap
    {
        eRMS  = 0x0001,  // 有效值(RMS)
        ePK   = 0x0002,  // 峰值(PK)
        ePPK  = 0x0004,  // 峰峰值(PPK)
        ePKC  = 0x0008,  // 地毯值(PKC)
        eMEAN = 0x0010,  // 均值(MEAN)
        eLPE  = 0x0020,  // 低频能量(LPE)
        eMPE  = 0x0040,  // 中频能量(MPE)
        eHPE  = 0x0080,  // 高频能量(HPE)
    }

    /// <summary>
    /// 触发上传是否开启
    /// </summary>
    public enum enMeshEnableFun
    {
        eCloseFun   = 0x00,  // 关闭功能
        eOpenOwnFun = 0x01,  // 特征值超限，只触发所属波形的上传；
        eOpenALLFun = 0x03,  // 特征值超限，会触发所有其他波形上传；
    }
    /// <summary>
    /// 触发上传的范围
    /// </summary>
    public enum enTriggerRange
    {
        eTriggerOwn = 0x00,  // 特征值超限，只触发所属波形的上传；
        eTriggerAll = 0x01,  // 特征值超限，会触发所有其他波形上传
    }
    /// <summary>
    /// ws的路由模式使能
    /// </summary>
    public enum enMeshEnableRoute
    {
        eNoRoute = 0x00,  // 路由功能禁止
        eRoute   = 0x01,     // 路由功能使能
    }
    /// <summary>
    /// ws的路由模式使能
    /// </summary>
    public enum enEnableDebug
    {
        eNoDebug = 0x00,  // 调试功能禁止
        eDebug   = 0x01,     //调试功能使能
    }
    /// <summary>
    /// 测量定义类型
    /// </summary>
    public enum enMeasDefType
    {
        eNulWaveform  = 0x00,    // 
        eAccWaveform  = 0x01,    // 加速度波形
        eVelWaveform  = 0x02,    // 速度波形
        eDspWaveform  = 0x03,    // 位移波形
        eAccEnvelope  = 0x04,    // 加速度包络波形
        eLQform       = 0x05,    // LQ
        eRevStopform  = 0x06,    // 启停机
        eOriginalWave = 0x07,    // 原始波形
        eAppRevStopform = 0x08,  //应用侧的启停机
    }
    /// <summary>
    /// 加速度特征值类型
    /// </summary>
    public enum AccWvEvDef
    {
        eRMS = 0x01,           // 有效值
        ePK  = 0x02,           // 峰值
    }

    /// <summary>
    /// 速度特征值类型
    /// </summary>
    public enum VelWvEvDef
    {
        eRMS = 0x01,           // 有效值
        eLPE = 0x02,           // 低频能量
        eMPE = 0x04,           // 中频能量
        eHPE = 0x08,           // 高频能量
    }

    /// <summary>
    /// 位移特征值类型
    /// </summary>
    public enum DspWvEvDef
    {
        ePPK  = 0x01,           // 峰峰值
    }

    /// <summary>
    /// 加速度包络特征值类型
    /// </summary>
    public enum AevWvEvDef
    {
        ePK   = 0x01,           // 峰值
        ePKC  = 0x02,           // 地毯值
        eMEAN = 0x04,           // 均值
    }

    /// <summary>
    /// LQ特征值类型
    /// </summary>
    public enum LqWvEvDef
    {
        eRMS = 0x01,           // 有效值
    }

    /// <summary>
    /// 启停机特征值类型
    /// </summary>
    public enum SsWvEvDef
    {
        ePK  = 0x01,           // 峰值
    }

   
    /// <summary>
    /// 
    /// </summary>
    public class tSN
    {
        private const byte MAX_LEN = 71;
        public byte u8SnLen { get; set; }

        public byte[] u8aData = new byte[MAX_LEN];
    }

    /// <summary>
    /// 版本号
    /// </summary>
    public class tVer
    {
        // 主版本号
        public byte u8Main { get; set; }
        // 子版本号
        public byte u8Sub { get; set; }
        // 修订版本号
        public byte u8Rev { get; set; }
        // 构建版本号
        public byte u8Build { get; set; }
    }

    /// <summary>
    /// 定时采集间隔时间
    /// </summary>
    public class tTimingTime
    {
        // 时
        public byte u8Hour { get; set; }
        // 分
        public byte u8Min { get; set; }
    }

    /// <summary>
    /// RTC时间
    /// </summary>
    public class tRtcTime
    {
        // 时
        public byte u8Hour { get; set; }
        // 分
        public byte u8Min { get; set; }
        // 秒
        public byte u8Sec { get; set; }
    }

    /// <summary>
    /// 采集时间
    /// </summary>
    public class tDateTime
    {
        // 年
        public byte u8Year { get; set; }
        // 月
        public byte u8Month { get; set; }
        // 日
        public byte u8Day { get; set; }
        // 时
        public byte u8Hour { get; set; }
        // 分
        public byte u8Min { get; set; }
        // 秒
        public byte u8Sec { get; set; }
    }


    /// <summary>
    /// 加速度波形特征值类型定义
    /// </summary>
    public class tAccWvEvDef
    {
        public bool bAccWaveRMSValid = false;
        public float fAccRMSValue = 0.0f;
        public bool bAccWavePKValid = false;
        public float fAccPKValue = 0.0f;
        public int Len = 0;

        public void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0 || stream.Length <= offset)
                    throw new Exception("tAccWvEvDef.Serialize take invalid parameter");

                stream[offset] = (byte)enMeasDefType.eAccWaveform;
                if (bAccWaveRMSValid)
                    stream[offset] = (byte)(stream[offset] | (byte)(AccWvEvDef.eRMS) << 4);
                if (bAccWavePKValid)
                    stream[offset] = (byte)(stream[offset] | (byte)(AccWvEvDef.ePK) << 4);
                Len++;

                if (bAccWaveRMSValid)
                {
                    DataTypeConverter.FloatToMeshByteArr(fAccRMSValue, stream, offset + Len);
                    Len += 4;
                }
                if (bAccWavePKValid)
                {
                    DataTypeConverter.FloatToMeshByteArr(fAccPKValue, stream, offset + Len);
                    Len += 4;
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }

        public void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0 || stream.Length <= offset)
                    throw new Exception("tAccWvEvDef.Unserialize take invalid parameter");

                bAccWaveRMSValid = ((stream[offset + Len]>>4 & (byte)AccWvEvDef.eRMS) != 0x00) ? true : false;
                bAccWavePKValid  = ((stream[offset + Len]>>4 & (byte)AccWvEvDef.ePK) != 0x00) ? true : false;
                Len++;
                if (bAccWaveRMSValid)
                {
                    fAccRMSValue = DataTypeConverter.MeshByteArrToFloat(stream, offset + Len);
                    Len = Len + 4;
                }
                if (bAccWavePKValid)
                {
                    fAccPKValue = DataTypeConverter.MeshByteArrToFloat(stream, offset + Len);
                    Len = Len + 4;
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    /// <summary>
    /// 速度波形类型定义
    /// </summary>
    public class tVelWvEvDef
    {
        public bool bVelWaveRMSValid = false;
        public float fVelRMSValue = 0.0f;
        public bool bVelWaveLPEValid = false;
        public float fVelLPEValue = 0.0f;
        public bool bVelWaveMPEValid = false;
        public float fVelMPEValue = 0.0f;
        public bool bVelWaveHPEValid = false;
        public float fVelHPEValue = 0.0f;
        public int Len = 0;

        public void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0 || stream.Length <= offset)
                    throw new Exception("tVelWvEvDef.Serialize take invalid parameter");

                stream[offset] = (byte)enMeasDefType.eVelWaveform;
                if (bVelWaveRMSValid)
                    stream[offset] = (byte)(stream[offset] | (byte)(VelWvEvDef.eRMS) << 4);
                if (bVelWaveLPEValid == true)
                    stream[offset] = (byte)(stream[offset] | (byte)(VelWvEvDef.eLPE) << 4);
                if (bVelWaveMPEValid == true)
                    stream[offset] = (byte)(stream[offset] | (byte)(VelWvEvDef.eMPE) << 4);
                if (bVelWaveHPEValid == true)
                    stream[offset] = (byte)(stream[offset] | (byte)(VelWvEvDef.eHPE) << 4);
                Len++;

                if (bVelWaveRMSValid)
                {
                    DataTypeConverter.FloatToMeshByteArr(fVelRMSValue, stream, offset + Len);
                    Len += 4;
                }
                if (bVelWaveLPEValid == true)
                {
                    DataTypeConverter.FloatToMeshByteArr(fVelLPEValue, stream, offset + Len);
                    Len += 4;
                }
                if (bVelWaveMPEValid == true)
                {
                    DataTypeConverter.FloatToMeshByteArr(fVelMPEValue, stream, offset + Len);
                    Len += 4;
                }
                if (bVelWaveHPEValid == true)
                {
                    DataTypeConverter.FloatToMeshByteArr(fVelHPEValue, stream, offset + Len);
                    Len += 4;
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }

        public void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0 || stream.Length <= offset)
                    throw new Exception("tVelWvEvDef.Unserialize take invalid parameter");

                bVelWaveRMSValid = ((stream[offset + Len]>>4 & (byte)VelWvEvDef.eRMS) != 0x00) ? true : false;
                bVelWaveLPEValid = ((stream[offset + Len]>>4 & (byte)VelWvEvDef.eLPE) != 0x00) ? true : false;
                bVelWaveMPEValid = ((stream[offset + Len]>>4 & (byte)VelWvEvDef.eMPE) != 0x00) ? true : false;
                bVelWaveHPEValid = ((stream[offset + Len]>>4 & (byte)VelWvEvDef.eHPE) != 0x00) ? true : false;
                Len++;

                if (bVelWaveRMSValid)
                {
                    fVelRMSValue = DataTypeConverter.MeshByteArrToFloat(stream, offset + Len);
                    Len = Len + 4;
                }
                if (bVelWaveLPEValid)
                {
                    fVelLPEValue = DataTypeConverter.MeshByteArrToFloat(stream, offset + Len);
                    Len = Len + 4;
                }
                if (bVelWaveMPEValid)
                {
                    fVelMPEValue = DataTypeConverter.MeshByteArrToFloat(stream, offset + Len);
                    Len = Len + 4;
                }
                if (bVelWaveHPEValid)
                {
                    fVelHPEValue = DataTypeConverter.MeshByteArrToFloat(stream, offset + Len);
                    Len = Len + 4;
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    /// <summary>
    /// 位移波形类型定义
    /// </summary>
    public class tDspWvEvDef
    {
        // PKPK有效
        public bool bDspWavePKPKValid = false;
        public float fDspPKPKValue = 0.0f;
        public int Len = 0;

        public void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0 || stream.Length <= offset)
                    throw new Exception("tDspWvEvDef.Serialize take invalid parameter");

                stream[offset] = (byte)enMeasDefType.eDspWaveform;
                if (bDspWavePKPKValid)
                {
                    stream[offset] = (byte)(stream[offset] | (byte)(DspWvEvDef.ePPK) << 4);
                }
                Len++;

                if (bDspWavePKPKValid)
                {
                    DataTypeConverter.FloatToMeshByteArr(fDspPKPKValue, stream, offset + Len);
                    Len += 4;
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }

        public void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0 || stream.Length <= offset)
                    throw new Exception("tDspWvEvDef.Unserialize take invalid parameter");

                bDspWavePKPKValid = ((stream[offset + Len]>>4 & (byte)DspWvEvDef.ePPK) != 0x00) ? true : false;
                Len++;

                if (bDspWavePKPKValid)
                {
                    fDspPKPKValue = DataTypeConverter.MeshByteArrToFloat(stream, offset + Len);
                    Len = Len + 4;
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    /// <summary>
    /// 包络波形类型定义
    /// </summary>
    public class tAccEnvWvEvDef
    {
        public bool bAccEnvWavePKValid = false;
        public float fAccEnvPKValue = 0.0f;
        public bool bAccEnvWavePKCValid = false;
        public float fAccEnvPKCValue = 0.0f;
        public bool bAccEnvWaveMEANValid = false;
        public float fAccEnvMEANValue = 0.0f;
        public int Len = 0;

        public void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0 || stream.Length <= offset)
                    throw new Exception("tAccEnvWvEvDef.Serialize take invalid parameter");

                stream[offset] = (byte)enMeasDefType.eAccEnvelope;
                if (bAccEnvWavePKValid)
                    stream[offset] = (byte)(stream[offset] | (byte)(AevWvEvDef.ePK) << 4);
                if (bAccEnvWavePKCValid)
                    stream[offset] = (byte)(stream[offset] | (byte)(AevWvEvDef.ePKC) << 4);
                if (bAccEnvWaveMEANValid)
                    stream[offset] = (byte)(stream[offset] | (byte)(AevWvEvDef.eMEAN) << 4);
                Len++;

                if (bAccEnvWavePKValid)
                {
                    DataTypeConverter.FloatToMeshByteArr(fAccEnvPKValue, stream, offset + Len);
                    Len += 4;
                }
                if (bAccEnvWavePKCValid)
                {
                    DataTypeConverter.FloatToMeshByteArr(fAccEnvPKCValue, stream, offset + Len);
                    Len += 4;
                }
                if (bAccEnvWaveMEANValid)
                {
                    DataTypeConverter.FloatToMeshByteArr(fAccEnvMEANValue, stream, offset + Len);
                    Len += 4;
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }

        public void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                bAccEnvWavePKValid = ((stream[offset]>>4 & (byte)AevWvEvDef.ePK) != 0x00) ? true : false;
                bAccEnvWavePKCValid = ((stream[offset]>>4 & (byte)AevWvEvDef.ePKC) != 0x00) ? true : false;
                bAccEnvWaveMEANValid = ((stream[offset]>>4 & (byte)AevWvEvDef.eMEAN) != 0x00) ? true : false;
                Len++;

                if (bAccEnvWavePKValid)
                {
                    fAccEnvPKValue = DataTypeConverter.MeshByteArrToFloat(stream, offset + Len);
                    Len = Len + 4;
                }
                if (bAccEnvWavePKCValid)
                {
                    fAccEnvPKCValue = DataTypeConverter.MeshByteArrToFloat(stream, offset + Len);
                    Len = Len + 4;
                }
                if (bAccEnvWaveMEANValid)
                {
                    fAccEnvMEANValue = DataTypeConverter.MeshByteArrToFloat(stream, offset + Len);
                    Len = Len + 4;
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    /// <summary>
    /// LQ波形类型定义
    /// </summary>
    public class tLQWvEvDef
    {
        //LQ特征值是否有效
        public bool bLQWaveRMSValid = false;
        public float fLQRMSValue = 0.0f;
        public int Len = 0;

        public void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0 || stream.Length <= offset)
                    throw new Exception("tLQWvEvDef.Serialize take invalid parameter");

                stream[offset] = (byte)enMeasDefType.eLQform;
                if (bLQWaveRMSValid)
                    stream[offset] = (byte)(stream[offset] | (byte)(LqWvEvDef.eRMS) << 4);
                Len++;

                if (bLQWaveRMSValid)
                {
                    DataTypeConverter.FloatToMeshByteArr(fLQRMSValue, stream, offset + Len);
                    Len += 4;
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }

        public void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                bLQWaveRMSValid = ((stream[offset]>>4 & (byte)LqWvEvDef.eRMS) != 0x00) ? true : false;
                Len++;

                if (bLQWaveRMSValid)
                {
                    fLQRMSValue = DataTypeConverter.MeshByteArrToFloat(stream, offset + Len);
                    Len += 4;
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    /// <summary>
    /// 启停机波形类型定义
    /// </summary>
    public class tRevStopWvEvDef
    {
        public bool bRevStopWavePKValid = false;
        public float fRevStopPKValue = 0.0f;
        public int Len = 0;

        public void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0 || stream.Length <= offset)
                    throw new Exception("tRevStopWvEvDef.Serialize take invalid parameter");

                stream[offset] = (byte)enMeasDefType.eRevStopform;
                if (bRevStopWavePKValid)
                    stream[offset] = (byte)(stream[offset] | (byte)(SsWvEvDef.ePK) << 4);
                Len++;

                if (bRevStopWavePKValid)
                {
                    DataTypeConverter.FloatToMeshByteArr(fRevStopPKValue, stream, offset + Len);
                    Len += 4;
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }

        public void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                bRevStopWavePKValid = ((stream[offset] >> 4 & (byte)SsWvEvDef.ePK) != 0x00) ? true : false;
                Len++;

                if (bRevStopWavePKValid)
                {
                    fRevStopPKValue = DataTypeConverter.MeshByteArrToFloat(stream, offset + Len);
                    Len += 4;
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    /*
    /// <summary>
    /// 波形类型定义
    /// bWaveRMSValid:!0=特征值有效，0=特征值有效
    /// </summary>
    public class tWvEvDef
    {
        // 测量定义类型
        public enMeasDefType MeasDefType { get; set; }
        // 特征值类型
        public byte u8EigenValueType { get; set; }
        //加速度特征值是否有效
        // RMS有效
        public bool bAccWaveRMSValid { get; set; }
        // PK有效
        public bool bAccWavePKValid { get; set; }
        //速度特征值是否有效
        // RMS有效
        public bool bVelWaveRMSValid { get; set; }
        // 低频有效
        public bool bVelWaveLPEValid { get; set; }
        // 中频有效
        public bool bVelWaveMPEValid { get; set; }
        // 高频有效
        public bool bVelWaveHPEValid { get; set; }
        //位移特征值是否有效
        // PKPK有效
        public bool bDspWavePKPKValid { get; set; }
        //加速度包络特征值是否有效
        // PK有效
        public bool bAccEnvWavePKValid { get; set; }
        // PKC有效
        public bool bAccEnvWavePKCValid { get; set; }  
        // 均值有效
        public bool bAccEnvWaveMEANValid { get; set; }
        //LQ特征值是否有效
        public bool bLQWaveRMSValid { get; set; }
        //启停机特征值是否有效
        public bool bRevStopWavePKValid { get; set; }

        public int Len
        {
            get { return 1; }
            set { }
        }

        public void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tMeshMeasDef.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tMeshMeasDef.Serialize take invalid Index");

                stream[offset] = (byte)((byte)MeasDefType | (u8EigenValueType << 4));                
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }

        public void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {               
                MeasDefType = (enMeasDefType)(stream[offset] & 0x0F);
                if (MeasDefType == enMeasDefType.eAccWaveform)
                {
                    bAccWaveRMSValid = (((stream[offset] >> 4) & (byte)AccWvEvDef.eRMS) > 0) ? true : false;
                    bAccWavePKValid = (((stream[offset] >> 4) & (byte)AccWvEvDef.ePK) > 0) ? true : false;
                }
                else if (MeasDefType == enMeasDefType.eVelWaveform)
                {
                    bVelWaveRMSValid = (((stream[offset] >> 4) & (byte)VelWvEvDef.eRMS) > 0) ? true : false;
                    bVelWaveLPEValid = (((stream[offset] >> 4) & (byte)VelWvEvDef.eLPE) > 0) ? true : false;
                    bVelWaveMPEValid = (((stream[offset] >> 4) & (byte)VelWvEvDef.eMPE) > 0) ? true : false;
                    bVelWaveHPEValid = (((stream[offset] >> 4) & (byte)VelWvEvDef.eHPE) > 0) ? true : false;
                }
                else if (MeasDefType == enMeasDefType.eDspWaveform)
                {
                    bDspWavePKPKValid = (((stream[offset] >> 4) & (byte)DspWvEvDef.ePPK) > 0) ? true : false;
                }
                else if (MeasDefType == enMeasDefType.eAccEnvelope)
                {
                    bAccEnvWavePKValid = (((stream[offset] >> 4) & (byte)AevWvEvDef.ePK) > 0) ? true : false;
                    bAccEnvWavePKCValid = (((stream[offset] >> 4) & (byte)AevWvEvDef.ePKC) > 0) ? true : false;
                    bAccEnvWaveMEANValid = (((stream[offset] >> 4) & (byte)AevWvEvDef.eMEAN) > 0) ? true : false;
                }
                else if (MeasDefType == enMeasDefType.eLQform)
                {
                    bLQWaveRMSValid = (((stream[offset] >> 4) & (byte)LqWvEvDef.eRMS) > 0) ? true : false;
                }
                else if (MeasDefType == enMeasDefType.eRevStopform)
                {
                    bRevStopWavePKValid = (((stream[offset] >> 4) & (byte)SsWvEvDef.ePK) > 0) ? true : false;
                }              
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    */

    /// <summary>
    /// 测量定义
    /// </summary>
    public class tMeshMeasDef
    {
        // 测量定义类型
        public enMeasDefType MeasDefType = enMeasDefType.eNulWaveform;
        // 特征值类型
        public byte u8EigenValueType = 0;
        // 波形长度
        public UInt16 u16WaveLen = 0;
        // 特征值长度
        public UInt16 u16EigenLen = 0;
        // 滤波器下限频率，如果测量定义为加速度包络时，此字段表示“包络滤波器”
        public UInt16 u16LowFreq = 0;
        // 滤波器上限频率，如果测量定义为加速度包络时，此字段表示“包络带宽”
        public UInt16 u16UpperFreq = 0;
       
        public int Len
        {
            get { return 3; }
            set { }
        }

        public void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tMeshMeasDef.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tMeshMeasDef.Serialize take invalid Index");

                stream[offset] = (byte)((byte)MeasDefType | ((byte)u8EigenValueType << 4));
                stream[offset + 1] = (byte)(MeasDefGear.EncodeWaveLen(u16WaveLen) | MeasDefGear.EncodeEigenLen(u16EigenLen));
                if (MeasDefType == enMeasDefType.eAccEnvelope)
                {
                    stream[offset + 2] = (byte)(MeasDefGear.EncodeAevBw(u16UpperFreq) | MeasDefGear.EncodeAevFt(u16LowFreq));
                }
                else
                {
                    stream[offset + 2] = (byte)(MeasDefGear.EncodeUpFreq(u16UpperFreq) | MeasDefGear.EncodeDwFreq(u16LowFreq));
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }

        public void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tMeshMeasDef.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tMeshMeasDef.Unserialize take invalid Index"); 
                MeasDefType      = (enMeasDefType)(stream[offset] & 0x0F);
                u8EigenValueType = (byte)(stream[offset] >> 4);
                u16WaveLen       = MeasDefGear.DecodeWaveLen((byte)(stream[offset + 1] & 0xF0));
                u16EigenLen      = MeasDefGear.DecodeEigenLen((byte)(stream[offset + 1] & 0x0F));
                if (MeasDefType == enMeasDefType.eAccEnvelope)
                {
                    u16UpperFreq = MeasDefGear.DecodeAevBw((byte)(stream[offset + 2] & 0xF0));
                    u16LowFreq   = MeasDefGear.DecodeAevFt((byte)(stream[offset + 2] & 0x0F));
                }
                else
                {
                    u16UpperFreq = MeasDefGear.DecodeUpFreq((byte)(stream[offset + 2] & 0xF0));
                    u16LowFreq   = MeasDefGear.DecodeDwFreq((byte)(stream[offset + 2] & 0x0F));
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    
    /// <summary>
    /// 触发式测量定义
    /// </summary>
    /*
    public class tTriggerDef
    {
        // 测量定义类型
        public enMeasDefType MeasDefType { get; set; }
        //0x00禁止，0x01加速度特征值使能，0x02，速度特征值使能，0x03位移特征值使能，0x04包络特征值使能
        public byte u8Flag { get; set; }
        //true：有效值使能；false：有效值禁止
        public bool bRMSenable { get; set; }
        //true:峰值使能；false：峰值禁止
        public bool bPKenable { get; set; }
        // PKPK有效
        public bool bPKPKenable { get; set; }
        // PKC有效
        public bool bPKCenable { get; set; }
        // 低频有效
        public bool bLPEenable { get; set; }
        // 中频有效
        public bool bMPEenable { get; set; }
        // 高频有效
        public bool bHPEenable { get; set; }
        // 均值有效
        public bool bMEANenable { get; set; }
        // 特征值的具体数据
        public float fCharRmsvalue { get; set; }
        public float fCharPKvalue { get; set; }
        public float fCharPKPKvalue { get; set; }
        public float fCharPKCvalue { get; set; }
        public float fCharLPEvalue { get; set; }
        public float fCharMPEvalue { get; set; }
        public float fCharHPEvalue { get; set; }
        public float fCharMEANvalue { get; set; }
        public int defLen = 0;
        
       // public int Len
        //{
         //   get { return 15; }
         //   set { }
      //  }

        public void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (MeasDefType == enMeasDefType.eAccWaveform)
                {
                    stream[offset++] = (byte)(u8Flag |((bRMSenable == true)?0x10:0x00)| ((bPKenable == true)?0x20:0x00));

                    if (bRMSenable == true )
                    {                      
                        DataTypeConverter.FloatToMeshByteArr(fCharRmsvalue, stream, offset);
                        offset = offset + 4;
                    }
                    if (bPKenable == true)
                    {
                        DataTypeConverter.FloatToMeshByteArr(fCharRmsvalue, stream, offset);
                        offset = offset + 4;
                    }

                }
                else if (MeasDefType == enMeasDefType.eVelWaveform)
                {
                    stream[offset++] = (byte)(u8Flag | ((bRMSenable == true) ? 0x10 : 0x00) | ((bLPEenable == true) ? 0x20 : 0x00)
                                                     | ((bMPEenable == true) ? 0x40 : 0x00) | ((bHPEenable == true) ? 0x80 : 0x00));
                    if (bRMSenable == true)
                    {
                        DataTypeConverter.FloatToMeshByteArr(fCharRmsvalue, stream, offset);
                        offset = offset + 4;
                    }
                    if(bLPEenable == true)
                    {
                        DataTypeConverter.FloatToMeshByteArr(fCharLPEvalue, stream, offset);
                        offset = offset + 4;
                    }
                    if(bMPEenable == true)
                    {
                        DataTypeConverter.FloatToMeshByteArr(fCharMPEvalue, stream, offset);
                        offset = offset + 4;
                    }
                    if(bHPEenable == true)
                    {
                        DataTypeConverter.FloatToMeshByteArr(fCharHPEvalue, stream, offset);
                        offset = offset + 4;
                    }
                }
                else if (MeasDefType == enMeasDefType.eDspWaveform)
                {
                    stream[offset++] = (byte)(u8Flag | ((bPKPKenable == true) ? 0x10 : 0x00));
                    DataTypeConverter.FloatToMeshByteArr(fCharPKPKvalue, stream, offset);
                    offset = offset + 4;                  
                }
                else if (MeasDefType == enMeasDefType.eAccEnvelope)
                {
                    stream[offset++] = (byte)(u8Flag | ((bPKenable == true) ? 0x10 : 0x00) | ((bPKCenable == true) ? 0x20 : 0x00)
                                                     | ((bMEANenable == true) ? 0x40 : 0x00));
                    if (bPKenable == true)
                    {
                        DataTypeConverter.FloatToMeshByteArr(fCharRmsvalue, stream, offset);
                        offset = offset + 4;
                    }
                    if (bPKCenable == true)
                    {
                        DataTypeConverter.FloatToMeshByteArr(fCharLPEvalue, stream, offset);
                        offset = offset + 4;
                    }
                    if (bMEANenable == true)
                    {
                        DataTypeConverter.FloatToMeshByteArr(fCharMPEvalue, stream, offset);
                        offset = offset + 4;
                    }
                }
                defLen = offset;
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }

        public void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {              
                
                u8Flag = stream[offset++];
                if ((enMeasDefType)(u8Flag & 0x0F) == enMeasDefType.eAccWaveform)
                {                   
                    if ((AccWvEvDef)((u8Flag >> 4) & (byte)AccWvEvDef.eRMS) == AccWvEvDef.eRMS)
                    {
                        bRMSenable = true;
                        fCharRmsvalue = DataTypeConverter.MeshByteArrToFloat(stream, offset);
                        offset = offset + 4;
                    }
                    if ((AccWvEvDef)((u8Flag >> 4) & (byte)AccWvEvDef.ePK) == AccWvEvDef.ePK)
                    {
                        bPKenable = true;
                        fCharPKCvalue = DataTypeConverter.MeshByteArrToFloat(stream, offset);
                        offset = offset + 4;
                    }

                }
                else if ((enMeasDefType)(u8Flag & 0x0F) == enMeasDefType.eVelWaveform)
                {
                    if ((VelWvEvDef)((u8Flag >> 4) & (byte)VelWvEvDef.eRMS) == VelWvEvDef.eRMS)
                    {
                        bRMSenable = true;
                        fCharRmsvalue =  DataTypeConverter.MeshByteArrToFloat(stream, offset);
                        offset = offset + 4;
                    }
                    if ((VelWvEvDef)((u8Flag >> 4) & (byte)VelWvEvDef.eLPE) == VelWvEvDef.eLPE)
                    {
                        bLPEenable = true;
                        fCharLPEvalue = DataTypeConverter.MeshByteArrToFloat(stream, offset);
                        offset = offset + 4;
                    }
                    if ((VelWvEvDef)((u8Flag >> 4) & (byte)VelWvEvDef.eMPE) == VelWvEvDef.eMPE)
                    {
                        bMPEenable = true;
                        fCharMPEvalue = DataTypeConverter.MeshByteArrToFloat(stream, offset);
                        offset = offset + 4;
                    }
                    if ((VelWvEvDef)((u8Flag >> 4) & (byte)VelWvEvDef.eHPE) == VelWvEvDef.eHPE)
                    {
                        bHPEenable = true;
                        fCharHPEvalue = DataTypeConverter.MeshByteArrToFloat(stream, offset);
                        offset = offset + 4;
                    }
                }
                else if (MeasDefType == enMeasDefType.eDspWaveform)
                {
                    if ((DspWvEvDef)((u8Flag >> 4) & (byte)DspWvEvDef.ePPK) == DspWvEvDef.ePPK)
                    {
                        bPKPKenable = true;
                        fCharPKPKvalue = DataTypeConverter.MeshByteArrToFloat(stream, offset);
                        offset = offset + 4;
                    }
                }
                else if (MeasDefType == enMeasDefType.eAccEnvelope)
                {
                    if ((AevWvEvDef)((u8Flag >> 4) & (byte)AevWvEvDef.ePK) == AevWvEvDef.ePK)
                    {
                        bPKenable = true;
                       fCharPKvalue = DataTypeConverter.MeshByteArrToFloat(stream, offset);
                        offset = offset + 4;
                    }
                    if ((AevWvEvDef)((u8Flag >> 4) & (byte)AevWvEvDef.ePKC) == AevWvEvDef.ePKC)
                    {
                        bPKCenable = true;
                        fCharPKCvalue = DataTypeConverter.MeshByteArrToFloat(stream, offset);
                        offset = offset + 4;
                    }
                    if ((AevWvEvDef)((u8Flag >> 4) & (byte)AevWvEvDef.eMEAN) == AevWvEvDef.eMEAN)
                    {
                        bMEANenable = true;
                        fCharMEANvalue = DataTypeConverter.MeshByteArrToFloat(stream, offset);
                        offset = offset + 4;
                    }
                }
                defLen = offset;
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
   */

    public class tAppHeader
    {
        public tMAC mac = new tMAC();

        public UInt16 u16WSID = 2;
        //public UInt16 u16SSNID = 0;
        /// <summary>
        /// 网络中的设备总数
        /// </summary>
        public byte u8DevTotal = 0;
        /// <summary>
        /// 网络中的设备序号
        /// </summary>
        public byte u8DevNum = 0;

        public byte u8MainCMD = 0;
        public byte u8SubCMD = 0;
        public bool isRequest = false;
        public byte u8ParamLen = 0;
        public byte u8RC = 0;
        /// <summary>
        /// 应用层协议头长度
        /// </summary>
        protected int Len
        {
            get { return 7; }
            set { ; }
        }

        protected void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tAppHeader.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tAppHeader.Serialize take invalid Index");

                DataTypeConverter.UInt16ToMeshByteArr(u16WSID, stream, offset);
                stream[offset + 2] = u8DevTotal;
                stream[offset + 3] = u8DevNum;
                //DataTypeConverter.UInt16ToMeshByteArr(u16SSNID, stream, offset + 2);
                stream[offset + 4] = (byte)(u8SubCMD | (u8MainCMD << 5));
                stream[offset + 5] = (byte)(((byte)(isRequest ? 0x01 : 0x00) << 7) | u8ParamLen);
                stream[offset + 6] = u8RC;
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }

        protected void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tAppHeader.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tAppHeader.Unserialize take invalid Index");

                u16WSID    = DataTypeConverter.MeshByteArrToUInt16(stream, offset);
                //u16SSNID   = DataTypeConverter.MeshByteArrToUInt16(stream, offset + 2);
                u8DevTotal = stream[offset + 2];
                u8DevNum = stream[offset + 3];
                u8MainCMD  = (byte)stream[offset + 4];
                u8MainCMD  = (byte)(u8MainCMD >> 5);
                u8SubCMD   = (byte)stream[offset + 4];
                u8SubCMD   = (byte)(u8SubCMD & 0x1F);
                u8ParamLen = stream[offset + 5];
                u8ParamLen = (byte)(u8ParamLen & 0x7F);
                isRequest  = ((stream[offset + 5] & 0x80) == 0x00) ? false : true;
                u8RC       = stream[offset + 6];
               
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

#region WS上传的请求参数定义
    /// <summary>
    /// 自描述报告参数
    /// </summary>
    public class tMeshSelfReportParam : tAppHeader
    {
        // 因联主程序固件版本信息
        public tVer verMcuFw = new tVer();
        // 因联启动程序固件版本信息
        public tVer verMcuBl = new tVer();
        // 协议栈组件版本信息
        public tVer verMoteStack = new tVer();
        // 保留
        public tVer verRsv = new tVer();
        //传感器类型定义(可按位与)：0x01，振动(Vib)；0x02，温度(Tmp)；
        public List<enSensorType> SensorTypeMap = new List<enSensorType>();
        //振动传感器其他功能，局部同步，全局同步，可按位与
        public List<enVibFuncMap> VibFuncMap = new List<enVibFuncMap>();
        //振动传感器支持的波形类型定义(可按位与)
        //0x01，加速度波形(Acc)；0x02，速度波形(Vel)；
        //0x04，位移波形(Dsp)；0x08，加速度包络波形(AccEvp)；
        //0x10，LQ波形(LQ)；0x20，启停机波形(SS)；
        public List<enVibWvTMap> VibWvTMap = new List<enVibWvTMap>();
        //振动传感器支持的特征值类型定义(可按位与)：
        //0x0001，有效值(RMS)；0x0002，峰值(PK)；0x0004，峰峰值(PPK)；
        //0x0008，地毯值(PKC)；0x0010，均值(MEAN)；0x0020，低频能量(LPE)；
        //0x0040，中频能量(MPE)0x0080，高频能量(HPE)；
        public List<enVibEvTMap> VibEvTMap = new List<enVibEvTMap>();
        //WS中Mote的NetID
        public UInt16 u16NetWorkID = 0;
        public tMeshSelfReportParam()
        {
            base.u8ParamLen = 21;
        }

        public new int Len
        {
            get { return (base.Len + base.u8ParamLen); }
            set { ; }
        }
    
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            byte   u8SensorType = 0;
            byte   u8VibFunc    = 0;
            byte   u8VibWvT     = 0;
            UInt16 u16VibEvT    = 0;
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("take invalid Index");

                base.Unserialize(stream, offset);
                offset += base.Len;
                verMcuFw.u8Main = stream[offset++];
                verMcuFw.u8Sub = stream[offset++];
                verMcuFw.u8Rev = stream[offset++];
                verMcuFw.u8Build = stream[offset++];
                verMcuBl.u8Main = stream[offset++];
                verMcuBl.u8Sub = stream[offset++];
                verMcuBl.u8Rev = stream[offset++];
                verMcuBl.u8Build = stream[offset++];
                verMoteStack.u8Main = stream[offset++];
                verMoteStack.u8Sub = stream[offset++];
                verMoteStack.u8Rev = stream[offset++];
                verMoteStack.u8Build = stream[offset++];
                verRsv.u8Main = stream[offset++];
                verRsv.u8Sub = stream[offset++];
                verRsv.u8Rev = stream[offset++];
                verRsv.u8Build = stream[offset++];
                u8SensorType = stream[offset++];
                u8VibFunc = stream[offset++];
                u8VibWvT = stream[offset++];
                u16VibEvT = DataTypeConverter.MeshByteArrToUInt16(stream, offset);
                offset += 2;
                u16NetWorkID = DataTypeConverter.MeshByteArrToUInt16(stream, offset);

                if ((u8SensorType & (byte)enSensorType.eVib) != 0)
                    SensorTypeMap.Add(enSensorType.eVib);
                if ((u8SensorType & (byte)enSensorType.eTmp) != 0)
                    SensorTypeMap.Add(enSensorType.eTmp);

                if ((u8VibFunc & (byte)enVibFuncMap.eFacilitySync) != 0)
                    VibFuncMap.Add(enVibFuncMap.eFacilitySync);
                if ((u8VibFunc & (byte)enVibFuncMap.eMeshSync) != 0)
                    VibFuncMap.Add(enVibFuncMap.eMeshSync);

                if ((u8VibWvT & (byte)enVibWvTMap.eAcc) != 0)
                    VibWvTMap.Add(enVibWvTMap.eAcc);
                if ((u8VibWvT & (byte)enVibWvTMap.eVel) != 0)
                    VibWvTMap.Add(enVibWvTMap.eVel);
                if ((u8VibWvT & (byte)enVibWvTMap.eDsp) != 0)
                    VibWvTMap.Add(enVibWvTMap.eDsp);
                if ((u8VibWvT & (byte)enVibWvTMap.eAccEvp) != 0)
                    VibWvTMap.Add(enVibWvTMap.eAccEvp);
                if ((u8VibWvT & (byte)enVibWvTMap.eLQ) != 0)
                    VibWvTMap.Add(enVibWvTMap.eLQ);
                if ((u8VibWvT & (byte)enVibWvTMap.eRevStop) != 0)
                    VibWvTMap.Add(enVibWvTMap.eRevStop);

                if ((u16VibEvT & (byte)enVibEvTMap.eRMS) != 0)
                    VibEvTMap.Add(enVibEvTMap.eRMS);
                if ((u16VibEvT & (byte)enVibEvTMap.ePK) != 0)
                    VibEvTMap.Add(enVibEvTMap.ePK);
                if ((u16VibEvT & (byte)enVibEvTMap.ePPK) != 0)
                    VibEvTMap.Add(enVibEvTMap.ePPK);
                if ((u16VibEvT & (byte)enVibEvTMap.ePKC) != 0)
                    VibEvTMap.Add(enVibEvTMap.ePKC);
                if ((u16VibEvT & (byte)enVibEvTMap.eMEAN) != 0)
                    VibEvTMap.Add(enVibEvTMap.eMEAN);
                if ((u16VibEvT & (byte)enVibEvTMap.eLPE) != 0)
                    VibEvTMap.Add(enVibEvTMap.eLPE);
                if ((u16VibEvT & (byte)enVibEvTMap.eMPE) != 0)
                    VibEvTMap.Add(enVibEvTMap.eMPE);
                if ((u16VibEvT & (byte)enVibEvTMap.eHPE) != 0)
                    VibEvTMap.Add(enVibEvTMap.eHPE);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "tSelfReportParam.Unserialize " + ex.Message.ToString());
            }
        }
    }
    /// <summary>
    /// 健康报告参数
    /// </summary>
    public class tHealthReportParam : tAppHeader
    {
        // WS当前状态机状态值
        public byte u8State = 0;
        //WS系统运行错误码；
        public UInt16 u16ErrorCode = 0;
        // Mote启动原因
        public byte u8WakeupMode = 0;
        // MoteBoot
        public byte u8MoteBoot = 0;
        // 采集时间
        public tDateTime TmpTime = new tDateTime();
        // 被监测设备的温度值；
        public float u32FacilityTmp = 0.0f;
        // 仪器自身运行温度；
        public float u32DeviceTmp = 0.0f;
        //电池状态：0x00：电池状态良好；0x01：电池状态报警；
        public float u32BatState = 0.0f;
        

        public tHealthReportParam()
        {
            base.u8ParamLen = 23;
        }

        public new int Len
        {
            get { return (base.Len + base.u8ParamLen); }
            set { ; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("take invalid Index");

                base.Unserialize(stream, offset);
                offset += base.Len;
                u8State         = stream[offset++];
                u16ErrorCode    = DataTypeConverter.MeshByteArrToUInt16(stream, offset);
                offset += 2;
                u8WakeupMode    = stream[offset++];
                u8MoteBoot      = stream[offset++];
                TmpTime.u8Year  = stream[offset++];
                TmpTime.u8Month = stream[offset++];
                TmpTime.u8Day   = stream[offset++];
                TmpTime.u8Hour  = stream[offset++];
                TmpTime.u8Min   = stream[offset++];
                TmpTime.u8Sec   = stream[offset++];
                u32FacilityTmp  = DataTypeConverter.MeshByteArrToFloat(stream, offset);
                offset += 4;
                u32DeviceTmp    = DataTypeConverter.MeshByteArrToFloat(stream, offset);
                offset += 4;
                u32BatState     = DataTypeConverter.MeshByteArrToFloat(stream, offset);
                offset += 4;               
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "tHealthReportParam.Unserialize " + ex.Message.ToString());
            }
        }
    }
    /// <summary>
    /// 波形描述信息
    /// </summary>
    public class tMeshWaveDescParam : tAppHeader
    {
        // 波形采集时间
        public tDateTime DaqTime = new tDateTime();
        // 采集方式
        public enMeshWsDaqMode DaqMode = enMeshWsDaqMode.eTiming;
        //波形类型
        public enMeasDefType WaveType = enMeasDefType.eNulWaveform;
        // 加速度波形
        public tAccWvEvDef AccDef = null;
        // 速度波形
        public tVelWvEvDef VelDef = null;
        // 位移波形
        public tDspWvEvDef DspDef = null;
        // 加速度包络波形
        public tAccEnvWvEvDef AccEnvDef = null;
        // LQ波形
        public tLQWvEvDef LQDef = null;
        // 启停机
        public tRevStopWvEvDef RevStopDef = null;
        // 波形数据总长度
        public UInt16 u16WaveLen = 0;
        // 幅值缩放因子
        public float f32AmpScaler = 1.0f;
        // 波形数据总帧数
        public UInt16 u16TotalFramesNum = 0;
        // 非加速度包络时，表示上限频率；加速度包络时，表示包络带宽
        public UInt16 u16UpFreqOrAevBw = 0;
        // 非加速度包络时，表示下限频率；加速度包络时，表示滤波器系数
        public UInt16 u16DwFreqOrAevFt = 0;
        // 所有特征值类型
        public byte u8EigenValueType = 0;

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0 || stream.Length <= offset)
                    throw new Exception("take invalid parameter");

                base.Unserialize(stream, offset);
                offset += base.Len;

                DaqTime.u8Year  = stream[offset++];
                DaqTime.u8Month = stream[offset++];
                DaqTime.u8Day   = stream[offset++];
                DaqTime.u8Hour  = stream[offset++];
                DaqTime.u8Min   = stream[offset++];
                DaqTime.u8Sec   = stream[offset++];
                DaqMode = (enMeshWsDaqMode)stream[offset++];
                WaveType = (enMeasDefType)(stream[offset] & 0x0F);
                u8EigenValueType = (byte)(stream[offset] & 0xF0);
                switch (WaveType)
                {
                    case enMeasDefType.eAccWaveform:
                    {
                        AccDef = new tAccWvEvDef();
                        AccDef.Unserialize(stream, offset);
                        offset += AccDef.Len;
                        u16UpFreqOrAevBw = MeasDefGear.DecodeUpFreq((byte)(stream[offset] & 0xF0));
                        u16DwFreqOrAevFt = MeasDefGear.DecodeDwFreq((byte)(stream[offset] & 0x0F));
                        offset++;
                        break;
                    }
                    case enMeasDefType.eVelWaveform:
                    {
                        VelDef = new tVelWvEvDef();
                        VelDef.Unserialize(stream, offset);
                        offset += VelDef.Len;
                        u16UpFreqOrAevBw = MeasDefGear.DecodeUpFreq((byte)(stream[offset] & 0xF0));
                        u16DwFreqOrAevFt = MeasDefGear.DecodeDwFreq((byte)(stream[offset] & 0x0F));
                        offset++;
                        break;
                    }
                    case enMeasDefType.eDspWaveform:
                    {
                        DspDef = new tDspWvEvDef();
                        DspDef.Unserialize(stream, offset);
                        offset += DspDef.Len;
                        u16UpFreqOrAevBw = MeasDefGear.DecodeUpFreq((byte)(stream[offset] & 0xF0));
                        u16DwFreqOrAevFt = MeasDefGear.DecodeDwFreq((byte)(stream[offset] & 0x0F));
                        offset++;
                        break;
                    }
                    case enMeasDefType.eAccEnvelope:
                    {
                        AccEnvDef = new tAccEnvWvEvDef();
                        AccEnvDef.Unserialize(stream, offset);
                        offset += AccEnvDef.Len;
                        u16UpFreqOrAevBw = MeasDefGear.DecodeAevBw((byte)(stream[offset] & 0xF0));
                        u16DwFreqOrAevFt = MeasDefGear.DecodeAevFt((byte)(stream[offset] & 0x0F));
                        offset++;
                        break;
                    }
                    case enMeasDefType.eLQform:
                    {
                        LQDef = new tLQWvEvDef();
                        LQDef.Unserialize(stream, offset);
                        offset += LQDef.Len;
                        u16UpFreqOrAevBw = MeasDefGear.DecodeUpFreq((byte)(stream[offset] & 0xF0));
                        u16DwFreqOrAevFt = MeasDefGear.DecodeDwFreq((byte)(stream[offset] & 0x0F));
                        offset++;
                        break;
                    }
                    case enMeasDefType.eRevStopform:
                    {
                        RevStopDef = new tRevStopWvEvDef();
                        RevStopDef.Unserialize(stream, offset);
                        offset += RevStopDef.Len;
                        u16UpFreqOrAevBw = MeasDefGear.DecodeUpFreq((byte)(stream[offset] & 0xF0));
                        u16DwFreqOrAevFt = MeasDefGear.DecodeDwFreq((byte)(stream[offset] & 0x0F));
                        offset++;
                        break;
                    }
                    default:
                        break;
                }

                u16WaveLen = DataTypeConverter.MeshByteArrToUInt16(stream, offset);
                offset += 2;
                f32AmpScaler = DataTypeConverter.MeshByteArrToFloat(stream, offset);
                offset += 4;
                u16TotalFramesNum = DataTypeConverter.MeshByteArrToUInt16(stream, offset);
                offset += 2;

                base.u8ParamLen = (byte)offset;
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "tWaveDescParam.Unserialize " + ex.Message.ToString());
            }
        }
    }
    /// <summary>
    /// 波形数据
    /// </summary>
    public class tWaveDataParam : tAppHeader
    {
        // 测量定义类型
        public enMeasDefType WaveType = enMeasDefType.eNulWaveform;
        // 波形数据总帧数
        public UInt16 u16TotalFramesNum = 0;
        // 波形数据当前帧数
        public UInt16 u16CurrentFrameID = 0;
        // 当前承载的波形数据大小
        public byte u8PacketSize = 0;
        // 波形数据
        public byte[] u8aData = null;

        private const byte WAVETYPE_OFFSET = 0;
        private const byte TTLFRMNUM_OFFSET = WAVETYPE_OFFSET + 1;
        private const byte CURFRMNUM_OFFSET = TTLFRMNUM_OFFSET + 2;
        private const byte PKTSIZE_OFFSET = CURFRMNUM_OFFSET + 2;
        private const byte DATA_OFFSET = PKTSIZE_OFFSET + 1;
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception(" take invalid parameter");

                base.Unserialize(stream, offset);

                WaveType          = (enMeasDefType)stream[offset + base.Len + WAVETYPE_OFFSET];
                u16TotalFramesNum = DataTypeConverter.MeshByteArrToUInt16(stream, offset + base.Len + TTLFRMNUM_OFFSET);
                u16CurrentFrameID = DataTypeConverter.MeshByteArrToUInt16(stream, offset + base.Len + CURFRMNUM_OFFSET);
                u8PacketSize      = stream[offset + base.Len + PKTSIZE_OFFSET];
                u8aData           = new byte[u8PacketSize];
                Array.Copy(stream, offset + base.Len + DATA_OFFSET, u8aData, 0, u8PacketSize);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "tWaveDataParam.Unserialize " + ex.Message.ToString());
            }
        }
    }
    /// <summary>
    /// 特征值
    /// </summary>
    public class tMeshEigenValueParam : tAppHeader
    {
        // 采集时间
        public tDateTime SampleTime = null;
        // Acc波形类型
        public tAccWvEvDef AccDef = null;
        // Vel波形类型
        public tVelWvEvDef VelDef = null;
        // Dsp波形类型
        public tDspWvEvDef DspDef = null;
        // AccEnv波形类型
        public tAccEnvWvEvDef AccEnvDef = null;
        // lq波形类型
        public tLQWvEvDef LQDef = null;
        // 启停机波形类型
        public tRevStopWvEvDef RevStopDef = null;

        public tMeshEigenValueParam()
        {
            SampleTime = new tDateTime();
        }

        private const byte SMPTIME_OFFSET = 0;
        private const byte WAVETYPE_OFFSET = SMPTIME_OFFSET + 6;            

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0 || stream.Length <= offset)
                    throw new Exception("take invalid parameter");          

                base.Unserialize(stream, offset);
                offset += base.Len;

                SampleTime.u8Year  = stream[offset++];
                SampleTime.u8Month = stream[offset++];
                SampleTime.u8Day   = stream[offset++];
                SampleTime.u8Hour  = stream[offset++];
                SampleTime.u8Min   = stream[offset++];
                SampleTime.u8Sec   = stream[offset++];

                while (((byte)enMeasDefType.eNulWaveform < (stream[offset] & 0x0F))
                      || ((byte)enMeasDefType.eOriginalWave > (stream[offset] & 0x0F)))
                {
                    enMeasDefType WaveType = (enMeasDefType)(stream[offset] & 0x0F);
                    switch (WaveType)
                    {
                        case enMeasDefType.eAccWaveform:
                        {
                            AccDef = new tAccWvEvDef();
                            AccDef.Unserialize(stream, offset);
                            offset += AccDef.Len;
                            break;
                        }
                        case enMeasDefType.eVelWaveform:
                        {
                            VelDef = new tVelWvEvDef();
                            VelDef.Unserialize(stream, offset);
                            offset += VelDef.Len;
                            break;
                        }
                        case enMeasDefType.eDspWaveform:
                        {
                            DspDef = new tDspWvEvDef();
                            DspDef.Unserialize(stream, offset);
                            offset += DspDef.Len;
                            break;
                        }
                        case enMeasDefType.eAccEnvelope:
                        {
                            AccEnvDef = new tAccEnvWvEvDef();
                            AccEnvDef.Unserialize(stream, offset);
                            offset += AccEnvDef.Len;
                            break;
                        }
                        case enMeasDefType.eLQform:
                        {
                            LQDef = new tLQWvEvDef();
                            LQDef.Unserialize(stream, offset);
                            offset += LQDef.Len;
                            break;
                        }
                        case enMeasDefType.eRevStopform:
                        {
                            RevStopDef = new tRevStopWvEvDef();
                            RevStopDef.Unserialize(stream, offset);
                            offset += RevStopDef.Len;
                            break;
                        }
                        default:
                            return;
                    }

                    if (offset >= stream.Length)
                        break;
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "tEigenValueParam.Unserialize " + ex.Message.ToString());
            }
        }
    }
#endregion 网络上传的请求参数定义

#region 服务器下发请求参数定义

#region 设置类命令参数
    public class tCaliTimeParam : tAppHeader
    {
        // 秒
        public UInt64 u64Seconds { get; set; }
        // 微秒，保留字段
        public UInt32 u32Microseconds { get; set; }

        public tCaliTimeParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eSet;
            base.u8SubCMD = (byte)enAppSetSubCMD.eTimeCali;
            base.isRequest = true;
            base.u8ParamLen = 12;
        }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tCaliTimeParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tCaliTimeParam.Serialize take invalid Index");

                base.Serialize(stream, offset);

                DataTypeConverter.UInt64ToMeshByteArr(u64Seconds, stream, offset + base.Len);
                DataTypeConverter.UInt32ToMeshByteArr(u32Microseconds, stream, offset + base.Len + 8);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tSetNetworkIdParam : tAppHeader
    {
        public UInt16 u16ID { get; set; }

        public tSetNetworkIdParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eSet;
            base.u8SubCMD = (byte)enAppSetSubCMD.eNetworkID;
            base.isRequest = true;
            base.u8ParamLen = 2;
        }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetNetworkIdParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetNetworkIdParam.Serialize take invalid Index");

                base.Serialize(stream, offset);
                DataTypeConverter.UInt16ToMeshByteArr(u16ID, stream, offset + base.Len);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tMeshSetMeasDefParam : tAppHeader
    {
        // 采集方式
        public enMeshWsDaqMode DaqMode = enMeshWsDaqMode.eTiming;
        // 定时温度采集周期时&分
        public tTimingTime TmpDaqPeriod = new tTimingTime();
        // 特征值采集周期（该值为温度采集周期的整倍数值）
        public UInt16 u16EigenDaqMult = 1;
        // 波形采集周期（该值为特征值采集周期的整倍数值）
        public UInt16 u16WaveDaqMult = 1;
        // 频谱低频段上限频率
        public UInt16 LFBSpcUpFreq = 0;
        // 频谱低频段下限频率
        public UInt16 LFBSpcLwFreq = 0;
        // 频谱中频段上限频率
        public UInt16 MFBSpcUpFreq = 0;
        // 频谱中频段下限频率
        public UInt16 MFBSpcLwFreq = 0;
        // 频谱高频段上限频率
        public UInt16 HFBSpcUpFreq = 0;
        // 频谱高频段下限频率
        public UInt16 HFBSpcLwFreq = 0;
        //测量定义数量
        public byte u8MeasDefCnt = 0;
        // 加速度波形测量定义
        public tMeshMeasDef AccMdf = null;
        // 加速度包络波形测量定义
        public tMeshMeasDef AccEnvMdf = null;
        // 速度波形测量定义
        public tMeshMeasDef VelMdf = null;
        // 位移波形测量定义
        public tMeshMeasDef DspMdf = null;
        // LQ波形测量定义
        public tMeshMeasDef LQMdf = null;
        // 启停机测量定义
        public tMeshMeasDef RevStop = null;

        public tMeshSetMeasDefParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eSet;
            base.u8SubCMD = (byte)enAppSetSubCMD.eMeasDef;
            base.isRequest = true;
        }

        public new int Len
        {
            get { return base.Len + (20/*固定应用负载数据*/ + u8MeasDefCnt * 3/*码流中1个测量定义的固定长度*/); }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetMeasDefParam.Serialize take invalid parameter");
                base.u8ParamLen = (byte)(20 + u8MeasDefCnt * 3);
                base.Serialize(stream, offset);
                offset += base.Len;
                stream[offset++] = (byte)DaqMode;
                stream[offset++] = (byte)TmpDaqPeriod.u8Hour;
                stream[offset++] = (byte)TmpDaqPeriod.u8Min;
                DataTypeConverter.UInt16ToMeshByteArr(u16EigenDaqMult, stream, offset); offset += 2;
                DataTypeConverter.UInt16ToMeshByteArr(u16WaveDaqMult, stream, offset); offset += 2;
                DataTypeConverter.UInt16ToMeshByteArr(LFBSpcUpFreq, stream, offset); offset += 2;
                DataTypeConverter.UInt16ToMeshByteArr(LFBSpcLwFreq, stream, offset); offset += 2;
                DataTypeConverter.UInt16ToMeshByteArr(MFBSpcUpFreq, stream, offset); offset += 2;
                DataTypeConverter.UInt16ToMeshByteArr(MFBSpcLwFreq, stream, offset); offset += 2;
                DataTypeConverter.UInt16ToMeshByteArr(HFBSpcUpFreq, stream, offset); offset += 2;
                DataTypeConverter.UInt16ToMeshByteArr(HFBSpcLwFreq, stream, offset); offset += 2;
                stream[offset++] = u8MeasDefCnt;

                if (AccMdf != null)
                {
                    AccMdf.Serialize(stream, offset);
                    offset += 3;
                }
                if (VelMdf != null)
                {
                    VelMdf.Serialize(stream, offset);
                    offset += 3;
                }
                if (DspMdf != null)
                {
                    DspMdf.Serialize(stream, offset);
                    offset += 3;
                }
                if (AccEnvMdf != null)
                {
                    AccEnvMdf.Serialize(stream, offset);
                    offset += 3;
                }
                if (LQMdf != null)
                {
                    LQMdf.Serialize(stream, offset);
                    offset += 3;
                }
                if (RevStop != null)
                {
                    RevStop.Serialize(stream, offset);
                    offset += 3;
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tMeshSetWsSnParam : tAppHeader
    {
        public byte u8SnLen { get; set; }
        private byte[] _sn = null;
        public byte[] sn
        {
            get { return _sn; }
            set
            {
                if (value == null || value.Length == 0)
                    throw new Exception("Set sn is null or length is 0");

                u8SnLen = (byte)value.Length;
                base.u8ParamLen = (byte)(u8SnLen + 1);
                _sn = value;
            }
        }

        public tMeshSetWsSnParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eSet;
            base.u8SubCMD = (byte)enAppSetSubCMD.eSn;
            base.isRequest = true;
            base.u8ParamLen = 0;
        }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetWsSnParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetWsSnParam.Serialize take invalid Index");

                base.Serialize(stream, offset);
                offset += base.Len;
                stream[offset++] = u8SnLen;
                Array.Copy(sn, 0, stream, offset, sn.Length);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tMeshCaliSensorParam : tAppHeader
    {
        //校准标志位：0x01：直接设置；0x02：进行自校准；
        public byte u8Flag = 0;
        public float Gain = 0.0f;
        public float Offset = 0.0f;
        public tMeshCaliSensorParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eSet;
            base.u8SubCMD = (byte)enAppSetSubCMD.eCaliCoeff;
            base.isRequest = true;
            base.u8ParamLen = 9;
        }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tCaliSensorParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tCaliSensorParam.Serialize take invalid Index");

                base.Serialize(stream, offset);
                offset += base.Len;
                stream[offset++] = u8Flag;
                DataTypeConverter.FloatToMeshByteArr(Gain, stream, offset);
                offset += 4;
                DataTypeConverter.FloatToMeshByteArr(Offset, stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tSetADCloseVoltParam : tAppHeader
    {
        public float VoltageLimit = 0.0f;
        public byte VoltageCount = 0;

        public tSetADCloseVoltParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eSet;
            base.u8SubCMD = (byte)enAppSetSubCMD.eADCloseVolt;
            base.isRequest = true;
            base.u8ParamLen = 5;
        }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetADCloseVoltParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetADCloseVoltParam.Serialize take invalid Index");
                base.Serialize(stream, offset);
                offset += base.Len;
                DataTypeConverter.FloatToMeshByteArr(VoltageLimit, stream, offset);
                offset += 4;
                stream[offset] = VoltageCount;
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tSetWsStateParam : tAppHeader
    {
        public byte WsState = 0;

        public tSetWsStateParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eSet;
            base.u8SubCMD = (byte)enAppSetSubCMD.eRevStop;
            base.isRequest = true;
            base.u8ParamLen = 1;
        }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetWsStateParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetWsStateParam.Serialize take invalid Index");

                base.Serialize(stream, offset);
                offset += base.Len;
                stream[offset] = WsState;
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tMeshSetTrigParam : tAppHeader
    {
        // 触发是否开启
        public enMeshEnableFun Enable = enMeshEnableFun.eCloseFun;
        // Acc波形类型
        public tAccWvEvDef AccMdf = new tAccWvEvDef();
        // Vel波形类型
        public tVelWvEvDef VelMdf = new tVelWvEvDef();
        // Dsp波形类型
        public tDspWvEvDef DspMdf = new tDspWvEvDef();
        // AccEnv波形类型
        public tAccEnvWvEvDef AccEnvMdf = new tAccEnvWvEvDef();

        public tMeshSetTrigParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eSet;
            base.u8SubCMD = (byte)enAppSetSubCMD.eTrigParam;
            base.isRequest = true;
            base.u8ParamLen = 45;
        }

        public new int Len
        {
            get
            {
                return base.u8ParamLen + base.Len;
            }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetTriggerUploadParam.Serialize take invalid parameter");

                base.Serialize(stream, offset);
                offset += base.Len;
                stream[offset++] = (byte)Enable;

                // 只要加速度开启至少一个特征值有效，则此波形的触发式功能有效
                if (AccMdf.bAccWaveRMSValid || AccMdf.bAccWavePKValid)
                {
                    stream[offset] = (byte)enMeasDefType.eAccWaveform;
                    if (AccMdf.bAccWaveRMSValid)
                    {
                        stream[offset] = (byte)(stream[offset] | (byte)(AccWvEvDef.eRMS) << 4);
                        DataTypeConverter.FloatToMeshByteArr(AccMdf.fAccRMSValue, stream, offset + 1);
                    }
                    if (AccMdf.bAccWavePKValid)
                    {
                        stream[offset] = (byte)(stream[offset] | (byte)(AccWvEvDef.ePK) << 4);
                        DataTypeConverter.FloatToMeshByteArr(AccMdf.fAccPKValue, stream, offset + 5);
                    }
                }
                else
                {
                    stream[offset] = 0;
                }
                offset += 9;

                // 只要速度开启至少一个特征值有效，则此波形的触发式功能有效
                if (VelMdf.bVelWaveRMSValid || VelMdf.bVelWaveLPEValid || VelMdf.bVelWaveMPEValid || VelMdf.bVelWaveHPEValid)
                {
                    stream[offset] = (byte)enMeasDefType.eVelWaveform;
                    if (VelMdf.bVelWaveRMSValid)
                    {
                        stream[offset] = (byte)(stream[offset] | (byte)(VelWvEvDef.eRMS) << 4);
                        DataTypeConverter.FloatToMeshByteArr(VelMdf.fVelRMSValue, stream, offset + 1);
                    }
                    if (VelMdf.bVelWaveLPEValid)
                    {
                        stream[offset] = (byte)(stream[offset] | (byte)(VelWvEvDef.eLPE) << 4);
                        DataTypeConverter.FloatToMeshByteArr(VelMdf.fVelLPEValue, stream, offset + 5);
                    }
                    if (VelMdf.bVelWaveMPEValid)
                    {
                        stream[offset] = (byte)(stream[offset] | (byte)(VelWvEvDef.eMPE) << 4);
                        DataTypeConverter.FloatToMeshByteArr(VelMdf.fVelMPEValue, stream, offset + 9);
                    }
                    if (VelMdf.bVelWaveHPEValid)
                    {
                        stream[offset] = (byte)(stream[offset] | (byte)(VelWvEvDef.eHPE) << 4);
                        DataTypeConverter.FloatToMeshByteArr(VelMdf.fVelHPEValue, stream, offset + 13);
                    }
                }
                else
                {
                    stream[offset] = 0;
                }
                offset += 17;

                // 只要位移开启至少一个特征值有效，则此波形的触发式功能有效
                if (DspMdf.bDspWavePKPKValid)
                {
                    stream[offset] = (byte)enMeasDefType.eDspWaveform;
                    stream[offset] = (byte)(stream[offset] | (byte)(DspWvEvDef.ePPK) << 4);
                    DataTypeConverter.FloatToMeshByteArr(DspMdf.fDspPKPKValue, stream, offset + 1);
                }
                else
                {
                    stream[offset] = 0;
                }
                offset += 5;

                // 只要加速度包络开启至少一个特征值有效，则此波形的触发式功能有效
                if (AccEnvMdf.bAccEnvWavePKValid || AccEnvMdf.bAccEnvWavePKCValid || AccEnvMdf.bAccEnvWaveMEANValid)
                {
                    stream[offset] = (byte)enMeasDefType.eAccEnvelope;
                    if (AccEnvMdf.bAccEnvWavePKValid)
                    {
                        stream[offset] = (byte)(stream[offset] | (byte)(AevWvEvDef.ePK) << 4);
                        DataTypeConverter.FloatToMeshByteArr(AccEnvMdf.fAccEnvPKValue, stream, offset + 1);
                    }
                    if (AccEnvMdf.bAccEnvWavePKCValid)
                    {
                        stream[offset] = (byte)(stream[offset] | (byte)(AevWvEvDef.ePKC) << 4);
                        DataTypeConverter.FloatToMeshByteArr(AccEnvMdf.fAccEnvPKCValue, stream, offset + 5);
                    }
                    if (AccEnvMdf.bAccEnvWaveMEANValid)
                    {
                        stream[offset] = (byte)(stream[offset] | (byte)(AevWvEvDef.eMEAN) << 4);
                        DataTypeConverter.FloatToMeshByteArr(AccEnvMdf.fAccEnvMEANValue, stream, offset + 9);
                    }
                }
                else
                {
                    stream[offset] = 0;
                }
                offset += 13;
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tSetWsRouteMode : tAppHeader
    {
        public byte WsRouteMode = 0;

        public tSetWsRouteMode()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eSet;
            base.u8SubCMD = (byte)enAppSetSubCMD.eWsRouteMode;
            base.isRequest = true;
            base.u8ParamLen = 1;
        }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetWsRouteParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetWsRouteParam.Serialize take invalid Index");

                base.Serialize(stream, offset);
                offset += base.Len;
                stream[offset] = WsRouteMode;
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tSetWsDebugMode : tAppHeader
    {
        public byte WsDebugMode { get; set; }

        public tSetWsDebugMode()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eSet;
            base.u8SubCMD = (byte)enAppSetSubCMD.eWsDebugMode;
            base.isRequest = true;
            base.u8ParamLen = 1;
        }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetWsDebugParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetWsDebugParam.Serialize take invalid Index");

                base.Serialize(stream, offset);
                stream[offset + base.Len] = WsDebugMode;
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tCtlWsUpstrParam : tAppHeader
    {
        // 控制字段：0表示进入升级周期，1表示退出升级周期
        public byte u8Control { get; set; }

        public tCtlWsUpstrParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eUpdate;
            base.u8SubCMD = (byte)enAppUpdateSubCMD.eControl;
            base.isRequest = true;
            base.u8ParamLen = 1;
        }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tControlWsUpstreamParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tControlWsUpstreamParam.Serialize take invalid Index");

                base.Serialize(stream, offset);
                stream[offset + base.Len] = u8Control;
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    
#endregion

#region 获取类命令参数
    public class tGetSelfReportParam : tAppHeader
    {
        public tGetSelfReportParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eGet;
            base.u8SubCMD = (byte)enAppGetSubCMD.eSelfReport;
            base.isRequest = true;
            base.u8ParamLen = 0;
        }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tGetSelfReportParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tGetSelfReportParam.Serialize take invalid Index");

                base.Serialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tGetMeasDefParam : tAppHeader
    {
        public tGetMeasDefParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eGet;
            base.u8SubCMD = (byte)enAppGetSubCMD.eMeasDef;
            base.isRequest = true;
            base.u8ParamLen = 0;
        }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tGetMeasDefParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tGetMeasDefParam.Serialize take invalid Index");

                base.Serialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tGetWsSnParam : tAppHeader
    {
        public tGetWsSnParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eGet;
            base.u8SubCMD = (byte)enAppGetSubCMD.eSn;
            base.isRequest = true;
            base.u8ParamLen = 0;
        }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tGetWsSnParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tGetWsSnParam.Serialize take invalid Index");

                base.Serialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tGetCaliCoeffParam : tAppHeader
    {
        public tGetCaliCoeffParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eGet;
            base.u8SubCMD = (byte)enAppGetSubCMD.eCaliCoeff;
            base.isRequest = true;
            base.u8ParamLen = 0;
        }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tGetCaliCoeffParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tGetCaliCoeffParam.Serialize take invalid Index");

                base.Serialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tGetADCloseVoltParam : tAppHeader
    {
        public tGetADCloseVoltParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eGet;
            base.u8SubCMD = (byte)enAppGetSubCMD.eADCloseVolt;
            base.isRequest = true;
            base.u8ParamLen = 0;
        }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tGetADCloseVoltParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tGetADCloseVoltParam.Serialize take invalid Index");

                base.Serialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tGetWsStartOrStopParam : tAppHeader
    {
        public tGetWsStartOrStopParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eGet;
            base.u8SubCMD = (byte)enAppGetSubCMD.eRevStop;
            base.isRequest = true;
            base.u8ParamLen = 0;
        }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tGetWsStartOrStopParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tGetWsStartOrStopParam.Serialize take invalid Index");

                base.Serialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tGetTrigParam : tAppHeader
    {
        public tGetTrigParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eGet;
            base.u8SubCMD = (byte)enAppGetSubCMD.eTrigParam;
            base.isRequest = true;
            base.u8ParamLen = 0;
        }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tGetTrigParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tGetTrigParam.Serialize take invalid Index");

                base.Serialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tGetWsRouteMode : tAppHeader
    {
        public tGetWsRouteMode()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eGet;
            base.u8SubCMD = (byte)enAppGetSubCMD.eWsRouteMode;
            base.isRequest = true;
            base.u8ParamLen = 0;
        }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tGetWsRouteMode.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tGetWsRouteMode.Serialize take invalid Index");

                base.Serialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
#endregion

#region 升级类命令参数
    public class tSetFwDescInfoParam : tAppHeader//未处理
    {
        /// <summary>
        /// 固件魔术字
        /// </summary>
        public UInt32 u32MagicWord = 0;
        /// <summary>
        /// 更新固件版本号
        /// </summary>
        public tVer FwVer = new tVer();
        /// <summary>
        /// 固件大小
        /// </summary>
        public UInt32 u32FwSize = 0;
        /// <summary>
        /// 固件分包总个数
        /// </summary>
        public UInt16 u16TotalBlkNum = 0;
        /// <summary>
        /// 单个固件分包最大值
        /// </summary>
        public byte u8MaxBlkSize = 0;
        /// <summary>
        /// 固件执行切入点
        /// </summary>
        public UInt32 u32EntryPoint = 0;

        public tSetFwDescInfoParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eUpdate;
            base.u8SubCMD = (byte)enAppUpdateSubCMD.eFwDesc;
            base.isRequest = true;
            base.u8ParamLen = 18;
        }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetFwDescInfoParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetFwDescInfoParam.Serialize take invalid Index");

                base.Serialize(stream, offset);
                DataTypeConverter.UInt32ToMeshByteArr(u32MagicWord, stream, offset + base.Len);
                stream[offset + base.Len + 4] = FwVer.u8Main;
                stream[offset + base.Len + 5] = FwVer.u8Sub;
                stream[offset + base.Len + 6] = FwVer.u8Rev;
                stream[offset + base.Len + 7] = FwVer.u8Build;
                DataTypeConverter.UInt32ToMeshByteArr(u32FwSize, stream, offset + base.Len + 8);
                //DataTypeConverter.UInt16ToMeshByteArr(u16TotalDataPacketNum, stream, offset + base.Len + 12);
                stream[offset + base.Len + 13] = u8MaxBlkSize;
                DataTypeConverter.UInt32ToMeshByteArr(u32EntryPoint, stream, offset + base.Len + 14);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tSetFwDataParam : tAppHeader
    {
        /// <summary>
        /// 当前固件分块数据编号
        /// </summary>
        public UInt16 u16BlkIdx = 0;
        /// <summary>
        /// 当前固件分块数据大小
        /// </summary>
        private byte u8BlkSize = 0;
        /// <summary>
        /// 当前固件分块数据
        /// </summary>
        public byte[] u8aData = null;

        public byte u8DataPacketSize
        {
            get { return u8BlkSize; }
            set
            {
                u8BlkSize = value;
                base.u8ParamLen = (byte)(3 + u8BlkSize);
            }
        }

        public tSetFwDataParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eUpdate;
            base.u8SubCMD = (byte)enAppUpdateSubCMD.eFwData;
            base.isRequest = true;
            base.u8ParamLen = (byte)(3 + u8DataPacketSize);
        }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetFwDataParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetFwDataParam.Serialize take invalid Index");

                base.Serialize(stream, offset);
                DataTypeConverter.UInt16ToMeshByteArr(u16BlkIdx, stream, offset + base.Len);
                stream[offset + base.Len + 2] = u8DataPacketSize;
                Array.Copy(u8aData, 0, stream, offset + base.Len + 3, u8DataPacketSize);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
#endregion

#region 恢复类命令参数
    public class tMeshRestoreWSParam : tAppHeader
    {
        //恢复出厂测试的目标：0：整机WS；1：MCU；2：Mote；
        public byte u8Target { get; set; }

        public tMeshRestoreWSParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eRestore;
            base.u8SubCMD = (byte)enAppRestoreSubCMD.eWS;
            base.isRequest = true;
            base.u8ParamLen = 1;
        }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tRestoreWSParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tRestoreWSParam.Serialize take invalid Index");

                base.Serialize(stream, offset);

                stream[offset + base.Len] = u8Target;
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tMeshResetWSParam : tAppHeader
    {
        //恢复出厂测试的目标：0：整机WS；1：MCU；2：Mote；
        public byte u8Target { get; set; }

        public tMeshResetWSParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eReset;
            base.u8SubCMD = (byte)enAppResetSubCMD.eWS;
            base.isRequest = true;
            base.u8ParamLen = 1;
        }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tResetWSParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tResetWSParam.Serialize take invalid Index");

                base.Serialize(stream, offset);
                stream[offset + base.Len] = u8Target;
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
#endregion

#endregion

#region 服务器对网络请求的应答结果定义

#region 设置类命令结果定义

    public class tCaliTimeResult : tAppHeader
    {
    }
    public class tMeshSetWsNwIdResult : tAppHeader
    {
        public new int Len
        {
            get { return base.Len; }
            set { ; }
        }
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetNetworkIdResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetNetworkIdResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tMeshSetMeasDefResult : tAppHeader
    {
        public new int Len
        {
            get { return base.Len; }
            set { ; }
        }
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetMeasDefResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetMeasDefResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
       
    }
    public class tMeshSetWsSnResult : tAppHeader
    {
        public new int Len
        {
            get { return base.Len; }
            set { ; }
        }
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetWsSnResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetWsSnResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tMeshCaliSensorResult : tAppHeader
    {
        // 保留
        public byte u8Rev { get; set; }
        // 增益
        public float f32Gain { get; set; }
        // 偏移
        public float f32Offset { get; set; }


        public new int Len
        {
            get { return (base.Len + 9); }
            set { ; }
        }

        private const byte REV_OFFSET = 0;
        private const byte GAIN_OFFSET = REV_OFFSET + 1;
        private const byte OFF_OFFSET = GAIN_OFFSET + 4;

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tCaliSensorResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tCaliSensorResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);

                u8Rev = stream[offset + base.Len + REV_OFFSET];
                f32Gain = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + GAIN_OFFSET);
                f32Offset = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + OFF_OFFSET);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tMeshSetADCloseVoltResult : tAppHeader
    {
        public new int Len
        {
            get { return base.Len; }
            set { ; }
        }
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetADCloseVoltResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetADCloseVoltResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tMeshSetWsStartOrStopResult : tAppHeader
    {
        public new int Len
        {
            get { return base.Len; }
            set { ; }
        }
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetWsStartOrStopResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetWsStartOrStopResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tMeshSetTrigParamResult : tAppHeader
    {
        public new int Len
        {
            get { return base.Len; }
            set { ; }
        }
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetTrigParamResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetTrigParamResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tMeshSetWsRouteModeResult : tAppHeader
    {
        public new int Len
        {
            get { return base.Len; }
            set { ; }
        }
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetWsRouteModeResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetWsRouteModeResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tSetWsDebugModeResult : tAppHeader
    {
        public new int Len
        {
            get { return base.Len; }
            set { ; }
        }
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetWsDebugModeResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetWsDebugModeResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

#endregion

#region 获取类命令结果定义
    public class tGetSelfReportResult : tAppHeader
    {
        // 因联主程序固件版本信息
        public tVer verMcuFw = null;
        // 因联启动程序固件版本信息
        public tVer verMcuBl = null;
        // 协议栈组件版本信息
        public tVer verMoteStack = null;
        // 保留
        public tVer verRsv = null;
        //传感器类型定义(可按位与)：0x01，振动(Vib)；0x02，温度(Tmp)；
        public List<enSensorType> SensorTypeMap = null;
        //振动传感器其他功能，局部同步，全局同步，可按位与
        public List<enVibFuncMap> VibFuncMap = null;
        //振动传感器支持的波形类型定义(可按位与)
        //0x01，加速度波形(Acc)；0x02，速度波形(Vel)；
        //0x04，位移波形(Dsp)；0x08，加速度包络波形(AccEvp)；
        //0x10，LQ波形(LQ)；0x20，启停机波形(SS)；
        public List<enVibWvTMap> VibWvTMap = null;
        //振动传感器支持的特征值类型定义(可按位与)：
        //0x0001，有效值(RMS)；0x0002，峰值(PK)；0x0004，峰峰值(PPK)；
        //0x0008，地毯值(PKC)；0x0010，均值(MEAN)；0x0020，低频能量(LPE)；
        //0x0040，中频能量(MPE)0x0080，高频能量(HPE)；
        public List<enVibEvTMap> VibEvTMap = null;

        public tGetSelfReportResult()
        {
            verMcuFw      = new tVer();
            verMcuBl      = new tVer();
            verMoteStack  = new tVer();
            verRsv        = new tVer();
            SensorTypeMap = new List<enSensorType>();
            VibFuncMap    = new List<enVibFuncMap>();
            VibWvTMap     = new List<enVibWvTMap>();
            VibEvTMap     = new List<enVibEvTMap>();
            base.u8ParamLen = 21;
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            byte u8SensorType = 0;
            byte u8VibFunc = 0;
            byte u8VibWvT = 0;
            UInt16 u16VibEvT = 0;
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("take invalid parameter");

                base.Unserialize(stream, offset);
                offset += base.Len;
                verMcuFw.u8Main      = stream[offset++];
                verMcuFw.u8Sub       = stream[offset++];
                verMcuFw.u8Rev       = stream[offset++];
                verMcuFw.u8Build     = stream[offset++];
                verMcuBl.u8Main      = stream[offset++];
                verMcuBl.u8Sub       = stream[offset++];
                verMcuBl.u8Rev       = stream[offset++];
                verMcuBl.u8Build     = stream[offset++];
                verMoteStack.u8Main  = stream[offset++];
                verMoteStack.u8Sub   = stream[offset++];
                verMoteStack.u8Rev   = stream[offset++];
                verMoteStack.u8Build = stream[offset++];
                verRsv.u8Main        = stream[offset++];
                verRsv.u8Sub         = stream[offset++];
                verRsv.u8Rev         = stream[offset++];
                verRsv.u8Build       = stream[offset++];
                u8SensorType         = stream[offset++];
                u8VibFunc            = stream[offset++];
                u8VibWvT             = stream[offset++];
                u16VibEvT = DataTypeConverter.MeshByteArrToUInt16(stream, offset);

                if ((u8SensorType & (byte)enSensorType.eVib) != 0)
                    SensorTypeMap.Add(enSensorType.eVib);
                if ((u8SensorType & (byte)enSensorType.eTmp) != 0)
                    SensorTypeMap.Add(enSensorType.eTmp);

                if ((u8VibFunc & (byte)enVibFuncMap.eFacilitySync) != 0)
                    VibFuncMap.Add(enVibFuncMap.eFacilitySync);
                if ((u8VibFunc & (byte)enVibFuncMap.eMeshSync) != 0)
                    VibFuncMap.Add(enVibFuncMap.eMeshSync);

                if ((u8VibWvT & (byte)enVibWvTMap.eAcc) != 0)
                    VibWvTMap.Add(enVibWvTMap.eAcc);
                if ((u8VibWvT & (byte)enVibWvTMap.eVel) != 0)
                    VibWvTMap.Add(enVibWvTMap.eVel);
                if ((u8VibWvT & (byte)enVibWvTMap.eDsp) != 0)
                    VibWvTMap.Add(enVibWvTMap.eDsp);
                if ((u8VibWvT & (byte)enVibWvTMap.eAccEvp) != 0)
                    VibWvTMap.Add(enVibWvTMap.eAccEvp);
                if ((u8VibWvT & (byte)enVibWvTMap.eLQ) != 0)
                    VibWvTMap.Add(enVibWvTMap.eLQ);
                if ((u8VibWvT & (byte)enVibWvTMap.eRevStop) != 0)
                    VibWvTMap.Add(enVibWvTMap.eRevStop);

                if ((u16VibEvT & (byte)enVibEvTMap.eRMS) != 0)
                    VibEvTMap.Add(enVibEvTMap.eRMS);
                if ((u16VibEvT & (byte)enVibEvTMap.ePK) != 0)
                    VibEvTMap.Add(enVibEvTMap.ePK);
                if ((u16VibEvT & (byte)enVibEvTMap.ePPK) != 0)
                    VibEvTMap.Add(enVibEvTMap.ePPK);
                if ((u16VibEvT & (byte)enVibEvTMap.ePKC) != 0)
                    VibEvTMap.Add(enVibEvTMap.ePKC);
                if ((u16VibEvT & (byte)enVibEvTMap.eMEAN) != 0)
                    VibEvTMap.Add(enVibEvTMap.eMEAN);
                if ((u16VibEvT & (byte)enVibEvTMap.eLPE) != 0)
                    VibEvTMap.Add(enVibEvTMap.eLPE);
                if ((u16VibEvT & (byte)enVibEvTMap.eMPE) != 0)
                    VibEvTMap.Add(enVibEvTMap.eMPE);
                if ((u16VibEvT & (byte)enVibEvTMap.eHPE) != 0)
                    VibEvTMap.Add(enVibEvTMap.eHPE);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "tGetSelfReportResult.Unserialize " + ex.Message.ToString());
            }
        }
    }
    public class tGetMeasDefResult : tAppHeader
    {
        // 采集方式
        public enWsDaqMode DaqMode = enWsDaqMode.eTiming;
        // 定时温度采集周期时&分
        public tTimingTime TmpDaqPeriod = new tTimingTime();
        // 特征值采集周期（该值为温度采集周期的整倍数值）
        public UInt16 u16EigenDaqMult = 1;
        // 波形采集周期（该值为特征值采集周期的整倍数值）
        public UInt16 u16WaveDaqMult = 1;
        // 频谱低频段上限频率
        public UInt16 LFBSpcUpFreq = 0;
        // 频谱低频段下限频率
        public UInt16 LFBSpcLwFreq = 0;
        // 频谱中频段上限频率
        public UInt16 MFBSpcUpFreq = 0;
        // 频谱中频段下限频率
        public UInt16 MFBSpcLwFreq = 0;
        // 频谱高频段上限频率
        public UInt16 HFBSpcUpFreq = 0;
        // 频谱高频段下限频率
        public UInt16 HFBSpcLwFreq = 0;
        //测量定义数量
        public byte u8MeasDefCnt = 0;
        // 加速度波形测量定义
        public tMeshMeasDef AccMdf = null;
        // 速度波形测量定义
        public tMeshMeasDef VelMdf = null;
        // 位移波形测量定义
        public tMeshMeasDef DspMdf = null;
        // 加速度包络波形测量定义
        public tMeshMeasDef AccEnvMdf = null;
        // LQ波形测量定义
        public tMeshMeasDef LQMdf = null;
        // 启停机测量定义
        public tMeshMeasDef RevStop = null;

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("take invalid parameter");

                base.Unserialize(stream, offset);
                offset += base.Len;
                DaqMode             = (enWsDaqMode)stream[offset++];
                TmpDaqPeriod.u8Hour = stream[offset++];
                TmpDaqPeriod.u8Min  = stream[offset++];
                u16EigenDaqMult     = (UInt16)DataTypeConverter.MeshByteArrToInt16(stream, offset);
                offset += 2;
                u16WaveDaqMult      = (UInt16)DataTypeConverter.MeshByteArrToInt16(stream, offset);
                offset += 2;
                LFBSpcUpFreq        = (UInt16)DataTypeConverter.MeshByteArrToInt16(stream, offset);
                offset += 2;
                LFBSpcLwFreq        = (UInt16)DataTypeConverter.MeshByteArrToInt16(stream, offset);
                offset += 2;
                MFBSpcUpFreq        = (UInt16)DataTypeConverter.MeshByteArrToInt16(stream, offset);
                offset += 2;
                MFBSpcLwFreq        = (UInt16)DataTypeConverter.MeshByteArrToInt16(stream, offset);
                offset += 2;
                HFBSpcUpFreq        = (UInt16)DataTypeConverter.MeshByteArrToInt16(stream, offset);
                offset += 2;
                HFBSpcLwFreq        = (UInt16)DataTypeConverter.MeshByteArrToInt16(stream, offset);
                offset += 2;
                u8MeasDefCnt        = stream[offset++];

                for (int i = 0; i < u8MeasDefCnt; i++)
                {
                    enMeasDefType mdtype = (enMeasDefType)(stream[offset] & 0x0F);
                    if (mdtype == enMeasDefType.eAccWaveform)
                    {
                        AccMdf = new tMeshMeasDef();
                        AccMdf.Unserialize(stream, offset);
                        offset += AccMdf.Len;
                    }
                    else if (mdtype == enMeasDefType.eVelWaveform)
                    {
                        VelMdf = new tMeshMeasDef();
                        VelMdf.Unserialize(stream, offset);
                        offset += VelMdf.Len;
                    }
                    else if (mdtype == enMeasDefType.eDspWaveform)
                    {
                        DspMdf = new tMeshMeasDef();
                        DspMdf.Unserialize(stream, offset);
                        offset += DspMdf.Len;
                    }
                    else if (mdtype == enMeasDefType.eAccEnvelope)
                    {
                        AccEnvMdf = new tMeshMeasDef();
                        AccEnvMdf.Unserialize(stream, offset);
                        offset += AccEnvMdf.Len;
                    }
                    else if (mdtype == enMeasDefType.eLQform)
                    {
                        LQMdf = new tMeshMeasDef();
                        LQMdf.Unserialize(stream, offset);
                        offset += LQMdf.Len;
                    }
                    else if (mdtype == enMeasDefType.eRevStopform)
                    {
                        RevStop = new tMeshMeasDef();
                        RevStop.Unserialize(stream, offset);
                        offset += RevStop.Len;
                    }
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "tGetMeasDefResult.Serialize " + ex.Message.ToString());
            }
        }
    }

    public class tGetWsSnResult : tAppHeader
    {
        public byte u8SnLen { get; set; }
        public string sn { get; set; }

        public tGetWsSnResult()
        {
            sn = null;
        }

        public new int Len
        {
            get { return (base.Len + sn.Length + 1); }
            set { ; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("take invalid parameter");

                base.Unserialize(stream, offset);
                offset += base.Len;
                u8SnLen = stream[offset++];
                byte[] strBytes = new byte[u8SnLen];
                Array.Copy(stream, offset, strBytes, 0, u8SnLen);
                sn = System.Text.Encoding.Default.GetString(strBytes);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "tGetWsSnResult.Unserialize " + ex.Message.ToString());
            }
        }
    }

    public class tGetSensorCaliResult : tAppHeader
    {
        public byte Flag { get; set; }
        public float Gain { get; set; }
        public float Offset { get; set; }

        public tGetSensorCaliResult()
        {
            Flag = 0;
        }

        public new int Len
        {
            get { return (base.Len + 9); }
            set { ; }
        }

        private const byte FLAG_OFFSET = 0;
        private const byte GAIN_OFFSET = FLAG_OFFSET + 1;
        private const byte OFFSET_OFFSET = GAIN_OFFSET + 4;
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("take invalid parameter");
                base.Unserialize(stream, offset);
                Flag = stream[offset + base.Len + FLAG_OFFSET];
                Gain = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + GAIN_OFFSET);
                Offset = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + OFFSET_OFFSET);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "tGetSensorCaliResult.Unserialize " + ex.Message.ToString());
            }
        }
    }

    public class tGetADCloseVoltResult : tAppHeader
    {
        public float VoltageLimit { get; set; }
        public byte VoltageCount { get; set; }

        public tGetADCloseVoltResult()
        {
            VoltageLimit = 0;
            VoltageCount = 0;
        }

        public new int Len
        {
            get { return (base.Len + 6); }
            set { ; }
        }

        private const byte VL_OFFSET = 0;
        private const byte VC_OFFSET = VL_OFFSET + 4;
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("take invalid parameter");               
                base.Unserialize(stream, offset);

                VoltageLimit = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + VL_OFFSET);
                VoltageCount = stream[offset + base.Len + VC_OFFSET];
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "tGetADCloseVoltResult.Unserialize " + ex.Message.ToString());
            }
        }
    }

    public class tGetRevStopResult : tAppHeader
    {
        public byte bStart { get; set; }

        public tGetRevStopResult()
        {
            bStart = 0;
        }

        public new int Len
        {
            get { return (base.Len + 1); }
            set { ; }
        }

        private const byte START_OFFSET = 0;
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("take invalid parameter");
                base.Unserialize(stream, offset);
                
                bStart = stream[offset + base.Len + START_OFFSET];
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "tGetRevStopResult.Unserialize " + ex.Message.ToString());
            }
        }
    }

    public class tGetTrigParamResult : tAppHeader
    {
        // 触发是否开启
        public enEnableFun Enable = enEnableFun.eCloseFun;
        // Acc波形类型
        public tAccWvEvDef AccMdf = new tAccWvEvDef();
        // Vel波形类型
        public tVelWvEvDef VelMdf = new tVelWvEvDef();
        // Dsp波形类型
        public tDspWvEvDef DspMdf = new tDspWvEvDef();
        // AccEnv波形类型
        public tAccEnvWvEvDef AccEnvMdf = new tAccEnvWvEvDef();

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("take invalid parameter");

                base.Unserialize(stream, offset);
                offset += base.Len;
                Enable = (enEnableFun)stream[offset++];

                byte accwevdef = stream[offset];
                if ((byte)(accwevdef >> 4 & (byte)(AccWvEvDef.eRMS)) != 0)
                {
                    AccMdf.bAccWaveRMSValid = true;
                    AccMdf.fAccRMSValue = DataTypeConverter.MeshByteArrToFloat(stream, offset + 1);
                }
                if ((byte)(accwevdef >> 4 & (byte)(AccWvEvDef.ePK)) != 0)
                {
                    AccMdf.bAccWavePKValid = true;
                    AccMdf.fAccPKValue = DataTypeConverter.MeshByteArrToFloat(stream, offset + 5);
                }
                offset += 9;

                byte velwevdef = stream[offset];
                if ((byte)(velwevdef >> 4 & (byte)(VelWvEvDef.eRMS)) != 0)
                {
                    VelMdf.bVelWaveRMSValid = true;
                    VelMdf.fVelRMSValue = DataTypeConverter.MeshByteArrToFloat(stream, offset + 1);
                }
                if ((byte)(velwevdef >> 4 & (byte)(VelWvEvDef.eLPE)) != 0)
                {
                    VelMdf.bVelWaveLPEValid = true;
                    VelMdf.fVelLPEValue = DataTypeConverter.MeshByteArrToFloat(stream, offset + 5);
                }
                if ((byte)(velwevdef >> 4 & (byte)(VelWvEvDef.eMPE)) != 0)
                {
                    VelMdf.bVelWaveMPEValid = true;
                    VelMdf.fVelMPEValue = DataTypeConverter.MeshByteArrToFloat(stream, offset + 9);
                }
                if ((byte)(velwevdef >> 4 & (byte)(VelWvEvDef.eHPE)) != 0)
                {
                    VelMdf.bVelWaveHPEValid = true;
                    VelMdf.fVelHPEValue = DataTypeConverter.MeshByteArrToFloat(stream, offset + 13);
                }
                offset += 17;

                byte dspwevdef = stream[offset];
                if ((byte)(dspwevdef >> 4 & (byte)(DspWvEvDef.ePPK)) != 0)
                {
                    DspMdf.bDspWavePKPKValid = true;
                    DspMdf.fDspPKPKValue = DataTypeConverter.MeshByteArrToFloat(stream, offset + 1);
                }
                offset += 5;

                byte aevwevdef = stream[offset];
                if ((byte)(aevwevdef >> 4 & (byte)(AevWvEvDef.ePK)) != 0)
                {
                    AccEnvMdf.bAccEnvWavePKValid = true;
                    AccEnvMdf.fAccEnvPKValue = DataTypeConverter.MeshByteArrToFloat(stream, offset + 1);
                }
                if ((byte)(aevwevdef >> 4 & (byte)(AevWvEvDef.ePKC)) != 0)
                {
                    AccEnvMdf.bAccEnvWavePKCValid = true;
                    AccEnvMdf.fAccEnvPKCValue = DataTypeConverter.MeshByteArrToFloat(stream, offset + 5);
                }
                if ((byte)(aevwevdef >> 4 & (byte)(AevWvEvDef.eMEAN)) != 0)
                {
                    AccEnvMdf.bAccEnvWaveMEANValid = true;
                    AccEnvMdf.fAccEnvMEANValue = DataTypeConverter.MeshByteArrToFloat(stream, offset + 9);
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "tGetTrigParamResult.Serialize " + ex.Message.ToString());
            }
        }
    }

    public class tGetWsRouteResult : tAppHeader
    {    
        public byte mode { get; set; }

        public tGetWsRouteResult()
        {
            mode = 0;
        }

        public new int Len
        {
            get { return (base.Len + 1); }
            set { ; }
        }

        private const byte MODE_OFFSET = 0;
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("take invalid parameter");
                base.Unserialize(stream, offset);

                mode = stream[offset + base.Len + MODE_OFFSET];
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "tGetADCloseVoltResult.Unserialize " + ex.Message.ToString());
            }
        }
    }

#endregion

#region 恢复类命令结果定义

    public class tMeshRestoreWSResult : tAppHeader
    {
        public new int Len
        {
            get { return base.Len; }
            set { ; }
        }
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("take invalid parameter");

                base.Unserialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "tRestoreWSResult.Unserialize " + ex.Message.ToString());
            }
        }
    }

    public class tMeshRestoreWGResult : tAppHeader
    {
        public new int Len
        {
            get { return base.Len ; }
            set { ; }
        }

        private const byte RC_OFFSET = 0;
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tRestoreWGResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tRestoreWGResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    public class tMeshResetWSResult : tAppHeader
    {
        public new int Len
        {
            get { return base.Len; }
            set { ; }
        }
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("take invalid parameter");

                base.Unserialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "tResetWSResult.Unserialize " + ex.Message.ToString());
            }
        }
    }

    public class tMeshResetWGResult : tAppHeader
    {

        public new int Len
        {
            get { return base.Len; }
            set { ; }
        }

        private const byte RC_OFFSET = 0;
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tResetWGResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tResetWGResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

#endregion

#region 升级类命令结果定义
    public class tSetFwDescInfoResult : tAppHeader
    {

        public new int Len
        {
            get { return (base.Len ); }
            set { ; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("take invalid parameter");

                base.Unserialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "tSetFwDataResult.Unserialize " + ex.Message.ToString());
            }
        }
    }

    public class tSetFwDataResult : tAppHeader
    {
        // 需求的数据块号
        public UInt16[] u16FwBlock { get; set; }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }


        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("take invalid parameter");

                base.Unserialize(stream, offset);

                if (base.u8RC == 1)
                {
                    u16FwBlock = new UInt16[base.u8ParamLen / 2];
                    for (int i = 0; i < base.u8ParamLen / 2; i++)
                    {
                        u16FwBlock[i] = DataTypeConverter.MeshByteArrToUInt16(stream, offset + base.Len + i * 2);
                    }
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, "tSetFwDataResult.Unserialize " + ex.Message.ToString());
            }
        }
    }

    public class tCtlWsUpstrResult : tAppHeader
    {
        public new int Len
        {
            get { return (base.Len); }
            set { ; }
        }

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetFwDataResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetFwDataResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

#endregion

#endregion

#region 网络上传的请求对应应答参数定义

    public class tMeshSelfReportResult : tAppHeader
    {
        public tMeshSelfReportResult()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eNotify;
            base.u8SubCMD = (byte)enAppNotifySubCMD.eSelfReport;
            base.isRequest = false;
            base.u8RC = u8RC;
        }

        public new int Len
        {
            get { return  base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSelfReportResult.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSelfReportResult.Serialize take invalid Index");

                base.Serialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    public class tHealthReportResult : tAppHeader
    {
        public tHealthReportResult()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eNotify;
            base.u8SubCMD = (byte)enAppNotifySubCMD.eHealthReport;
            base.isRequest = false;
            base.u8RC = u8RC;
        }

        public new int Len
        {
            get { return base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSelfReportResult.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSelfReportResult.Serialize take invalid Index");

                base.Serialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    public class tMeshWaveDescResult : tAppHeader
    {
        public tMeshWaveDescResult()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eNotify;
            base.u8SubCMD = (byte)enAppNotifySubCMD.eWaveDesc;
            base.isRequest = false;
            base.u8RC = u8RC;
        }

        public new int Len
        {
            get { return  base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tWaveDescResult.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tWaveDescResult.Serialize take invalid Index");

                base.Serialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    public class tMeshWaveDataResult : tAppHeader
    {
        // 响应码
        private UInt16[] aLostNum = null;
        public UInt16[] u16aLostNum
        {
            get { return aLostNum; }
            set
            {
                if (value == null || value.Length <= 0 || value.Length > 36)
                    throw new Exception("tWaveDataResult set invalid value");

                base.u8ParamLen = (byte)(2 * value.Length);
                aLostNum = value;
            }
        }

        /// <summary>
        /// 要求重传的波形分块个数
        /// </summary>
        public byte u8ReqWaveBlkCnt
        {
            set
            {
                if (0 > value || value > 30)
                    throw new Exception("Max request wave block number is 30");

                u16aLostNum = new UInt16[value];
                base.u8ParamLen = (byte)(2 * value);
            }
        }

        public tMeshWaveDataResult()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eNotify;
            base.u8SubCMD = (byte)enAppNotifySubCMD.eWaveData;
            base.isRequest = false;
            base.u8ParamLen = 0;
        }

        public new int Len
        {
            get { return base.u8ParamLen + base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tWaveDataResult.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tWaveDataResult.Serialize take invalid Index");

                base.Serialize(stream, offset);
                if (base.u8ParamLen == 0)
                    return;
                for (int i = 0; i < aLostNum.Length; i++)
                {
                    DataTypeConverter.UInt16ToMeshByteArr(aLostNum[i], stream, offset + base.Len + 2 * i);
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    public class tMeshEigenValueResult : tAppHeader
    {
        public tMeshEigenValueResult()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eNotify;
            base.u8SubCMD = (byte)enAppNotifySubCMD.eEigenVal;
            base.isRequest = false;
            base.u8RC = u8RC;
        }

        public new int Len
        {
            get { return base.Len; }
            set { ; }
        }

        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tEigenValueResult.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tEigenValueResult.Serialize take invalid Index");

                base.Serialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

#endregion 网络上传的请求对应应答参数定义
}
