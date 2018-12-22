using iCMS.Common.Component.Data.Response.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.SystemInitSets
{
    public class ComSetDataForVibAndEigenResult
    {
        /// <summary>
        /// 振动信号类型集合
        /// </summary>
        public List<CommonInfo> VibsInfo { get; set; }

        /// <summary>
        /// 特征值类型集合
        /// </summary>
        public List<CommonInfo> EigenInfo { get; set; }
    }
}
