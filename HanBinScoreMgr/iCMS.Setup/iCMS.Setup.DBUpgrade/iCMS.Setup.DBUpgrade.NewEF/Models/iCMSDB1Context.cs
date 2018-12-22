using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using iCMS.Setup.DBUpgrade.NewEF.Models.Mapping;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class iCMSDB1Context : DbContext
    {
        static iCMSDB1Context()
        {
            Database.SetInitializer<iCMSDB1Context>(null);
        }

        public iCMSDB1Context()
            : base("Name=NewContext")
        {
        }

        public DbSet<T_DATA_REALTIME_COLLECT_INFO> T_DATA_REALTIME_COLLECT_INFO { get; set; }
        public DbSet<T_DATA_TEMPE_DEVICE_MSITEDATA_1> T_DATA_TEMPE_DEVICE_MSITEDATA_1 { get; set; }
        public DbSet<T_DATA_TEMPE_DEVICE_MSITEDATA_2> T_DATA_TEMPE_DEVICE_MSITEDATA_2 { get; set; }
        public DbSet<T_DATA_TEMPE_DEVICE_MSITEDATA_3> T_DATA_TEMPE_DEVICE_MSITEDATA_3 { get; set; }
        public DbSet<T_DATA_TEMPE_DEVICE_MSITEDATA_4> T_DATA_TEMPE_DEVICE_MSITEDATA_4 { get; set; }
        public DbSet<T_DATA_TEMPE_WS_MSITEDATA_1> T_DATA_TEMPE_WS_MSITEDATA_1 { get; set; }
        public DbSet<T_DATA_TEMPE_WS_MSITEDATA_2> T_DATA_TEMPE_WS_MSITEDATA_2 { get; set; }
        public DbSet<T_DATA_TEMPE_WS_MSITEDATA_3> T_DATA_TEMPE_WS_MSITEDATA_3 { get; set; }
        public DbSet<T_DATA_TEMPE_WS_MSITEDATA_4> T_DATA_TEMPE_WS_MSITEDATA_4 { get; set; }
        public DbSet<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACC> T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACC { get; set; }
        public DbSet<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP> T_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP { get; set; }
        public DbSet<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL> T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVL { get; set; }
        public DbSet<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQ> T_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQ { get; set; }
        public DbSet<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_VEL> T_DATA_VIBRATING_SIGNAL_CHAR_HIS_VEL { get; set; }
        public DbSet<T_DATA_VIBSINGAL_RT> T_DATA_VIBSINGAL_RT { get; set; }
        public DbSet<T_DATA_VOLTAGE_WS_MSITEDATA_1> T_DATA_VOLTAGE_WS_MSITEDATA_1 { get; set; }
        public DbSet<T_DATA_VOLTAGE_WS_MSITEDATA_2> T_DATA_VOLTAGE_WS_MSITEDATA_2 { get; set; }
        public DbSet<T_DATA_VOLTAGE_WS_MSITEDATA_3> T_DATA_VOLTAGE_WS_MSITEDATA_3 { get; set; }
        public DbSet<T_DATA_VOLTAGE_WS_MSITEDATA_4> T_DATA_VOLTAGE_WS_MSITEDATA_4 { get; set; }
        public DbSet<T_DICT_CONFIG> T_DICT_CONFIG { get; set; }
        public DbSet<T_DICT_CONNECT_STATUS_TYPE> T_DICT_CONNECT_STATUS_TYPE { get; set; }
        public DbSet<T_DICT_DEVICE_TYPE> T_DICT_DEVICE_TYPE { get; set; }
        public DbSet<T_DICT_EIGEN_VALUE_TYPE> T_DICT_EIGEN_VALUE_TYPE { get; set; }
        public DbSet<T_DICT_MEASURE_SITE_MONITOR_TYPE> T_DICT_MEASURE_SITE_MONITOR_TYPE { get; set; }
        public DbSet<T_DICT_MEASURE_SITE_TYPE> T_DICT_MEASURE_SITE_TYPE { get; set; }
        public DbSet<T_DICT_MONITORTREE_TYPE> T_DICT_MONITORTREE_TYPE { get; set; }
        public DbSet<T_DICT_SENSOR_TYPE> T_DICT_SENSOR_TYPE { get; set; }
        public DbSet<T_DICT_VIBRATING_SIGNAL_TYPE> T_DICT_VIBRATING_SIGNAL_TYPE { get; set; }
        public DbSet<T_DICT_WAVE_LENGTH_VALUE> T_DICT_WAVE_LENGTH_VALUE { get; set; }
        public DbSet<T_DICT_WAVE_LOWERLIMIT_VALUE> T_DICT_WAVE_LOWERLIMIT_VALUE { get; set; }
        public DbSet<T_DICT_WAVE_UPPERLIMIT_VALUE> T_DICT_WAVE_UPPERLIMIT_VALUE { get; set; }
        public DbSet<T_DICT_WIRELESS_GATEWAY_TYPE> T_DICT_WIRELESS_GATEWAY_TYPE { get; set; }
        public DbSet<T_SYS_BEARING> T_SYS_BEARING { get; set; }
        public DbSet<T_SYS_DEV_ALMRECORD> T_SYS_DEV_ALMRECORD { get; set; }
        public DbSet<T_SYS_DEVICE> T_SYS_DEVICE { get; set; }
        public DbSet<T_SYS_FACTORY> T_SYS_FACTORY { get; set; }
        public DbSet<T_SYS_IMAGE> T_SYS_IMAGE { get; set; }
        public DbSet<T_SYS_MEASURESITE> T_SYS_MEASURESITE { get; set; }
        public DbSet<T_SYS_MODULE> T_SYS_MODULE { get; set; }
        public DbSet<T_SYS_MONITOR_TREE> T_SYS_MONITOR_TREE { get; set; }
        public DbSet<T_SYS_MONITOR_TREE_PROPERTY> T_SYS_MONITOR_TREE_PROPERTY { get; set; }
        public DbSet<T_SYS_OPERATION> T_SYS_OPERATION { get; set; }
        public DbSet<T_SYS_ROLE> T_SYS_ROLE { get; set; }
        public DbSet<T_SYS_ROLEMODULE> T_SYS_ROLEMODULE { get; set; }
        public DbSet<T_SYS_SYSLOG> T_SYS_SYSLOG { get; set; }
        public DbSet<T_SYS_TEMPE_DEVICE_SET_MSITEALM> T_SYS_TEMPE_DEVICE_SET_MSITEALM { get; set; }
        public DbSet<T_SYS_TEMPE_WS_SET_MSITEALM> T_SYS_TEMPE_WS_SET_MSITEALM { get; set; }
        public DbSet<T_SYS_USER> T_SYS_USER { get; set; }
        public DbSet<T_SYS_USER_RALATION_DEVICE> T_SYS_USER_RALATION_DEVICE { get; set; }
        public DbSet<T_SYS_USERLOG> T_SYS_USERLOG { get; set; }
        public DbSet<T_SYS_VIBRATING_SET_SIGNALALM> T_SYS_VIBRATING_SET_SIGNALALM { get; set; }
        public DbSet<T_SYS_VIBSINGAL> T_SYS_VIBSINGAL { get; set; }
        public DbSet<T_SYS_VOLTAGE_SET_MSITEALM> T_SYS_VOLTAGE_SET_MSITEALM { get; set; }
        public DbSet<T_SYS_WG> T_SYS_WG { get; set; }
        public DbSet<T_SYS_WS> T_SYS_WS { get; set; }
        public DbSet<T_SYS_WSN_ALMRECORD> T_SYS_WSN_ALMRECORD { get; set; }
        public DbSet<View_ACCHistoryData> View_ACCHistoryData { get; set; }
        public DbSet<View_DevHistoryData> View_DevHistoryData { get; set; }
        public DbSet<View_DeviceTempHistortyData> View_DeviceTempHistortyData { get; set; }
        public DbSet<View_DISPHistoryData> View_DISPHistoryData { get; set; }
        public DbSet<View_ENVLHistoryData> View_ENVLHistoryData { get; set; }
        public DbSet<View_Get_WS_Status> View_Get_WS_Status { get; set; }
        public DbSet<View_Get_WS_Status_ForTrigger> View_Get_WS_Status_ForTrigger { get; set; }
        public DbSet<View_LQHistoryData> View_LQHistoryData { get; set; }
        public DbSet<View_VELHistoryData> View_VELHistoryData { get; set; }
        public DbSet<ViewGetMSInfo> ViewGetMSInfoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new T_DATA_REALTIME_COLLECT_INFOMap());
            modelBuilder.Configurations.Add(new T_DATA_TEMPE_DEVICE_MSITEDATA_1Map());
            modelBuilder.Configurations.Add(new T_DATA_TEMPE_DEVICE_MSITEDATA_2Map());
            modelBuilder.Configurations.Add(new T_DATA_TEMPE_DEVICE_MSITEDATA_3Map());
            modelBuilder.Configurations.Add(new T_DATA_TEMPE_DEVICE_MSITEDATA_4Map());
            modelBuilder.Configurations.Add(new T_DATA_TEMPE_WS_MSITEDATA_1Map());
            modelBuilder.Configurations.Add(new T_DATA_TEMPE_WS_MSITEDATA_2Map());
            modelBuilder.Configurations.Add(new T_DATA_TEMPE_WS_MSITEDATA_3Map());
            modelBuilder.Configurations.Add(new T_DATA_TEMPE_WS_MSITEDATA_4Map());
            modelBuilder.Configurations.Add(new T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ACCMap());
            modelBuilder.Configurations.Add(new T_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISPMap());
            modelBuilder.Configurations.Add(new T_DATA_VIBRATING_SIGNAL_CHAR_HIS_ENVLMap());
            modelBuilder.Configurations.Add(new T_DATA_VIBRATING_SIGNAL_CHAR_HIS_LQMap());
            modelBuilder.Configurations.Add(new T_DATA_VIBRATING_SIGNAL_CHAR_HIS_VELMap());
            modelBuilder.Configurations.Add(new T_DATA_VIBSINGAL_RTMap());
            modelBuilder.Configurations.Add(new T_DATA_VOLTAGE_WS_MSITEDATA_1Map());
            modelBuilder.Configurations.Add(new T_DATA_VOLTAGE_WS_MSITEDATA_2Map());
            modelBuilder.Configurations.Add(new T_DATA_VOLTAGE_WS_MSITEDATA_3Map());
            modelBuilder.Configurations.Add(new T_DATA_VOLTAGE_WS_MSITEDATA_4Map());
            modelBuilder.Configurations.Add(new T_DICT_CONFIGMap());
            modelBuilder.Configurations.Add(new T_DICT_CONNECT_STATUS_TYPEMap());
            modelBuilder.Configurations.Add(new T_DICT_DEVICE_TYPEMap());
            modelBuilder.Configurations.Add(new T_DICT_EIGEN_VALUE_TYPEMap());
            modelBuilder.Configurations.Add(new T_DICT_MEASURE_SITE_MONITOR_TYPEMap());
            modelBuilder.Configurations.Add(new T_DICT_MEASURE_SITE_TYPEMap());
            modelBuilder.Configurations.Add(new T_DICT_MONITORTREE_TYPEMap());
            modelBuilder.Configurations.Add(new T_DICT_SENSOR_TYPEMap());
            modelBuilder.Configurations.Add(new T_DICT_VIBRATING_SIGNAL_TYPEMap());
            modelBuilder.Configurations.Add(new T_DICT_WAVE_LENGTH_VALUEMap());
            modelBuilder.Configurations.Add(new T_DICT_WAVE_LOWERLIMIT_VALUEMap());
            modelBuilder.Configurations.Add(new T_DICT_WAVE_UPPERLIMIT_VALUEMap());
            modelBuilder.Configurations.Add(new T_DICT_WIRELESS_GATEWAY_TYPEMap());
            modelBuilder.Configurations.Add(new T_SYS_BEARINGMap());
            modelBuilder.Configurations.Add(new T_SYS_DEV_ALMRECORDMap());
            modelBuilder.Configurations.Add(new T_SYS_DEVICEMap());
            modelBuilder.Configurations.Add(new T_SYS_FACTORYMap());
            modelBuilder.Configurations.Add(new T_SYS_IMAGEMap());
            modelBuilder.Configurations.Add(new T_SYS_MEASURESITEMap());
            modelBuilder.Configurations.Add(new T_SYS_MODULEMap());
            modelBuilder.Configurations.Add(new T_SYS_MONITOR_TREEMap());
            modelBuilder.Configurations.Add(new T_SYS_MONITOR_TREE_PROPERTYMap());
            modelBuilder.Configurations.Add(new T_SYS_OPERATIONMap());
            modelBuilder.Configurations.Add(new T_SYS_ROLEMap());
            modelBuilder.Configurations.Add(new T_SYS_ROLEMODULEMap());
            modelBuilder.Configurations.Add(new T_SYS_SYSLOGMap());
            modelBuilder.Configurations.Add(new T_SYS_TEMPE_DEVICE_SET_MSITEALMMap());
            modelBuilder.Configurations.Add(new T_SYS_TEMPE_WS_SET_MSITEALMMap());
            modelBuilder.Configurations.Add(new T_SYS_USERMap());
            modelBuilder.Configurations.Add(new T_SYS_USER_RALATION_DEVICEMap());
            modelBuilder.Configurations.Add(new T_SYS_USERLOGMap());
            modelBuilder.Configurations.Add(new T_SYS_VIBRATING_SET_SIGNALALMMap());
            modelBuilder.Configurations.Add(new T_SYS_VIBSINGALMap());
            modelBuilder.Configurations.Add(new T_SYS_VOLTAGE_SET_MSITEALMMap());
            modelBuilder.Configurations.Add(new T_SYS_WGMap());
            modelBuilder.Configurations.Add(new T_SYS_WSMap());
            modelBuilder.Configurations.Add(new T_SYS_WSN_ALMRECORDMap());
            modelBuilder.Configurations.Add(new View_ACCHistoryDataMap());
            modelBuilder.Configurations.Add(new View_DevHistoryDataMap());
            modelBuilder.Configurations.Add(new View_DeviceTempHistortyDataMap());
            modelBuilder.Configurations.Add(new View_DISPHistoryDataMap());
            modelBuilder.Configurations.Add(new View_ENVLHistoryDataMap());
            modelBuilder.Configurations.Add(new View_Get_WS_StatusMap());
            modelBuilder.Configurations.Add(new View_Get_WS_Status_ForTriggerMap());
            modelBuilder.Configurations.Add(new View_LQHistoryDataMap());
            modelBuilder.Configurations.Add(new View_VELHistoryDataMap());
            modelBuilder.Configurations.Add(new ViewGetMSInfoMap());
        }
    }
}
