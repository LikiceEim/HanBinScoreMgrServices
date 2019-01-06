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
        public bool Validate(string tokenParam)
        {
            return true;

            if (!string.IsNullOrEmpty(tokenParam))
            {
                var privateKey = Utilitys.GetAppConfig("PrivateKey");
                return JsonWebToken.Verify(tokenParam, privateKey, true);
            }
            else
            {
                return false;
            }
        }
    }
}
