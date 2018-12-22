using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class T_SYS_USERLOGMap : EntityTypeConfiguration<T_SYS_USERLOG>
    {
        public T_SYS_USERLOGMap()
        {
            // Primary Key
            this.HasKey(t => t.UserLogID);

            // Properties
            this.Property(t => t.Record)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.IPAddress)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("T_SYS_USERLOG");
            this.Property(t => t.UserLogID).HasColumnName("UserLogID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.Record).HasColumnName("Record");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
            this.Property(t => t.IPAddress).HasColumnName("IPAddress");
        }
    }
}
