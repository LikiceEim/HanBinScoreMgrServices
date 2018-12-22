/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Presentation.Server.DiagnosticAnalysis
 *文件名：  IDiagnosticAnalysisService
 *创建人：  王颖辉
 *创建时间：2016-10-21
 *描述：诊断分析服务接口
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using iCMS.Common.Component.Data.Request.DiagnosticAnalysis;
using iCMS.Common.Component.Data.Request.DiagnosticAnalysis.MonitorTree;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Response.DiagnosticAnalysis;
using iCMS.Common.Component.Data.Response.SystemInitSets;
using iCMS.Common.Component.Data.Response;
using iCMS.Common.Component.Data.Request.DevicesConfig;
using iCMS.Common.Component.Data.Response.Common;

namespace iCMS.Presentation.Server.DiagnosticAnalysis
{
    #region 诊断分析
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IDiagnosticAnalysis”。
    [ServiceContract]
    public interface IDiagnosticAnalysisService
    {
        #region 形貌图展示
        /// <summary>
        /// 形貌图展示
        /// </summary>
        /// <returns></returns>
        [WebInvoke(UriTemplate = "GetDevImgData", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<DevImgDataResult> GetDevImgData(DevImgDataParameter parameter);

        #endregion

        #region 通过设备Id,获取形貌图配置下的所有位置信息
        /// <summary>
        /// 形貌图展示
        /// </summary>
        /// <returns></returns>
        [WebInvoke(UriTemplate = "GetConfigListByDeviceId", BodyStyle = WebMessageBodyStyle.Bare, Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-09-03
        /// 通过设备Id,获取形貌图配置下的所有位置信息
        /// </summary>
        /// <param name="deviceId">设备id</param>
        /// <returns></returns>
        BaseResponse<ConfigListByDeviceIDResult> GetConfigListByDeviceID(ConfigListByDeviceIDParameter parameter);

        #endregion

        #region 左侧导航监测树
        [WebInvoke(UriTemplate = "GetMonitorTreeDataForNavigation",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<MonitorTreeDataForNavigationResult> GetMonitorTreeDataForNavigation(GetMonitorTreeDataForNavigationParameter Parameter);
        #endregion

        #region 获取形貌图显示配置
        /// <summary>
        /// 获取形貌图显示配置 ,LF
        /// </summary>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<SystemConfigResult> GetTopographicMapSets(BaseRequest Parameter);
        #endregion

        #region 获取形貌图设备图片信息
        /// <summary>
        /// 获取形貌图设备图片信息,LF
        /// </summary>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<SystemConfigResult> GetTopographicMapPictureInfo(BaseRequest Parameter);
        #endregion

        #region 获取无线传感器数据接口
        /// <summary>
        /// 获取无线传感器数据接口
        /// </summary>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<WSInfoResult> GetWSStatusData(GetWSStatusParameter param);
        #endregion

        #region 报警提醒
        /// <summary>
        /// 报警提醒
        /// </summary>
        /// <returns></returns>
        [WebInvoke(UriTemplate = "GetDevAlarmRemindDataByUserID",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<DevAlarmRemindDataResult> GetDevAlarmRemindDataByUserID(QueryDevWarningDataParameter Parameter);
        #endregion

        #region 系统配置信息通用查询接口
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容：系统配置信息通用查询接口
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        [WebInvoke(UriTemplate = "GetConfigListForDeviceMonitorCenter",
         BodyStyle = WebMessageBodyStyle.Bare,
         Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetConfigListForDeviceMonitorCenterResult> GetConfigListForDeviceMonitorCenter(GetConfigListForDeviceMonitorCenterParameter parameter);
        #endregion

        #region 获取当前设备状态及报告统计
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容:获取当前设备状态及报告统计
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        [WebInvoke(UriTemplate = "GetDeviceTopographicMapStatistics",
         BodyStyle = WebMessageBodyStyle.Bare,
         Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetDeviceTopographicMapStatisticsResult> GetDeviceTopographicMapStatistics(GetDeviceTopographicMapStatisticsParameter parameter);
        #endregion

        #region 获取对形貌图数据展示
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容：获取对形貌图数据展示
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [WebInvoke(
           BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetWSImgDataResult> GetWSImgData(GetWSImgDataParameter parameter);

        #endregion

        #region 获取当前传感器状态及报告统计
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容：获取当前传感器状态及报告统计
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [WebInvoke(
          BodyStyle = WebMessageBodyStyle.Bare,
          Method = "POST",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetWSStatusAndReportStatisticsResult> GetWSStatusAndReportStatistics(GetWSStatusAndReportStatisticsParameter parameter);
        #endregion

        #region 通过WSID获取挂靠设备信息
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容：通过WSID获取挂靠设备信息
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        [WebInvoke(
          BodyStyle = WebMessageBodyStyle.Bare,
          Method = "POST",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetDeviceInfoByWSIDResult> GetDeviceInfoByWSID(GetDeviceInfoByWSIDParameter parameter);

        #endregion

        #region 通过设备ID获取传感器
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容：通过设备ID获取传感器
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        [WebInvoke(
        BodyStyle = WebMessageBodyStyle.Bare,
        Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetWSByDeviceIDResult> GetWSByDeviceID(GetWSByDeviceIDParameter parameter);

        #endregion

        #region 获取设备数量
        /// <summary>
        /// 获取设备数量
        /// </summary>
        /// <param name="parameter"></param>
        [WebInvoke(
        BodyStyle = WebMessageBodyStyle.Bare,
        Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<ResponseResult> GetDeviceCount(GetDeviceCountParameter parameter);

        #endregion

        #region 获取WS数量
        /// <summary>
        /// 获取WS数量
        /// </summary>
        /// <param name="parameter"></param>
        [WebInvoke(
        BodyStyle = WebMessageBodyStyle.Bare,
        Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<ResponseResult> GetWSCount(GetWSCountParameter parameter);


        #endregion
    }
    #endregion
}
