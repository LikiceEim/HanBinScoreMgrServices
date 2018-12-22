using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Setup.DBUpgrade.UpgradeFM.DB
{
    /// <summary>
    /// 数据上下文标识接口，它对于业务层应该是公开的
    /// 它对于实现上下文的方法，它并不关心，可以是linq2sql,ef,ado.net,nhibernate,memory,nosql等
    /// </summary>
    public interface IUnitOfWork
    {
    }
}
