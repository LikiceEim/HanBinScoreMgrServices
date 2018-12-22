using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.OldEF.Models.Mapping
{
    public class T_SysLogMap : EntityTypeConfiguration<T_SysLog>
    {
        public T_SysLogMap()
        {
            // Primary Key
            this.HasKey(t => t.SysLogID);

            // Properties
            this.Property(t => t.Record)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("T_SysLog");
            this.Property(t => t.SysLogID).HasColumnName("SysLogID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.Record).HasColumnName("Record");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
