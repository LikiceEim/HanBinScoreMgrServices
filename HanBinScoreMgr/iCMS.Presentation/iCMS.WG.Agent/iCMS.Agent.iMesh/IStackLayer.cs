using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMesh
{
    /// <summary>
    /// 协议栈分层接口
    /// </summary>
    interface IStackLayer
    {
        void Initialize();
        void Reset();
    }
}
