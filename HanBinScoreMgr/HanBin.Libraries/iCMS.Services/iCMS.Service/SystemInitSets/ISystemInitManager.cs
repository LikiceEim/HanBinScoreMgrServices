/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 * 命名空间：iCMS.Presentation.Server.SystemManager
 * 文件名：  SystemManagerService
 * 创建人：  QXM
 * 创建时间：2016/10/28 10:10:19
 * 描述：服务表现层，响应调用方对用户、权限组、日志的操作请求
 *=====================================================================**/

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.SystemInitSets;
using iCMS.Common.Component.Data.Request.SystemManager;
using iCMS.Common.Component.Data.Response.DiagnosticAnalysis;
using iCMS.Common.Component.Data.Response.SystemInitSets;

namespace iCMS.Service.Web.SystemInitSets
{
    #region 系统配置
    /// <summary>
    /// 系统配置
    /// </summary>
    public interface ISystemInitManager
    {
        #region 获取功能信息接口
        BaseResponse<ModuleResult> GetModuleData(ModuleDataParameter param);
        #endregion

        #region 添加模块信息接口
        BaseResponse<bool> AddModuleData(AddModuleDataParameter param);
        #endregion

        #region 编辑模块信息接口
        BaseResponse<bool> EditModuleData(EditModuleDataParameter param);
        #endregion

        #region 删除模块信息
        BaseResponse<bool> DeleteModuleData(DeleteModuleDataParameter param);
        #endregion

        #region 获取二级及以上模型，通过父节点ID
        BaseResponse<ModuleResult> GetSecondLevelModuleByParentId(SecondLevelModuleByParentIdParameter param);
        #endregion

        #region 通用配置查看，通过父ID
        BaseResponse<ConfigResult> GetConfigByParentID(ConfigByParentIDParameter param);
        #endregion

        #region Add Config
        BaseResponse<bool> AddConfig(AddConfigParameter param);
        #endregion

        #region Edit Config
        BaseResponse<bool> EditConfig(EditConfigParameter param);
        #endregion

        #region Delete Config
        BaseResponse<bool> DeleteConfig(DeleteConfigParameter param);
        #endregion

        #region 相同节点下是否有重名，通过父ID和名称
        BaseResponse<ExistConfigNameResult> IsExistConfigNameByNameAndParnetId(ExistConfigNameByNameAndParnetIdParameter param);
        #endregion

        #region 相同节点下是否有重名，通过id和名称
        BaseResponse<ExistConfigNameResult> IsExistConfigNameByIDAndName(ExistConfigNameByIDAndNameParameter param);
        #endregion

        #region 通过父Name 和 Name获取系统配置信息
        BaseResponse<ConfigResult> GetConfigByName(ConfigByNameParameter param);
        #endregion

        BaseResponse<ConfigResult> GetConfigByID(GetConfigByIDParam param);
        //#region 获取系统图片
        //BaseResponse GetSysImagesData(SysImagesParameter param);
        //#endregion

        #region 判断系统中是否已存在监测树
        /// <summary>
        /// 创建人：王龙杰
        /// 创建时间：2017-12-14
        /// 创建记录：判断系统中是否已存在监测树
        /// </summary>
        /// <returns></returns>
        BaseResponse<ExistMonitorTreeResult> IsExistMonitorTree();
        #endregion

        #region 判断监测树节点下是否存在设备
        /// <summary>
        /// 创建人：王龙杰
        /// 创建时间：2017-12-13
        /// 创建记录：判断监测树节点下是否存在设备
        /// </summary>
        /// <returns></returns>
        BaseResponse<ExistDeviceInMonitorTreeResult> IsExistDeviceInMonitorTree();
        #endregion

        #region 获取通用数据监测树类型接口
        BaseResponse<CommonDataResult> GetMonitorTreeTypeDataForCommon(GetComSetDataParameter param);
        #endregion

