using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.OldEF.Models
{
    public partial class T_Device
    {
        public int DevID { get; set; }
        public int DevGroupID { get; set; }
        public Nullable<int> MonitorTreeID { get; set; }
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
        public int DevStatus { get; set; }
        public System.DateTime DevSDate { get; set; }
        public System.DateTime AddDate { get; set; }
        public Nullable<float> Length { get; set; }
        public Nullable<float> Width { get; set; }
        public Nullable<float> Height { get; set; }
        public string Model { get; set; }
        public string BearingType { get; set; }
        public string BearingModel { get; set; }
        public string LubricatingForm { get; set; }
        public Nullable<float> outputVolume { get; set; }
        public string Position { get; set; }
        public int SensorSize { get; set; }
        public Nullable<float> Power { get; set; }
        public long GroupNo { get; set; }
        public Nullable<float> RatedCurrent { get; set; }
        public Nullable<float> RatedVoltage { get; set; }
        public string Media { get; set; }
        public Nullable<float> OutputMaxPressure { get; set; }
        public Nullable<float> HeadOfDelivery { get; set; }
        public string CouplingType { get; set; }
        public int RunStatus { get; set; }
        public int ImageID { get; set; }
        public string PersonInCharge { get; set; }
        public string PersonInChargeTel { get; set; }

        public string DevModel { get; set; }
    }
}
