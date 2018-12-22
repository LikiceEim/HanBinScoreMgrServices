/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 * 命名空间：  iCMS.Presentation.Server.SystemInitSets
 * 文件名：    SystemInitSetsService
 * 创建人：    钱行慕
 * 创建时间：  2016/10/19 10:10:19
 * 描述：服务表现层，响应调用方对用户、权限组、日志的操作请求
 *
 * 修改人：张辽阔
 * 修改时间：2016-11-10
 * 描述：增加错误编码
 *
 * 修改人：张辽阔
 * 修改时间：2016-12-15
 * 描述：未通过安全验证时，增加日志记录
 *=====================================================================**/

using System.Threading.Tasks;
using System.ServiceModel;

using iCMS.Presentation.Common;
using iCMS.Service.Web.SystemInitSets;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Response.SystemInitSets;
using iCMS.Common.Component.Data.Request.SystemInitSets;
using iCMS.Common.Component.Tool;
using iCMS.Common.Component.Data.Response.DiagnosticAnalysis;
using iCMS.Common.Component.Data.Request.SystemManager;

namespace iCMS.Presentation.Server.SystemInitSets
{
    #region 系统配置
    /// <summary>
    /// 系统配置
    /// </summary>
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [CustomExceptionBehaviour(typeof(CustomExceptionHandler))]
    public class SystemInitSetsService : BaseService, ISystemInitSetsService
    {
        #region 变量
        private ISystemInitManager initManager = null;
        #endregion

        #region 构造函数
        public SystemInitSetsService(ISystemInitManager manager)
        {
            initManager = manager;
        }
        #endregion

        #region 获取功能信息接口

