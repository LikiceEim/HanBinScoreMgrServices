using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using iCMS.Setup.DBUpgrade.OldEF.Models.Mapping;

namespace iCMS.Setup.DBUpgrade.OldEF.Models
{
    public partial class OiCMSDBContext : DbContext
    {
        static OiCMSDBContext()
        {
            Database.SetInitializer<OiCMSDBContext>(null);
        }

        public OiCMSDBContext()
            : base("Name=OldContext")
        {
        }

        public DbSet<T_AlmRecord> T_AlmRecord { get; set; }
        public DbSet<T_BEARING> T_BEARING { get; set; }
        public DbSet<T_COM> T_COM { get; set; }
        public DbSet<T_Common> T_Common { get; set; }
        public DbSet<T_DevGroup> T_DevGroup { get; set; }
        public DbSet<T_Device> T_Device { get; set; }
        public DbSet<T_Factory> T_Factory { get; set; }
        public DbSet<T_Image> T_Image { get; set; }
        public DbSet<T_MeasureSite> T_MeasureSite { get; set; }
        public DbSet<T_Module> T_Module { get; set; }
        public DbSet<T_MonitorTree> T_MonitorTree { get; set; }
        public DbSet<T_MonitorTreeProperty> T_MonitorTreeProperty { get; set; }
        public DbSet<T_MSiteAlm> T_MSiteAlm { get; set; }
        public DbSet<T_MSiteData> T_MSiteData { get; set; }
        public DbSet<T_Operation> T_Operation { get; set; }
        public DbSet<T_Plant> T_Plant { get; set; }
        public DbSet<T_Role> T_Role { get; set; }
        public DbSet<T_RoleModule> T_RoleModule { get; set; }
        public DbSet<T_SingalAlmSet> T_SingalAlmSet { get; set; }
        public DbSet<T_SysLog> T_SysLog { get; set; }
        public DbSet<T_User> T_User { get; set; }
        public DbSet<T_UserLog> T_UserLog { get; set; }
        public DbSet<T_VibScan> T_VibScan { get; set; }
        public DbSet<T_VibSingal> T_VibSingal { get; set; }
        public DbSet<T_VibSingalHisData> T_VibSingalHisData { get; set; }
        public DbSet<T_VibSingalRTData> T_VibSingalRTData { get; set; }
        public DbSet<T_WG> T_WG { get; set; }
        public DbSet<T_WS> T_WS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new T_AlmRecordMap());
            modelBuilder.Configurations.Add(new T_BEARINGMap());
            modelBuilder.Configurations.Add(new T_COMMap());
            modelBuilder.Configurations.Add(new T_CommonMap());
            modelBuilder.Configurations.Add(new T_DevGroupMap());
            modelBuilder.Configurations.Add(new T_DeviceMap());
            modelBuilder.Configurations.Add(new T_FactoryMap());
            modelBuilder.Configurations.Add(new T_ImageMap());
            modelBuilder.Configurations.Add(new T_MeasureSiteMap());
            modelBuilder.Configurations.Add(new T_ModuleMap());

        

            modelBuilder.Configurations.Add(new T_MonitorTreeMap());
            //不映射到数据库中
           // modelBuilder.Entity<T_MonitorTree>().Ignore(p => p.Status);

            modelBuilder.Configurations.Add(new T_MonitorTreePropertyMap());
            modelBuilder.Configurations.Add(new T_MSiteAlmMap());
            modelBuilder.Configurations.Add(new T_MSiteDataMap());
            modelBuilder.Configurations.Add(new T_OperationMap());
            modelBuilder.Configurations.Add(new T_PlantMap());
            modelBuilder.Configurations.Add(new T_RoleMap());
            modelBuilder.Configurations.Add(new T_RoleModuleMap());
            modelBuilder.Configurations.Add(new T_SingalAlmSetMap());
            modelBuilder.Configurations.Add(new T_SysLogMap());
            modelBuilder.Configurations.Add(new T_UserMap());
            modelBuilder.Configurations.Add(new T_UserLogMap());
            modelBuilder.Configurations.Add(new T_VibScanMap());
            modelBuilder.Configurations.Add(new T_VibSingalMap());
            modelBuilder.Configurations.Add(new T_VibSingalHisDataMap());
            modelBuilder.Configurations.Add(new T_VibSingalRTDataMap());
            modelBuilder.Configurations.Add(new T_WGMap());
            modelBuilder.Configurations.Add(new T_WSMap());
        }
    }
}
