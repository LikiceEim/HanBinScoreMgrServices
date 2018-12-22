using iCMS.Frameworks.Core.DB.Models;
using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Response.Cloud
{
    public class DeviceTypeResult
    {
        public List<DeviceType> DeviceTypeList { get; set; }

        public DeviceTypeResult() 
        {
            DeviceTypeList = new List<DeviceType>();
        }
    }
}
