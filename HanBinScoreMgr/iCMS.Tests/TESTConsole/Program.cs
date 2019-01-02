using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





using HanBin.Presentation.Service.ScoreService;
using System.IO;
using HanBin.Common.Component.Tool;
using HanBin.Common.Component.Data.Request.HanBin.ScoreManager;
using HanBin.Common.Component.Data.Request.HanBin.OfficerManager;


namespace TESTConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            string cloudServer = @"http://111.231.200.224:8842/HanBinScoreService.svc";
            string localServer = @"http://localhost:2892/HanBin/SystemService";
            RestClient client = new RestClient(localServer);
            GetOfficerListParameter param = new GetOfficerListParameter();
            param.OrganizationID = -1;
            param.LevelID = -1;
            param.Keyword = "";
            param.Page = 1;
            param.PageSize = 15;
            //UpFile upfile = new UpFile();
            //upfile.FileName = "testFile";
            //upfile.FileSize = 100;

            //upfile.FileStream = File.Create(filePath);
            string json = param.ToClientString();
            var res = client.Post(json, "GetOfficerList");

            Console.WriteLine(res);

            Console.ReadKey();
        }

        static Tuple<string, string, string> GetNames()
        {
            return new Tuple<string, string, string>("a", "b", "c");
        }
    }
}