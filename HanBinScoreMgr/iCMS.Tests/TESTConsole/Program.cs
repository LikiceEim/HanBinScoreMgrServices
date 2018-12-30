using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

using iCMS.Common.Component.Tool;
using iCMS.Common.Component.Tool.Extensions;
using iCMS.Common.Component.Data.Request;
using iCMS.Common.Component.Data.Base;


using iCMS.Common.Component.Data.Enum;


using iCMS.Common.Component.Data.Request.HanBin.OrganManage;
using iCMS.Common.Component.Data.Request.HanBin.OfficerManager;
using iCMS.Common.Component.Data.Request.HanBin.SystemManage;

namespace TESTConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            var psd = MD5Helper.GetMD5("000000");

            
            var payload = new Dictionary<string, object>
                            {
                                    { "name", "MrBug" },                
                                    {"exp",1000},
                                    {"jti","luozhipeng" }
                            };

            var privateKey = "QXM.HanBin";

            var token = JsonWebToken.Encode(payload, privateKey, JwtHashAlgorithm.HS512);


            var str = JsonWebToken.Decode(token, privateKey);

            //RestClient client = new RestClient("http://localhost:2892/HanBin/SystemService");

            //for (int i = 0; i < 5; i++)
            //{
            //    string method = "AddUser";
            //    AddUserParameter param = new AddUserParameter();
            //    param.UserToken = "Test2" + i;
            //    param.Gender = 1;
            //    param.AddUserID = 1;
            //    param.OrganizationID = 1;
            //    param.PWD = "pawword";
            //    param.RoleID = 1;



            //    string json = param.ToClientString();

            //    string retPost = client.Post(json, method);
            //    Console.WriteLine("post请求：" + retPost);
            //}



            Console.ReadKey();
        }

        static Tuple<string, string, string> GetNames()
        {
            return new Tuple<string, string, string>("a", "b", "c");
        }
    }
}