/************************************************************************************
*Copyright (c) 2016iLine All Rights Reserved.
*命名空间：iCMS.Service.Utility
*文件名：  IUtilityManager
*创建人：  QXM
*创建时间：2016-10-31
*描述：通用数据
/************************************************************************************/

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Response.Utility;
using iCMS.Common.Component.Data.Response.DiagnosticAnalysis;
using iCMS.Common.Component.Data.Request.Utility;
using iCMS.Common.Component.Data.Response.DevicesConfig;

namespace iCMS.Service.Web.Utility
{
    #region 通用数据
    /// <summary>
    /// 通用数据
    /// </summary>
    public interface IUtilityManager
    {
        #region 是否有新数据
        /// <summary>
        /// 是否有新数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        BaseResponse<NewValueExistedResult> IsNewValueExisted(IsNewValueExistedParameter param);
        #endregion

        #region 是否有权限
        /// <summary>
        /// 是否有权限
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        BaseResponse<bool> IsAuthorized(IsAuthorizedParameter param);
        #endregion

        #region 获取监测树节点
        /// <summary>
        /// 获取监测树节点
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        BaseResponse<MonitorTreeDataForNavigationResult> GetMonitorTreeNodes(MTNodesParameter param);
        #endregion

        #region 通过父节点获取监测树节点
        /// <summary>
        /// 通过父节点获取监测树节点
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        BaseResponse<MonitorTreeDataForNavigationResult> GetMonitorTreeParentNodes(MonitorTreeNodesParameter param);
        #endregion

        #region 判断节点树是否挂靠设备，若有，则返回设备名称
        BaseResponse<GetDeviceNameForMTNodeResult> GetDeviceNameForMTNode(DeviceNameForMTNodeParameter param);
        #endregion

        #region Server根据节点获取其所有的父节点
        BaseResponse<MonitorTreeDataForNavigationResult> GetServerTreeParentNodes(MonitorTreeNodesParameter param);
        #endregion

        #region 轴承库相关
        /// <summary>
        /// 根据轴承厂商、轴承型号、轴承描述进行搜索
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        BaseResponse<BearingEigenResult> BearingSearch(BearingSearchParameter param);

        /// <summary>
        /// 根据厂商名称、编号等信息检索出符合要求的，厂商信息。如未输入任何有意义信息，则查询出所有厂商信息
        /// </summary>
        /// <returns></returns>
        BaseResponse<BearingFactoryResult> GetFactorys(GetFactoriesParameter param);

        /// <summary>
        /// 添加新的轴承特征频率信息到轴承库中。所有参数参数必须提供，否则返回错误
        /// </summary>
        /// <returns></returns>
        BaseResponse<bool> AddBearinEigenvalue(AddBearinEigenvalueParameter param);

        /// <summary>
        /// 修改已存在的轴承特征频率信息
        /// </summary>
        /// <returns></returns>
        BaseResponse<bool> UpdateBearinEigenvalue(UpdateBearinEigenvalueParameter param);

        /// <summary>
        ///删除已存在的轴承特征频率信息 
        /// </summary>
        /// <returns></returns>
        BaseResponse<bool> DeleteBearinEigenvalue(DeleteBearinEigenvalueParameter param);

        /// <summary>
        /// 新增轴承库厂商
        /// </summary>
        /// <returns></returns>
        BaseResponse<bool> FactoryAdd(AddFactoryParameter param);

        /// <summary>
        /// 修改轴承库厂商
        /// </summary>
        /// <returns></returns>
        BaseResponse<bool> FactoryUpdate(UpdateFactoryParameter param);

        /// <summary>
        /// 删除轴承库厂商
        /// </summary>
        /// <returns></returns>
        BaseResponse<bool> FactoryDelete(DeleteFactoryParameter param);

        /// <summary>
        /// 通过轴承厂商编号，获取轴承型号信息
        /// </summary>
        /// <returns></returns>
        BaseResponse<BearResult> GetBearingByFactoryID(GetBearingByFactoryIDParameter param);

        /// <summary>
        /// 验证规则，保证轴承库厂商&型号的唯一性
        /// </summary>
        /// <returns></returns>
        BaseResponse<bool> CheckFidAndBearingNumUnique(CheckFidAndBearingNumUniqueParameter param);
        #endregion

        #region 图片管理
        /// <summary>
        /// 图片预览
        /// </summary>
        BaseResponse<AllImagesResult> GetAllImages(AllImagesParameter param);

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        BaseResponse<bool> SaveImagePath(SaveImageParameter param);

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        BaseResponse<bool> DeleteImage(DeleteImageParameter param);
        #endregion

        #region 根据传入设备ID，获取所有完整的父监测树节点，同时返回当前设备所在监测树节点
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间：2017-09-28
        /// 创建内容:根据传入设备ID，获取所有完整的父监测树节点，同时返回当前设备所在监测树节点
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<GetFullMonitorTreeDataByDeviceResult> GetFullMonitorTreeDataByDevice(GetFullMonitorTreeDataByDeviceParameter parameter);
        #endregion

        #region 系统说明-读取功能介绍
        /// <summary>
        /// 系统说明-读取功能介绍
        /// 创建人：王龙杰
        /// 创建时间：2017-10-13
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<MoudleContentDetailResult> GetMoudleContent(MoudleContentDetailParameter parameter);
        #endregion

        #region 读取系统说明列表
        /// <summary>
        /// 读取系统说明列表
        /// 创建人：王龙杰
        /// 创建时间：2017-10-31
        /// </summary>
        /// <returns></returns>
        BaseResponse<MoudleContentListResult> GetMoudleContentList();
        #endregion

        #region 新增系统说明
        /// <summary>
        /// 新增系统说明
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> AddMoudleContent(AddMoudleContentParameter parameter);
        #endregion

        #region 编辑系统说明
        /// <summary>
        /// 编辑系统说明
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> EditMoudleContent(EditMoudleContentParameter parameter);
        #endregion

        #region 删除系统说明
        /// <summary>
        /// 删除系统说明
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> DeleteMoudleContent(DeleteMoudleContentParameter parameter);
        #endregion

        #region 获取用户权限
        /// <summary>
        /// 获取用户权限
        /// 创建人：王龙杰
        /// 创建时间：2017-10-13
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        BaseResponse<GetAuthorizedResult> GetAuthorized(GetAuthorizedParam parameter);

        #endregion

        #region 批量删除轴承库厂商
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间:2017-10-26
        /// 创建内容:批量删除轴承库厂商
        /// </summary>
        /// <returns></returns>
        BaseResponse<bool> FactorysDelete(DeleteFactoryParameter param);
        #endregion

        #region 获取监测树子节点，通过父子点
        /// <summary>
        /// 获取监测树子节点，通过父子点
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<GetMonitorTreeByParentIDResult> GetMonitorTreeByParentID(GetMonitorTreeByParentIDParameter parameter);
        #endregion

    }
    #endregion
}
