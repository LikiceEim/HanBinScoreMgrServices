using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.OldEF.Models.Mapping
{
    public class T_BEARINGMap : EntityTypeConfiguration<T_BEARING>
    {
        public T_BEARINGMap()
        {
            // Primary Key
            this.HasKey(t => t.BearingID);

            // Properties
            this.Property(t => t.FactoryName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.FactoryID)
                .HasMaxLength(100);

            this.Property(t => t.BearingNum)
                .HasMaxLength(100);

            this.Property(t => t.BearingDescribe)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("T_BEARING");
            this.Property(t => t.BearingID).HasColumnName("BearingID");
            this.Property(t => t.FactoryName).HasColumnName("FactoryName");
            this.Property(t => t.FactoryID).HasColumnName("FactoryID");
            this.Property(t => t.BearingNum).HasColumnName("BearingNum");
            this.Property(t => t.BearingDescribe).HasColumnName("BearingDescribe");
            this.Property(t => t.BPFO).HasColumnName("BPFO");
            this.Property(t => t.BPFI).HasColumnName("BPFI");
            this.Property(t => t.BSF).HasColumnName("BSF");
            this.Property(t => t.FTF).HasColumnName("FTF");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