        public BaseResponse<ModuleResult> GetModuleData(ModuleDataParameter param)
        {
            if (this.ValidateData<ModuleDataParameter>(param))
            {
                return initManager.GetModuleData(param);
            }
            else
            {
                BaseResponse<ModuleResult> result = new BaseResponse<ModuleResult>();
                result.IsSuccessful = false;
                result.Code = "000591";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 添加模块信息接口

        public BaseResponse<bool> AddModuleData(AddModuleDataParameter param)
        {
            if (this.ValidateData<AddModuleDataParameter>(param))
            {
                return initManager.AddModuleData(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "000601";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 编辑模块信息接口

        public BaseResponse<bool> EditModuleData(EditModuleDataParameter param)
        {
            if (this.ValidateData<EditModuleDataParameter>(param))
            {
                return initManager.EditModuleData(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "000611";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 删除模块信息

        public BaseResponse<bool> DeleteModuleData(DeleteModuleDataParameter param)
        {
            if (this.ValidateData<DeleteModuleDataParameter>(param))
            {
                return initManager.DeleteModuleData(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "000621";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 获取二级及以上模型，通过父节点ID

        public BaseResponse<ModuleResult> GetSecondLevelModuleByParentId(SecondLevelModuleByParentIdParameter param)
        {
            if (this.ValidateData<SecondLevelModuleByParentIdParameter>(param))
            {
                return initManager.GetSecondLevelModuleByParentId(param);
            }
            else
            {
                BaseResponse<ModuleResult> result = new BaseResponse<ModuleResult>();
                result.IsSuccessful = false;
                result.Code = "000631";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 通用配置查看，通过父ID

        public BaseResponse<ConfigResult> GetConfigByParentID(ConfigByParentIDParameter param)
        {
            if (this.ValidateData<ConfigByParentIDParameter>(param))
            {
                return initManager.GetConfigByParentID(param);
            }
            else
            {
                BaseResponse<ConfigResult> result = new BaseResponse<ConfigResult>();
                result.IsSuccessful = false;
                result.Code = "000641";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region Add Config

        public BaseResponse<bool> AddConfig(AddConfigParameter param)
        {
            if (this.ValidateData<AddConfigParameter>(param))
            {
                return initManager.AddConfig(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "000651";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region Edit Config

        public BaseResponse<bool> EditConfig(EditConfigParameter param)
        {
            if (this.ValidateData<EditConfigParameter>(param))
            {
                return initManager.EditConfig(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "000661";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region Delete Config

        public BaseResponse<bool> DeleteConfig(DeleteConfigParameter param)
        {
            if (this.ValidateData<DeleteConfigParameter>(param))
            {
                return initManager.DeleteConfig(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "000671";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 相同节点下是否有重名，通过父ID和名称

        public BaseResponse<ExistConfigNameResult> IsExistConfigNameByNameAndParnetId(ExistConfigNameByNameAndParnetIdParameter param)
        {
            if (this.ValidateData<ExistConfigNameByNameAndParnetIdParameter>(param))
            {
                return initManager.IsExistConfigNameByNameAndParnetId(param);
            }
            else
            {
                BaseResponse<ExistConfigNameResult> result = new BaseResponse<ExistConfigNameResult>();
                result.IsSuccessful = false;
                result.Code = "000681";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 相同节点下是否有重名，通过id和名称

        public BaseResponse<ExistConfigNameResult> IsExistConfigNameByIDAndName(ExistConfigNameByIDAndNameParameter param)
        {
            if (this.ValidateData<ExistConfigNameByIDAndNameParameter>(param))
            {
                return initManager.IsExistConfigNameByIDAndName(param);
            }
            else
            {
                BaseResponse<ExistConfigNameResult> result = new BaseResponse<ExistConfigNameResult>();
                result.IsSuccessful = false;
                result.Code = "000691";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 通过父Name 和 Name获取系统配置信息

        public BaseResponse<ConfigResult> GetConfigByName(ConfigByNameParameter param)
        {
            if (this.ValidateData<ConfigByNameParameter>(param))
            {
                return initManager.GetConfigByName(param);
            }
            else
            {
                BaseResponse<ConfigResult> result = new BaseResponse<ConfigResult>();
                result.IsSuccessful = false;
                result.Code = "000701";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        public BaseResponse<ConfigResult> GetConfigByID(GetConfigByIDParam param)
        {
            if (this.ValidateData<GetConfigByIDParam>(param))
            {
                return initManager.GetConfigByID(param);
            }
            else
            {
                BaseResponse<ConfigResult> result = new BaseResponse<ConfigResult>();
                result.IsSuccessful = false;
                result.Code = "000701";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 判断系统中是否已存在监测树
        /// <summary>
        /// 创建人：王龙杰
        /// 创建时间：2017-12-14
        /// 创建记录：判断系统中是否已存在监测树
        /// </summary>
        /// <returns></returns>
        public BaseResponse<ExistMonitorTreeResult> IsExistMonitorTree()
        {
            return initManager.IsExistMonitorTree();
        }

        #endregion

        #region 判断监测树节点下是否存在设备
        /// <summary>
        /// 创建人：王龙杰
        /// 创建时间：2017-12-13
        /// 创建记录：判断监测树节点下是否存在设备
        /// </summary>
        /// <returns></returns>
        public BaseResponse<ExistDeviceInMonitorTreeResult> IsExistDeviceInMonitorTree()
        {
            return initManager.IsExistDeviceInMonitorTree();
        }

        #endregion

        /// <summary>
        /// 通用数据查看接口
        /// </summary>
        /// <returns></returns>
        public BaseResponse<CommonDataResult> GetComSetData(GetComSetDataParameter parameter)
        {
            BaseResponse<CommonDataResult> result = new BaseResponse<CommonDataResult>();
            if (this.ValidateData<GetComSetDataParameter>(parameter))
            {
                int table = parameter.table;
                switch (table)
                {
                    //监测树类型
                    case 1:
                        return initManager.GetMonitorTreeTypeDataForCommon(parameter);

                    //设备类型
                    case 2:
                        return initManager.GetDeviceTypeDataForCommon(parameter);

                    //测量位置类型
                    case 3:
                        return initManager.GetMSiteTypeDataForCommon(parameter);

                    //测量位置监测类型
                    case 4:
                        return initManager.GetMSiteMTTypeDataForCommon(parameter);

                    //无线振动信号类型
                    case 5:
                        return initManager.GetVIBTypeDataForCommon(parameter);

                    //特征值
                    case 6:
                        return initManager.GetEigenTypeDataForCommon(parameter);

                    //波长
                    case 7:
                        return initManager.GetWaveLengthTypeDataForCommon(parameter);

                    //上限频率
                    case 8:
                        return initManager.GetWaveUpperLimitTypeDataForCommon(parameter);

                    //下限频率
                    case 9:
                        return initManager.GetWaveLowerLimitTypeDataForCommon(parameter);

                    //传感器类型
                    case 10:
                        return initManager.GetWSTypeDataForCommon(parameter);

                    //传感器挂靠个数
                    case 11:
                        return initManager.GetWGTypeDataForCommon(parameter);

                    //三轴振动信号类型
                    case 13:
                        return initManager.GetTriaxialVIBTypeDataForCommon(parameter);

                    //有线振动信号类型
                    case 14:
                        return initManager.GetWiredVIBTypeDataForCommon(parameter);

                    //获取特征值波长
                    case 29:
                        return initManager.GetEigenWaveLengthTypeDataForCommon(parameter);

                    //获取包络滤波器上限
                    case 30:
                        return initManager.GetEnvlFilterUpperTypeDataForCommon(parameter);

                    //获取包络滤波器下限
                    case 31:
                        return initManager.GetEnvlFilterLowerTypeDataForCommon(parameter);

                    default:
                        result.IsSuccessful = false;
                        result.Code = "";
                        Task.Run(() => LogHelper.WriteLog(""));
                        return result;
                }
            }
            else
            {
                result.IsSuccessful = false;
                result.Code = "001151";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 通用数据添加接口
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> AddComSetData(AddComSetDataParameter parameter)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            if (this.ValidateData<AddComSetDataParameter>(parameter))
            {
                int table = parameter.table;
                switch (table)
                {
                    case 1:
                        return initManager.AddMonitorTreeTypeData(parameter);
                    case 2:
                        return initManager.AddDeviceTypeData(parameter);
                    case 3:
                        return initManager.AddMSiteTypeData(parameter);
                    case 4:
                        return initManager.AddMSiteMTTypeData(parameter);
                    case 5:
                        return initManager.AddVIBTypeData(parameter);
                    case 6:
                        return initManager.AddEigenTypeData(parameter);
                    case 7:
                        return initManager.AddWaveLengthTypeData(parameter);
                    case 8:
                        return initManager.AddWaveUpperLimitTypeData(parameter);
                    case 9:
                        return initManager.AddWaveLowerLimitTypeData(parameter);
                    case 10:
                        return initManager.AddWSTypeData(parameter);
                    case 11:
                        return initManager.AddWGTypeData(parameter);

                    case 29:
                        //特征值波长
                        return initManager.AddEigenValueWaveLengthTypeData(parameter);
                    case 30:
                        // 包络滤波器上限
                        return initManager.AddEnvlFilterUpperTypeData(parameter);
                    case 31:
                        //包络滤波器下限
                        return initManager.AddEnvlFilterLowerTypeData(parameter);

                    default:
                        result.IsSuccessful = false;
                        result.Code = "";
                        return result;
                }
            }
            else
            {
                result.IsSuccessful = false;
                result.Code = "";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 通用数据编辑接口
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> EditComSetData(EditComSetDataParameter parameter)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            if (this.ValidateData<EditComSetDataParameter>(parameter))
            {
                int table = parameter.table;
                switch (table)
                {
                    case 1:
                        return initManager.EditMonitorTreeTypeData(parameter);
                    case 2:
                        return initManager.EditDeviceTypeData(parameter);
                    case 3:
                        return initManager.EditMSiteTypeData(parameter);
                    case 4:
                        return initManager.EditMSiteMTTypeData(parameter);
                    case 5:
                        return initManager.EditVIBTypeData(parameter);
                    case 6:
                        return initManager.EditEigenTypeData(parameter);
                    case 7:
                        return initManager.EditWaveLengthTypeData(parameter);
                    case 8:
                        return initManager.EditWaveUpperLimitTypeData(parameter);
                    case 9:
                        return initManager.EditWaveLowerLimitTypeData(parameter);
                    case 10:
                        return initManager.EditWSTypeData(parameter);
                    case 11:
                        return initManager.EditWGTypeData(parameter);

                    case 30:
                        return initManager.EditEnvlFilterUpperType(parameter);
                    case 31:
                        return initManager.EditEnvlFilterLowerType(parameter);
                    default:
                        result.IsSuccessful = false;
                        result.Code = "";
                        return result;
                }
            }
            else
            {
                result.IsSuccessful = false;
                result.Code = "";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 通用数据删除接口
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> DeleteComSetData(DeleteComSetDataParameter parameter)
        {
            BaseResponse<bool> result = new BaseResponse<bool>();
            if (this.ValidateData<DeleteComSetDataParameter>(parameter))
            {
                int table = parameter.table;
                switch (table)
                {
                    case 1:
                        return initManager.DeleteMonitorTreeTypeData(parameter);
                    case 2:
                        return initManager.DeleteDeviceTypeData(parameter);
                    case 3:
                        return initManager.DeleteMSiteTypeData(parameter);
                    case 4:
                        return initManager.DeleteMSiteMTTypeData(parameter);
                    case 5:
                        return initManager.DeleteVIBTypeData(parameter);
                    case 6:
                        return initManager.DeleteEigenTypeData(parameter);
                    case 7:
                        return initManager.DeleteWaveLengthTypeData(parameter);
                    case 8:
                        return initManager.DeleteWaveUpperLimitTypeData(parameter);
                    case 9:
                        return initManager.DeleteWaveLowerLimitTypeData(parameter);
                    case 10:
                        return initManager.DeleteWSTypeData(parameter);
                    case 11:
                        return initManager.DeleteWGTypeData(parameter);

                    case 30:
                        return initManager.DeleteEnvlFilterUpperType(parameter);
                    case 31:
                        return initManager.DeleteEnvlFilterLowerType(parameter);
                    default:
                        result.IsSuccessful = false;
                        result.Code = "";
                        return result;
                }
            }
            else
            {
                result.IsSuccessful = false;
                result.Code = "";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #region 连接状态 CRUD

        public BaseResponse<CommonDataResult> GetConnectTypeDataForCommon(ViewConnectTypeParameter param)
        {
            if (this.ValidateData<ViewConnectTypeParameter>(param))
            {
                return initManager.GetConnectTypeDataForCommon(param);
            }
            else
            {
                BaseResponse<CommonDataResult> result = new BaseResponse<CommonDataResult>();
                result.IsSuccessful = false;
                result.Code = "001151";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        public BaseResponse<bool> AddConnectTypeData(AddConnectTypeParameter param)
        {
            if (this.ValidateData<AddConnectTypeParameter>(param))
            {
                return initManager.AddConnectTypeData(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001161";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        public BaseResponse<bool> EditConnectTypeData(EditConnectTypeParameter param)
        {
            if (this.ValidateData<EditConnectTypeParameter>(param))
            {
                return initManager.EditConnectTypeData(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001171";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        public BaseResponse<bool> DeleteConnectTypeData(DeleteConnectTypeParameter param)
        {
            if (this.ValidateData<DeleteConnectTypeParameter>(param))
            {
                return initManager.DeleteConnectTypeData(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001181";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        /// <summary>
        /// 获取设备树和Server树
        /// </summary>
        /// <param name="Para"></param>
        /// <returns></returns>
        public BaseResponse<MonitorTreeDataForNavigationResult> GetAllMonitorTreeAndServerTreebyRole(RoleForMonitorTreeAndServerTreeParameter Para)
        {
            if (this.ValidateData<MonitorTreeDataForNavigationResult>(Para))
            {
                return initManager.GetAllMonitorTreeAndServerTreebyRole(Para);
            }
            else
            {
                BaseResponse<MonitorTreeDataForNavigationResult> result = new BaseResponse<MonitorTreeDataForNavigationResult>();
                result.IsSuccessful = false;
                result.Code = "000701";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #region 监测树类型级别(Describe)是否重复

        /// <summary>
        /// 监测树类型级别(Describe)是否重复
        /// 创建人:王龙杰
        /// 创建时间：2017-11-21
        /// </summary>
        /// <param name="Para"></param>
        /// <returns></returns>
        public BaseResponse<IsRepeatResult> IsExistDescribeInMonitorTree(IsExistDescribeInMonitorTreeParameter Para)
        {
            if (this.ValidateData<IsExistDescribeInMonitorTreeParameter>(Para))
            {
                return initManager.IsExistDescribeInMonitorTree(Para);
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

        /// <summary>
        /// 验证通用数据是否有相同Code
        /// </summary>
        /// <param name="Para"></param>
        /// <returns></returns>
        public BaseResponse<IsRepeatResult> IsExistComSetCode(IsExistComSetCodeParameter Para)
        {
            if (this.ValidateData<IsExistComSetCodeParameter>(Para))
            {
                return initManager.IsExistComSetCode(Para, false);
            }
            else
            {
                BaseResponse<IsRepeatResult> result = new BaseResponse<IsRepeatResult>();
                result.IsSuccessful = false;
                result.Code = "000701";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 通过ID验证除当前外，通用数据是否有相同Code
        /// </summary>
        /// <param name="Para"></param>
        /// <returns></returns>
        public BaseResponse<IsRepeatResult> IsExistCodeByIDAndCode(IsExistComSetCodeParameter Para)
        {
            if (this.ValidateData<IsExistComSetCodeParameter>(Para))
            {
                return initManager.IsExistComSetCode(Para, true);
            }
            else
            {
                BaseResponse<IsRepeatResult> result = new BaseResponse<IsRepeatResult>();
                result.IsSuccessful = false;
                result.Code = "000701";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 验证通用数据是否有相同Name
        /// </summary>
        /// <param name="Para"></param>
        /// <returns></returns>
        public BaseResponse<IsRepeatResult> IsExistComSetName(IsExistComSetNameParameter Para)
        {
            if (this.ValidateData<IsExistComSetNameParameter>(Para))
            {
                return initManager.IsExistComSetName(Para, false);
            }
            else
            {
                BaseResponse<IsRepeatResult> result = new BaseResponse<IsRepeatResult>();
                result.IsSuccessful = false;
                result.Code = "000701";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 通过ID验证除当前外，通用数据是否有相同Name
        /// </summary>
        /// <param name="Para"></param>
        /// <returns></returns>
        public BaseResponse<IsRepeatResult> IsExistNameByIDAndName(IsExistComSetNameParameter Para)
        {
            if (this.ValidateData<IsExistComSetNameParameter>(Para))
            {
                return initManager.IsExistComSetName(Para, true);
            }
            else
            {
                BaseResponse<IsRepeatResult> result = new BaseResponse<IsRepeatResult>();
                result.IsSuccessful = false;
                result.Code = "000701";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 设置通用数据显示顺序
        /// </summary>
        /// <param name="Para"></param>
        /// <returns></returns>
        public BaseResponse<bool> SetComSetOrder(SetComSetOrderParameter Para)
        {
            if (this.ValidateData<SetComSetOrderParameter>(Para))
            {
                return initManager.SetComSetOrder(Para);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "000701";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 获取振动信号类型和特征值类型(可用状态)
        /// </summary>
        /// <param name="Para"></param>
        /// <returns></returns>
        public BaseResponse<ComSetDataForVibAndEigenResult> GetComSetDataForVibAndEigen()
        {
            return initManager.GetComSetDataForVibAndEigen();
        }

        /// <summary>
        /// 通过父Code 和 Code获取系统配置信息，与通过Code获取Config合并
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<ConfigResult> GetConfigByCode(ConfigByCodeParameter Para)
        {
            if (this.ValidateData<ConfigByCodeParameter>(Para))
            {
                return initManager.GetConfigByCode(Para);
            }
            else
            {
                BaseResponse<ConfigResult> result = new BaseResponse<ConfigResult>();
                result.IsSuccessful = false;
                result.Code = "000701";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #region 获取通用数据振动及特征值信息
        /// <summary>
        /// 获取通用数据振动及特征值信息
        /// </summary>
        /// <param name="parameter"></param>
        public BaseResponse<GetVibAndEigenComSetInfoResult> GetVibAndEigenComSetInfo(BaseRequest parameter)
        {
            if (this.ValidateData<BaseRequest>(parameter))
            {
                return initManager.GetVibAndEigenComSetInfo();
            }
            else
            {
                BaseResponse<GetVibAndEigenComSetInfoResult> result = new BaseResponse<GetVibAndEigenComSetInfoResult>();
                result.IsSuccessful = false;
                result.Code = "009211";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 批量编辑
        /// <summary>
        /// 创建人：王颖辉 
        /// 创建时间:2017-10-21
        /// 创建内容:批量编辑
        /// </summary>
        /// <param name="parameter"参数</param>
        /// <returns></returns>
        public BaseResponse<bool> EditConfigList(EditConfigListParameter parameter)
        {
            if (this.ValidateData<EditConfigListParameter>(parameter))
            {
                return initManager.EditConfigList(parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "009211";
                Task.Run(() => LogHelper.WriteLog("未通过安全验证：（" + result.Code + "：" + result.Reason + "）"));
                return result;
            }
        }
        #endregion

        #region 获取通用数据监测树类型接口

        public BaseResponse<CommonDataResult> GetMonitorTreeTypeDataForCommon(MonitorTreeTypeParameter param)
        {
            if (this.ValidateData<MonitorTreeTypeParameter>(param))
            {
                return initManager.GetMonitorTreeTypeDataForCommon1(param);
            }
            else
            {
                BaseResponse<CommonDataResult> result = new BaseResponse<CommonDataResult>();
                result.IsSuccessful = false;
                result.Code = "000711";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion
    }
    #endregion
}