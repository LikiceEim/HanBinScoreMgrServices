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

            string cloudServer = @"http://111.231.200.224:8842/HanBinScoreService.svc";
            string localServer = @"http://127.0.0.1:8829/HanBinScoreService.svc";
            RestClient client = new RestClient(cloudServer);
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