        #region 获取通用数据监测树类型接口
        BaseResponse<CommonDataResult> GetMonitorTreeTypeDataForCommon1(MonitorTreeTypeParameter param);
        #endregion

        #region 新增监测树类型
        BaseResponse<bool> AddMonitorTreeTypeData(AddComSetDataParameter param);
        #endregion

        #region 编辑监测树类型
        BaseResponse<bool> EditMonitorTreeTypeData(EditComSetDataParameter param);
        #endregion

        #region 删除监测树类型
        BaseResponse<bool> DeleteMonitorTreeTypeData(DeleteComSetDataParameter param);
        #endregion

        #region 监测树类型级别(Describe)是否重复

        /// <summary>
        /// 监测树类型级别(Describe)是否重复
        /// 创建人:王龙杰
        /// 创建时间：2017-11-21
        /// </summary>
        /// <param name="Para"></param>
        /// <returns></returns>
        BaseResponse<IsRepeatResult> IsExistDescribeInMonitorTree(IsExistDescribeInMonitorTreeParameter Para);

        #endregion

        #region 获取通用数据设备类型接口
        BaseResponse<CommonDataResult> GetDeviceTypeDataForCommon(GetComSetDataParameter param);
        #endregion

        #region 添加设备类型接口
        BaseResponse<bool> AddDeviceTypeData(AddComSetDataParameter param);
        #endregion

        #region 编辑设备类型
        BaseResponse<bool> EditDeviceTypeData(EditComSetDataParameter param);
        #endregion

        #region 删除设备类型
        BaseResponse<bool> DeleteDeviceTypeData(DeleteComSetDataParameter param);
        #endregion

        #region 获取通用数据测量位置类型接口
        BaseResponse<CommonDataResult> GetMSiteTypeDataForCommon(GetComSetDataParameter param);
        #endregion

        #region 新增测量位置类型
        BaseResponse<bool> AddMSiteTypeData(AddComSetDataParameter param);
        #endregion

        #region 编辑测量位置类型
        BaseResponse<bool> EditMSiteTypeData(EditComSetDataParameter param);
        #endregion

        #region 删除测量位置类型
        BaseResponse<bool> DeleteMSiteTypeData(DeleteComSetDataParameter param);
        #endregion

        #region 获取通用数据测量位置监测类型接口
        BaseResponse<CommonDataResult> GetMSiteMTTypeDataForCommon(GetComSetDataParameter param);
        #endregion

        #region 新增测量位置监测类型
        BaseResponse<bool> AddMSiteMTTypeData(AddComSetDataParameter param);
        #endregion

        #region 编辑测量位置监测类型
        BaseResponse<bool> EditMSiteMTTypeData(EditComSetDataParameter param);
        #endregion

        #region 删除测量位置监测类型
        BaseResponse<bool> DeleteMSiteMTTypeData(DeleteComSetDataParameter param);
        #endregion

        #region 获取通用数据无线传感器振动类型数据
        BaseResponse<CommonDataResult> GetVIBTypeDataForCommon(GetComSetDataParameter param);
        #endregion

        #region 添加通用数据振动类型
        BaseResponse<bool> AddVIBTypeData(AddComSetDataParameter param);
        #endregion

        #region 编辑通用数据振动类型
        BaseResponse<bool> EditVIBTypeData(EditComSetDataParameter param);
        #endregion

        #region 删除通用数据振动类型
        BaseResponse<bool> DeleteVIBTypeData(DeleteComSetDataParameter param);
        #endregion

        #region 获取通用数据特征值类型接口
        BaseResponse<CommonDataResult> GetEigenTypeDataForCommon(GetComSetDataParameter param);
        #endregion

        #region 新增特征值类型
        BaseResponse<bool> AddEigenTypeData(AddComSetDataParameter param);
        #endregion

        #region 编辑特征值类型
        BaseResponse<bool> EditEigenTypeData(EditComSetDataParameter param);
        #endregion

