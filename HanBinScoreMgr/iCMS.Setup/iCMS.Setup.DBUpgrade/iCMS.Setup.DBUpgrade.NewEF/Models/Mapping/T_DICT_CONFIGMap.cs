using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class T_DICT_CONFIGMap : EntityTypeConfiguration<T_DICT_CONFIG>
    {
        public T_DICT_CONFIGMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Describe)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("T_DICT_CONFIG");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsUsed).HasColumnName("IsUsed");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.IsDefault).HasColumnName("IsDefault");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
            this.Property(t => t.ParentId).HasColumnName("ParentId");
            this.Property(t => t.Describe).HasColumnName("Describe");
        }
    }
}
