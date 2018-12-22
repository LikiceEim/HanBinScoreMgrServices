using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMesh
{
	/// <summary>
    /// WS运行状态
    /// </summary>
    public enum enWsRunState
    {
        // 运行状态
        eRun = 0x00,
        // 初始化状态
        eIni = 0x01,
        // 发送状态
        eSnd = 0x02,
        // 采集状态
        eDaq = 0x03,
        // 故障状态
        eFut = 0x04,
        // 升级状态
        eUpt = 0x05,
    }
	/// <summary>
    /// WS采集方式
    /// </summary>
    public enum enWsDaqMode
    {
        eTiming     = 0x01,     // 定时采集
        eImmediate  = 0x02,  // 即时采集
    }
	/// 触发上传是否开启
    /// </summary>
    public enum enEnableFun
    {
        eCloseFun = 0x00,  // 关闭功能
        eOpenFun  = 0x01,  // 开启功能
    }
	/// ws的路由模式使能
    /// </summary>
    public enum enEnableRoute
    {
        eRoute   = 0x00,  // 路由模式
        eNoRoute = 0x01,  // 非路由模式
    }
	/// <summary>
    /// 特征值类型
    /// </summary>
    public enum enEigenValueType
    {
        eRMS    = 0x01,           // 有效值
        ePK     = 0x02,           // 峰值
        ePKPK   = 0x04,           // 峰峰值
        ePKC    = 0x08,           // 地毯值
        eLQ     = 0x10,           // LQ值 
    }
    /// 测量定义
    /// bWaveRMSValid:!0=特征值有效，0=特征值有效
    /// </summary>
    public class tMeasDef
    {
        // 测量定义类型
        public enMeasDefType MeasDefType { get; set; }
        // 是否订阅
        public bool bSubscribed { get; set; }
        // 特征值类型
        public byte u8EigenValueType { get; set; }
        // RMS有效
        public int bWaveRMSValid { get; set; }
        // PK有效
        public int bWavePKValid { get; set; }
        // PKPK有效
        public int bWavePKPKValid { get; set; }
        // PKC有效
        public int bWavePKCValid { get; set; }

        // 数据长度
        public UInt16 u16WaveLen { get; set; }
        // 滤波器下限频率，如果测量定义为加速度包络时，此字段表示“包络滤波器”
        public UInt16 u16LowFreq { get; set; }
        // 滤波器上限频率，如果测量定义为加速度包络时，此字段表示“包络带宽”
        public UInt16 u16UpperFreq { get; set; }

        public int Len
        {
            get { return 9; }
            set { }
        }
        /*
        public void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tMeasDef.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tMeasDef.Serialize take invalid Index");

                stream[offset] = (byte)MeasDefType;
                stream[offset + 1] = (byte)(bSubscribed ? 0x01 : 0x00);
                stream[offset + 2] = u8EigenValueType;
                DataTypeConverter.UInt16ToMeshByteArr(u16WaveLen, stream, (byte)(offset + 3));
                // 下发测量定义时先上限后下限
                DataTypeConverter.UInt16ToMeshByteArr(u16UpperFreq, stream, (byte)(offset + 5));
                DataTypeConverter.UInt16ToMeshByteArr(u16LowFreq, stream, (byte)(offset + 7));
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
                    throw new Exception("tMeasDef.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tMeasDef.Unserialize take invalid Index");

                MeasDefType = (enMeasDefType)stream[offset];
                bSubscribed = (stream[offset + 1] == 0x00) ? false : true;
                u8EigenValueType = stream[offset + 2];
                bWaveRMSValid = u8EigenValueType & (int)enEigenValueType.eRMS;
                bWavePKValid = u8EigenValueType & (int)enEigenValueType.ePK;
                bWavePKPKValid = u8EigenValueType & (int)enEigenValueType.ePKPK;
                bWavePKCValid = u8EigenValueType & (int)enEigenValueType.ePKC;
                u16WaveLen = DataTypeConverter.MeshByteArrToUInt16(stream, offset + 3);
                // 上传的测量定义先下限后上限
                u16LowFreq = DataTypeConverter.MeshByteArrToUInt16(stream, offset + 5);
                u16UpperFreq = DataTypeConverter.MeshByteArrToUInt16(stream, offset + 7);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
        */
    }

    /// <summary>
    /// 触发式测量定义(除包络外)
    /// </summary>
    public class tTriggerDef
    {
        // 测量定义类型
        public enMeasDefType MeasDefType { get; set; }
        // 是否使能
        public bool bEnable { get; set; }
        // 基准值
        public byte u8Flag { get; set; }
        // 特征值的具体数据
        public float fCharRmsvalue { get; set; }
        public float fCharPKvalue { get; set; }
        public float fCharPKPKvalue { get; set; }
        public int Len
        {
            get { return 15; }
            set { }
        }

        public void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tTriggerMeasDef.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tTriggerMeasDef.Serialize take invalid Index");

                stream[offset] = (byte)MeasDefType;
                stream[offset + 1] = (byte)(bEnable ? 0x01 : 0x00);
                stream[offset + 2] = u8Flag;
                DataTypeConverter.FloatToMeshByteArr(fCharRmsvalue, stream, (byte)(offset + 3));
                DataTypeConverter.FloatToMeshByteArr(fCharPKvalue, stream, (byte)(offset + 7));
                DataTypeConverter.FloatToMeshByteArr(fCharPKPKvalue, stream, (byte)(offset + 11));
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
                    throw new Exception("tTriggerMeasDef.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tTriggerMeasDef.Unserialize take invalid Index");

                MeasDefType = (enMeasDefType)stream[offset];
                bEnable = (stream[offset + 1] == 0x00) ? false : true;
                u8Flag = stream[offset + 2];
                fCharRmsvalue = DataTypeConverter.MeshByteArrToFloat(stream, offset + 3);
                fCharPKvalue = DataTypeConverter.MeshByteArrToFloat(stream, offset + 7);
                fCharPKPKvalue = DataTypeConverter.MeshByteArrToFloat(stream, offset + 11);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    /// <summary>
    /// 触发式包络测量定义
    /// </summary>
    public class tTriggerVelDef
    {
        // 测量定义类型
        public enMeasDefType MeasDefType { get; set; }
        // 是否使能
        public bool bEnable { get; set; }
        // 基准值
        public byte u8Flag { get; set; }
        // 特征值的具体数据
        public float fCharPKvalue { get; set; }
        public float fCharPKCvalue { get; set; }
        public int Len
        {
            get { return 15; }
            set { }
        }

        public void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tTriggerMeasDef.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tTriggerMeasDef.Serialize take invalid Index");

                stream[offset] = (byte)MeasDefType;
                stream[offset + 1] = (byte)(bEnable ? 0x01 : 0x00);
                stream[offset + 2] = u8Flag;
                DataTypeConverter.FloatToMeshByteArr(fCharPKvalue, stream, (byte)(offset + 3));
                DataTypeConverter.FloatToMeshByteArr(fCharPKCvalue, stream, (byte)(offset + 7));
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
                    throw new Exception("tTriggerMeasDef.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tTriggerMeasDef.Unserialize take invalid Index");

                MeasDefType = (enMeasDefType)stream[offset];
                bEnable = (stream[offset + 1] == 0x00) ? false : true;
                u8Flag = stream[offset + 2];
                fCharPKvalue = DataTypeConverter.MeshByteArrToFloat(stream, offset + 3);
                fCharPKCvalue = DataTypeConverter.MeshByteArrToFloat(stream, offset + 7);              
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    /*
    public class tAppHeader
    {     
        public tMAC mac = new tMAC();

        public byte u8WSID = 2;
        public byte u8SSNID = 0;
        public UInt16 u16TtlFrmNum = 1;
        public UInt16 u16CurFrmNum = 0;

        public byte u8MainCMD = 0;
        public byte u8SubCMD = 0;
        public bool isRequest = false;
        public byte u8ParamLen = 0;

        /// <summary>
        /// 应用层协议头长度
        /// </summary>
        protected int Len
        {
            get { return 10; }
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

                stream[offset + 0] = u8WSID;
                stream[offset + 1] = u8SSNID;
                DataTypeConverter.UInt16ToMeshByteArr(u16TtlFrmNum, stream, offset + 2);
                DataTypeConverter.UInt16ToMeshByteArr(u16CurFrmNum, stream, offset + 4);
                stream[offset + 6] = u8MainCMD;
                stream[offset + 7] = u8SubCMD;
                stream[offset + 8] = (byte)(isRequest ? 0x01 : 0x00);
                stream[offset + 9] = u8ParamLen;
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

                u8WSID = stream[offset];
                u8SSNID = stream[offset + 1];
                u16TtlFrmNum = DataTypeConverter.MeshByteArrToUInt16(stream, offset + 2);
                u16CurFrmNum = DataTypeConverter.MeshByteArrToUInt16(stream, offset + 4);

                u8MainCMD = stream[offset + 6];
                u8SubCMD = stream[offset + 7];
                isRequest = (stream[offset + 8] == 0x00) ? false : true;
                u8ParamLen = stream[offset + 9];
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }       
    }
    */
    #region WS上传的请求参数定义
    /// <summary>
    /// 自报告参数
    /// </summary>
    public class tSelfReportParam : tAppHeader
    {
        // WS运行状态
        public enWsRunState State { get; set; }
        // WS固件版本号
        public tVer Version { get; set; }
        // WS运行错误码
        public UInt16 u16ErrCode { get; set; }
        // WS测量温度
        public float f32Temperature { get; set; }
        // WS测量电压
        public float f32Voltage { get; set; }
        // WS唤醒方式
        public enWsWkupMode WakeupMode { get; set; }
        // WS重启原因
        public enMoteBoot MoteBoot { get; set; }
        // 温度采集间隔时间
        public byte u8TempDaqPeriod { get; set; }
        // 特征值采集计数
        public UInt16 u16CharCnt { get; set; }
        // 波形采集计数
        public UInt16 u16WaveCnt { get; set; }
        // Rtc时间
        public tRtcTime RTCTime { get; set; }

        public tSelfReportParam()
        {
            Version = new tVer();
            RTCTime = new tRtcTime();
        }

        public new int Len
        {
            get { return (base.Len + 25); }
            set { ; }
        }

        private const byte STATE_OFFSET = 0;
        private const byte VERTION_OFFSET = STATE_OFFSET + 1;
        private const byte EC_OFFSET = VERTION_OFFSET + 4;
        private const byte TEMP_OFFSET = EC_OFFSET + 2;
        private const byte VOL_OFFSET = TEMP_OFFSET + 4;
        private const byte WKUPMODE_OFFSET = VOL_OFFSET + 4;
        private const byte MOTEBOOT_OFFSET = WKUPMODE_OFFSET + 1;
        private const byte TMPDAQPERIOD_OFFSET = MOTEBOOT_OFFSET + 1;
        private const byte CHARCNT_OFFSET = TMPDAQPERIOD_OFFSET + 1;
        private const byte WAVECNT_OFFSET = CHARCNT_OFFSET + 2;
        private const byte RTCTIME_OFFSET = WAVECNT_OFFSET + 2;
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSelfReportParam.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSelfReportParam.Unserialize take invalid Index");

                base.Unserialize(stream, offset);

                State = (enWsRunState)stream[offset + base.Len + STATE_OFFSET];
                Version.u8Main = stream[offset + base.Len + VERTION_OFFSET];
                Version.u8Sub = stream[offset + base.Len + VERTION_OFFSET + 1];
                Version.u8Rev = stream[offset + base.Len + VERTION_OFFSET + 2];
                Version.u8Build = stream[offset + base.Len + VERTION_OFFSET + 3];
                u16ErrCode = DataTypeConverter.MeshByteArrToUInt16(stream, (offset + base.Len + EC_OFFSET));
                f32Temperature = DataTypeConverter.MeshByteArrToFloat(stream, (offset + base.Len + TEMP_OFFSET));
                f32Voltage = DataTypeConverter.MeshByteArrToFloat(stream, (offset + base.Len + VOL_OFFSET));
                WakeupMode = (enWsWkupMode)stream[offset + base.Len + WKUPMODE_OFFSET];
                MoteBoot = (enMoteBoot)stream[offset + base.Len + MOTEBOOT_OFFSET];
                u8TempDaqPeriod = stream[offset + base.Len + TMPDAQPERIOD_OFFSET];
                u16CharCnt = DataTypeConverter.MeshByteArrToUInt16(stream, (offset + base.Len + CHARCNT_OFFSET));
                u16WaveCnt = DataTypeConverter.MeshByteArrToUInt16(stream, (offset + base.Len + WAVECNT_OFFSET));
                RTCTime.u8Hour = stream[offset + base.Len + RTCTIME_OFFSET];
                RTCTime.u8Min = stream[offset + base.Len + RTCTIME_OFFSET + 1];
                RTCTime.u8Sec = stream[offset + base.Len + RTCTIME_OFFSET + 2];
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    /// <summary>
    /// 波形描述信息
    /// </summary>
    public class tWaveDescParam : tAppHeader
    {
        // 波形采集时间
        public tDateTime DaqTime { get; set; }
        // 采集方式
        public enWsDaqMode DaqMode { get; set; }
        // 波形测量定义
        public tMeasDef MeasDef { get; set; }
        // 幅值缩放因子
        public float f32AmpScaler = 0;
        // 波形有效值
        public float f32RMS = 0;
        // 波形峰值
        public float f32PK = 0;
        // 波形峰峰值
        public float f32PKPK = 0;
        // 波形地毯值
        public float f32PKC = 0;
        // 波形数据总帧数
        public UInt16 u16TotalFramesNum = 0;
        // 波形数据当前帧数
        public UInt16 u16CurrentFrameID = 0;

        public tWaveDescParam()
        {
            DaqTime = new tDateTime();
            MeasDef = new tMeasDef();
        }

        public new int Len
        {
            get { return (12 + 40); }
            set { ; }
        }
        /*
        private const byte DAQTIME_OFFSET = 0;
        private const byte DAQMODE_OFFSET = DAQTIME_OFFSET + 6;
        private const byte MEASDEF_OFFSET = DAQMODE_OFFSET + 1;
        private const byte AMPSCALER_OFFSET = MEASDEF_OFFSET + 9;
        private const byte RMS_OFFSET = AMPSCALER_OFFSET + 4;
        private const byte PK_OFFSET = RMS_OFFSET + 4;
        private const byte PKPK_OFFSET = PK_OFFSET + 4;
        private const byte PKC_OFFSET = PKPK_OFFSET + 4;
        private const byte TTLFRMNUM_OFFSET = PKC_OFFSET + 4;
        private const byte CURFRMNUM_OFFSET = PKC_OFFSET + 2;
        

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tWaveDescParam.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tWaveDescParam.Unserialize take invalid Index");

                base.Unserialize(stream, offset);

                DaqTime.u8Year = stream[offset + base.Len + DAQTIME_OFFSET];
                DaqTime.u8Month = stream[offset + base.Len + DAQTIME_OFFSET + 1];
                DaqTime.u8Day = stream[offset + base.Len + DAQTIME_OFFSET + 2];
                DaqTime.u8Hour = stream[offset + base.Len + DAQTIME_OFFSET + 3];
                DaqTime.u8Min = stream[offset + base.Len + DAQTIME_OFFSET + 4];
                DaqTime.u8Sec = stream[offset + base.Len + DAQTIME_OFFSET + 5];
                DaqMode = (enWsDaqMode)stream[offset + base.Len + DAQMODE_OFFSET];
                MeasDef.Unserialize(stream, offset + base.Len + MEASDEF_OFFSET);
                f32AmpScaler = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + AMPSCALER_OFFSET);
                f32RMS = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + RMS_OFFSET);
                f32PK = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + PK_OFFSET);
                f32PKPK = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + PKPK_OFFSET);
                f32PKC = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + PKC_OFFSET);
                u16TotalFramesNum = DataTypeConverter.MeshByteArrToUInt16(stream, offset + base.Len + TTLFRMNUM_OFFSET);
                u16CurrentFrameID = DataTypeConverter.MeshByteArrToUInt16(stream, offset + base.Len + CURFRMNUM_OFFSET);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
            
        }
         */
    }
    /// <summary>
    /// 特征值
    /// </summary>
    /// bool类型的值当为true是为有效
    public class tEigenValueParam : tAppHeader
    {
        // 采集时间
        public tDateTime SampleTime { get; set; }
        // 采集方式
        public enWsDaqMode DaqMode { get; set; }
        // Acc波形类型
        //private byte u8PamAccWaveType;
        public enMeasDefType AccWaveType { get; set; }
        //bAccRMSValid:特征值是否有效，0为无效，非0为有效
        public int bAccRMSValid = 0;
        //f32AccRMS：特征值参数
        public float f32AccRMS = 0;
        public int bAccPKValid = 0;
        public float f32AccPK = 0;
        public int bAccPKPKValid = 0;
        public float f32AccPKPK = 0;
        // Vel波形类型
       // private byte u8PamVelWaveType;
        public enMeasDefType VelWaveType { get; set; }
        public int bVelRMSValid = 0;
        public float f32VelRMS = 0;
        public int bVelPKValid = 0;
        public float f32VelPK = 0;
        public int bVelPKPKValid = 0;
        public float f32VelPKPK = 0;
        // Dsp波形类型
        //private byte u8PamDspWaveType;
        public enMeasDefType DspWaveType { get; set; }
        public int bDspRMSValid = 0;
        public float f32DspRMS = 0;
        public int bDspPKValid = 0;
        public float f32DspPK = 0;
        public int bDspPKPKValid = 0;
        public float f32DspPKPK = 0;
        // AccEnv波形类型
        //private byte u8PamAccEnvWaveType;
        public enMeasDefType AccEnvWaveType = 0;
        public int bAccEnvPKValid = 0;
        public float f32AccEnvPK = 0;
        public int bAccEnvPKCValid = 0;
        public float f32AccEnvPKC = 0;

        public tEigenValueParam()
        {
            SampleTime = new tDateTime();
        }
        /*
        public new int Len
        {
            get { return (base.Len + 55); }
            set { ; }
        }
        
        private const byte SMPTIME_OFFSET = 0;
        private const byte DAQMODE_OFFSET = SMPTIME_OFFSET + 6;
        private const byte ACCWAVETYPE_OFFSET = DAQMODE_OFFSET + 1;
        private const byte ACCRMS_OFFSET = ACCWAVETYPE_OFFSET + 1;
        private const byte ACCPK_OFFSET = ACCRMS_OFFSET + 4;
        private const byte ACCPKPK_OFFSET = ACCPK_OFFSET + 4;
        private const byte VELWAVETYPE_OFFSET = ACCPKPK_OFFSET + 4;
        private const byte VELRMS_OFFSET = VELWAVETYPE_OFFSET + 1;
        private const byte VELPK_OFFSET = VELRMS_OFFSET + 4;
        private const byte VELPKPK_OFFSET = VELPK_OFFSET + 4;
        private const byte DSPWAVETYPE_OFFSET = VELPKPK_OFFSET + 4;
        private const byte DSPRMS_OFFSET = DSPWAVETYPE_OFFSET + 1;
        private const byte DSPPK_OFFSET = DSPRMS_OFFSET + 4;
        private const byte DSPPKPK_OFFSET = DSPPK_OFFSET + 4;
        private const byte ACCENVWAVETYPE_OFFSET = DSPPKPK_OFFSET + 4;
        private const byte ACCENVPK_OFFSET = ACCENVWAVETYPE_OFFSET + 1;
        private const byte ACCENVPKC_OFFSET = ACCENVPK_OFFSET + 4;

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tEigenValueParam.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tEigenValueParam.Unserialize take invalid Index");

                base.Unserialize(stream, offset);

                SampleTime.u8Year = stream[offset + base.Len + SMPTIME_OFFSET];
                SampleTime.u8Month = stream[offset + base.Len + SMPTIME_OFFSET + 1];
                SampleTime.u8Day = stream[offset + base.Len + SMPTIME_OFFSET + 2];
                SampleTime.u8Hour = stream[offset + base.Len + SMPTIME_OFFSET + 3];
                SampleTime.u8Min = stream[offset + base.Len + SMPTIME_OFFSET + 4];
                SampleTime.u8Sec = stream[offset + base.Len + SMPTIME_OFFSET + 5];

                DaqMode = (enWsDaqMode)stream[offset + base.Len + DAQMODE_OFFSET];

                u8PamAccWaveType = stream[offset + base.Len + ACCWAVETYPE_OFFSET];
                AccWaveType = (enMeasDefType)(u8PamAccWaveType & 0x0F);
                bAccRMSValid = ((int)u8PamAccWaveType >> 4) & (int)enEigenValueType.eRMS;
                f32AccRMS = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + ACCRMS_OFFSET);
                bAccPKValid = ((int)u8PamAccWaveType >> 4) & (int)enEigenValueType.ePK;
                f32AccPK = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + ACCPK_OFFSET);
                bAccPKPKValid = ((int)u8PamAccWaveType >> 4) & (int)enEigenValueType.ePKPK;
                f32AccPKPK = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + ACCPKPK_OFFSET);

                u8PamVelWaveType = stream[offset + base.Len + VELWAVETYPE_OFFSET];
                VelWaveType = (enMeasDefType)(u8PamVelWaveType & 0x0F);
                bVelRMSValid = ((int)u8PamVelWaveType >> 4) & (int)enEigenValueType.eRMS;
                f32VelRMS = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + VELRMS_OFFSET);
                bVelPKValid = ((int)u8PamVelWaveType >> 4) & (int)enEigenValueType.ePK;
                f32VelPK = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + VELPK_OFFSET);
                bVelPKPKValid = ((int)u8PamVelWaveType >> 4) & (int)enEigenValueType.ePKPK;
                f32VelPKPK = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + VELPKPK_OFFSET);

                u8PamDspWaveType = stream[offset + base.Len + DSPWAVETYPE_OFFSET];
                DspWaveType = (enMeasDefType)(u8PamDspWaveType & 0x0F);
                bDspRMSValid = ((int)u8PamDspWaveType >> 4) & (int)enEigenValueType.eRMS;
                f32DspRMS = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + DSPRMS_OFFSET);
                bDspPKValid = ((int)u8PamDspWaveType >> 4) & (int)enEigenValueType.ePK;
                f32DspPK = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + DSPPK_OFFSET);
                bDspPKPKValid = ((int)u8PamDspWaveType >> 4) & (int)enEigenValueType.ePKPK;
                f32DspPKPK = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + DSPPKPK_OFFSET);

                u8PamAccEnvWaveType = stream[offset + base.Len + ACCENVWAVETYPE_OFFSET];
                AccEnvWaveType = (enMeasDefType)(u8PamAccEnvWaveType & 0x0F);
                bAccEnvPKValid = ((int)u8PamAccEnvWaveType >> 4) & (int)enEigenValueType.ePK;
                f32AccEnvPK = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + ACCENVPK_OFFSET);
                bAccEnvPKCValid = ((int)u8PamAccEnvWaveType >> 4) & (int)enEigenValueType.ePKC;
                f32AccEnvPKC = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + ACCENVPKC_OFFSET);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
        */
    }
    /// <summary>
    /// 温度电压
    /// </summary>
    public class tTmpVoltageParam : tAppHeader
    {
        // 温度值
        public float f32Temperature { get; set; }
        // 电池电压
        public float f32Voltage { get; set; }
        // 保留字段
        public float f32Reserved { get; set; }
        // 新增加采集时间字段，放置于最后保证新的Agent可与以前的WS版本兼容
        public tDateTime SampleTime { get; set; }

        public tTmpVoltageParam()
        {
            SampleTime = new tDateTime();
        }

        public new int Len
        {
            get { return (base.Len + 12); }// 此处长度少加6保证与老版本WS兼容 
            set { ; }
        }

        private const byte TMP_OFFSET = 0;
        private const byte VOL_OFFSET = TMP_OFFSET + 4;
        private const byte REV_OFFSET = VOL_OFFSET + 4;
        private const byte TIM_OFFSET = REV_OFFSET + 4;

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tTmpVoltageParam.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tTmpVoltageParam.Unserialize take invalid Index");

                base.Unserialize(stream, offset);

                f32Temperature = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + TMP_OFFSET);
                f32Voltage = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + VOL_OFFSET);
                f32Reserved = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + REV_OFFSET);
                // 以下判断是为了与老版本WS兼容，老板WS固件中无采集时间字段
                if (stream.Length >= (offset + Len + 6))
                {
                    SampleTime.u8Year = stream[offset + base.Len + TIM_OFFSET];
                    SampleTime.u8Month = stream[offset + base.Len + TIM_OFFSET + 1];
                    SampleTime.u8Day = stream[offset + base.Len + TIM_OFFSET + 2];
                    SampleTime.u8Hour = stream[offset + base.Len + TIM_OFFSET + 3];
                    SampleTime.u8Min = stream[offset + base.Len + TIM_OFFSET + 4];
                    SampleTime.u8Sec = stream[offset + base.Len + TIM_OFFSET + 5];
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    /// <summary>
    /// 启停机
    /// </summary>
    public class tRevStopParam : tAppHeader
    {
        // 采集时间
        public tDateTime SampleTime { get; set; }
        // 采集方式
        public enWsDaqMode DaqMode { get; set; }
        // 波形类型
        public enMeasDefType WaveType { get; set; }
        // 特征值类型
        public enEigenValueType EigenType { get; set; }

        public float f32EigenRMS { get; set; }
        public float f32EigenPK { get; set; }
        public float f32EigenPKPK { get; set; }
        public float f32EigenPKC { get; set; }

        public tRevStopParam()
        {
            SampleTime = new tDateTime();
        }

        public new int Len
        {
            get { return (base.Len + 21); }
            set { ; }
        }
		/*
        private const byte SMPTIME_OFFSET = 0;
        private const byte DAQMODE_OFFSET = SMPTIME_OFFSET + 6;
        private const byte ACCWAVETYPE_OFFSET = DAQMODE_OFFSET + 1;
        private const byte CHARTYPE_OFFSET = ACCWAVETYPE_OFFSET + 1;
        private const byte EIGENRMS_OFFSET = CHARTYPE_OFFSET + 1;
        private const byte EIGENPK_OFFSET = EIGENRMS_OFFSET + 4;
        private const byte EIGENPKPK_OFFSET = EIGENPK_OFFSET + 4;
        private const byte EIGENPKC_OFFSET = EIGENPKPK_OFFSET + 4;

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tRevStopParam.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tRevStopParam.Unserialize take invalid Index");

                base.Unserialize(stream, offset);

                SampleTime.u8Year = stream[offset + base.Len + SMPTIME_OFFSET];
                SampleTime.u8Month = stream[offset + base.Len + SMPTIME_OFFSET + 1];
                SampleTime.u8Day = stream[offset + base.Len + SMPTIME_OFFSET + 2];
                SampleTime.u8Hour = stream[offset + base.Len + SMPTIME_OFFSET + 3];
                SampleTime.u8Min = stream[offset + base.Len + SMPTIME_OFFSET + 4];
                SampleTime.u8Sec = stream[offset + base.Len + SMPTIME_OFFSET + 5];

                DaqMode = (enWsDaqMode)stream[offset + base.Len + DAQMODE_OFFSET];
                WaveType = (enMeasDefType)stream[offset + base.Len + ACCWAVETYPE_OFFSET];
                EigenType = (SsWvEvDef)stream[offset + base.Len + CHARTYPE_OFFSET];
                f32EigenRMS = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + EIGENRMS_OFFSET);
                f32EigenPK = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + EIGENPK_OFFSET);
                f32EigenPKPK = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + EIGENPKPK_OFFSET);
                f32EigenPKC = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + EIGENPKC_OFFSET);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
		*/
    }
    /// <summary>
    /// LQ值报告
    /// </summary>
    public class tLQParam : tAppHeader
    {
        // 采集时间
        public tDateTime SampleTime { get; set; }
        // 采集方式
        public enWsDaqMode DaqMode { get; set; }
        // 波形类型
        public enMeasDefType WaveType { get; set; }
        // 特征值类型
        public enEigenValueType EigenType { get; set; }
        public float EigenValuePara { get; set; }

        public tLQParam()
        {
            SampleTime = new tDateTime();
        }

        public new int Len
        {
            get { return (base.Len + 21); }
            set { ; }
        }
		/*
        private const byte SMPTIME_OFFSET = 0;
        private const byte DAQMODE_OFFSET = SMPTIME_OFFSET + 6;
        private const byte ACCWAVETYPE_OFFSET = DAQMODE_OFFSET + 1;
        private const byte CHARTYPE_OFFSET = ACCWAVETYPE_OFFSET + 1;
        private const byte CHARPARA_OFFSET = CHARTYPE_OFFSET + 1;

        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tLQParam.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tLQParam.Unserialize take invalid Index");

                base.Unserialize(stream, offset);

                SampleTime.u8Year = stream[offset + base.Len + SMPTIME_OFFSET];
                SampleTime.u8Month = stream[offset + base.Len + SMPTIME_OFFSET + 1];
                SampleTime.u8Day = stream[offset + base.Len + SMPTIME_OFFSET + 2];
                SampleTime.u8Hour = stream[offset + base.Len + SMPTIME_OFFSET + 3];
                SampleTime.u8Min = stream[offset + base.Len + SMPTIME_OFFSET + 4];
                SampleTime.u8Sec = stream[offset + base.Len + SMPTIME_OFFSET + 5];

                DaqMode = (enWsDaqMode)stream[offset + base.Len + DAQMODE_OFFSET];
                WaveType = (enMeasDefType)stream[offset + base.Len + ACCWAVETYPE_OFFSET];
                EigenType = (LqWvEvDef)stream[offset + base.Len + CHARTYPE_OFFSET];
                EigenValuePara = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + CHARPARA_OFFSET);

            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
		*/
    }

    #endregion 网络上传的请求参数定义

    #region 服务器下发请求参数定义

    #region 设置类命令参数  
    public class tSetWsIdParam : tAppHeader
    {
        public byte u8ID { get; set; }
        public UInt32 u32Reserved1 { get; set; }
        public UInt32 u32Reserved2 { get; set; }

        public tSetWsIdParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eSet;
            base.u8SubCMD = (byte)enAppSetSubCMD.eWSID;
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
                    throw new Exception("tSetWsIdParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetWsIdParam.Serialize take invalid Index");

                base.Serialize(stream, offset);

                stream[offset + base.Len] = u8ID;
                DataTypeConverter.UInt32ToMeshByteArr(u32Reserved1, stream, offset + base.Len + 1);
                DataTypeConverter.UInt32ToMeshByteArr(u32Reserved2, stream, offset + base.Len + 5);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tSetMeasDefParam : tAppHeader
    {
        // 采集方式
        public enWsDaqMode DaqMode { get; set; }
        // 定时温度采集周期时&分
        public tTimingTime TmpDaqPeriod { get; set; }
        // 特征值采集周期（该值为温度采集周期的整倍数值）
        public UInt16 u16EigenDaqMult { get; set; }
        // 波形采集周期（该值为温度采集周期的整倍数值）
        public UInt16 u16WaveDaqMult { get; set; }
        // 加速度波形测量定义
        public tMeasDef AccMdf { get; set; }
        // 加速度包络波形测量定义
        public tMeasDef AccEnvMdf { get; set; }
        // 速度波形测量定义
        public tMeasDef VelMdf { get; set; }
        // 位移波形测量定义
        public tMeasDef DspMdf { get; set; }
        // LQ波形测量定义
        public tMeasDef LQMdf { get; set; }
        // 启停机测量定义
        public tMeasDef RevStop { get; set; }

        public tSetMeasDefParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eSet;
            base.u8SubCMD = (byte)enAppSetSubCMD.eMeasDef;
            base.isRequest = true;
            base.u8ParamLen = 61;

            TmpDaqPeriod = new tTimingTime();
            AccMdf = new tMeasDef();
            AccEnvMdf = new tMeasDef();
            VelMdf = new tMeasDef();
            DspMdf = new tMeasDef();
            LQMdf = new tMeasDef();
            RevStop = new tMeasDef();
        }
    }      
    public class tSetWsSnParam : tAppHeader
    {
        private byte[] _sn = null;
        public byte[] sn
        {
            get { return _sn; }
            set
            {
                if (value == null || value.Length == 0)
                    throw new Exception("Set sn is null or length is 0");

                base.u8ParamLen = (byte)value.Length;
                _sn = value;
            }
        }

        public tSetWsSnParam()
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
                Array.Copy(sn, 0, stream, offset + base.Len, sn.Length);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tCaliSensorParam : tAppHeader
    {
        public tCaliSensorParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eSet;
            base.u8SubCMD = (byte)enAppSetSubCMD.eCaliCoeff;
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
                    throw new Exception("tCaliSensorParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tCaliSensorParam.Serialize take invalid Index");

                base.Serialize(stream, offset);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tSetTrigParam : tAppHeader
    {
        // 触发是否开启
        public enEnableFun Enable { get; set; }
        // 加速度波形测量定义
        public tTriggerDef AccMdf { get; set; }
        // 加速度包络波形测量定义
        public tTriggerVelDef AccEnvMdf { get; set; }
        // 速度波形测量定义
        public tTriggerDef VelMdf { get; set; }
        // 位移波形测量定义
        public tTriggerDef DspMdf { get; set; }

        public tSetTrigParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eSet;
            base.u8SubCMD = (byte)enAppSetSubCMD.eTrigParam;
            base.isRequest = true;
            base.u8ParamLen = 61;

            AccMdf = new tTriggerDef();
            AccEnvMdf = new tTriggerVelDef();
            VelMdf = new tTriggerDef();
            DspMdf = new tTriggerDef();
        }

        public new int Len
        {
            get { return base.u8ParamLen + 7; }
            set { ; }
        }
        /*
        public new void Serialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetTriggerUploadParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetTriggerUploadParam.Serialize take invalid Index");

                base.Serialize(stream, offset);

                stream[offset + base.Len] = (byte)Enable;
                AccMdf.Serialize(stream, offset + base.Len + 1);
                AccEnvMdf.Serialize(stream, offset + base.Len + 16);
                VelMdf.Serialize(stream, offset + base.Len + 31);
                DspMdf.Serialize(stream, offset + base.Len + 46);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
        */
    }
    #endregion

    #region 恢复类命令参数
    public class tRestoreWSParam : tAppHeader
    {
        public byte u8Mote { get; set; }
        public byte u8MCU { get; set; }

        public tRestoreWSParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eRestore;
            base.u8SubCMD = (byte)enAppRestoreSubCMD.eWS;
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
                    throw new Exception("tRestoreWSParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tRestoreWSParam.Serialize take invalid Index");

                base.Serialize(stream, offset);

                stream[offset + base.Len] = u8Mote;
                stream[offset + base.Len + 1] = u8MCU;
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    } 
    public class tResetWSParam : tAppHeader
    {
        public byte u8Mote { get; set; }
        public byte u8MCU { get; set; }

        public tResetWSParam()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eReset;
            base.u8SubCMD = (byte)enAppResetSubCMD.eWS;
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
                    throw new Exception("tResetWSParam.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tResetWSParam.Serialize take invalid Index");

                base.Serialize(stream, offset);
                stream[offset + base.Len] = u8Mote;
                stream[offset + base.Len + 1] = u8MCU;
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
    public class tSetWsIdResult : tAppHeader
    {
        // 响应码
        public UInt16 u16RC { get; set; }
        // 新设置的WSid
        public byte u8NewWsId { get; set; }

        public new int Len
        {
            get { return (base.Len + 3); }
            set { ; }
        }

        private const byte RC_OFFSET = 0;
        private const byte WSID_OFFSET = RC_OFFSET + 2;
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetWsIdResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetWsIdResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
                u16RC = DataTypeConverter.MeshByteArrToUInt16(stream, offset + base.Len + RC_OFFSET);
                u8NewWsId = stream[offset + base.Len + WSID_OFFSET];
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    public class tSetNetworkIdResult : tAppHeader
    {
        // 响应码
        public UInt16 u16RC { get; set; }

        public new int Len
        {
            get { return (base.Len + 2); }
            set { ; }
        }

        private const byte RC_OFFSET = 0;
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetNetworkIdResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetNetworkIdResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
                u16RC = DataTypeConverter.MeshByteArrToUInt16(stream, offset + base.Len + RC_OFFSET);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    public class tSetMeasDefResult : tAppHeader
    {
        // 响应码
        public UInt16 u16RC { get; set; }

        public new int Len
        {
            get { return (base.Len + 2); }
            set { ; }
        }

        private const byte RC_OFFSET = 0;
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetMeasDefResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetMeasDefResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
                u16RC = DataTypeConverter.MeshByteArrToUInt16(stream, offset + base.Len + RC_OFFSET);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    public class tSetWsSnResult : tAppHeader
    {
        // 响应码
        public UInt16 u16RC { get; set; }

        public new int Len
        {
            get { return (base.Len + 2); }
            set { ; }
        }

        private const byte RC_OFFSET = 0;
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetWsSnResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetWsSnResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
                u16RC = DataTypeConverter.MeshByteArrToUInt16(stream, offset + base.Len + RC_OFFSET);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    public class tCaliSensorResult : tAppHeader
    {
        // 校准是否成功, 1:成功, 0:失败
        public UInt16 u16RC { get; set; }
        // 错误码
        public UInt16 u16EC { get; set; }
        // 增益
        public float f32Gain { get; set; }
        // 偏移
        public float f32Offset { get; set; }
        // 保留1
        public float f32Reserved1 { get; set; }
        // 保留2
        public float f32Reserved2 { get; set; }

        public new int Len
        {
            get { return (base.Len + 20); }
            set { ; }
        }

        private const byte RC_OFFSET = 0;
        private const byte EC_OFFSET = RC_OFFSET + 2;
        private const byte GAIN_OFFSET = EC_OFFSET + 2;
        private const byte OFF_OFFSET = GAIN_OFFSET + 4;
        private const byte REV1_OFFSET = OFF_OFFSET + 4;
        private const byte REV2_OFFSET = REV1_OFFSET + 4;
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tCaliSensorResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tCaliSensorResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);

                u16RC = DataTypeConverter.MeshByteArrToUInt16(stream, offset + base.Len + RC_OFFSET);
                u16EC = DataTypeConverter.MeshByteArrToUInt16(stream, offset + base.Len + EC_OFFSET);
                f32Gain = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + GAIN_OFFSET);
                f32Offset = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + OFF_OFFSET);
                f32Reserved1 = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + REV1_OFFSET);
                f32Reserved2 = DataTypeConverter.MeshByteArrToFloat(stream, offset + base.Len + REV2_OFFSET);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    public class tSetADCloseVoltResult : tAppHeader
    {
        // 响应码
        public UInt16 u16RC { get; set; }

        public new int Len
        {
            get { return (base.Len + 2); }
            set { ; }
        }

        private const byte RC_OFFSET = 0;
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetADCloseVoltResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetADCloseVoltResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
                u16RC = DataTypeConverter.MeshByteArrToUInt16(stream, offset + base.Len + RC_OFFSET);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    public class tSetWsStartOrStopResult : tAppHeader
    {
        // 响应码
        public UInt16 u16RC { get; set; }

        public new int Len
        {
            get { return (base.Len + 2); }
            set { ; }
        }

        private const byte RC_OFFSET = 0;
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetWsStartOrStopResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetWsStartOrStopResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
                u16RC = DataTypeConverter.MeshByteArrToUInt16(stream, offset + base.Len + RC_OFFSET);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    public class tSetTrigParamResult : tAppHeader
    {
        // 响应码
        public UInt16 u16RC { get; set; }

        public new int Len
        {
            get { return (base.Len + 2); }
            set { ; }
        }

        private const byte RC_OFFSET = 0;
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetTrigParamResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetTrigParamResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
                u16RC = DataTypeConverter.MeshByteArrToUInt16(stream, offset + base.Len + RC_OFFSET);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    public class tSetWsRouteModeResult : tAppHeader
    {
        // 响应码
        public UInt16 u16RC { get; set; }

        public new int Len
        {
            get { return (base.Len + 2); }
            set { ; }
        }

        private const byte RC_OFFSET = 0;
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tSetWsRouteModeResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSetWsRouteModeResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
                u16RC = DataTypeConverter.MeshByteArrToUInt16(stream, offset + base.Len + RC_OFFSET);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    #endregion

    #region 恢复类命令结果定义
    public class tRestoreWSResult : tAppHeader
    {
        public UInt16 u16RC { get; set; }

        public new int Len
        {
            get { return (base.Len + 2); }
            set { ; }
        }

        private const byte RC_OFFSET = 0;
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tRestoreWSResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tRestoreWSResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
                u16RC = DataTypeConverter.MeshByteArrToUInt16(stream, offset + base.Len + RC_OFFSET);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tRestoreWGResult : tAppHeader
    {
        public UInt16 u16RC { get; set; }

        public new int Len
        {
            get { return (base.Len + 2); }
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
                u16RC = DataTypeConverter.MeshByteArrToUInt16(stream, offset + base.Len + RC_OFFSET);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tResetWSResult : tAppHeader
    {
        public UInt16 u16RC { get; set; }

        public new int Len
        {
            get { return (base.Len + 2); }
            set { ; }
        }

        private const byte RC_OFFSET = 0;
        public new void Unserialize(byte[] stream, int offset = 0)
        {
            try
            {
                if (stream == null || stream.Length <= 0)
                    throw new Exception("tResetWSResult.Unserialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tResetWSResult.Unserialize take invalid Index");

                base.Unserialize(stream, offset);
                u16RC = DataTypeConverter.MeshByteArrToUInt16(stream, offset + base.Len + RC_OFFSET);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    public class tResetWGResult : tAppHeader
    {
        public UInt16 u16RC { get; set; }

        public new int Len
        {
            get { return (base.Len + 2); }
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
                u16RC = DataTypeConverter.MeshByteArrToUInt16(stream, offset + base.Len + RC_OFFSET);
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

    public class tSelfReportResult : tAppHeader
    {
        // 响应码
        public UInt16 u16RC { get; set; }
        // 秒
        public UInt64 u64Seconds { get; set; }
        // 微妙
        public UInt32 u32Microseconds { get; set; }

        public tSelfReportResult()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eNotify;
            base.u8SubCMD = (byte)enAppNotifySubCMD.eSelfReport;
            base.isRequest = false;
            base.u8ParamLen = 14;
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
                    throw new Exception("tSelfReportResult.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tSelfReportResult.Serialize take invalid Index");

                base.Serialize(stream, offset);

                DataTypeConverter.UInt16ToMeshByteArr(u16RC, stream, offset + base.Len);
                DataTypeConverter.UInt64ToByteArr(u64Seconds, stream, offset + base.Len + 2);
                //DataTypeConverter.UInt64ToMeshByteArr(u64Seconds, stream, offset + base.Len + 2);
                DataTypeConverter.UInt32ToMeshByteArr(u32Microseconds, stream, offset + base.Len + 10);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    public class tWaveDescResult : tAppHeader
    {
        // 响应码
        public UInt16 u16RC { get; set; }

        public tWaveDescResult()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eNotify;
            base.u8SubCMD = (byte)enAppNotifySubCMD.eWaveDesc;
            base.isRequest = false;
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
                    throw new Exception("tWaveDescResult.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tWaveDescResult.Serialize take invalid Index");

                base.Serialize(stream, offset);
                DataTypeConverter.UInt16ToMeshByteArr(u16RC, stream, offset + base.Len);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    public class tWaveDataResult : tAppHeader
    {
        // 响应码
        private UInt16[] aRC = null;
        public UInt16[] u16aRC
        {
            get { return aRC; }
            set
            {
                if (value == null || value.Length <= 0 || value.Length > 36)
                    throw new Exception("tWaveDataResult set invalid value");

                base.u8ParamLen = (byte)(2 * value.Length);
                aRC = value;
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

                u16aRC = new UInt16[value];
                base.u8ParamLen = (byte)(2 * value);
            }
        }

        public tWaveDataResult()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eNotify;
            base.u8SubCMD = (byte)enAppNotifySubCMD.eWaveData;
            base.isRequest = false;
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
                    throw new Exception("tWaveDataResult.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tWaveDataResult.Serialize take invalid Index");

                base.Serialize(stream, offset);
                for (int i = 0; i < aRC.Length; i++)
                {
                    DataTypeConverter.UInt16ToMeshByteArr(aRC[i], stream, offset + base.Len + 2 * i);
                }
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    public class tEigenValueResult : tAppHeader
    {
        // 响应码
        public UInt16 u16RC { get; set; }

        public tEigenValueResult()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eNotify;
            base.u8SubCMD = (byte)enAppNotifySubCMD.eEigenVal;
            base.isRequest = false;
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
                    throw new Exception("tEigenValueResult.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tEigenValueResult.Serialize take invalid Index");

                base.Serialize(stream, offset);
                DataTypeConverter.UInt16ToMeshByteArr(u16RC, stream, offset + base.Len);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    public class tTmpVolResult : tAppHeader
    {
        // 响应码
        public UInt16 u16RC { get; set; }

        public tTmpVolResult()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eNotify;
            base.u8SubCMD = (byte)enAppNotifySubCMD.eTmpVol;
            base.isRequest = false;
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
                    throw new Exception("tTmpVolResult.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tTmpVolResult.Serialize take invalid Index");

                base.Serialize(stream, offset);
                DataTypeConverter.UInt16ToMeshByteArr(u16RC, stream, offset + base.Len);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    public class tRevStopResult : tAppHeader
    {
        // 响应码
        public UInt16 u16RC { get; set; }

        public tRevStopResult()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eNotify;
            base.u8SubCMD = (byte)enAppNotifySubCMD.eRevStop;
            base.isRequest = false;
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
                    throw new Exception("tRevStopResult.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tRevStopResult.Serialize take invalid Index");

                base.Serialize(stream, offset);
                DataTypeConverter.UInt16ToMeshByteArr(u16RC, stream, offset + base.Len);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }

    public class tLQResult : tAppHeader
    {
        // 响应码
        public UInt16 u16RC { get; set; }

        public tLQResult()
        {
            base.u8MainCMD = (byte)enAppMainCMD.eNotify;
            base.u8SubCMD = (byte)enAppNotifySubCMD.eLQ;
            base.isRequest = false;
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
                    throw new Exception("tLQResult.Serialize take invalid parameter");
                if (stream.Length < (offset + Len))
                    throw new Exception("tLQResult.Serialize take invalid Index");

                base.Serialize(stream, offset);
                DataTypeConverter.UInt16ToMeshByteArr(u16RC, stream, offset + base.Len);
            }
            catch (Exception ex)
            {
                CommStackLog.RecordErr(enLogLayer.eAdapter, ex.Message.ToString());
            }
        }
    }
    #endregion 网络上传的请求对应应答参数定义

    
}
