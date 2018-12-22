using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DAUService
{
    public class GetUsableChannelByDAUIDResult
    {
        public List<ChannelInfo> ChannelInfo { get; set; }

        public GetUsableChannelByDAUIDResult()
        {
            this.ChannelInfo = new List<ChannelInfo>();
        }
    }

    public class ChannelInfo
    {
        public int WSID { get; set; }

        public string WSName { get; set; }
    }
}
