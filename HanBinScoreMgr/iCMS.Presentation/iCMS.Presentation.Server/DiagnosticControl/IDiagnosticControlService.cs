/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 * 命名空间：iCMS.Presentation.Server.DiagnostiControl
 * 文件名：  IDiagnostiControlService
 * 创建人：  王颖辉
 * 创建时间：2016-10-21
 * 描述：诊断控件服务接口
/************************************************************************************/

using System.ServiceModel;
using System.ServiceModel.Web;

using iCMS.Common.Component.Data.Request.DiagnosticControl;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Response.DiagnosticControl;
using iCMS.Common.Component.Data.Response.SystemInitSets;

namespace iCMS.Presentation.Server.DiagnosticControl
{
    #region 诊断控件
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IDiagnostiControlService”。
    [ServiceContract]
    public interface IDiagnosticControlService
    {
        #region 获取设备信息
        [WebInvoke(UriTemplate = "GetDeviceInfo", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
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
        BaseResponse<DeviceInfoResult> GetDeviceInfo(DeviceInfoParameter parameter);
        #endregion

        #region 取得时域波形数据
        [WebInvoke(UriTemplate = "GetWaveData", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
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
        /// <returns></returns>
        BaseResponse<WaveDataResult> GetWaveData(WaveDataParameter parameter);

        #endregion

        #region  取得频域波形数据

        [WebInvoke(UriTemplate = "GetSpectrumData", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
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
        BaseResponse<SpectrumDataResult> GetSpectrumData(SpectrumDataParameter parameter);


        #endregion

        #region 获取振动信号类型

        [WebInvoke(UriTemplate = "GetSignalType", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-07-30
        /// 创建记录：获取振动信号类型
        /// </summary>
        /// <param name="chartID">当前请求的控件编号</param>
        /// <returns>振动信号类型</returns>
        BaseResponse<SignalTypeResult> GetSignalType(SignalTypeParameter parameter);

        #endregion

        #region 根据振动信号类型获取特征值类型

        [WebInvoke(UriTemplate = "GetEigenvalueTypeBySignal", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 根据振动信号类型获取特征值类型
        /// </summary>
        /// <param name="signalType">振动信号类型</param>
        /// <param name="chartID">控件id</param>
        /// <returns></returns>
        BaseResponse<EigenvalueTypeResult> GetEigenvalueTypeBySignal(EigenvalueTypeBySignalParameter parameter);

        #endregion

        #region 获取趋势图数据
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-07-30
        /// 创建记录：根据振动信号类型获取对应的特征值类型
        /// </summary>
        /// <returns>趋势图数据</returns>
        [WebInvoke(UriTemplate = "GetTendencyData", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<VibratingSignalResult> GetTendencyData(TendencyDataParameter parameter);

        #endregion

        #region 测量位置的温度历史数据检索操作
        [WebInvoke(UriTemplate = "GetTemperatureTendencysData", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-08-01
        /// 创建记录：测量位置的温度历史数据检索操作
        /// </summary>
        /// <returns>测量位置的温度历史数据</returns>
        BaseResponse<WSTemperatureResult> GetTemperatureTendencysData(TemperatureTendencysDataParameter parameter);

        #endregion

        #region 测量位置对应的传感器的电池电压历史数据的检索操作
        [WebInvoke(UriTemplate = "GetBatteryVolatageTendencysData", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-08-01
        /// 创建记录：测量位置对应的传感器的电池电压历史数据的检索操作
        /// </summary>
        /// <returns>传感器的电池电压历史数据</returns>
        BaseResponse<BatteryVolatageResult> GetBatteryVolatageTendencysData(BatteryVolatageTendencysDataParameter parameter);

        #endregion

        #region 判断振动信号是否存在最新实时数据
        [WebInvoke(UriTemplate = "HasRTTrendDataForVibsignal", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-10-21
        /// 创建记录：判断振动信号是否存在最新实时数据
        /// </summary>
        /// <returns>判断是否存在最新实时数据</returns>
        BaseResponse<RTTrendDataForVibsignalResult> HasRTTrendDataForVibsignal(RTTrendDataForVibsignalParameter parameter);

        #endregion

        #region 判断温度是否存在最新实时数据
        [WebInvoke(UriTemplate = "HasRTTrendDataForTemperature", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 判断温度是否存在最新实时数据
        /// </summary>
        /// <returns></returns>
        BaseResponse<RTTrendDataForVibsignalResult> HasRTTrendDataForTemperature(RTTrendDataForTemperatureParameter parameter);

        #endregion

        #region 判断电池电压是否存在最新实时数据
        [WebInvoke(UriTemplate = "HasRTTrendDataForVoltage", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 判断电池电压是否存在最新实时数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<RTTrendDataForVibsignalResult> HasRTTrendDataForVoltage(RTTrendDataForVoltageParameter parameter);
        #endregion

        #region 查看设备诊断报告，获取列表
        [WebInvoke(UriTemplate = "GetDiagnoseReport", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 查看设备诊断报告，获取列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<GetDiagnoseReportResult> GetDiagnoseReport(GetDiagnoseReportParameter parameter);
        #endregion

        #region 查看设备诊断报告，报告详情
        [WebInvoke(UriTemplate = "GetDiagnoseReportDetail", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 查看设备诊断报告，报告详情
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<GetDiagnoseReportDetailResult> GetDiagnoseReportDetail(GetDiagnoseReportDetailParameter parameter);
        #endregion

        #region 添加设备诊断报告
        [WebInvoke(UriTemplate = "AddDiagnoseReport", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 添加设备诊断报告
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<AddDiagnoseReportResult> AddDiagnoseReport(AddDiagnoseReportParameter parameter);
        #endregion

        #region 编辑设备诊断报告
        [WebInvoke(UriTemplate = "EditDiagnoseReport", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 编辑设备诊断报告
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<EditDiagnoseReportResult> EditDiagnoseReport(EditDiagnoseReportParameter parameter);
        #endregion

        #region 删除设备诊断报告
        [WebInvoke(UriTemplate = "DeleteDiagnoseReport", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 编辑设备诊断报告
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> DeleteDiagnoseReport(DeleteDiagnoseReportParameter parameter);
        #endregion

        #region 通过用户ID获取未读设备诊断报告数量、可读全部诊断报告数量，以及未读设诊断报告列表
        [WebInvoke(UriTemplate = "GetUnreadDRptCountAndDRptByUserID", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 通过用户ID获取未读设备诊断报告数量、可读全部诊断报告数量，以及未读设诊断报告列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<GetUnreadDRptCountAndDRptByUserIDResult> GetUnreadDRptCountAndDRptByUserID(GetUnreadDRptCountAndDRptByUserIDParameter parameter);
        #endregion

        #region 确认诊断报告“已读”
        [WebInvoke(UriTemplate = "ReadDiagnoseReportComfirm", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 确认诊断报告“已读”
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> ReadDiagnoseReportComfirm(ReadDiagnoseReportComfirmParameter parameter);
        #endregion

        #region 查看设备维修日志，获取列表
        [WebInvoke(UriTemplate = "GetDeviceMaintainReport", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 查看设备维修日志，获取列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<GetMaintainReportResult> GetDeviceMaintainReport(GetDeviceMaintainReportParameter parameter);
        #endregion

        #region 查看网关维修日志，获取列表
        [WebInvoke(UriTemplate = "GetWGMaintainReport", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 查看网关维修日志，获取列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<GetMaintainReportResult> GetWGMaintainReport(GetWGMaintainReportParameter parameter);
        #endregion

        #region 查看传感器维修日志，获取列表
        [WebInvoke(UriTemplate = "GetWSMaintainReport", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 查看传感器维修日志，获取列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<GetMaintainReportResult> GetWSMaintainReport(GetWSMaintainReportParameter parameter);
        #endregion

        #region 查看维修日志，详情
        [WebInvoke(UriTemplate = "GetMaintainReportDetail", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 查看维修日志，详情
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<GetMaintainReportDetailResult> GetMaintainReportDetail(GetMaintainReportDetailParameter parameter);
        #endregion

        #region 添加设备维修日志
        [WebInvoke(UriTemplate = "AddMaintainReport", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 添加设备维修日志
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<AddMaintainReportResult> AddMaintainReport(AddMaintainReportParameter parameter);
        #endregion

        #region 编辑设备维修日志
        [WebInvoke(UriTemplate = "EditMaintainReport", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 编辑设备维修日志
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<EditMaintainReportResult> EditMaintainReport(EditMaintainReportParameter parameter);
        #endregion

        #region 删除设备维修日志
        [WebInvoke(UriTemplate = "DeleteMaintainReport", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 删除设备维修日志
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> DeleteMaintainReport(DeleteMaintainReportParameter parameter);
        #endregion

        #region 通过用户ID获取未读设备维修日志数量、可读全部维修日志数量，以及未读设维修日志列表
        [WebInvoke(UriTemplate = "GetUnreadMRptCountAndMRptByUserID", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 通过用户ID获取未读设备维修日志数量、可读全部维修日志数量，以及未读设维修日志列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<GetUnreadMRptCountAndMRptByUserIDResult> GetUnreadMRptCountAndMRptByUserID(GetUnreadMRptCountAndMRptByUserIDParameter parameter);
        #endregion

        #region 确认维修日志“已读”
        [WebInvoke(UriTemplate = "ReadMaintainReportComfirm", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 确认维修日志“已读”
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> ReadMaintainReportComfirm(ReadMaintainReportComfirmParameter parameter);
        #endregion

        #region 查看设备诊断报告及维修日志模板
        [WebInvoke(UriTemplate = "ViewReportTemplate", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 查看设备诊断报告及维修日志模板
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<ViewReportTemplateResult> ViewReportTemplate(ViewReportTemplateParameter parameter);
        #endregion

        #region 编辑设备诊断报告及维修日志模板
        [WebInvoke(UriTemplate = "EditReportTemplate", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 编辑设备诊断报告及维修日志模板
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> EditReportTemplate(EditReportTemplateParameter parameter);
        #endregion

        #region 判断诊断报告/维修日志名称是否重复
        [WebInvoke(UriTemplate = "IsReportNameExist", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 判断诊断报告/维修日志名称是否重复
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<IsRepeatResult> IsReportNameExist(IsReportNameExistParameter parameter);
        #endregion

        #region  获取瀑布图数据
        [WebInvoke(UriTemplate = "GetWaterfallData", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 获取瀑布图数据
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<GetWaterfallDataResult> GetWaterfallData(GetWaterfallDataParameter parameter);

        #endregion

        #region  通过轴承厂商、轴承型号获取轴承详细信息
        [WebInvoke(UriTemplate = "GetBearingInfoDataByBearingNumAndFactoryID", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 通过轴承厂商、轴承型号获取轴承详细信息
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<BearingInfoData> GetBearingInfoDataByBearingNumAndFactoryID(BearingInfoDataParameter parameter);

        #endregion

        #region 转速趋势图数据查询
        /// <summary>
        /// 转速趋势图数据查询
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [WebInvoke( BodyStyle = WebMessageBodyStyle.Bare, 
                    Method = "POST",
                    RequestFormat = WebMessageFormat.Json, 
                    ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetRotationTendencysDataResult> GetRotationTendencysData(GetRotationTendencysDataParameter parameter);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
                   Method = "POST",
                   RequestFormat = WebMessageFormat.Json,
                   ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<HasRTTrendDataForSpeedResult> HasRTTrendDataForSpeed(HasRTTrendDataForSpeedParameter param);
        #endregion
    }
    #endregion
}