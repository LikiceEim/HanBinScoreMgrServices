using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class T_SYS_ROLEMap : EntityTypeConfiguration<T_SYS_ROLE>
    {
        public T_SYS_ROLEMap()
        {
            // Primary Key
            this.HasKey(t => t.RoleID);

            // Properties
            this.Property(t => t.RoleName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("T_SYS_ROLE");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.IsShow).HasColumnName("IsShow");
            this.Property(t => t.IsDeault).HasColumnName("IsDeault");
            this.Property(t => t.RoleName).HasColumnName("RoleName");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
