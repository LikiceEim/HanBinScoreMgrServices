using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class ViewGetMSInfoMap : EntityTypeConfiguration<ViewGetMSInfo>
    {
        public ViewGetMSInfoMap()
        {
            // Primary Key
            this.HasKey(t => new { t.WSID, t.MACADDR, t.LinkStatus, t.MSiteID, t.MSiteName, t.DevID, t.ChannelID, t.MeasureSiteType, t.SensorCosA, t.SensorCosB, t.MSiteStatus, t.MSiteSDate, t.SerialNo, t.BearingType, t.LubricatingForm, t.AddDate, t.id, t.OperatorKey, t.MSID, t.OperationType, t.Bdate, t.OperationResult });

            // Properties
            this.Property(t => t.WSID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MACADDR)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LinkStatus)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MSiteID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MSiteName)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.DevID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ChannelID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MeasureSiteType)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MSiteStatus)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.WaveTime)
                .HasMaxLength(50);

            this.Property(t => t.FlagTime)
                .HasMaxLength(50);

            this.Property(t => t.TemperatureTime)
                .HasMaxLength(50);

            this.Property(t => t.Remark)
                .HasMaxLength(100);

            this.Property(t => t.Position)
                .HasMaxLength(100);

            this.Property(t => t.SerialNo)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BearingType)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.BearingModel)
                .HasMaxLength(100);

            this.Property(t => t.LubricatingForm)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.OperatorKey)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.MSID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.OperationType)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.OperationResult)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ViewGetMSInfo");
            this.Property(t => t.WSID).HasColumnName("WSID");
            this.Property(t => t.MACADDR).HasColumnName("MACADDR");
            this.Property(t => t.LinkStatus).HasColumnName("LinkStatus");
            this.Property(t => t.MSiteID).HasColumnName("MSiteID");
            this.Property(t => t.MSiteName).HasColumnName("MSiteName");
            this.Property(t => t.DevID).HasColumnName("DevID");
            this.Property(t => t.VibScanID).HasColumnName("VibScanID");
            this.Property(t => t.ChannelID).HasColumnName("ChannelID");
            this.Property(t => t.MeasureSiteType).HasColumnName("MeasureSiteType");
            this.Property(t => t.SensorCosA).HasColumnName("SensorCosA");
            this.Property(t => t.SensorCosB).HasColumnName("SensorCosB");
            this.Property(t => t.MSiteStatus).HasColumnName("MSiteStatus");
            this.Property(t => t.MSiteSDate).HasColumnName("MSiteSDate");
            this.Property(t => t.WaveTime).HasColumnName("WaveTime");
            this.Property(t => t.FlagTime).HasColumnName("FlagTime");
            this.Property(t => t.TemperatureTime).HasColumnName("TemperatureTime");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.SerialNo).HasColumnName("SerialNo");
            this.Property(t => t.BearingID).HasColumnName("BearingID");
            this.Property(t => t.BearingType).HasColumnName("BearingType");
            this.Property(t => t.BearingModel).HasColumnName("BearingModel");
            this.Property(t => t.LubricatingForm).HasColumnName("LubricatingForm");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.OperatorKey).HasColumnName("OperatorKey");
            this.Property(t => t.MSID).HasColumnName("MSID");
            this.Property(t => t.OperationType).HasColumnName("OperationType");
            this.Property(t => t.Bdate).HasColumnName("Bdate");
            this.Property(t => t.EDate).HasColumnName("EDate");
            this.Property(t => t.OperationResult).HasColumnName("OperationResult");
            this.Property(t => t.OperationReson).HasColumnName("OperationReson");
            this.Property(t => t.DAQStyle).HasColumnName("DAQStyle");
        }
    }
}
