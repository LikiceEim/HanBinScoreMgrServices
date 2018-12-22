using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using iCMS.Common.Component.Data.Base.DB;

namespace iCMS.Frameworks.Core.DB.Models
{
    [Table("T_SYS_SpeedSamplingMDF")]
    public class SpeedSamplingMDF : EntityBase
    {
        #region Model

        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int SpeedMDFID { get; set; }

        /// <summary>
        /// 测点ID
        /// </summary>
        public int MSiteID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float PulsNumPerP { get; set; }

        #endregion
    }
}