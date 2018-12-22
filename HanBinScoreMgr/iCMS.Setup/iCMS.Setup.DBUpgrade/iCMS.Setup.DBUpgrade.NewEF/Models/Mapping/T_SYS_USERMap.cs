using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class T_SYS_USERMap : EntityTypeConfiguration<T_SYS_USER>
    {
        public T_SYS_USERMap()
        {
            // Primary Key
            this.HasKey(t => t.UserID);

            // Properties
            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.PSW)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(50);

            this.Property(t => t.LockPSW)
                .HasMaxLength(50);

            this.Property(t => t.Phone)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("T_SYS_USER");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.IsShow).HasColumnName("IsShow");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.PSW).HasColumnName("PSW");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.LockPSW).HasColumnName("LockPSW");
            this.Property(t => t.LoginCount).HasColumnName("LoginCount");
            this.Property(t => t.LastLoginDate).HasColumnName("LastLoginDate");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
        }
    }
}
