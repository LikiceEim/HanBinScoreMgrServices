/***********************************************************************
 *Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.Service.Utility
 *文件名：  UtilityManager
 *创建人：  QXM
 *创建时间：2016/10/28 10:10:19
 *描述：服务表现层，响应调用方对用户、权限组、日志的操作请求
 *
 *修改人：张辽阔
 *修改时间：2016-11-15
 *描述：增加错误编码
 *=====================================================================**/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Enum;
using iCMS.Common.Component.Tool;
using iCMS.Frameworks.Core.DB.Models;
using iCMS.Common.Component.Data.Response.Statistics;
using iCMS.Service.Common;
using iCMS.Common.Component.Data.Response.Utility;
using iCMS.Common.Component.Data.Response.DiagnosticAnalysis;
using iCMS.Common.Component.Data.Request.Utility;
using iCMS.Frameworks.Core.Repository;
using iCMS.Common.Component.Data.Response.DevicesConfig;
using Microsoft.Practices.Unity;
using System.Data.SqlClient;

namespace iCMS.Service.Web.Utility
{
    #region 公共管理
    /// <summary>
    /// 公共管理
    /// </summary>
    public class UtilityManager : IUtilityManager
    {
        #region 私有变量 Added by QXM, 2016/11/22, APPLY DI
        private readonly IRepository<MonitorTree> monitorRepository = null;
        private readonly IRepository<Gateway> wgRepository = null;
        private readonly IRepository<WS> wsRepository = null;
        private readonly IRepository<Device> deviceRepository = null;
        private readonly IRepository<User> userRepository = null;
        private readonly IRepository<Role> roleRepository = null;
        private readonly IRepository<Module> moduleRepository = null;
        private readonly IRepository<Bearing> bearingRepository = null;
        private readonly IRepository<Factory> factoryRepository = null;
        private readonly IRepository<RoleModule> roleModuleRepository = null;
        private readonly IRepository<MeasureSite> measureSiteRepository = null;
        private readonly IRepository<Image> imageRepository = null;
        private readonly IRepository<HelpDocument> helpDocumentRepository = null;

        [Dependency]
        public IRepository<UserRalationDevice> userRalationDeviceRepository
        {
            get;
            set;
        }


        [Dependency]
        public ICacheDICT cacheDICT
        {
            get;
            set;
        }

        public UtilityManager(IRepository<MonitorTree> monitorRepository,
                              IRepository<Gateway> wgRepository,
                              IRepository<WS> wsRepository,
                              IRepository<Device> deviceRepository,
                              IRepository<User> userRepository,
                              IRepository<Role> roleRepository,
                              IRepository<Module> moduleRepository,
                              IRepository<Bearing> bearingRepository,
                              IRepository<Factory> factoryRepository,
                              IRepository<RoleModule> roleModuleRepository,
                              IRepository<MeasureSite> measureSiteRepository,
                              IRepository<Image> imageRepository,
                              IRepository<HelpDocument> helpDocumentRepository)
        {
            this.monitorRepository = monitorRepository;
            this.wgRepository = wgRepository;
            this.wsRepository = wsRepository;
            this.deviceRepository = deviceRepository;
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.moduleRepository = moduleRepository;
            this.bearingRepository = bearingRepository;
            this.factoryRepository = factoryRepository;
            this.roleModuleRepository = roleModuleRepository;
            this.measureSiteRepository = measureSiteRepository;
            this.imageRepository = imageRepository;
            this.helpDocumentRepository = helpDocumentRepository;
        }
        #endregion

        #region 是否新值
        /// <summary>
        /// 是否新值
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<NewValueExistedResult> IsNewValueExisted(IsNewValueExistedParameter param)
        {
            //Int	表名
            //T_SYS_MONITOR_TREE：1
            //T_ SYS_WG：2
            //T_ SYS_WS：3
            //T_SYS_DEVICE：4
            //T_ SYS_USER：5
            //T_ SYS_ROLE：6
            //T_ SYS_MODULE：7
            //T_SYS_BEARING:8
            //T_SYS_FACTORY:9
            //String	字段名
            //监测树配置：名称
            //无限网关：网关编号，网关名称
            //无线传感器：SN,MAC地址
            //设备树管理：设备名称，设备编号
            //用户管理：用户名
            //权限功能：模组名称,code
            //角色管理：角色名称
            BaseResponse<NewValueExistedResult> response = new BaseResponse<NewValueExistedResult>();
            NewValueExistedResult result = new NewValueExistedResult();
            int? OID = param.OID;
            string FactoryID = param.FactoryID;
            string NewValue = param.NewValue;
            int Table = param.Table;
            string Name = param.Name;

            bool isExisted = false;

            try
            {
                if (!OID.HasValue)//新增操作
                {
                    switch (Table)
                    {
                        case 1:
                            isExisted = monitorRepository
                                .GetDatas<MonitorTree>(t => t.Name.Trim().Equals(NewValue.Trim()), false)
                                .Any();
                            break;
                        case 2:
                            if (Name.Equals("WGName"))
                            {
                                isExisted = wgRepository
                                    .GetDatas<Gateway>(t => t.WGName.Trim().Equals(NewValue.Trim()), false)
                                    .Any();
                            }
                            else if (Name.Equals("WGNO"))
                            {
                                int wgNo = Convert.ToInt32(NewValue);
                                isExisted = wgRepository
                                    .GetDatas<Gateway>(t => t.WGNO == wgNo, false)
                                    .Any();
                            }
                            else if (Name.Equals("AgentAddress"))
                            {
                                isExisted = wgRepository
                                    .GetDatas<Gateway>(t => t.AgentAddress.Equals(NewValue), false)
                                    .Any();
                            }
                            break;
                        case 3:
                            if (Name.Equals("WSName"))
                            {
                                isExisted = wsRepository
                                    .GetDatas<WS>(t => t.WSName.Trim() == NewValue.Trim(), false)
                                    .Any();
                            }
                            if (Name.Equals("MACADDR"))
                            {
                                isExisted = wsRepository
                                    .GetDatas<WS>(t => t.MACADDR.Trim() == NewValue.Trim(), false)
                                    .Any();
                            }
                            break;
                        case 4:
                            if (Name.Equals("DevName"))
                            {
                                isExisted = deviceRepository
                                    .GetDatas<Device>(t => t.DevName.Trim().Equals(NewValue.Trim()), false)
                                    .Any();
                            }
                            else if (Name.Equals("DevNO"))
                            {
                                int newDevNO = Convert.ToInt32(NewValue);
                                isExisted = deviceRepository
                                    .GetDatas<Device>(t => t.DevNO == newDevNO, false)
                                    .Any();
                            }
                            break;
                        case 5:
                            if (Name.Equals("UserName"))
                            {
                                isExisted = userRepository
                                    .GetDatas<User>(t => t.IsDeleted == false
                                        && t.UserName.Trim().Equals(NewValue.Trim()), false)
                                    .Any();
                            }
                            if (Name.Equals("AccountName"))
                            {
                                isExisted = userRepository
                                    .GetDatas<User>(t => t.IsDeleted == false
                                        && t.AccountName.Trim().Equals(NewValue.Trim()), false)
                                    .Any();
                            }
                            break;
                        case 6:
                            isExisted = roleRepository
                                .GetDatas<Role>(t => t.RoleName.Trim().Equals(NewValue.Trim()), false)
                                .Any();
                            break;
                        case 7:
                            if (Name.Equals("ModuleName"))
                            {
                                isExisted = moduleRepository
                                    .GetDatas<Module>(t => t.ModuleName.Trim().Equals(NewValue.Trim()), false)
                                    .Any();
                            }
                            if (Name.Equals("Code"))
                            {
                                isExisted = moduleRepository
                                    .GetDatas<Module>(t => t.Code.Trim().Equals(NewValue.Trim()), false)
                                    .Any();
                            }
                            break;
                        case 8:
                            if (Name.Equals("BearingNum"))
                            {
                                isExisted = bearingRepository
                                    .GetDatas<Bearing>(t => t.BearingNum.Trim().Equals(NewValue.Trim()), false)
                                    .Any();
                            }
                            break;
                        case 9:
                            if (Name.Equals("FactoryID"))
                            {
                                isExisted = factoryRepository
                                    .GetDatas<Factory>(t => t.FactoryID.Trim().Equals(NewValue.Trim()), false)
                                    .Any();
                            }
                            if (Name.Equals("FactoryName"))
                            {
                                isExisted = factoryRepository
                                    .GetDatas<Factory>(t => t.FactoryName.Trim().Equals(NewValue.Trim()), false)
                                    .Any();
                            }
                            break;
                    }
                }
                else//更新操作 
                {
                    switch (Table)
                    {
                        case 1:
                            isExisted = monitorRepository
                                .GetDatas<MonitorTree>(t => t.MonitorTreeID != OID && t.Name.Trim().Equals(NewValue), false)
                                .Any();
                            break;
                        case 2:
                            if (Name.Equals("WGName"))
                            {
                                isExisted = wgRepository
                                    .GetDatas<Gateway>(t => t.WGID != OID && t.WGName.Trim().Equals(NewValue.Trim()), false)
                                    .Any();
                            }
                            else if (Name.Equals("WGNO"))
                            {
                                int wgNo = Convert.ToInt32(NewValue);
                                isExisted = wgRepository
                                    .GetDatas<Gateway>(t => t.WGID != OID && t.WGNO == wgNo, false)
                                    .Any();
                            }
                            else if (Name.Equals("AgentAddress"))
                            {
                                isExisted = wgRepository
                                    .GetDatas<Gateway>(t => t.WGID != OID && t.AgentAddress.Equals(NewValue), false)
                                    .Any();
                            }
                            break;
                        case 3:
                            if (Name.Equals("WSName"))
                            {
                                isExisted = wsRepository
                                    .GetDatas<WS>(t => t.WSID != OID && t.WSName.Trim() == NewValue.Trim(), false)
                                    .Any();
                            }
                            if (Name.Equals("MACADDR"))
                            {
                                isExisted = wsRepository
                                    .GetDatas<WS>(t => t.WSID != OID && t.MACADDR.Trim() == NewValue.Trim(), false)
                                    .Any();
                            }
                            break;
                        case 4:
                            if (Name.Equals("DevName"))
                            {
                                isExisted = deviceRepository
                                    .GetDatas<Device>(t => t.DevID != OID && t.DevName.Trim().Equals(NewValue.Trim()), false)
                                    .Any();
                            }
                            else if (Name.Equals("DevNO"))
                            {
                                int newDevNO = Convert.ToInt32(NewValue);
                                isExisted = deviceRepository
                                    .GetDatas<Device>(t => t.DevID != OID && t.DevNO == newDevNO, false)
                                    .Any();
                            }
                            break;
                        case 5:
                            if (Name.Equals("UserName"))
                            {
                                isExisted = userRepository
                                    .GetDatas<User>(t => t.IsDeleted == false
                                        && t.UserID != OID
                                        && t.UserName.Trim().Equals(NewValue.Trim()), false)
                                    .Any();
                            }
                            if (Name.Equals("AccountName"))
                            {
                                isExisted = userRepository
                                    .GetDatas<User>(t => t.IsDeleted == false
                                        && t.UserID != OID
                                        && t.AccountName.Trim().Equals(NewValue.Trim()), false)
                                    .Any();
                            }

                            break;
                        case 6:
                            isExisted = roleRepository
                                .GetDatas<Role>(t => t.RoleID != OID && t.RoleName.Trim().Equals(NewValue.Trim()), false)
                                .Any();
                            break;
                        case 7:
                            if (Name.Equals("ModuleName"))
                            {
                                isExisted = moduleRepository
                                    .GetDatas<Module>(t => t.ModuleID != OID && t.ModuleName.Trim().Equals(NewValue.Trim()), false)
                                    .Any();
                            }
                            if (Name.Equals("Code"))
                            {
                                isExisted = moduleRepository
                                    .GetDatas<Module>(t => t.ModuleID != OID && t.Code.Trim().Equals(NewValue.Trim()), false)
                                    .Any();
                            }
                            break;
                        case 8:
                            if (Name.Equals("BearingNum"))
                            {
                                isExisted = bearingRepository
                                    .GetDatas<Bearing>(t => t.BearingID != OID &&
                                            t.FactoryID.Trim().Equals(FactoryID.Trim()) &&
                                            t.BearingNum.Trim().Equals(NewValue.Trim()), false)
                                    .Any();
                            }
                            break;
                        case 9:
                            if (Name.Equals("FactoryID"))
                            {
                                isExisted = factoryRepository
                                    .GetDatas<Factory>(t => t.ID != OID && t.FactoryID.Trim().Equals(NewValue.Trim()), false)
                                    .Any();
                            }
                            if (Name.Equals("FactoryName"))
                            {
                                isExisted = factoryRepository
                                    .GetDatas<Factory>(t => t.ID != OID && t.FactoryName.Trim().Equals(NewValue.Trim()), false)
                                    .Any();
                            }
                            break;
                    }
                }
                response.IsSuccessful = true;
                result.IsExisted = isExisted;
                response.Result = result;

                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Code = "004501";
                response.Result = result;
                return response;
            }
        }
        #endregion

        #region 是否有权限
        /// <summary>
        /// 是否有权限
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<bool> IsAuthorized(IsAuthorizedParameter param)
        {
            string RoleCode = param.RoleCode;
            string code = param.Code;

            BaseResponse<bool> result = null;
            try
            {
                var module = moduleRepository.GetDatas<Module>(t => t.Code.Equals(code), true).FirstOrDefault();
                if (module == null)
                {
                    result = new BaseResponse<bool> { IsSuccessful = false, Code = "004512", Result = false };
                    return result;
                }

                var ModuleCode = module.Code;


                //var isAuthoried = roleModuleRepository.GetDatas<RoleModule>(t => t.RoleID == roleID && t.ModuleID == ModuleID, false).Any();

                //过滤停用模板  王颖辉  2017-02-21
                //ModuleID => ModuleCode   王龙杰  2017-09-27
                var dbContext = new iCMSDbContext();
                var isAuthoried = (from roleModule in dbContext.RoleModule
                                   join moduledb in dbContext.Module on roleModule.ModuleCode equals moduledb.Code into roleModel_module
                                   where roleModule.RoleCode == RoleCode && roleModule.RoleCode == ModuleCode && module.IsUsed == 1
                                   from rm in roleModel_module.DefaultIfEmpty()
                                   select rm.ModuleID).Count() > 0;

                if (isAuthoried)
                {
                    result = new BaseResponse<bool>
                    {
                        IsSuccessful = true,
                        Code = string.Empty,
                        Result = true,
                    };
                }
                else
                {
                    result = new BaseResponse<bool> { IsSuccessful = true, Code = "004522", Result = false, };
                }
                return result;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                result = new BaseResponse<bool> { IsSuccessful = false, Code = "004531" };
                return result;
            }
        }
        #endregion

        #region 获取监测树节点
        /// <summary>
        /// 获取监测树节点
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<MonitorTreeDataForNavigationResult> GetMonitorTreeNodes(MTNodesParameter param)
        {
            //原因
            string reason = string.Empty;
            BaseResponse<MonitorTreeDataForNavigationResult> response = new BaseResponse<MonitorTreeDataForNavigationResult>();

            #region 初始化
            List<MTStatusInfo> mTDevStatusInfos = new List<MTStatusInfo>();
            MonitorTreeDataForNavigationResult monitorTreeResult = new MonitorTreeDataForNavigationResult();
            #endregion

            try
            {
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    #region 添加根节点
                    //添加根节点
                    MTStatusInfo mTDevStatusInfo = new MTStatusInfo() { MTId = "0", MTPid = "0" };
                    mTDevStatusInfos.Add(mTDevStatusInfo);
                    #endregion

                    if (param.UserID > 0)
                    {
                        mTDevStatusInfos = GetMonitorLevelTree(param.UserID);
                    }
                    else
                    {
                        var linq = (from mt in dbContext.MonitorTree
                                    join mtt in dbContext.MonitorTreeType on mt.Type equals mtt.ID
                                    select new
                                    {
                                        MTId = mt.MonitorTreeID,
                                        MTPid = mt.PID,
                                        MTName = mt.Name,
                                        MTStatus = mt.Status,
                                        MTType = mtt.Code,
                                        RecordID = mt.MonitorTreeID,
                                    }).ToList();
                        var viewMonitorTree = linq.Select(item => new MTStatusInfo
                        {
                            MTId = item.MTId.ToString(),
                            MTPid = item.MTPid.ToString(),
                            MTName = item.MTName.ToString(),
                            MTStatus = item.MTStatus.ToString(),
                            MTType = item.MTType.ToString(),
                            RecordID = item.RecordID.ToString(),
                        });
                        mTDevStatusInfos.AddRange(viewMonitorTree);
                    }

                    monitorTreeResult.MTDevStatusInfos = mTDevStatusInfos;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.Result = null;
                response.Code = "004541";
                response.IsSuccessful = false;
                return response;
            }

            response.Result = monitorTreeResult;
            response.Code = reason;
            response.IsSuccessful = true;
            return response;
        }

        //public BaseResponse<MonitorTreeDataForNavigationResult> GetMonitorTreeNodes(MTNodesParameter param)
        //{
        //    //原因
        //    string reason = string.Empty;
        //    int type = param.Type;
        //    BaseResponse<MonitorTreeDataForNavigationResult> response = new BaseResponse<MonitorTreeDataForNavigationResult>();

        //    #region 初始化
        //    List<MTStatusInfo> mTDevStatusInfos = new List<MTStatusInfo>();
        //    MonitorTreeDataForNavigationResult monitorTreeResult = new MonitorTreeDataForNavigationResult();
        //    #endregion

        //    try
        //    {
        //        #region 添加根节点
        //        //添加根节点
        //        MTStatusInfo mTDevStatusInfo = new MTStatusInfo() { MTId = "0", MTPid = "0" };
        //        mTDevStatusInfos.Add(mTDevStatusInfo);
        //        #endregion

        //        switch (type)
        //        {
        //            case 0://0:返回设备以上级别
        //                if (param.UserID > 0)
        //                {
        //                    mTDevStatusInfos = GetMonitorLevelTree(param.UserID);
        //                }
        //                else
        //                {
        //                    mTDevStatusInfos = GetMonitorLevelTree();
        //                }

        //                break;

        //            case 1://1:返回到设备级别

        //                if (param.UserID > 0)
        //                {
        //                    mTDevStatusInfos = GetDeviceLevelTree(param.UserID);
        //                }
        //                else
        //                {
        //                    mTDevStatusInfos = GetDeviceLevelTree();
        //                }


        //                break;

        //            case 2://2:返回整棵监测树（包含特征值等信息）

        //                if (param.UserID > 0)
        //                {
        //                    mTDevStatusInfos = GetMonitorTreeList(param.UserID, 0);
        //                }
        //                else
        //                {
        //                    mTDevStatusInfos = GetMonitorTreeList();
        //                }
        //                break;

