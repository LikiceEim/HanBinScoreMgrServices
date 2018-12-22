using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.OldEF.Models.Mapping
{
    public class T_VibSingalMap : EntityTypeConfiguration<T_VibSingal>
    {
        public T_VibSingalMap()
        {
            // Primary Key
            this.HasKey(t => t.SingalID);

            // Properties
            this.Property(t => t.Remark)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("T_VibSingal");
            this.Property(t => t.SingalID).HasColumnName("SingalID");
            this.Property(t => t.MSiteID).HasColumnName("MSiteID");
            this.Property(t => t.UpLimitFrequency).HasColumnName("UpLimitFrequency");
            this.Property(t => t.LowLimitFrequency).HasColumnName("LowLimitFrequency");
            this.Property(t => t.StorageTrighters).HasColumnName("StorageTrighters");
            this.Property(t => t.EnlvpBandW).HasColumnName("EnlvpBandW");
            this.Property(t => t.EnlvpFilter).HasColumnName("EnlvpFilter");
            this.Property(t => t.SingalType).HasColumnName("SingalType");
            this.Property(t => t.SingalStatus).HasColumnName("SingalStatus");
            this.Property(t => t.SingalSDate).HasColumnName("SingalSDate");
            this.Property(t => t.WaveDataLength).HasColumnName("WaveDataLength");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
            this.Property(t => t.Remark).HasColumnName("Remark");
        }
    }
}
