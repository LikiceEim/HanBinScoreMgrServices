/***********************************************************************
 *Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：  iCMS.Presentation.Server
 *文件名：    IUtilityService.cs
 *创建人：    钱行慕
 *创建时间：  2016/10/19 10:10:19
 *描述：服务表现层，响应调用方对用户、权限组、日志的操作请求
 *=====================================================================**/

using System.ServiceModel;
using System.ServiceModel.Web;

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Response.Utility;
using iCMS.Common.Component.Data.Response.DiagnosticAnalysis;
using iCMS.Common.Component.Data.Request.Utility;
using iCMS.Common.Component.Data.Response.DevicesConfig;
using iCMS.Common.Component.Data.Response.Common;

namespace iCMS.Presentation.Server
{
    [ServiceContract]
    public interface IUtilityService
    {
        /// <summary>
        /// 获取角色是否具有权限
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
                Method = "POST",
                RequestFormat = WebMessageFormat.Json,
                ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> IsAuthorized(IsAuthorizedParameter param);
        /// <summary>
        /// 获取监测树节点
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
                Method = "POST",
                RequestFormat = WebMessageFormat.Json,
                ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<MonitorTreeDataForNavigationResult> GetMonitorTreeNodes(MTNodesParameter param);
        /// <summary>
        /// 监测树根据节点获取其所有父节点
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
                Method = "POST",
                RequestFormat = WebMessageFormat.Json,
                ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<MonitorTreeDataForNavigationResult> GetMonitorTreeParentNodes(MonitorTreeNodesParameter param);

        #region 判断节点树是否挂靠设备，若有，则返回设备名称
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
               Method = "POST",
               RequestFormat = WebMessageFormat.Json,
               ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetDeviceNameForMTNodeResult> GetDeviceNameForMTNode(DeviceNameForMTNodeParameter param);
        #endregion

        #region Server根据节点获取其所有的父节点
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
          Method = "POST",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<MonitorTreeDataForNavigationResult> GetServerTreeParentNodes(MonitorTreeNodesParameter param);
        #endregion

        #region 判断名称是否重复
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<NewValueExistedResult> IsNewValueExisted(IsNewValueExistedParameter param);
        #endregion

        #region 轴承库相关
        /// <summary>
        /// 根据轴承厂商、轴承型号、轴承描述进行搜索
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
              Method = "POST",
              RequestFormat = WebMessageFormat.Json,
              ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<BearingEigenResult> BearingSearch(BearingSearchParameter param);

        /// <summary>
        /// 根据厂商名称、编号等信息检索出符合要求的，厂商信息。如未输入任何有意义信息，则查询出所有厂商信息
        /// </summary>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
              Method = "POST",
              RequestFormat = WebMessageFormat.Json,
              ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<BearingFactoryResult> GetFactorys(GetFactoriesParameter param);

        /// <summary>
        /// 添加新的轴承特征频率信息到轴承库中。所有参数参数必须提供，否则返回错误
        /// </summary>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> AddBearinEigenvalue(AddBearinEigenvalueParameter param);

        /// <summary>
        /// 修改已存在的轴承特征频率信息
        /// </summary>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> UpdateBearinEigenvalue(UpdateBearinEigenvalueParameter param);
        /// <summary>
        ///删除已存在的轴承特征频率信息 
        /// </summary>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> DeleteBearinEigenvalue(DeleteBearinEigenvalueParameter param);

        /// <summary>
        /// 新增轴承库厂商
        /// </summary>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> FactoryAdd(AddFactoryParameter param);
        /// <summary>
        /// 修改轴承库厂商
        /// </summary>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> FactoryUpdate(UpdateFactoryParameter param);
        /// <summary>
        /// 删除轴承库厂商
        /// </summary>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> FactoryDelete(DeleteFactoryParameter param);
        /// <summary>
        /// 通过轴承厂商编号，获取轴承型号信息
        /// </summary>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<BearResult> GetBearingByFactoryID(GetBearingByFactoryIDParameter param);
        /// <summary>
        /// 验证规则，保证轴承库厂商&型号的唯一性
        /// </summary>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> CheckFidAndBearingNumUnique(CheckFidAndBearingNumUniqueParameter param);

        #endregion

        /// <summary>
        /// 系统日志写入
        /// </summary>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> LogSysMessage(AddSysLogParameter param);

        #region 图片管理
        /// <summary>
        /// 图片预览
        /// </summary>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
         Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<AllImagesResult> GetAllImages(AllImagesParameter param);

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
            Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> SaveImagePath(SaveImageParameter param);

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> DeleteImage(DeleteImageParameter param);

        #region  根据传入设备ID，获取所有完整的父监测树节点，同时返回当前设备所在监测树节点
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间：2017-09-28
        /// 创建内容:根据传入设备ID，获取所有完整的父监测树节点，同时返回当前设备所在监测树节点
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<GetFullMonitorTreeDataByDeviceResult> GetFullMonitorTreeDataByDevice(GetFullMonitorTreeDataByDeviceParameter parameter);
        #endregion

        /// <summary>
        /// 系统说明-读取功能介绍
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [WebInvoke(
            BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<MoudleContentDetailResult> GetMoudleContent(MoudleContentDetailParameter parameter);

        /// <summary>
        /// 读取系统说明列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [WebInvoke(
            BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<MoudleContentListResult> GetMoudleContentList();

        /// <summary>
        /// 新增系统说明
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [WebInvoke(
            BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> AddMoudleContent(AddMoudleContentParameter parameter);

        /// <summary>
        /// 编辑系统说明
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [WebInvoke(
            BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> EditMoudleContent(EditMoudleContentParameter parameter);

        /// <summary>
        /// 删除系统说明
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [WebInvoke(
            BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> DeleteMoudleContent(DeleteMoudleContentParameter parameter);

        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [WebInvoke(
            BodyStyle = WebMessageBodyStyle.Bare,
           Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetAuthorizedResult> GetAuthorized(GetAuthorizedParam parameter);

        #region 安全验证
        /// <summary>
        /// 创建时间：2017-07-24
        /// 创建人:王颖辉
        /// 创建内容:安全验证
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
                 Method = "POST",
                 RequestFormat = WebMessageFormat.Json,
                 ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<ResponseResult> SafetyVerification(BaseRequest parameter);
        #endregion

        #endregion


        #region 批量删除轴承库厂商
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间:2017-10-26
        /// 创建内容:批量删除轴承库厂商
        /// </summary>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
          Method = "POST",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<bool> FactorysDelete(DeleteFactoryParameter parameter);

        #endregion

        #region 获取监测树子节点，通过父子点
        /// <summary>
        /// 获取监测树子节点，通过父子点
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare,
          Method = "POST",
          RequestFormat = WebMessageFormat.Json,
          ResponseFormat = WebMessageFormat.Json)]
        BaseResponse<GetMonitorTreeByParentIDResult> GetMonitorTreeByParentID(GetMonitorTreeByParentIDParameter parameter);
        #endregion
    }
}
