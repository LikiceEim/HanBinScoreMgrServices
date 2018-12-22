using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.OldEF.Models.Mapping
{
    public class T_VibSingalHisDataMap : EntityTypeConfiguration<T_VibSingalHisData>
    {
        public T_VibSingalHisDataMap()
        {
            // Primary Key
            this.HasKey(t => t.HisDataID);

            // Properties
            this.Property(t => t.WaveData)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("T_VibSingalHisData");
            this.Property(t => t.HisDataID).HasColumnName("HisDataID");
            this.Property(t => t.SingalID).HasColumnName("SingalID");
            this.Property(t => t.MSID).HasColumnName("MSID");
            this.Property(t => t.DevID).HasColumnName("DevID");
            this.Property(t => t.SamplingDate).HasColumnName("SamplingDate");
            this.Property(t => t.Rotate).HasColumnName("Rotate");
            this.Property(t => t.WaveData).HasColumnName("WaveData");
            this.Property(t => t.TransformCofe).HasColumnName("TransformCofe");
            this.Property(t => t.RealSamplingFrequency).HasColumnName("RealSamplingFrequency");
            this.Property(t => t.SamplingPointData).HasColumnName("SamplingPointData");
            this.Property(t => t.AlmStatus).HasColumnName("AlmStatus");
            this.Property(t => t.E1).HasColumnName("E1");
            this.Property(t => t.E2).HasColumnName("E2");
            this.Property(t => t.E3).HasColumnName("E3");
            this.Property(t => t.E4).HasColumnName("E4");
            this.Property(t => t.E5).HasColumnName("E5");
            this.Property(t => t.E6).HasColumnName("E6");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
