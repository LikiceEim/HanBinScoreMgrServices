using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.OldEF.Models.Mapping
{
    public class T_ImageMap : EntityTypeConfiguration<T_Image>
    {
        public T_ImageMap()
        {
            // Primary Key
            this.HasKey(t => t.ImageID);

            // Properties
            this.Property(t => t.ImageName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ImageURL)
                .HasMaxLength(100);

            this.Property(t => t.ImagePath)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("T_Image");
            this.Property(t => t.ImageID).HasColumnName("ImageID");
            this.Property(t => t.ImageName).HasColumnName("ImageName");
            this.Property(t => t.ImageURL).HasColumnName("ImageURL");
            this.Property(t => t.ImagePath).HasColumnName("ImagePath");
            this.Property(t => t.Width).HasColumnName("Width");
            this.Property(t => t.Height).HasColumnName("Height");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
