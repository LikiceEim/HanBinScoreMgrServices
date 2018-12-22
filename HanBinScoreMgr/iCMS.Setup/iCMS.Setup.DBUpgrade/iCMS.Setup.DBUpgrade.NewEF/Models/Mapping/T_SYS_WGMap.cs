using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class T_SYS_WGMap : EntityTypeConfiguration<T_SYS_WG>
    {
        public T_SYS_WGMap()
        {
            // Primary Key
            this.HasKey(t => t.WGID);

            // Properties
            this.Property(t => t.WGName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.WGModel)
                .HasMaxLength(50);

            this.Property(t => t.SoftwareVersion)
                .HasMaxLength(50);

            this.Property(t => t.Remark)
                .HasMaxLength(100);

            this.Property(t => t.PersonInCharge)
                .HasMaxLength(50);

            this.Property(t => t.PersonInChargeTel)
                .HasMaxLength(50);

            this.Property(t => t.AgentAddress)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("T_SYS_WG");
            this.Property(t => t.WGID).HasColumnName("WGID");
            this.Property(t => t.WGName).HasColumnName("WGName");
            this.Property(t => t.WGNO).HasColumnName("WGNO");
            this.Property(t => t.NetWorkID).HasColumnName("NetWorkID");
            this.Property(t => t.WGType).HasColumnName("WGType");
            this.Property(t => t.LinkStatus).HasColumnName("LinkStatus");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
            this.Property(t => t.WGModel).HasColumnName("WGModel");
            this.Property(t => t.SoftwareVersion).HasColumnName("SoftwareVersion");
            this.Property(t => t.RunStatus).HasColumnName("RunStatus");
            this.Property(t => t.ImageID).HasColumnName("ImageID");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.PersonInCharge).HasColumnName("PersonInCharge");
            this.Property(t => t.PersonInChargeTel).HasColumnName("PersonInChargeTel");
            this.Property(t => t.MonitorTreeID).HasColumnName("MonitorTreeID");
            this.Property(t => t.AgentAddress).HasColumnName("AgentAddress");
        }
    }
}
