
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iCMS.Setup.DBUpgrade.NewEF.Models;
using olds = iCMS.Setup.DBUpgrade.OldEF.Models.OiCMSDBContext;
using news = iCMS.Setup.DBUpgrade.NewEF.Models.iCMSDB1Context;

using iCMS.Setup.DBUpgrade.OldEF.Models;
using System.Linq.Expressions;
using iCMS.Setup.DBUpgrade.UpgradeFM.Common;
using iCMS.Setup.DBUpgrade.UpgradeFM.DB;
using System.Threading;
using System.Xml;
using System.Configuration;
using System.Data.SqlClient;
using iCMS.Common.Component.Tool;

namespace iCMS.Setup.DBUpgrade.UpgradeFM
{
    #region 数据库升级
    /// <summary>
    /// 数据库升级
    /// </summary>
    public partial class UpgradeMain : Form
    {
        olds oldDB = null;
        news newDB = null;

        int currentIndex = 0;
        int max = 10000;

        //线程处理数据量
        int threedDataCount = 100000;

        int checkMax = 0;

        public UpgradeMain()
        {
            InitializeComponent();
        }

        #region 数据迁移事件
        /// <summary>
        /// 数据迁移事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void transferBtn_Click(object sender, EventArgs e)
        {
            transferBtn.Enabled = false;
            SetAppConfig();

            oldDB = new olds();
            newDB = new news();

            Thread thread = new Thread(new ThreadStart(LoadData));
            thread.IsBackground = true;
            thread.Start();

            progressBar1.Minimum = 0;
            progressBar1.Maximum = max;
        }
        #endregion

