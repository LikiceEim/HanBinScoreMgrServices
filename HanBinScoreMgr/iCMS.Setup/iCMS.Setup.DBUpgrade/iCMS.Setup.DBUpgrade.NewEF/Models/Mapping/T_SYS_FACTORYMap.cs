using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class T_SYS_FACTORYMap : EntityTypeConfiguration<T_SYS_FACTORY>
    {
        public T_SYS_FACTORYMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.FactoryID)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.FactoryName)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("T_SYS_FACTORY");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.FactoryID).HasColumnName("FactoryID");
            this.Property(t => t.FactoryName).HasColumnName("FactoryName");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
