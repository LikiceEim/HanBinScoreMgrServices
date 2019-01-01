using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Tool.Extensions
{
    public class JsonRawMapper : WebContentTypeMapper
    {
        public override WebContentFormat GetMessageFormatForContentType(string contentType)
        {
            return WebContentFormat.Raw;
            //return base.GetMessageFormatForContentType(contentType);
        }
    }
}
