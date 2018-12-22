using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.OldEF.Models.Mapping
{
    public class T_MeasureSiteMap : EntityTypeConfiguration<T_MeasureSite>
    {
        public T_MeasureSiteMap()
        {
            // Primary Key
            this.HasKey(t => t.MSiteID);

            // Properties
            this.Property(t => t.WaveTime)
                .HasMaxLength(50);

            this.Property(t => t.FlagTime)
                .HasMaxLength(50);

            this.Property(t => t.Remark)
                .HasMaxLength(100);

            this.Property(t => t.Position)
                .HasMaxLength(100);

            this.Property(t => t.TemperatureTime)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("T_MeasureSite");
            this.Property(t => t.MSiteID).HasColumnName("MSiteID");
            this.Property(t => t.MSiteName).HasColumnName("MSiteName");
            this.Property(t => t.DevID).HasColumnName("DevID");
            this.Property(t => t.VibScanID).HasColumnName("VibScanID");
            this.Property(t => t.WSID).HasColumnName("WSID");
            this.Property(t => t.ChannelID).HasColumnName("ChannelID");
            this.Property(t => t.MeasureSiteType).HasColumnName("MeasureSiteType");
            this.Property(t => t.SensorCosA).HasColumnName("SensorCosA");
            this.Property(t => t.SensorCosB).HasColumnName("SensorCosB");
            this.Property(t => t.MSiteStatus).HasColumnName("MSiteStatus");
            this.Property(t => t.MSiteSDate).HasColumnName("MSiteSDate");
            this.Property(t => t.WaveTime).HasColumnName("WaveTime");
            this.Property(t => t.FlagTime).HasColumnName("FlagTime");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.SerialNo).HasColumnName("SerialNo");
            this.Property(t => t.BearingID).HasColumnName("BearingID");
            this.Property(t => t.TemperatureTime).HasColumnName("TemperatureTime");
        }
    }
}
