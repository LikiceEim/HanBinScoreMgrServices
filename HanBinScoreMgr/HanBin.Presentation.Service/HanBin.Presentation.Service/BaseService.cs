using HanBin.Common.Component.Data.Base;
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
        public bool Validate(BaseRequest param)
        {
            var request = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.BaseUri.Host;
            param.RequestIP = request;

            //return true;

            if (!string.IsNullOrEmpty(param.Token))
            {
                var privateKey = Utilitys.GetAppConfig("PrivateKey");
                return JsonWebToken.Verify(param.Token, privateKey, true);
            }
            else
            {
                return false;
            }
        }
    }
}
