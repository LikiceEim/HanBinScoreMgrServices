using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.OldEF.Models.Mapping
{
    public class T_MSiteAlmMap : EntityTypeConfiguration<T_MSiteAlm>
    {
        public T_MSiteAlmMap()
        {
            // Primary Key
            this.HasKey(t => t.MSiteAlmID);

            // Properties
            // Table & Column Mappings
            this.ToTable("T_MSiteAlm");
            this.Property(t => t.MSiteAlmID).HasColumnName("MSiteAlmID");
            this.Property(t => t.MSiteID).HasColumnName("MSiteID");
            this.Property(t => t.MSDType).HasColumnName("MSDType");
            this.Property(t => t.WarnValue).HasColumnName("WarnValue");
            this.Property(t => t.AlmValue).HasColumnName("AlmValue");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
