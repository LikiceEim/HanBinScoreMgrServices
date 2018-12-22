using iCMS.Common.Component.Data.Enum;
using iCMS.Common.Component.Tool;
using iCMS.Frameworks.Core.DB.Models;
using iCMS.Frameworks.Core.Repository;
using iCMS.Setup.CloudPushManually.Entity;
using iCMS.Setup.CloudPushManually.Function;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iCMS.Setup.CloudPushManually
{
    public partial class CustomizedPushFrm : Form
    {
        #region 私有变量
        private IRepository<MonitorTreeType> monitorTreeTypeRepository = null;
        private IRepository<MonitorTree> monitorTreeRepository = null;
        private IRepository<Device> deviceRepository = null;
        private IRepository<MeasureSite> measureSiteRepository = null;
        private IRepository<MeasureSiteType> measureSiteTypeRepository = null;
        private IRepository<TempeDeviceSetMSiteAlm> tempeDeviceSetMSiteAlmRepository = null;
        private IRepository<VoltageSetMSiteAlm> voltageSetMSiteAlmRepository = null;
        private IRepository<VibSingal> vibSignalRepository = null;
        private IRepository<VibratingSingalType> vibratingSingalTypeRepository = null;
        private IRepository<SignalAlmSet> signalAlmSetRepository = null;
        private IRepository<EigenValueType> eigenValueTypeRepository = null;
        private IRepository<Gateway> wgRepository = null;

        //后台工作者线程
        //private BackgroundWorker m_backGroudWorker = null;
        #endregion

        #region 构造函数与初始化
        public CustomizedPushFrm()
        {
            InitializeComponent();

            this.progressBar.Visible = false;
            InitDAO();
        }

        private void InitDAO()
        {
            monitorTreeTypeRepository = new Repository<MonitorTreeType>();
            monitorTreeRepository = new Repository<MonitorTree>();
            deviceRepository = new Repository<Device>();
            measureSiteRepository = new Repository<MeasureSite>();
            measureSiteTypeRepository = new Repository<MeasureSiteType>();
            tempeDeviceSetMSiteAlmRepository = new Repository<TempeDeviceSetMSiteAlm>();
            voltageSetMSiteAlmRepository = new Repository<VoltageSetMSiteAlm>();
            vibSignalRepository = new Repository<VibSingal>();
            vibratingSingalTypeRepository = new Repository<VibratingSingalType>();
            signalAlmSetRepository = new Repository<SignalAlmSet>();
            eigenValueTypeRepository = new Repository<EigenValueType>();
            wgRepository = new Repository<Gateway>();
        }

        private void CustomizedPushFrm_Load(object sender, EventArgs e)
        {
            try
            {
                //工厂从ID去取，名称可能有变。王颖辉 2017-03-31
                int groupID = (int)EnumMonitorTreeTypeID.Group;
                int factoryID = (int)EnumMonitorTreeTypeID.Factory;
                int workshopID = (int)EnumMonitorTreeTypeID.Workshop;
                int crewID = (int)EnumMonitorTreeTypeID.Crew;

                var monitorTreeTypes = monitorTreeTypeRepository.GetDatas<MonitorTreeType>(t => true, true).ToList();
                var teamType = monitorTreeTypes.Where(t => t.ID== groupID).FirstOrDefault();
                var enterpriseType = monitorTreeTypes.Where(t => t.ID== factoryID).FirstOrDefault();
                var workshopType = monitorTreeTypes.Where(t => t.ID== workshopID).FirstOrDefault();
                var groupType = monitorTreeTypes.Where(t => t.ID== crewID).FirstOrDefault();

                var enterprises = monitorTreeRepository.GetDatas<MonitorTree>(t => t.Type == enterpriseType.ID, true).ToList();
                if (!enterprises.Any())
                {
                    return;
                }

                foreach (var item in enterprises)
                {
                    ICMSTreeNode enterpriseNode = new ICMSTreeNode(item.Name);
                    enterpriseNode.TableID = item.MonitorTreeID;
                    enterpriseNode.DataType = EnumCloudOperationType.Enterprise;
                    enterpriseNode.Order = EnumSortOrder.Enterprise;

                    this.tviCMSDatas.Nodes.Add(enterpriseNode);

                    var workshops = monitorTreeRepository.GetDatas<MonitorTree>(t => t.Type == workshopType.ID && t.PID == item.MonitorTreeID, true).ToList();
                    foreach (var workshop in workshops)
                    {
                        ICMSTreeNode workshopNode = new ICMSTreeNode(workshop.Name);
                        workshopNode.TableID = workshop.MonitorTreeID;
                        workshopNode.DataType = EnumCloudOperationType.Workshop;
                        workshopNode.Order = EnumSortOrder.Workshop;
                        enterpriseNode.Nodes.Add(workshopNode);

                        List<int> allChildrenMT = new List<int>();
                        GetAllChildren(workshop.MonitorTreeID, allChildrenMT);

                        var devices = deviceRepository.GetDatas<Device>(t => t.UseType == 0 && allChildrenMT.Contains(t.MonitorTreeID), true).ToList();
                        if (!devices.Any()) { continue; }
                        foreach (var device in devices)
                        {
                            ICMSTreeNode deviceNode = new ICMSTreeNode(device.DevName);
                            deviceNode.TableID = device.DevID;
                            deviceNode.DataType = EnumCloudOperationType.Device;
                            deviceNode.Order = EnumSortOrder.Device;

                            workshopNode.Nodes.Add(deviceNode);

                            var measureSites = measureSiteRepository.GetDatas<MeasureSite>(t => t.DevID == device.DevID && t.WSID.HasValue, true).ToList();
                            if (!measureSites.Any())
                            {
                                continue;
                            }
                            foreach (var msite in measureSites)
                            {
                                //获取测点名字
                                var msiteType = measureSiteTypeRepository.GetByKey(msite.MSiteName);
                                var siteName = msiteType == null ? "Unknown" : msiteType.Name;

                                ICMSTreeNode msiteNode = new ICMSTreeNode(siteName);
                                msiteNode.TableID = msite.MSiteID;
                                msiteNode.DataType = EnumCloudOperationType.Measuresite;
                                msiteNode.Order = EnumSortOrder.MeasureSite;

                                deviceNode.Nodes.Add(msiteNode);

                                #region 设备温度告警阈值
                                var deviceTempeAlmset = tempeDeviceSetMSiteAlmRepository.GetDatas<TempeDeviceSetMSiteAlm>(t => t.MsiteID == msite.MSiteID, true).FirstOrDefault();
                                if (deviceTempeAlmset != null)
                                {
                                    ICMSTreeNode deviceTempeNode = new ICMSTreeNode("设备温度");
                                    deviceTempeNode.TableID = deviceTempeAlmset.MsiteAlmID;
                                    deviceTempeNode.DataType = EnumCloudOperationType.DeviceTemperatureAlarmSet;

                                    deviceTempeNode.Order = EnumSortOrder.AlamThreshold;
                                    msiteNode.Nodes.Add(deviceTempeNode);
                                }
                                #endregion

                                #region 传感器电池电压告警阈值
                                var volateAlmset = voltageSetMSiteAlmRepository.GetDatas<VoltageSetMSiteAlm>(t => t.MsiteID == msite.MSiteID, true).FirstOrDefault();
                                if (volateAlmset != null)
                                {
                                    ICMSTreeNode volateAlmsetNode = new ICMSTreeNode("电池电压");
                                    volateAlmsetNode.TableID = volateAlmset.MsiteAlmID;
                                    volateAlmsetNode.DataType = EnumCloudOperationType.BatteryVoltageAlarmSet;
                                    volateAlmsetNode.Order = EnumSortOrder.AlamThreshold;

                                    msiteNode.Nodes.Add(volateAlmsetNode);
                                }
                                #endregion

                                #region 振动信号
                                var signals = vibSignalRepository.GetDatas<VibSingal>(t => t.MSiteID == msite.MSiteID, true).ToList();
                                if (!signals.Any())
                                {
                                    continue;
                                }

                                //只显示 速度，加速度，包络
                                List<string> selectedSignalNameList = new List<string> { "速度", "加速度", "包络" };
                                var vibSignalTypes = vibratingSingalTypeRepository.GetDatas<VibratingSingalType>(t => true, true).ToList();
                                foreach (var signal in signals)
                                {
                                    var signalType = vibSignalTypes.Where(t => t.ID == signal.SingalType).FirstOrDefault();
                                    var signalName = signalType == null ? "Unknown" : signalType.Name;
                                    ICMSTreeNode vibSignalNode = new ICMSTreeNode(signalName);

                                    if (!selectedSignalNameList.Contains(signalName))
                                    {
                                        continue;
                                    }
                                    vibSignalNode.TableID = signal.SingalID;
                                    vibSignalNode.DataType = EnumCloudOperationType.VibSignal;
                                    vibSignalNode.Order = EnumSortOrder.Singal;

                                    msiteNode.Nodes.Add(vibSignalNode);

                                    #region 振动信号特征值
                                    var signalAlmsets = signalAlmSetRepository.GetDatas<SignalAlmSet>(t => t.SingalID == signal.SingalID, true).ToList();
                                    if (!signalAlmsets.Any())
                                    {
                                        continue;
                                    }

                                    var eigenValueTypes = eigenValueTypeRepository.GetDatas<EigenValueType>(t => true, true).ToList();
                                    foreach (var signalAlmset in signalAlmsets)
                                    {
                                        var eigenValueType = eigenValueTypes.Where(t => t.ID == signalAlmset.ValueType).FirstOrDefault();
                                        string eigenName = eigenValueType == null ? "" : eigenValueType.Name;

                                        ICMSTreeNode eigenValueNode = new ICMSTreeNode(eigenName);
                                        eigenValueNode.TableID = signalAlmset.SingalAlmID;
                                        eigenValueNode.DataType = EnumCloudOperationType.SignalAlarmSet;
                                        eigenValueNode.Order = EnumSortOrder.AlamThreshold;

                                        vibSignalNode.Nodes.Add(eigenValueNode);
                                    }

                                    #endregion
                                }
                                #endregion
                            }
                        }
                    }
                }

                this.tviCMSDatas.CheckBoxes = true;
                this.tviCMSDatas.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        private void tviCMSDatas_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //return;

            ICMSTreeNode selectedNode = e.Node as ICMSTreeNode;
            if (selectedNode == null)
            {
                return;
            }

            List<ICMSTreeNode> allChildrenNodes = new List<ICMSTreeNode>();


            FindAllChildrenNodes(selectedNode, allChildrenNodes);

            if (selectedNode.Checked)
            {
                foreach (var node in allChildrenNodes)
                {
                    node.Checked = true;
                }
            }
            else
            {
                foreach (var node in allChildrenNodes)
                {
                    node.Checked = false;
                }
            }
        }

        private void FindAllChildrenNodes(ICMSTreeNode selectedNode, List<ICMSTreeNode> allChildrenNodes)
        {
            var childNodes = selectedNode.Nodes;
            foreach (ICMSTreeNode tNode in childNodes)
            {
                allChildrenNodes.Add(tNode);

                FindAllChildrenNodes(tNode, allChildrenNodes);
            }
        }

        #region GetAllChildren
        /// <summary>
        /// GetAllChildren
        /// </summary>
        /// <param name="monitorTreeID"></param>
        /// <param name="allChildNodes"></param>
        private void GetAllChildren(int monitorTreeID, List<int> allChildNodes)
        {
            allChildNodes.Add(monitorTreeID);
            var childNodes = monitorTreeRepository.GetDatas<MonitorTree>(m => m.PID == monitorTreeID, false).ToList();

            foreach (var mt in childNodes)
            {
                //找到包含当前节点及所有子节点
                GetAllChildren(mt.MonitorTreeID, allChildNodes);
            }
        }
        #endregion

        private void btnPushCloud_Click(object sender, EventArgs e)
        {
            //m_backGroudWorker.RunWorkerAsync(this);

            this.progressBar.Visible = false;

            #region 后台线程
            // 后台执行连接手持
            DoWorkBackGround workFunc = new DoWorkBackGround(PushConfigData);

            FormWorkBackGround frm = new FormWorkBackGround(workFunc);

            // 程序更新进度
            frm.AutoUpdateProgressBar = true;
            // 可以取消
            frm.SupportsCancellation = false;

            if (frm.ShowDialog() == DialogResult.Yes)
            {
                MessageBox.Show("配置数据推送成功！", "提示");
            }
            #endregion
        }

        private void PushConfigData(BackgroundWorker worker, DoWorkEventArgs e)
        {
            try
            {
                List<ICMSTreeNode> allSelectedNodes = new List<ICMSTreeNode>();
                var rootNode = this.tviCMSDatas.Nodes;
                foreach (ICMSTreeNode node in rootNode)
                {
                    GetAllSelectedNodes(node, allSelectedNodes);
                }

                if (!allSelectedNodes.Any())
                {
                    return;
                }

                //获取了所有选择的节点，调用推送方法
                allSelectedNodes = allSelectedNodes.OrderBy(t => t.Order).ToList();

                //向推送节点列表中添加网关数据
                var wgs = wgRepository.GetDatas<Gateway>(t => true, true).ToList();

                foreach (var wg in wgs)
                {
                    ICMSTreeNode wgNode = new ICMSTreeNode("WG");
                    wgNode.TableID = wg.WGID;
                    wgNode.DataType = EnumCloudOperationType.WirelessGateway;
                    wgNode.Order = EnumSortOrder.Gateway;

                    allSelectedNodes.Add(wgNode);
                }

                var count = allSelectedNodes.Count;
                int tempPercentage = (100 / count);
                int progress = 0;

                foreach (ICMSTreeNode treeNode in allSelectedNodes)
                {
                    CloudPushHelper.CloudSend(treeNode);

                    progress += tempPercentage;
                    worker.ReportProgress(progress);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                throw;
            }
        }

        private void GetAllSelectedNodes(ICMSTreeNode treeNode, List<ICMSTreeNode> allSelectedNodes)
        {
            if (treeNode.Checked)
            {
                allSelectedNodes.Add(treeNode);
            }
            var nodes = treeNode.Nodes;
            foreach (ICMSTreeNode node in nodes)
            {
                GetAllSelectedNodes(node, allSelectedNodes);
            }
        }
    }
}
