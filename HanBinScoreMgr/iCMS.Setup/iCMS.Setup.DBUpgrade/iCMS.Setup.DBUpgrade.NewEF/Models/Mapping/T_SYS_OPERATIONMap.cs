using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class T_SYS_OPERATIONMap : EntityTypeConfiguration<T_SYS_OPERATION>
    {
        public T_SYS_OPERATIONMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.OperatorKey)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.OperationResult)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("T_SYS_OPERATION");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.OperatorKey).HasColumnName("OperatorKey");
            this.Property(t => t.MSID).HasColumnName("MSID");
            this.Property(t => t.OperationType).HasColumnName("OperationType");
            this.Property(t => t.Bdate).HasColumnName("Bdate");
            this.Property(t => t.EDate).HasColumnName("EDate");
            this.Property(t => t.OperationResult).HasColumnName("OperationResult");
            this.Property(t => t.OperationReson).HasColumnName("OperationReson");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
            this.Property(t => t.DAQStyle).HasColumnName("DAQStyle");
            this.Property(t => t.WSID).HasColumnName("WSID");
        }
    }
}
