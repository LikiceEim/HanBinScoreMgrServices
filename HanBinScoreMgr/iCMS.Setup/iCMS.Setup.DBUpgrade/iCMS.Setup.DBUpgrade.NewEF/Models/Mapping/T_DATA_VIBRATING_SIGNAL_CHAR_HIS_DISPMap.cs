using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class T_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISPMap : EntityTypeConfiguration<T_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP>
    {
        public T_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISPMap()
        {
            // Primary Key
            this.HasKey(t => t.HisDataID);

            // Properties
            this.Property(t => t.WaveDataPath)
                .HasMaxLength(100);

            this.Property(t => t.Rotate)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("T_DATA_VIBRATING_SIGNAL_CHAR_HIS_DISP");
            this.Property(t => t.HisDataID).HasColumnName("HisDataID");
            this.Property(t => t.SingalID).HasColumnName("SingalID");
            this.Property(t => t.MsiteID).HasColumnName("MsiteID");
            this.Property(t => t.DevID).HasColumnName("DevID");
            this.Property(t => t.SamplingDate).HasColumnName("SamplingDate");
            this.Property(t => t.WaveDataPath).HasColumnName("WaveDataPath");
            this.Property(t => t.Rotate).HasColumnName("Rotate");
            this.Property(t => t.TransformCofe).HasColumnName("TransformCofe");
            this.Property(t => t.RealSamplingFrequency).HasColumnName("RealSamplingFrequency");
            this.Property(t => t.SamplingPointData).HasColumnName("SamplingPointData");
            this.Property(t => t.AlmStatus).HasColumnName("AlmStatus");
            this.Property(t => t.DAQStyle).HasColumnName("DAQStyle");
            this.Property(t => t.PeakValue).HasColumnName("PeakValue");
            this.Property(t => t.PeakPeakValue).HasColumnName("PeakPeakValue");
            this.Property(t => t.EffValue).HasColumnName("EffValue");
            this.Property(t => t.PeakWarnValue).HasColumnName("PeakWarnValue");
            this.Property(t => t.PeakAlmValue).HasColumnName("PeakAlmValue");
            this.Property(t => t.PeakPeakWarnValue).HasColumnName("PeakPeakWarnValue");
            this.Property(t => t.PeakPeakAlmValue).HasColumnName("PeakPeakAlmValue");
            this.Property(t => t.EffWarnValue).HasColumnName("EffWarnValue");
            this.Property(t => t.EffAlmValue).HasColumnName("EffAlmValue");
            this.Property(t => t.MonthDate).HasColumnName("MonthDate");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
