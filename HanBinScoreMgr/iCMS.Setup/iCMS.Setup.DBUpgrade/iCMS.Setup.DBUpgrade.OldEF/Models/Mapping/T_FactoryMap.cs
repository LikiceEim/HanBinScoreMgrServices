using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.OldEF.Models.Mapping
{
    public class T_FactoryMap : EntityTypeConfiguration<T_Factory>
    {
        public T_FactoryMap()
        {
            // Primary Key
            this.HasKey(t => t.FactoryID);

            // Properties
            this.Property(t => t.FacName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.FacAddress)
                .HasMaxLength(100);

            this.Property(t => t.FacMark)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("T_Factory");
            this.Property(t => t.FactoryID).HasColumnName("FactoryID");
            this.Property(t => t.FacName).HasColumnName("FacName");
            this.Property(t => t.FacAddress).HasColumnName("FacAddress");
            this.Property(t => t.FacMark).HasColumnName("FacMark");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.FacStatus).HasColumnName("FacStatus");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
