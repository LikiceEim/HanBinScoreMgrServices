using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class T_DATA_TEMPE_WS_MSITEDATA_2Map : EntityTypeConfiguration<T_DATA_TEMPE_WS_MSITEDATA_2>
    {
        public T_DATA_TEMPE_WS_MSITEDATA_2Map()
        {
            // Primary Key
            this.HasKey(t => t.MsiteDataID);

            // Properties
            // Table & Column Mappings
            this.ToTable("T_DATA_TEMPE_WS_MSITEDATA_2");
            this.Property(t => t.MsiteDataID).HasColumnName("MsiteDataID");
            this.Property(t => t.MsiteID).HasColumnName("MsiteID");
            this.Property(t => t.SamplingDate).HasColumnName("SamplingDate");
            this.Property(t => t.MsDataValue).HasColumnName("MsDataValue");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.WSID).HasColumnName("WSID");
            this.Property(t => t.WarnValue).HasColumnName("WarnValue");
            this.Property(t => t.AlmValue).HasColumnName("AlmValue");
            this.Property(t => t.MonthDate).HasColumnName("MonthDate");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