        #region 删除特征值类型
        BaseResponse<bool> DeleteEigenTypeData(DeleteComSetDataParameter param);
        #endregion

        #region  获取通用数据波长类型接口

        BaseResponse<CommonDataResult> GetWaveLengthTypeDataForCommon(GetComSetDataParameter param);

        #endregion

        #region 获取通用数据特征值波长类型接口

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-11
        /// 创建记录：获取通用数据特征值波长类型
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        BaseResponse<CommonDataResult> GetEigenWaveLengthTypeDataForCommon(GetComSetDataParameter param);

        #endregion

        #region  新增波长类型
        BaseResponse<bool> AddWaveLengthTypeData(AddComSetDataParameter param);
        #endregion

        #region 编辑波长类型
        BaseResponse<bool> EditWaveLengthTypeData(EditComSetDataParameter param);
        #endregion

        #region 删除波长类型
        BaseResponse<bool> DeleteWaveLengthTypeData(DeleteComSetDataParameter param);
        #endregion

        #region 获取通用数据波长上限频率类型接口
        BaseResponse<CommonDataResult> GetWaveUpperLimitTypeDataForCommon(GetComSetDataParameter param);
        #endregion

        #region 添加波长上限
        BaseResponse<bool> AddWaveUpperLimitTypeData(AddComSetDataParameter param);
        #endregion

        #region 编辑波长上限
        BaseResponse<bool> EditWaveUpperLimitTypeData(EditComSetDataParameter param);
        #endregion

        #region 删除波长上限
        BaseResponse<bool> DeleteWaveUpperLimitTypeData(DeleteComSetDataParameter param);
        #endregion

        #region 获取通用数据波长下限频率类型接口
        BaseResponse<CommonDataResult> GetWaveLowerLimitTypeDataForCommon(GetComSetDataParameter param);
        #endregion

        #region 添加波长下限频率类型
        BaseResponse<bool> AddWaveLowerLimitTypeData(AddComSetDataParameter param);
        #endregion

        #region 编辑波长下限频率类型
        BaseResponse<bool> EditWaveLowerLimitTypeData(EditComSetDataParameter param);
        #endregion

        #region 删除波长下限频率类型
        BaseResponse<bool> DeleteWaveLowerLimitTypeData(DeleteComSetDataParameter param);
        #endregion

        #region WS类型通用数据 CRUD
        BaseResponse<CommonDataResult> GetWSTypeDataForCommon(GetComSetDataParameter param);

        BaseResponse<bool> AddWSTypeData(AddComSetDataParameter param);

        BaseResponse<bool> EditWSTypeData(EditComSetDataParameter param);

        BaseResponse<bool> DeleteWSTypeData(DeleteComSetDataParameter param);
        #endregion

        #region WG类型通用数据 CRUD
        BaseResponse<CommonDataResult> GetWGTypeDataForCommon(GetComSetDataParameter param);

        BaseResponse<bool> AddWGTypeData(AddComSetDataParameter param);

        BaseResponse<bool> EditWGTypeData(EditComSetDataParameter param);

        BaseResponse<bool> DeleteWGTypeData(DeleteComSetDataParameter param);
        #endregion

        #region 连接状态 CRUD
        BaseResponse<CommonDataResult> GetConnectTypeDataForCommon(ViewConnectTypeParameter param);

        BaseResponse<bool> AddConnectTypeData(AddConnectTypeParameter param);

        BaseResponse<bool> EditConnectTypeData(EditConnectTypeParameter param);

        BaseResponse<bool> DeleteConnectTypeData(DeleteConnectTypeParameter param);
        #endregion

        #region 形貌图设置信息 LF
        BaseResponse<SystemConfigResult> GetTopographicMapSets();

        BaseResponse<SystemConfigResult> GetTopographicMapPictureInfo();
        #endregion

