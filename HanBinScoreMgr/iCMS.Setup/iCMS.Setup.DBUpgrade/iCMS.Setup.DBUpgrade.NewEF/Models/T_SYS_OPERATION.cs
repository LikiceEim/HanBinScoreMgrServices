using System;
using System.Collections.Generic;

namespace iCMS.Setup.DBUpgrade.NewEF.Models
{
    public partial class T_SYS_OPERATION
    {
        public int id { get; set; }
        public string OperatorKey { get; set; }
        public int MSID { get; set; }
        public int OperationType { get; set; }
        public System.DateTime Bdate { get; set; }
        public Nullable<System.DateTime> EDate { get; set; }
        public string OperationResult { get; set; }
        public string OperationReson { get; set; }
        public System.DateTime AddDate { get; set; }
        public Nullable<int> DAQStyle { get; set; }
        public Nullable<int> WSID { get; set; }
    }
}
