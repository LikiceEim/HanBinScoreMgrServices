using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





using HanBin.Presentation.Service.ScoreService;
using System.IO;
using HanBin.Common.Component.Tool;
using HanBin.Common.Component.Data.Request.HanBin.ScoreManager;


namespace TESTConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            //var psd = MD5Helper.GetMD5("000000");


            //var payload = new Dictionary<string, object>
            //                {
            //                        { "name", "MrBug" },                
            //                        {"exp",1000},
            //                        {"jti","luozhipeng" }
            //                };

            //var privateKey = "QXM.HanBin";

            //var token = JsonWebToken.Encode(payload, privateKey, JwtHashAlgorithm.HS512);


            //var str = JsonWebToken.Decode(token, privateKey);


            //string filePath = @"D:\142934.docx";
            RestClient client = new RestClient("http://127.0.0.1:8829/HanBinScoreService.svc");
            GetHonourBoardParameter param = new GetHonourBoardParameter();
            param.RankNumber = 5;
            //UpFile upfile = new UpFile();
            //upfile.FileName = "testFile";
            //upfile.FileSize = 100;

            //upfile.FileStream = File.Create(filePath);
            string json = param.ToClientString();
            var res = client.Post(json, "GetHonourBoard");

            Console.WriteLine(res);

            Console.ReadKey();
        }

        static Tuple<string, string, string> GetNames()
        {
            return new Tuple<string, string, string>("a", "b", "c");
        }
    }
}