using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class T_SYS_WSN_ALMRECORDMap : EntityTypeConfiguration<T_SYS_WSN_ALMRECORD>
    {
        public T_SYS_WSN_ALMRECORDMap()
        {
            // Primary Key
            this.HasKey(t => t.AlmRecordID);

            // Properties
            this.Property(t => t.DevName)
                .HasMaxLength(100);

            this.Property(t => t.DevNO)
                .HasMaxLength(100);

            this.Property(t => t.MSiteName)
                .HasMaxLength(100);

            this.Property(t => t.WGName)
                .HasMaxLength(100);

            this.Property(t => t.WSName)
                .HasMaxLength(100);

            this.Property(t => t.MonitorTreeID)
                .HasMaxLength(100);

            this.Property(t => t.Content)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("T_SYS_WSN_ALMRECORD");
            this.Property(t => t.AlmRecordID).HasColumnName("AlmRecordID");
            this.Property(t => t.DevID).HasColumnName("DevID");
            this.Property(t => t.DevName).HasColumnName("DevName");
            this.Property(t => t.DevNO).HasColumnName("DevNO");
            this.Property(t => t.MSiteID).HasColumnName("MSiteID");
            this.Property(t => t.MSiteName).HasColumnName("MSiteName");
            this.Property(t => t.WGID).HasColumnName("WGID");
            this.Property(t => t.WGName).HasColumnName("WGName");
            this.Property(t => t.WSID).HasColumnName("WSID");
            this.Property(t => t.WSName).HasColumnName("WSName");
            this.Property(t => t.MonitorTreeID).HasColumnName("MonitorTreeID");
            this.Property(t => t.MSAlmID).HasColumnName("MSAlmID");
            this.Property(t => t.AlmStatus).HasColumnName("AlmStatus");
            this.Property(t => t.SamplingValue).HasColumnName("SamplingValue");
            this.Property(t => t.WarningValue).HasColumnName("WarningValue");
            this.Property(t => t.DangerValue).HasColumnName("DangerValue");
            this.Property(t => t.BDate).HasColumnName("BDate");
            this.Property(t => t.EDate).HasColumnName("EDate");
            this.Property(t => t.LatestStartTime).HasColumnName("LatestStartTime");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