        //            case 3://3:返回测量位置层数据
        //                if (param.UserID > 0)
        //                {
        //                    mTDevStatusInfos = GetMeasureSiteLevelList(param.UserID);
        //                }
        //                else
        //                {
        //                    mTDevStatusInfos = GetMeasureSiteLevelList();
        //                }

        //                break;

        //            default:
        //                break;
        //        }
        //        monitorTreeResult.MTDevStatusInfos = mTDevStatusInfos;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteLog(ex);
        //        response.Result = null;
        //        response.Code = "004541";
        //        response.IsSuccessful = false;
        //        return response;
        //    }

        //    response.Result = monitorTreeResult;
        //    response.Code = reason;
        //    response.IsSuccessful = true;
        //    return response;
        //}
        #endregion

        #region 获取监测树父节点
        /// <summary>
        /// 获取监测树父节点
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<MonitorTreeDataForNavigationResult> GetMonitorTreeParentNodes(MonitorTreeNodesParameter param)
        {
            BaseResponse<MonitorTreeDataForNavigationResult> response = new BaseResponse<MonitorTreeDataForNavigationResult>();

            //参数
            int RecordID = param.RecordID;
            int mtType = param.Type;

            #region 初始化
            MonitorTreeDataForNavigationResult monitorTreeResult = new MonitorTreeDataForNavigationResult();
            var reason = string.Empty;
            string name = string.Empty;
            List<int> treeNodeIDList = new List<int>();
            #endregion

            #region 获取数据
            try
            {
                List<MTStatusInfo> saveTree = new List<MTStatusInfo>();
                //全部设备，主备机
                var monitorTree = GetMonitorTreeList(2);

                //查找树id
                MTStatusInfo monitorTreeInfo = monitorTree
                    .Where(item => item.RecordID == RecordID.ToString()
                        && item.MTType.Equals(mtType.ToString()))
                    .FirstOrDefault();
                string monitorTreeID = string.Empty;
                if (monitorTreeInfo != null)
                {
                    monitorTreeID = monitorTreeInfo.MTId;
                }

                //递归查找
                GetParentNodeByCurrentNode(saveTree, monitorTree, monitorTreeID);
                foreach (var tree in saveTree)
                {
                    int id = Convert.ToInt32(tree.MTId);
                    treeNodeIDList.Add(id);
                }
                GetNodeNameByTree(saveTree, monitorTreeID.ToString(), ref name);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = true;
                response.Code = "004551";
                response.Result = monitorTreeResult;
                return response;
            }

            #endregion

            #region 结果值
            //倒序
            string[] arrayName = name.Split('#');
            name = string.Empty;
            for (int i = arrayName.Length - 1; i >= 0; i--)
            {
                name += arrayName[i] + "#";
            }
            name = name.TrimEnd('#');
            monitorTreeResult.TreeNode = name;
            monitorTreeResult.TreeNodeID = treeNodeIDList.Reverse<int>().ToList<int>();

            //返回值
            response.IsSuccessful = true;
            response.Code = string.Empty;

            response.Result = monitorTreeResult;

            return response;
            #endregion
        }
        #endregion

