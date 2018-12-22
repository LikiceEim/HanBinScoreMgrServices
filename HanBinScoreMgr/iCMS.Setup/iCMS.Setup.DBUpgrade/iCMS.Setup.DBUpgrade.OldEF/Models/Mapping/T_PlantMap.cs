using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.OldEF.Models.Mapping
{
    public class T_PlantMap : EntityTypeConfiguration<T_Plant>
    {
        public T_PlantMap()
        {
            // Primary Key
            this.HasKey(t => t.PlantID);

            // Properties
            this.Property(t => t.Pname)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.PMark)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("T_Plant");
            this.Property(t => t.PlantID).HasColumnName("PlantID");
            this.Property(t => t.FactoryID).HasColumnName("FactoryID");
            this.Property(t => t.Pname).HasColumnName("Pname");
            this.Property(t => t.PMark).HasColumnName("PMark");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.PStatus).HasColumnName("PStatus");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