        #region 加载数据
        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {

            try
            {
                SetRadioButton(monitorPRbt);
                //添加监测树扩展信息
                AddMonitorProperty();

                SetRadioButton(monitorTreeRbt);

                //添加监测树
                AddMonitorTree();

                SetRadioButton(deviceRbt);
                //添加设备
                AddDevice();

                SetRadioButton(measureSiteRbt);
                //添加测点
                AddMeasureSite();


                SetRadioButton(wgRbt);
                //添加无线网关
                AddWG();

                SetRadioButton(wsRbt);
                //无线传感器
                AddWS();


                SetRadioButton(vibsignalRbt);
                //添加振动信号
                AddVibsignal();

                SetRadioButton(vibsignalSetRbt);
                //添加预值
                AddEigenValue();

                SetRadioButton(measureSiteSetRbt);

                //添加测点的报警预值
                AddMeasureAlmSet();



                SetRadioButton(deviceAlarmRbt);
                //设备报警记录添加
                AddDeviceAlarmRecord();

                SetRadioButton(wsAlarmRbt);
                //传感器报警记录添加
                AddWSAlarmRecord();

                SetRadioButton(deviceDataRbt);
                //添加设备的采集数据
                AddDeviceData();


                SetRadioButton(vibsignalDataRbt);
                //添加振动信号数据
                AddVibSignalData();

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);

                MessageBox.Show("数据迁移出错，请核对数据库连接或查看日志");
            }
            finally
            {
                MessageBox.Show("数据库迁移完成。");
            }


            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                transferBtn.Enabled = true;
            }));
        }
        #endregion

        #region 添加监测树扩展信息
        public void AddMonitorProperty()
        {
            //  string sql = " SET IDENTITY_INSERT [dbo].[T_SYS_MONITOR_TREE_PROPERTY] ON ";
            // newDB.Database.ExecuteSqlCommand(sql);
            //查找监测树属性信息

            DbContextRepository<T_SYS_MONITOR_TREE_PROPERTY> re = new DbContextRepository<T_SYS_MONITOR_TREE_PROPERTY>();
            List<T_SYS_MONITOR_TREE_PROPERTY> lt = new List<T_SYS_MONITOR_TREE_PROPERTY>();

            var monitorPropertyList = oldDB.T_MonitorTreeProperty.Where(item => item.MonitorTreePropertyID > 0);

            max = monitorPropertyList.Count();
            if (max == 0)
            {
                return;
            }
            currentIndex = 1;
            SetMaxPbValue(max);
            SetPbValue(currentIndex);
            SetLabelText(SetMessage(currentIndex, max));


            if (monitorPropertyList != null && monitorPropertyList.Count() > 0)
            {
                foreach (var oldMonitorProperty in monitorPropertyList)
                {

                    //查询是否已经添加成功
                    var newMonitorProperty = newDB.T_SYS_MONITOR_TREE_PROPERTY.Where(item => item.MonitorTreePropertyID == oldMonitorProperty.MonitorTreePropertyID).FirstOrDefault();

                    //如果不存在，则不进行添加
                    if (newMonitorProperty == null)
                    {
                        T_SYS_MONITOR_TREE_PROPERTY insertMonitorProperty = new T_SYS_MONITOR_TREE_PROPERTY();
                        insertMonitorProperty.MonitorTreePropertyID = oldMonitorProperty.MonitorTreePropertyID;
                        insertMonitorProperty.Address = oldMonitorProperty.Address;
                        insertMonitorProperty.URL = oldMonitorProperty.URL;
                        insertMonitorProperty.TelphoneNO = oldMonitorProperty.TelphoneNO;
                        insertMonitorProperty.FaxNO = oldMonitorProperty.FaxNO;
                        insertMonitorProperty.Latitude = oldMonitorProperty.Latitude;
                        insertMonitorProperty.Longtitude = oldMonitorProperty.Longtitude;
                        insertMonitorProperty.ChildCount = oldMonitorProperty.ChildCount;
                        insertMonitorProperty.Length = oldMonitorProperty.Length;
                        insertMonitorProperty.Width = oldMonitorProperty.Width;
                        insertMonitorProperty.Area = oldMonitorProperty.Area;
                        insertMonitorProperty.PersonInCharge = oldMonitorProperty.PersonInCharge;
                        insertMonitorProperty.PersonInChargeTel = oldMonitorProperty.PersonInChargeTel;
                        insertMonitorProperty.Remark = oldMonitorProperty.Remark;
                        insertMonitorProperty.AddDate = oldMonitorProperty.AddDate;
                        lt.Add(insertMonitorProperty);
                    }
                    SetLabelText(SetMessage(currentIndex, max));
                    SetPbValue(currentIndex);
                    currentIndex++;
                }
            }

            //有数据
            if (lt.Count() > 0)
            {
                re.BulkInsert(lt, true);
            }

        }
        #endregion

        #region 添加监测树
        /// <summary>
        /// 添加监测树
        /// </summary>
        public void AddMonitorTree()
        {
            DbContextRepository<T_SYS_MONITOR_TREE> re = new DbContextRepository<T_SYS_MONITOR_TREE>();
            List<T_SYS_MONITOR_TREE> lt = new List<T_SYS_MONITOR_TREE>();
            // string sql = “ select columnA, columnB from TableA where 1 = 1 ”;
            // oldDB.ex<TableAObject>(sql).ToList();//TableAObject就是你定义的对象，对
            //获取历史数据，从历史数据库中
            var monitorList = oldDB.T_MonitorTree.Where(item => item.MonitorTreeID > 0);
            max = monitorList.Count();
            if (max == 0)
            {
                return;
            }
            currentIndex = 1;
            SetMaxPbValue(max);
            SetPbValue(currentIndex);
            SetLabelText(SetMessage(currentIndex, max));


            if (monitorList != null && monitorList.Count() > 0)
            {
                //遍历历史数据
                foreach (var oldMonitor in monitorList)
                {
                    //查询是否已经插入到新数据库中
                    var newMonitor = newDB.T_SYS_MONITOR_TREE.Where(item => item.MonitorTreeID == oldMonitor.MonitorTreeID).FirstOrDefault();

                    //如果数据库已经存在，则不进行添加，如果不存在，则进行添加
                    if (newMonitor == null)
                    {
                        T_SYS_MONITOR_TREE insertMonitor = new T_SYS_MONITOR_TREE();
                        insertMonitor.MonitorTreeID = oldMonitor.MonitorTreeID;
                        insertMonitor.PID = oldMonitor.PID;
                        insertMonitor.OID = oldMonitor.OID;
                        insertMonitor.IsDefault = 1;
                        insertMonitor.Name = oldMonitor.Name;
                        insertMonitor.Des = oldMonitor.Des;
                        //获取监测树类型
                        var common = oldDB.T_Common.Where(item => item.CID == oldMonitor.Type).FirstOrDefault();

                        //监测树类型id
                        int typeId = 0;
                        if (common != null)
                        {
                            string typeName = common.CValue;
                            var monitorType = newDB.T_DICT_MONITORTREE_TYPE.Where(item => item.Name == typeName).FirstOrDefault();

                            if (monitorType != null)
                            {
                                typeId = monitorType.ID;
                            }
                        }
                        insertMonitor.Type = typeId;
                        insertMonitor.ImageID = oldMonitor.ImageID;
                        insertMonitor.MonitorTreePropertyID = oldMonitor.MonitorTreePropertyID;
                        insertMonitor.Status = oldMonitor.Status;
                        insertMonitor.AddDate = DateTime.Now;

                        if (insertMonitor.MonitorTreePropertyID == 0)
                        {

                            try
                            {
                                //添加一条属性数据
                                T_SYS_MONITOR_TREE_PROPERTY insertMonitorProperty = new T_SYS_MONITOR_TREE_PROPERTY();
                                insertMonitorProperty.Address = "";
                                insertMonitorProperty.AddDate = DateTime.Now;
                                newDB.T_SYS_MONITOR_TREE_PROPERTY.Add(insertMonitorProperty);
                                newDB.SaveChanges();
                                insertMonitor.MonitorTreePropertyID = insertMonitorProperty.MonitorTreePropertyID;

                            }
                            catch (Exception ex)
                            {
                                LogHelper.WriteLog(ex);
                            }

                        }
                        lt.Add(insertMonitor);


                    }
                    SetLabelText(SetMessage(currentIndex, max));
                    SetPbValue(currentIndex);
                    currentIndex++;
                }
            }
            //有数据
            if (lt.Count() > 0)
            {
                re.BulkInsert(lt, true);
            }
        }
        #endregion

        #region 添加无线网关

        /// <summary>
        /// 添加无线网关
        /// </summary>
        public void AddWG()
        {

            DbContextRepository<T_SYS_WG> re = new DbContextRepository<T_SYS_WG>();
            List<T_SYS_WG> lt = new List<T_SYS_WG>();


            //查询历史数据库数据
            var oldWGList = oldDB.T_WG.Where(item => item.WGID > 0);

            max = oldWGList.Count();
            if (max == 0)
            {
                return;
            }
            currentIndex = 1;
            SetMaxPbValue(max);
            SetPbValue(currentIndex);
            SetLabelText(SetMessage(currentIndex, max));


            if (oldWGList != null && oldWGList.Count() > 0)
            {
                //遍历数据
                foreach (var wg in oldWGList)
                {
                    //查询新数据库是否已经插入
                    var newWG = newDB.T_SYS_WG.Where(item => item.WGID == wg.WGID).FirstOrDefault();
                    if (newWG == null)
                    {
                        T_SYS_WG insertWG = new T_SYS_WG();
                        insertWG.WGID = wg.WGID;
                        insertWG.WGName = wg.WGName;
                        insertWG.WGNO = wg.WGNO;
                        insertWG.NetWorkID = wg.NetWorkID;
                        int wgType = 0;
                        //获取监测树类型
                        var common = oldDB.T_Common.Where(item => item.CID == wg.WGType).FirstOrDefault();

                        //监测树类型id
                        int typeId = 0;
                        if (common != null)
                        {
                            string typeName = common.CValue;
                            var monitorType = newDB.T_DICT_WIRELESS_GATEWAY_TYPE.Where(item => item.Name == typeName).FirstOrDefault();

                            if (monitorType != null)
                            {
                                typeId = monitorType.ID;
                            }
                        }

                        //网关类型id
                        insertWG.WGType = typeId;
                        //网关连接状态1连接其它断开
                        insertWG.LinkStatus = wg.LinkStatus == 1 ? 1 : 0;
                        insertWG.AddDate = wg.AddDate;
                        insertWG.SoftwareVersion = wg.SoftwareVersion;
                        insertWG.RunStatus = wg.RunStatus;
                        insertWG.ImageID = wg.ImageID;
                        insertWG.Remark = wg.Remark;
                        insertWG.PersonInCharge = wg.PersonInCharge ?? "";
                        insertWG.PersonInChargeTel = wg.PersonInChargeTel ?? "";
                        insertWG.MonitorTreeID = wg.MonitorTreeID;
                        insertWG.AgentAddress = "";
                        lt.Add(insertWG);

                        //现库为空
                        //insertWG.WGModel = wg.Model;
                    }

                    SetLabelText(SetMessage(currentIndex, max));
                    SetPbValue(currentIndex);
                    currentIndex++;
                }
            }

            //有数据
            if (lt.Count() > 0)
            {
                re.BulkInsert(lt, true);
            }
        }
        #endregion

        #region 添加传感器
        public void AddWS()
        {
            DbContextRepository<T_SYS_WS> re = new DbContextRepository<T_SYS_WS>();
            List<T_SYS_WS> lt = new List<T_SYS_WS>();
            //查询历史数据库所有数据
            var wsList = oldDB.T_WS.Where(item => item.WSID > 0);

            max = wsList.Count();
            if (max == 0)
            {
                return;
            }
            currentIndex = 1;
            SetMaxPbValue(max);
            SetPbValue(currentIndex);
            SetLabelText(SetMessage(currentIndex, max));



            if (wsList != null && wsList.Count() > 0)
            {
                foreach (var ws in wsList)
                {

                    var newWS = newDB.T_SYS_WS.Where(item => item.WSID == ws.WSID).FirstOrDefault();
                    //新数据库不存在数据
                    if (newWS == null)
                    {
                        T_SYS_WS insertWS = new T_SYS_WS();
                        insertWS.WSID = ws.WSID;
                        insertWS.WGID = ws.WGID;
                        insertWS.WSNO = ws.WSNO;
                        insertWS.BatteryVolatage = ws.BatteryVolatage;
                        insertWS.MACADDR = ws.MACADDR;
                        insertWS.WSName = ws.WSName;

                        //获取监测树类型
                        var common = oldDB.T_Common.Where(item => item.CID == ws.SensorType).FirstOrDefault();

                        //监测树类型id
                        int sensorTypeId = 0;
                        if (common != null)
                        {
                            string typeName = common.CValue;
                            var commonType = newDB.T_DICT_SENSOR_TYPE.Where(item => item.Name == typeName).FirstOrDefault();

                            if (commonType != null)
                            {
                                sensorTypeId = commonType.ID;
                            }
                        }

                        insertWS.SensorType = sensorTypeId;

                        //连接状态1为连接，其它为断开
                        insertWS.LinkStatus = ws.LinkStatus == 1 ? 1 : 0;
                        insertWS.AddDate = ws.AddDate;
                        insertWS.Vendor = ws.Vendor;
                        insertWS.WSModel = ws.Model;
                        NullableConverter nullableConverter = new NullableConverter(typeof(DateTime?));
                        DateTime? setupTime = DateTime.MinValue;
                        if (!string.IsNullOrEmpty(ws.SetupTime))
                        {
                            setupTime = (DateTime?)Convert.ToDateTime(ws.SetupTime);
                        }
                        else
                        {
                            setupTime = null;
                        }

                        insertWS.SetupTime = setupTime;
                        insertWS.SetupPersonInCharge = ws.SetupPersonInCharge;
                        insertWS.SNCode = ws.SNCode;
                        insertWS.FirmwareVersion = ws.FirmwareVersion;
                        insertWS.AntiExplosionSerialNo = ws.AntiExplosionSerialNo;
                        insertWS.RunStatus = 1;
                        insertWS.ImageID = ws.ImageID;
                        insertWS.PersonInCharge = ws.PersonInCharge;
                        insertWS.PersonInChargeTel = ws.PersonInChargeTel;
                        insertWS.Remark = ws.Remark;
                        insertWS.AddDate = DateTime.Now;
                        int usestatus = 0;//使用状态
                        var oldWS = oldDB.T_MeasureSite.Where(item => item.WSID == ws.WSID).FirstOrDefault();
                        if (oldWS != null)
                        {
                            usestatus = 1;
                        }
                        insertWS.UseStatus = usestatus;
                        insertWS.AlmStatus = ws.Status;
                        lt.Add(insertWS);

                    }

                    SetLabelText(SetMessage(currentIndex, max));
                    SetPbValue(currentIndex);
                    currentIndex++;
                }
            }
            //有数据
            if (lt.Count() > 0)
            {
                re.BulkInsert(lt, true);
            }
        }
        #endregion

        #region 添加设备

        /// <summary>
        /// 添加设备
        /// </summary>
        public void AddDevice()
        {
            DbContextRepository<T_SYS_DEVICE> re = new DbContextRepository<T_SYS_DEVICE>();
            List<T_SYS_DEVICE> lt = new List<T_SYS_DEVICE>();
            //查询历史库设备数据
            var deviceList = oldDB.T_Device.Where(item => item.DevID > 0);

            max = deviceList.Count();
            if (max == 0)
            {
                return;
            }
            currentIndex = 1;
            SetMaxPbValue(max);
            SetPbValue(currentIndex);
            SetLabelText(SetMessage(currentIndex, max));


            if (deviceList != null && deviceList.Count() > 0)
            {
                foreach (var device in deviceList)
                {
                    //查询新库中是否已经存在数据
                    var newDevice = newDB.T_SYS_DEVICE.Where(item => item.DevID == device.DevID).FirstOrDefault();
                    if (newDevice == null)
                    {
                        T_SYS_DEVICE insertDevice = new T_SYS_DEVICE();
                        insertDevice.DevID = device.DevID;
                        insertDevice.MonitorTreeID = device.MonitorTreeID ?? 0;
                        insertDevice.DevName = device.DevName;

                        insertDevice.DevNO = device.DevNO;
                        insertDevice.Rotate = device.Rotate;

                        //获取监测树类型
                        var common = oldDB.T_Common.Where(item => item.CID == device.DevType).FirstOrDefault();

                        //监测树类型id
                        int typeId = 0;
                        if (common != null)
                        {
                            string typeName = common.CValue;
                            var type = newDB.T_DICT_DEVICE_TYPE.Where(item => item.Name == typeName).FirstOrDefault();

                            if (type != null)
                            {
                                typeId = type.ID;
                            }
                        }
                        insertDevice.DevType = typeId;


                        insertDevice.DevManufacturer = device.DevManufacturer;
                        insertDevice.LastCheckDate = device.LastCheckDate;
                        insertDevice.DevManager = device.DevManager;
                        insertDevice.DevPic = device.DevPic;
                        insertDevice.DevMadeDate = device.DevMadeDate;
                        insertDevice.DevMark = device.DevMark;
                        insertDevice.Longitude = device.Longitude;
                        insertDevice.Latitude = device.Latitude;
                        insertDevice.RunStatus = 1;//运行状态
                        insertDevice.DevSDate = device.DevSDate;
                        insertDevice.AddDate = device.AddDate;
                        insertDevice.Width = device.Width;
                        insertDevice.Length = device.Length;
                        insertDevice.Height = device.Height;
                        //insertDevice.Model = device.Model;
                        //   insertDevice.BearingType = device.BearingType;
                        // insertDevice.BearingModel = device.BearingModel;
                        // insertDevice.LubricatingForm = device.LubricatingForm;
                        insertDevice.outputVolume = device.outputVolume;
                        insertDevice.Position = device.Position;
                        insertDevice.SensorSize = device.SensorSize;
                        insertDevice.Power = device.Power;
                        // insertDevice.GroupNo = device.GroupNo;
                        insertDevice.RatedCurrent = device.RatedCurrent;
                        insertDevice.RatedVoltage = device.RatedVoltage;
                        insertDevice.Media = device.Media;
                        insertDevice.OutputMaxPressure = device.OutputMaxPressure;
                        insertDevice.HeadOfDelivery = device.HeadOfDelivery;
                        insertDevice.CouplingType = device.CouplingType;
                        insertDevice.ImageID = device.ImageID;
                        insertDevice.PersonInCharge = device.PersonInCharge;
                        insertDevice.PersonInChargeTel = device.PersonInChargeTel;
                        insertDevice.DevModel = device.DevModel;
                        insertDevice.AlmStatus = device.RunStatus;

                        lt.Add(insertDevice);

                    }

                    SetLabelText(SetMessage(currentIndex, max));
                    SetPbValue(currentIndex);
                    currentIndex++;
                }
            }

            //有数据
            if (lt.Count() > 0)
            {
                re.BulkInsert(lt, true);
            }
        }

        #endregion

        #region 添加测量位置

        /// <summary>
        /// 添加测量位置
        /// </summary>
        public void AddMeasureSite()
        {
            DbContextRepository<T_SYS_MEASURESITE> re = new DbContextRepository<T_SYS_MEASURESITE>();
            List<T_SYS_MEASURESITE> lt = new List<T_SYS_MEASURESITE>();
            //获取历史库中设备信息
            var measureSiteList = oldDB.T_MeasureSite.Where(item => item.MSiteID > 0);

            max = measureSiteList.Count();
            if (max == 0)
            {
                return;
            }
            currentIndex = 1;
            SetMaxPbValue(max);
            SetPbValue(currentIndex);
            SetLabelText(SetMessage(currentIndex, max));



            if (measureSiteList != null && measureSiteList.Count() > 0)
            {
                foreach (var measureSite in measureSiteList)
                {
                    //新库是否存在数据
                    var newMeasureSite = newDB.T_SYS_MEASURESITE.Where(item => item.MSiteID == measureSite.MSiteID).FirstOrDefault();
                    if (newMeasureSite == null)
                    {
                        T_SYS_MEASURESITE insertMeasureSite = new T_SYS_MEASURESITE();
                        insertMeasureSite.MSiteID = measureSite.MSiteID;

                        insertMeasureSite.DevID = measureSite.DevID;
                        insertMeasureSite.VibScanID = measureSite.VibScanID;
                        insertMeasureSite.WSID = measureSite.WSID;
                        insertMeasureSite.ChannelID = measureSite.ChannelID;

                        //获取监测树类型
                        var common = oldDB.T_Common.Where(item => item.CID == measureSite.MSiteName).FirstOrDefault();

                        //监测树类型id
                        int typeId = 0;
                        if (common != null)
                        {
                            string typeName = common.CValue;
                            var type = newDB.T_DICT_MEASURE_SITE_TYPE.Where(item => item.Name == typeName).FirstOrDefault();

                            if (type != null)
                            {
                                typeId = type.ID;
                            }
                        }

                        insertMeasureSite.MSiteName = typeId;

                        insertMeasureSite.MeasureSiteType = 0;

                        insertMeasureSite.SensorCosA = measureSite.SensorCosA;
                        insertMeasureSite.SensorCosB = measureSite.SensorCosB;
                        insertMeasureSite.MSiteStatus = measureSite.MSiteStatus;
                        insertMeasureSite.MSiteSDate = measureSite.MSiteSDate;
                        insertMeasureSite.WaveTime = measureSite.WaveTime;
                        insertMeasureSite.FlagTime = measureSite.FlagTime;
                        insertMeasureSite.AddDate = measureSite.AddDate;
                        insertMeasureSite.Remark = measureSite.Remark;
                        insertMeasureSite.Position = measureSite.Position;
                        insertMeasureSite.SerialNo = measureSite.SerialNo;
                        insertMeasureSite.BearingID = measureSite.BearingID;

                        //添加空数据
                        insertMeasureSite.BearingType = "";
                        insertMeasureSite.LubricatingForm = "";
                        insertMeasureSite.TemperatureTime = measureSite.TemperatureTime;

                        lt.Add(insertMeasureSite);

                    }

                    SetLabelText(SetMessage(currentIndex, max));
                    SetPbValue(currentIndex);
                    currentIndex++;
                }
            }

            //有数据
            if (lt.Count() > 0)
            {
                re.BulkInsert(lt, true);
            }

        }

        #endregion

        #region 添加振动信号

        /// <summary>
        /// 添加振动信号
        /// </summary>
        public void AddVibsignal()
        {
            DbContextRepository<T_SYS_VIBSINGAL> re = new DbContextRepository<T_SYS_VIBSINGAL>();
            List<T_SYS_VIBSINGAL> lt = new List<T_SYS_VIBSINGAL>();
            //振动信号历史数据
            var vibsignalList = oldDB.T_VibSingal.Where(item => item.SingalID > 0);

            max = vibsignalList.Count();
            if (max == 0)
            {
                return;
            }
            currentIndex = 1;
            SetMaxPbValue(max);
            SetPbValue(currentIndex);
            SetLabelText(SetMessage(currentIndex, max));


            if (vibsignalList != null && vibsignalList.Count() > 0)
            {
                foreach (var vibsingal in vibsignalList)
                {

                    //查询新库是否存在数据
                    var newVibsignal = newDB.T_SYS_VIBSINGAL.Where(item => item.SingalID == vibsingal.SingalID).FirstOrDefault();
                    if (newVibsignal == null)
                    {
                        T_SYS_VIBSINGAL insertVibSignal = new T_SYS_VIBSINGAL();
                        insertVibSignal.SingalID = vibsingal.SingalID;
                        insertVibSignal.MSiteID = vibsingal.MSiteID;

                        //获取类型
                        var common = oldDB.T_Common.Where(item => item.CID == vibsingal.SingalType).FirstOrDefault();

                        //类型id
                        int typeId = 0;
                        string signalTypeName = string.Empty;
                        if (common != null)
                        {
                            string typeName = common.CValue;
                            var type = newDB.T_DICT_VIBRATING_SIGNAL_TYPE.Where(item => item.Name == typeName).FirstOrDefault();

                            if (type != null)
                            {
                                typeId = type.ID;
                                signalTypeName = type.Name;
                            }
                        }


                        insertVibSignal.SingalType = typeId;

                        //查询振动信号类型名称

                        if (signalTypeName != "包络")
                        {

                            //获取类型
                            common = oldDB.T_Common.Where(item => item.CID == vibsingal.UpLimitFrequency).FirstOrDefault();

                            //类型id
                            typeId = 0;
                            if (common != null)
                            {
                                int cValue = Convert.ToInt32(common.CValue);
                                var type = newDB.T_DICT_WAVE_UPPERLIMIT_VALUE.Where(item => item.WaveUpperLimitValue == cValue && item.VibratingSignalTypeID == insertVibSignal.SingalType).FirstOrDefault();

                                if (type != null)
                                {
                                    typeId = type.ID;
                                }
                            }


                            insertVibSignal.UpLimitFrequency = typeId;

                            //获取类型
                            common = oldDB.T_Common.Where(item => item.CID == vibsingal.LowLimitFrequency).FirstOrDefault();

                            //类型id
                            typeId = 0;
                            if (common != null)
                            {
                                int cValue = Convert.ToInt32(common.CValue);
                                var type = newDB.T_DICT_WAVE_LOWERLIMIT_VALUE.Where(item => item.WaveLowerLimitValue == cValue && item.VibratingSignalTypeID == insertVibSignal.SingalType).FirstOrDefault();

                                if (type != null)
                                {
                                    typeId = type.ID;
                                }
                            }

                            insertVibSignal.LowLimitFrequency = typeId;


                        }
                        else
                        {
                            //获取类型
                            common = oldDB.T_Common.Where(item => item.CID == vibsingal.EnlvpBandW).FirstOrDefault();

                            //类型id
                            typeId = 0;
                            if (common != null)
                            {
                                int cValue = Convert.ToInt32(common.CValue);
                                var type = newDB.T_DICT_WAVE_UPPERLIMIT_VALUE.Where(item => item.WaveUpperLimitValue == cValue && item.VibratingSignalTypeID == insertVibSignal.SingalType).FirstOrDefault();

                                if (type != null)
                                {
                                    typeId = type.ID;
                                }
                            }


                            insertVibSignal.EnlvpBandW = typeId;


                            //获取类型
                            common = oldDB.T_Common.Where(item => item.CID == vibsingal.EnlvpFilter).FirstOrDefault();

                            //类型id
                            typeId = 0;
                            if (common != null)
                            {
                                int cValue = Convert.ToInt32(common.CValue);
                                var type = newDB.T_DICT_WAVE_LOWERLIMIT_VALUE.Where(item => item.WaveLowerLimitValue == cValue && item.VibratingSignalTypeID == insertVibSignal.SingalType).FirstOrDefault();

                                if (type != null)
                                {
                                    typeId = type.ID;
                                }
                            }


                            insertVibSignal.EnlvpFilter = typeId;

                        }






                        insertVibSignal.StorageTrighters = vibsingal.StorageTrighters;






                        insertVibSignal.DAQStyle = 1;



                        insertVibSignal.SingalStatus = vibsingal.SingalStatus;
                        insertVibSignal.SingalSDate = vibsingal.SingalSDate;

                        //获取类型
                        common = oldDB.T_Common.Where(item => item.CID == vibsingal.WaveDataLength).FirstOrDefault();

                        //类型id
                        typeId = 0;
                        if (common != null)
                        {
                            int cValue = Convert.ToInt32(common.CValue);
                            var type = newDB.T_DICT_WAVE_LENGTH_VALUE.Where(item => item.WaveLengthValue == cValue && item.VibratingSignalTypeID == insertVibSignal.SingalType).FirstOrDefault();

                            if (type != null)
                            {
                                typeId = type.ID;
                            }
                        }

                        insertVibSignal.WaveDataLength = typeId;
                        insertVibSignal.AddDate = vibsingal.AddDate;
                        insertVibSignal.Remark = vibsingal.Remark;
                        lt.Add(insertVibSignal);

                    }


                    SetLabelText(SetMessage(currentIndex, max));
                    SetPbValue(currentIndex);
                    currentIndex++;
                }
            }

            //有数据
            if (lt.Count() > 0)
            {
                re.BulkInsert(lt, true);
            }
        }
        #endregion

        #region 添加报警预值

        /// <summary>
        /// 添加振动信号报警预值
        /// </summary>
        public void AddEigenValue()
        {
            DbContextRepository<T_SYS_VIBRATING_SET_SIGNALALM> re = new DbContextRepository<T_SYS_VIBRATING_SET_SIGNALALM>();
            List<T_SYS_VIBRATING_SET_SIGNALALM> lt = new List<T_SYS_VIBRATING_SET_SIGNALALM>();
            //查询历史库中 报警预值
            var signalAlmSetList = oldDB.T_SingalAlmSet.Where(item => item.SingalAlmID > 0);

            max = signalAlmSetList.Count();
            if (max == 0)
            {
                return;
            }
            currentIndex = 1;
            SetMaxPbValue(max);
            SetPbValue(currentIndex);
            SetLabelText(SetMessage(currentIndex, max));


            if (signalAlmSetList != null & signalAlmSetList.Count() > 0)
            {

                foreach (var signalAlmSet in signalAlmSetList)
                {
                    //查询新库是否有最新数据
                    var newSignalAlmSet = newDB.T_SYS_VIBRATING_SET_SIGNALALM.Where(item => item.SingalAlmID == signalAlmSet.SingalAlmID).FirstOrDefault();
                    if (newSignalAlmSet == null)
                    {
                        T_SYS_VIBRATING_SET_SIGNALALM insertSignalAlm = new T_SYS_VIBRATING_SET_SIGNALALM();
                        insertSignalAlm.SingalAlmID = signalAlmSet.SingalAlmID;
                        insertSignalAlm.SingalID = signalAlmSet.SingalID;

                        //获取类型
                        var common = oldDB.T_Common.Where(item => item.CID == signalAlmSet.ValueType).FirstOrDefault();

                        //类型id
                        int typeId = 0;
                        if (common != null)
                        {
                            string typeName = common.CValue;

                            //查到振动信号类型
                            var vibsignalType = newDB.T_SYS_VIBSINGAL.Where(item => item.SingalID == insertSignalAlm.SingalID).FirstOrDefault();
                            var vibsignalTypeId = 0;
                            if (vibsignalType != null)
                            {
                                vibsignalTypeId = vibsignalType.SingalType;
                            }
                            else
                            {
                                continue;//继续执行
                            }

                            var type = newDB.T_DICT_EIGEN_VALUE_TYPE.Where(item => item.Name == typeName && item.VibratingSignalTypeID == vibsignalTypeId).FirstOrDefault();

                            if (type != null)
                            {
                                typeId = type.ID;
                            }
                        }
                        insertSignalAlm.ValueType = typeId;

                        insertSignalAlm.WarnValue = signalAlmSet.WarnValue;
                        insertSignalAlm.AlmValue = signalAlmSet.AlmValue;
                        insertSignalAlm.Status = signalAlmSet.Status;
                        insertSignalAlm.AddDate = signalAlmSet.AddDate;
                        insertSignalAlm.ThrendAlarmPrvalue = 0;
                        insertSignalAlm.UploadTrigger = 0;
                        lt.Add(insertSignalAlm);

                    }

                    SetLabelText(SetMessage(currentIndex, max));
                    SetPbValue(currentIndex);
                    currentIndex++;
                }
            }

            //有数据
            if (lt.Count() > 0)
            {
                re.BulkInsert(lt, true);
            }
        }
        #endregion

        #region 添加测点的报警预值
        /// <summary>
        /// 添加测点的报警记录
        /// </summary>
        public void AddMeasureAlmSet()
        {
            DbContextRepository<T_SYS_VIBSINGAL> re = new DbContextRepository<T_SYS_VIBSINGAL>();
            List<T_SYS_VIBSINGAL> lt = new List<T_SYS_VIBSINGAL>();
            //查询历史库中数据
            var measureAlmSetList = oldDB.T_MSiteAlm.Where(item => item.MSiteAlmID > 0);

            max = measureAlmSetList.Count();
            if (max == 0)
            {
                return;
            }
            currentIndex = 1;
            SetMaxPbValue(max);
            SetPbValue(currentIndex);
            SetLabelText(SetMessage(currentIndex, max));

            if (measureAlmSetList != null && measureAlmSetList.Count() > 0)
            {
                foreach (var measureAlmSet in measureAlmSetList)
                {
                    //获取类型
                    var common = oldDB.T_Common.Where(item => item.CID == measureAlmSet.MSDType).FirstOrDefault();
                    //类型id
                    if (common != null)
                    {
                        string typeName = common.CValue;

                        switch (typeName)
                        {
                            case "温度":
                                AddDeviceTemperatureAlmSet(measureAlmSet);
                                AddWSTemperatureAlmSet(measureAlmSet);
                                break;
                            case "电池电压":
                                AddDeviceBatteryVoltage(measureAlmSet);
                                break;

                        }

                    }

                    SetLabelText(SetMessage(currentIndex, max));
                    SetPbValue(currentIndex);
                    currentIndex++;
                }
            }
        }
        #endregion

        #region 添加设备温度报警预值
        /// <summary>
        /// 添加设备温度报警预值
        /// </summary>
        public void AddDeviceTemperatureAlmSet(T_MSiteAlm alm)
        {
            DbContextRepository<T_SYS_TEMPE_DEVICE_SET_MSITEALM> re = new DbContextRepository<T_SYS_TEMPE_DEVICE_SET_MSITEALM>();
            List<T_SYS_TEMPE_DEVICE_SET_MSITEALM> lt = new List<T_SYS_TEMPE_DEVICE_SET_MSITEALM>();
            //如果现实库不存在，则进行添加
            var newDeviceTemperatureAlmSet = newDB.T_SYS_TEMPE_DEVICE_SET_MSITEALM.Where(item => item.MsiteAlmID == alm.MSiteAlmID).FirstOrDefault();
            if (newDeviceTemperatureAlmSet == null)
            {
                T_SYS_TEMPE_DEVICE_SET_MSITEALM insertMSisteALM = new T_SYS_TEMPE_DEVICE_SET_MSITEALM();
                insertMSisteALM.MsiteAlmID = alm.MSiteAlmID;
                insertMSisteALM.MsiteID = alm.MSiteID;
                insertMSisteALM.WarnValue = alm.WarnValue;
                insertMSisteALM.AlmValue = alm.AlmValue;
                insertMSisteALM.Status = alm.Status;
                insertMSisteALM.AddDate = alm.AddDate;

                lt.Add(insertMSisteALM);



            }
            //有数据
            if (lt.Count() > 0)
            {
                re.BulkInsert(lt, true);
            }
        }
        #endregion

        #region 添加传感器温度报警预值
        /// <summary>
        /// 添加设备温度报警预值
        /// </summary>
        public void AddWSTemperatureAlmSet(T_MSiteAlm alm)
        {
            DbContextRepository<T_SYS_TEMPE_WS_SET_MSITEALM> re = new DbContextRepository<T_SYS_TEMPE_WS_SET_MSITEALM>();
            List<T_SYS_TEMPE_WS_SET_MSITEALM> lt = new List<T_SYS_TEMPE_WS_SET_MSITEALM>();
            //如果现实库不存在，则进行添加
            var newDeviceTemperatureAlmSet = newDB.T_SYS_TEMPE_WS_SET_MSITEALM.Where(item => item.MsiteAlmID == alm.MSiteAlmID).FirstOrDefault();
            if (newDeviceTemperatureAlmSet == null)
            {
                T_SYS_TEMPE_WS_SET_MSITEALM insertMSisteALM = new T_SYS_TEMPE_WS_SET_MSITEALM();
                insertMSisteALM.MsiteAlmID = alm.MSiteAlmID;
                insertMSisteALM.MsiteID = alm.MSiteID;
                insertMSisteALM.WarnValue = alm.WarnValue;
                insertMSisteALM.AlmValue = alm.AlmValue;
                insertMSisteALM.Status = alm.Status;
                insertMSisteALM.AddDate = alm.AddDate;

                lt.Add(insertMSisteALM);



            }
            //有数据
            if (lt.Count() > 0)
            {
                re.BulkInsert(lt, true);
            }
        }
        #endregion

        #region 添加电池电压报警预值
        /// <summary>
        /// 添加设备温度报警预值
        /// </summary>
        public void AddDeviceBatteryVoltage(T_MSiteAlm alm)
        {
            DbContextRepository<T_SYS_VOLTAGE_SET_MSITEALM> re = new DbContextRepository<T_SYS_VOLTAGE_SET_MSITEALM>();
            List<T_SYS_VOLTAGE_SET_MSITEALM> lt = new List<T_SYS_VOLTAGE_SET_MSITEALM>();
            //如果现实库不存在，则进行添加
            var newBatteryVoltageAlmSet = newDB.T_SYS_VOLTAGE_SET_MSITEALM.Where(item => item.MsiteAlmID == alm.MSiteAlmID).FirstOrDefault();
            if (newBatteryVoltageAlmSet == null)
            {
                T_SYS_VOLTAGE_SET_MSITEALM insertALM = new T_SYS_VOLTAGE_SET_MSITEALM();
                insertALM.MsiteAlmID = alm.MSiteAlmID;
                insertALM.MsiteID = alm.MSiteID;
                insertALM.WarnValue = alm.WarnValue;
                insertALM.AlmValue = alm.AlmValue;
                insertALM.Status = alm.Status;
                insertALM.AddDate = alm.AddDate;

                lt.Add(insertALM);


            }
            //有数据
            if (lt.Count() > 0)
            {
                re.BulkInsert(lt, true);
            }
        }
        #endregion

        #region 传感器报警记录添加

        /// <summary>
        ///   报警记录添加
        /// </summary>
        public void AddWSAlarmRecord()
        {
            DbContextRepository<T_SYS_WSN_ALMRECORD> re = new DbContextRepository<T_SYS_WSN_ALMRECORD>();
            List<T_SYS_WSN_ALMRECORD> lt = new List<T_SYS_WSN_ALMRECORD>();
            //查询历史数据报警记录
            var alarmRecordList = oldDB.T_AlmRecord.Where(item => item.AlmRecordID > 0 && item.SingalID == 0 && item.MSAlmID != 28);

            max = alarmRecordList.Count();
            if (max == 0)
            {
                return;
            }
            currentIndex = 1;
            SetMaxPbValue(max);
            SetPbValue(currentIndex);
            SetLabelText(SetMessage(currentIndex, max));

            if (alarmRecordList != null && alarmRecordList.Count() > 0)
            {
                foreach (var alarmRecord in alarmRecordList)
                {
                    var newDeivceAlarmRecord = newDB.T_SYS_WSN_ALMRECORD.Where(item => item.AlmRecordID == alarmRecord.AlmRecordID).FirstOrDefault();
                    if (newDeivceAlarmRecord == null)
                    {
                        T_SYS_WSN_ALMRECORD insert = new T_SYS_WSN_ALMRECORD();
                        insert.AlmRecordID = alarmRecord.AlmRecordID;
                        insert.DevID = alarmRecord.DevID;
                        insert.MSiteID = alarmRecord.MSiteID;
                        // insert.SingalID = alarmRecord.SingalID;
                        int almId = alarmRecord.MSAlmID;
                        int msAlmID = 0;
                        if (almId == 441)
                        {
                            //不导入数据库
                            continue;
                        }

                        switch (almId)
                        {
                            case 0:
                                msAlmID = (int)AlarmInfoType.devVib;
                                break;
                            case 439://无线传感器
                                msAlmID = (int)AlarmInfoType.wsLink;
                                break;
                            case 440://无线网关
                                msAlmID = (int)AlarmInfoType.wgLink;
                                break;
                            case 108://电池电压
                                msAlmID = (int)AlarmInfoType.wsVoltage;
                                break;
                            case 28://温度
                                msAlmID = (int)AlarmInfoType.devTemperature;
                                break;
                            case 209://无线传感器
                                msAlmID = (int)AlarmInfoType.wsLink;
                                break;
                        }

                        insert.MSAlmID = msAlmID;




                        insert.AlmStatus = alarmRecord.AlmStatus;
                        insert.BDate = alarmRecord.BDate;
                        insert.EDate = alarmRecord.EDate;
                        insert.AddDate = alarmRecord.AddDate;
                        insert.LatestStartTime = alarmRecord.LatestStartTime;
                        insert.Content = alarmRecord.Content;

                        //获取设备名称和设备编号
                        var deivce = oldDB.T_Device.Where(item => item.DevID == alarmRecord.DevID).FirstOrDefault();
                        var deivceId = 0;
                        if (deivce != null)
                        {
                            insert.DevName = deivce.DevName;
                            insert.DevNO = deivce.DevNO;
                            deivceId = deivce.DevID;
                        }

                        //获取位置名称
                        var measureSite = newDB.T_SYS_MEASURESITE.Where(item => item.MSiteID == insert.MSiteID).FirstOrDefault();
                        if (measureSite != null)
                        {
                            var measureSiteType = newDB.T_DICT_MEASURE_SITE_TYPE.Where(item => item.ID == measureSite.MSiteName).FirstOrDefault();
                            if (measureSiteType != null)
                            {
                                insert.MSiteName = measureSiteType.Name;
                            }

                            //获取WS
                            var ws = newDB.T_SYS_WS.Where(item => item.WSID == measureSite.WSID).FirstOrDefault();
                            if (ws != null)
                            {
                                insert.WSID = ws.WSID;
                                insert.WSName = ws.WSName;

                                //获取WG
                                var wg = newDB.T_SYS_WG.Where(item => item.WGID == ws.WGID).FirstOrDefault();
                                if (wg != null)
                                {
                                    insert.WGID = wg.WGID;
                                    insert.WGName = wg.WGName;
                                }
                            }
                        }
                        else
                        {
                            var type = (AlarmInfoType)msAlmID;
                            switch (type)
                            {
                                case AlarmInfoType.wgLink:
                                    {
                                        //获取WG
                                        var wg = newDB.T_SYS_WG.Where(item => item.WGID > 0).FirstOrDefault();
                                        if (wg != null)
                                        {
                                            insert.WGID = wg.WGID;
                                            insert.WGName = wg.WGName;
                                        }
                                    }
                                    break;
                                default:
                                    continue;
                            }
                        }


                        if (deivce != null)
                        {
                            //获取监测树id
                            var moniterTree = newDB.T_SYS_MONITOR_TREE.Where(item => item.MonitorTreeID == deivce.MonitorTreeID).FirstOrDefault();
                            if (moniterTree != null)
                            {
                                List<int> intList = new List<int>();
                                intList.Add(moniterTree.MonitorTreeID);
                                GetMonitorTreeIdList(moniterTree, intList);
                                string idList = string.Empty;
                                foreach (var id in intList.Reverse<int>())
                                {
                                    idList += id + "#";
                                }
                                idList = idList.TrimEnd('#');
                                insert.MonitorTreeID = idList;
                            }


                        }

                        //电池电压报警
                        if (alarmRecord.MSAlmID == 108)
                        {
                            //获取报警预值
                            var almSet = oldDB.T_MSiteAlm.Where(item => item.MSiteID == insert.MSiteID && item.MSDType == 108).FirstOrDefault();
                            if (almSet != null)
                            {
                                insert.WarningValue = almSet.WarnValue;
                                insert.DangerValue = almSet.AlmValue;
                            }

                            //获取采集数据
                            var deviceData = oldDB.T_MSiteData.Where(item => item.MSiteID == insert.MSiteID && item.MSDType == 108 && item.MSDDate >= alarmRecord.LatestStartTime).OrderBy(item => item.MSiteDataID).FirstOrDefault();
                            if (deviceData != null)
                            {
                                insert.SamplingValue = deviceData.MSDValue;
                            }
                        }
                        lt.Add(insert);
                    }

                    SetLabelText(SetMessage(currentIndex, max));
                    SetPbValue(currentIndex);
                    currentIndex++;
                }
            }
            //有数据
            if (lt.Count() > 0)
            {
                re.BulkInsert(lt, true);
            }
            SetPbValue(max);
        }
        #endregion

        #region 设备报警记录添加

        /// <summary>
        ///   报警记录添加
        /// </summary>
        public void AddDeviceAlarmRecord()
        {
            DbContextRepository<T_SYS_DEV_ALMRECORD> re = new DbContextRepository<T_SYS_DEV_ALMRECORD>();
            List<T_SYS_DEV_ALMRECORD> lt = new List<T_SYS_DEV_ALMRECORD>();
            //查询历史数据报警记录 28 为设备温度
            var alarmRecordList = oldDB.T_AlmRecord.Where(item => item.AlmRecordID > 0 && item.SingalID > 0 || item.MSAlmID == 28).OrderBy(item => item.AlmRecordID);

            max = alarmRecordList.Count();
            if (max == 0)
            {
                return;
            }
            currentIndex = 1;
            SetMaxPbValue(max);
            SetPbValue(currentIndex);
            SetLabelText(SetMessage(currentIndex, max));


            if (alarmRecordList != null && alarmRecordList.Count() > 0)
            {
                foreach (var alarmRecord in alarmRecordList)
                {
                    var newDeivceAlarmRecord = newDB.T_SYS_DEV_ALMRECORD.Where(item => item.AlmRecordID == alarmRecord.AlmRecordID).FirstOrDefault();
                    if (newDeivceAlarmRecord == null)
                    {
                        T_SYS_DEV_ALMRECORD insert = new T_SYS_DEV_ALMRECORD();
                        insert.AlmRecordID = alarmRecord.AlmRecordID;
                        insert.DevID = alarmRecord.DevID;
                        insert.MSiteID = alarmRecord.MSiteID;
                        insert.SingalID = alarmRecord.SingalID;

                        int almId = alarmRecord.MSAlmID;
                        int msAlmID = 0;
                        if (almId == 441)
                        {
                            //不导入数据库
                            continue;
                        }

                        switch (almId)
                        {
                            case 0:
                                msAlmID = (int)AlarmInfoType.devVib;
                                break;
                            case 439://无线传感器
                                msAlmID = (int)AlarmInfoType.wsLink;
                                break;
                            case 440://无线网关
                                msAlmID = (int)AlarmInfoType.wgLink;
                                break;
                            case 108://电池电压
                                msAlmID = (int)AlarmInfoType.wsVoltage;
                                break;
                            case 28://温度
                                msAlmID = (int)AlarmInfoType.devTemperature;
                                break;
                        }


                        insert.MSAlmID = msAlmID;

                        insert.AlmStatus = alarmRecord.AlmStatus;
                        insert.BDate = alarmRecord.BDate;
                        insert.EDate = alarmRecord.EDate;
                        insert.AddDate = alarmRecord.AddDate;
                        insert.LatestStartTime = alarmRecord.LatestStartTime;
                        insert.Content = alarmRecord.Content;

                        //设备温度报警id
                        if (alarmRecord.MSAlmID == 28)
                        {
                            //报警预值
                            var deivceTemperatureAlarmSet = newDB.T_SYS_TEMPE_DEVICE_SET_MSITEALM.Where(item => item.MsiteID == insert.MSiteID).FirstOrDefault();
                            if (deivceTemperatureAlarmSet != null)
                            {
                                //去掉报警Id
                                // insert.MSAlmID = deivceTemperatureAlarmSet.MsiteAlmID;


                                insert.DangerValue = deivceTemperatureAlarmSet.AlmValue;
                                insert.WarningValue = deivceTemperatureAlarmSet.WarnValue;
                            }

                            //温度采集数据
                            var deviceTemperatureData = oldDB.T_MSiteData.Where(item => item.MSDType == 28 && item.MSiteID == insert.MSiteID && item.MSDDate >= alarmRecord.LatestStartTime).OrderBy(item => item.MSiteDataID).FirstOrDefault();
                            if (deviceTemperatureData != null)
                            {
                                insert.SamplingValue = deviceTemperatureData.MSDValue;
                            }
                        }

                        //获取设备名称和设备编号
                        var deivce = oldDB.T_Device.Where(item => item.DevID == alarmRecord.DevID).FirstOrDefault();
                        if (deivce != null)
                        {
                            insert.DevName = deivce.DevName;
                            insert.DevNO = deivce.DevNO;
                        }

                        //获取位置名称
                        var measureSite = newDB.T_SYS_MEASURESITE.Where(item => item.MSiteID == insert.MSiteID).FirstOrDefault();
                        if (measureSite != null)
                        {
                            var measureSiteType = newDB.T_DICT_MEASURE_SITE_TYPE.Where(item => item.ID == measureSite.MSiteName).FirstOrDefault();
                            if (measureSiteType != null)
                            {
                                insert.MSiteName = measureSiteType.Name;
                            }
                        }
                        else
                        {
                            continue;
                        }

                        //获取振动信号名称
                        var vibSignal = newDB.T_SYS_VIBSINGAL.Where(item => item.SingalID == insert.SingalID).FirstOrDefault();
                        if (vibSignal != null)
                        {
                            var vibSignalType = newDB.T_DICT_VIBRATING_SIGNAL_TYPE.Where(item => item.ID == vibSignal.SingalType).FirstOrDefault();
                            if (vibSignalType != null)
                            {
                                insert.SingalName = vibSignalType.Name;
                            }
                        }


                        if (deivce != null)
                        {
                            //获取监测树id
                            var moniterTree = newDB.T_SYS_MONITOR_TREE.Where(item => item.MonitorTreeID == deivce.MonitorTreeID).FirstOrDefault();
                            if (moniterTree != null)
                            {
                                List<int> intList = new List<int>();
                                intList.Add(moniterTree.MonitorTreeID);
                                GetMonitorTreeIdList(moniterTree, intList);
                                string idList = string.Empty;
                                foreach (var id in intList.Reverse<int>())
                                {
                                    idList += id + "#";
                                }
                                idList = idList.TrimEnd('#');
                                insert.MonitorTreeID = idList;
                            }
                        }

                        //添加特征值名称
                        var almSetList = newDB.T_SYS_VIBRATING_SET_SIGNALALM.Where(item => item.SingalID == insert.SingalID);
                        if (almSetList != null && almSetList.Count() > 0)
                        {
                            //报警预值，设置为一个，直接处理
                            int count = almSetList.Count();
                            if (count == 1)
                            {
                                var almSet = almSetList.FirstOrDefault();
                                var eigenValueName = string.Empty;
                                var eigenValueType = newDB.T_DICT_EIGEN_VALUE_TYPE.Where(item => item.ID == almSet.ValueType).FirstOrDefault();
                                if (eigenValueType != null)
                                {
                                    eigenValueName = eigenValueType.Name;
                                }
                                insert.SingalValue = eigenValueName;
                                insert.WarningValue = almSet.WarnValue;
                                insert.DangerValue = almSet.AlmValue;

                                //设备振动信号报警id
                                if (alarmRecord.MSAlmID != 28)
                                {
                                    insert.SingalAlmID = almSet.SingalAlmID;
                                }

                                //获取采集值

                                var hisData = oldDB.T_VibSingalHisData.Where(item => item.SamplingDate == alarmRecord.EDate && item.SingalID == insert.SingalID).OrderByDescending(item => item.AlmStatus).OrderByDescending(item => item.SamplingDate).FirstOrDefault();
                                if (hisData != null)
                                {
                                    //峰值
                                    if (hisData.E1 > 0)
                                    {
                                        insert.SamplingValue = hisData.E1;
                                    }
                                    //峰峰值
                                    if (hisData.E2 > 0)
                                    {
                                        insert.SamplingValue = hisData.E2;
                                    }
                                    //有效值
                                    if (hisData.E3 > 0)
                                    {
                                        insert.SamplingValue = hisData.E3;
                                    }
                                    //地毯值
                                    if (hisData.E4 > 0)
                                    {
                                        insert.SamplingValue = hisData.E4;
                                    }
                                    //LQ值
                                    if (hisData.E5 > 0)
                                    {
                                        insert.SamplingValue = hisData.E5;
                                    }
                                }
                            }
                            else
                            {
                                //配置多个报警预值时
                                var hisData = oldDB.T_VibSingalHisData.Where(item => item.SamplingDate == alarmRecord.EDate && item.SingalID == insert.SingalID).OrderByDescending(item => item.AlmStatus).OrderByDescending(item => item.SamplingDate).FirstOrDefault();
                                if (hisData != null)
                                {
                                    //峰值
                                    if (hisData.E1 > 0)
                                    {
                                        SetAlarmSet(insert, "峰值");
                                        insert.SamplingValue = hisData.E1;
                                    }
                                    //峰峰值
                                    if (hisData.E2 > 0)
                                    {
                                        SetAlarmSet(insert, "峰峰值");
                                        insert.SamplingValue = hisData.E2;
                                    }
                                    //有效值
                                    if (hisData.E3 > 0)
                                    {
                                        SetAlarmSet(insert, "有效值");
                                        insert.SamplingValue = hisData.E3;
                                    }
                                    //地毯值
                                    if (hisData.E4 > 0)
                                    {
                                        SetAlarmSet(insert, "地毯值");
                                        insert.SamplingValue = hisData.E4;
                                    }
                                    //LQ值
                                    if (hisData.E5 > 0)
                                    {
                                        SetAlarmSet(insert, "LQ值");
                                        insert.SamplingValue = hisData.E5;
                                    }
                                }

                            }
                        }


                        lt.Add(insert);

                    }

                    SetLabelText(SetMessage(currentIndex, max));
                    SetPbValue(currentIndex);
                    currentIndex++;
                }
            }
            //有数据
            if (lt.Count() > 0)
            {
                re.BulkInsert(lt, true);
            }
        }
        #endregion

        #region 添加设备的采集数据
        public void AddDeviceData()
        {

            //查询历史库是否有数据
            int count = oldDB.T_MSiteData.Where(item => item.MSiteDataID > 0).Count();
            //当前页数

            if (count == 0)
            {
                return;
            }
            //线程个数
            int threedCount = (count + threedDataCount - 1) / threedDataCount;

            max = count;
            currentIndex = 1;
            SetMaxPbValue(max);
            SetPbValue(currentIndex);
            SetLabelText(SetMessage(currentIndex, max));

            int pageSize = 1000;

            for (int index = 1; index <= threedCount; index++)
            {
                //MyDelegate mydelegate = new MyDelegate(TestMethod);

                List<int> info = new List<int>();
                info.Add(index);
                info.Add(count);
                info.Add(pageSize);
                info.Add(threedCount);
                //mydelegate.BeginInvoke(info, TestCallback, "a");

                Thread thread = new Thread(new ParameterizedThreadStart(ThreedDevice));
                thread.Start(info);  //启动异步线程 
            }

            //checkMax 二次验证
            while (currentIndex < max && checkMax < max)
            {
                //休息两秒
                Thread.Sleep(1000);
            }
            SetPbValue(max);
            SetLabelText(SetMessage(max, max));
        }
        #endregion

        #region 添加振动信号数据
        /// <summary>
        ///添加振动信号数据 
        /// </summary>
        public void AddVibSignalData()
        {

            //查询历史库是否有数据
            int count = oldDB.T_VibSingalHisData.Where(item => item.HisDataID > 0).Count();
            //当前页数

            if (count == 0)
            {
                return;
            }
            //线程个数
            int threedCount = (count + threedDataCount - 1) / threedDataCount;

            max = count;
            currentIndex = 1;
            checkMax = 0;
            SetMaxPbValue(max);
            SetPbValue(currentIndex);
            SetLabelText(SetMessage(currentIndex, max));

            int pageSize = 1000;

            for (int index = 1; index <= threedCount; index++)
            {
                //MyDelegate mydelegate = new MyDelegate(TestMethod);

                List<int> info = new List<int>();
                info.Add(index);
                info.Add(count);
                info.Add(pageSize);
                info.Add(threedCount);
                //mydelegate.BeginInvoke(info, TestCallback, "a");

                Thread thread = new Thread(new ParameterizedThreadStart(ThreedVibsignal));
                thread.Start(info);  //启动异步线程 
            }

            while (currentIndex < max && checkMax < max)
            {
                //休息两秒
                Thread.Sleep(500);
            }
            SetPbValue(max);
            SetLabelText(SetMessage(max, max));
        }
        #endregion

        #region 公共方法

        #region 添加设备温度数据
        /// <summary>
        /// 添加设备温度数据
        /// </summary>
        /// <param name="data"></param>
        public void AddDeviceTemperatureData(T_MSiteData data)
        {


            switch (data.MSiteID % 4)
            {
                case 0:
                    {
                        var db14 = new iCMSDB1Context();
                        var db12 = new OiCMSDBContext();
                        DbContextRepository<T_DATA_TEMPE_DEVICE_MSITEDATA_1> re = new DbContextRepository<T_DATA_TEMPE_DEVICE_MSITEDATA_1>();
                        List<T_DATA_TEMPE_DEVICE_MSITEDATA_1> lt = new List<T_DATA_TEMPE_DEVICE_MSITEDATA_1>();
                        var newData = db14.T_DATA_TEMPE_DEVICE_MSITEDATA_1.Where(item => item.MsiteDataID == data.MSiteDataID).FirstOrDefault();
                        if (newData == null)
                        {
                            T_DATA_TEMPE_DEVICE_MSITEDATA_1 insert = new T_DATA_TEMPE_DEVICE_MSITEDATA_1();
                            insert.MsiteDataID = data.MSiteDataID;
                            insert.MsiteID = data.MSiteID;
                            insert.SamplingDate = data.MSDDate;
                            insert.MsDataValue = data.MSDValue;
                            insert.Status = data.MSDStatus;
                            insert.AddDate = data.AddDate;
                            insert.MonthDate = data.AddDate.Month;

                            var almSet = db12.T_MSiteAlm.Where(item => item.MSiteID == data.MSiteID && item.MSDType == data.MSDType).FirstOrDefault();
                            if (almSet != null)
                            {
                                insert.WarnValue = almSet.WarnValue;
                                insert.AlmValue = almSet.AlmValue;
                            }


                            lt.Add(insert);
                        }
                        //有数据
                        if (lt.Count() > 0)
                        {
                            re.BulkInsert(lt, true);
                        }
                    }
                    break;
                case 1:
                    {
                        var db14 = new iCMSDB1Context();
                        var db12 = new OiCMSDBContext();
                        DbContextRepository<T_DATA_TEMPE_DEVICE_MSITEDATA_2> re = new DbContextRepository<T_DATA_TEMPE_DEVICE_MSITEDATA_2>();
                        List<T_DATA_TEMPE_DEVICE_MSITEDATA_2> lt = new List<T_DATA_TEMPE_DEVICE_MSITEDATA_2>();
                        var newData = db14.T_DATA_TEMPE_DEVICE_MSITEDATA_2.Where(item => item.MsiteDataID == data.MSiteDataID).FirstOrDefault();
                        if (newData == null)
                        {
                            T_DATA_TEMPE_DEVICE_MSITEDATA_2 insert = new T_DATA_TEMPE_DEVICE_MSITEDATA_2();
                            insert.MsiteDataID = data.MSiteDataID;
                            insert.MsiteID = data.MSiteID;
                            insert.SamplingDate = data.MSDDate;
                            insert.MsDataValue = data.MSDValue;
                            insert.Status = data.MSDStatus;
                            insert.AddDate = data.AddDate;
                            insert.MonthDate = data.AddDate.Month;
                            var almSet = db12.T_MSiteAlm.Where(item => item.MSiteID == data.MSiteID && item.MSDType == data.MSDType).FirstOrDefault();
                            if (almSet != null)
                            {
                                insert.WarnValue = almSet.WarnValue;
                                insert.AlmValue = almSet.AlmValue;
                            }

                            lt.Add(insert);
                        }
                        //有数据
                        if (lt.Count() > 0)
                        {
                            re.BulkInsert(lt, true);
                        }
                    }
                    break;
                case 2:
                    {
                        var db14 = new iCMSDB1Context();
                        var db12 = new OiCMSDBContext();
                        DbContextRepository<T_DATA_TEMPE_DEVICE_MSITEDATA_3> re = new DbContextRepository<T_DATA_TEMPE_DEVICE_MSITEDATA_3>();
                        List<T_DATA_TEMPE_DEVICE_MSITEDATA_3> lt = new List<T_DATA_TEMPE_DEVICE_MSITEDATA_3>();
                        var newData = db14.T_DATA_TEMPE_DEVICE_MSITEDATA_3.Where(item => item.MsiteDataID == data.MSiteDataID).FirstOrDefault();
                        if (newData == null)
                        {
                            T_DATA_TEMPE_DEVICE_MSITEDATA_3 insert = new T_DATA_TEMPE_DEVICE_MSITEDATA_3();
                            insert.MsiteDataID = data.MSiteDataID;
                            insert.MsiteID = data.MSiteID;
                            insert.SamplingDate = data.MSDDate;
                            insert.MsDataValue = data.MSDValue;
                            insert.Status = data.MSDStatus;
                            insert.AddDate = data.AddDate;
                            insert.MonthDate = data.AddDate.Month;
                            var almSet = db12.T_MSiteAlm.Where(item => item.MSiteID == data.MSiteID && item.MSDType == data.MSDType).FirstOrDefault();
                            if (almSet != null)
                            {
                                insert.WarnValue = almSet.WarnValue;
                                insert.AlmValue = almSet.AlmValue;
                            }

                            lt.Add(insert);
                        }
                        //有数据
                        if (lt.Count() > 0)
                        {
                            re.BulkInsert(lt, true);
                        }
                    }
                    break;
                case 3:
                    {
                        var db14 = new iCMSDB1Context();
                        var db12 = new OiCMSDBContext();
                        DbContextRepository<T_DATA_TEMPE_DEVICE_MSITEDATA_4> re = new DbContextRepository<T_DATA_TEMPE_DEVICE_MSITEDATA_4>();
                        List<T_DATA_TEMPE_DEVICE_MSITEDATA_4> lt = new List<T_DATA_TEMPE_DEVICE_MSITEDATA_4>();
                        var newData = db14.T_DATA_TEMPE_DEVICE_MSITEDATA_4.Where(item => item.MsiteDataID == data.MSiteDataID).FirstOrDefault();
                        if (newData == null)
                        {
                            T_DATA_TEMPE_DEVICE_MSITEDATA_4 insert = new T_DATA_TEMPE_DEVICE_MSITEDATA_4();
                            insert.MsiteDataID = data.MSiteDataID;
                            insert.MsiteID = data.MSiteID;
                            insert.SamplingDate = data.MSDDate;
                            insert.MsDataValue = data.MSDValue;
                            insert.Status = data.MSDStatus;
                            insert.AddDate = data.AddDate;
                            insert.MonthDate = data.AddDate.Month;
                            var almSet = db12.T_MSiteAlm.Where(item => item.MSiteID == data.MSiteID && item.MSDType == data.MSDType).FirstOrDefault();
                            if (almSet != null)
                            {
                                insert.WarnValue = almSet.WarnValue;
                                insert.AlmValue = almSet.AlmValue;
                            }

                            lt.Add(insert);
                        }
                        //有数据
                        if (lt.Count() > 0)
                        {
                            re.BulkInsert(lt, true);
                        }
                    }
                    break;

            }
        }
        #endregion

        #region 添加传感器温度数据
        /// <summary>
        /// 添加设备温度数据
        /// </summary>
        /// <param name="data"></param>
        public void AddWSTemperatureData(T_MSiteData data)
        {
            switch (data.MSiteID % 4)
            {
                case 0:
                    {
                        var db14 = new iCMSDB1Context();
                        var db12 = new OiCMSDBContext();
                        DbContextRepository<T_DATA_TEMPE_WS_MSITEDATA_1> re = new DbContextRepository<T_DATA_TEMPE_WS_MSITEDATA_1>();
                        List<T_DATA_TEMPE_WS_MSITEDATA_1> lt = new List<T_DATA_TEMPE_WS_MSITEDATA_1>();
                        var newData = db14.T_DATA_TEMPE_WS_MSITEDATA_1.Where(item => item.MsiteDataID == data.MSiteDataID).FirstOrDefault();
                        if (newData == null)
                        {
                            T_DATA_TEMPE_WS_MSITEDATA_1 insert = new T_DATA_TEMPE_WS_MSITEDATA_1();
                            insert.MsiteDataID = data.MSiteDataID;
                            insert.MsiteID = data.MSiteID;
                            insert.SamplingDate = data.MSDDate;
                            insert.MsDataValue = data.MSDValue;
                            insert.Status = data.MSDStatus;
                            insert.AddDate = data.AddDate;
                            insert.MonthDate = data.AddDate.Month;
                            var almSet = db12.T_MSiteAlm.Where(item => item.MSiteID == data.MSiteID && item.MSDType == data.MSDType).FirstOrDefault();
                            if (almSet != null)
                            {
                                insert.WarnValue = almSet.WarnValue;
                                insert.AlmValue = almSet.AlmValue;
                            }

                            lt.Add(insert);
                        }
                        //有数据
                        if (lt.Count() > 0)
                        {
                            re.BulkInsert(lt, true);
                        }
                    }
                    break;
                case 1:
                    {
                        var db14 = new iCMSDB1Context();
                        var db12 = new OiCMSDBContext();
                        DbContextRepository<T_DATA_TEMPE_WS_MSITEDATA_2> re = new DbContextRepository<T_DATA_TEMPE_WS_MSITEDATA_2>();
                        List<T_DATA_TEMPE_WS_MSITEDATA_2> lt = new List<T_DATA_TEMPE_WS_MSITEDATA_2>();
                        var newData = db14.T_DATA_TEMPE_WS_MSITEDATA_2.Where(item => item.MsiteDataID == data.MSiteDataID).FirstOrDefault();
                        if (newData == null)
                        {
                            T_DATA_TEMPE_WS_MSITEDATA_2 insert = new T_DATA_TEMPE_WS_MSITEDATA_2();
                            insert.MsiteDataID = data.MSiteDataID;
                            insert.MsiteID = data.MSiteID;
                            insert.SamplingDate = data.MSDDate;
                            insert.MsDataValue = data.MSDValue;
                            insert.Status = data.MSDStatus;
                            insert.AddDate = data.AddDate;
                            insert.MonthDate = data.AddDate.Month;
                            var almSet = db12.T_MSiteAlm.Where(item => item.MSiteID == data.MSiteID && item.MSDType == data.MSDType).FirstOrDefault();
                            if (almSet != null)
                            {
                                insert.WarnValue = almSet.WarnValue;
                                insert.AlmValue = almSet.AlmValue;
                            }

                            lt.Add(insert);
                        }
                        //有数据
                        if (lt.Count() > 0)
                        {
                            re.BulkInsert(lt, true);
                        }
                    }
                    break;
                case 2:
                    {
                        var db14 = new iCMSDB1Context();
                        var db12 = new OiCMSDBContext();
                        DbContextRepository<T_DATA_TEMPE_WS_MSITEDATA_3> re = new DbContextRepository<T_DATA_TEMPE_WS_MSITEDATA_3>();
                        List<T_DATA_TEMPE_WS_MSITEDATA_3> lt = new List<T_DATA_TEMPE_WS_MSITEDATA_3>();
                        var newData = db14.T_DATA_TEMPE_WS_MSITEDATA_3.Where(item => item.MsiteDataID == data.MSiteDataID).FirstOrDefault();
                        if (newData == null)
                        {
                            T_DATA_TEMPE_WS_MSITEDATA_3 insert = new T_DATA_TEMPE_WS_MSITEDATA_3();
                            insert.MsiteDataID = data.MSiteDataID;
                            insert.MsiteID = data.MSiteID;
                            insert.SamplingDate = data.MSDDate;
                            insert.MsDataValue = data.MSDValue;
                            insert.Status = data.MSDStatus;
                            insert.AddDate = data.AddDate;
                            insert.MonthDate = data.AddDate.Month;
                            var almSet = db12.T_MSiteAlm.Where(item => item.MSiteID == data.MSiteID && item.MSDType == data.MSDType).FirstOrDefault();
                            if (almSet != null)
                            {
                                insert.WarnValue = almSet.WarnValue;
                                insert.AlmValue = almSet.AlmValue;
                            }


                            lt.Add(insert);
                        }
                        //有数据
                        if (lt.Count() > 0)
                        {
                            re.BulkInsert(lt, true);
                        }
                    }
                    break;
                case 3:
                    {
                        var db14 = new iCMSDB1Context();
                        var db12 = new OiCMSDBContext();
                        DbContextRepository<T_DATA_TEMPE_WS_MSITEDATA_4> re = new DbContextRepository<T_DATA_TEMPE_WS_MSITEDATA_4>();
                        List<T_DATA_TEMPE_WS_MSITEDATA_4> lt = new List<T_DATA_TEMPE_WS_MSITEDATA_4>();
                        var newData = db14.T_DATA_TEMPE_WS_MSITEDATA_4.Where(item => item.MsiteDataID == data.MSiteDataID).FirstOrDefault();
                        if (newData == null)
                        {
                            T_DATA_TEMPE_WS_MSITEDATA_4 insert = new T_DATA_TEMPE_WS_MSITEDATA_4();
                            insert.MsiteDataID = data.MSiteDataID;
                            insert.MsiteID = data.MSiteID;
                            insert.SamplingDate = data.MSDDate;
                            insert.MsDataValue = data.MSDValue;
                            insert.Status = data.MSDStatus;
                            insert.AddDate = data.AddDate;
                            insert.MonthDate = data.AddDate.Month;
                            var almSet = db12.T_MSiteAlm.Where(item => item.MSiteID == data.MSiteID && item.MSDType == data.MSDType).FirstOrDefault();
                            if (almSet != null)
                            {
                                insert.WarnValue = almSet.WarnValue;
                                insert.AlmValue = almSet.AlmValue;
                            }

                            lt.Add(insert);
                        }
                        //有数据
                        if (lt.Count() > 0)
                        {
                            re.BulkInsert(lt, true);
                        }
                    }
                    break;

            }
        }
        #endregion

        #region 添加电池电压数据
        /// <summary>
        /// 添加设备温度数据
        /// </summary>
        /// <param name="data"></param>
        public void AddDeviceBatteryVoltageData(T_MSiteData data)
        {
            switch (data.MSiteID % 4)
            {
                case 0:
                    {

                        var db14 = new iCMSDB1Context();
                        var db12 = new OiCMSDBContext();
                        DbContextRepository<T_DATA_VOLTAGE_WS_MSITEDATA_1> re = new DbContextRepository<T_DATA_VOLTAGE_WS_MSITEDATA_1>();
                        List<T_DATA_VOLTAGE_WS_MSITEDATA_1> lt = new List<T_DATA_VOLTAGE_WS_MSITEDATA_1>();
                        var newData = db14.T_DATA_VOLTAGE_WS_MSITEDATA_1.Where(item => item.MsiteDataID == data.MSiteDataID).FirstOrDefault();
                        if (newData == null)
                        {
                            T_DATA_VOLTAGE_WS_MSITEDATA_1 insert = new T_DATA_VOLTAGE_WS_MSITEDATA_1();
                            insert.MsiteDataID = data.MSiteDataID;
                            insert.MsiteID = data.MSiteID;
                            insert.SamplingDate = data.MSDDate;
                            insert.MsDataValue = data.MSDValue;
                            insert.Status = data.MSDStatus;
                            insert.AddDate = data.AddDate;
                            insert.MonthDate = data.AddDate.Month;
                            var almSet = db12.T_MSiteAlm.Where(item => item.MSiteID == data.MSiteID && item.MSDType == data.MSDType).FirstOrDefault();
                            if (almSet != null)
                            {
                                insert.WarnValue = almSet.WarnValue;
                                insert.AlmValue = almSet.AlmValue;
                            }

                            lt.Add(insert);
                        }
                        //有数据
                        if (lt.Count() > 0)
                        {
                            re.BulkInsert(lt, true);
                        }
                    }
                    break;
                case 1:
                    {
                        var db14 = new iCMS.Setup.DBUpgrade.NewEF.Models.iCMSDB1Context();
                        var db12 = new iCMS.Setup.DBUpgrade.OldEF.Models.OiCMSDBContext();
                        DbContextRepository<T_DATA_VOLTAGE_WS_MSITEDATA_2> re = new DbContextRepository<T_DATA_VOLTAGE_WS_MSITEDATA_2>();
                        List<T_DATA_VOLTAGE_WS_MSITEDATA_2> lt = new List<T_DATA_VOLTAGE_WS_MSITEDATA_2>();
                        var newData = db14.T_DATA_VOLTAGE_WS_MSITEDATA_2.Where(item => item.MsiteDataID == data.MSiteDataID).FirstOrDefault();
                        if (newData == null)
                        {
                            T_DATA_VOLTAGE_WS_MSITEDATA_2 insert = new T_DATA_VOLTAGE_WS_MSITEDATA_2();
                            insert.MsiteDataID = data.MSiteDataID;
                            insert.MsiteID = data.MSiteID;
                            insert.SamplingDate = data.MSDDate;
                            insert.MsDataValue = data.MSDValue;
                            insert.Status = data.MSDStatus;
                            insert.AddDate = data.AddDate;
                            insert.MonthDate = data.AddDate.Month;
                            var almSet = db12.T_MSiteAlm.Where(item => item.MSiteID == data.MSiteID && item.MSDType == data.MSDType).FirstOrDefault();
                            if (almSet != null)
                            {
                                insert.WarnValue = almSet.WarnValue;
                                insert.AlmValue = almSet.AlmValue;
                            }

                            lt.Add(insert);
                        }
                        //有数据
                        if (lt.Count() > 0)
                        {
                            re.BulkInsert(lt, true);
                        }
                    }
                    break;
                case 2:
                    {
                        var db14 = new iCMS.Setup.DBUpgrade.NewEF.Models.iCMSDB1Context();
                        var db12 = new iCMS.Setup.DBUpgrade.OldEF.Models.OiCMSDBContext();
                        DbContextRepository<T_DATA_VOLTAGE_WS_MSITEDATA_3> re = new DbContextRepository<T_DATA_VOLTAGE_WS_MSITEDATA_3>();
                        List<T_DATA_VOLTAGE_WS_MSITEDATA_3> lt = new List<T_DATA_VOLTAGE_WS_MSITEDATA_3>();
                        var newData = db14.T_DATA_VOLTAGE_WS_MSITEDATA_3.Where(item => item.MsiteDataID == data.MSiteDataID).FirstOrDefault();
                        if (newData == null)
                        {
                            T_DATA_VOLTAGE_WS_MSITEDATA_3 insert = new T_DATA_VOLTAGE_WS_MSITEDATA_3();
                            insert.MsiteDataID = data.MSiteDataID;
                            insert.MsiteID = data.MSiteID;
                            insert.SamplingDate = data.MSDDate;
                            insert.MsDataValue = data.MSDValue;
                            insert.Status = data.MSDStatus;
                            insert.AddDate = data.AddDate;
                            insert.MonthDate = data.AddDate.Month;
                            var almSet = db12.T_MSiteAlm.Where(item => item.MSiteID == data.MSiteID && item.MSDType == data.MSDType).FirstOrDefault();
                            if (almSet != null)
                            {
                                insert.WarnValue = almSet.WarnValue;
                                insert.AlmValue = almSet.AlmValue;
                            }

                            lt.Add(insert);
                        }
                        //有数据
                        if (lt.Count() > 0)
                        {
                            re.BulkInsert(lt, true);
                        }
                    }
                    break;
                case 3:
                    {
                        var db14 = new iCMS.Setup.DBUpgrade.NewEF.Models.iCMSDB1Context();
                        var db12 = new iCMS.Setup.DBUpgrade.OldEF.Models.OiCMSDBContext();
                        DbContextRepository<T_DATA_VOLTAGE_WS_MSITEDATA_4> re = new DbContextRepository<T_DATA_VOLTAGE_WS_MSITEDATA_4>();
                        List<T_DATA_VOLTAGE_WS_MSITEDATA_4> lt = new List<T_DATA_VOLTAGE_WS_MSITEDATA_4>();
                        var newData = db14.T_DATA_VOLTAGE_WS_MSITEDATA_4.Where(item => item.MsiteDataID == data.MSiteDataID).FirstOrDefault();
                        if (newData == null)
                        {
                            T_DATA_VOLTAGE_WS_MSITEDATA_4 insert = new T_DATA_VOLTAGE_WS_MSITEDATA_4();
                            insert.MsiteDataID = data.MSiteDataID;
                            insert.MsiteID = data.MSiteID;
                            insert.SamplingDate = data.MSDDate;
                            insert.MsDataValue = data.MSDValue;
                            insert.Status = data.MSDStatus;
                            insert.AddDate = data.AddDate;
                            insert.MonthDate = data.AddDate.Month;
                            var almSet = db12.T_MSiteAlm.Where(item => item.MSiteID == data.MSiteID && item.MSDType == data.MSDType).FirstOrDefault();
                            if (almSet != null)
                            {
                                insert.WarnValue = almSet.WarnValue;
                                insert.AlmValue = almSet.AlmValue;
                            }

                            lt.Add(insert);
                        }
                        //有数据
                        if (lt.Count() > 0)
                        {
                            re.BulkInsert(lt, true);
                        }
                    }
                    break;

            }
        }
        #endregion

        #region 添加加速度振动信号数据
        /// <summary>
        /// 添加加速度振动信号数据
        /// </summary>
        public void AddVibSignalDataForAcc(T_VibSingalHisData data)
        {
            var db12 = new OiCMSDBContext();
            var db14 = new iCMSDB1Context();
            DbContextRepository<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACC> re = new DbContextRepository<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACC>();
            List<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACC> lt = new List<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACC>();

            var newAccData = db14.T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACC.Where(item => item.HisDataID == data.HisDataID).FirstOrDefault();
            if (newAccData == null)
            {
                T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACC insert = new T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACC();
                insert.HisDataID = data.HisDataID;
                insert.SingalID = data.SingalID;
                insert.MsiteID = data.MSID;
                insert.DevID = data.DevID;
                insert.DAQStyle = 1;
                insert.SamplingDate = data.SamplingDate;
                insert.Rotate = data.Rotate.ToString();
                insert.WaveDataPath = data.WaveData;
                insert.TransformCofe = data.TransformCofe;
                insert.RealSamplingFrequency = data.RealSamplingFrequency;
                insert.SamplingPointData = data.SamplingPointData;
                insert.AlmStatus = data.AlmStatus;
                insert.PeakValue = data.E1;
                insert.PeakPeakValue = data.E2;
                insert.EffValue = data.E3;
                insert.AddDate = data.AddDate;
                insert.MonthDate = data.AddDate.Month;

                #region 添加报警预值
                //设置表里面找特征值对就报警预值
                var enginList = db12.T_SingalAlmSet.Where(item => item.SingalID == data.SingalID);
                if (enginList != null && enginList.Count() > 0)
                {
                    foreach (var engin in enginList)
                    {
                        //获取类型
                        var common = db12.T_Common.Where(item => item.CID == engin.ValueType).FirstOrDefault();
                        if (common != null)
                        {

                            string name = common.CValue.Trim();

                            switch (name)
                            {
                                case "峰值":
                                    {
                                        insert.PeakAlmValue = engin.AlmValue;
                                        insert.PeakWarnValue = engin.WarnValue;
                                    }

                                    break;
                                case "峰峰值":
                                    {
                                        insert.PeakPeakAlmValue = engin.AlmValue;
                                        insert.PeakPeakWarnValue = engin.WarnValue;
                                    }

                                    break;
                                case "有效值":
                                    {
                                        insert.EffAlmValue = engin.AlmValue;
                                        insert.EffWarnValue = engin.WarnValue;
                                    }
                                    break;
                            }
                        }
                    }
                }
                #endregion

                lt.Add(insert);
            }
            //有数据
            if (lt.Count() > 0)
            {
                re.BulkInsert(lt, true);
            }
        }
        #endregion

        #region 添加速度振动信号数据
        /// <summary>
        /// 添加速度振动信号数据
        /// </summary>
        public void AddVibSignalDataForVel(T_VibSingalHisData data)
        {
            var db12 = new OiCMSDBContext();
            var db14 = new iCMSDB1Context();
            DbContextRepository<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_VEL> re = new DbContextRepository<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_VEL>();
            List<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_VEL> lt = new List<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_VEL>();

            var newAccData = db14.T_DATA_VIBRATING_SIGNAL_CHAR_HIS_VEL.Where(item => item.HisDataID == data.HisDataID).FirstOrDefault();
            if (newAccData == null)
            {
                T_DATA_VIBRATING_SIGNAL_CHAR_HIS_VEL insert = new T_DATA_VIBRATING_SIGNAL_CHAR_HIS_VEL();
                insert.HisDataID = data.HisDataID;
                insert.SingalID = data.SingalID;
                insert.MsiteID = data.MSID;
                insert.DevID = data.DevID;
                insert.DAQStyle = 1;
                insert.SamplingDate = data.SamplingDate;
                insert.Rotate = data.Rotate.ToString();
                insert.WaveDataPath = data.WaveData;
                insert.TransformCofe = data.TransformCofe;
                insert.RealSamplingFrequency = data.RealSamplingFrequency;
                insert.SamplingPointData = data.SamplingPointData;
                insert.AlmStatus = data.AlmStatus;
                insert.PeakValue = data.E1;
                insert.PeakPeakValue = data.E2;
                insert.EffValue = data.E3;
                insert.AddDate = data.AddDate;
                insert.MonthDate = data.AddDate.Month;

                #region 添加报警预值
                //设置表里面找特征值对就报警预值
                var enginList = db12.T_SingalAlmSet.Where(item => item.SingalID == data.SingalID);
                if (enginList != null && enginList.Count() > 0)
                {
                    foreach (var engin in enginList)
                    {
                        //获取类型
                        var common = db12.T_Common.Where(item => item.CID == engin.ValueType).FirstOrDefault();
                        if (common != null)
                        {

                            string name = common.CValue.Trim();

                            switch (name)
                            {
                                case "峰值":
                                    {
                                        insert.PeakAlmValue = engin.AlmValue;
                                        insert.PeakWarnValue = engin.WarnValue;
                                    }

                                    break;
                                case "峰峰值":
                                    {
                                        insert.PeakPeakAlmValue = engin.AlmValue;
                                        insert.PeakPeakWarnValue = engin.WarnValue;
                                    }

                                    break;
                                case "有效值":
                                    {
                                        insert.EffAlmValue = engin.AlmValue;
                                        insert.EffWarnValue = engin.WarnValue;
                                    }
                                    break;
                            }
                        }
                    }
                }
                #endregion


                lt.Add(insert);
            }
            //有数据
            if (lt.Count() > 0)
            {
                re.BulkInsert(lt, true);
            }
        }
        #endregion

        #region 添加包络振动信号数据
        /// <summary>
        /// 添加速度振动信号数据
        /// </summary>
        public void AddVibSignalDataForEnvel(T_VibSingalHisData data)
        {
            var db12 = new OiCMSDBContext();
            var db14 = new iCMSDB1Context();
            DbContextRepository<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL> re = new DbContextRepository<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL>();
            List<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL> lt = new List<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL>();
            var newAccData = db14.T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL.Where(item => item.HisDataID == data.HisDataID).FirstOrDefault();
            if (newAccData == null)
            {
                T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL insert = new T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL();
                insert.HisDataID = data.HisDataID;
                insert.SingalID = data.SingalID;
                insert.MsiteID = data.MSID;
                insert.DevID = data.DevID;
                insert.DAQStyle = 1;
                insert.SamplingDate = data.SamplingDate;
                insert.Rotate = data.Rotate.ToString();
                insert.WaveDataPath = data.WaveData;
                insert.TransformCofe = data.TransformCofe;
                insert.RealSamplingFrequency = data.RealSamplingFrequency;
                insert.SamplingPointData = data.SamplingPointData;
                insert.AlmStatus = data.AlmStatus;
                insert.PeakValue = data.E1;
                insert.CarpetValue = data.E4;
                insert.AddDate = data.AddDate;
                insert.MonthDate = data.AddDate.Month;
                #region 添加报警预值
                //设置表里面找特征值对就报警预值
                var enginList = db12.T_SingalAlmSet.Where(item => item.SingalID == data.SingalID);
                if (enginList != null && enginList.Count() > 0)
                {
                    foreach (var engin in enginList)
                    {
                        //获取类型
                        var common = db12.T_Common.Where(item => item.CID == engin.ValueType).FirstOrDefault();
                        if (common != null)
                        {

                            string name = common.CValue.Trim();

                            switch (name)
                            {
                                case "峰值":
                                    {
                                        insert.PeakAlmValue = engin.AlmValue;
                                        insert.PeakWarnValue = engin.WarnValue;
                                    }

                                    break;
                                case "地毯值":
                                    {
                                        insert.CarpetAlmValue = engin.AlmValue;
                                        insert.CarpetWarnValue = engin.WarnValue;
                                    }

                                    break;

                            }
                        }
                    }
                }
                #endregion

                lt.Add(insert);
            }
            //有数据
            if (lt.Count() > 0)
            {
                re.BulkInsert(lt, true);
            }
        }
        #endregion

        #region 添加位移振动信号数据
        /// <summary>
        /// 添加速度振动信号数据
        /// </summary>
        public void AddVibSignalDataForDisp(T_VibSingalHisData data)
        {
            var db12 = new OiCMSDBContext();
            var db14 = new iCMSDB1Context();
            DbContextRepository<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP> re = new DbContextRepository<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP>();
            List<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP> lt = new List<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP>();
            var newAccData = db14.T_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP.Where(item => item.HisDataID == data.HisDataID).FirstOrDefault();
            if (newAccData == null)
            {
                T_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP insert = new T_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP();
                insert.HisDataID = data.HisDataID;
                insert.SingalID = data.SingalID;
                insert.MsiteID = data.MSID;
                insert.DevID = data.DevID;
                insert.DAQStyle = 1;
                insert.SamplingDate = data.SamplingDate;
                insert.Rotate = data.Rotate.ToString();
                insert.WaveDataPath = data.WaveData;
                insert.TransformCofe = data.TransformCofe;
                insert.RealSamplingFrequency = data.RealSamplingFrequency;
                insert.SamplingPointData = data.SamplingPointData;
                insert.AlmStatus = data.AlmStatus;
                insert.PeakValue = data.E1;
                insert.PeakPeakValue = data.E2;
                insert.EffValue = data.E3;
                insert.AddDate = data.AddDate;
                insert.MonthDate = data.AddDate.Month;

                #region 添加报警预值
                //设置表里面找特征值对就报警预值
                var enginList = db12.T_SingalAlmSet.Where(item => item.SingalID == data.SingalID);
                if (enginList != null && enginList.Count() > 0)
                {
                    foreach (var engin in enginList)
                    {
                        //获取类型
                        var common = db12.T_Common.Where(item => item.CID == engin.ValueType).FirstOrDefault();
                        if (common != null)
                        {

                            string name = common.CValue.Trim();

                            switch (name)
                            {
                                case "峰值":
                                    {
                                        insert.PeakAlmValue = engin.AlmValue;
                                        insert.PeakWarnValue = engin.WarnValue;
                                    }

                                    break;
                                case "峰峰值":
                                    {
                                        insert.PeakPeakAlmValue = engin.AlmValue;
                                        insert.PeakPeakWarnValue = engin.WarnValue;
                                    }

                                    break;
                                case "有效值":
                                    {
                                        insert.EffAlmValue = engin.AlmValue;
                                        insert.EffWarnValue = engin.WarnValue;
                                    }
                                    break;
                            }
                        }
                    }
                }
                #endregion
                lt.Add(insert);
            }
            //有数据
            if (lt.Count() > 0)
            {
                re.BulkInsert(lt, true);
            }
        }
        #endregion

        #region 添加LQ振动信号数据
        /// <summary>
        /// 添加速度振动信号数据
        /// </summary>
        public void AddVibSignalDataForLQ(T_VibSingalHisData data)
        {
            var db12 = new OiCMSDBContext();
            var db14 = new iCMSDB1Context();
            DbContextRepository<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQ> re = new DbContextRepository<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQ>();
            List<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQ> lt = new List<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQ>();
            var newAccData = db14.T_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQ.Where(item => item.HisDataID == data.HisDataID).FirstOrDefault();
            if (newAccData == null)
            {
                T_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQ insert = new T_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQ();
                insert.HisDataID = data.HisDataID;
                insert.SingalID = data.SingalID;
                insert.MSITEID = data.MSID;
                insert.DevID = data.DevID;
                insert.DAQStyle = 1;
                insert.SamplingDate = data.SamplingDate;
                insert.Rotate = data.Rotate.ToString();
                insert.WaveDataPath = data.WaveData;
                insert.TransformCofe = data.TransformCofe;
                insert.RealSamplingFrequency = data.RealSamplingFrequency;
                insert.SamplingPointData = data.SamplingPointData;
                insert.AlmStatus = data.AlmStatus;
                insert.LQValue = data.E5;
                insert.AddDate = data.AddDate;
                insert.MonthDate = data.AddDate.Month;
                #region 添加报警预值
                //设置表里面找特征值对就报警预值
                var enginList = db12.T_SingalAlmSet.Where(item => item.SingalID == data.SingalID);
                if (enginList != null && enginList.Count() > 0)
                {
                    foreach (var engin in enginList)
                    {
                        //获取类型
                        var common = db12.T_Common.Where(item => item.CID == engin.ValueType).FirstOrDefault();
                        if (common != null)
                        {

                            string name = common.CValue.Trim();

                            switch (name)
                            {
                                case "LQ值":
                                    {
                                        insert.LQAlmValue = engin.AlmValue;
                                        insert.LQWarnValue = engin.WarnValue;
                                    }

                                    break;
                            }
                        }
                    }
                }
                #endregion
                lt.Add(insert);
            }
            //有数据
            if (lt.Count() > 0)
            {
                re.BulkInsert(lt, true);
            }
        }
        #endregion

        #region 分页方法
        /// <summary>
        /// 分页方法
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <typeparam name="TKey">key</typeparam>
        /// <param name="select"></param>
        /// <param name="where"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="Total"></param>
        /// <returns></returns>
        public List<T> getPageDate<T, TKey>(Expression<Func<T, T>> select, Expression<Func<T, bool>> where, Expression<Func<T, TKey>> order, int pageIndex, int pageSize, out int Total)
            where T : class
        {

            var db = new iCMS.Setup.DBUpgrade.OldEF.Models.OiCMSDBContext();
            Total = db.Set<T>().Where(where).Count();
            var list = db.Set<T>().Where(where).OrderByDescending(order).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return list.ToList();
        }
        #endregion

        #region 监测树查询Id列表

        /// <summary>
        /// 递归查找id
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="idList"></param>
        public void GetMonitorTreeIdList(T_SYS_MONITOR_TREE tree, List<int> idList)
        {

            var treeInfo = newDB.T_SYS_MONITOR_TREE.Where(item => item.MonitorTreeID == tree.PID).FirstOrDefault();
            if (treeInfo != null)
            {

                idList.Add(treeInfo.MonitorTreeID);
                GetMonitorTreeIdList(treeInfo, idList);
            }
        }
        #endregion


        /// <summary>
        /// 设置特征值报警预值和名称
        /// </summary>
        /// <param name="insert"></param>
        /// <param name="eigenValueName"></param>
        public void SetAlarmSet(T_SYS_DEV_ALMRECORD insert, string eigenValueName)
        {
            //获取振动信号灯类型
            var vibsignalType = newDB.T_SYS_VIBSINGAL.Where(item => item.SingalID == insert.SingalID).FirstOrDefault();
            if (vibsignalType != null)
            {
                //获取特征值类型
                var eigenType = newDB.T_DICT_EIGEN_VALUE_TYPE.Where(item => item.Name == eigenValueName && item.VibratingSignalTypeID == vibsignalType.SingalType).FirstOrDefault();
                if (eigenType != null)
                {
                    //获取报警设置 
                    var signalAlmSet = newDB.T_SYS_VIBRATING_SET_SIGNALALM.Where(item => item.SingalID == insert.SingalID && item.ValueType == eigenType.ID).FirstOrDefault();
                    if (signalAlmSet != null)
                    {
                        insert.SingalValue = eigenType.Name;
                        insert.WarningValue = signalAlmSet.WarnValue;
                        insert.DangerValue = signalAlmSet.AlmValue;
                    }

                    //设备振动信号报警id
                    if (insert.MSAlmID != 28)
                    {
                        var deviceAlarmSet = newDB.T_SYS_VIBRATING_SET_SIGNALALM.Where(item => item.SingalID == insert.SingalID && item.ValueType == eigenType.ID).FirstOrDefault();
                        if (deviceAlarmSet != null)
                        {
                            insert.SingalAlmID = deviceAlarmSet.SingalAlmID;
                        }
                    }
                }
            }
        }
        #endregion

        #region 委托

        public delegate string MyDelegate(object data);
        delegate void labDelegate(string str);
        private void SetLabelText(string str)
        {
            if (messageLbl.InvokeRequired)
            {

                try
                {
                    Invoke(new labDelegate(SetLabelText), new string[] { str });
                }
                catch
                {

                }
                finally { }

            }
            else
            {
                messageLbl.Text = str;
            }
        }

        delegate void dgvDelegate(DataTable table);
        //private void SetDgvDataSource(DataTable table)
        //{
        //    //if (dataGridView1.InvokeRequired)
        //    //{
        //    //    Invoke(new dgvDelegate(SetDgvDataSource), new object[] { table });
        //    //}
        //    //else
        //    //{
        //    //    dataGridView1.DataSource = table;
        //    //}
        //}

        private delegate void pbDelegate(int value);
        private void SetPbValue(int value)
        {
            if (progressBar1.InvokeRequired)
            {
                Invoke(new pbDelegate(SetPbValue), new object[] { value });
            }
            else
            {
                progressBar1.Value = value;
            }
        }

        private delegate void pbMaxDelegate(int value);
        private void SetMaxPbValue(int value)
        {
            if (progressBar1.InvokeRequired)
            {
                Invoke(new pbMaxDelegate(SetMaxPbValue), new object[] { value });

            }
            else
            {
                progressBar1.Maximum = value;
            }
        }


        private delegate void SetRadioButtonDelegate(RadioButton rb);
        private void SetRadioButton(RadioButton rb)
        {
            if (rb.InvokeRequired)
            {
                Invoke(new SetRadioButtonDelegate(SetRadioButton), new object[] { rb });

            }
            else
            {
                rb.Select();
            }
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="currentIndex">当前索引</param>
        /// <param name="max">最大条数</param>
        /// <returns></returns>
        public string SetMessage(int currentIndex, int max)
        {
            if (max == 0)
            {
                return "N/A";
            }
            string str = string.Format("总数量：{0}条 完成：{1},百分比：{2:0.00}%", max, currentIndex + "/" + max, Convert.ToDecimal(currentIndex) / Convert.ToDecimal(max) * 100);
            return str;
        }

        #endregion

        #region 修改配置文件
        /// <summary>
        /// 修改配置文件
        /// </summary>
        public void SetAppConfig()
        {
            try
            {
                string oldDBKey = "OldContext";
                string oldDBValue = "Data Source={0};Initial Catalog={1};User ID={2};Password={3};MultipleActiveResultSets=True";
                oldDBValue = string.Format(oldDBValue, oldDBServerNameTxt.Text.Trim(), oldDBNameTxt.Text.Trim(), oldDBUserTxt.Text.Trim(), oldDbPassWordTxt.Text.Trim());
                AppSettingHelper.SetValue(oldDBKey, oldDBValue);

                if (!ConnectionTest(oldDBValue))
                {
                    MessageBox.Show("数据库配置不正确，请检查数据库配置");
                    transferBtn.Enabled = true;
                    return;
                }

                string newDBKey = "NewContext";
                string newDBValue = "Data Source={0};Initial Catalog={1};User ID={2};Password={3};MultipleActiveResultSets=True";
                newDBValue = string.Format(newDBValue, newDBServerNameTxt.Text.Trim(), newDBNameTxt.Text.Trim(), newDBUserTxt.Text.Trim(), newDBPassWordTxt.Text.Trim());
                AppSettingHelper.SetValue(newDBKey, newDBValue);
                if (!ConnectionTest(oldDBValue))
                {
                    MessageBox.Show("数据库配置不正确，请检查数据库配置");
                    transferBtn.Enabled = true;
                    return;
                }

            }
            catch (Exception e)
            {
                string error = e.Message;
                MessageBox.Show("数据库配置不正确，请检查数据库配置");
                transferBtn.Enabled = true;
                return;
            }
        }
        #endregion

        #region 测试连接数据库是否成功
        /// <summary>
        /// 测试连接数据库是否成功
        /// </summary>
        /// <returns></returns>
        public static bool ConnectionTest(string connectionString)
        {


            connectionString = connectionString + ";Connection Timeout=3";
            bool IsCanConnectioned = false;
            //创建连接对象
            SqlConnection mySqlConnection = new SqlConnection(connectionString);
            //ConnectionTimeout 在.net 1.x 可以设置 在.net 2.0后是只读属性，则需要在连接字符串设置
            //如：server=.;uid=sa;pwd=;database=PMIS;Integrated Security=SSPI; Connection Timeout=30
            //mySqlConnection.
            try
            {
                //Open DataBase
                //打开数据库
                mySqlConnection.Open();
                IsCanConnectioned = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                //Can not Open DataBase
                //打开不成功 则连接不成功
                IsCanConnectioned = false;
            }
            finally
            {
                //Close DataBase
                //关闭数据库连接
                //  mySqlConnection.Close();
            }
            //mySqlConnection   is   a   SqlConnection   object 
            if (mySqlConnection.State == ConnectionState.Closed || mySqlConnection.State == ConnectionState.Broken)
            {
                //Connection   is   not   available  
                return IsCanConnectioned;
            }
            else
            {
                //Connection   is   available  
                return IsCanConnectioned;
            }
        }
        #endregion

        #region 多线程处理设备数据
        #region 多线程添加数据
        /// <summary>
        /// 多线程添加数据
        /// </summary>
        /// <param name="startIndex">开始值</param>
        /// <param name="dataCount">数据量</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="threedCount">线程数量</param>
        public void ThreedAddDeviceData(int dataCount, int pageSize, int threedIndex, int threedCount)
        {
            var db12 = new OiCMSDBContext();
            //当前页数
            int pageIndex = 0;
            int totalPageNum = 0;
            int outCount = 0;

            if (threedIndex < threedCount)
            {
                pageIndex = (threedIndex - 1) * threedDataCount / pageSize + 1;
                //总页数
                totalPageNum = threedDataCount / pageSize;
            }
            else
            {
                pageIndex = (threedIndex - 1) * threedDataCount / pageSize + 1;
                //小于总数
                if (threedCount == 1)
                {
                    totalPageNum = (dataCount + pageSize - 1) / pageSize;
                }
                else
                {
                    //总页数
                    totalPageNum = (dataCount - (threedIndex - 1) * threedDataCount + pageSize - 1) / pageSize;
                }

            }
            int datacount = 0;
            for (int i = 0; i < totalPageNum; i++)
            {
                var deviceDataList = getPageDate<T_MSiteData, int>(c => c, c => c.MSiteDataID > 0, c => c.MSiteDataID, pageIndex, pageSize, out outCount);
                if (deviceDataList != null && deviceDataList.Count() > 0)
                {

                    #region 每一100条进行插入
                    foreach (var deviceData in deviceDataList)
                    {
                        //获取类型
                        var common = db12.T_Common.Where(item => item.CID == deviceData.MSDType).FirstOrDefault();
                        //类型id
                        if (common != null)
                        {
                            string typeName = common.CValue;

                            switch (typeName)
                            {
                                case "温度":
                                    AddDeviceTemperatureData(deviceData);
                                    AddWSTemperatureData(deviceData);
                                    break;
                                case "电池电压":
                                    AddDeviceBatteryVoltageData(deviceData);
                                    break;

                            }

                        }

                        datacount++;
                        SetLabelText(SetMessage(currentIndex, max));
                        SetPbValue(currentIndex);
                        currentIndex++;
                        // SetLabelText(SetMessage(currentIndex, max));
                        //SetPbValue(currentIndex);
                        //currentIndex++;
                    }
                    #endregion

                    pageIndex++;
                }

            }
            checkMax += datacount;
        }
        #endregion

        #region 设备数据多线程
        /// <summary>
        /// 设备数据多线程
        /// </summary>
        /// <param name="data">参数</param>
        public void ThreedDevice(object data)
        {
            List<int> info = data as List<int>;

            ThreedAddDeviceData(info[1], info[2], info[0], info[3]);
        }
        #endregion
        #endregion

        #region 多线程处理振动信号数据

        #region 多线程添加数据
        /// <summary>
        /// 多线程添加数据
        /// </summary>
        /// <param name="startIndex">开始值</param>
        /// <param name="dataCount">数据量</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="threedCount">线程数量</param>
        public void ThreedAddVibsignalData(int dataCount, int pageSize, int threedIndex, int threedCount)
        {
            var db12 = new OiCMSDBContext();
            var db121 = new OiCMSDBContext();
            //当前页数
            int pageIndex = 0;
            int totalPageNum = 0;
            int outCount = 0;

            if (threedIndex < threedCount)
            {
                pageIndex = (threedIndex - 1) * threedDataCount / pageSize + 1;
                //总页数
                totalPageNum = threedDataCount / pageSize;
            }
            else
            {
                pageIndex = (threedIndex - 1) * threedDataCount / pageSize + 1;
                //总页数
                totalPageNum = (dataCount - (threedIndex - 1) * threedDataCount + pageSize - 1) / pageSize;
            }

            int datacount = 0;
            for (int i = 0; i < totalPageNum; i++)
            {
                //每一100条每一页
                var vibSignalDataList = getPageDate<T_VibSingalHisData, int>(c => c, c => c.HisDataID > 0, c => c.HisDataID, pageIndex, pageSize, out outCount);

                if (vibSignalDataList != null && vibSignalDataList.Count() > 0)
                {
                    #region 数据处理
                    foreach (var vibSignalData in vibSignalDataList)
                    {



                        //通过振动信号查找振动信号类型
                        var vibsignal = db12.T_VibSingal.Where(item => item.SingalID == vibSignalData.SingalID).FirstOrDefault();
                        if (vibsignal != null)
                        {
                            //获取类型
                            var common = db121.T_Common.Where(item => item.CID == vibsignal.SingalType).FirstOrDefault();

                            if (common != null)
                            {
                                string typeName = common.CValue;

                                switch (typeName)
                                {
                                    case "加速度":
                                        AddVibSignalDataForAcc(vibSignalData);
                                        break;
                                    case "速度":
                                        AddVibSignalDataForVel(vibSignalData);
                                        break;
                                    case "包络":
                                        AddVibSignalDataForEnvel(vibSignalData);
                                        break;
                                    case "位移":
                                        AddVibSignalDataForDisp(vibSignalData);
                                        break;
                                    case "LQ":
                                        AddVibSignalDataForLQ(vibSignalData);
                                        break;
                                }

                            }
                        }

                        datacount++;
                        SetLabelText(SetMessage(currentIndex, max));
                        SetPbValue(currentIndex);
                        currentIndex++;

                    }
                    #endregion

                    pageIndex++;
                }
            }
            checkMax += datacount;

        }
        #endregion

        #region 振动信号数据多线程
        /// <summary>
        /// 设备数据多线程
        /// </summary>
        /// <param name="data">参数</param>
        public void ThreedVibsignal(object data)
        {
            List<int> info = data as List<int>;

            ThreedAddVibsignalData(info[1], info[2], info[0], info[3]);
        }
        #endregion
        #endregion

    }
    #endregion
}
