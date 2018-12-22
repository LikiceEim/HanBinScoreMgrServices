using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.OldEF.Models.Mapping
{
    public class T_AlmRecordMap : EntityTypeConfiguration<T_AlmRecord>
    {
        public T_AlmRecordMap()
        {
            // Primary Key
            this.HasKey(t => t.AlmRecordID);

            // Properties
            this.Property(t => t.Content)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("T_AlmRecord");
            this.Property(t => t.AlmRecordID).HasColumnName("AlmRecordID");
            this.Property(t => t.DevID).HasColumnName("DevID");
            this.Property(t => t.MSiteID).HasColumnName("MSiteID");
            this.Property(t => t.SingalID).HasColumnName("SingalID");
            this.Property(t => t.MSAlmID).HasColumnName("MSAlmID");
            this.Property(t => t.AlmStatus).HasColumnName("AlmStatus");
            this.Property(t => t.BDate).HasColumnName("BDate");
            this.Property(t => t.EDate).HasColumnName("EDate");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
            this.Property(t => t.LatestStartTime).HasColumnName("LatestStartTime");
            this.Property(t => t.Content).HasColumnName("Content");
        }
    }
}
