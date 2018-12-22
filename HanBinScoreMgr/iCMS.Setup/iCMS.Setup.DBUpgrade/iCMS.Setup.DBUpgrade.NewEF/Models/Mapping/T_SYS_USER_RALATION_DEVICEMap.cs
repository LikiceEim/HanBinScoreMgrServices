using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class T_SYS_USER_RALATION_DEVICEMap : EntityTypeConfiguration<T_SYS_USER_RALATION_DEVICE>
    {
        public T_SYS_USER_RALATION_DEVICEMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.MTIds)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("T_SYS_USER_RALATION_DEVICE");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.DevId).HasColumnName("DevId");
            this.Property(t => t.MTIds).HasColumnName("MTIds");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}
