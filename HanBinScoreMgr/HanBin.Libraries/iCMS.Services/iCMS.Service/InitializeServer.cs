/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Service.Web
 *文件名：  IInitializeServer
 *创建人：  王颖辉
 *创建时间：2016-11-24
 *描述：初始化数据
/************************************************************************************/
using System;
using System.Configuration;
using System.Linq;

using iCMS.Common.Component.Data.Enum;
using iCMS.Common.Component.Tool;
using iCMS.Frameworks.Core.DB;
using iCMS.Frameworks.Core.DB.Models;
using iCMS.Service.Web;
using iCMS.Frameworks.Core.Repository;

namespace iCMS.Service.Web
{
    #region 初始化数据
    /// <summary>
    /// 初始化数据
    /// </summary>
    public class InitializeServer : IInitializeServer, IDisposable
    {
        #region 变量
        private readonly IRepository<Module> moduleRepository;
        private readonly IRepository<Config> configRepository;
        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public InitializeServer(IRepository<Module> moduleRepository,
            IRepository<Config> configRepository)
        {
            this.moduleRepository = moduleRepository;
            this.configRepository = configRepository;
        }
        #endregion

        #region 初始化系统变量
        /// <summary>
        /// 初始化系统变量
        /// </summary>
        public void InitializeEnvironmentVariables()
        {
            bool isContinue = true;
            int num = 1;
            while (isContinue)
            {
                try
                {
                    Module module = moduleRepository
                        .GetDatas<Module>(p => true, false)
                        .Where(obj => obj.Code == System.Configuration.ConfigurationManager.AppSettings["DevStartStopFunc"].ToString())
                        .ToList()
                        .FirstOrDefault();
                    if (module.IsUsed == 1)
                        iCMS.Common.Component.Tool.ConstObject.availabilityCriticalValue = true;
                    else
                        iCMS.Common.Component.Tool.ConstObject.availabilityCriticalValue = false;

                    string condition = EnumHelper.GetDescription(EnumConfig.AlarmConfig);
                    Config rootConfig = configRepository
                        .GetDatas<Config>(p => true, false)
                        .Where(item => item.Name == condition
                            && item.ParentId == 0)
                        .FirstOrDefault();

                    if (rootConfig != null)
                    {
                        //二级节点信息,报警方式
                        condition = EnumHelper.GetDescription(EnumConfig.AlarmPatternConfig);
                        Config alarmPatternConfig = configRepository
                            .GetDatas<Config>(p => true, false)
                            .Where(item => item.Name == condition
                                && item.ParentId == rootConfig.ID)
                            .FirstOrDefault();
                        //根节点是否存在
                        if (alarmPatternConfig != null && alarmPatternConfig.Value == "1")
                            iCMS.Common.Component.Tool.ConstObject.AlarmsConfirmed = true;
                        else
                            iCMS.Common.Component.Tool.ConstObject.AlarmsConfirmed = false;

                        //二级节点信息,趋势报警
                        condition = EnumHelper.GetDescription(EnumConfig.TrendAlarmConfig);
                        Config trendAlarmConfig = configRepository
                            .GetDatas<Config>(p => true, false)
                            .Where(item => item.Name == condition
                                && item.ParentId == rootConfig.ID)
                            .FirstOrDefault();

                        //趋势报警状态修改由原来的Value修改为IsUsed 王颖辉 2017-02-21 [张阳提出]
                        if (trendAlarmConfig != null && trendAlarmConfig.IsUsed == 1)
                            iCMS.Common.Component.Tool.ConstObject.TrendAlarms = true;
                        else
                            iCMS.Common.Component.Tool.ConstObject.TrendAlarms = false;

                        try
                        {
                            float.TryParse(ConfigurationManager.AppSettings["TrendAlarmsSet"].ToString(),
                                out iCMS.Common.Component.Tool.ConstObject.TrendAlarmsPercentage);
                        }
                        catch
                        {
                            iCMS.Common.Component.Tool.ConstObject.TrendAlarmsPercentage = 0.2F;
                        }
                    }
                    isContinue = false;
                    LogHelper.WriteLog("初始环境变量成功");
                    break;
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(ex);
                    LogHelper.WriteLog("初始环境变量失败,5秒后第" + num + "次尝试");
                    num++;
                    System.Threading.Thread.Sleep(5000);
                }
            }
        }
        #endregion

        #region 释放资源
        public void Dispose()
        {

        }
        #endregion
    }
    #endregion
}