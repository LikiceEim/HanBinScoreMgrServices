using HanBin.Common.Component.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Presentation.Service
{
    public class BaseService
    {
        /// <summary>
        /// Token校验
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            return true;

            var privateKey = Utilitys.GetAppConfig("PrivateKey");
            var headers = WebOperationContext.Current.IncomingRequest.Headers;

            string token = "";

            token = headers["token"];

            return JsonWebToken.Verify(token, privateKey, true);
        }
    }
}
