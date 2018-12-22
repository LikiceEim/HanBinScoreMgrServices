using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMesh
{
    public enum eResetReqOff
    {
        DN_RESET_REQ_OFFS_TYPE = 0,
        DN_RESET_REQ_OFFS_MACADDRESS = DN_RESET_REQ_OFFS_TYPE + 1,
        DN_RESET_REQ_LEN = DN_RESET_REQ_OFFS_MACADDRESS + 9
    }
}
