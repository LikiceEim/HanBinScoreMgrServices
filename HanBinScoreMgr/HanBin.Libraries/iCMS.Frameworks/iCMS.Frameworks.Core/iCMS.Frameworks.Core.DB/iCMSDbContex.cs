using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using iCMS.Common.Component.Tool;
using iCMS.Frameworks.Core.DB.Models;

namespace iCMS.Frameworks.Core.DB
{
    public class iCMSDbContext : DbContext, IDisposable
    {
        public iCMSDbContext()
            : base(EcanSecurity.Decode(Utilitys.GetAppConfig("iCMS"), EcanSecurity.GetKey(Utilitys.GetAppConfig("DBSecret"))))
        {
            Database.SetInitializer<iCMSDbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }

        public DbSet<Bearing> Bearing { get; set; }
        public DbSet<ConnectStatusType> ConnectStatusType { get; set; }
        public DbSet<DevAlmRecord> DevAlmRecord { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<DeviceType> DeviceType { get; set; }
        public DbSet<EigenValueType> EigenValueType { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<MeasureSite> Measuresite { get; set; }
        public DbSet<MeasureSiteMonitorType> MeasureSiteMonitorType { get; set; }
        public DbSet<MeasureSiteType> MeasureSiteType { get; set; }

        public DbSet<Module> Module { get; set; }
        public DbSet<MonitorTree> MonitorTree { get; set; }
        public DbSet<MonitorTreeProperty> MonitorTreeProperty { get; set; }
        public DbSet<MonitorTreeType> MonitorTreeType { get; set; }
        public DbSet<Operation> Operation { get; set; }
        public DbSet<RealTimeCollectInfo> RealTimeCollectInfo { get; set; }

        public DbSet<Role> Role { get; set; }
        public DbSet<RoleModule> RoleModule { get; set; }
        public DbSet<SensorType> SensorType { get; set; }
        public DbSet<SysLog> SysLogs { get; set; }

        //public DbSet<TempeDeviceMsitedata> TempeDeviceMsitedata { get; set; }
        public DbSet<TempeDeviceMsitedata_1> TempeDeviceMsitedata_1 { get; set; }
        public DbSet<TempeDeviceMsitedata_2> TempeDeviceMsitedata_2 { get; set; }
        public DbSet<TempeDeviceMsitedata_3> TempeDeviceMsitedata_3 { get; set; }
        public DbSet<TempeDeviceMsitedata_4> TempeDeviceMsitedata_4 { get; set; }

        public DbSet<TempeDeviceSetMSiteAlm> TempeDeviceSetMSiteAlm { get; set; }
        //public DbSet<TempeWSMsitedata> TempeWSMsitedata { get; set; }
        public DbSet<TempeWSMsitedata_1> TempeWSMsitedata_1 { get; set; }
        public DbSet<TempeWSMsitedata_2> TempeWSMsitedata_2 { get; set; }
        public DbSet<TempeWSMsitedata_3> TempeWSMsitedata_3 { get; set; }
        public DbSet<TempeWSMsitedata_4> TempeWSMsitedata_4 { get; set; }
        public DbSet<TempeWSSetMSiteAlm> TempeWSSetMsitealm { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }
        public DbSet<UserRalationDevice> UserRalationDevice { get; set; }

        public DbSet<VibratingSingalCharHisAcc> VibratingSingalCharHisAcc { get; set; }
        public DbSet<VibratingSingalCharHisDisp> VibratingSingalCharHisDisp { get; set; }
        public DbSet<VibratingSingalCharHisEnvl> VibratingSingalCharHisEnvl { get; set; }
        public DbSet<VibratingSingalCharHisLQ> VibratingSingalCharHisLQ { get; set; }
        public DbSet<VibratingSingalCharHisVel> VibratingSingalCharHisVel { get; set; }

        public DbSet<VibratingSingalType> VibratingSingalType { get; set; }
        public DbSet<VibSingal> VibSingal { get; set; }
        public DbSet<VibSingalRT> VibSingalRT { get; set; }
        public DbSet<VoltageSetMSiteAlm> VoltageSetMsitealm { get; set; }
        //public DbSet<VoltageWSMsitedata> VoltageWSMsitedata { get; set; }
        public DbSet<VoltageWSMSiteData_1> VoltageWSMsitedata_1 { get; set; }
        public DbSet<VoltageWSMSiteData_2> VoltageWSMsitedata_2 { get; set; }
        public DbSet<VoltageWSMSiteData_3> VoltageWSMsitedata_3 { get; set; }
        public DbSet<VoltageWSMSiteData_4> VoltageWSMsitedata_4 { get; set; }

        public DbSet<WaveLengthValues> WaveLengthValues { get; set; }
        public DbSet<WaveLowerLimitValues> WaveLowerLimitValues { get; set; }
        public DbSet<WaveUpperLimitValues> WaveUpperLimitValues { get; set; }
        public DbSet<WirelessGatewayType> WirelessGatewayType { get; set; }
        public DbSet<WSnAlmRecord> WsnAlmrecord { get; set; }

        public DbSet<WS> WS { get; set; }
        public DbSet<Gateway> WG { get; set; }
        public DbSet<SignalAlmSet> SignalAlmSet { get; set; }

        public DbSet<Factory> Factories { get; set; }

        public DbSet<Config> Config { get; set; }
    }
}