        #region 获取设备树和Server树
        BaseResponse<MonitorTreeDataForNavigationResult> GetAllMonitorTreeAndServerTreebyRole(RoleForMonitorTreeAndServerTreeParameter Para);

        #endregion

        #region 通用数据是否有相同Code
        BaseResponse<IsRepeatResult> IsExistComSetCode(IsExistComSetCodeParameter Para, bool isHaveID);
        #endregion

        #region 通用数据是否有相同Name
        BaseResponse<IsRepeatResult> IsExistComSetName(IsExistComSetNameParameter Para, bool isHaveID);
        #endregion

        #region 设置通用数据显示顺序
        BaseResponse<bool> SetComSetOrder(SetComSetOrderParameter Para);
        #endregion

        #region 获取振动信号类型和特征值类型(可用状态)
        BaseResponse<ComSetDataForVibAndEigenResult> GetComSetDataForVibAndEigen();
        #endregion

        #region 通过父Code 和 Code获取系统配置信息，与通过Code获取Config合并
        BaseResponse<ConfigResult> GetConfigByCode(ConfigByCodeParameter Para);
        #endregion

        #region 获取通用数据振动及特征值信息
        /// <summary>
        /// 获取通用数据振动及特征值信息
        /// </summary>
        BaseResponse<GetVibAndEigenComSetInfoResult> GetVibAndEigenComSetInfo();
        #endregion

        #region 批量编辑
        /// <summary>
        /// 创建人：王颖辉 
        /// 创建时间:2017-10-21
        /// 创建内容:批量编辑
        /// </summary>
        /// <param name="parameter"参数</param>
        /// <returns></returns>
        BaseResponse<bool> EditConfigList(EditConfigListParameter parameter);
        #endregion

        #region 获取通用数据三轴传感器振动类型数据

        /// <summary>
        /// 创建人：张辽阔 
        /// 创建时间：2018-05-11
        /// 创建内容：获取三轴传感器振动类型数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<CommonDataResult> GetTriaxialVIBTypeDataForCommon(GetComSetDataParameter parameter);

        #endregion

        #region 获取通用数据有线传感器振动类型数据

        /// <summary>
        /// 创建人：张辽阔 
        /// 创建时间：2018-05-11
        /// 创建内容：获取有线传感器振动类型数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<CommonDataResult> GetWiredVIBTypeDataForCommon(GetComSetDataParameter parameter);

        #endregion

        #region  新增特征值波长类型
        BaseResponse<bool> AddEigenValueWaveLengthTypeData(AddComSetDataParameter param);
        #endregion

        #region 添加包络滤波器上限
        BaseResponse<bool> AddEnvlFilterUpperTypeData(AddComSetDataParameter param);
        #endregion

        #region 获取包络滤波器上限
        BaseResponse<CommonDataResult> GetEnvlFilterUpperTypeDataForCommon(GetComSetDataParameter param);
        #endregion

        #region 编辑包络滤波器上限
        BaseResponse<bool> EditEnvlFilterUpperType(EditComSetDataParameter para);
        #endregion

        #region 删除包络滤波器上限
        BaseResponse<bool> DeleteEnvlFilterUpperType(DeleteComSetDataParameter para);
        #endregion

        #region 添加包络滤波器下限
        BaseResponse<bool> AddEnvlFilterLowerTypeData(AddComSetDataParameter param);
        #endregion

        #region 获取包络滤波器下限
        BaseResponse<CommonDataResult> GetEnvlFilterLowerTypeDataForCommon(GetComSetDataParameter param);

        #region 编辑包络滤波器下限
        BaseResponse<bool> EditEnvlFilterLowerType(EditComSetDataParameter para);

        #endregion

        #region 删除包络滤波器下限
        BaseResponse<bool> DeleteEnvlFilterLowerType(DeleteComSetDataParameter para);

        #endregion
        #endregion
    }
    #endregion
}