        #region 判断节点树是否挂靠设备，若有，则返回设备名称
        public BaseResponse<GetDeviceNameForMTNodeResult> GetDeviceNameForMTNode(DeviceNameForMTNodeParameter param)
        {
            int monitorTreeID = param.MonitorTreeID;

            BaseResponse<GetDeviceNameForMTNodeResult> response = new BaseResponse<GetDeviceNameForMTNodeResult>();
            var monitorTree = GetMonitorTreeList();
            string deviceName = string.Empty;
            GetDeviceNameForMTNodeResult result = new GetDeviceNameForMTNodeResult();
            result.IsExistDeviceChild = false;
            try
            {
                IsExistChildDevice(monitorTree, monitorTreeID.ToString(), ref deviceName);
                if (!string.IsNullOrWhiteSpace(deviceName))
                {
                    result.IsExistDeviceChild = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "004561";
                return response;
            }
            response.IsSuccessful = true;

            response.Code = string.Empty;
            response.Result = result;
            return response;
        }
        #endregion

        #region Server根据节点获取其所有的父节点
        public BaseResponse<MonitorTreeDataForNavigationResult> GetServerTreeParentNodes(MonitorTreeNodesParameter param)
        {
            BaseResponse<MonitorTreeDataForNavigationResult> response = new BaseResponse<MonitorTreeDataForNavigationResult>();
            int recordID = param.RecordID;
            string type = param.Type.ToString();

            MonitorTreeDataForNavigationResult monitorTreeResult = new MonitorTreeDataForNavigationResult();
            var reason = string.Empty;
            string name = string.Empty;
            List<int> treeNodeIDList = new List<int>();
            try
            {
                List<MTStatusInfo> saveTree = new List<MTStatusInfo>();
                var monitorTree = GetServerTreeList();

                //获取树自身id
                var monitorInfo = monitorTree.Where(item => item.RecordID == recordID.ToString() && item.MTType == type).FirstOrDefault();
                string id = string.Empty;
                if (monitorInfo != null)
                {
                    id = monitorInfo.MTId;
                }

                //获取所有父节点
                GetParentNodeByCurrentNode(saveTree, monitorTree, id.ToString());

                //获取所有父节点名称
                GetNodeNameByTree(saveTree, id.ToString(), ref name);

                //节点id list
                foreach (var tree in saveTree)
                {
                    int mTId = Convert.ToInt32(tree.MTId);
                    treeNodeIDList.Add(mTId);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Result = null;
                response.Code = "004571";
                return response;
            }

            //倒序
            string[] arrayName = name.Split('#');
            name = string.Empty;
            for (int i = arrayName.Length - 1; i >= 0; i--)
            {
                name += arrayName[i] + "#";
            }
            name = name.TrimEnd('#');
            monitorTreeResult.TreeNode = name;
            monitorTreeResult.TreeNodeID = treeNodeIDList.Reverse<int>().ToList<int>();

            response.IsSuccessful = true;
            response.Result = monitorTreeResult;

            return response;
        }
        #endregion

        #region 查找树的所有节点名称
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-07-29
        /// 查找树的所有节点名称
        /// <param name="resourceTree">数据源</param>
        /// <param name="name">保存名称</param>
        private void IsExistChildDevice(List<MTStatusInfo> resourceTree, string childTreeId, ref string deviceName)
        {
            //查找子节点
            var list = resourceTree.Where(item => item.MTPid == childTreeId);
            foreach (var info in list)
            {
                //5代表设备
                if (info.MTType == "5")
                {
                    deviceName = info.MTName;
                }
                else
                {
                    IsExistChildDevice(resourceTree, info.MTId, ref deviceName);
                }
            }
        }
        #endregion

        #region 获取当前节点所有父节点信息
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-07-29
        /// 获取当前节点所有父节点信息
        /// </summary>
        /// <param name="treeId">当前树节点</param>
        /// <returns>节点列表</returns>
        private List<MTStatusInfo> GetParentNodeByCurrentNode(List<MTStatusInfo> saveTree, List<MTStatusInfo> resourceTree, string treeId)
        {
            GetParentTree(saveTree, resourceTree, treeId);
            return saveTree;
        }
        #endregion

        #region 查找树的所有节点名称
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-07-29
        /// 查找树的所有节点名称
        /// <param name="resourceTree">数据源</param>
        /// <param name="name">保存名称</param>
        private void GetNodeNameByTree(List<MTStatusInfo> resourceTree, string treeId, ref string name)
        {
            var node = resourceTree.Where(item => item.MTId == treeId).FirstOrDefault();

            if (node != null && node.MTName != null)
            {
                string treeName = node.MTName;
                name += "#" + treeName;
            }
            else
            {
                return;
            }
            GetNodeNameByTree(resourceTree, node.MTPid, ref name);
        }
        #endregion

        #region 私有方法
        #region 父节点递归树
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-07-29
        /// 父节点递归树
        /// </summary>
        /// <param name="saveTree">保存结果树</param>
        /// <param name="resourceTree">数据源</param>
        /// <param name="treeId">树id</param>
        private void GetParentTree(List<MTStatusInfo> saveTree, List<MTStatusInfo> resourceTree, string treeId)
        {
            if (treeId == "0")
            {
                return;
            }
            //当前节点
            var node = resourceTree.Where(item => item.MTId == treeId).FirstOrDefault();
            if (node != null)
            {
                saveTree.Add(node);
                var parentId = node.MTPid;
                //父节点
                var parentNode = resourceTree.Where(item => item.MTId == parentId).FirstOrDefault();
                if (parentNode != null)
                {
                    //递归
                    GetParentTree(saveTree, resourceTree, parentNode.MTId);
                }
            }
        }
        #endregion

        #region 获取监测树级别
        /// <summary>
        /// 获取监测树级别
        /// </summary>
        /// <returns></returns>
        private List<MTStatusInfo> GetMonitorLevelTree()
        {
            List<MTStatusInfo> mTDevStatusInfos = new List<MTStatusInfo>();

            #region 添加根节点
            //添加根节点
            MTStatusInfo mTDevStatusInfo = new MTStatusInfo() { MTId = "0", MTPid = "0" };
            mTDevStatusInfos.Add(mTDevStatusInfo);
            #endregion

            var sqlViewMonitorTree = ConstObject.SQL_View_MonitorTree;
            var viewMonitorTree = new iCMSDbContext().Database.SqlQuery<View_MonitorTree>(sqlViewMonitorTree).ToList<View_MonitorTree>();

            #region 监测树
            var monitorList = viewMonitorTree.Select(item => new
            {
                item.MonitorTreeID,//监测树id
                item.Name,//监测树名称
                item.Lvl,//监测树级别
                item.Describe,//监测树描述
                item.PID,//父节点id
            }).Distinct();

            var monitorRootList = monitorList.Where(item => item.Lvl == 0);
            var mtId = 1;
            if (viewMonitorTree != null && viewMonitorTree.Count() > 0)
            {
                mtId = viewMonitorTree.Max(item => item.MonitorTreeID) + 1;
            }
            if (monitorRootList != null && monitorRootList.Count() > 0)
            {
                foreach (var monitor in monitorRootList)
                {
                    MTStatusInfo mTStatusInfo = new MTStatusInfo();
                    mTStatusInfo.MTPid = "0";//上传树id
                    mTStatusInfo.MTId = monitor.MonitorTreeID.ToString();
                    mTStatusInfo.MTName = monitor.Name;
                    mTStatusInfo.MTStatus = "1";//正常状态
                    mTStatusInfo.MTType = monitor.Describe;
                    mTStatusInfo.Remark = monitor.Name;
                    mTStatusInfo.RecordID = monitor.MonitorTreeID.ToString();
                    //添加 
                    mTDevStatusInfos.Add(mTStatusInfo);

                    var monitorSecondList = monitorList.Where(item => item.Lvl == 1 && item.PID == monitor.MonitorTreeID);

                    if (monitorSecondList != null && monitorSecondList.Count() > 0)
                    {
                        foreach (var second in monitorSecondList)
                        {
                            MTStatusInfo secondMTStatusInfo = new MTStatusInfo();
                            secondMTStatusInfo.MTPid = mTStatusInfo.MTId;//上传树id
                            secondMTStatusInfo.MTId = second.MonitorTreeID.ToString();
                            secondMTStatusInfo.MTName = second.Name;
                            secondMTStatusInfo.MTStatus = "1";//正常状态
                            secondMTStatusInfo.MTType = second.Describe;
                            secondMTStatusInfo.Remark = second.Name;
                            secondMTStatusInfo.RecordID = second.MonitorTreeID.ToString();
                            //添加 
                            mTDevStatusInfos.Add(secondMTStatusInfo);

                            var monitroThirdList = monitorList.Where(item => item.Lvl == 2 && item.PID == second.MonitorTreeID);

                            if (monitroThirdList != null && monitroThirdList.Count() > 0)
                            {
                                foreach (var third in monitroThirdList)
                                {
                                    MTStatusInfo thirdMTStatusInfo = new MTStatusInfo();
                                    thirdMTStatusInfo.MTPid = secondMTStatusInfo.MTId;//上传树id
                                    thirdMTStatusInfo.MTId = third.MonitorTreeID.ToString();
                                    thirdMTStatusInfo.MTName = third.Name;
                                    thirdMTStatusInfo.MTStatus = "1";//正常状态
                                    thirdMTStatusInfo.MTType = third.Describe;
                                    thirdMTStatusInfo.Remark = third.Name;
                                    thirdMTStatusInfo.RecordID = third.MonitorTreeID.ToString();
                                    //添加 
                                    mTDevStatusInfos.Add(thirdMTStatusInfo);

                                    var monitrofourthList = monitorList.Where(item => item.Lvl == 3 && item.PID == third.MonitorTreeID);

                                    if (monitrofourthList != null && monitrofourthList.Count() > 0)
                                    {
                                        foreach (var fourth in monitrofourthList)
                                        {
                                            MTStatusInfo fourthMTStatusInfo = new MTStatusInfo();
                                            fourthMTStatusInfo.MTPid = thirdMTStatusInfo.MTId;//上传树id
                                            fourthMTStatusInfo.MTId = fourth.MonitorTreeID.ToString();
                                            fourthMTStatusInfo.MTName = fourth.Name;
                                            fourthMTStatusInfo.MTStatus = "1";//正常状态
                                            fourthMTStatusInfo.MTType = fourth.Describe;
                                            fourthMTStatusInfo.Remark = fourth.Name;
                                            fourthMTStatusInfo.RecordID = fourth.MonitorTreeID.ToString();
                                            //添加 
                                            mTDevStatusInfos.Add(fourthMTStatusInfo);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            return mTDevStatusInfos;
        }
        #endregion

        #region 获取监测树级别,通过用户ID
        /// <summary>
        /// 获取监测树级别
        /// </summary>
        /// <returns></returns>
        private List<MTStatusInfo> GetMonitorLevelTree(int userId)
        {
            List<MTStatusInfo> mTDevStatusInfos = new List<MTStatusInfo>();

            #region 添加根节点
            //添加根节点
            MTStatusInfo mTDevStatusInfo = new MTStatusInfo() { MTId = "0", MTPid = "0" };
            mTDevStatusInfos.Add(mTDevStatusInfo);
            #endregion

            string sqlViewMonitorTree = string.Format(@"WITH    tree1
                                                      AS ( SELECT   a.MonitorTreeID TreeId ,
                                                                    a.PID ParentId ,
                                                                    a.TrueId ,
                                                                    a.Level
                                                           FROM     View_DeviceTree a
                                                           WHERE    a.TrueId IN ( SELECT DISTINCT
                                                                                            DevId
                                                                                  FROM      T_SYS_USER_RELATION_DEVICE
                                                                                  WHERE     UserID = {0} )
                                                                    AND a.Level = ( SELECT  COUNT(1) + 1
                                                                                    FROM    [dbo].[T_DICT_MONITORTREE_TYPE]
                                                                                    WHERE   IsUsable = 1
                                                                                  )
                                                           UNION ALL
                                                           SELECT   k.MonitorTreeID TreeId ,
                                                                    k.PID ParentId ,
                                                                    k.TrueId ,
                                                                    k.Level
                                                           FROM     View_DeviceTree k
                                                                    INNER JOIN tree1 c ON c.ParentId = k.MonitorTreeID
                                                         )
                                                SELECT  TSMT.PID MTPid ,
                                                        TSMT.MonitorTreeID MTId ,
                                                        TSMT.Name MTName ,
                                                        1 MTStatus ,
                                                        ( SELECT    TDMT.Code
                                                          FROM      dbo.T_DICT_MONITORTREE_TYPE AS TDMT
                                                          WHERE     TDMT.ID = TSMT.Type
                                                        ) MTType ,
                                                        TSMT.Name Remark ,
                                                        TSMT.MonitorTreeID RecordID
                                                FROM    dbo.T_SYS_MONITOR_TREE AS TSMT
                                                WHERE   TSMT.MonitorTreeID IN (
                                                        SELECT DISTINCT
                                                                TrueId
                                                        FROM    tree1
                                                        WHERE   Level < ( SELECT    COUNT(1) + 1
                                                                          FROM      [dbo].[T_DICT_MONITORTREE_TYPE]
                                                                          WHERE     IsUsable = 1
                                                                        ) )", userId);//ConstObject.SQL_View_MonitorTree;
            var viewMonitorTreeTemp = new iCMSDbContext().Database.SqlQuery<MTStatusDBInfo>(sqlViewMonitorTree).ToList<MTStatusDBInfo>();

            var viewMonitorTree = viewMonitorTreeTemp.Select(item => new MTStatusInfo
            {
                MTId = item.MTId.ToString(),
                MTPid = item.MTPid.ToString(),
                MTName = item.MTName.ToString(),
                MTStatus = item.MTStatus.ToString(),
                MTType = item.MTType.ToString(),
                RecordID = item.RecordID.ToString(),
                Remark = item.Remark.ToString(),
            });

            mTDevStatusInfos.AddRange(viewMonitorTree);

            return mTDevStatusInfos;
        }
        #endregion

        #region 获取设备级别
        /// <summary>
        /// 获取设备级别
        /// </summary>
        /// <returns></returns>
        private List<MTStatusInfo> GetDeviceLevelTree()
        {
            List<MTStatusInfo> mTDevStatusInfos = new List<MTStatusInfo>();

            #region 添加根节点
            //添加根节点
            MTStatusInfo mTDevStatusInfo = new MTStatusInfo() { MTId = "0", MTPid = "0" };
            mTDevStatusInfos.Add(mTDevStatusInfo);
            #endregion

            var sqlViewMonitorTree = ConstObject.SQL_View_MonitorTree;
            var viewMonitorTree = new iCMSDbContext().Database.SqlQuery<View_MonitorTree>(sqlViewMonitorTree).ToList<View_MonitorTree>();

            #region 监测树
            var monitorList = viewMonitorTree.Select(item => new
            {
                item.MonitorTreeID,//监测树id
                item.Name,//监测树名称
                item.Lvl,//监测树级别
                item.Describe,//监测树描述
                item.PID,//父节点id
            }).Distinct();

            var monitorRootList = monitorList.Where(item => item.Lvl == 0);
            var mtId = 1;
            if (viewMonitorTree != null && viewMonitorTree.Count() > 0)
            {
                mtId = viewMonitorTree.Max(item => item.MonitorTreeID) + 1;
            }
            if (monitorRootList != null && monitorRootList.Count() > 0)
            {
                foreach (var monitor in monitorRootList)
                {
                    MTStatusInfo mTStatusInfo = new MTStatusInfo();
                    mTStatusInfo.MTPid = "0";//上传树id
                    mTStatusInfo.MTId = monitor.MonitorTreeID.ToString();
                    mTStatusInfo.MTName = monitor.Name;
                    mTStatusInfo.MTStatus = "1";//正常状态
                    mTStatusInfo.MTType = monitor.Describe;
                    mTStatusInfo.Remark = monitor.Name;
                    mTStatusInfo.RecordID = monitor.MonitorTreeID.ToString();
                    //添加 
                    mTDevStatusInfos.Add(mTStatusInfo);

                    var monitorSecondList = monitorList.Where(item => item.Lvl == 1 && item.PID == monitor.MonitorTreeID);

                    if (monitorSecondList != null && monitorSecondList.Count() > 0)
                    {
                        foreach (var second in monitorSecondList)
                        {
                            MTStatusInfo secondMTStatusInfo = new MTStatusInfo();
                            secondMTStatusInfo.MTPid = mTStatusInfo.MTId;//上传树id
                            secondMTStatusInfo.MTId = second.MonitorTreeID.ToString();
                            secondMTStatusInfo.MTName = second.Name;
                            secondMTStatusInfo.MTStatus = "1";//正常状态
                            secondMTStatusInfo.MTType = second.Describe;
                            secondMTStatusInfo.Remark = second.Name;
                            secondMTStatusInfo.RecordID = second.MonitorTreeID.ToString();
                            //添加 
                            mTDevStatusInfos.Add(secondMTStatusInfo);

                            var monitroThirdList = monitorList.Where(item => item.Lvl == 2 && item.PID == second.MonitorTreeID);

                            if (monitroThirdList != null && monitroThirdList.Count() > 0)
                            {
                                foreach (var third in monitroThirdList)
                                {
                                    MTStatusInfo thirdMTStatusInfo = new MTStatusInfo();
                                    thirdMTStatusInfo.MTPid = secondMTStatusInfo.MTId;//上传树id
                                    thirdMTStatusInfo.MTId = third.MonitorTreeID.ToString();
                                    thirdMTStatusInfo.MTName = third.Name;
                                    thirdMTStatusInfo.MTStatus = "1";//正常状态
                                    thirdMTStatusInfo.MTType = third.Describe;
                                    thirdMTStatusInfo.Remark = third.Name;
                                    thirdMTStatusInfo.RecordID = third.MonitorTreeID.ToString();
                                    //添加 
                                    mTDevStatusInfos.Add(thirdMTStatusInfo);

                                    var monitrofourthList = monitorList.Where(item => item.Lvl == 3 && item.PID == third.MonitorTreeID);

                                    if (monitrofourthList != null && monitrofourthList.Count() > 0)
                                    {
                                        foreach (var fourth in monitrofourthList)
                                        {
                                            MTStatusInfo fourthMTStatusInfo = new MTStatusInfo();
                                            fourthMTStatusInfo.MTPid = thirdMTStatusInfo.MTId;//上传树id
                                            fourthMTStatusInfo.MTId = fourth.MonitorTreeID.ToString();
                                            fourthMTStatusInfo.MTName = fourth.Name;
                                            fourthMTStatusInfo.MTStatus = "1";//正常状态
                                            fourthMTStatusInfo.MTType = fourth.Describe;
                                            fourthMTStatusInfo.Remark = fourth.Name;
                                            fourthMTStatusInfo.RecordID = fourth.MonitorTreeID.ToString();
                                            //添加 
                                            mTDevStatusInfos.Add(fourthMTStatusInfo);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            #region 监测树以下
            //if (false)
            if (monitorList != null && monitorList.Count() > 0)
            {
                foreach (var monitor in monitorList)
                {
                    #region 设备
                    //添加设备
                    var deviceList = viewMonitorTree.Where(item => item.MonitorTreeID == monitor.MonitorTreeID && item.DevID != null).
                          Select(item => new
                          {
                              item.DevID,//设备id
                              item.DevName,//设备名称
                              item.RunStatus,//运行状态
                              item.AlmStatus,//报警状态
                              item.UseType,//主，备机
                          }).Distinct();

                    if (deviceList != null && deviceList.Count() > 0)
                    {
                        foreach (var device in deviceList)
                        {
                            MTStatusInfo deviceMTStatusInfo = new MTStatusInfo();
                            deviceMTStatusInfo.MTId = (mtId++).ToString();
                            deviceMTStatusInfo.MTPid = monitor.MonitorTreeID.ToString();

                            //备注，如果是停机是4,如果是正常就取AlmStatus状态
                            //        //主机
                            if (device.UseType == 0)
                            {
                                if (device.RunStatus == (int)EnumRunStatus.Stop)
                                {
                                    //备用设备
                                    if (device.UseType == 1)
                                    {
                                        //停机状态
                                        deviceMTStatusInfo.MTName = device.DevName + "<span style='color:#808080'>(备用设备)</span>";
                                        deviceMTStatusInfo.MTStatus = "4";
                                    }
                                    else
                                    {
                                        //停机状态
                                        deviceMTStatusInfo.MTName = device.DevName + "(stop)";
                                        deviceMTStatusInfo.MTStatus = "4";
                                    }


                                }
                                else
                                {
                                    //运行状态
                                    deviceMTStatusInfo.MTName = device.DevName;
                                    deviceMTStatusInfo.MTStatus = device.AlmStatus.ToString();
                                }
                            }
                            else
                            {
                                deviceMTStatusInfo.MTName = device.DevName + "<span style='color:#808080'>(备用设备)</span>";
                                deviceMTStatusInfo.MTStatus = device.AlmStatus.ToString();
                            }

                            deviceMTStatusInfo.MTType = "5";//设备
                            deviceMTStatusInfo.Remark = device.DevName;
                            deviceMTStatusInfo.RecordID = device.DevID.ToString();

                            //添加 
                            mTDevStatusInfos.Add(deviceMTStatusInfo);
                        }
                    }
                    #endregion
                }
            }

            #endregion

            return mTDevStatusInfos;
        }
        #endregion

        #region 获取设备级别，通过用户ID
        /// <summary>
        /// 获取设备级别，通过用户ID 
        /// </summary>
        /// <returns></returns>
        private List<MTStatusInfo> GetDeviceLevelTree(int userId)
        {
            List<MTStatusInfo> mTDevStatusInfos = new List<MTStatusInfo>();

            #region 添加根节点
            //添加根节点
            MTStatusInfo mTDevStatusInfo = new MTStatusInfo() { MTId = "0", MTPid = "0" };
            mTDevStatusInfos.Add(mTDevStatusInfo);
            #endregion

            var sqlViewMonitorTree = string.Format(@"WITH    tree1
                                      AS ( SELECT   a.MonitorTreeID TreeId ,
                                                    a.Name MTName ,
                                                    a.PID ParentId ,
                                                    a.TrueId ,
                                                    a.Level
                                           FROM     Tree a
                                           WHERE    a.Level = (SELECT COUNT(1)+1 FROM [dbo].[T_DICT_MONITORTREE_TYPE] WHERE IsUsable=1)
                                                    AND a.TrueId IN (
                                                    SELECT DISTINCT
                                                            DevId
                                                    FROM    T_SYS_USER_RELATION_DEVICE
                                                    WHERE   UserID = {0} )
                                           UNION ALL
                                           SELECT   k.MonitorTreeID TreeId ,
                                                    k.Name MTName ,
                                                    k.PID ParentId ,
                                                    k.TrueId ,
                                                    k.Level
                                           FROM     Tree k
                                                    INNER JOIN tree1 c ON c.ParentId = k.MonitorTreeID
                                         )
                                SELECT DISTINCT
                                        TSMT.ParentId MTPid ,
                                        TSMT.TreeId MTId ,
                                        TSMT.MTName MTName ,
                                        1 MTStatus ,
                                        Level MTType ,
                                        TSMT.MTName Remark ,
                                        TSMT.TreeId RecordID
                                FROM    tree1 AS TSMT", userId);

            var viewMonitorTreeTemp = new iCMSDbContext().Database.SqlQuery<viewMonitorTree>(sqlViewMonitorTree).ToList<viewMonitorTree>();

            var level = cacheDICT.GetInstance().GetCacheType<DeviceType>().Where(item => item.IsUsable == 1).Count();
            var temp = viewMonitorTreeTemp.Where(item => item.Level == level);
            foreach (var i in temp)
            {
                if (i.UseType == 0)
                {
                    if (i.MTStatus == (int)EnumRunStatus.Stop)
                    {
                        //备用设备
                        if (i.UseType == 1)
                        {
                            //停机状态
                            i.Name = i.Name + "<span style='color:#808080'>(备用设备)</span>";
                        }
                        else
                        {
                            //停机状态
                            i.Name = i.Name + "(stop)";
                        }
                    }
                    else
                    {
                        //运行状态
                        i.Name = i.Name;
                    }
                }
                else
                {
                    i.Name = i.Name + "<span style='color:#808080'>(备用设备)</span>";
                }
            }

            var viewMonitorTree = viewMonitorTreeTemp.Select(item => new MTStatusInfo()
            {

                MTId = item.MonitorTreeID.ToString(),
                MTPid = item.PId.ToString(),
                MTName = item.Name.ToString(),
                MTStatus = item.MTStatus.ToString(),
                MTType = item.Level.ToString(),
                RecordID = item.TrueId.ToString(),
                Remark = item.Name.ToString(),
            }
            );

            mTDevStatusInfos.AddRange(viewMonitorTree);

            return mTDevStatusInfos;
        }
        #endregion

        #region 获取测点级别
        /// <summary>
        /// 获取测点级别
        /// </summary>
        /// <returns></returns>
        private List<MTStatusInfo> GetMeasureSiteLevelList()
        {
            List<MTStatusInfo> mTDevStatusInfos = new List<MTStatusInfo>();

            #region 添加根节点
            //添加根节点
            MTStatusInfo mTDevStatusInfo = new MTStatusInfo() { MTId = "0", MTPid = "0" };
            mTDevStatusInfos.Add(mTDevStatusInfo);
            #endregion

            var sqlViewMonitorTree = ConstObject.SQL_View_MonitorTree;
            var viewMonitorTree = new iCMSDbContext().Database.SqlQuery<View_MonitorTree>(sqlViewMonitorTree).ToList<View_MonitorTree>();

            #region 监测树
            var monitorList = viewMonitorTree.Select(item => new
            {
                item.MonitorTreeID,//监测树id
                item.Name,//监测树名称
                item.Lvl,//监测树级别
                item.Describe,//监测树描述
                item.PID,//父节点id
            }).Distinct();

            var monitorRootList = monitorList.Where(item => item.Lvl == 0);
            var mtId = 1;
            if (viewMonitorTree != null && viewMonitorTree.Count() > 0)
            {
                mtId = viewMonitorTree.Max(item => item.MonitorTreeID) + 1;
            }
            if (monitorRootList != null && monitorRootList.Count() > 0)
            {
                foreach (var monitor in monitorRootList)
                {
                    MTStatusInfo mTStatusInfo = new MTStatusInfo();
                    mTStatusInfo.MTPid = "0";//上传树id
                    mTStatusInfo.MTId = monitor.MonitorTreeID.ToString();
                    mTStatusInfo.MTName = monitor.Name;
                    mTStatusInfo.MTStatus = "1";//正常状态
                    mTStatusInfo.MTType = monitor.Describe;
                    mTStatusInfo.Remark = monitor.Name;
                    mTStatusInfo.RecordID = monitor.MonitorTreeID.ToString();
                    //添加 
                    mTDevStatusInfos.Add(mTStatusInfo);

                    var monitorSecondList = monitorList.Where(item => item.Lvl == 1 && item.PID == monitor.MonitorTreeID);

                    if (monitorSecondList != null && monitorSecondList.Count() > 0)
                    {
                        foreach (var second in monitorSecondList)
                        {
                            MTStatusInfo secondMTStatusInfo = new MTStatusInfo();
                            secondMTStatusInfo.MTPid = mTStatusInfo.MTId;//上传树id
                            secondMTStatusInfo.MTId = second.MonitorTreeID.ToString();
                            secondMTStatusInfo.MTName = second.Name;
                            secondMTStatusInfo.MTStatus = "1";//正常状态
                            secondMTStatusInfo.MTType = second.Describe;
                            secondMTStatusInfo.Remark = second.Name;
                            secondMTStatusInfo.RecordID = second.MonitorTreeID.ToString();
                            //添加 
                            mTDevStatusInfos.Add(secondMTStatusInfo);

                            var monitroThirdList = monitorList.Where(item => item.Lvl == 2 && item.PID == second.MonitorTreeID);

                            if (monitroThirdList != null && monitroThirdList.Count() > 0)
                            {
                                foreach (var third in monitroThirdList)
                                {
                                    MTStatusInfo thirdMTStatusInfo = new MTStatusInfo();
                                    thirdMTStatusInfo.MTPid = secondMTStatusInfo.MTId;//上传树id
                                    thirdMTStatusInfo.MTId = third.MonitorTreeID.ToString();
                                    thirdMTStatusInfo.MTName = third.Name;
                                    thirdMTStatusInfo.MTStatus = "1";//正常状态
                                    thirdMTStatusInfo.MTType = third.Describe;
                                    thirdMTStatusInfo.Remark = third.Name;
                                    thirdMTStatusInfo.RecordID = third.MonitorTreeID.ToString();
                                    //添加 
                                    mTDevStatusInfos.Add(thirdMTStatusInfo);

                                    var monitrofourthList = monitorList.Where(item => item.Lvl == 3 && item.PID == third.MonitorTreeID);

                                    if (monitrofourthList != null && monitrofourthList.Count() > 0)
                                    {
                                        foreach (var fourth in monitrofourthList)
                                        {
                                            MTStatusInfo fourthMTStatusInfo = new MTStatusInfo();
                                            fourthMTStatusInfo.MTPid = thirdMTStatusInfo.MTId;//上传树id
                                            fourthMTStatusInfo.MTId = fourth.MonitorTreeID.ToString();
                                            fourthMTStatusInfo.MTName = fourth.Name;
                                            fourthMTStatusInfo.MTStatus = "1";//正常状态
                                            fourthMTStatusInfo.MTType = fourth.Describe;
                                            fourthMTStatusInfo.Remark = fourth.Name;
                                            fourthMTStatusInfo.RecordID = fourth.MonitorTreeID.ToString();
                                            //添加 
                                            mTDevStatusInfos.Add(fourthMTStatusInfo);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            #region 监测树以下
            //if (false)
            if (monitorList != null && monitorList.Count() > 0)
            {
                foreach (var monitor in monitorList)
                {
                    #region 设备
                    //添加设备
                    var deviceList = viewMonitorTree.Where(item => item.MonitorTreeID == monitor.MonitorTreeID && item.DevID != null).
                          Select(item => new
                          {
                              item.DevID,//设备id
                              item.DevName,//设备名称
                              item.RunStatus,//运行状态
                              item.AlmStatus,//报警状态
                              item.UseType,//主备机
                          }).Distinct();

                    if (deviceList != null && deviceList.Count() > 0)
                    {
                        foreach (var device in deviceList)
                        {
                            MTStatusInfo deviceMTStatusInfo = new MTStatusInfo();
                            deviceMTStatusInfo.MTId = (mtId++).ToString();
                            deviceMTStatusInfo.MTPid = monitor.MonitorTreeID.ToString();

                            //备注，如果是停机是4,如果是正常就取AlmStatus状态
                            if (device.RunStatus == (int)EnumRunStatus.Stop)
                            {

                                if (device.UseType == 1)
                                {
                                    //备用设备
                                    deviceMTStatusInfo.MTName = device.DevName + "<span style='color:#808080'>(备用设备)</span>";
                                    deviceMTStatusInfo.MTStatus = "4";
                                }
                                else
                                {
                                    //停机状态
                                    deviceMTStatusInfo.MTName = device.DevName + "(stop)";
                                    deviceMTStatusInfo.MTStatus = "4";
                                }

                            }
                            else
                            {
                                //运行状态
                                deviceMTStatusInfo.MTName = device.DevName;
                                deviceMTStatusInfo.MTStatus = device.AlmStatus.ToString();
                            }

                            deviceMTStatusInfo.MTType = "5";//设备
                            deviceMTStatusInfo.Remark = device.DevName;
                            deviceMTStatusInfo.RecordID = device.DevID.ToString();

                            //添加 
                            mTDevStatusInfos.Add(deviceMTStatusInfo);

                            #region 测量位置
                            //添加测量位置
                            var measureSiteList = viewMonitorTree.Where(item => item.DevID == device.DevID && item.MSiteID != null).
                                  Select(item => new
                                  {
                                      item.MSiteID,//位置id
                                      item.MeasureSiteName,//位置名称
                                      item.MSiteStatus,//位置状态
                                      item.WSName,//网关名称
                                  }).Distinct();

                            if (measureSiteList != null && measureSiteList.Count() > 0)
                            {
                                foreach (var measureSite in measureSiteList)
                                {
                                    //重复数据，添加随机数
                                    MTStatusInfo measureSiteMTStatusInfo = new MTStatusInfo();

                                    measureSiteMTStatusInfo.MTId = (mtId++).ToString();
                                    measureSiteMTStatusInfo.MTPid = deviceMTStatusInfo.MTId;
                                    measureSiteMTStatusInfo.MTName = measureSite.MeasureSiteName;//;关联到别外一张表
                                    measureSiteMTStatusInfo.MTStatus = measureSite.MSiteStatus.ToString();
                                    measureSiteMTStatusInfo.MTType = "6";//设备
                                    measureSiteMTStatusInfo.Remark = measureSite.WSName;
                                    measureSiteMTStatusInfo.RecordID = measureSite.MSiteID.ToString();

                                    //添加 最后面 
                                    mTDevStatusInfos.Add(measureSiteMTStatusInfo);
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
            }

            #endregion

            return mTDevStatusInfos;
        }
        #endregion

        #region 获取测点级别
        /// <summary>
        /// 获取测点级别
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="level">级别</param>
        /// <returns></returns>
        private List<MTStatusInfo> GetMeasureSiteLevelList(int userId)
        {
            List<MTStatusInfo> mTDevStatusInfos = new List<MTStatusInfo>();

            #region 添加根节点
            //添加根节点
            MTStatusInfo mTDevStatusInfo = new MTStatusInfo() { MTId = "0", MTPid = "0" };
            mTDevStatusInfos.Add(mTDevStatusInfo);
            #endregion

            SqlParameter[] sqlParam = new SqlParameter[1];
            sqlParam[0] = new SqlParameter("@userID", userId);

            var viewMonitorTreeDB = new iCMSDbContext().Database.SqlQuery<MTStatusDBInfo>("execute SP_GetMeasureSiteLevelByUserID @UserID", sqlParam).ToList<MTStatusDBInfo>();
            var viewMonitorTree = viewMonitorTreeDB.Select(
                item => new MTStatusInfo()
                {
                    MTId = item.MTId.ToString(),
                    MTName = item.MTName,
                    MTStatus = item.MTStatus.ToString(),
                    MTPid = item.MTPid.ToString(),
                    MTType = item.MTType.ToString(),
                    RecordID = item.RecordID.ToString(),
                    Remark = item.Remark,
                }
            );
            mTDevStatusInfos.AddRange(viewMonitorTree);

            return mTDevStatusInfos;
        }
        #endregion

        #region 获取监测树，通过类型
        /// <summary>
        /// 获取监测树，通过类型
        /// </summary>
        /// <param name="useType"></param>
        /// <returns></returns>
        private List<MTStatusInfo> GetMonitorTreeList(int useType = 0)
        {
            List<MTStatusInfo> mTDevStatusInfos = new List<MTStatusInfo>();

            #region 添加根节点
            //添加根节点
            MTStatusInfo mTDevStatusInfo = new MTStatusInfo() { MTId = "0", MTPid = "0" };
            mTDevStatusInfos.Add(mTDevStatusInfo);
            #endregion

            var sqlViewMonitorTree = ConstObject.SQL_View_MonitorTree;
            var viewMonitorTree = new iCMSDbContext().Database.SqlQuery<View_MonitorTree>(sqlViewMonitorTree).ToList<View_MonitorTree>();

            #region 监测树
            var monitorList = viewMonitorTree.Select(item => new
            {
                item.MonitorTreeID,//监测树id
                item.Name,//监测树名称
                item.Lvl,//监测树级别
                item.Describe,//监测树描述
                item.PID,//父节点id
            }).Distinct();

            var monitorRootList = monitorList.Where(item => item.Lvl == 0);
            var mtId = 1;
            if (viewMonitorTree != null && viewMonitorTree.Count() > 0)
            {
                mtId = viewMonitorTree.Max(item => item.MonitorTreeID) + 1;
            }
            if (monitorRootList != null && monitorRootList.Count() > 0)
            {
                foreach (var monitor in monitorRootList)
                {
                    MTStatusInfo mTStatusInfo = new MTStatusInfo();
                    mTStatusInfo.MTPid = "0";//上传树id
                    mTStatusInfo.MTId = monitor.MonitorTreeID.ToString();
                    mTStatusInfo.MTName = monitor.Name;
                    mTStatusInfo.MTStatus = "1";//正常状态
                    mTStatusInfo.MTType = monitor.Describe;
                    mTStatusInfo.Remark = monitor.Name;
                    mTStatusInfo.RecordID = monitor.MonitorTreeID.ToString();
                    //添加 
                    mTDevStatusInfos.Add(mTStatusInfo);

                    var monitorSecondList = monitorList.Where(item => item.Lvl == 1 && item.PID == monitor.MonitorTreeID);

                    if (monitorSecondList != null && monitorSecondList.Count() > 0)
                    {
                        foreach (var second in monitorSecondList)
                        {
                            MTStatusInfo secondMTStatusInfo = new MTStatusInfo();
                            secondMTStatusInfo.MTPid = mTStatusInfo.MTId;//上传树id
                            secondMTStatusInfo.MTId = second.MonitorTreeID.ToString();
                            secondMTStatusInfo.MTName = second.Name;
                            secondMTStatusInfo.MTStatus = "1";//正常状态
                            secondMTStatusInfo.MTType = second.Describe;
                            secondMTStatusInfo.Remark = second.Name;
                            secondMTStatusInfo.RecordID = second.MonitorTreeID.ToString();
                            //添加 
                            mTDevStatusInfos.Add(secondMTStatusInfo);

                            var monitroThirdList = monitorList.Where(item => item.Lvl == 2 && item.PID == second.MonitorTreeID);

                            if (monitroThirdList != null && monitroThirdList.Count() > 0)
                            {
                                foreach (var third in monitroThirdList)
                                {
                                    MTStatusInfo thirdMTStatusInfo = new MTStatusInfo();
                                    thirdMTStatusInfo.MTPid = secondMTStatusInfo.MTId;//上传树id
                                    thirdMTStatusInfo.MTId = third.MonitorTreeID.ToString();
                                    thirdMTStatusInfo.MTName = third.Name;
                                    thirdMTStatusInfo.MTStatus = "1";//正常状态
                                    thirdMTStatusInfo.MTType = third.Describe;
                                    thirdMTStatusInfo.Remark = third.Name;
                                    thirdMTStatusInfo.RecordID = third.MonitorTreeID.ToString();
                                    //添加 
                                    mTDevStatusInfos.Add(thirdMTStatusInfo);

                                    var monitrofourthList = monitorList.Where(item => item.Lvl == 3 && item.PID == third.MonitorTreeID);

                                    if (monitrofourthList != null && monitrofourthList.Count() > 0)
                                    {
                                        foreach (var fourth in monitrofourthList)
                                        {
                                            MTStatusInfo fourthMTStatusInfo = new MTStatusInfo();
                                            fourthMTStatusInfo.MTPid = thirdMTStatusInfo.MTId;//上传树id
                                            fourthMTStatusInfo.MTId = fourth.MonitorTreeID.ToString();
                                            fourthMTStatusInfo.MTName = fourth.Name;
                                            fourthMTStatusInfo.MTStatus = "1";//正常状态
                                            fourthMTStatusInfo.MTType = fourth.Describe;
                                            fourthMTStatusInfo.Remark = fourth.Name;
                                            fourthMTStatusInfo.RecordID = fourth.MonitorTreeID.ToString();
                                            //添加 
                                            mTDevStatusInfos.Add(fourthMTStatusInfo);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            #region 监测树以下
            //if (false)
            if (monitorList != null && monitorList.Count() > 0)
            {
                foreach (var monitor in monitorList)
                {
                    #region 设备
                    //添加设备
                    var deviceList = viewMonitorTree
                        .Where(item => item.MonitorTreeID == monitor.MonitorTreeID
                            && item.DevID != null
                            && (item.UseType == useType || item.UseType == null))
                        .Select(item => new
                        {
                            item.DevID,//设备id
                            item.DevName,//设备名称
                            item.RunStatus,//运行状态
                            item.AlmStatus,//报警状态
                        })
                        .Distinct();

                    //全部数据
                    if (useType == 2)
                    {
                        deviceList = viewMonitorTree.Where(item => item.MonitorTreeID == monitor.MonitorTreeID && item.DevID != null).
                          Select(item => new
                          {
                              item.DevID,//设备id
                              item.DevName,//设备名称
                              item.RunStatus,//运行状态
                              item.AlmStatus,//报警状态
                          }).Distinct();
                    }

                    if (deviceList != null && deviceList.Count() > 0)
                    {
                        foreach (var device in deviceList)
                        {
                            MTStatusInfo deviceMTStatusInfo = new MTStatusInfo();
                            deviceMTStatusInfo.MTId = (mtId++).ToString();
                            deviceMTStatusInfo.MTPid = monitor.MonitorTreeID.ToString();

                            //备注，如果是停机是4,如果是正常就取AlmStatus状态
                            if (device.RunStatus == (int)EnumRunStatus.Stop)
                            {
                                //停机状态
                                deviceMTStatusInfo.MTName = device.DevName + "(stop)";
                                deviceMTStatusInfo.MTStatus = "4";
                            }
                            else
                            {
                                //运行状态
                                deviceMTStatusInfo.MTName = device.DevName;
                                deviceMTStatusInfo.MTStatus = device.AlmStatus.ToString();
                            }

                            deviceMTStatusInfo.MTType = "5";//设备
                            deviceMTStatusInfo.Remark = device.DevName;
                            deviceMTStatusInfo.RecordID = device.DevID.ToString();

                            //添加 
                            mTDevStatusInfos.Add(deviceMTStatusInfo);

                            #region 测量位置
                            //添加测量位置
                            var measureSiteList = viewMonitorTree.Where(item => item.DevID == device.DevID && item.MSiteID != null).
                                  Select(item => new
                                  {
                                      item.MSiteID,//位置id
                                      item.MeasureSiteName,//位置名称
                                      item.MSiteStatus,//位置状态
                                      item.WSName,//网关名称
                                  }).Distinct();

                            if (measureSiteList != null && measureSiteList.Count() > 0)
                            {
                                foreach (var measureSite in measureSiteList)
                                {
                                    //重复数据，添加随机数
                                    MTStatusInfo measureSiteMTStatusInfo = new MTStatusInfo();

                                    measureSiteMTStatusInfo.MTId = (mtId++).ToString();
                                    measureSiteMTStatusInfo.MTPid = deviceMTStatusInfo.MTId;
                                    measureSiteMTStatusInfo.MTName = measureSite.MeasureSiteName;//;关联到别外一张表
                                    measureSiteMTStatusInfo.MTStatus = measureSite.MSiteStatus.ToString();
                                    measureSiteMTStatusInfo.MTType = "6";//设备
                                    measureSiteMTStatusInfo.Remark = measureSite.WSName;
                                    measureSiteMTStatusInfo.RecordID = measureSite.MSiteID.ToString();

                                    //添加 最后面 
                                    mTDevStatusInfos.Add(measureSiteMTStatusInfo);

                                    #region 设备温度
                                    //添加设备温度
                                    var temperatureList = viewMonitorTree.Where(item => item.MSiteID == measureSite.MSiteID && item.MsiteAlmID != null)
                                        .Select(item => new
                                        {
                                            item.MsiteAlmID,//温度报警id
                                            item.DeviceTemperatureStatus,//设备温度状态
                                        }).Distinct();
                                    if (temperatureList != null && temperatureList.Count() > 0)
                                    {
                                        foreach (var deviceTemperature in temperatureList)
                                        {
                                            MTStatusInfo tempeDeviceMTStatusInfo = new MTStatusInfo();
                                            tempeDeviceMTStatusInfo.MTId = (mtId++).ToString();
                                            tempeDeviceMTStatusInfo.MTPid = measureSiteMTStatusInfo.MTId;
                                            tempeDeviceMTStatusInfo.MTStatus = deviceTemperature.DeviceTemperatureStatus.ToString();
                                            tempeDeviceMTStatusInfo.MTType = "9";//设备温度
                                            tempeDeviceMTStatusInfo.MTName = "设备温度";
                                            tempeDeviceMTStatusInfo.Remark = "设备温度";
                                            tempeDeviceMTStatusInfo.RecordID = deviceTemperature.MsiteAlmID.ToString();

                                            //添加 
                                            mTDevStatusInfos.Add(tempeDeviceMTStatusInfo);
                                        }
                                    }
                                    #endregion

                                    #region 加速度
                                    //添加加速度

                                    var accList = viewMonitorTree
                                        .Where(item => item.MSiteID == measureSite.MSiteID
                                            && item.SingalID != null
                                            && item.VibratingTypeId == (int)EnumVibSignalType.Accelerated)
                                        .Select(item => new
                                        {
                                            item.SingalID,//振动信号id
                                            item.VibratingTypeName,//类型名称
                                            item.SingalStatus,//振动信号状态
                                        })
                                        .Distinct();

                                    if (accList != null && accList.Count() > 0)
                                    {
                                        foreach (var acc in accList)
                                        {
                                            MTStatusInfo accelerationMTStatusInfo = new MTStatusInfo();
                                            accelerationMTStatusInfo.MTId = (mtId++).ToString();
                                            accelerationMTStatusInfo.MTPid = measureSiteMTStatusInfo.MTId;
                                            accelerationMTStatusInfo.MTStatus = acc.SingalStatus.ToString();
                                            accelerationMTStatusInfo.MTType = "7";//加速度
                                            accelerationMTStatusInfo.MTName = acc.VibratingTypeName;
                                            accelerationMTStatusInfo.Remark = acc.VibratingTypeName;
                                            accelerationMTStatusInfo.RecordID = acc.SingalID.ToString();

                                            //添加 
                                            mTDevStatusInfos.Add(accelerationMTStatusInfo);

                                            #region 特征值
                                            //添加特征值
                                            var enginList = viewMonitorTree.Where(item => item.SingalID == acc.SingalID && item.SingalAlmID != null)
                                                .Select(item => new
                                                {
                                                    item.SingalAlmID,//特征值id
                                                    item.EigenTypeName,
                                                    item.EnginStatus,//振动信号状态
                                                }).Distinct();
                                            if (enginList != null && enginList.Count() > 0)
                                            {
                                                foreach (var engin in enginList)
                                                {
                                                    MTStatusInfo signalAlmSetMTStatusInfo = new MTStatusInfo();
                                                    signalAlmSetMTStatusInfo.MTId = (mtId++).ToString();
                                                    signalAlmSetMTStatusInfo.MTPid = accelerationMTStatusInfo.MTId;
                                                    signalAlmSetMTStatusInfo.MTStatus = engin.EnginStatus.ToString();
                                                    signalAlmSetMTStatusInfo.MTType = "8";//特征值
                                                    signalAlmSetMTStatusInfo.MTName = engin.EigenTypeName;
                                                    signalAlmSetMTStatusInfo.Remark = engin.EigenTypeName;
                                                    signalAlmSetMTStatusInfo.RecordID = engin.SingalAlmID.ToString();

                                                    //添加 
                                                    mTDevStatusInfos.Add(signalAlmSetMTStatusInfo);
                                                }
                                            }
                                            #endregion
                                        }
                                    }

                                    #endregion

                                    #region 速度
                                    //添加加速度
                                    var velList = viewMonitorTree
                                        .Where(item => item.MSiteID == measureSite.MSiteID
                                            && item.SingalID != null
                                            && item.VibratingTypeId == (int)EnumVibSignalType.Velocity)
                                        .Select(item => new
                                        {
                                            item.SingalID,//振动信号id
                                            item.VibratingTypeName,//类型名称
                                            item.SingalStatus,//振动信号状态
                                        })
                                        .Distinct();

                                    if (velList != null && velList.Count() > 0)
                                    {
                                        foreach (var vel in velList)
                                        {
                                            MTStatusInfo velMTStatusInfo = new MTStatusInfo();
                                            velMTStatusInfo.MTId = (mtId++).ToString();
                                            velMTStatusInfo.MTPid = measureSiteMTStatusInfo.MTId;
                                            velMTStatusInfo.MTStatus = vel.SingalStatus.ToString();
                                            velMTStatusInfo.MTType = "7";//加速度
                                            velMTStatusInfo.MTName = vel.VibratingTypeName;
                                            velMTStatusInfo.Remark = vel.VibratingTypeName;
                                            velMTStatusInfo.RecordID = vel.SingalID.ToString();

                                            //添加 
                                            mTDevStatusInfos.Add(velMTStatusInfo);

                                            #region 特征值
                                            //添加特征值
                                            var velEnginList = viewMonitorTree.Where(item => item.SingalID == vel.SingalID && item.SingalAlmID != null)
                                                .Select(item => new
                                                {
                                                    item.SingalAlmID,//特征值id
                                                    item.EigenTypeName,
                                                    item.EnginStatus,//振动信号状态
                                                }).Distinct();
                                            if (velEnginList != null && velEnginList.Count() > 0)
                                            {
                                                foreach (var engin in velEnginList)
                                                {
                                                    MTStatusInfo signalAlmSetMTStatusInfo = new MTStatusInfo();
                                                    signalAlmSetMTStatusInfo.MTId = (mtId++).ToString();
                                                    signalAlmSetMTStatusInfo.MTPid = velMTStatusInfo.MTId;
                                                    signalAlmSetMTStatusInfo.MTStatus = engin.EnginStatus.ToString();
                                                    signalAlmSetMTStatusInfo.MTType = "8";//特征值
                                                    signalAlmSetMTStatusInfo.MTName = engin.EigenTypeName;
                                                    signalAlmSetMTStatusInfo.Remark = engin.EigenTypeName;
                                                    signalAlmSetMTStatusInfo.RecordID = engin.SingalAlmID.ToString();

                                                    //添加 
                                                    mTDevStatusInfos.Add(signalAlmSetMTStatusInfo);
                                                }
                                            }
                                            #endregion
                                        }
                                    }

                                    #endregion

                                    #region 包络
                                    //添加加速度
                                    var envelopeList = viewMonitorTree
                                        .Where(item => item.MSiteID == measureSite.MSiteID
                                            && item.SingalID != null
                                            && item.VibratingTypeId == (int)EnumVibSignalType.Envelope)
                                        .Select(item => new
                                        {
                                            item.SingalID,//振动信号id
                                            item.VibratingTypeName,//类型名称
                                            item.SingalStatus,//振动信号状态
                                        })
                                        .Distinct();

                                    if (envelopeList != null && envelopeList.Count() > 0)
                                    {
                                        foreach (var envelope in envelopeList)
                                        {
                                            MTStatusInfo envelopeMTStatusInfo = new MTStatusInfo();
                                            envelopeMTStatusInfo.MTId = (mtId++).ToString();
                                            envelopeMTStatusInfo.MTPid = measureSiteMTStatusInfo.MTId;
                                            envelopeMTStatusInfo.MTStatus = envelope.SingalStatus.ToString();
                                            envelopeMTStatusInfo.MTType = "7";//加速度
                                            envelopeMTStatusInfo.MTName = envelope.VibratingTypeName;
                                            envelopeMTStatusInfo.Remark = envelope.VibratingTypeName;
                                            envelopeMTStatusInfo.RecordID = envelope.SingalID.ToString();

                                            //添加 
                                            mTDevStatusInfos.Add(envelopeMTStatusInfo);

                                            #region 特征值
                                            //添加特征值
                                            var envelopeEnginList = viewMonitorTree.Where(item => item.SingalID == envelope.SingalID && item.SingalAlmID != null)
                                                .Select(item => new
                                                {
                                                    item.SingalAlmID,//特征值id
                                                    item.EigenTypeName,
                                                    item.EnginStatus,//振动信号状态
                                                }).Distinct();
                                            if (envelopeEnginList != null && envelopeEnginList.Count() > 0)
                                            {
                                                foreach (var engin in envelopeEnginList)
                                                {
                                                    MTStatusInfo signalAlmSetMTStatusInfo = new MTStatusInfo();
                                                    signalAlmSetMTStatusInfo.MTId = (mtId++).ToString();
                                                    signalAlmSetMTStatusInfo.MTPid = envelopeMTStatusInfo.MTId;
                                                    signalAlmSetMTStatusInfo.MTStatus = engin.EnginStatus.ToString();
                                                    signalAlmSetMTStatusInfo.MTType = "8";//特征值
                                                    signalAlmSetMTStatusInfo.MTName = engin.EigenTypeName;
                                                    signalAlmSetMTStatusInfo.Remark = engin.EigenTypeName;
                                                    signalAlmSetMTStatusInfo.RecordID = engin.SingalAlmID.ToString();

                                                    //添加 
                                                    mTDevStatusInfos.Add(signalAlmSetMTStatusInfo);
                                                }
                                            }
                                            #endregion
                                        }
                                    }

                                    #endregion

                                    #region 位移
                                    //添加加速度
                                    var displacementNameList = viewMonitorTree
                                        .Where(item => item.MSiteID == measureSite.MSiteID
                                            && item.SingalID != null
                                            && item.VibratingTypeId == (int)EnumVibSignalType.Displacement)
                                        .Select(item => new
                                        {
                                            item.SingalID,//振动信号id
                                            item.VibratingTypeName,//类型名称
                                            item.SingalStatus,//振动信号状态
                                        })
                                        .Distinct();

                                    if (displacementNameList != null && displacementNameList.Count() > 0)
                                    {
                                        foreach (var displacement in displacementNameList)
                                        {
                                            MTStatusInfo displacementMTStatusInfo = new MTStatusInfo();
                                            displacementMTStatusInfo.MTId = (mtId++).ToString();
                                            displacementMTStatusInfo.MTPid = measureSiteMTStatusInfo.MTId;
                                            displacementMTStatusInfo.MTStatus = displacement.SingalStatus.ToString();
                                            displacementMTStatusInfo.MTType = "7";//加速度
                                            displacementMTStatusInfo.MTName = displacement.VibratingTypeName;
                                            displacementMTStatusInfo.Remark = displacement.VibratingTypeName;
                                            displacementMTStatusInfo.RecordID = displacement.SingalID.ToString();

                                            //添加 
                                            mTDevStatusInfos.Add(displacementMTStatusInfo);

                                            #region 特征值
                                            //添加特征值
                                            var displacementEnginList = viewMonitorTree
                                                .Where(item => item.SingalID == displacement.SingalID && item.SingalAlmID != null)
                                                .Select(item => new
                                                {
                                                    item.SingalAlmID,//特征值id
                                                    item.EigenTypeName,
                                                    item.EnginStatus,//振动信号状态
                                                })
                                                .Distinct();
                                            if (displacementEnginList != null && displacementEnginList.Count() > 0)
                                            {
                                                foreach (var engin in displacementEnginList)
                                                {
                                                    MTStatusInfo signalAlmSetMTStatusInfo = new MTStatusInfo();
                                                    signalAlmSetMTStatusInfo.MTId = (mtId++).ToString();
                                                    signalAlmSetMTStatusInfo.MTPid = displacementMTStatusInfo.MTId;
                                                    signalAlmSetMTStatusInfo.MTStatus = engin.EnginStatus.ToString();
                                                    signalAlmSetMTStatusInfo.MTType = "8";//特征值
                                                    signalAlmSetMTStatusInfo.MTName = engin.EigenTypeName;
                                                    signalAlmSetMTStatusInfo.Remark = engin.EigenTypeName;
                                                    signalAlmSetMTStatusInfo.RecordID = engin.SingalAlmID.ToString();

                                                    //添加 
                                                    mTDevStatusInfos.Add(signalAlmSetMTStatusInfo);
                                                }
                                            }
                                            #endregion
                                        }
                                    }

                                    #endregion

                                    #region LQ
                                    //添加加速度
                                    var lqList = viewMonitorTree
                                        .Where(item => item.MSiteID == measureSite.MSiteID
                                            && item.SingalID != null && item.VibratingTypeId == (int)EnumVibSignalType.LQ)
                                        .Select(item => new
                                        {
                                            item.SingalID,//振动信号id
                                            item.VibratingTypeName,//类型名称
                                            item.SingalStatus,//振动信号状态
                                        })
                                        .Distinct();

                                    if (lqList != null && lqList.Count() > 0)
                                    {
                                        foreach (var lq in lqList)
                                        {
                                            MTStatusInfo lqMTStatusInfo = new MTStatusInfo();
                                            lqMTStatusInfo.MTId = (mtId++).ToString();
                                            lqMTStatusInfo.MTPid = measureSiteMTStatusInfo.MTId;
                                            lqMTStatusInfo.MTStatus = lq.SingalStatus.ToString();
                                            lqMTStatusInfo.MTType = "7";//加速度
                                            lqMTStatusInfo.MTName = lq.VibratingTypeName;
                                            lqMTStatusInfo.Remark = lq.VibratingTypeName;
                                            lqMTStatusInfo.RecordID = lq.SingalID.ToString();

                                            //添加 
                                            mTDevStatusInfos.Add(lqMTStatusInfo);

                                            #region 特征值
                                            //添加特征值
                                            var lqEnginList = viewMonitorTree.Where(item => item.SingalID == lq.SingalID && item.SingalAlmID != null)
                                                .Select(item => new
                                                {
                                                    item.SingalAlmID,//特征值id
                                                    item.EigenTypeName,
                                                    item.EnginStatus,//振动信号状态
                                                }).Distinct();
                                            if (lqEnginList != null && lqEnginList.Count() > 0)
                                            {
                                                foreach (var engin in lqEnginList)
                                                {
                                                    MTStatusInfo signalAlmSetMTStatusInfo = new MTStatusInfo();
                                                    signalAlmSetMTStatusInfo.MTId = (mtId++).ToString();
                                                    signalAlmSetMTStatusInfo.MTPid = lqMTStatusInfo.MTId;
                                                    signalAlmSetMTStatusInfo.MTStatus = engin.EnginStatus.ToString();
                                                    signalAlmSetMTStatusInfo.MTType = "8";//特征值
                                                    signalAlmSetMTStatusInfo.MTName = engin.EigenTypeName;
                                                    signalAlmSetMTStatusInfo.Remark = engin.EigenTypeName;
                                                    signalAlmSetMTStatusInfo.RecordID = engin.SingalAlmID.ToString();

                                                    //添加 
                                                    mTDevStatusInfos.Add(signalAlmSetMTStatusInfo);
                                                }
                                            }
                                            #endregion
                                        }
                                    }

                                    #endregion
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
            }

            #endregion

            return mTDevStatusInfos;
        }
        #endregion

        #region 获取监测树，通过类型，通过用户Id和级别、设备是否使用
        /// <summary>
        /// 获取监测树，通过类型
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="level">级别</param>
        /// <param name="useType">设备是否使用</param>
        /// <returns></returns>
        private List<MTStatusInfo> GetMonitorTreeList(int userId, int level, int useType = 0)
        {
            List<MTStatusInfo> mTDevStatusInfos = new List<MTStatusInfo>();

            #region 添加根节点
            //添加根节点
            MTStatusInfo mTDevStatusInfo = new MTStatusInfo() { MTId = "0", MTPid = "0" };
            mTDevStatusInfos.Add(mTDevStatusInfo);
            #endregion

            var sqlViewMonitorTree = ConstObject.SQL_View_MonitorTree;
            var viewMonitorTree = new iCMSDbContext().Database.SqlQuery<View_MonitorTree>(sqlViewMonitorTree).ToList<View_MonitorTree>();

            var tree = GetDeviceTree(userId);
            var monitorTreeDBList = monitorRepository.GetDatas<MonitorTree>(item => true, false);
            var deviceDBList = deviceRepository.GetDatas<Device>(item => true, false);

            #region 监测树
            var monitorRootList = tree.Where(item => item.Level == 1);
            var mtId = tree.Max(item => item.TreeId) + 1;
            if (monitorRootList != null && monitorRootList.Count() > 0)
            {
                foreach (var monitor in monitorRootList)
                {
                    MTStatusInfo mTStatusInfo = new MTStatusInfo();
                    var info = monitorTreeDBList.Where(item => item.MonitorTreeID == monitor.TrueId).FirstOrDefault();

                    mTStatusInfo.MTPid = "0";//上传树id
                    mTStatusInfo.MTId = monitor.TreeId.ToString();
                    mTStatusInfo.MTName = monitor.TreeName;
                    mTStatusInfo.MTStatus = "1";//正常状态
                    mTStatusInfo.MTType = info.Des;
                    mTStatusInfo.Remark = monitor.TreeName;
                    mTStatusInfo.RecordID = monitor.TrueId.ToString();
                    //添加 
                    mTDevStatusInfos.Add(mTStatusInfo);

                    var monitorSecondList = tree.Where(item => item.Level == 2 && item.ParentId == monitor.TreeId);

                    if (monitorSecondList != null && monitorSecondList.Count() > 0)
                    {
                        foreach (var second in monitorSecondList)
                        {
                            MTStatusInfo secondMTStatusInfo = new MTStatusInfo();
                            info = monitorTreeDBList.Where(item => item.MonitorTreeID == monitor.TrueId).FirstOrDefault();
                            secondMTStatusInfo.MTPid = mTStatusInfo.MTId;//上传树id
                            secondMTStatusInfo.MTId = second.TreeId.ToString();
                            secondMTStatusInfo.MTName = second.TreeName;
                            secondMTStatusInfo.MTStatus = "1";//正常状态
                            secondMTStatusInfo.MTType = info.Des;
                            secondMTStatusInfo.Remark = second.TreeName;
                            secondMTStatusInfo.RecordID = second.TrueId.ToString();
                            //添加 
                            mTDevStatusInfos.Add(secondMTStatusInfo);

                            var monitroThirdList = tree.Where(item => item.Level == 3 && item.ParentId == second.TreeId);

                            if (monitroThirdList != null && monitroThirdList.Count() > 0)
                            {
                                foreach (var third in monitroThirdList)
                                {
                                    MTStatusInfo thirdMTStatusInfo = new MTStatusInfo();
                                    info = monitorTreeDBList.Where(item => item.MonitorTreeID == monitor.TrueId).FirstOrDefault();
                                    thirdMTStatusInfo.MTPid = secondMTStatusInfo.MTId;//上传树id
                                    thirdMTStatusInfo.MTId = third.TreeId.ToString();
                                    thirdMTStatusInfo.MTName = third.TreeName;
                                    thirdMTStatusInfo.MTStatus = "1";//正常状态
                                    thirdMTStatusInfo.MTType = info.Des;
                                    thirdMTStatusInfo.Remark = third.TreeName;
                                    thirdMTStatusInfo.RecordID = third.TrueId.ToString();
                                    //添加 
                                    mTDevStatusInfos.Add(thirdMTStatusInfo);

                                    var monitrofourthList = tree.Where(item => item.Level == 4 && item.ParentId == third.TreeId);

                                    if (monitrofourthList != null && monitrofourthList.Count() > 0)
                                    {
                                        foreach (var fourth in monitrofourthList)
                                        {
                                            MTStatusInfo fourthMTStatusInfo = new MTStatusInfo();
                                            info = monitorTreeDBList.Where(item => item.MonitorTreeID == monitor.TrueId).FirstOrDefault();
                                            fourthMTStatusInfo.MTPid = thirdMTStatusInfo.MTId;//上传树id
                                            fourthMTStatusInfo.MTId = fourth.TreeId.ToString();
                                            fourthMTStatusInfo.MTName = fourth.TreeName;
                                            fourthMTStatusInfo.MTStatus = "1";//正常状态
                                            fourthMTStatusInfo.MTType = info.Des;
                                            fourthMTStatusInfo.Remark = fourth.TreeName;
                                            fourthMTStatusInfo.RecordID = fourth.TrueId.ToString();
                                            //添加 
                                            mTDevStatusInfos.Add(fourthMTStatusInfo);

                                            #region 监测树以下
                                            #region 设备
                                            //添加设备
                                            var deviceList = tree.Where(item => item.Level == 5 && item.ParentId == fourth.TreeId);

                                            if (deviceList != null && deviceList.Count() > 0)
                                            {
                                                foreach (var device in deviceList)
                                                {
                                                    MTStatusInfo deviceMTStatusInfo = new MTStatusInfo();
                                                    deviceMTStatusInfo.MTId = device.TreeId.ToString();
                                                    deviceMTStatusInfo.MTPid = fourth.TreeId.ToString();
                                                    var deviceDB = deviceDBList.Where(item => item.MonitorTreeID == monitor.TrueId).FirstOrDefault();
                                                    //备注，如果是停机是4,如果是正常就取AlmStatus状态
                                                    if (deviceDB.RunStatus == (int)EnumRunStatus.Stop)
                                                    {

                                                        if (deviceDB.UseType == 1)
                                                        {
                                                            //备用设备
                                                            deviceMTStatusInfo.MTName = deviceDB.DevName + "<span style='color:#808080'>(备用设备)</span>";
                                                            deviceMTStatusInfo.MTStatus = "4";
                                                        }
                                                        else
                                                        {
                                                            //停机状态
                                                            deviceMTStatusInfo.MTName = deviceDB.DevName + "(stop)";
                                                            deviceMTStatusInfo.MTStatus = "4";
                                                        }

                                                    }
                                                    else
                                                    {
                                                        //运行状态
                                                        deviceMTStatusInfo.MTName = deviceDB.DevName;
                                                        deviceMTStatusInfo.MTStatus = deviceDB.AlmStatus.ToString();
                                                    }

                                                    deviceMTStatusInfo.MTType = "5";//设备
                                                    deviceMTStatusInfo.Remark = deviceDB.DevName;
                                                    deviceMTStatusInfo.RecordID = deviceDB.DevID.ToString();

                                                    //添加 
                                                    mTDevStatusInfos.Add(deviceMTStatusInfo);

                                                    #region 测量位置
                                                    //添加测量位置
                                                    var measureSiteList = viewMonitorTree.Where(item => item.DevID == deviceDB.DevID && item.MSiteID != null).
                                                          Select(item => new
                                                          {
                                                              item.MSiteID,//位置id
                                                              item.MeasureSiteName,//位置名称
                                                              item.MSiteStatus,//位置状态
                                                              item.WSName,//网关名称
                                                          }).Distinct();

                                                    if (measureSiteList != null && measureSiteList.Count() > 0)
                                                    {
                                                        foreach (var measureSite in measureSiteList)
                                                        {
                                                            //重复数据，添加随机数
                                                            MTStatusInfo measureSiteMTStatusInfo = new MTStatusInfo();

                                                            measureSiteMTStatusInfo.MTId = (mtId++).ToString();
                                                            measureSiteMTStatusInfo.MTPid = deviceMTStatusInfo.MTId;
                                                            measureSiteMTStatusInfo.MTName = measureSite.MeasureSiteName;//;关联到别外一张表
                                                            measureSiteMTStatusInfo.MTStatus = measureSite.MSiteStatus.ToString();
                                                            measureSiteMTStatusInfo.MTType = "6";//设备
                                                            measureSiteMTStatusInfo.Remark = measureSite.WSName;
                                                            measureSiteMTStatusInfo.RecordID = measureSite.MSiteID.ToString();

                                                            //添加 最后面 
                                                            mTDevStatusInfos.Add(measureSiteMTStatusInfo);


                                                            #region 设备温度
                                                            //添加设备温度
                                                            var temperatureList = viewMonitorTree.Where(item => item.MSiteID == measureSite.MSiteID && item.MsiteAlmID != null)
                                                                .Select(item => new
                                                                {
                                                                    item.MsiteAlmID,//温度报警id
                                                                    item.DeviceTemperatureStatus,//设备温度状态
                                                                }).Distinct();
                                                            if (temperatureList != null && temperatureList.Count() > 0)
                                                            {
                                                                foreach (var deviceTemperature in temperatureList)
                                                                {
                                                                    MTStatusInfo tempeDeviceMTStatusInfo = new MTStatusInfo();
                                                                    tempeDeviceMTStatusInfo.MTId = (mtId++).ToString();
                                                                    tempeDeviceMTStatusInfo.MTPid = measureSiteMTStatusInfo.MTId;
                                                                    tempeDeviceMTStatusInfo.MTStatus = deviceTemperature.DeviceTemperatureStatus.ToString();
                                                                    tempeDeviceMTStatusInfo.MTType = "9";//设备温度
                                                                    tempeDeviceMTStatusInfo.MTName = "设备温度";
                                                                    tempeDeviceMTStatusInfo.Remark = "设备温度";
                                                                    tempeDeviceMTStatusInfo.RecordID = deviceTemperature.MsiteAlmID.ToString();

                                                                    //添加 
                                                                    mTDevStatusInfos.Add(tempeDeviceMTStatusInfo);
                                                                }
                                                            }
                                                            #endregion

                                                            #region 加速度
                                                            //添加加速度
                                                            var accList = viewMonitorTree
                                                                .Where(item => item.MSiteID == measureSite.MSiteID
                                                                    && item.SingalID != null
                                                                    && item.VibratingTypeId == (int)EnumVibSignalType.Accelerated)
                                                                .Select(item => new
                                                                {
                                                                    item.SingalID,//振动信号id
                                                                    item.VibratingTypeName,//类型名称
                                                                    item.SingalStatus,//振动信号状态
                                                                })
                                                                .Distinct();

                                                            if (accList != null && accList.Count() > 0)
                                                            {
                                                                foreach (var acc in accList)
                                                                {
                                                                    MTStatusInfo accelerationMTStatusInfo = new MTStatusInfo();
                                                                    accelerationMTStatusInfo.MTId = (mtId++).ToString();
                                                                    accelerationMTStatusInfo.MTPid = measureSiteMTStatusInfo.MTId;
                                                                    accelerationMTStatusInfo.MTStatus = acc.SingalStatus.ToString();
                                                                    accelerationMTStatusInfo.MTType = "7";//加速度
                                                                    accelerationMTStatusInfo.MTName = acc.VibratingTypeName;
                                                                    accelerationMTStatusInfo.Remark = acc.VibratingTypeName;
                                                                    accelerationMTStatusInfo.RecordID = acc.SingalID.ToString();

                                                                    //添加 
                                                                    mTDevStatusInfos.Add(accelerationMTStatusInfo);

                                                                    #region 特征值
                                                                    //添加特征值
                                                                    var enginList = viewMonitorTree.Where(item => item.SingalID == acc.SingalID && item.SingalAlmID != null)
                                                                        .Select(item => new
                                                                        {
                                                                            item.SingalAlmID,//特征值id
                                                                            item.EigenTypeName,
                                                                            item.EnginStatus,//振动信号状态
                                                                        }).Distinct();
                                                                    if (enginList != null && enginList.Count() > 0)
                                                                    {
                                                                        foreach (var engin in enginList)
                                                                        {
                                                                            MTStatusInfo signalAlmSetMTStatusInfo = new MTStatusInfo();
                                                                            signalAlmSetMTStatusInfo.MTId = (mtId++).ToString();
                                                                            signalAlmSetMTStatusInfo.MTPid = accelerationMTStatusInfo.MTId;
                                                                            signalAlmSetMTStatusInfo.MTStatus = engin.EnginStatus.ToString();
                                                                            signalAlmSetMTStatusInfo.MTType = "8";//特征值
                                                                            signalAlmSetMTStatusInfo.MTName = engin.EigenTypeName;
                                                                            signalAlmSetMTStatusInfo.Remark = engin.EigenTypeName;
                                                                            signalAlmSetMTStatusInfo.RecordID = engin.SingalAlmID.ToString();

                                                                            //添加 
                                                                            mTDevStatusInfos.Add(signalAlmSetMTStatusInfo);
                                                                        }
                                                                    }
                                                                    #endregion
                                                                }
                                                            }

                                                            #endregion

                                                            #region 速度
                                                            //添加加速度
                                                            var velList = viewMonitorTree
                                                                .Where(item => item.MSiteID == measureSite.MSiteID
                                                                    && item.SingalID != null
                                                                    && item.VibratingTypeId == (int)EnumVibSignalType.Velocity)
                                                                .Select(item => new
                                                                {
                                                                    item.SingalID,//振动信号id
                                                                    item.VibratingTypeName,//类型名称
                                                                    item.SingalStatus,//振动信号状态
                                                                })
                                                                .Distinct();

                                                            if (velList != null && velList.Count() > 0)
                                                            {
                                                                foreach (var vel in velList)
                                                                {
                                                                    MTStatusInfo velMTStatusInfo = new MTStatusInfo();
                                                                    velMTStatusInfo.MTId = (mtId++).ToString();
                                                                    velMTStatusInfo.MTPid = measureSiteMTStatusInfo.MTId;
                                                                    velMTStatusInfo.MTStatus = vel.SingalStatus.ToString();
                                                                    velMTStatusInfo.MTType = "7";//加速度
                                                                    velMTStatusInfo.MTName = vel.VibratingTypeName;
                                                                    velMTStatusInfo.Remark = vel.VibratingTypeName;
                                                                    velMTStatusInfo.RecordID = vel.SingalID.ToString();

                                                                    //添加 
                                                                    mTDevStatusInfos.Add(velMTStatusInfo);

                                                                    #region 特征值
                                                                    //添加特征值
                                                                    var velEnginList = viewMonitorTree.Where(item => item.SingalID == vel.SingalID && item.SingalAlmID != null)
                                                                        .Select(item => new
                                                                        {
                                                                            item.SingalAlmID,//特征值id
                                                                            item.EigenTypeName,
                                                                            item.EnginStatus,//振动信号状态
                                                                        }).Distinct();
                                                                    if (velEnginList != null && velEnginList.Count() > 0)
                                                                    {
                                                                        foreach (var engin in velEnginList)
                                                                        {
                                                                            MTStatusInfo signalAlmSetMTStatusInfo = new MTStatusInfo();
                                                                            signalAlmSetMTStatusInfo.MTId = (mtId++).ToString();
                                                                            signalAlmSetMTStatusInfo.MTPid = velMTStatusInfo.MTId;
                                                                            signalAlmSetMTStatusInfo.MTStatus = engin.EnginStatus.ToString();
                                                                            signalAlmSetMTStatusInfo.MTType = "8";//特征值
                                                                            signalAlmSetMTStatusInfo.MTName = engin.EigenTypeName;
                                                                            signalAlmSetMTStatusInfo.Remark = engin.EigenTypeName;
                                                                            signalAlmSetMTStatusInfo.RecordID = engin.SingalAlmID.ToString();

                                                                            //添加 
                                                                            mTDevStatusInfos.Add(signalAlmSetMTStatusInfo);
                                                                        }
                                                                    }
                                                                    #endregion
                                                                }
                                                            }

                                                            #endregion

                                                            #region 包络
                                                            //添加加速度
                                                            var envelopeList = viewMonitorTree
                                                                .Where(item => item.MSiteID == measureSite.MSiteID
                                                                    && item.SingalID != null
                                                                    && item.VibratingTypeId == (int)EnumVibSignalType.Envelope)
                                                                .Select(item => new
                                                                {
                                                                    item.SingalID,//振动信号id
                                                                    item.VibratingTypeName,//类型名称
                                                                    item.SingalStatus,//振动信号状态
                                                                })
                                                                .Distinct();

                                                            if (envelopeList != null && envelopeList.Count() > 0)
                                                            {
                                                                foreach (var envelope in envelopeList)
                                                                {
                                                                    MTStatusInfo envelopeMTStatusInfo = new MTStatusInfo();
                                                                    envelopeMTStatusInfo.MTId = (mtId++).ToString();
                                                                    envelopeMTStatusInfo.MTPid = measureSiteMTStatusInfo.MTId;
                                                                    envelopeMTStatusInfo.MTStatus = envelope.SingalStatus.ToString();
                                                                    envelopeMTStatusInfo.MTType = "7";//加速度
                                                                    envelopeMTStatusInfo.MTName = envelope.VibratingTypeName;
                                                                    envelopeMTStatusInfo.Remark = envelope.VibratingTypeName;
                                                                    envelopeMTStatusInfo.RecordID = envelope.SingalID.ToString();

                                                                    //添加 
                                                                    mTDevStatusInfos.Add(envelopeMTStatusInfo);

                                                                    #region 特征值
                                                                    //添加特征值
                                                                    var envelopeEnginList = viewMonitorTree.Where(item => item.SingalID == envelope.SingalID && item.SingalAlmID != null)
                                                                        .Select(item => new
                                                                        {
                                                                            item.SingalAlmID,//特征值id
                                                                            item.EigenTypeName,
                                                                            item.EnginStatus,//振动信号状态
                                                                        }).Distinct();
                                                                    if (envelopeEnginList != null && envelopeEnginList.Count() > 0)
                                                                    {
                                                                        foreach (var engin in envelopeEnginList)
                                                                        {
                                                                            MTStatusInfo signalAlmSetMTStatusInfo = new MTStatusInfo();
                                                                            signalAlmSetMTStatusInfo.MTId = (mtId++).ToString();
                                                                            signalAlmSetMTStatusInfo.MTPid = envelopeMTStatusInfo.MTId;
                                                                            signalAlmSetMTStatusInfo.MTStatus = engin.EnginStatus.ToString();
                                                                            signalAlmSetMTStatusInfo.MTType = "8";//特征值
                                                                            signalAlmSetMTStatusInfo.MTName = engin.EigenTypeName;
                                                                            signalAlmSetMTStatusInfo.Remark = engin.EigenTypeName;
                                                                            signalAlmSetMTStatusInfo.RecordID = engin.SingalAlmID.ToString();

                                                                            //添加 
                                                                            mTDevStatusInfos.Add(signalAlmSetMTStatusInfo);
                                                                        }
                                                                    }
                                                                    #endregion
                                                                }
                                                            }

                                                            #endregion

                                                            #region 位移
                                                            //添加加速度
                                                            var displacementNameList = viewMonitorTree
                                                                .Where(item => item.MSiteID == measureSite.MSiteID
                                                                    && item.SingalID != null
                                                                    && item.VibratingTypeId == (int)EnumVibSignalType.Displacement)
                                                                .Select(item => new
                                                                {
                                                                    item.SingalID,//振动信号id
                                                                    item.VibratingTypeName,//类型名称
                                                                    item.SingalStatus,//振动信号状态
                                                                })
                                                                .Distinct();

                                                            if (displacementNameList != null && displacementNameList.Count() > 0)
                                                            {
                                                                foreach (var displacement in displacementNameList)
                                                                {
                                                                    MTStatusInfo displacementMTStatusInfo = new MTStatusInfo();
                                                                    displacementMTStatusInfo.MTId = (mtId++).ToString();
                                                                    displacementMTStatusInfo.MTPid = measureSiteMTStatusInfo.MTId;
                                                                    displacementMTStatusInfo.MTStatus = displacement.SingalStatus.ToString();
                                                                    displacementMTStatusInfo.MTType = "7";//加速度
                                                                    displacementMTStatusInfo.MTName = displacement.VibratingTypeName;
                                                                    displacementMTStatusInfo.Remark = displacement.VibratingTypeName;
                                                                    displacementMTStatusInfo.RecordID = displacement.SingalID.ToString();

                                                                    //添加 
                                                                    mTDevStatusInfos.Add(displacementMTStatusInfo);

                                                                    #region 特征值
                                                                    //添加特征值
                                                                    var displacementEnginList = viewMonitorTree
                                                                        .Where(item => item.SingalID == displacement.SingalID && item.SingalAlmID != null)
                                                                        .Select(item => new
                                                                        {
                                                                            item.SingalAlmID,//特征值id
                                                                            item.EigenTypeName,
                                                                            item.EnginStatus,//振动信号状态
                                                                        })
                                                                        .Distinct();
                                                                    if (displacementEnginList != null && displacementEnginList.Count() > 0)
                                                                    {
                                                                        foreach (var engin in displacementEnginList)
                                                                        {
                                                                            MTStatusInfo signalAlmSetMTStatusInfo = new MTStatusInfo();
                                                                            signalAlmSetMTStatusInfo.MTId = (mtId++).ToString();
                                                                            signalAlmSetMTStatusInfo.MTPid = displacementMTStatusInfo.MTId;
                                                                            signalAlmSetMTStatusInfo.MTStatus = engin.EnginStatus.ToString();
                                                                            signalAlmSetMTStatusInfo.MTType = "8";//特征值
                                                                            signalAlmSetMTStatusInfo.MTName = engin.EigenTypeName;
                                                                            signalAlmSetMTStatusInfo.Remark = engin.EigenTypeName;
                                                                            signalAlmSetMTStatusInfo.RecordID = engin.SingalAlmID.ToString();

                                                                            //添加 
                                                                            mTDevStatusInfos.Add(signalAlmSetMTStatusInfo);
                                                                        }
                                                                    }
                                                                    #endregion
                                                                }
                                                            }

                                                            #endregion

                                                            #region LQ
                                                            //添加加速度
                                                            var lqList = viewMonitorTree
                                                                .Where(item => item.MSiteID == measureSite.MSiteID
                                                                    && item.SingalID != null && item.VibratingTypeId == (int)EnumVibSignalType.LQ)
                                                                .Select(item => new
                                                                {
                                                                    item.SingalID,//振动信号id
                                                                    item.VibratingTypeName,//类型名称
                                                                    item.SingalStatus,//振动信号状态
                                                                })
                                                                .Distinct();

                                                            if (lqList != null && lqList.Count() > 0)
                                                            {
                                                                foreach (var lq in lqList)
                                                                {
                                                                    MTStatusInfo lqMTStatusInfo = new MTStatusInfo();
                                                                    lqMTStatusInfo.MTId = (mtId++).ToString();
                                                                    lqMTStatusInfo.MTPid = measureSiteMTStatusInfo.MTId;
                                                                    lqMTStatusInfo.MTStatus = lq.SingalStatus.ToString();
                                                                    lqMTStatusInfo.MTType = "7";//加速度
                                                                    lqMTStatusInfo.MTName = lq.VibratingTypeName;
                                                                    lqMTStatusInfo.Remark = lq.VibratingTypeName;
                                                                    lqMTStatusInfo.RecordID = lq.SingalID.ToString();

                                                                    //添加 
                                                                    mTDevStatusInfos.Add(lqMTStatusInfo);

                                                                    #region 特征值
                                                                    //添加特征值
                                                                    var lqEnginList = viewMonitorTree.Where(item => item.SingalID == lq.SingalID && item.SingalAlmID != null)
                                                                        .Select(item => new
                                                                        {
                                                                            item.SingalAlmID,//特征值id
                                                                            item.EigenTypeName,
                                                                            item.EnginStatus,//振动信号状态
                                                                        }).Distinct();
                                                                    if (lqEnginList != null && lqEnginList.Count() > 0)
                                                                    {
                                                                        foreach (var engin in lqEnginList)
                                                                        {
                                                                            MTStatusInfo signalAlmSetMTStatusInfo = new MTStatusInfo();
                                                                            signalAlmSetMTStatusInfo.MTId = (mtId++).ToString();
                                                                            signalAlmSetMTStatusInfo.MTPid = lqMTStatusInfo.MTId;
                                                                            signalAlmSetMTStatusInfo.MTStatus = engin.EnginStatus.ToString();
                                                                            signalAlmSetMTStatusInfo.MTType = "8";//特征值
                                                                            signalAlmSetMTStatusInfo.MTName = engin.EigenTypeName;
                                                                            signalAlmSetMTStatusInfo.Remark = engin.EigenTypeName;
                                                                            signalAlmSetMTStatusInfo.RecordID = engin.SingalAlmID.ToString();

                                                                            //添加 
                                                                            mTDevStatusInfos.Add(signalAlmSetMTStatusInfo);
                                                                        }
                                                                    }
                                                                    #endregion
                                                                }
                                                            }

                                                            #endregion
                                                        }
                                                    }
                                                    #endregion
                                                }
                                            }
                                            #endregion
                                            #endregion
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            return mTDevStatusInfos;
        }
        #endregion

        #region 获取被监测设备列表树
        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2016-07-29
        /// 创建记录：获取被监测设备列表树
        /// </summary>
        /// <returns>监测设备列表树</returns>
        private List<MTStatusInfo> GetServerTreeList()
        {
            List<MTStatusInfo> mTWSNStatusInfos = new List<MTStatusInfo>();

            #region 添加根节点
            //添加根节点
            MTStatusInfo mTWSStatusInfo = new MTStatusInfo() { MTId = "0", MTPid = "0" };
            mTWSNStatusInfos.Add(mTWSStatusInfo);
            #endregion

            //添加根节点
            MTStatusInfo rootNode = new MTStatusInfo()
            {
                MTId = "10000",
                MTPid = mTWSStatusInfo.MTPid,
                MTName = "Server",
                MTType = "12",
                RecordID = "0",
            };
            mTWSNStatusInfos.Add(rootNode);

            var dbContext = new iCMSDbContext();
            var query =
                (from wg in dbContext.WG
                 join ws in dbContext.WS on wg.WGID equals ws.WGID into wg_ws
                 from ws in wg_ws.DefaultIfEmpty()
                 join measureSite in dbContext.Measuresite on ws.WSID equals measureSite.WSID into ws_measureSite
                 from measureSite in ws_measureSite.DefaultIfEmpty()
                 join temperature in dbContext.TempeWSSetMsitealm on measureSite.MSiteID equals temperature.MsiteID into measureSite_temperature
                 from temperature in measureSite_temperature.DefaultIfEmpty()
                 join voltage in dbContext.VoltageSetMsitealm on measureSite.MSiteID equals voltage.MsiteID into measureSite_voltage
                 from voltage in measureSite_voltage.DefaultIfEmpty()
                 select new
                 {
                     WGId = wg.WGID,//网关id
                     WGName = wg.WGName,//网关名称
                     WSID = ws == null ? 0 : ws.WSID,
                     WSName = ws.WSName,//传感器名称
                     WGLinkStatus = wg.LinkStatus,//网关状态
                     WSLinkStatus = ws == null ? -1 : ws.LinkStatus,//传感器状态
                     TemperatureId = temperature == null ? 0 : temperature.MsiteAlmID,//温度id
                     TemperatureStatus = temperature == null ? 0 : temperature.Status,//温度状态
                     VoltageId = voltage == null ? 0 : voltage.MsiteAlmID,//电压id
                     VoltageStatus = voltage == null ? -1 : voltage.Status,//电压状态
                 })
                .ToList();

            //增加自增id
            var queryList = query.OrderBy(item => item.WGId).Select((item, index) => new
            {
                RowId = index + 1,
                WGId = item.WGId,//网关id
                WGName = item.WGName,//网关名称
                WSId = item.WSID,
                WSName = item.WSName,//传感器名称
                WGLinkStatus = item.WGLinkStatus,//网关状态
                WSLinkStatus = item.WSLinkStatus,//传感器状态
                TemperatureId = item.TemperatureId,//温度id
                TemperatureStatus = item.TemperatureStatus,//温度状态
                VoltageId = item.VoltageId,//电压id
                VoltageStatus = item.VoltageStatus,//电压状态
            }).ToList();

            var wgIdList = queryList.Select(item => item.WGId).Distinct().ToList<int>();

            int mtId = 1;//为树的自增id
            foreach (var wgId in wgIdList)
            {
                #region 添加网关信息
                //添加网关信息
                var wg = queryList.Where(item => item.WGId == wgId).FirstOrDefault();
                if (wg != null)
                {
                    MTStatusInfo wGMTStatusInfo = new MTStatusInfo();
                    wGMTStatusInfo.MTId = (mtId++).ToString();
                    wGMTStatusInfo.MTPid = rootNode.MTId.ToString();
                    wGMTStatusInfo.MTStatus = wg.WGLinkStatus.ToString();

                    wGMTStatusInfo.MTType = "13";//网关
                    wGMTStatusInfo.MTName = wg.WGName;
                    wGMTStatusInfo.Remark = wg.WGName;
                    wGMTStatusInfo.RecordID = wg.WGId.ToString();
                    //添加
                    mTWSNStatusInfos.Add(wGMTStatusInfo);

                    //添加传感器，一个网关对应多个传感器
                    var wsIdList = queryList.Where(item => item.WGId == wgId).Select(item => item.WSId).Distinct();

                    foreach (var wsId in wsIdList)
                    {
                        if (wsId != 0)
                        {
                            var ws = queryList.Where(item => item.WSId == wsId).FirstOrDefault();
                            if (ws != null)
                            {
                                MTStatusInfo wSMTStatusInfo = new MTStatusInfo();
                                wSMTStatusInfo.MTId = (mtId++).ToString();
                                wSMTStatusInfo.MTPid = wGMTStatusInfo.MTId;
                                wSMTStatusInfo.MTStatus = ws.WSLinkStatus.ToString();
                                wSMTStatusInfo.MTType = "14";//传感器
                                wSMTStatusInfo.MTName = ws.WSName;
                                wSMTStatusInfo.Remark = ws.WSName;
                                wSMTStatusInfo.RecordID = ws.WSId.ToString();
                                //添加
                                mTWSNStatusInfos.Add(wSMTStatusInfo);

                                #region 传感器温度
                                //添加
                                var temperature = queryList.Where(item => item.WSId == ws.WSId && item.TemperatureId != 0).FirstOrDefault();
                                if (temperature != null)
                                {
                                    if (temperature.TemperatureId != 0)
                                    {
                                        MTStatusInfo tempeWSMTStatusInfo = new MTStatusInfo();
                                        tempeWSMTStatusInfo.MTId = (mtId++).ToString();
                                        tempeWSMTStatusInfo.MTPid = wSMTStatusInfo.MTId;
                                        tempeWSMTStatusInfo.MTStatus = temperature.TemperatureStatus.ToString();
                                        tempeWSMTStatusInfo.MTType = "10";//传感器温度
                                        tempeWSMTStatusInfo.MTName = "传感器温度";
                                        tempeWSMTStatusInfo.Remark = "传感器温度";
                                        tempeWSMTStatusInfo.RecordID = temperature.TemperatureId.ToString();

                                        //添加
                                        mTWSNStatusInfos.Add(tempeWSMTStatusInfo);
                                    }
                                }
                                #endregion

                                #region 电池电压
                                //添加
                                var voltage = queryList.Where(item => item.WSId == ws.WSId && item.VoltageId != 0).FirstOrDefault();
                                if (voltage != null)
                                {
                                    if (voltage.VoltageId != 0)
                                    {
                                        MTStatusInfo voltageMTStatusInfo = new MTStatusInfo();
                                        voltageMTStatusInfo.MTId = (mtId++).ToString();
                                        voltageMTStatusInfo.MTPid = wSMTStatusInfo.MTId;
                                        voltageMTStatusInfo.MTStatus = voltage.VoltageStatus.ToString();
                                        voltageMTStatusInfo.MTType = "11";//电池电压
                                        voltageMTStatusInfo.MTName = "电池电压";
                                        voltageMTStatusInfo.Remark = "电池电压";
                                        voltageMTStatusInfo.RecordID = voltage.VoltageId.ToString();

                                        //添加
                                        mTWSNStatusInfos.Add(voltageMTStatusInfo);
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                }
                #endregion
            }

            return mTWSNStatusInfos;
        }
        #endregion
        #endregion

        #region 轴承库相关
        /// <summary>
        /// 根据轴承厂商、轴承型号、轴承描述进行搜索
        /// 
        /// 修改人：张辽阔
        /// 修改时间：2016-11-08
        /// 修改记录：返回结果集中添加1.4.1 新增字段
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<BearingEigenResult> BearingSearch(BearingSearchParameter param)
        {
            BaseResponse<BearingEigenResult> response = null;
            List<BearingInfo> bearingList = new List<BearingInfo>();
            BearingEigenResult result = new BearingEigenResult();

            var iCMSContext = new iCMSDbContext();

            try
            {
                ListSortDirection direction = new ListSortDirection();
                if (param.Order.ToLower().Equals("desc"))
                {
                    direction = ListSortDirection.Descending;
                }
                else
                {
                    direction = ListSortDirection.Ascending;
                }
                PropertySortCondition sortCondition = new PropertySortCondition(param.Sort, direction);

                //string bearingQuery = GenerateSql(param.SearchType, param.FactoryName, param.FactoryID, param.BearingNum, param.BearingDescribe);
                //var bearings = new iCMSDbContext().Database.SqlQuery<Bearing>(bearingQuery);

                #region bearing search
                //var bearingNew = bearingRepository.GetDatas<Bearing>(t => true, true);
                var bearingNew = from bearing in iCMSContext.Bearing select bearing;
                if (param.SearchType == 0)//精确查找
                {
                    if (!string.IsNullOrEmpty(param.FactoryName))
                    {
                        bearingNew = bearingNew.Where(t => t.FactoryName.ToLower().Trim().Equals(param.FactoryName.ToLower().Trim()));
                    }
                    if (!string.IsNullOrEmpty(param.FactoryID))
                    {
                        bearingNew = bearingNew.Where(t => t.FactoryID == param.FactoryID);
                    }
                    if (!string.IsNullOrEmpty(param.BearingNum))
                    {
                        bearingNew = bearingNew.Where(t => t.BearingNum.ToLower().Trim().Equals(param.BearingNum.ToLower().Trim()));
                    }
                    if (!string.IsNullOrEmpty(param.BearingDescribe))
                    {
                        bearingNew = bearingNew.Where(t => t.BearingDescribe.ToLower().Trim().Equals(param.BearingDescribe.ToLower().Trim()));
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(param.FactoryName))
                    {
                        bearingNew = bearingNew.Where(t => t.FactoryName.IndexOf(param.FactoryName) > -1);
                    }
                    if (!string.IsNullOrEmpty(param.FactoryID))
                    {
                        bearingNew = bearingNew.Where(t => t.FactoryID == param.FactoryID);
                    }
                    if (!string.IsNullOrEmpty(param.BearingNum))
                    {
                        bearingNew = bearingNew.Where(t => t.BearingNum.IndexOf(param.BearingNum) > -1);
                    }
                    if (!string.IsNullOrEmpty(param.BearingDescribe))
                    {
                        bearingNew = bearingNew.Where(t => t.BearingDescribe.IndexOf(param.BearingDescribe) > -1);
                    }
                }

                #endregion

                int total = 0;

                var linq =
                    from b in bearingNew
                    join f in iCMSContext.Factories on b.FactoryID equals f.FactoryID into groups
                    from g in groups.DefaultIfEmpty()
                    select new BearingInfo
                    {
                        FactoryName = g.FactoryName,
                        FactoryID = b.FactoryID,
                        BearingNum = b.BearingNum,
                        BearingDescribe = b.BearingDescribe,
                        BPFO = b.BPFO,
                        BPFI = b.BPFI,
                        BSF = b.BSF,
                        FTF = b.FTF,
                        EigenvalueID = b.BearingID,
                        BearingID = b.BearingID,
                        BallsNumber = b.BallsNumber,
                        BallDiameter = b.BallDiameter,
                        PitchDiameter = b.PitchDiameter,
                        ContactAngle = b.ContactAngle,
                        AddDate = b.AddDate,
                    };

                linq = linq.AsQueryable().Where(param.Page, param.PageSize, out total, sortCondition);
                bearingList.AddRange(linq.ToList());
                result.PageNum = param.Page;
                result.ChartID = param.ChartID;
                result.Total = total;
                result.BearingList = bearingList;

                response = new BaseResponse<BearingEigenResult>();
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                result.BearingList = new List<BearingInfo>();
                result.ChartID = param.ChartID;
                result.PageNum = param.Page;
                result.Total = 0;
                response = new BaseResponse<BearingEigenResult>("004581");
                response.Result = result;

                return response;
            }
        }

        /// <summary>
        /// 根据厂商名称、编号等信息检索出符合要求的，厂商信息。如未输入任何有意义信息，则查询出所有厂商信息
        /// 
        /// 修改人：张辽阔
        /// 修改时间：2016-11-04
        /// 修改记录：增加页数为-1的逻辑
        /// </summary>
        /// <returns></returns>      
        public BaseResponse<BearingFactoryResult> GetFactorys(GetFactoriesParameter param)
        {
            BaseResponse<BearingFactoryResult> response = null;
            BearingFactoryResult result = new BearingFactoryResult();

            try
            {
                int total = 0;
                ListSortDirection? direction = null;
                IQueryable<Factory> factories = null;
                PropertySortCondition sortCondition = null;

                if (!string.IsNullOrWhiteSpace(param.Order) && !string.IsNullOrWhiteSpace(param.Sort))
                {
                    if (!string.IsNullOrWhiteSpace(param.Order) && param.Order.ToLower().Equals("desc"))
                    {
                        direction = ListSortDirection.Descending;
                    }
                    else
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    sortCondition = new PropertySortCondition(param.Sort, direction.Value);
                }
                if (param.Page > -1)
                {
                    if (!string.IsNullOrEmpty(param.FactoryID))
                    {
                        factories = factoryRepository.GetDatas<Factory>(t => t.FactoryID.IndexOf(param.FactoryID) > -1, true);
                    }
                    if (!string.IsNullOrEmpty(param.FactoryName))
                    {
                        if (factories != null)
                            factories = factories.Where(t => t.FactoryName.IndexOf(param.FactoryName) > -1);
                        else
                            factories = factoryRepository.GetDatas<Factory>(t => t.FactoryName.IndexOf(param.FactoryName) > -1, true);
                    }
                    if (factories == null)
                        factories = factoryRepository.GetDatas<Factory>(t => true, true);

                    factories = factories.Where(param.Page, param.PageSize, out total, sortCondition);
                }
                else
                {
                    if (sortCondition != null)
                        factories = factoryRepository.GetDatas<Factory>(t => true, true).OrderBy(sortCondition);
                    else
                        factories = factoryRepository.GetDatas<Factory>(t => true, true);

                    total = factories.Count();
                }

                #region Distinct

                var linq =
                    from f in factories
                    select new BearingFactoryInfo
                    {
                        ID = f.ID,
                        FactoryID = f.FactoryID,
                        FactoryName = f.FactoryName
                    };

                #endregion

                result.ChartID = param.ChartID;
                result.FactoryList.AddRange(linq.ToList());
                result.Total = total;

                response = new BaseResponse<BearingFactoryResult>();
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                result.ChartID = param.ChartID;
                result.Total = 0;
                response = new BaseResponse<BearingFactoryResult>("004591");
                return response;
            }
        }

        /// <summary>
        /// 添加新的轴承特征频率信息到轴承库中。所有参数参数必须提供，否则返回错误
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> AddBearinEigenvalue(AddBearinEigenvalueParameter param)
        {
            BaseResponse<bool> response = null;

            try
            {
                #region  数据有效性验证
                if (string.IsNullOrEmpty(param.FactoryName))
                {
                    response = new BaseResponse<bool>("004602");
                    return response;
                }
                float bpfoF = param.BPFO;
                float bpfiF = param.BPFI;
                float bsfF = param.BSF;
                float ftfF = param.FTF;
                #endregion
                Bearing bearing = new Bearing();
                bearing.FactoryID = param.FactoryID;
                bearing.FactoryName = param.FactoryName;
                bearing.BearingNum = param.BearingNum;
                bearing.BearingDescribe = param.BearingDescribe;
                bearing.BPFO = bpfoF;
                bearing.BPFI = bpfiF;
                bearing.BSF = bsfF;
                bearing.FTF = ftfF;

                //1.4.1新增字段
                bearing.BallDiameter = param.BallDiameter;
                bearing.BallsNumber = param.BallsNumber;
                bearing.PitchDiameter = param.PitchDiameter;
                bearing.ContactAngle = param.ContactAngle;

                #region 验证 FactoryID和BearingNum 是否唯一  王颖辉 2017-02-21

                int count = bearingRepository.GetDatas<Bearing>(item => item.FactoryID == param.FactoryID && item.BearingNum == param.BearingNum, true).Count();
                if (count > 0)
                {
                    response = new BaseResponse<bool>("008041");
                    return response;
                }
                #endregion


                OperationResult opres = bearingRepository.AddNew<Bearing>(bearing);
                if (opres.ResultType == EnumOperationResultType.Success)
                {
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("008031");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("004611");
                return response;
            }
        }

        /// <summary>
        /// 修改已存在的轴承特征频率信息
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> UpdateBearinEigenvalue(UpdateBearinEigenvalueParameter param)
        {
            BaseResponse<bool> response = null;

            try
            {
                #region  数据有效性验证
                if (string.IsNullOrEmpty(param.FactoryName))
                {
                    response = new BaseResponse<bool>("004622");
                    return response;
                }

                #endregion
                Bearing bearingInDb = bearingRepository.GetByKey(param.BearingEigenID);
                if (bearingInDb == null)
                {
                    response = new BaseResponse<bool>("004632");
                    return response;
                }
                bearingInDb.FactoryID = param.FactoryID;
                bearingInDb.FactoryName = param.FactoryName;
                bearingInDb.BearingNum = param.BearingNum;
                bearingInDb.BearingDescribe = param.BearingDescribe;
                bearingInDb.BPFO = param.BPFO;
                bearingInDb.BPFI = param.BPFI;
                bearingInDb.BSF = param.BSF;
                bearingInDb.FTF = param.FTF;

                //1.4.1新增字段
                bearingInDb.BallDiameter = param.BallDiameter;
                bearingInDb.BallsNumber = param.BallsNumber;
                bearingInDb.PitchDiameter = param.PitchDiameter;
                bearingInDb.ContactAngle = param.ContactAngle;

                #region 验证 FactoryID和BearingNum 是否唯一  王颖辉 2017-02-21

                int count = bearingRepository.GetDatas<Bearing>(item => item.FactoryID == param.FactoryID && item.BearingNum == param.BearingNum && item.BearingID != param.BearingEigenID, true).Count();
                if (count > 0)
                {
                    response = new BaseResponse<bool>("008041");
                    return response;
                }
                #endregion


                OperationResult opres = bearingRepository.Update<Bearing>(bearingInDb);
                if (opres.ResultType == EnumOperationResultType.Success)
                {
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("004641");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("004641");
                return response;
            }
        }

        /// <summary>
        ///删除已存在的轴承特征频率信息 
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> DeleteBearinEigenvalue(DeleteBearinEigenvalueParameter param)
        {
            BaseResponse<bool> response = null;
            if (param.BearingEigenID.Count == 0)
            {
                response = new BaseResponse<bool>("004652");
                return response;
            }
            List<int?> bearingEigenIDList = new List<int?>();
            foreach (var item in param.BearingEigenID)
            {
                if (item <= 0)
                {
                    response = new BaseResponse<bool>("004652");
                    return response;
                }
                bearingEigenIDList.Add(item);
            }
            if (!bearingEigenIDList.Any())
            {
                response = new BaseResponse<bool>("004652");
                return response;
            }

            try
            {
                return ExecuteDB.ExecuteTrans((context) =>
                {
                    if (context.Measuresite.GetDatas<MeasureSite>(context, p => bearingEigenIDList.Contains(p.BearingID)).Any())
                    {
                        response = new BaseResponse<bool>("004662");
                        return response;
                    }

                    OperationResult opres = context.Bearing.Delete(context, p => bearingEigenIDList.Contains(p.BearingID));
                    if (opres.ResultType == EnumOperationResultType.Success)
                    {
                        response = new BaseResponse<bool>();
                        return response;
                    }
                    else
                    {
                        response = new BaseResponse<bool>("004671");
                        return response;
                    }
                });
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("004671");
                return response;
            }
        }

        /// <summary>
        /// 新增轴承库厂商
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> FactoryAdd(AddFactoryParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                if (string.IsNullOrEmpty(param.FactoryID) || string.IsNullOrEmpty(param.FactoryName))
                {
                    response = new BaseResponse<bool>("004682");
                    return response;
                }
                Factory factory = new Factory();
                factory.FactoryID = param.FactoryID;
                factory.FactoryName = param.FactoryName;

                OperationResult opres = factoryRepository.AddNew<Factory>(factory);
                if (opres.ResultType == EnumOperationResultType.Success)
                {
                    response = new BaseResponse<bool>();
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("004691");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("004691");
                return response;
            }
        }

        /// <summary>
        /// 修改轴承库厂商
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> FactoryUpdate(UpdateFactoryParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                var factoryInDb = factoryRepository.GetDatas<Factory>(t => t.FactoryID.Equals(param.FactoryID), true).FirstOrDefault();
                if (factoryInDb == null)
                {
                    response = new BaseResponse<bool>("004702");
                    return response;
                }
                if (string.IsNullOrEmpty(param.FactoryName))
                {
                    response = new BaseResponse<bool>("004712");
                    return response;
                }
                factoryInDb.FactoryName = param.FactoryName;
                OperationResult opres = factoryRepository.Update<Factory>(factoryInDb);

                if (opres.ResultType == EnumOperationResultType.Success)
                {
                    //修改轴承库存储的FactoryName
                    var bearingsInDB = bearingRepository.GetDatas<Bearing>(t => t.FactoryID.Equals(factoryInDb.FactoryID), true).ToList();
                    foreach (Bearing bearing in bearingsInDB)
                    {
                        bearing.FactoryName = factoryInDb.FactoryName;
                        OperationResult updBearingResult = bearingRepository.Update<Bearing>(bearing);
                        if (updBearingResult.ResultType != EnumOperationResultType.Success)
                        {
                            response = new BaseResponse<bool>("004721");
                            return response;
                        }
                    }

                    return new BaseResponse<bool>();
                }
                else
                {
                    response = new BaseResponse<bool>("004721");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("004721");
                return response;
            }
        }

        /// <summary>
        /// 删除轴承库厂商
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> FactoryDelete(DeleteFactoryParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                if (string.IsNullOrWhiteSpace(param.FactoryID))
                {
                    response = new BaseResponse<bool>("004732");
                    return response;
                }

                param.FactoryID = param.FactoryID.Trim();
                return ExecuteDB.ExecuteTrans((context) =>
                {
                    if (context.Bearing.GetDatas<Bearing>(context, p => p.FactoryID == param.FactoryID).Any())
                    {
                        response = new BaseResponse<bool>("004742");
                        return response;
                    }

                    var factory = context.Factories.GetDatas<Factory>(context, f => f.FactoryID.Equals(param.FactoryID)).FirstOrDefault();
                    //空判断  王颖辉 2016-08-30
                    if (factory != null)
                    {
                        OperationResult opres = context.Factories.Delete<Factory>(context, factory);
                        if (opres.ResultType == EnumOperationResultType.Success)
                        {
                            response = new BaseResponse<bool>();
                            return response;
                        }
                        else
                        {
                            response = new BaseResponse<bool>("004751");
                            return response;
                        }
                    }
                    //如果没有删除厂商，依然返回成功 张辽阔 2016-08-31 修改
                    else
                    {
                        response = new BaseResponse<bool>();
                        return response;
                    }
                });
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("004751");
                return response;
            }
        }

        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间:2017-10-26
        /// 创建内容:删除轴承库厂商
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> FactorysDelete(DeleteFactoryParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                //空数据
                if (param.FactoryIDList == null && param.FactoryIDList.Any())
                {
                    response = new BaseResponse<bool>("004732");
                    return response;
                }

                //判断是否为空
                foreach (var info in param.FactoryIDList)
                {
                    if (string.IsNullOrWhiteSpace(info.Trim()))
                    {
                        response = new BaseResponse<bool>("004732");
                        return response;
                    }
                }


                return ExecuteDB.ExecuteTrans((context) =>
                {

                    bool isExist = false;
                    //判断是否为空
                    foreach (var info in param.FactoryIDList)
                    {
                        if (context.Bearing.GetDatas<Bearing>(context, p => p.FactoryID == info.Trim()).Any())
                        {
                            isExist = true;
                            break;
                        }
                    }

                    if (isExist)
                    {
                        response = new BaseResponse<bool>("004742");
                        return response;
                    }

                    OperationResult opres = context.Factories.Delete<Factory>(context, f => param.FactoryIDList.Contains(f.FactoryID));

                    if (opres.ResultType == EnumOperationResultType.Success)
                    {
                        response = new BaseResponse<bool>();
                        return response;
                    }
                    else
                    {
                        response = new BaseResponse<bool>("004751");
                        return response;
                    }
                });
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("004751");
                return response;
            }
        }

        /// <summary>
        /// 通过轴承厂商编号，获取轴承型号信息
        /// </summary>
        /// <returns></returns>
        public BaseResponse<BearResult> GetBearingByFactoryID(GetBearingByFactoryIDParameter param)
        {
            BaseResponse<BearResult> response = new BaseResponse<BearResult>();
            BearResult result = new BearResult();
            result.BearingList = new List<BearingList>();
            try
            {
                var bearingList = bearingRepository
                    .GetDatas<Bearing>(item => item.FactoryID == param.FactoryID, false)
                    .Select(item => new BearingList
                    {
                        FactoryID = item.FactoryID,
                        FactoryName = item.FactoryName,
                        BearingID = item.BearingID,
                        BearingNum = item.BearingNum,
                    })
                    .ToList<BearingList>();

                result.BearingList = bearingList;
                response = new BaseResponse<BearResult>();
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<BearResult>("004761");
                return response;
            }
        }

        /// <summary>
        /// 验证规则，保证轴承库厂商&型号的唯一性
        /// </summary>
        /// <returns></returns>
        public BaseResponse<bool> CheckFidAndBearingNumUnique(CheckFidAndBearingNumUniqueParameter param)
        {
            BaseResponse<bool> response = null;
            try
            {
                List<Bearing> bearings = new List<Bearing>();
                if (!param.BearingID.HasValue)
                {
                    //Added
                    bearings = bearingRepository
                        .GetDatas<Bearing>(t => t.FactoryID.Equals(param.FactoryID)
                            && t.BearingNum.Equals(param.BearingNum), true)
                        .ToList();
                }
                else
                {
                    //Update
                    int bearingId = param.BearingID.Value;
                    bearings = bearingRepository
                        .GetDatas<Bearing>(t => t.BearingID != bearingId
                            && t.FactoryID.Equals(param.FactoryID)
                            && t.BearingNum.Equals(param.BearingNum), true)
                        .ToList();
                }

                if (bearings != null && bearings.Count > 0)
                {
                    response = new BaseResponse<bool>();
                    //是否唯一存储在Result中
                    response.Result = false;
                }
                else
                {
                    response = new BaseResponse<bool>();
                    //是否唯一存储在Result中
                    response.Result = true;
                }

                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("004771");
                return response;
            }
        }

        /// <summary>
        /// 拼接SQL
        /// </summary>
        /// <param name="searchType"></param>
        /// <param name="FactoryName"></param>
        /// <param name="FactoryID"></param>
        /// <param name="BearingNum"></param>
        /// <param name="BearingDescribe"></param>
        /// <returns></returns>
        private string GenerateSql(int searchType, string FactoryName, string FactoryID, string BearingNum, string BearingDescribe)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from T_SYS_BEARING where 1=1");
            if (searchType == 0)   //精确查找
            {
                if (!string.IsNullOrEmpty(FactoryName))
                {
                    sb.Append(" and FactoryName = '" + FactoryName + "'");
                }
                if (!string.IsNullOrEmpty(FactoryID))
                {
                    sb.Append(" and FactoryID = '" + FactoryID + "'");
                }
                if (!string.IsNullOrEmpty(BearingNum))
                {
                    sb.Append(" and BearingNum = '" + BearingNum + "'");
                }
                if (!string.IsNullOrEmpty(BearingDescribe))
                {
                    sb.Append(" and BearingDescribe = '" + BearingDescribe + "'");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(FactoryName))
                {
                    sb.Append(" and FactoryName LIKE '%" + FactoryName + "%'");
                }
                if (!string.IsNullOrEmpty(FactoryID))
                {
                    sb.Append(" and FactoryID = '" + FactoryID + "'");
                }
                if (!string.IsNullOrEmpty(BearingNum))
                {
                    sb.Append(" and BearingNum LIKE '%" + BearingNum + "%'");
                }
                if (!string.IsNullOrEmpty(BearingDescribe))
                {
                    sb.Append(" and BearingDescribe LIKE '%" + BearingDescribe + "%'");
                }
            }

            return sb.ToString();
        }
        #endregion

        #region 图片管理
        /// <summary>
        /// 图片预览
        /// </summary>
        public BaseResponse<AllImagesResult> GetAllImages(AllImagesParameter param)
        {
            BaseResponse<AllImagesResult> response = new BaseResponse<AllImagesResult>();
            AllImagesResult result = new AllImagesResult();

            try
            {
                var images = new List<Image>();
                if (param.Type == -1)
                {
                    images = imageRepository.GetDatas<Image>(t => true, true).ToList();
                }
                else
                {
                    images = imageRepository.GetDatas<Image>(t => t.Type == param.Type, true).ToList();
                }

                var imageInfoList =
                    images.Select(t => new ImageInfo
                    {
                        ImageID = t.ImageID,
                        ImageName = t.ImageName,
                        ImagePath = t.ImagePath,
                        Type = t.Type,
                        Width = t.Width,
                        Height = t.Height
                    })
                    .ToList();
                result.ImageInfoList = imageInfoList;
                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<AllImagesResult>("004781");
                return response;
            }
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<bool> SaveImagePath(SaveImageParameter param)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                Image image = new Image();
                image.ImageName = param.ImageName;
                image.ImagePath = param.ImagePath;
                image.Type = param.ImageType;
                image.Width = param.Width;
                image.Height = param.Height;

                OperationResult result = imageRepository.AddNew<Image>(image);
                if (result.ResultType == EnumOperationResultType.Success)
                {
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("004791");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("#004791");
                return response;
            }
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public BaseResponse<bool> DeleteImage(DeleteImageParameter param)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();
            try
            {
                OperationResult result = imageRepository.Delete<Image>(param.ImageID);
                if (result.ResultType == EnumOperationResultType.Success)
                {
                    return response;
                }
                else
                {
                    response = new BaseResponse<bool>("004801");
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response = new BaseResponse<bool>("004801");
                return response;
            }
        }
        #endregion

        #region 根据传入设备ID，获取所有完整的父监测树节点，同时返回当前设备所在监测树节点
        /// <summary>
        /// 根据传入设备ID，获取所有完整的父监测树节点，同时返回当前设备所在监测树节点
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        public BaseResponse<GetFullMonitorTreeDataByDeviceResult> GetFullMonitorTreeDataByDevice(GetFullMonitorTreeDataByDeviceParameter parameter)
        {
            BaseResponse<GetFullMonitorTreeDataByDeviceResult> baseResponse = new BaseResponse<GetFullMonitorTreeDataByDeviceResult>();
            GetFullMonitorTreeDataByDeviceResult result = new GetFullMonitorTreeDataByDeviceResult();

            List<MTInfoWithType> mtInfoWithTypeList = new List<MTInfoWithType>();
            #region 验证
            //获取监测树数据时,设备Id不正确
            if (parameter.DevID < 1)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008982";
                return baseResponse;
            }


            var device = deviceRepository.GetByKey(parameter.DevID);
            //获取监测树数据时,设备未找到设备
            if (device == null)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "008992";
                return baseResponse;
            }

            #endregion



            try
            {
                var db = new iCMSDbContext();
                //获取设备信息
                var deviceList = new List<Device>();
                var deviceIDList = new List<int>();
                var monitorIDList = new List<int>();
                deviceIDList.Add(parameter.DevID);

                //设备节点，父机组Id
                var monitorId = device.MonitorTreeID;

                if (parameter.UserID.HasValue)
                {
                    var userRalationDeviceList = db.UserRalationDevice.GetDatas<UserRalationDevice>(db, item => item.UserID == parameter.UserID.Value);
                    if (userRalationDeviceList != null && userRalationDeviceList.Any())
                    {
                        deviceIDList = userRalationDeviceList.Select(item => item.DevId.Value).ToList();
                    }
                }

                //设备查询机组信息

                deviceList = db.Device.GetDatas<Device>(db, item => deviceIDList.Contains(item.DevID)).ToList();
                if (deviceList != null && deviceList.Any())
                {
                    monitorIDList = deviceList.Select(item => item.MonitorTreeID).ToList();
                }

                //监测树类型查找上层
                var linq = from tsmt in db.MonitorTree
                           join tdmt in db.MonitorTreeType on new { Type = tsmt.Type } equals new { Type = tdmt.ID } into tdmt_join
                           from tdmt in tdmt_join.DefaultIfEmpty()
                           where
                             tdmt.IsUsable == 1
                           select new
                           {
                               tsmt.MonitorTreeID,
                               ParentID = tsmt.PID,
                               tsmt.IsDefault,
                               tsmt.Name,
                               Description = tsmt.Des,
                               TypeID = tdmt.Describe,
                               TypeName = tdmt.Name
                           };

                var monitorList = linq.Select(item => new
                {
                    item.MonitorTreeID,
                    ParentID = item.ParentID,
                    item.IsDefault,
                    item.Name,
                    Description = item.Description,
                    TypeID = Convert.ToInt32(item.TypeID),
                    TypeName = item.Name
                }).ToList();


                //查询上层节点
                for (int type = 4; type > 0; type--)
                {
                    MTInfoWithType mTInfoWithType = new MTInfoWithType();
                    var parnetMonitorTreeList = monitorList.Where(item => item.TypeID == type && monitorIDList.Contains(item.MonitorTreeID));
                    if (parnetMonitorTreeList != null && parnetMonitorTreeList.Any())
                    {
                        mTInfoWithType.TypeID = parnetMonitorTreeList.FirstOrDefault().TypeID;
                        mTInfoWithType.TypeName = parnetMonitorTreeList.FirstOrDefault().TypeName;
                        mTInfoWithType.MTInfo = parnetMonitorTreeList.Select(item => new iCMS.Common.Component.Data.Response.DevicesConfig.MTInfo
                        {
                            MonitorTreeID = item.MonitorTreeID,
                            ParentID = item.ParentID,
                            IsDefault = item.IsDefault,
                            Name = item.Name,
                            Description = item.Description,
                            TypeID = Convert.ToInt32(item.TypeID),
                            TypeName = item.Name
                        }).ToList();

                        //查找选中ID
                        var selectId = 0;
                        if (mTInfoWithType.MTInfo != null && mTInfoWithType.MTInfo.Any())
                        {
                            var selectInfo = mTInfoWithType.MTInfo.Where(item => item.MonitorTreeID == monitorId).FirstOrDefault();
                            if (selectInfo != null)
                            {
                                selectId = selectInfo.MonitorTreeID;
                                monitorId = selectInfo.ParentID;
                            }
                            else
                            {
                                monitorId = 0;
                            }
                        }
                        mTInfoWithType.SelectedID = selectId;
                    };

                    //获取上层节点
                    if (mTInfoWithType.MTInfo != null && mTInfoWithType.MTInfo.Any())
                    {
                        monitorIDList = mTInfoWithType.MTInfo.Select(item => item.ParentID).ToList();
                    }
                    else
                    {
                        monitorIDList = new List<int>();
                    }
                    mtInfoWithTypeList.Add(mTInfoWithType);
                }

                result.MTInfoWithTypeList = mtInfoWithTypeList;
                baseResponse.IsSuccessful = true;
                baseResponse.Result = result;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "009001";

            }

            return baseResponse;
        }
        #endregion

        #region 系统说明

        /// <summary>
        /// 读取系统说明列表
        /// 创建人：王龙杰
        /// 创建时间：2017-10-31
        /// </summary>
        /// <returns></returns>
        public BaseResponse<MoudleContentListResult> GetMoudleContentList()
        {
            BaseResponse<MoudleContentListResult> response = new BaseResponse<MoudleContentListResult>();
            MoudleContentListResult result = new MoudleContentListResult();
            List<MoudleContent> hdList = new List<MoudleContent>();
            try
            {
                using (iCMSDbContext dataContext = new iCMSDbContext())
                {
                    hdList = (from hd in dataContext.HelpDocument
                              select new MoudleContent
                              {
                                  ID = hd.ID,
                                  PID = hd.PID,
                                  Code = hd.Code,
                                  Name = hd.Name,
                                  IsShow = hd.IsShow        //add by lwj,是否显示
                              }).ToList();
                    result.MoudleContentList = hdList;
                    response.IsSuccessful = true;
                    response.Result = result;
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "009471";
                return response;
            }
        }

        /// <summary>
        /// 系统说明-读取功能介绍
        /// 创建人：王龙杰
        /// 创建时间：2017-10-13
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<MoudleContentDetailResult> GetMoudleContent(MoudleContentDetailParameter parameter)
        {
            BaseResponse<MoudleContentDetailResult> response = new BaseResponse<MoudleContentDetailResult>();
            MoudleContentDetailResult result = new MoudleContentDetailResult();

            if (string.IsNullOrEmpty(parameter.Code))
            {
                response.IsSuccessful = false;
                response.Code = "008502";
                return response;
            }

            try
            {
                using (iCMSDbContext dataContext = new iCMSDbContext())
                {
                    result = (from hd in dataContext.HelpDocument
                              where hd.Code == parameter.Code
                              select new MoudleContentDetailResult
                              {
                                  id = hd.ID,
                                  Code = hd.Code,
                                  Name = hd.Name,
                                  Content = hd.Substance,
                                  IsShow = hd.IsShow        //add by lwj,是否显示
                              }).FirstOrDefault();
                    response.IsSuccessful = true;
                    response.Result = result;
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "008491";
                return response;
            }
        }

        /// <summary>
        /// 新增系统说明
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> AddMoudleContent(AddMoudleContentParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            try
            {
                HelpDocument helpDocument = new HelpDocument();
                helpDocument.PID = parameter.PID;
                helpDocument.Code = Guid.NewGuid().ToString();
                helpDocument.Name = parameter.Name;
                helpDocument.Substance = parameter.Content;
                helpDocument.IsShow = parameter.IsShow;     //add by lwj,添加时判断是否显示
                OperationResult operationResult = helpDocumentRepository.AddNew(helpDocument);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    response.IsSuccessful = true;
                    response.Result = true;
                    return response;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Code = "009481";
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "009481";
                return response;
            }
        }

        /// <summary>
        /// 编辑系统说明
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> EditMoudleContent(EditMoudleContentParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            try
            {
                HelpDocument helpDocument = helpDocumentRepository.GetByKey(parameter.ID);
                helpDocument.Name = parameter.Name;
                helpDocument.Substance = parameter.Content;
                helpDocument.IsShow = parameter.IsShow;         //add by lwj---编辑时判断是否显示
                OperationResult operationResult = helpDocumentRepository.Update(helpDocument);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    response.IsSuccessful = true;
                    response.Result = true;
                    return response;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Code = "009491";
                    return response;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "009491";
                return response;
            }
        }

        /// <summary>
        /// 删除系统说明
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<bool> DeleteMoudleContent(DeleteMoudleContentParameter parameter)
        {
            BaseResponse<bool> response = new BaseResponse<bool>();

            try
            {
                if (helpDocumentRepository.GetDatas<HelpDocument>(p => p.PID == parameter.ID, false).Any())
                {
                    response.IsSuccessful = false;
                    response.Code = "009512";
                    return response;
                }

                OperationResult operationResult = helpDocumentRepository.Delete<HelpDocument>(parameter.ID);
                if (operationResult.ResultType == EnumOperationResultType.Success)
                {
                    response.IsSuccessful = true;
                    response.Result = true;
                    return response;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Code = "009501";
                    return response;
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                response.IsSuccessful = false;
                response.Code = "009501";
                return response;
            }
        }

        #endregion

        #region 获取用户权限
        /// <summary>
        /// 获取用户权限
        /// 创建人：王龙杰
        /// 创建时间：2017-10-13
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public BaseResponse<GetAuthorizedResult> GetAuthorized(GetAuthorizedParam parameter)
        {
            BaseResponse<GetAuthorizedResult> response = new BaseResponse<GetAuthorizedResult>();
            GetAuthorizedResult result = new GetAuthorizedResult();

            try
            {
                using (iCMSDbContext dbContext = new iCMSDbContext())
                {
                    string RoleCode = parameter.RoleCode;
                    parameter.Code.ForEach(t =>
                    {
                        bool isAuthority = false;
                        if (t.Trim() == "MonitorModule" || t.Trim() == "PassivityMonitorModule")
                        {
                            List<string> childModuleCodes = new List<string>();
                            childModuleCodes.Add(t);
                            GetChildModuleCodes(childModuleCodes, new List<string> { t.Trim() }, dbContext);
                            isAuthority = (from rm in dbContext.RoleModule
                                           join module in dbContext.Module on rm.ModuleCode equals module.Code
                                           where module.IsUsed == 1
                                           where rm.RoleCode == RoleCode && childModuleCodes.Contains(rm.ModuleCode)
                                           select rm).Any();
                        }
                        else
                        {
                            isAuthority = (from rm in dbContext.RoleModule
                                           join module in dbContext.Module on rm.ModuleCode equals module.Code
                                           where rm.RoleCode == RoleCode && rm.ModuleCode == t && module.IsUsed == 1
                                           select rm).Any();
                        }

                        result.Result.Add(isAuthority);
                    });
                }

                response.Result = result;
                return response;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.Code = "008511";
                return response;
            }
        }

        private void GetChildModuleCodes(List<string> childModuleCodes, List<string> ParCode, iCMSDbContext dbContext)
        {
            List<string> moduleCodes = new List<string>();
            List<Module> modules = new List<Module>();

            modules = (from m in dbContext.Module
                       where
                        (from m1 in dbContext.Module where ParCode.Contains(m1.Code) select m1.ModuleID).Contains(m.ParID)
                       select m).ToList();
            if (modules.Count > 0)
            {
                moduleCodes = modules.Select(s => s.Code).ToList();
                childModuleCodes.AddRange(moduleCodes);
                GetChildModuleCodes(childModuleCodes, moduleCodes, dbContext);
            }
        }

        #endregion

        #region 获取设备树信息
        /// <summary>
        /// 获取设备树信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        private List<Tree> GetDeviceTree(int userId)
        {
            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter("@userId", userId);
            sqlParam[1] = new SqlParameter("@level", 5);
            OperationResult operationResult = measureSiteRepository
                .SqlQuery<Tree>("EXEC GetDeviceTree @userId,@level", sqlParam);
            List<Tree> treeList = new List<Tree>();
            if (operationResult.ResultType == EnumOperationResultType.Success)
            {
                treeList = operationResult.AppendData as List<Tree>;
            }
            return treeList;
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
            BaseResponse<GetMonitorTreeByParentIDResult> baseResponse = new BaseResponse<GetMonitorTreeByParentIDResult>();
            GetMonitorTreeByParentIDResult result = new GetMonitorTreeByParentIDResult();
            result.MonitorTree = new List<MonitorTreeInfoForMonitorTree>();
            baseResponse.IsSuccessful = true;
            baseResponse.Result = result;

            #region 验证
            //获取监测树子节点时，父节点id不正确
            if (parameter.ParentID < 0)
            {
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "009642";
                return baseResponse;
            }
            #endregion

            try
            {
                result.MonitorTree = monitorRepository.GetDatas<MonitorTree>(item => item.PID == parameter.ParentID, true)
                                                    .Select(item => new MonitorTreeInfoForMonitorTree
                                                    {
                                                        MonitorTreeID = item.MonitorTreeID,
                                                        Name = item.Name,
                                                    }).ToList();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                baseResponse.IsSuccessful = false;
                baseResponse.Code = "009651";
            }
            return baseResponse;

        }
        #endregion
    }
    #endregion
}