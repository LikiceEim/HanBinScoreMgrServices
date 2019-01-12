using HanBin.Common.Component.Data.Base;
using HanBin.Presentation.Service.ScoreService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Presentation.Service.FileService
{
    [ServiceContract]
    public interface IFileService
    {
        [WebInvoke(UriTemplate = "UploadFile/{filename}", Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        BaseResponse<UpFileResult> UploadFile(string filename, Stream FileStream);

        [WebGet(UriTemplate = "DownloadFile/{fileName}")]
        Stream DownLoadFile(string fileName);
    }
}
