using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Presentation.Service.DownloadService
{
    [ServiceContract]
    public interface IDownLoadFile
    {
        [WebGet(UriTemplate = "DownloadFile/{fileName}")]
        Stream DownloadFile(string fileName);
    }
}
