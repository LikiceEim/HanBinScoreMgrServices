/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 * 命名空间：iCMS.Service.DevicesConfig
 * 文件名：  IDevicesConfigManager
 * 创建人：  LF
 * 创建时间：2016-10-21
 * 描述：设备树业务处理类
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.DevicesConfig;
using iCMS.Common.Component.Data.Request.DiagnosticAnalysis.MonitorTree;
using iCMS.Common.Component.Data.Response.DevicesConfig;
using iCMS.Frameworks.Core.DB.Models;
using iCMS.Common.Component.Data.Response.Common;
using iCMS.Common.Component.Data.Response.DiagnosticControl;
using iCMS.Common.Component.Data.Request.DiagnosticControl;

namespace iCMS.Service.Web.DevicesConfig
{
    public interface IDevicesConfigManager
    {
        #region 设备树设置

        /// <summary>
        /// 创建人：王龙杰
        /// 创建时间：2017-11-10
        /// 创建记录：获取设备下拉列表
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<GetDeviceSelectListResult> GetDeviceSelectList(GetDeviceSelectListParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-31
        /// 创建记录：获取设备类型数据信息
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<DeviceTypeInfoResult> GetDeviceTypeForDeviceTree(BaseRequest parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：获取测量位置类型数据信息
        /// </summary>
        /// <param name="parameter">获取测量位置类型数据信息参数</param>
        /// <returns></returns>
        BaseResponse<MSiteTypeInfoResult> GetMSiteTypeForDeviceTree(MSiteTypeForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-10-31
        /// 创建记录：获取测量位置监测类型数据信息
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<MSMTTypeInfoResult> GetMSMTTypeForDeviceTree(BaseRequest parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：获取振动信号类型数据信息
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        BaseResponse<VibSignalTypeForDeviceTreeResult> GetVibSignalTypeForDeviceTree(BaseRequest parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：获取波形属性(波长，上限频率，下限频率)类型数据信息
        /// </summary>
        /// <param name="parameter">获取波形属性(波长，上限频率，下限频率)类型数据信息参数</param>
        /// <returns></returns>
        BaseResponse<WaveAttrTypeForDeviceTreeResult> GetWaveAttrTypeForDeviceTree(WaveAttrTypeForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：获取特征值类型数据信息
        /// </summary>
        /// <param name="parameter">获取特征值类型数据信息参数</param>
        /// <returns></returns>
        BaseResponse<EigenvalueTypeForDeviceTreeResult> GetEigenvalueTypeForDeviceTree(EigenvalueTypeForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：获取设备和测量位置数据信息
        /// </summary>
        /// <param name="parameter">获取设备和测量位置数据信息参数</param>
        /// <returns></returns>
        BaseResponse<DeviceAndMSiteDataForDeviceTreeResult> GetDeviceAndMSiteDataForDeviceTree(DeviceAndMSiteDataForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：获取设备和测量位置数据信息
        /// </summary>
        /// <param name="parameter">获取设备和测量位置数据信息参数</param>
        /// <returns></returns>
        BaseResponse<GetDeviceAndMSiteDataByDevIdForDeviceTreeResult> GetDeviceAndMSiteDataByDevIdForDeviceTree(DeviceAndMSiteDataByDevIdForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：获取测量位置报警和振动信号数据信息
        /// </summary>
        /// <param name="parameter">获取测量位置报警和振动信号数据信息参数</param>
        /// <returns></returns>
        BaseResponse<MSiteAlmAndSignalDataForDeviceTreeResult> GetMSiteAlmAndSignalDataForDeviceTree(MSiteAlmAndSignalDataForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：获取振动信号报警配置数据信息
        /// </summary>
        /// <param name="parameter">获取振动信号报警配置数据信息参数</param>
        /// <returns></returns>
        BaseResponse<SignalAlmDataForDeviceTreeResult> GetSignalAlmDataForDeviceTree(SignalAlmDataForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：添加设备数据信息
        /// </summary>
        /// <param name="parameter">添加设备数据信息参数</param>
        /// <returns></returns>
        BaseResponse<ResponseResult> AddDeviceRecordForDeviceTree(AddDeviceRecordForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：添加测量位置数据信息
        /// </summary>
        /// <param name="parameter">添加测量位置数据信息参数</param>
        /// <returns></returns>
        BaseResponse<MSiteRecordForDeviceTreeResult> AddMSiteRecordForDeviceTree(AddMSiteRecordForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：添加振动信号数据信息
        /// </summary>
        /// <param name="parameter">添加振动信号数据信息参数</param>
        /// <returns></returns>
        BaseResponse<VibSignalRecordForDeviceTreeResult> AddVibSignalRecordForDeviceTree(AddVibSignalRecordForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-01
        /// 创建记录：添加测量位置报警配置及报警值信息
        /// </summary>
        /// <param name="parameter">添加测量位置报警配置及报警值信息参数</param>
        /// <returns></returns>
        BaseResponse<MSiteAlmRecordForDeviceTreeResult> AddMSiteAlmRecordForDeviceTree(AddMSiteAlmRecordForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：添加特征值及报警值信息
        /// </summary>
        /// <param name="parameter">添加特征值及报警值信息参数</param>
        /// <returns></returns>
        BaseResponse<SignalAlmRecordForDeviceTreeResult> AddSignalAlmRecordForDeviceTree(AddSignalAlmRecordForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：编辑设备信息
        /// </summary>
        /// <param name="parameter">编辑设备信息参数</param>
        /// <returns></returns>
        BaseResponse<bool> EditDeviceRecordForDeviceTree(EditDeviceRecordForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：编辑测量位置信息
        /// </summary>
        /// <param name="parameter">编辑测量位置信息参数</param>
        /// <returns></returns>
        BaseResponse<bool> EditMSiteRecordForDeviceTree(EditMSiteRecordForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：编辑测量位置报警配置及报警值信息
        /// </summary>
        /// <param name="parameter">编辑测量位置报警配置及报警值信息参数</param>
        /// <returns></returns>
        BaseResponse<bool> EditMSiteAlmRecordForDeviceTree(EditMSiteAlmRecordForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：编辑振动信号信息
        /// </summary>
        /// <param name="parameter">编辑振动信号信息参数</param>
        /// <returns></returns>
        BaseResponse<bool> EditVibSignalRecordForDeviceTree(EditVibSignalRecordForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：编辑特征值及报警值信息
        /// </summary>
        /// <param name="parameter">编辑特征值及报警值信息参数</param>
        /// <returns></returns>
        BaseResponse<bool> EditSignalAlmRecordForDeviceTree(EditSignalAlmRecordForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：删除设备信息
        /// </summary>
        /// <param name="parameter">删除设备信息参数</param>
        /// <returns></returns>
        BaseResponse<bool> DeleteDeviceRecordForDeviceTree(DeleteDeviceRecordForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：删除测量位置信息
        /// </summary>
        /// <param name="parameter">删除测量位置信息信息参数</param>
        /// <returns></returns>
        BaseResponse<bool> DeleteMSiteRecordForDeviceTree(DeleteMSiteRecordForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：删除振动信号信息
        /// </summary>
        /// <param name="parameter">删除振动信号信息参数</param>
        /// <returns></returns>
        BaseResponse<bool> DeteleVibSignalRecordForDeviceTree(DeteleVibSignalRecordForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-11-02
        /// 创建记录：删除测量位置报警配置及报警值信息
        /// </summary>
        /// <param name="parameter">删除测量位置报警配置及报警值信息参数</param>
        /// <returns></returns>
        BaseResponse<bool> DeleteMSiteAlmRecordForDeviceTree(DeleteMSiteAlmRecordForDeviceTreeParameter parameter);

        /// <summary>
        /// 获取特征值删除接口
        /// </summary>
        /// <returns></returns>
        BaseResponse<bool> DeleteSignalAlmRecordForDeviceTree(DeleteSignalAlmParameter param);

        /// <summary>
        /// 复制单个测点的测量定义
        /// </summary>
        /// <returns></returns>
        BaseResponse<CopyMSResult> CopySingleMS(int SourceMSId, int TargetDevId, int TargetMsName, int? WSID = 0);

        /// <summary>
        /// 复制设备下所有的测量定义
        /// </summary>
        /// <returns></returns>
        BaseResponse<CopyMSResult> CopyAllMS(int TargetDevId, string MSAndWSStr);

        /// <summary>
        /// 设备切换
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        BaseResponse<bool> ChangeDevUsedType(ChangeDevUsedTypeParameter param);

        /// <summary>
        /// 获取触发式上传，触发值为空的测量定义
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        BaseResponse<TriggerUploadResult> GetTriggerUploadDataByMeasureSites(GetTriggerUploadDataParameter param);

        #endregion

        #region 监测树设置

        /// <summary>
        /// 获取监测树类型数据
        /// </summary>
        /// <returns></returns>
        BaseResponse<MonitorTreeTypeDataResult> QueryMonitorTreeType();
        /// <summary>
        /// 获取监测树数据 
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        BaseResponse<MonitorTreeResult> GetMonitorTreeDataInfo(MonitorTreeDatasParameter Parameter);
        /// <summary>
        /// 添加监测树       
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        BaseResponse<AddMonitorTreeResult> AddMonitorTree(AddMonitorTreeParameter Parameter);
        /// <summary>
        /// 编辑监测树
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> EditMonitorTree(EditMonitorTreeParameter Parameter);
        /// <summary>
        /// 删除监测树
        /// </summary>
        /// <param name="Parameter"></param>
        /// <returns></returns>
        BaseResponse<bool> DeleteMonitorTree(DelteMonitorTreeParameter Parameter);


        #region 对应监测树类型是否有数据
        /// <summary>
        /// 对应监测树类型是否有数据
        /// </summary>
        /// <param name="parameter">参数</param>
        BaseResponse<IsExistMonitorTreeDataByTypeResult> IsExistMonitorTreeDataByType(IsExistMonitorTreeDataByTypeParameter parameter);
        #endregion

        #region 根据传入监测树类型，获取系统默认最高级别监测树节点，且返回节点均存在子节点
        /// <summary>
        /// 根据传入监测树类型，获取系统默认最高级别监测树节点，且返回节点均存在子节点
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<GetFullMonitorTreeDataByTypeResult> GetFullMonitorTreeDataByType(GetFullMonitorTreeDataByTypeParameter parameter);
        #endregion

        #region 获取网关下的传感器
        /// <summary>
        /// 获取网关下的传感器
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<GetWSDataByWGIDResult> GetWSDataByWGID(GetWSDataByWGIDParameter parameter);
        #endregion

        #region 获取测量位置详细信息
        /// <summary>
        /// 获取测量位置详细信息
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<GetMeasureSiteDetailInfoResult> GetMeasureSiteDetailInfo(GetMeasureSiteDetailInfoParameter parameter);
        #endregion

        #region 编辑临时测量定义
        /// <summary>
        ///创建人:王颖辉
        ///创建时间:2017-10-10
        /// 创建内容：编辑临时测量定义
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<ResponseResult> EditTemporaryVib(EditTemporaryVibParameter parameter);

        #endregion

        #endregion

        #region 获取不同设备统计信息
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-12
        /// 创建内容:获取不同设备统计信息
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<GetDeviceRuningTypeCountResult> GetDeviceRuningTypeCount(GetDeviceRuningTypeCountParameter parameter);
        #endregion

        #region 获取近6个月不同报警类型的设备统计
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-12
        /// 创建内容:获取近6个月不同报警类型的设备统计
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<Get6MonthAlarmTypeDeviceCountParameterResult> Get6MonthAlarmTypeDeviceCount(Get6MonthAlarmTypeDeviceCountParameter parameter);
        #endregion

        #region 获取某监测树下设备状态统计
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-12
        /// 创建内容:获取某监测树下设备状态统计
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<GetDeviceStatusStatisticResult> GetDeviceStatusStatistic(GetDeviceStatusStatisticParameter parameter);
        #endregion

        #region 获取某监测树下所有设备状态
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-13
        /// 创建内容:获取某监测树下所有设备状态
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<GetDeviceStatusStatisticByMonitroIDResult> GetDeviceStatusStatisticByMonitroID(GetDeviceStatusStatisticByMonitroIDParameter parameter);
        #endregion

        #region 获取用户所管理传感器连接状态统计
        /// <summary>
        /// 创建人:王颖辉
        /// 创建时间:2017-10-12
        /// 创建内容:获取用户所管理传感器连接状态统计
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<GetWSStatusCountParameterResult> GetWSStatusCount(GetWSStatusCountParameter parameter);
        #endregion

        #region 获取WG连接状态统计
        /// <summary>
        /// 创建人：王颖辉 
        /// 创建时间：2017-10-16
        /// 创建内容：获取WG连接状态统计
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<GetWGLinkStatusCountResult> GetWGLinkStatusCount(GetWGLinkStatusCountParameter parameter);
        #endregion

        #region 获取用户所管理传感器在近6个月内，不同报警类型下设备总数
        /// <summary>
        /// 创建人：王颖辉 
        /// 创建时间：2017-10-16
        /// 创建内容：获取用户所管理传感器在近6个月内，不同报警类型下设备总数
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<GetSixMonthAlarmTypeWSCountResult> GetSixMonthAlarmTypeWSCount(GetSixMonthAlarmTypeWSCountParameter parameter);
        #endregion

        #region 获取用户管理的WS和网关信息
        /// <summary>
        /// 创建人：王颖辉 
        /// 创建时间：2017-10-16
        /// 创建内容：获取用户管理的WS和网关信息
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<GetWSAndWGStatusInfoResult> GetWSAndWGStatusInfo(GetWSAndWGStatusInfoParameter parameter);

        #endregion

        #region 主备切换时获取设备列表信息
        /// <summary>
        /// 主备切换时获取设备列表信息
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<GetMainStandbyDeviceListByDeviceIdResult> GetMainStandbyDeviceListByDeviceId(GetMainStandbyDeviceListByDeviceIdParameter parameter);
        #endregion

        #region 通过设备ID获取设备上所有的未挂靠转速的非转速测点

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：通过设备ID获取设备上所有的未挂靠转速的非转速测点
        /// </summary>
        /// <param name="parameter">获取设备上所有的未挂靠转速的非转速测点参数</param>
        BaseResponse<GetMSiteDataByDevIDForDeviceTreeResult> GetMSiteDataByDevIDForDeviceTree(GetMSiteDataByDevIDForDeviceTreeParameter parameter);

        #endregion

        #region 获取用户管理网关、采集单元下拉列表
        BaseResponse<GetWGMaintainListResult> GetWGMaintainList(GetWGMaintainListParameter param);
        #endregion

        #region 转速测点相关接口

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：获取转速测量位置详细信息
        /// </summary>
        /// <param name="parameter">获取转速测量位置详细信息参数</param>
        /// <returns></returns>
        BaseResponse<GetSpeedMSiteForDeviceTreeResult> GetSpeedMSiteForDeviceTree(GetSpeedMSiteForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：添加转速测量位置
        /// </summary>
        /// <param name="parameter">添加转速测量位置参数</param>
        /// <returns></returns>
        BaseResponse<bool> AddSpeedMSiteForDeviceTree(AddSpeedMSiteForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：编辑转速测量位置
        /// </summary>
        /// <param name="parameter">编辑转速测量位置参数</param>
        /// <returns></returns>
        BaseResponse<bool> EditSpeedMSiteForDeviceTree(EditSpeedMSiteForDeviceTreeParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：复制转速测量位置
        /// </summary>
        /// <param name="parameter">复制转速测量位置参数</param>
        /// <returns></returns>
        BaseResponse<CopyAllSpeedMSResult> CopyAllSpeedMS(CopyAllSpeedMSParameter parameter);

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-08
        /// 创建记录：通过设备ID读取设备上的采集单元
        /// </summary>
        /// <param name="parameter">通过设备ID读取设备上的采集单元参数</param>
        /// <returns></returns>
        BaseResponse<GetDAUInfoByDevIDResult> GetDAUInfoByDevID(GetDAUInfoByDevIDParameter parameter);

        #endregion

        #region 获取用户管理传感器下拉列表
        BaseResponse<GetWSMaintainListResult> GetWSMaintainList(GetWSMaintainListParameter param);
        #endregion

        #region 获取采集单元信息

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2018-05-17
        /// 创建记录：获取采集单元信息
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        BaseResponse<List<MTDauResult>> GetDauRelation(GetDauRelationParameter parameter);

        #endregion
    }
}