using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.OldEF.Models.Mapping
{
    public class T_VibScanMap : EntityTypeConfiguration<T_VibScan>
    {
        public T_VibScanMap()
        {
            // Primary Key
            this.HasKey(t => t.VibScanID);

            // Properties
            this.Property(t => t.VibScanName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.VibScanAddress)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("T_VibScan");
            this.Property(t => t.VibScanID).HasColumnName("VibScanID");
            this.Property(t => t.COMID).HasColumnName("COMID");
            this.Property(t => t.VibScanName).HasColumnName("VibScanName");
            this.Property(t => t.VibScanAddress).HasColumnName("VibScanAddress");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
