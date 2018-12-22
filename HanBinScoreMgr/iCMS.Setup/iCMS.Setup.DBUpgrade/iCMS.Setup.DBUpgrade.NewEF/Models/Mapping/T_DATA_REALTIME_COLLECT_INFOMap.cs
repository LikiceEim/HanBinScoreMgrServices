using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class T_DATA_REALTIME_COLLECT_INFOMap : EntityTypeConfiguration<T_DATA_REALTIME_COLLECT_INFO>
    {
        public T_DATA_REALTIME_COLLECT_INFOMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.MSName)
                .HasMaxLength(50);

            this.Property(t => t.MSDesInfo)
                .HasMaxLength(200);

            this.Property(t => t.MSSpeedUnit)
                .HasMaxLength(50);

            this.Property(t => t.MSACCUnit)
                .HasMaxLength(50);

            this.Property(t => t.MSDispUnit)
                .HasMaxLength(50);

            this.Property(t => t.MSEnvelopingUnit)
                .HasMaxLength(50);

            this.Property(t => t.MSLQUnit)
                .HasMaxLength(50);

            this.Property(t => t.MSDevTemperatureUnit)
                .HasMaxLength(50);

            this.Property(t => t.MSWSTemperatureUnit)
                .HasMaxLength(50);

            this.Property(t => t.MSWSBatteryVolatageUnit)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("T_DATA_REALTIME_COLLECT_INFO");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.DevID).HasColumnName("DevID");
            this.Property(t => t.MSID).HasColumnName("MSID");
            this.Property(t => t.MSName).HasColumnName("MSName");
            this.Property(t => t.MSStatus).HasColumnName("MSStatus");
            this.Property(t => t.MSDesInfo).HasColumnName("MSDesInfo");
            this.Property(t => t.MSDataStatus).HasColumnName("MSDataStatus");
            this.Property(t => t.MSSpeedSingalID).HasColumnName("MSSpeedSingalID");
            this.Property(t => t.MSSpeedUnit).HasColumnName("MSSpeedUnit");
            this.Property(t => t.MSSpeedVirtualValue).HasColumnName("MSSpeedVirtualValue");
            this.Property(t => t.MSSpeedPeakValue).HasColumnName("MSSpeedPeakValue");
            this.Property(t => t.MSSpeedPeakPeakValue).HasColumnName("MSSpeedPeakPeakValue");
            this.Property(t => t.MSSpeedVirtualStatus).HasColumnName("MSSpeedVirtualStatus");
            this.Property(t => t.MSSpeedPeakStatus).HasColumnName("MSSpeedPeakStatus");
            this.Property(t => t.MSSpeedPeakPeakStatus).HasColumnName("MSSpeedPeakPeakStatus");
            this.Property(t => t.MSSpeedVirtualTime).HasColumnName("MSSpeedVirtualTime");
            this.Property(t => t.MSSpeedPeakTime).HasColumnName("MSSpeedPeakTime");
            this.Property(t => t.MSSpeedPeakPeakTime).HasColumnName("MSSpeedPeakPeakTime");
            this.Property(t => t.MSACCSingalID).HasColumnName("MSACCSingalID");
            this.Property(t => t.MSACCUnit).HasColumnName("MSACCUnit");
            this.Property(t => t.MSACCVirtualValue).HasColumnName("MSACCVirtualValue");
            this.Property(t => t.MSACCPeakValue).HasColumnName("MSACCPeakValue");
            this.Property(t => t.MSACCPeakPeakValue).HasColumnName("MSACCPeakPeakValue");
            this.Property(t => t.MSACCVirtualStatus).HasColumnName("MSACCVirtualStatus");
            this.Property(t => t.MSACCPeakStatus).HasColumnName("MSACCPeakStatus");
            this.Property(t => t.MSACCPeakPeakStatus).HasColumnName("MSACCPeakPeakStatus");
            this.Property(t => t.MSACCVirtualTime).HasColumnName("MSACCVirtualTime");
            this.Property(t => t.MSACCPeakTime).HasColumnName("MSACCPeakTime");
            this.Property(t => t.MSACCPeakPeakTime).HasColumnName("MSACCPeakPeakTime");
            this.Property(t => t.MSDispSingalID).HasColumnName("MSDispSingalID");
            this.Property(t => t.MSDispUnit).HasColumnName("MSDispUnit");
            this.Property(t => t.MSDispVirtualValue).HasColumnName("MSDispVirtualValue");
            this.Property(t => t.MSDispPeakValue).HasColumnName("MSDispPeakValue");
            this.Property(t => t.MSDispPeakPeakValue).HasColumnName("MSDispPeakPeakValue");
            this.Property(t => t.MSDispVirtualStatus).HasColumnName("MSDispVirtualStatus");
            this.Property(t => t.MSDispPeakStatus).HasColumnName("MSDispPeakStatus");
            this.Property(t => t.MSDispPeakPeakStatus).HasColumnName("MSDispPeakPeakStatus");
            this.Property(t => t.MSDispVirtualTime).HasColumnName("MSDispVirtualTime");
            this.Property(t => t.MSDispPeakTime).HasColumnName("MSDispPeakTime");
            this.Property(t => t.MSDispPeakPeakTime).HasColumnName("MSDispPeakPeakTime");
            this.Property(t => t.MSEnvelSingalID).HasColumnName("MSEnvelSingalID");
            this.Property(t => t.MSEnvelopingPEAKValue).HasColumnName("MSEnvelopingPEAKValue");
            this.Property(t => t.MSEnvelopingCarpetValue).HasColumnName("MSEnvelopingCarpetValue");
            this.Property(t => t.MSEnvelopingUnit).HasColumnName("MSEnvelopingUnit");
            this.Property(t => t.MSEnvelopingPEAKStatus).HasColumnName("MSEnvelopingPEAKStatus");
            this.Property(t => t.MSEnvelopingCarpetStatus).HasColumnName("MSEnvelopingCarpetStatus");
            this.Property(t => t.MSEnvelopingPEAKTime).HasColumnName("MSEnvelopingPEAKTime");
            this.Property(t => t.MSEnvelopingCarpetTime).HasColumnName("MSEnvelopingCarpetTime");
            this.Property(t => t.MSLQSingalID).HasColumnName("MSLQSingalID");
            this.Property(t => t.MSLQValue).HasColumnName("MSLQValue");
            this.Property(t => t.MSLQStatus).HasColumnName("MSLQStatus");
            this.Property(t => t.MSLQUnit).HasColumnName("MSLQUnit");
            this.Property(t => t.MSLQTime).HasColumnName("MSLQTime");
            this.Property(t => t.MSDevTemperatureStatus).HasColumnName("MSDevTemperatureStatus");
            this.Property(t => t.MSDevTemperatureValue).HasColumnName("MSDevTemperatureValue");
            this.Property(t => t.MSDevTemperatureUnit).HasColumnName("MSDevTemperatureUnit");
            this.Property(t => t.MSDevTemperatureTime).HasColumnName("MSDevTemperatureTime");
            this.Property(t => t.MSWSTemperatureStatus).HasColumnName("MSWSTemperatureStatus");
            this.Property(t => t.MSWSTemperatureValue).HasColumnName("MSWSTemperatureValue");
            this.Property(t => t.MSWSTemperatureUnit).HasColumnName("MSWSTemperatureUnit");
            this.Property(t => t.MSWSTemperatureTime).HasColumnName("MSWSTemperatureTime");
            this.Property(t => t.MSWSBatteryVolatageValue).HasColumnName("MSWSBatteryVolatageValue");
            this.Property(t => t.MSWSBatteryVolatageUnit).HasColumnName("MSWSBatteryVolatageUnit");
            this.Property(t => t.MSWSBatteryVolatageStatus).HasColumnName("MSWSBatteryVolatageStatus");
            this.Property(t => t.MSWSBatteryVolatageTime).HasColumnName("MSWSBatteryVolatageTime");
            this.Property(t => t.MSWGLinkStatus).HasColumnName("MSWGLinkStatus");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
