using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class T_SYS_VIBRATING_SET_SIGNALALMMap : EntityTypeConfiguration<T_SYS_VIBRATING_SET_SIGNALALM>
    {
        public T_SYS_VIBRATING_SET_SIGNALALMMap()
        {
            // Primary Key
            this.HasKey(t => new { t.SingalAlmID, t.SingalID, t.ValueType, t.WarnValue, t.AlmValue, t.Status, t.AddDate });

            // Properties
            this.Property(t => t.SingalAlmID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.SingalID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ValueType)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Status)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("T_SYS_VIBRATING_SET_SIGNALALM");
            this.Property(t => t.SingalAlmID).HasColumnName("SingalAlmID");
            this.Property(t => t.SingalID).HasColumnName("SingalID");
            this.Property(t => t.ValueType).HasColumnName("ValueType");
            this.Property(t => t.WarnValue).HasColumnName("WarnValue");
            this.Property(t => t.AlmValue).HasColumnName("AlmValue");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
            this.Property(t => t.UploadTrigger).HasColumnName("UploadTrigger");
            this.Property(t => t.ThrendAlarmPrvalue).HasColumnName("ThrendAlarmPrvalue");
        }
    }
}
