using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class T_DICT_SENSOR_TYPEMap : EntityTypeConfiguration<T_DICT_SENSOR_TYPE>
    {
        public T_DICT_SENSOR_TYPEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Describe)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("T_DICT_SENSOR_TYPE");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Describe).HasColumnName("Describe");
            this.Property(t => t.IsUsable).HasColumnName("IsUsable");
            this.Property(t => t.IsDefault).HasColumnName("IsDefault");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
