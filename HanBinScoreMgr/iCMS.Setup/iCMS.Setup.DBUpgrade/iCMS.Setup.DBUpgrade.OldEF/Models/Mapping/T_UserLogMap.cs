using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.OldEF.Models.Mapping
{
    public class T_UserLogMap : EntityTypeConfiguration<T_UserLog>
    {
        public T_UserLogMap()
        {
            // Primary Key
            this.HasKey(t => t.UserLogID);

            // Properties
            this.Property(t => t.Record)
                .IsRequired()
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("T_UserLog");
            this.Property(t => t.UserLogID).HasColumnName("UserLogID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.Record).HasColumnName("Record");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
