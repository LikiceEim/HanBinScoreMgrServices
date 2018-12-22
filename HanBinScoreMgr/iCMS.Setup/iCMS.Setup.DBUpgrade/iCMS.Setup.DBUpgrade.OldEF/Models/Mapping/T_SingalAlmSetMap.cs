using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.OldEF.Models.Mapping
{
    public class T_SingalAlmSetMap : EntityTypeConfiguration<T_SingalAlmSet>
    {
        public T_SingalAlmSetMap()
        {
            // Primary Key
            this.HasKey(t => t.SingalAlmID);

            // Properties
            // Table & Column Mappings
            this.ToTable("T_SingalAlmSet");
            this.Property(t => t.SingalAlmID).HasColumnName("SingalAlmID");
            this.Property(t => t.SingalID).HasColumnName("SingalID");
            this.Property(t => t.ValueType).HasColumnName("ValueType");
            this.Property(t => t.WarnValue).HasColumnName("WarnValue");
            this.Property(t => t.AlmValue).HasColumnName("AlmValue");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
