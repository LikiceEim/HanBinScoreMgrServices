using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.OldEF.Models.Mapping
{
    public class T_COMMap : EntityTypeConfiguration<T_COM>
    {
        public T_COMMap()
        {
            // Primary Key
            this.HasKey(t => t.COMID);

            // Properties
            this.Property(t => t.COMName)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("T_COM");
            this.Property(t => t.COMID).HasColumnName("COMID");
            this.Property(t => t.COMName).HasColumnName("COMName");
            this.Property(t => t.BaudRate).HasColumnName("BaudRate");
            this.Property(t => t.ParCheck).HasColumnName("ParCheck");
            this.Property(t => t.DataBit).HasColumnName("DataBit");
            this.Property(t => t.StopBit).HasColumnName("StopBit");
            this.Property(t => t.CRCCheck).HasColumnName("CRCCheck");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
