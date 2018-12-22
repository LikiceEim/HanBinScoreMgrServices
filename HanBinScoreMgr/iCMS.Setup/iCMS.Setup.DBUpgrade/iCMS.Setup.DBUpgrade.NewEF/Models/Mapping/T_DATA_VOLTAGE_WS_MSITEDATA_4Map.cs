using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class T_DATA_VOLTAGE_WS_MSITEDATA_4Map : EntityTypeConfiguration<T_DATA_VOLTAGE_WS_MSITEDATA_4>
    {
        public T_DATA_VOLTAGE_WS_MSITEDATA_4Map()
        {
            // Primary Key
            this.HasKey(t => t.MsiteDataID);

            // Properties
            // Table & Column Mappings
            this.ToTable("T_DATA_VOLTAGE_WS_MSITEDATA_4");
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
