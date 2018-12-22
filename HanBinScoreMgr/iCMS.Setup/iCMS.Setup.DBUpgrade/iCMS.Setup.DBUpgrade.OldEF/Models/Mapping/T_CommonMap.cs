using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.OldEF.Models.Mapping
{
    public class T_CommonMap : EntityTypeConfiguration<T_Common>
    {
        public T_CommonMap()
        {
            // Primary Key
            this.HasKey(t => t.CID);

            // Properties
            this.Property(t => t.CValue)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.CDes)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("T_Common");
            this.Property(t => t.CID).HasColumnName("CID");
            this.Property(t => t.CValue).HasColumnName("CValue");
            this.Property(t => t.CPID).HasColumnName("CPID");
            this.Property(t => t.COID).HasColumnName("COID");
            this.Property(t => t.DataType).HasColumnName("DataType");
            this.Property(t => t.CDes).HasColumnName("CDes");
            this.Property(t => t.IsDefault).HasColumnName("IsDefault");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
