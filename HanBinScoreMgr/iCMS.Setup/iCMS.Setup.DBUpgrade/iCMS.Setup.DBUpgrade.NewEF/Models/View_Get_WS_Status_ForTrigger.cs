using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class View_Get_WS_Status_ForTrigger
    {
        public string FirmwareVersion { get; set; }
        public int WSID { get; set; }
        public string MAC { get; set; }
        public string MSName { get; set; }
        public int LinkStatu { get; set; }
        public string WSName { get; set; }
        public int UseStatus { get; set; }
        public Nullable<int> OperationType { get; set; }
        public Nullable<int> MSID { get; set; }
        public string CMDType { get; set; }
        public string TriggerStatus { get; set; }
        public Nullable<System.DateTime> EdateForTrigger { get; set; }
    }
}
