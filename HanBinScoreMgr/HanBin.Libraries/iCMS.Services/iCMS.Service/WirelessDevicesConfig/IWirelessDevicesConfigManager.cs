/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Service.WirelessDevicesConfig
 *文件名：  IWirelessDevicesConfigManager
 *创建人：  张辽阔
 *创建时间：2016-10-26
 *描述：监测设备配置接口
/************************************************************************************/
using iCMS.Common.Component.Data.Request.WirelessDevicesConfig;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Response.WirelessDevicesConfig;

namespace iCMS.Service.Web.WirelessDevicesConfig
{
    #region 监测设备配置接口
    /// <summary>
    /// 创建人：张辽阔
    /// 创建时间：2016-10-26
    /// 创建记录：监测设备配置接口
    /// </summary>
    public interface IWirelessDevicesConfigManager
    {
        #region 无线网关

        #region 获取无线网关信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-26
        /// 创建记录：获取无线网关信息
        /// </summary>
        /// <param name="parameter">获取无线网关信息的请求参数</param>
        /// <returns></returns>
        BaseResponse<GetWGDataResult> GetWGData(GetWGDataParameter parameter);
        #endregion

        #region 获取无线网关信息
        /// <summary>
        /// 创建人：王龙杰
        /// 创建时间：2017-11-08
        /// 创建记录：获取无线网关下拉列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<GetWGSelectListResult> GetWGSelectList(GetWGSelectListParameter parameter);
        #endregion

        #region 添加无线网关信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-27
        /// 创建记录：添加无线网关信息
        /// </summary>
        /// <param name="parameter">添加无线网关信息的请求参数</param>
        /// <returns></returns>
        BaseResponse<bool> AddWGData(AddWGDataParameter parameter);
        #endregion

        #region 编辑无线网关信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-27
        /// 创建记录：编辑无线网关信息
        /// </summary>
        /// <param name="parameter">添加无线网关信息的请求参数</param>
        /// <returns></returns>
        BaseResponse<bool> EditWGData(EditWGDataParameter parameter);
        #endregion

        #region 批量删除无线网关信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-27
        /// 创建记录：批量删除无线网关信息
        /// </summary>
        /// <param name="parameter">批量删除无线网关信息的请求参数</param>
        /// <returns></returns>
        BaseResponse<bool> DeleteWGData(DeleteWGDataParameter parameter);
        #endregion

        #region 验证Agent是否可以访问
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-28
        /// 创建记录：验证Agent是否可以访问
        /// </summary>
        /// <param name="parameter">验证Agent是否可以访问的请求参数</param>
        /// <returns></returns>
        BaseResponse<AgentAccessResult> IsAgentAccess(AgentAccessParameter parameter);
        #endregion

        #region 获取无线网关数据
        /// <summary>
        /// 获取无线网关数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<GetWGDataByUserIDResult> GetWGDataByUserID(GetWGDataByUserIDParameter parameter);

        #endregion
        #endregion

        #region 无线传感器

        /// <summary>
        /// 创建人：王龙杰
        /// 创建时间：2017-11-10
        /// 创建记录：获取无线传感器下拉列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<GetWSSelectListResult> GetWSSelectList(GetWSSelectListParameter parameter);

        #region 获取无线传感器信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-28
        /// 创建记录：获取无线传感器信息
        /// </summary>
        /// <param name="parameter">获取无线传感器信息的请求参数</param>
        /// <returns></returns>
        BaseResponse<GetWSDataResult> GetWSData(GetWSDataParameter parameter);
        #endregion

        #region 添加无线传感器信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-28
        /// 创建记录：添加无线传感器信息
        /// </summary>
        /// <param name="parameter">添加无线传感器信息的请求参数</param>
        /// <returns></returns>
        BaseResponse<bool> AddWSData(AddWSDataParameter parameter);
        #endregion

        #region 编辑无线传感器信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-28
        /// 创建记录：编辑无线传感器信息
        /// </summary>
        /// <param name="parameter">添加无线传感器信息的请求参数</param>
        /// <returns></returns>
        BaseResponse<bool> EditWSData(EditWSDataParameter parameter);
        #endregion

        #region 批量删除无线传感器信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-28
        /// 创建记录：批量删除无线传感器信息
        /// </summary>
        /// <param name="parameter">批量删除无线传感器信息的请求参数</param>
        /// <returns></returns>
        BaseResponse<bool> DeleteWSData(DeleteWSDataParameter parameter);
        #endregion

        #region 获取某一网关下的无线传感器的状态信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-28
        /// 创建记录：获取某一网关下的无线传感器的状态信息
        /// </summary>
        /// <param name="parameter">获取某一网关下的无线传感器的状态信息的请求参数</param>
        /// <returns></returns>
        BaseResponse<WSStatusInfoResult> GetWSStatusInfoByWGNO(WSStatusInfoByWGNOParameter parameter);
        #endregion

        #region 获取多个设备下的无线传感器的状态信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-31
        /// 创建记录：获取多个设备下的无线传感器的状态信息
        /// </summary>
        /// <param name="parameter">获取多个设备下的无线传感器的状态信息的请求参数</param>
        /// <returns></returns>
        BaseResponse<WSStatusInfoResult> GetWSStatusInfoByDevice(WSStatusInfoByDeviceParameter parameter);
        #endregion

        #region 获取某一测点下的无线传感器的状态信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-31
        /// 创建记录：获取某一测点下的无线传感器的状态信息
        /// </summary>
        /// <param name="parameter">获取某一测点下的无线传感器的状态信息的请求参数</param>
        /// <returns></returns>
        BaseResponse<WSStatusInfoResult> GetWSStatusInfoByMSite(WSStatusInfoByMSiteParameter parameter);
        #endregion

        #region 获取1+个无线传感器的状态信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-31
        /// 创建记录：获取1+个无线传感器的状态信息
        /// </summary>
        /// <param name="parameter">获取1+个无线传感器的状态信息的请求参数</param>
        /// <returns></returns>
        BaseResponse<WSStatusInfoResult> GetWSStatusInfo(WSStatusInfoParameter parameter);
        #endregion

        #region 获取同一操作标识Key值下的无线传感器的状态信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-31
        /// 创建记录：获取同一操作标识Key值下的无线传感器的状态信息
        /// </summary>
        /// <param name="parameter">获取同一操作标识Key值下的无线传感器的状态信息的请求参数</param>
        /// <returns></returns>
        BaseResponse<WSStatusInfoResult> GetWSStatusInfoByKey(WSStatusInfoByKeyParameter parameter);
        #endregion

        #region 无线传感器集合数据添加接口
        /// <summary>
        /// 无线传感器集合数据添加接口
        /// </summary>
        /// <returns></returns>
        BaseResponse<AddWSListDataResult> AddWSListData(AddWSListDataParameter parameter);
        #endregion

        #region 获取网关简单信息
        BaseResponse<GetWGSimpleInfoResult> GetWGSimpleInfo(GetWGSimpleInfoParameter param);
        #endregion

        #endregion
    }
    #endregion
}