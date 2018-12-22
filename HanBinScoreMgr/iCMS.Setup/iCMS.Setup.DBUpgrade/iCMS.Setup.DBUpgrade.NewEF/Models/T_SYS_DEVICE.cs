using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_SYS_DEVICE
    {
        public int DevID { get; set; }
        public int MonitorTreeID { get; set; }
        public string DevName { get; set; }
        public string DevNO { get; set; }
        public float Rotate { get; set; }
        public int DevType { get; set; }
        public string DevManufacturer { get; set; }
        public Nullable<System.DateTime> LastCheckDate { get; set; }
        public string DevManager { get; set; }
        public string DevPic { get; set; }
        public Nullable<System.DateTime> DevMadeDate { get; set; }
        public string DevMark { get; set; }
        public Nullable<float> Longitude { get; set; }
        public Nullable<float> Latitude { get; set; }
        public int AlmStatus { get; set; }
        public System.DateTime DevSDate { get; set; }
        public Nullable<float> Length { get; set; }
        public Nullable<float> Width { get; set; }
        public Nullable<float> Height { get; set; }
        public Nullable<float> outputVolume { get; set; }
        public string Position { get; set; }
        public int SensorSize { get; set; }
        public Nullable<float> Power { get; set; }
        public Nullable<float> RatedCurrent { get; set; }
        public Nullable<float> RatedVoltage { get; set; }
        public string Media { get; set; }
        public Nullable<float> OutputMaxPressure { get; set; }
        public Nullable<float> HeadOfDelivery { get; set; }
        public string CouplingType { get; set; }
        public int UseType { get; set; }
        public int RunStatus { get; set; }
        public int ImageID { get; set; }
        public string PersonInCharge { get; set; }
        public string PersonInChargeTel { get; set; }
        public string DevModel { get; set; }
        public Nullable<float> StatusCriticalValue { get; set; }
        public System.DateTime AddDate { get; set; }
    }
}
