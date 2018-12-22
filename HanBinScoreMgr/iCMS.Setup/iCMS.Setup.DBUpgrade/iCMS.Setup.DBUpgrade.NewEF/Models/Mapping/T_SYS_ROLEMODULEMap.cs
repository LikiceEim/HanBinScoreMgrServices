using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class T_SYS_ROLEMODULEMap : EntityTypeConfiguration<T_SYS_ROLEMODULE>
    {
        public T_SYS_ROLEMODULEMap()
        {
            // Primary Key
            this.HasKey(t => t.RMID);

            // Properties
            // Table & Column Mappings
            this.ToTable("T_SYS_ROLEMODULE");
            this.Property(t => t.RMID).HasColumnName("RMID");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.ModuleID).HasColumnName("ModuleID");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
