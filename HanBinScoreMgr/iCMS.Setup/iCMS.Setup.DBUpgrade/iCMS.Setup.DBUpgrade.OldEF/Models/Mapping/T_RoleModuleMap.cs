using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.OldEF.Models.Mapping
{
    public class T_RoleModuleMap : EntityTypeConfiguration<T_RoleModule>
    {
        public T_RoleModuleMap()
        {
            // Primary Key
            this.HasKey(t => t.RMID);

            // Properties
            // Table & Column Mappings
            this.ToTable("T_RoleModule");
            this.Property(t => t.RMID).HasColumnName("RMID");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.ModuleID).HasColumnName("ModuleID");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
