/************************************************************************************
 * Copyright (c) @ILine All Rights Reserved.
 *命名空间：
 *文件名：  
 *创建人：  张辽阔
 *创建时间：2016.07.22
 *描述：字典表缓存
 **=================================================================================
 *修改记录
 *修改时间：2016年7月25日16:18:52
 *修改人： LF
 *修改原因：修改取得单例函数中递归调用BUG
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using iCMS.Frameworks.Core.DB.Models;
using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB
{
    /// <summary>
    /// 字典表缓存
    /// </summary>
    public class CacheDICT
    {
        private static CacheDICT M_Instance = null;
        private static readonly object M_S_Lock = new object();
        private static readonly object M_DICT_Lock = new object();

        /// <summary>
        /// 取得单例
        /// </summary>
        /// <param name="dataContext">增加该参数，防止事务死锁</param>
        /// <returns></returns>
        public static CacheDICT GetInstance(iCMSDbContext dataContext = null)
        {
            if (M_Instance == null)
            {
                lock (M_S_Lock)
                {
                    if (M_Instance == null)
                    {
                        M_Instance = new CacheDICT();
                    }
                }
            }
            if (HttpRuntime.Cache["CacheDICTKey"] == null)
            {
                M_Instance.InitCacheType(dataContext);
            }
            return M_Instance;
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetCacheType<TEntity>() where TEntity : EntityBase
        {
            Dictionary<string, IEnumerable<EntityBase>> Temp_Dict = HttpRuntime.Cache["CacheDICTKey"] as Dictionary<string, IEnumerable<EntityBase>>;
            if (Temp_Dict == null)
            {
                return new List<TEntity>();
            }
            else
            {
                return Temp_Dict[typeof(TEntity).FullName] as IEnumerable<TEntity>;
            }
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetCacheType<TEntity>(Func<TEntity, bool> predicate) where TEntity : EntityBase
        {
            if (predicate == null)
            {
                return new List<TEntity>();
            }
            Dictionary<string, IEnumerable<EntityBase>> Temp_Dict = HttpRuntime.Cache["CacheDICTKey"] as Dictionary<string, IEnumerable<EntityBase>>;
            if (Temp_Dict == null)
            {
                return new List<TEntity>();
            }
            else
            {
                var Result = Temp_Dict[typeof(TEntity).FullName] as IEnumerable<TEntity>;
                if (Result == null)
                {
                    return new List<TEntity>();
                }
                return Result.Where(predicate);
            }
        }

        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="dataContext">增加该参数，防止事务死锁</param>
        public void UpdateCacheType<TEntity>(iCMSDbContext dataContext = null) where TEntity : EntityBase
        {
            lock (M_DICT_Lock)
            {
                //连接状态类型
                if (typeof(TEntity) == typeof(ConnectStatusType))
                {
                    if (dataContext == null)
                        UpdateCacheType<ConnectStatusType>(DBServices.GetInstance().ConnectStatusTypeProxy.GetDatas<ConnectStatusType>(p => true, false));
                    else
                        UpdateCacheType<ConnectStatusType>(dataContext.ConnectStatusType.ToList().AsQueryable());
                }
                //设备类型
                else if (typeof(TEntity) == typeof(DeviceType))
                {
                    if (dataContext == null)
                        UpdateCacheType<DeviceType>(DBServices.GetInstance().DeviceTypeProxy.GetDatas<DeviceType>(p => true, false));
                    else
                        UpdateCacheType<DeviceType>(dataContext.DeviceType.ToList().AsQueryable());
                }
                //特征值类型
                else if (typeof(TEntity) == typeof(EigenValueType))
                {
                    if (dataContext == null)
                        UpdateCacheType<EigenValueType>(DBServices.GetInstance().EigenValueTypeProxy.GetDatas<EigenValueType>(p => true, false));
                    else
                        UpdateCacheType<EigenValueType>(dataContext.EigenValueType.ToList().AsQueryable());
                }
                //测量位置监测类型
                else if (typeof(TEntity) == typeof(MeasureSiteMonitorType))
                {
                    if (dataContext == null)
                        UpdateCacheType<MeasureSiteMonitorType>(DBServices.GetInstance().MeasureSiteMonitorTypeProxy.GetDatas<MeasureSiteMonitorType>(p => true, false));
                    else
                        UpdateCacheType<MeasureSiteMonitorType>(dataContext.MeasureSiteMonitorType.ToList().AsQueryable());
                }
                //测量位置类型
                else if (typeof(TEntity) == typeof(MeasureSiteType))
                {
                    if (dataContext == null)
                        UpdateCacheType<MeasureSiteType>(DBServices.GetInstance().MeasureSiteTypeProxy.GetDatas<MeasureSiteType>(p => true, false));
                    else
                        UpdateCacheType<MeasureSiteType>(dataContext.MeasureSiteType.ToList().AsQueryable());
                }
                //监测树类型
                else if (typeof(TEntity) == typeof(MonitorTreeType))
                {
                    if (dataContext == null)
                        UpdateCacheType<MonitorTreeType>(DBServices.GetInstance().MonitorTreeTypeProxy.GetDatas<MonitorTreeType>(p => true, false));
                    else
                        UpdateCacheType<MonitorTreeType>(dataContext.MonitorTreeType.ToList().AsQueryable());
                }
                //传感器类型
                else if (typeof(TEntity) == typeof(SensorType))
                {
                    if (dataContext == null)
                        UpdateCacheType<SensorType>(DBServices.GetInstance().SensorTypeProxy.GetDatas<SensorType>(p => true, false));
                    else
                        UpdateCacheType<SensorType>(dataContext.SensorType.ToList().AsQueryable());
                }
                //振动信号类型
                else if (typeof(TEntity) == typeof(VibratingSingalType))
                {
                    if (dataContext == null)
                        UpdateCacheType<VibratingSingalType>(DBServices.GetInstance().VibratingSingalTypeProxy.GetDatas<VibratingSingalType>(p => true, false));
                    else
                        UpdateCacheType<VibratingSingalType>(dataContext.VibratingSingalType.ToList().AsQueryable());
                }
                //波形长度数值
                else if (typeof(TEntity) == typeof(WaveLengthValues))
                {
                    if (dataContext == null)
                        UpdateCacheType<WaveLengthValues>(DBServices.GetInstance().WaveLengthValuesProxy.GetDatas<WaveLengthValues>(p => true, false));
                    else
                        UpdateCacheType<WaveLengthValues>(dataContext.WaveLengthValues.ToList().AsQueryable());
                }
                //波形下限频率值
                else if (typeof(TEntity) == typeof(WaveLowerLimitValues))
                {
                    if (dataContext == null)
                        UpdateCacheType<WaveLowerLimitValues>(DBServices.GetInstance().WaveLowerLimitValuesProxy.GetDatas<WaveLowerLimitValues>(p => true, false));
                    else
                        UpdateCacheType<WaveLowerLimitValues>(dataContext.WaveLowerLimitValues.ToList().AsQueryable());
                }
                //波形上限频率值
                else if (typeof(TEntity) == typeof(WaveUpperLimitValues))
                {
                    if (dataContext == null)
                        UpdateCacheType<WaveUpperLimitValues>(DBServices.GetInstance().WaveUpperLimitValuesProxy.GetDatas<WaveUpperLimitValues>(p => true, false));
                    else
                        UpdateCacheType<WaveUpperLimitValues>(dataContext.WaveUpperLimitValues.ToList().AsQueryable());
                }
                //无线网关类型
                else if (typeof(TEntity) == typeof(WirelessGatewayType))
                {
                    if (dataContext == null)
                        UpdateCacheType<WirelessGatewayType>(DBServices.GetInstance().WirelessGatewayTypeProxy.GetDatas<WirelessGatewayType>(p => true, false));
                    else
                        UpdateCacheType<WirelessGatewayType>(dataContext.WirelessGatewayType.ToList().AsQueryable());
                }
            }
        }

        /// <summary>
        /// 更新缓存中的数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="CacheValue"></param>
        private void UpdateCacheType<TEntity>(IEnumerable<TEntity> CacheValue) where TEntity : EntityBase
        {
            var TEntity_Type = typeof(TEntity);
            Dictionary<string, IEnumerable<EntityBase>> Temp_Dict = HttpRuntime.Cache["CacheDICTKey"] as Dictionary<string, IEnumerable<EntityBase>>;
            if (Temp_Dict == null)
            {
                Temp_Dict = new Dictionary<string, IEnumerable<EntityBase>>();
                Temp_Dict[TEntity_Type.FullName] = CacheValue;
                HttpRuntime.Cache["CacheDICTKey"] = Temp_Dict;
            }
            else
            {
                Temp_Dict[TEntity_Type.FullName] = CacheValue;
            }
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-09-20
        /// 创建记录：初始化缓存
        /// </summary>
        /// <param name="dataContext">增加该参数，防止事务死锁</param>
        private void InitCacheType(iCMSDbContext dataContext = null)
        {
            lock (M_DICT_Lock)
            {
                Dictionary<string, IEnumerable<EntityBase>> Temp_Dict = HttpRuntime.Cache["CacheDICTKey"] as Dictionary<string, IEnumerable<EntityBase>>;
                if (Temp_Dict == null)
                {
                    Temp_Dict = new Dictionary<string, IEnumerable<EntityBase>>();
                    //连接状态类型
                    if (dataContext == null)
                        Temp_Dict[typeof(ConnectStatusType).FullName] = DBServices.GetInstance().ConnectStatusTypeProxy
                            .GetDatas<ConnectStatusType>(p => true, false);
                    else
                        Temp_Dict[typeof(ConnectStatusType).FullName] = dataContext.ConnectStatusType
                            .ToList().AsQueryable();
                    //设备类型
                    if (dataContext == null)
                        Temp_Dict[typeof(DeviceType).FullName] = DBServices.GetInstance().DeviceTypeProxy
                            .GetDatas<DeviceType>(p => true, false);
                    else
                        Temp_Dict[typeof(DeviceType).FullName] = dataContext.DeviceType
                            .ToList().AsQueryable();
                    //特征值类型
                    if (dataContext == null)
                        Temp_Dict[typeof(EigenValueType).FullName] = DBServices.GetInstance().EigenValueTypeProxy
                            .GetDatas<EigenValueType>(p => true, false);
                    else
                        Temp_Dict[typeof(EigenValueType).FullName] = dataContext.EigenValueType
                            .ToList().AsQueryable();
                    //测量位置监测类型
                    if (dataContext == null)
                        Temp_Dict[typeof(MeasureSiteMonitorType).FullName] = DBServices.GetInstance().MeasureSiteMonitorTypeProxy
                            .GetDatas<MeasureSiteMonitorType>(p => true, false);
                    else
                        Temp_Dict[typeof(MeasureSiteMonitorType).FullName] = dataContext.MeasureSiteMonitorType
                            .ToList().AsQueryable();
                    //测量位置类型
                    if (dataContext == null)
                        Temp_Dict[typeof(MeasureSiteType).FullName] = DBServices.GetInstance().MeasureSiteTypeProxy
                            .GetDatas<MeasureSiteType>(p => true, false);
                    else
                        Temp_Dict[typeof(MeasureSiteType).FullName] = dataContext.MeasureSiteType
                            .ToList().AsQueryable();
                    //监测树类型
                    if (dataContext == null)
                        Temp_Dict[typeof(MonitorTreeType).FullName] = DBServices.GetInstance().MonitorTreeTypeProxy
                            .GetDatas<MonitorTreeType>(p => true, false);
                    else
                        Temp_Dict[typeof(MonitorTreeType).FullName] = dataContext.MonitorTreeType
                            .ToList().AsQueryable();
                    //传感器类型
                    if (dataContext == null)
                        Temp_Dict[typeof(SensorType).FullName] = DBServices.GetInstance().SensorTypeProxy
                            .GetDatas<SensorType>(p => true, false);
                    else
                        Temp_Dict[typeof(SensorType).FullName] = dataContext.SensorType
                            .ToList().AsQueryable();
                    //振动信号类型
                    if (dataContext == null)
                        Temp_Dict[typeof(VibratingSingalType).FullName] = DBServices.GetInstance().VibratingSingalTypeProxy
                            .GetDatas<VibratingSingalType>(p => true, false);
                    else
                        Temp_Dict[typeof(VibratingSingalType).FullName] = dataContext.VibratingSingalType
                            .ToList().AsQueryable();
                    //波形长度数值
                    if (dataContext == null)
                        Temp_Dict[typeof(WaveLengthValues).FullName] = DBServices.GetInstance().WaveLengthValuesProxy
                            .GetDatas<WaveLengthValues>(p => true, false);
                    else
                        Temp_Dict[typeof(WaveLengthValues).FullName] = dataContext.WaveLengthValues
                            .ToList().AsQueryable();
                    //波形下限频率值
                    if (dataContext == null)
                        Temp_Dict[typeof(WaveLowerLimitValues).FullName] = DBServices.GetInstance().WaveLowerLimitValuesProxy
                            .GetDatas<WaveLowerLimitValues>(p => true, false);
                    else
                        Temp_Dict[typeof(WaveLowerLimitValues).FullName] = dataContext.WaveLowerLimitValues
                            .ToList().AsQueryable();
                    //波形上限频率值
                    if (dataContext == null)
                        Temp_Dict[typeof(WaveUpperLimitValues).FullName] = DBServices.GetInstance().WaveUpperLimitValuesProxy
                            .GetDatas<WaveUpperLimitValues>(p => true, false);
                    else
                        Temp_Dict[typeof(WaveUpperLimitValues).FullName] = dataContext.WaveUpperLimitValues
                            .ToList().AsQueryable();
                    //无线网关类型
                    if (dataContext == null)
                        Temp_Dict[typeof(WirelessGatewayType).FullName] = DBServices.GetInstance().WirelessGatewayTypeProxy
                            .GetDatas<WirelessGatewayType>(p => true, false);
                    else
                        Temp_Dict[typeof(WirelessGatewayType).FullName] = dataContext.WirelessGatewayType
                            .ToList().AsQueryable();
                    HttpRuntime.Cache["CacheDICTKey"] = Temp_Dict;
                }
            }
        }
    }
}