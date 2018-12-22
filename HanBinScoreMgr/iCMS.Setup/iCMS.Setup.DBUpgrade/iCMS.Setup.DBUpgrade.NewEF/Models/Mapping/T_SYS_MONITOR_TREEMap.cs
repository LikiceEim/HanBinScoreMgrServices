using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class T_SYS_MONITOR_TREEMap : EntityTypeConfiguration<T_SYS_MONITOR_TREE>
    {
        public T_SYS_MONITOR_TREEMap()
        {
            // Primary Key
            this.HasKey(t => t.MonitorTreeID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Des)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("T_SYS_MONITOR_TREE");
            this.Property(t => t.MonitorTreeID).HasColumnName("MonitorTreeID");
            this.Property(t => t.PID).HasColumnName("PID");
            this.Property(t => t.OID).HasColumnName("OID");
            this.Property(t => t.IsDefault).HasColumnName("IsDefault");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Des).HasColumnName("Des");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.ImageID).HasColumnName("ImageID");
            this.Property(t => t.MonitorTreePropertyID).HasColumnName("MonitorTreePropertyID");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
