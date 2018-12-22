using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.OldEF.Models.Mapping
{
    public class T_ModuleMap : EntityTypeConfiguration<T_Module>
    {
        public T_ModuleMap()
        {
            // Primary Key
            this.HasKey(t => t.ModuleID);

            // Properties
            this.Property(t => t.ModuleName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ModuleValue)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Code)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("T_Module");
            this.Property(t => t.ModuleID).HasColumnName("ModuleID");
            this.Property(t => t.ModuleName).HasColumnName("ModuleName");
            this.Property(t => t.ModuleValue).HasColumnName("ModuleValue");
            this.Property(t => t.ParID).HasColumnName("ParID");
            this.Property(t => t.OID).HasColumnName("OID");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
            this.Property(t => t.Code).HasColumnName("Code");
        }
    }
}
