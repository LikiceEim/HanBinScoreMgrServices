using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class View_LQHistoryDataMap : EntityTypeConfiguration<View_LQHistoryData>
    {
        public View_LQHistoryDataMap()
        {
            // Primary Key
            this.HasKey(t => new { t.MSiteID, t.DevID, t.LQStat, t.CollectitTime, t.DataType });

            // Properties
            this.Property(t => t.MSiteID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MSiteName)
                .HasMaxLength(50);

            this.Property(t => t.DevID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DevName)
                .HasMaxLength(100);

            this.Property(t => t.LQStat)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DataType)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("View_LQHistoryData");
            this.Property(t => t.MSiteID).HasColumnName("MSiteID");
            this.Property(t => t.MSiteName).HasColumnName("MSiteName");
            this.Property(t => t.DevID).HasColumnName("DevID");
            this.Property(t => t.DevName).HasColumnName("DevName");
            this.Property(t => t.TempValue).HasColumnName("TempValue");
            this.Property(t => t.TempWarnSet).HasColumnName("TempWarnSet");
            this.Property(t => t.TempAlarmSet).HasColumnName("TempAlarmSet");
            this.Property(t => t.TempStat).HasColumnName("TempStat");
            this.Property(t => t.SpeedVirtualValue).HasColumnName("SpeedVirtualValue");
            this.Property(t => t.SpeedVirtualValueWarnSet).HasColumnName("SpeedVirtualValueWarnSet");
            this.Property(t => t.SpeedVirtualValueAlarmSet).HasColumnName("SpeedVirtualValueAlarmSet");
            this.Property(t => t.SpeedVirtualValueStat).HasColumnName("SpeedVirtualValueStat");
            this.Property(t => t.ACCPEAKValue).HasColumnName("ACCPEAKValue");
            this.Property(t => t.ACCPEAKValueWarnSet).HasColumnName("ACCPEAKValueWarnSet");
            this.Property(t => t.ACCPEAKValueAlarmSet).HasColumnName("ACCPEAKValueAlarmSet");
            this.Property(t => t.ACCPEAKValueStat).HasColumnName("ACCPEAKValueStat");
            this.Property(t => t.LQValue).HasColumnName("LQValue");
            this.Property(t => t.LQWarnSet).HasColumnName("LQWarnSet");
            this.Property(t => t.LQAlarmSet).HasColumnName("LQAlarmSet");
            this.Property(t => t.LQStat).HasColumnName("LQStat");
            this.Property(t => t.DisplacementDPEAKValue).HasColumnName("DisplacementDPEAKValue");
            this.Property(t => t.DisplacementDPEAKValueWarnSet).HasColumnName("DisplacementDPEAKValueWarnSet");
            this.Property(t => t.DisplacementDPEAKValueAlarmSet).HasColumnName("DisplacementDPEAKValueAlarmSet");
            this.Property(t => t.DisplacementDPEAKValueStat).HasColumnName("DisplacementDPEAKValueStat");
            this.Property(t => t.EnvelopPEAKValue).HasColumnName("EnvelopPEAKValue");
            this.Property(t => t.EnvelopPEAKValueWarnSet).HasColumnName("EnvelopPEAKValueWarnSet");
            this.Property(t => t.EnvelopPEAKValueAlmSet).HasColumnName("EnvelopPEAKValueAlmSet");
            this.Property(t => t.EnvelopPEAKValueStat).HasColumnName("EnvelopPEAKValueStat");
            this.Property(t => t.CollectitTime).HasColumnName("CollectitTime");
            this.Property(t => t.DataType).HasColumnName("DataType");
        }
    }
}
