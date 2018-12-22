/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 * 命名空间：iCMS.Presentation.Server.SystemInitSets
 * 文件名：  ISystemInitSetsService
 * 创建人：  钱行慕
 * 创建时间：2016/10/28 10:10:19
 * 描述：配置设置接口
 *=====================================================================**/

using System.ServiceModel;
using System.ServiceModel.Web;

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.SystemInitSets;
using iCMS.Common.Component.Data.Response.SystemInitSets;
using iCMS.Common.Component.Data.Response.DiagnosticAnalysis;
using iCMS.Common.Component.Data.Request.SystemManager;

namespace iCMS.Presentation.Server.SystemInitSets
{
    #region 配置设置接口
    /// <summary>
    /// 配置设置接口
    /// </summary>
    [ServiceContract]
    public interface ISystemInitSetsService
    {
        #region 获取功能信息接口
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<ModuleResult> GetModuleData(ModuleDataParameter param);
        #endregion

        #region 添加模块信息接口
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> AddModuleData(AddModuleDataParameter param);
        #endregion

        #region 编辑模块信息接口
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> EditModuleData(EditModuleDataParameter param);
        #endregion

        #region 删除模块信息
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> DeleteModuleData(DeleteModuleDataParameter param);
        #endregion

        #region 获取二级及以上模型，通过父节点ID
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<ModuleResult> GetSecondLevelModuleByParentId(SecondLevelModuleByParentIdParameter param);
        #endregion

        #region 通用配置查看，通过父ID
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<ConfigResult> GetConfigByParentID(ConfigByParentIDParameter param);
        #endregion

        #region 添加配置
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> AddConfig(AddConfigParameter param);
        #endregion

        #region 编辑配置
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> EditConfig(EditConfigParameter param);
        #endregion

        #region 删除配置
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> DeleteConfig(DeleteConfigParameter param);
        #endregion

        #region 相同节点下是否有重名，通过父ID和名称
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<ExistConfigNameResult> IsExistConfigNameByNameAndParnetId(ExistConfigNameByNameAndParnetIdParameter param);
        #endregion

        #region 相同节点下是否有重名，通过id和名称
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<ExistConfigNameResult> IsExistConfigNameByIDAndName(ExistConfigNameByIDAndNameParameter param);
        #endregion

        #region 通过父Name 和 Name获取系统配置信息
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<ConfigResult> GetConfigByName(ConfigByNameParameter param);
        #endregion

        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<ConfigResult> GetConfigByID(GetConfigByIDParam param);

        #region 通用数据设置
        [WebInvoke(UriTemplate = "IsExistMonitorTree",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<ExistMonitorTreeResult> IsExistMonitorTree();

        [WebInvoke(UriTemplate = "IsExistDeviceInMonitorTree",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<ExistDeviceInMonitorTreeResult> IsExistDeviceInMonitorTree();
        [WebInvoke(UriTemplate = "GetComSetData",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<CommonDataResult> GetComSetData(GetComSetDataParameter param);
        [WebInvoke(UriTemplate = "AddComSetData",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> AddComSetData(AddComSetDataParameter param);
        [WebInvoke(UriTemplate = "EditComSetData",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> EditComSetData(EditComSetDataParameter param);
        [WebInvoke(UriTemplate = "DeleteComSetData",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> DeleteComSetData(DeleteComSetDataParameter param);
        [WebInvoke(UriTemplate = "IsExistDescribeInMonitorTree",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<IsRepeatResult> IsExistDescribeInMonitorTree(IsExistDescribeInMonitorTreeParameter Para);
        #endregion

        #region 连接状态 CRUD
        [WebInvoke(UriTemplate = "GetConnectTypeDataForCommon",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<CommonDataResult> GetConnectTypeDataForCommon(ViewConnectTypeParameter param);
        [WebInvoke(UriTemplate = "AddConnectTypeData",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> AddConnectTypeData(AddConnectTypeParameter param);
        [WebInvoke(UriTemplate = "EditConnectTypeData",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> EditConnectTypeData(EditConnectTypeParameter param);
        [WebInvoke(UriTemplate = "DeleteConnectTypeData",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> DeleteConnectTypeData(DeleteConnectTypeParameter param);
        #endregion

        #region 获取设备树和Server树
        [WebInvoke(UriTemplate = "GetAllMonitorTreeAndServerTreebyRole",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<MonitorTreeDataForNavigationResult> GetAllMonitorTreeAndServerTreebyRole(RoleForMonitorTreeAndServerTreeParameter param);
        #endregion

        #region 验证通用数据是否有相同Code
        [WebInvoke(UriTemplate = "IsExistComSetCode",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<IsRepeatResult> IsExistComSetCode(IsExistComSetCodeParameter param);
        #endregion

        #region 通过ID验证除当前外，通用数据是否有相同Code
        [WebInvoke(UriTemplate = "IsExistCodeByIDAndCode",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<IsRepeatResult> IsExistCodeByIDAndCode(IsExistComSetCodeParameter param);
        #endregion

        #region 验证通用数据是否有相同Name
        [WebInvoke(UriTemplate = "IsExistComSetName",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<IsRepeatResult> IsExistComSetName(IsExistComSetNameParameter param);
        #endregion

        #region 获通过ID验证除当前外，通用数据是否有相同Name
        [WebInvoke(UriTemplate = "IsExistNameByIDAndName",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<IsRepeatResult> IsExistNameByIDAndName(IsExistComSetNameParameter param);
        #endregion

        #region 设置通用数据显示顺序
        [WebInvoke(UriTemplate = "SetComSetOrder",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> SetComSetOrder(SetComSetOrderParameter Para);
        #endregion

        #region 获取振动信号类型和特征值类型(可用状态)
        [WebInvoke(UriTemplate = "GetComSetDataForVibAndEigen",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<ComSetDataForVibAndEigenResult> GetComSetDataForVibAndEigen();
        #endregion

        #region 通过父Code 和 Code获取系统配置信息，与通过Code获取Config合并
        [WebInvoke(UriTemplate = "GetConfigByCode",
            BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<ConfigResult> GetConfigByCode(ConfigByCodeParameter param);
        #endregion

        #region 获取通用数据振动及特征值信息

        /// <summary>
        /// 获取通用数据振动及特征值信息
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetVibAndEigenComSetInfoResult> GetVibAndEigenComSetInfo(BaseRequest parameter);
        #endregion

        #region 批量编辑
        /// <summary>
        /// 创建人：王颖辉 
        /// 创建时间:2017-10-21
        /// 创建内容:批量编辑
        /// </summary>
        /// <param name="parameter"参数</param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> EditConfigList(EditConfigListParameter parameter);

        #endregion

        #region 获取通用数据监测树类型接口
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<CommonDataResult> GetMonitorTreeTypeDataForCommon(MonitorTreeTypeParameter param);
        #endregion
    }
    #endregion
}