/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Presentation.Server.WirelessDevicesConfig
 *文件名：  IWirelessDevicesConfigService
 *创建人：  张辽阔
 *创建时间：2016-10-28
 *描述：监测设备配置接口
/************************************************************************************/

using System.ServiceModel;
using System.ServiceModel.Web;

using iCMS.Common.Component.Data.Request.WirelessDevicesConfig;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Response.WirelessDevicesConfig;

namespace iCMS.Presentation.Server.WirelessDevicesConfig
{
    #region 监测设备配置接口
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-28
    /// 创建记录：监测设备配置接口
    /// </summary>
    [ServiceContract]
    public interface IWirelessDevicesConfigService
    {
        #region 无线网关

        #region 获取无线网关信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：获取无线网关信息
        /// </summary>
        /// <param name="parameter">获取无线网关信息的请求参数</param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetWGDataResult> GetWGData(GetWGDataParameter parameter);
        #endregion

        #region 获取无线网关信息
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
        Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetWGSelectListResult> GetWGSelectList(GetWGSelectListParameter parameter);
        #endregion

        #region 添加无线网关信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：添加无线网关信息
        /// </summary>
        /// <param name="parameter">添加无线网关信息的请求参数</param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> AddWGData(AddWGDataParameter parameter);
        #endregion

        #region 编辑无线网关信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：编辑无线网关信息
        /// </summary>
        /// <param name="parameter">添加无线网关信息的请求参数</param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> EditWGData(EditWGDataParameter parameter);
        #endregion

        #region 批量删除无线网关信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：批量删除无线网关信息
        /// </summary>
        /// <param name="parameter">批量删除无线网关信息的请求参数</param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> DeleteWGData(DeleteWGDataParameter parameter);
        #endregion

        #region 验证Agent是否可以访问
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：验证Agent是否可以访问
        /// </summary>
        /// <param name="parameter">验证Agent是否可以访问的请求参数</param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<AgentAccessResult> IsAgentAccess(AgentAccessParameter parameter);
        #endregion

        #region 获取无线网关数据
        /// <summary>
        /// 获取无线网关数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetWGDataByUserIDResult> GetWGDataByUserID(GetWGDataByUserIDParameter parameter);


        #endregion
        #endregion

        #region 无线传感器

        #region 获取无线传感器下拉列表
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
        Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetWSSelectListResult> GetWSSelectList(GetWSSelectListParameter parameter);
        #endregion

        #region 获取无线传感器信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：获取无线传感器信息
        /// </summary>
        /// <param name="parameter">获取无线传感器信息的请求参数</param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetWSDataResult> GetWSData(GetWSDataParameter parameter);
        #endregion

        #region 添加无线传感器信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：添加无线传感器信息
        /// </summary>
        /// <param name="parameter">添加无线传感器信息的请求参数</param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> AddWSData(AddWSDataParameter parameter);
        #endregion

        #region 编辑无线传感器信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：编辑无线传感器信息
        /// </summary>
        /// <param name="parameter">添加无线传感器信息的请求参数</param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> EditWSData(EditWSDataParameter parameter);
        #endregion

        #region 批量删除无线传感器信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：批量删除无线传感器信息
        /// </summary>
        /// <param name="parameter">批量删除无线传感器信息的请求参数</param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> DeleteWSData(DeleteWSDataParameter parameter);
        #endregion

        #region 获取某一网关下的无线传感器的状态信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：获取某一网关下的无线传感器的状态信息
        /// </summary>
        /// <param name="parameter">获取某一网关下的无线传感器的状态信息的请求参数</param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<WSStatusInfoResult> GetWSStatusInfoByWGNO(WSStatusInfoByWGNOParameter parameter);
        #endregion

        #region 获取多个设备下的无线传感器的状态信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：获取多个设备下的无线传感器的状态信息
        /// </summary>
        /// <param name="parameter">获取多个设备下的无线传感器的状态信息的请求参数</param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<WSStatusInfoResult> GetWSStatusInfoByDevice(WSStatusInfoByDeviceParameter parameter);
        #endregion

        #region 获取某一测点下的无线传感器的状态信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：获取某一测点下的无线传感器的状态信息
        /// </summary>
        /// <param name="parameter">获取某一测点下的无线传感器的状态信息的请求参数</param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<WSStatusInfoResult> GetWSStatusInfoByMSite(WSStatusInfoByMSiteParameter parameter);
        #endregion

        #region 获取1+个无线传感器的状态信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：获取1+个无线传感器的状态信息
        /// </summary>
        /// <param name="parameter">获取1+个无线传感器的状态信息的请求参数</param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<WSStatusInfoResult> GetWSStatusInfo(WSStatusInfoParameter parameter);
        #endregion

        #region 获取同一操作标识Key值下的无线传感器的状态信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：获取同一操作标识Key值下的无线传感器的状态信息
        /// </summary>
        /// <param name="parameter">获取同一操作标识Key值下的无线传感器的状态信息的请求参数</param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
             Method = "POST",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<WSStatusInfoResult> GetWSStatusInfoByKey(WSStatusInfoByKeyParameter parameter);
        #endregion

        #region 无线传感器集合数据添加接口
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        /// <summary>
        /// 无线传感器集合数据添加接口
        /// </summary>
        /// <returns></returns>
        BaseResponse<AddWSListDataResult> AddWSListData(AddWSListDataParameter parameter);
        #endregion

        #region 获取网关简单信息
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetWGSimpleInfoResult> GetWGSimpleInfo(GetWGSimpleInfoParameter param);
        #endregion

        #endregion
    }
    #endregion
}