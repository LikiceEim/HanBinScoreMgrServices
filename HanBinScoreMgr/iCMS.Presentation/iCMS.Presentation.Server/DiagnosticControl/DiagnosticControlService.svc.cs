/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 * 命名空间：iCMS.Presentation.Server.DiagnostiControl
 * 文件名：  DiagnostiControlService
 * 创建人：  王颖辉
 * 创建时间：2016-10-21
 * 描述：诊断控件服务接口
 *
 * 修改人：张辽阔
 * 修改时间：2016-11-10
 * 描述：增加错误编码
 *
 * 修改人：张辽阔
 * 修改时间：2016-12-15
 * 描述：未通过安全验证时，增加日志记录
/************************************************************************************/

using System.Threading.Tasks;
using System.ServiceModel;

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.DiagnosticControl;
using iCMS.Presentation.Common;
using iCMS.Service.DiagnosticControl;
using iCMS.Common.Component.Data.Response.DiagnosticControl;
using iCMS.Common.Component.Tool;
using iCMS.Common.Component.Data.Response.SystemInitSets;

namespace iCMS.Presentation.Server.DiagnosticControl
{
    #region 诊断控件

    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“DiagnostiControlService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 DiagnostiControlService.svc 或 DiagnostiControlService.svc.cs，然后开始调试。
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [CustomExceptionBehaviour(typeof(CustomExceptionHandler))]
    public class DiagnosticControlService : BaseService, IDiagnosticControlService
    {
        #region 变量

        public IDiagnosticControlManager diagnostiControlManager { get; private set; }

        #endregion

        #region 构造函数

        public DiagnosticControlService(IDiagnosticControlManager diagnostiControlManager)
        {
            this.diagnostiControlManager = diagnostiControlManager;
        }

        #endregion

