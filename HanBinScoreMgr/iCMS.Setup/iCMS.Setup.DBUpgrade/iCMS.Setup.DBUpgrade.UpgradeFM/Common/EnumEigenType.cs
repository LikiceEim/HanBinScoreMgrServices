using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Setup.DBUpgrade.UpgradeFM.Common
{
    #region 特征值
    /// <summary>
    /// 特征值类型名称
    /// </summary>
    [Description("特征值类型名称")]
    public enum EnumEigenType
    {


        /// <summary>
        /// 峰值
        /// </summary>
        [Description("峰值")]
        PeakValue = 1,

        /// <summary>
        /// 峰峰值
        /// </summary>
        [Description("峰峰值")]
        PeakPeakValue = 2,

        /// <summary>
        /// 有效值
        /// </summary>
        [Description("有效值")]
        EffectivityValue = 3,

        /// <summary>
        /// 地毯值
        /// </summary>
        [Description("地毯值")]
        CarpetValue = 4,
        /// <summary>
        /// LQ值
        /// </summary>
        [Description("LQ值")]
        LQValue = 5,
    }
    #endregion
}
