using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.OldEF.Models.Mapping
{
    public class T_DevGroupMap : EntityTypeConfiguration<T_DevGroup>
    {
        public T_DevGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.DevGroupID);

            // Properties
            this.Property(t => t.DevGroupName)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Des)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("T_DevGroup");
            this.Property(t => t.DevGroupID).HasColumnName("DevGroupID");
            this.Property(t => t.MonitorTreeID).HasColumnName("MonitorTreeID");
            this.Property(t => t.DevGroupName).HasColumnName("DevGroupName");
            this.Property(t => t.Des).HasColumnName("Des");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
