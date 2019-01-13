using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HanBin.Presentation.Service.DownloadService
{
    public class DownLoadFile : IDownLoadFile
    {
        public Stream DownloadFile(string fileName)
        {
            try
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory + @"\UploadFiles\";
                var FullFileName = Path.Combine(path, fileName);
                if (File.Exists(FullFileName))
                {
                    FileInfo DownloadFile = new FileInfo(FullFileName);
                    //WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                    var contentType = MimeMapping.GetMimeMapping(FullFileName);
                    WebOperationContext.Current.OutgoingResponse.ContentType = contentType;

                    WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "attachment;inline;filename=111");
                    Stream stream = File.OpenRead(FullFileName);
                    return stream;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
