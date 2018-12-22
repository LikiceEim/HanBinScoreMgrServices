/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：iCMS.Presentation.Server.WirelessDevicesConfig
 *文件名：  WirelessDevicesConfigService
 *创建人：  张辽阔
 *创建时间：2016-10-28
 *描述：监测设备配置接口
 *
 *修改人：张辽阔
 *修改时间：2016-11-11
 *描述：增加错误编码
 *
 *修改人：张辽阔
 *修改时间：2016-12-15
 *描述：未通过安全验证时，增加日志记录
/************************************************************************************/

using System.Threading.Tasks;

using iCMS.Presentation.Common;
using iCMS.Common.Component.Data.Request.WirelessDevicesConfig;
using iCMS.Service.Web.WirelessDevicesConfig;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Response.WirelessDevicesConfig;
using iCMS.Common.Component.Tool;
using System.ServiceModel;

namespace iCMS.Presentation.Server.WirelessDevicesConfig
{
    #region 监测设备配置WCF服务

    #endregion

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [CustomExceptionBehaviour(typeof(CustomExceptionHandler))]
    public class WirelessDevicesConfigService : BaseService, IWirelessDevicesConfigService
    {
        #region 变量

        public IWirelessDevicesConfigManager wirelessDevicesConfigManager { get; private set; }

        #endregion

        #region 构造函数
        public WirelessDevicesConfigService(IWirelessDevicesConfigManager manager)
        {
            wirelessDevicesConfigManager = manager;
        }
        #endregion

        #region 无线网关