        #region 获取设备信息

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-07-30
        /// 创建记录：获取设备信息
        /// </summary>
        /// <param name="devID">设备ID</param>
        /// <param name="msID">测量位置ID</param>
        /// <param name="samplingDate">采集时间</param>
        /// <param name="chartID">控件ID</param>
        /// <returns></returns>
        /// 修改： QXM 1.轴承信息从测点表关联查询；2.添加返回轴承厂商信息
        public BaseResponse<DeviceInfoResult> GetDeviceInfo(DeviceInfoParameter parameter)
        {
            if (ValidateData<DeviceInfoParameter>(parameter))
            {
                //return "";
                //缺少依赖注入
                return diagnostiControlManager.GetDeviceInfo(parameter);
            }
            else
            {
                BaseResponse<DeviceInfoResult> result = new BaseResponse<DeviceInfoResult>();
                result.IsSuccessful = false;
                result.Code = "000421";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 取得时域波形数据

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-08-01
        /// 创建记录：取得时域波形数据
        /// </summary>
        /// <param name="devID">设备ID</param>
        /// <param name="mSiteID">测量位置ID</param>
        /// <param name="singleType">振动信号类型</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="chartID">当前请求的控件编号</param>
        public BaseResponse<WaveDataResult> GetWaveData(WaveDataParameter parameter)
        {
            if (ValidateData<WaveDataParameter>(parameter))
            {
                return diagnostiControlManager.GetWaveData(parameter);
            }
            else
            {
                BaseResponse<WaveDataResult> result = new BaseResponse<WaveDataResult>();
                result.IsSuccessful = false;
                result.Code = "000431";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 取得频域波形数据

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-08-02
        /// 创建记录：取得频域波形数据
        /// </summary>
        /// <param name="devID">设备ID</param>
        /// <param name="mSiteID">测量位置ID</param>
        /// <param name="singleType">振动信号类型</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <param name="chartID">当前请求的控件编号</param>
        /// <returns></returns>
        public BaseResponse<SpectrumDataResult> GetSpectrumData(SpectrumDataParameter parameter)
        {
            if (ValidateData<SpectrumDataParameter>(parameter))
            {
                return diagnostiControlManager.GetSpectrumData(parameter);
            }
            else
            {
                BaseResponse<SpectrumDataResult> result = new BaseResponse<SpectrumDataResult>();
                result.IsSuccessful = false;
                result.Code = "000441";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 获取振动信号类型

        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-07-30
        /// 创建记录：获取振动信号类型
        /// </summary>
        /// <param name="chartID">当前请求的控件编号</param>
        /// <returns>振动信号类型</returns>

        public BaseResponse<SignalTypeResult> GetSignalType(SignalTypeParameter parameter)
        {
            if (ValidateData<SignalTypeParameter>(parameter))
            {
                return diagnostiControlManager.GetSignalType(parameter);
            }
            else
            {
                BaseResponse<SignalTypeResult> result = new BaseResponse<SignalTypeResult>();
                result.IsSuccessful = false;
                result.Code = "000451";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 根据振动信号类型获取特征值类型

        /// <summary>
        /// 根据振动信号类型获取特征值类型
        /// </summary>
        /// <param name="signalType">振动信号类型</param>
        /// <param name="chartID">控件id</param>
        /// <returns></returns>
        public BaseResponse<EigenvalueTypeResult> GetEigenvalueTypeBySignal(EigenvalueTypeBySignalParameter parameter)
        {
            if (ValidateData<EigenvalueTypeBySignalParameter>(parameter))
            {
                return diagnostiControlManager.GetEigenvalueTypeBySignal(parameter);
            }
            else
            {
                BaseResponse<EigenvalueTypeResult> result = new BaseResponse<EigenvalueTypeResult>();
                result.IsSuccessful = false;
                result.Code = "000461";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 获取趋势图数据

        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-07-30
        /// 创建记录：根据振动信号类型获取对应的特征值类型
        /// </summary>
        /// <returns>趋势图数据</returns>
        public BaseResponse<VibratingSignalResult> GetTendencyData(TendencyDataParameter parameter)
        {
            if (ValidateData<TendencyDataParameter>(parameter))
            {
                return diagnostiControlManager.GetTendencyData(parameter);
            }
            else
            {
                BaseResponse<VibratingSignalResult> result = new BaseResponse<VibratingSignalResult>();
                result.IsSuccessful = false;
                result.Code = "000471";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 测量位置的温度历史数据检索操作

        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-08-01
        /// 创建记录：测量位置的温度历史数据检索操作
        /// </summary>
        /// <returns>测量位置的温度历史数据</returns>
        public BaseResponse<WSTemperatureResult> GetTemperatureTendencysData(TemperatureTendencysDataParameter parameter)
        {
            if (ValidateData<TemperatureTendencysDataParameter>(parameter))
            {
                return diagnostiControlManager.GetTemperatureTendencysData(parameter);
            }
            else
            {
                BaseResponse<WSTemperatureResult> result = new BaseResponse<WSTemperatureResult>();
                result.IsSuccessful = false;
                result.Code = "000481";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 测量位置对应的传感器的电池电压历史数据的检索操作

        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-08-01
        /// 创建记录：测量位置对应的传感器的电池电压历史数据的检索操作
        /// </summary>
        /// <returns>传感器的电池电压历史数据</returns>
        public BaseResponse<BatteryVolatageResult> GetBatteryVolatageTendencysData(BatteryVolatageTendencysDataParameter parameter)
        {
            if (ValidateData<BatteryVolatageTendencysDataParameter>(parameter))
            {
                return diagnostiControlManager.GetBatteryVolatageTendencysData(parameter);
            }
            else
            {
                BaseResponse<BatteryVolatageResult> result = new BaseResponse<BatteryVolatageResult>();
                result.IsSuccessful = false;
                result.Code = "000491";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 判断振动信号是否存在最新实时数据

        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-10-21
        /// 创建记录：判断振动信号是否存在最新实时数据
        /// </summary>
        /// <returns>判断是否存在最新实时数据</returns>
        public BaseResponse<RTTrendDataForVibsignalResult> HasRTTrendDataForVibsignal(RTTrendDataForVibsignalParameter parameter)
        {
            if (ValidateData<RTTrendDataForVibsignalParameter>(parameter))
            {
                return diagnostiControlManager.HasRTTrendDataForVibsignal(parameter);
            }
            else
            {
                BaseResponse<RTTrendDataForVibsignalResult> result = new BaseResponse<RTTrendDataForVibsignalResult>();
                result.IsSuccessful = false;
                result.Code = "000501";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 判断温度是否存在最新实时数据

        /// <summary>
        /// 判断温度是否存在最新实时数据
        /// </summary>
        /// <returns></returns>
        public BaseResponse<RTTrendDataForVibsignalResult> HasRTTrendDataForTemperature(RTTrendDataForTemperatureParameter parameter)
        {
            if (ValidateData<RTTrendDataForTemperatureParameter>(parameter))
            {
                return diagnostiControlManager.HasRTTrendDataForTemperature(parameter);
            }
            else
            {
                BaseResponse<RTTrendDataForVibsignalResult> result = new BaseResponse<RTTrendDataForVibsignalResult>();
                result.IsSuccessful = false;
                result.Code = "000511";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 判断电池电压是否存在最新实时数据

        /// <summary>
        /// 判断电池电压是否存在最新实时数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<RTTrendDataForVibsignalResult> HasRTTrendDataForVoltage(RTTrendDataForVoltageParameter parameter)
        {
            if (ValidateData<RTTrendDataForVoltageParameter>(parameter))
            {
                return diagnostiControlManager.HasRTTrendDataForVoltage(parameter);
            }
            else
            {
                BaseResponse<RTTrendDataForVibsignalResult> result = new BaseResponse<RTTrendDataForVibsignalResult>();
                result.IsSuccessful = false;
                result.Code = "000521";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 查看设备诊断报告，获取列表

        /// <summary>
        /// 查看设备诊断报告，获取列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetDiagnoseReportResult> GetDiagnoseReport(GetDiagnoseReportParameter parameter)
        {
            if (ValidateData<GetDiagnoseReportParameter>(parameter))
            {
                return diagnostiControlManager.GetDiagnoseReport(parameter);
            }
            else
            {
                BaseResponse<GetDiagnoseReportResult> result = new BaseResponse<GetDiagnoseReportResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 查看设备诊断报告，报告详情

        /// <summary>
        /// 查看设备诊断报告，报告详情
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetDiagnoseReportDetailResult> GetDiagnoseReportDetail(GetDiagnoseReportDetailParameter parameter)
        {
            if (ValidateData<GetDiagnoseReportDetailResult>(parameter))
            {
                return diagnostiControlManager.GetDiagnoseReportDetail(parameter);
            }
            else
            {
                BaseResponse<GetDiagnoseReportDetailResult> result = new BaseResponse<GetDiagnoseReportDetailResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 添加设备诊断报告

        /// <summary>
        /// 添加设备诊断报告
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<AddDiagnoseReportResult> AddDiagnoseReport(AddDiagnoseReportParameter parameter)
        {
            if (ValidateData<AddDiagnoseReportParameter>(parameter))
            {
                return diagnostiControlManager.AddDiagnoseReport(parameter);
            }
            else
            {
                BaseResponse<AddDiagnoseReportResult> result = new BaseResponse<AddDiagnoseReportResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 编辑设备诊断报告

        /// <summary>
        /// 编辑设备诊断报告
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<EditDiagnoseReportResult> EditDiagnoseReport(EditDiagnoseReportParameter parameter)
        {
            if (ValidateData<EditDiagnoseReportParameter>(parameter))
            {
                return diagnostiControlManager.EditDiagnoseReport(parameter);
            }
            else
            {
                BaseResponse<EditDiagnoseReportResult> result = new BaseResponse<EditDiagnoseReportResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 判断诊断报告/维修日志名称是否重复
        /// <summary>
        /// 判断诊断报告/维修日志名称是否重复
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<IsRepeatResult> IsReportNameExist(IsReportNameExistParameter parameter)
        {
            if (ValidateData<IsReportNameExistParameter>(parameter))
            {
                return diagnostiControlManager.IsReportNameExist(parameter);
            }
            else
            {
                BaseResponse<IsRepeatResult> result = new BaseResponse<IsRepeatResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 删除设备诊断报告

        /// <summary>
        /// 删除设备诊断报告
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> DeleteDiagnoseReport(DeleteDiagnoseReportParameter parameter)
        {
            if (ValidateData<DeleteDiagnoseReportParameter>(parameter))
            {
                return diagnostiControlManager.DeleteDiagnoseReport(parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 通过用户ID获取未读设备诊断报告数量、可读全部诊断报告数量，以及未读设诊断报告列表

        /// <summary>
        /// 通过用户ID获取未读设备诊断报告数量、可读全部诊断报告数量，以及未读设诊断报告列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetUnreadDRptCountAndDRptByUserIDResult> GetUnreadDRptCountAndDRptByUserID(GetUnreadDRptCountAndDRptByUserIDParameter parameter)
        {
            if (ValidateData<GetUnreadDRptCountAndDRptByUserIDResult>(parameter))
            {
                return diagnostiControlManager.GetUnreadDRptCountAndDRptByUserID(parameter);
            }
            else
            {
                BaseResponse<GetUnreadDRptCountAndDRptByUserIDResult> result = new BaseResponse<GetUnreadDRptCountAndDRptByUserIDResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 确认诊断报告“已读”

        /// <summary>
        /// 确认诊断报告“已读”
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> ReadDiagnoseReportComfirm(ReadDiagnoseReportComfirmParameter parameter)
        {
            if (ValidateData<ReadDiagnoseReportComfirmParameter>(parameter))
            {
                return diagnostiControlManager.ReadDiagnoseReportComfirm(parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 查看设备维修日志，获取列表

        /// <summary>
        /// 查看设备维修日志，获取列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetMaintainReportResult> GetDeviceMaintainReport(GetDeviceMaintainReportParameter parameter)
        {
            if (ValidateData<GetDeviceMaintainReportParameter>(parameter))
            {
                return diagnostiControlManager.GetDeviceMaintainReport(parameter);
            }
            else
            {
                BaseResponse<GetMaintainReportResult> result = new BaseResponse<GetMaintainReportResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 查看网关维修日志，获取列表

        /// <summary>
        /// 查看网关维修日志，获取列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetMaintainReportResult> GetWGMaintainReport(GetWGMaintainReportParameter parameter)
        {
            if (ValidateData<GetWGMaintainReportParameter>(parameter))
            {
                return diagnostiControlManager.GetWGMaintainReport(parameter);
            }
            else
            {
                BaseResponse<GetMaintainReportResult> result = new BaseResponse<GetMaintainReportResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 查看传感器维修日志，获取列表

        /// <summary>
        /// 查看传感器维修日志，获取列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetMaintainReportResult> GetWSMaintainReport(GetWSMaintainReportParameter parameter)
        {
            if (ValidateData<GetWSMaintainReportParameter>(parameter))
            {
                return diagnostiControlManager.GetWSMaintainReport(parameter);
            }
            else
            {
                BaseResponse<GetMaintainReportResult> result = new BaseResponse<GetMaintainReportResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 查看维修日志，详情

        /// <summary>
        /// 查看维修日志，详情
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetMaintainReportDetailResult> GetMaintainReportDetail(GetMaintainReportDetailParameter parameter)
        {
            if (ValidateData<GetMaintainReportDetailParameter>(parameter))
            {
                return diagnostiControlManager.GetMaintainReportDetail(parameter);
            }
            else
            {
                BaseResponse<GetMaintainReportDetailResult> result = new BaseResponse<GetMaintainReportDetailResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 添加设备维修日志

        /// <summary>
        /// 添加设备维修日志
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<AddMaintainReportResult> AddMaintainReport(AddMaintainReportParameter parameter)
        {
            if (ValidateData<AddMaintainReportParameter>(parameter))
            {
                return diagnostiControlManager.AddMaintainReport(parameter);
            }
            else
            {
                BaseResponse<AddMaintainReportResult> result = new BaseResponse<AddMaintainReportResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 编辑设备维修日志

        /// <summary>
        /// 编辑设备维修日志
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<EditMaintainReportResult> EditMaintainReport(EditMaintainReportParameter parameter)
        {
            if (ValidateData<EditMaintainReportParameter>(parameter))
            {
                return diagnostiControlManager.EditMaintainReport(parameter);
            }
            else
            {
                BaseResponse<EditMaintainReportResult> result = new BaseResponse<EditMaintainReportResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 删除设备维修日志

        /// <summary>
        /// 删除设备维修日志
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> DeleteMaintainReport(DeleteMaintainReportParameter parameter)
        {
            if (ValidateData<DeleteMaintainReportParameter>(parameter))
            {
                return diagnostiControlManager.DeleteMaintainReport(parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 通过用户ID获取未读设备维修日志数量、可读全部维修日志数量，以及未读设维修日志列表

        /// <summary>
        /// 通过用户ID获取未读设备维修日志数量、可读全部维修日志数量，以及未读设维修日志列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetUnreadMRptCountAndMRptByUserIDResult> GetUnreadMRptCountAndMRptByUserID(GetUnreadMRptCountAndMRptByUserIDParameter parameter)
        {
            if (ValidateData<GetUnreadMRptCountAndMRptByUserIDParameter>(parameter))
            {
                return diagnostiControlManager.GetUnreadMRptCountAndMRptByUserID(parameter);
            }
            else
            {
                BaseResponse<GetUnreadMRptCountAndMRptByUserIDResult> result = new BaseResponse<GetUnreadMRptCountAndMRptByUserIDResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 确认维修日志“已读”

        /// <summary>
        /// 确认维修日志“已读”
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> ReadMaintainReportComfirm(ReadMaintainReportComfirmParameter parameter)
        {
            if (ValidateData<ReadMaintainReportComfirmParameter>(parameter))
            {
                return diagnostiControlManager.ReadMaintainReportComfirm(parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 查看设备诊断报告及维修日志模板

        /// <summary>
        /// 查看设备诊断报告及维修日志模板
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<ViewReportTemplateResult> ViewReportTemplate(ViewReportTemplateParameter parameter)
        {
            if (ValidateData<ViewReportTemplateParameter>(parameter))
            {
                return diagnostiControlManager.ViewReportTemplate(parameter);
            }
            else
            {
                BaseResponse<ViewReportTemplateResult> result = new BaseResponse<ViewReportTemplateResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 编辑设备诊断报告及维修日志模板

        /// <summary>
        /// 编辑设备诊断报告及维修日志模板
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> EditReportTemplate(EditReportTemplateParameter parameter)
        {
            if (ValidateData<EditReportTemplateParameter>(parameter))
            {
                return diagnostiControlManager.EditReportTemplate(parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 通过轴承厂商、轴承型号获取轴承详细信息

        /// <summary>
        /// 通过轴承厂商、轴承型号获取轴承详细信息
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<BearingInfoData> GetBearingInfoDataByBearingNumAndFactoryID(BearingInfoDataParameter parameter)
        {
            if (ValidateData<BearingInfoDataParameter>(parameter))
            {
                return diagnostiControlManager.GetBearingInfoDataByBearingNumAndFactoryID(parameter);
            }
            else
            {
                BaseResponse<BearingInfoData> result = new BaseResponse<BearingInfoData>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region  获取瀑布图数据
        /// <summary>
        /// 获取瀑布图数据
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetWaterfallDataResult> GetWaterfallData(GetWaterfallDataParameter parameter)
        {
            if (ValidateData<GetWaterfallDataParameter>(parameter))
            {
                return diagnostiControlManager.GetWaterfallData(parameter);
            }
            else
            {
                BaseResponse<GetWaterfallDataResult> result = new BaseResponse<GetWaterfallDataResult>();
                result.IsSuccessful = false;
                result.Code = "009201";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 转速趋势图数据查询
        /// <summary>
        /// 转速趋势图数据查询
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetRotationTendencysDataResult> GetRotationTendencysData(GetRotationTendencysDataParameter parameter)
        {
            if (ValidateData<GetRotationTendencysDataParameter>(parameter))
            {
                return diagnostiControlManager.GetRotationTendencysData(parameter);
            }
            else
            {
                BaseResponse<GetRotationTendencysDataResult> result = new BaseResponse<GetRotationTendencysDataResult>();
                result.IsSuccessful = false;
                result.Code = "010531";
                Task.Run(() => LogHelper.WriteLog("未通过安全验证：（" + result.Code + "：" + result.Reason + "）"));
                return result;
            }
        }

        public BaseResponse<HasRTTrendDataForSpeedResult> HasRTTrendDataForSpeed(HasRTTrendDataForSpeedParameter param)
        {
            if (ValidateData<HasRTTrendDataForSpeedParameter>(param))
            {
                return diagnostiControlManager.HasRTTrendDataForSpeed(param);
            }
            else
            {
                BaseResponse<HasRTTrendDataForSpeedResult> result = new BaseResponse<HasRTTrendDataForSpeedResult>();
                result.IsSuccessful = false;
                result.Code = "010601";
                Task.Run(() => LogHelper.WriteLog("未通过安全验证：（" + result.Code + "：" + result.Reason + "）"));
                return result;
            }
        }
        #endregion
    }
    #endregion
}