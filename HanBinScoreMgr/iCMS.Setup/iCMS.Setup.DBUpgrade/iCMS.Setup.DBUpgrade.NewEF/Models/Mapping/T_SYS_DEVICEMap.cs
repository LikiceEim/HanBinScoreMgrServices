using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace iCMS.Setup.DBUpgrade.NewEF.Models.Mapping
{
    public class T_SYS_DEVICEMap : EntityTypeConfiguration<T_SYS_DEVICE>
    {
        public T_SYS_DEVICEMap()
        {
            // Primary Key
            this.HasKey(t => t.DevID);

            // Properties
            this.Property(t => t.DevName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.DevNO)
                .HasMaxLength(50);

            this.Property(t => t.DevManufacturer)
                .HasMaxLength(100);

            this.Property(t => t.DevManager)
                .HasMaxLength(20);

            this.Property(t => t.DevPic)
                .HasMaxLength(100);

            this.Property(t => t.DevMark)
                .HasMaxLength(200);

            this.Property(t => t.Position)
                .HasMaxLength(100);

            this.Property(t => t.Media)
                .HasMaxLength(100);

            this.Property(t => t.CouplingType)
                .HasMaxLength(100);

            this.Property(t => t.PersonInCharge)
                .HasMaxLength(50);

            this.Property(t => t.PersonInChargeTel)
                .HasMaxLength(50);

            this.Property(t => t.DevModel)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("T_SYS_DEVICE");
            this.Property(t => t.DevID).HasColumnName("DevID");
            this.Property(t => t.MonitorTreeID).HasColumnName("MonitorTreeID");
            this.Property(t => t.DevName).HasColumnName("DevName");
            this.Property(t => t.DevNO).HasColumnName("DevNO");
            this.Property(t => t.Rotate).HasColumnName("Rotate");
            this.Property(t => t.DevType).HasColumnName("DevType");
            this.Property(t => t.DevManufacturer).HasColumnName("DevManufacturer");
            this.Property(t => t.LastCheckDate).HasColumnName("LastCheckDate");
            this.Property(t => t.DevManager).HasColumnName("DevManager");
            this.Property(t => t.DevPic).HasColumnName("DevPic");
            this.Property(t => t.DevMadeDate).HasColumnName("DevMadeDate");
            this.Property(t => t.DevMark).HasColumnName("DevMark");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.AlmStatus).HasColumnName("AlmStatus");
            this.Property(t => t.DevSDate).HasColumnName("DevSDate");
            this.Property(t => t.Length).HasColumnName("Length");
            this.Property(t => t.Width).HasColumnName("Width");
            this.Property(t => t.Height).HasColumnName("Height");
            this.Property(t => t.outputVolume).HasColumnName("outputVolume");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.SensorSize).HasColumnName("SensorSize");
            this.Property(t => t.Power).HasColumnName("Power");
            this.Property(t => t.RatedCurrent).HasColumnName("RatedCurrent");
            this.Property(t => t.RatedVoltage).HasColumnName("RatedVoltage");
            this.Property(t => t.Media).HasColumnName("Media");
            this.Property(t => t.OutputMaxPressure).HasColumnName("OutputMaxPressure");
            this.Property(t => t.HeadOfDelivery).HasColumnName("HeadOfDelivery");
            this.Property(t => t.CouplingType).HasColumnName("CouplingType");
            this.Property(t => t.UseType).HasColumnName("UseType");
            this.Property(t => t.RunStatus).HasColumnName("RunStatus");
            this.Property(t => t.ImageID).HasColumnName("ImageID");
            this.Property(t => t.PersonInCharge).HasColumnName("PersonInCharge");
            this.Property(t => t.PersonInChargeTel).HasColumnName("PersonInChargeTel");
            this.Property(t => t.DevModel).HasColumnName("DevModel");
            this.Property(t => t.StatusCriticalValue).HasColumnName("StatusCriticalValue");
            this.Property(t => t.AddDate).HasColumnName("AddDate");
        }
    }
}