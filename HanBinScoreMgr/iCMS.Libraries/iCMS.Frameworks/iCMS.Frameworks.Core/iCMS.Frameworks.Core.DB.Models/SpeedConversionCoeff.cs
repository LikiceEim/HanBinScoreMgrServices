using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Models
{
    [Table("T_SYS_SpeedConversionCoeff")]
    public class SpeedConversionCoeff : EntityBase
    {
        #region Model

        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SpeedMDFID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float LowSpdTermnalPulsNumPerCircle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float TransmissionRatio { get; set; }

        #endregion
    }
}