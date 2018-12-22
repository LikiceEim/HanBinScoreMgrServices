using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class View_Get_WS_Status_ForTriggerMap : EntityTypeConfiguration<View_Get_WS_Status_ForTrigger>
    {
        public View_Get_WS_Status_ForTriggerMap()
        {
            // Primary Key
            this.HasKey(t => new { t.WSID, t.MAC, t.LinkStatu, t.WSName, t.UseStatus });

            // Properties
            this.Property(t => t.FirmwareVersion)
                .HasMaxLength(50);

            this.Property(t => t.WSID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MAC)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.MSName)
                .HasMaxLength(50);

            this.Property(t => t.LinkStatu)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.WSName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.UseStatus)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.CMDType)
                .HasMaxLength(8);

            // Table & Column Mappings
            this.ToTable("View_Get_WS_Status_ForTrigger");
            this.Property(t => t.FirmwareVersion).HasColumnName("FirmwareVersion");
            this.Property(t => t.WSID).HasColumnName("WSID");
            this.Property(t => t.MAC).HasColumnName("MAC");
            this.Property(t => t.MSName).HasColumnName("MSName");
            this.Property(t => t.LinkStatu).HasColumnName("LinkStatu");
            this.Property(t => t.WSName).HasColumnName("WSName");
            this.Property(t => t.UseStatus).HasColumnName("UseStatus");
            this.Property(t => t.OperationType).HasColumnName("OperationType");
            this.Property(t => t.MSID).HasColumnName("MSID");
            this.Property(t => t.CMDType).HasColumnName("CMDType");
            this.Property(t => t.TriggerStatus).HasColumnName("TriggerStatus");
            this.Property(t => t.EdateForTrigger).HasColumnName("EdateForTrigger");
        }
    }
}
