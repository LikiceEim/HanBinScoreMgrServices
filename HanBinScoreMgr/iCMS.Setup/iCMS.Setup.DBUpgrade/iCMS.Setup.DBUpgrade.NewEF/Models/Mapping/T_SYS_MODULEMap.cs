using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class T_SYS_MODULEMap : EntityTypeConfiguration<T_SYS_MODULE>
    {
        public T_SYS_MODULEMap()
        {
            // Primary Key
            this.HasKey(t => t.ModuleID);

            // Properties
            this.Property(t => t.ModuleName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Code)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("T_SYS_MODULE");
            this.Property(t => t.ModuleID).HasColumnName("ModuleID");
            this.Property(t => t.ModuleName).HasColumnName("ModuleName");
            this.Property(t => t.ParID).HasColumnName("ParID");
            this.Property(t => t.IsUsed).HasColumnName("IsUsed");
            this.Property(t => t.IsDeault).HasColumnName("IsDeault");
            this.Property(t => t.OID).HasColumnName("OID");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
            this.Property(t => t.Code).HasColumnName("Code");
        }
    }
}
