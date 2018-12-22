using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.OldEF.Models.Mapping
{
    public class T_MonitorTreePropertyMap : EntityTypeConfiguration<T_MonitorTreeProperty>
    {
        public T_MonitorTreePropertyMap()
        {
            // Primary Key
            this.HasKey(t => t.MonitorTreePropertyID);

            // Properties
            this.Property(t => t.Address)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.URL)
                .HasMaxLength(100);

            this.Property(t => t.TelphoneNO)
                .HasMaxLength(100);

            this.Property(t => t.FaxNO)
                .HasMaxLength(100);

            this.Property(t => t.PersonInCharge)
                .HasMaxLength(50);

            this.Property(t => t.PersonInChargeTel)
                .HasMaxLength(50);

            this.Property(t => t.Remark)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("T_MonitorTreeProperty");
            this.Property(t => t.MonitorTreePropertyID).HasColumnName("MonitorTreePropertyID");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.URL).HasColumnName("URL");
            this.Property(t => t.TelphoneNO).HasColumnName("TelphoneNO");
            this.Property(t => t.FaxNO).HasColumnName("FaxNO");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.Longtitude).HasColumnName("Longtitude");
            this.Property(t => t.ChildCount).HasColumnName("ChildCount");
            this.Property(t => t.Length).HasColumnName("Length");
            this.Property(t => t.Width).HasColumnName("Width");
            this.Property(t => t.Area).HasColumnName("Area");
            this.Property(t => t.PersonInCharge).HasColumnName("PersonInCharge");
            this.Property(t => t.PersonInChargeTel).HasColumnName("PersonInChargeTel");
            this.Property(t => t.Remark).HasColumnName("Remark");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
