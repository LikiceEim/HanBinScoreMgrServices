using HanBin.Common.Component.Data.Base;
using HanBin.Common.Component.Tool;
using HanBin.Presentation.Service.ScoreService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HanBin.Presentation.Service.FileService
{

    public class FileService : IFileService
    {
        public BaseResponse<UpFileResult> UploadFile(string filename, Stream FileStream)
        {
            UpFileResult result = new UpFileResult();
            BaseResponse<UpFileResult> response = new BaseResponse<UpFileResult>();

            try
            {
                #region 获取文件名
                var fName = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.ToString();
                int len = fName.Length;
                int pos = fName.LastIndexOf('/');
                var realName = fName.Substring(pos + 1, (len - pos - 1));
                var extention = Path.GetExtension(realName).TrimStart('.');
                var oldName = realName.Substring(0, realName.LastIndexOf('.'));
                var saveFileName = oldName + DateTime.Now.Ticks + "." + extention;

                #endregion

                //获取文件大小
                long fileLength = WebOperationContext.Current.IncomingRequest.ContentLength;
                //fileLength = FileStream.Length;
                //UpFile parameter = new UpFile();

                if (true)
                {
                    string path = System.AppDomain.CurrentDomain.BaseDirectory + @"\UploadFiles\";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    byte[] buffer = new byte[fileLength];

                    string filePath = Path.Combine(path, saveFileName);
                    FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);

                    int count = 0;
                    while ((count = FileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fs.Write(buffer, 0, count);
                    }
                    //清空缓冲区
                    fs.Flush();
                    //关闭流
                    fs.Close();
                    result.FilePath = saveFileName;
                    response.Result = result;
                    return response;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Reason = "JWT_ERR";
                    return response;
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
                response.IsSuccessful = false;
                response.Reason = e.Message;
                return response;
            }
        }

        public Stream DownLoadFile(string fileName)
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
