/***********************************************************************
 *Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：  iCMS.Presentation.Server
 *文件名：    SystemManagerService
 *创建人：    钱行慕
 *创建时间：  2016/10/19 10:10:19
 *描述：公共接口服务
 *
 *修改人：张辽阔
 *修改时间：2016-11-11
 *描述：增加错误编码
 *
 *修改人：张辽阔
 *修改时间：2016-12-15
 *描述：未通过安全验证时，增加日志记录
 *=====================================================================**/

using System.Threading.Tasks;

using iCMS.Service.Web.Utility;
using iCMS.Presentation.Common;
using iCMS.Common.Component.Data.Base;
using iCMS.Service.Web.SystemManager;
using iCMS.Common.Component.Data.Response.Utility;
using iCMS.Common.Component.Data.Response.DiagnosticAnalysis;
using iCMS.Common.Component.Data.Request.Utility;
using iCMS.Common.Component.Tool;
using System.ServiceModel;
using iCMS.Common.Component.Data.Response.DevicesConfig;
using iCMS.Common.Component.Data.Response.Common;

namespace iCMS.Presentation.Server
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [CustomExceptionBehaviour(typeof(CustomExceptionHandler))]
    public class UtilityService : BaseService, IUtilityService
    {
        #region 私有变量

        private IUtilityManager utilityManager = null;
        private ILogManager logManager = null;

        #endregion

        #region 构造函数

        public UtilityService(IUtilityManager utilityManager, ILogManager logManager)
        {
            this.utilityManager = utilityManager;
            this.logManager = logManager;
        }

        #endregion

        #region 获取角色是否具有权限

        public BaseResponse<bool> IsAuthorized(IsAuthorizedParameter param)
        {
            if (this.ValidateData<IsAuthorizedParameter>(param))
            {
                return utilityManager.IsAuthorized(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001371";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 获取监测树节点

        public BaseResponse<MonitorTreeDataForNavigationResult> GetMonitorTreeNodes(MTNodesParameter param)
        {
            if (this.ValidateData<MTNodesParameter>(param))
            {
                return utilityManager.GetMonitorTreeNodes(param);
            }
            else
            {
                BaseResponse<MonitorTreeDataForNavigationResult> result = new BaseResponse<MonitorTreeDataForNavigationResult>();
                result.IsSuccessful = false;
                result.Code = "001381";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 监测树根据节点获取其所有父节点

        public BaseResponse<MonitorTreeDataForNavigationResult> GetMonitorTreeParentNodes(MonitorTreeNodesParameter param)
        {
            if (this.ValidateData<MonitorTreeNodesParameter>(param))
            {
                return utilityManager.GetMonitorTreeParentNodes(param);
            }
            else
            {
                BaseResponse<MonitorTreeDataForNavigationResult> result = new BaseResponse<MonitorTreeDataForNavigationResult>();
                result.IsSuccessful = false;
                result.Code = "001391";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 判断节点树是否挂靠设备，若有，则返回设备名称

        public BaseResponse<GetDeviceNameForMTNodeResult> GetDeviceNameForMTNode(DeviceNameForMTNodeParameter param)
        {
            if (this.ValidateData<DeviceNameForMTNodeParameter>(param))
            {
                return utilityManager.GetDeviceNameForMTNode(param);
            }
            else
            {
                BaseResponse<GetDeviceNameForMTNodeResult> result = new BaseResponse<GetDeviceNameForMTNodeResult>();
                result.IsSuccessful = false;
                result.Code = "001401";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region Server根据节点获取其所有的父节点

        public BaseResponse<MonitorTreeDataForNavigationResult> GetServerTreeParentNodes(MonitorTreeNodesParameter param)
        {
            if (this.ValidateData<MonitorTreeNodesParameter>(param))
            {
                return utilityManager.GetServerTreeParentNodes(param);
            }
            else
            {
                BaseResponse<MonitorTreeDataForNavigationResult> result = new BaseResponse<MonitorTreeDataForNavigationResult>();
                result.IsSuccessful = false;
                result.Code = "001411";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        /// <summary>
        /// 判断名称是否重复
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<NewValueExistedResult> IsNewValueExisted(IsNewValueExistedParameter param)
        {
            if (this.ValidateData<IsNewValueExistedParameter>(param))
            {
                return utilityManager.IsNewValueExisted(param);
            }
            else
            {
                BaseResponse<NewValueExistedResult> result = new BaseResponse<NewValueExistedResult>();
                result.IsSuccessful = false;
                result.Code = "001421";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #region 轴承库相关

        /// <summary>
        /// 根据轴承厂商、轴承型号、轴承描述进行搜索
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<BearingEigenResult> BearingSearch(BearingSearchParameter param)
        {
            if (this.ValidateData<BearingSearchParameter>(param))
            {
                return utilityManager.BearingSearch(param);
            }
            else
            {
                BaseResponse<BearingEigenResult> result = new BaseResponse<BearingEigenResult>();
                result.IsSuccessful = false;
                result.Code = "001431";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 根据厂商名称、编号等信息检索出符合要求的，厂商信息。如未输入任何有意义信息，则查询出所有厂商信息
        /// </summary>
        /// <returns></returns>
        public BaseResponse<BearingFactoryResult> GetFactorys(GetFactoriesParameter param)
        {
            if (this.ValidateData<GetFactoriesParameter>(param))
            {
                return utilityManager.GetFactorys(param);
            }
            else
            {
                BaseResponse<BearingFactoryResult> result = new BaseResponse<BearingFactoryResult>();
                result.IsSuccessful = false;
                result.Code = "001441";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 添加新的轴承特征频率信息到轴承库中。所有参数参数必须提供，否则返回错误
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> AddBearinEigenvalue(AddBearinEigenvalueParameter param)
        {
            if (this.ValidateData<AddBearinEigenvalueParameter>(param))
            {
                return utilityManager.AddBearinEigenvalue(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001451";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 修改已存在的轴承特征频率信息
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> UpdateBearinEigenvalue(UpdateBearinEigenvalueParameter param)
        {
            if (this.ValidateData<UpdateBearinEigenvalueParameter>(param))
            {
                return utilityManager.UpdateBearinEigenvalue(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001461";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        ///删除已存在的轴承特征频率信息 
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> DeleteBearinEigenvalue(DeleteBearinEigenvalueParameter param)
        {
            if (this.ValidateData<DeleteBearinEigenvalueParameter>(param))
            {
                return utilityManager.DeleteBearinEigenvalue(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001471";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 新增轴承库厂商
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> FactoryAdd(AddFactoryParameter param)
        {
            if (this.ValidateData<AddFactoryParameter>(param))
            {
                return utilityManager.FactoryAdd(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001481";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 修改轴承库厂商
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> FactoryUpdate(UpdateFactoryParameter param)
        {
            if (this.ValidateData<UpdateFactoryParameter>(param))
            {
                return utilityManager.FactoryUpdate(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001491";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 删除轴承库厂商
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> FactoryDelete(DeleteFactoryParameter param)
        {
            if (this.ValidateData<DeleteFactoryParameter>(param))
            {
                return utilityManager.FactoryDelete(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001501";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 通过轴承厂商编号，获取轴承型号信息
        /// </summary>
        /// <returns></returns>
        public BaseResponse<BearResult> GetBearingByFactoryID(GetBearingByFactoryIDParameter param)
        {
            if (this.ValidateData<GetBearingByFactoryIDParameter>(param))
            {
                return utilityManager.GetBearingByFactoryID(param);
            }
            else
            {
                BaseResponse<BearResult> result = new BaseResponse<BearResult>();
                result.IsSuccessful = false;
                result.Code = "001511";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 验证规则，保证轴承库厂商&型号的唯一性
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> CheckFidAndBearingNumUnique(CheckFidAndBearingNumUniqueParameter param)
        {
            if (this.ValidateData<CheckFidAndBearingNumUniqueParameter>(param))
            {
                return utilityManager.CheckFidAndBearingNumUnique(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001521";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        /// <summary>
        /// 系统日志写入
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> LogSysMessage(AddSysLogParameter param)
        {
            if (this.ValidateData<AddSysLogParameter>(param))
            {
                return logManager.LogSysMessage(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001531";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #region 图片管理

        /// <summary>
        /// 图片预览
        /// </summary>
        public BaseResponse<AllImagesResult> GetAllImages(AllImagesParameter param)
        {
            if (this.ValidateData<AllImagesParameter>(param))
            {
                return utilityManager.GetAllImages(param);
            }
            else
            {
                BaseResponse<AllImagesResult> result = new BaseResponse<AllImagesResult>();
                result.IsSuccessful = false;
                result.Code = "001541";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<bool> SaveImagePath(SaveImageParameter param)
        {
            if (this.ValidateData<SaveImageParameter>(param))
            {
                return utilityManager.SaveImagePath(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001551";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<bool> DeleteImage(DeleteImageParameter param)
        {
            if (this.ValidateData<DeleteImageParameter>(param))
            {
                return utilityManager.DeleteImage(param);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "001561";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #region 根据传入设备ID，获取所有完整的父监测树节点，同时返回当前设备所在监测树节点
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间：2017-09-28
        /// 创建内容:根据传入设备ID，获取所有完整的父监测树节点，同时返回当前设备所在监测树节点
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetFullMonitorTreeDataByDeviceResult> GetFullMonitorTreeDataByDevice(GetFullMonitorTreeDataByDeviceParameter parameter)
        {
            if (this.ValidateData<DeleteImageParameter>(parameter))
            {
                return utilityManager.GetFullMonitorTreeDataByDevice(parameter);
            }
            else
            {
                BaseResponse<GetFullMonitorTreeDataByDeviceResult> result = new BaseResponse<GetFullMonitorTreeDataByDeviceResult>();
                result.IsSuccessful = false;
                result.Code = "009221";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #endregion

        #region 系统说明-读取功能介绍
        /// <summary>
        /// 系统说明-读取功能介绍
        /// 创建人：王龙杰
        /// 创建时间：2017-10-13
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<MoudleContentDetailResult> GetMoudleContent(MoudleContentDetailParameter parameter)
        {
            if (this.ValidateData<MoudleContentDetailParameter>(parameter))
            {
                return utilityManager.GetMoudleContent(parameter);
            }
            else
            {
                BaseResponse<MoudleContentDetailResult> result = new BaseResponse<MoudleContentDetailResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        /// <summary>
        /// 读取系统说明列表
        /// 创建人：王龙杰
        /// 创建时间：2017-10-31
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<MoudleContentListResult> GetMoudleContentList()
        {
            return utilityManager.GetMoudleContentList();
        }

        /// <summary>
        /// 新增系统说明
        /// 创建人：王龙杰
        /// 创建时间：2017-10-31
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> AddMoudleContent(AddMoudleContentParameter parameter)
        {
            if (this.ValidateData<AddMoudleContentParameter>(parameter))
            {
                return utilityManager.AddMoudleContent(parameter);
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

        /// <summary>
        /// 编辑系统说明
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> EditMoudleContent(EditMoudleContentParameter parameter)
        {
            if (this.ValidateData<EditMoudleContentParameter>(parameter))
            {
                return utilityManager.EditMoudleContent(parameter);
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

        /// <summary>
        /// 删除系统说明
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> DeleteMoudleContent(DeleteMoudleContentParameter parameter)
        {
            if (this.ValidateData<DeleteMoudleContentParameter>(parameter))
            {
                return utilityManager.DeleteMoudleContent(parameter);
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

        #region 获取用户权限
        /// <summary>
        /// 获取用户权限
        /// 创建人：王龙杰
        /// 创建时间：2017-10-13
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<GetAuthorizedResult> GetAuthorized(GetAuthorizedParam parameter)
        {
            if (this.ValidateData<GetAuthorizedParam>(parameter))
            {
                return utilityManager.GetAuthorized(parameter);
            }
            else
            {
                BaseResponse<GetAuthorizedResult> result = new BaseResponse<GetAuthorizedResult>();
                result.IsSuccessful = false;
                result.Code = "008222";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }

        #endregion

        #region 安全验证
        /// <summary>
        /// 创建时间：2017-07-24
        /// 创建人:王颖辉
        /// 创建内容:安全验证
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<ResponseResult> SafetyVerification(BaseRequest parameter)
        {
            if (ValidateData<ResponseResult>(parameter))
            {
                BaseResponse<ResponseResult> result = new BaseResponse<ResponseResult>();
                result.IsSuccessful = true;
                LogHelper.WriteLog("通过安全验证");
                return result;
            }
            else
            {
                BaseResponse<ResponseResult> result = new BaseResponse<ResponseResult>();
                result.IsSuccessful = false;
                result.Code = "012451";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 批量删除轴承库厂商
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间:2017-10-26
        /// 创建内容:批量删除轴承库厂商
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> FactorysDelete(DeleteFactoryParameter parameter)
        {
            if (this.ValidateData<DeleteFactoryParameter>(parameter))
            {
                return utilityManager.FactorysDelete(parameter);
            }
            else
            {
                BaseResponse<bool> result = new BaseResponse<bool>();
                result.IsSuccessful = false;
                result.Code = "009371";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion

        #region 获取监测树子节点，通过父子点
        /// <summary>
        /// 获取监测树子节点，通过父子点
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetMonitorTreeByParentIDResult> GetMonitorTreeByParentID(GetMonitorTreeByParentIDParameter parameter)
        {
            if (this.ValidateData<GetMonitorTreeByParentIDParameter>(parameter))
            {
                return utilityManager.GetMonitorTreeByParentID(parameter);
            }
            else
            {
                BaseResponse<GetMonitorTreeByParentIDResult> result = new BaseResponse<GetMonitorTreeByParentIDResult>();
                result.IsSuccessful = false;
                result.Code = "009662";
                LogHelper.WriteLog(string.Format("未通过安全验证：（{0}：{1}", result.Code, result.Reason));
                return result;
            }
        }
        #endregion
    }
}