using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class T_SYS_VOLTAGE_SET_MSITEALMMap : EntityTypeConfiguration<T_SYS_VOLTAGE_SET_MSITEALM>
    {
        public T_SYS_VOLTAGE_SET_MSITEALMMap()
        {
            // Primary Key
            this.HasKey(t => t.MsiteAlmID);

            // Properties
            // Table & Column Mappings
            this.ToTable("T_SYS_VOLTAGE_SET_MSITEALM");
            this.Property(t => t.MsiteAlmID).HasColumnName("MsiteAlmID");
            this.Property(t => t.MsiteID).HasColumnName("MsiteID");
            this.Property(t => t.WarnValue).HasColumnName("WarnValue");
            this.Property(t => t.AlmValue).HasColumnName("AlmValue");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