        #region 获取无线网关信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：获取无线网关信息
        /// </summary>
        /// <param name="parameter">获取无线网关信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<GetWGDataResult> GetWGData(GetWGDataParameter parameter)
        {
            if (this.ValidateData<GetWGDataParameter>(parameter))
            {
                return wirelessDevicesConfigManager.GetWGData(parameter);
            }
            else
            {
                BaseResponse<GetWGDataResult> result = new BaseResponse<GetWGDataResult>();
                result.IsSuccessful = false;
                result.Code = "001571";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 获取无线网关下拉列表
        /// <summary>
        /// 创建人：王龙杰
        /// 创建时间：2017-11-08
        /// 创建记录：获取无线网关下拉列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetWGSelectListResult> GetWGSelectList(GetWGSelectListParameter parameter)
        {
            if (this.ValidateData<GetWGSelectListParameter>(parameter))
            {
                return wirelessDevicesConfigManager.GetWGSelectList(parameter);
            }
            else
            {
                BaseResponse<GetWGSelectListResult> result = new BaseResponse<GetWGSelectListResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 添加无线网关信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：添加无线网关信息
        /// </summary>
        /// <param name="parameter">添加无线网关信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<bool> AddWGData(AddWGDataParameter parameter)
        {
            if (this.ValidateData<AddWGDataParameter>(parameter))
            {
                return wirelessDevicesConfigManager.AddWGData(parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001581";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 编辑无线网关信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：编辑无线网关信息
        /// </summary>
        /// <param name="parameter">添加无线网关信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<bool> EditWGData(EditWGDataParameter parameter)
        {
            if (this.ValidateData<EditWGDataParameter>(parameter))
            {
                return wirelessDevicesConfigManager.EditWGData(parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001591";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 批量删除无线网关信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：批量删除无线网关信息
        /// </summary>
        /// <param name="parameter">批量删除无线网关信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<bool> DeleteWGData(DeleteWGDataParameter parameter)
        {
            if (this.ValidateData<DeleteWGDataParameter>(parameter))
            {
                return wirelessDevicesConfigManager.DeleteWGData(parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001601";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 验证Agent是否可以访问
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：验证Agent是否可以访问
        /// </summary>
        /// <param name="parameter">验证Agent是否可以访问的请求参数</param>
        /// <returns></returns>
        public BaseResponse<AgentAccessResult> IsAgentAccess(AgentAccessParameter parameter)
        {
            if (this.ValidateData<AgentAccessParameter>(parameter))
            {
                return wirelessDevicesConfigManager.IsAgentAccess(parameter);
            }
            else
            {
                BaseResponse<AgentAccessResult> result = new BaseResponse<AgentAccessResult>();
                result.IsSuccessful = false;
                result.Code = "001611";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 获取无线网关数据
        /// <summary>
        /// 获取无线网关数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetWGDataByUserIDResult> GetWGDataByUserID(GetWGDataByUserIDParameter parameter)
        {
            if (this.ValidateData<GetWGDataByUserIDParameter>(parameter))
            {
                return wirelessDevicesConfigManager.GetWGDataByUserID(parameter);
            }
            else
            {
                BaseResponse<GetWGDataByUserIDResult> result = new BaseResponse<GetWGDataByUserIDResult>();
                result.IsSuccessful = false;
                result.Code = "010012";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #endregion

        #region 无线传感器

        #region 获取无线网关信息
        /// <summary>
        /// 创建人：王龙杰
        /// 创建时间：2017-11-10
        /// 创建记录：获取无线传感器下拉列表
        /// </summary>
        /// <param name="parameter">获取无线传感器下拉列表</param>
        /// <returns></returns>
        public BaseResponse<GetWSSelectListResult> GetWSSelectList(GetWSSelectListParameter parameter)
        {
            if (this.ValidateData<GetWSSelectListParameter>(parameter))
            {
                return wirelessDevicesConfigManager.GetWSSelectList(parameter);
            }
            else
            {
                BaseResponse<GetWSSelectListResult> result = new BaseResponse<GetWSSelectListResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 获取无线传感器信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：获取无线传感器信息
        /// </summary>
        /// <param name="parameter">获取无线传感器信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<GetWSDataResult> GetWSData(GetWSDataParameter parameter)
        {
            if (this.ValidateData<GetWSDataParameter>(parameter))
            {
                return wirelessDevicesConfigManager.GetWSData(parameter);
            }
            else
            {
                BaseResponse<GetWSDataResult> result = new BaseResponse<GetWSDataResult>();
                result.IsSuccessful = false;
                result.Code = "001621";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 添加无线传感器信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：添加无线传感器信息
        /// </summary>
        /// <param name="parameter">添加无线传感器信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<bool> AddWSData(AddWSDataParameter parameter)
        {
            if (this.ValidateData<AddWSDataParameter>(parameter))
            {
                return wirelessDevicesConfigManager.AddWSData(parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001631";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 编辑无线传感器信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：编辑无线传感器信息
        /// </summary>
        /// <param name="parameter">添加无线传感器信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<bool> EditWSData(EditWSDataParameter parameter)
        {
            if (this.ValidateData<EditWSDataParameter>(parameter))
            {
                return wirelessDevicesConfigManager.EditWSData(parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001641";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 批量删除无线传感器信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：批量删除无线传感器信息
        /// </summary>
        /// <param name="parameter">批量删除无线传感器信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<bool> DeleteWSData(DeleteWSDataParameter parameter)
        {
            if (this.ValidateData<DeleteWSDataParameter>(parameter))
            {
                return wirelessDevicesConfigManager.DeleteWSData(parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001651";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 获取某一网关下的无线传感器的状态信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：获取某一网关下的无线传感器的状态信息
        /// </summary>
        /// <param name="parameter">获取某一网关下的无线传感器的状态信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<WSStatusInfoResult> GetWSStatusInfoByWGNO(WSStatusInfoByWGNOParameter parameter)
        {
            if (this.ValidateData<WSStatusInfoByWGNOParameter>(parameter))
            {
                return wirelessDevicesConfigManager.GetWSStatusInfoByWGNO(parameter);
            }
            else
            {
                BaseResponse<WSStatusInfoResult> result = new BaseResponse<WSStatusInfoResult>();
                result.IsSuccessful = false;
                result.Code = "001661";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 获取多个设备下的无线传感器的状态信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：获取多个设备下的无线传感器的状态信息
        /// </summary>
        /// <param name="parameter">获取多个设备下的无线传感器的状态信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<WSStatusInfoResult> GetWSStatusInfoByDevice(WSStatusInfoByDeviceParameter parameter)
        {
            if (this.ValidateData<WSStatusInfoByDeviceParameter>(parameter))
            {
                return wirelessDevicesConfigManager.GetWSStatusInfoByDevice(parameter);
            }
            else
            {
                BaseResponse<WSStatusInfoResult> result = new BaseResponse<WSStatusInfoResult>();
                result.IsSuccessful = false;
                result.Code = "001671";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 获取某一测点下的无线传感器的状态信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：获取某一测点下的无线传感器的状态信息
        /// </summary>
        /// <param name="parameter">获取某一测点下的无线传感器的状态信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<WSStatusInfoResult> GetWSStatusInfoByMSite(WSStatusInfoByMSiteParameter parameter)
        {
            if (this.ValidateData<WSStatusInfoByMSiteParameter>(parameter))
            {
                return wirelessDevicesConfigManager.GetWSStatusInfoByMSite(parameter);
            }
            else
            {
                BaseResponse<WSStatusInfoResult> result = new BaseResponse<WSStatusInfoResult>();
                result.IsSuccessful = false;
                result.Code = "001681";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 获取1+个无线传感器的状态信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：获取1+个无线传感器的状态信息
        /// </summary>
        /// <param name="parameter">获取1+个无线传感器的状态信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<WSStatusInfoResult> GetWSStatusInfo(WSStatusInfoParameter parameter)
        {
            if (this.ValidateData<WSStatusInfoParameter>(parameter))
            {
                return wirelessDevicesConfigManager.GetWSStatusInfo(parameter);
            }
            else
            {
                BaseResponse<WSStatusInfoResult> result = new BaseResponse<WSStatusInfoResult>();
                result.IsSuccessful = false;
                result.Code = "001691";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 获取同一操作标识Key值下的无线传感器的状态信息
        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：获取同一操作标识Key值下的无线传感器的状态信息
        /// </summary>
        /// <param name="parameter">获取同一操作标识Key值下的无线传感器的状态信息的请求参数</param>
        /// <returns></returns>
        public BaseResponse<WSStatusInfoResult> GetWSStatusInfoByKey(WSStatusInfoByKeyParameter parameter)
        {
            if (this.ValidateData<WSStatusInfoByKeyParameter>(parameter))
            {
                return wirelessDevicesConfigManager.GetWSStatusInfoByKey(parameter);
            }
            else
            {
                BaseResponse<WSStatusInfoResult> result = new BaseResponse<WSStatusInfoResult>();
                result.IsSuccessful = false;
                result.Code = "001701";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 无线传感器集合数据添加接口
        /// <summary>
        /// 无线传感器集合数据添加接口
        /// </summary>
        /// <returns></returns>
        public BaseResponse<AddWSListDataResult> AddWSListData(AddWSListDataParameter parameter)
        {
            if (this.ValidateData<AddWSListDataParameter>(parameter))
            {
                return wirelessDevicesConfigManager.AddWSListData(parameter);
            }
            else
            {
                BaseResponse<AddWSListDataResult> result = new BaseResponse<AddWSListDataResult>();
                result.IsSuccessful = false;
                result.Code = "006871";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 获取网关简单信息
        public BaseResponse<GetWGSimpleInfoResult> GetWGSimpleInfo(GetWGSimpleInfoParameter param)
        {
            if (this.ValidateData<GetWGSimpleInfoParameter>(param))
            {
                return wirelessDevicesConfigManager.GetWGSimpleInfo(param);
            }
            else
            {
                BaseResponse<GetWGSimpleInfoResult> result = new BaseResponse<GetWGSimpleInfoResult>();
                result.IsSuccessful = false;
                result.Code = "001631";
                LogHelper.WriteLog("未通过安全验证：（" + result.Code + "：" + result.Reason + "）");
                return result;
            }
        }
        #endregion
        #endregion
    }
}