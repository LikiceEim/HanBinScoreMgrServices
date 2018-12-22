using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using iCMS.Common.Component.Data.Request;

namespace iCMS.Presentation.Server
{
    [ServiceContract]
    public interface IOtherService
    {

        #region 判断名称是否重复
        [WebInvoke]
        string DoWork();
        #endregion
    }
}
