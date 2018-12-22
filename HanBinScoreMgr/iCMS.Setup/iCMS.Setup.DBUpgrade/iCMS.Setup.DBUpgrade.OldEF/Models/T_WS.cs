using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.OldEF.Models
{
    public partial class T_WS
    {
        public int WSID { get; set; }
        public int WGID { get; set; }
        public string WSName { get; set; }
        public int WSNO { get; set; }
        public float BatteryVolatage { get; set; }
        public int Status { get; set; }
        public string MACADDR { get; set; }
        public int SensorType { get; set; }
        public int LinkStatus { get; set; }
        public System.DateTime AddDate { get; set; }
        public string Vendor { get; set; }
        public string Model { get; set; }
        public string SetupTime { get; set; }
        public string SetupPersonInCharge { get; set; }
        public string SNCode { get; set; }
        public string FirmwareVersion { get; set; }
        public string AntiExplosionSerialNo { get; set; }
        public int RunStatus { get; set; }
        public int ImageID { get; set; }
        public string PersonInCharge { get; set; }
        public string PersonInChargeTel { get; set; }
        public string Remark { get; set; }
    }
}
