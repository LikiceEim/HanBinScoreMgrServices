using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.OldEF.Models.Mapping
{
    public class T_MSiteDataMap : EntityTypeConfiguration<T_MSiteData>
    {
        public T_MSiteDataMap()
        {
            // Primary Key
            this.HasKey(t => t.MSiteDataID);

            // Properties
            // Table & Column Mappings
            this.ToTable("T_MSiteData");
            this.Property(t => t.MSiteDataID).HasColumnName("MSiteDataID");
            this.Property(t => t.MSiteID).HasColumnName("MSiteID");
            this.Property(t => t.MSDType).HasColumnName("MSDType");
            this.Property(t => t.MSDDate).HasColumnName("MSDDate");
            this.Property(t => t.MSDValue).HasColumnName("MSDValue");
            this.Property(t => t.MSDStatus).HasColumnName("MSDStatus");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
