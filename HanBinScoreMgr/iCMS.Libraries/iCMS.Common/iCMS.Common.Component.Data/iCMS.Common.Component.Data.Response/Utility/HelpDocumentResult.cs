using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Utility
{
    public class MoudleContentListResult
    {
        public List<MoudleContent> MoudleContentList { get; set; }
    }

    public class MoudleContent
    {
        /// <summary>
        /// id
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public int PID { get; set; }

        /// <summary>
        /// 功能介绍Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否显示  add by lwj---2018.05.03
        /// </summary>
        public bool IsShow { get; set; }
    }

    public class MoudleContentDetailResult
    {
        /// <summary>
        /// id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public int PID { get; set; }

        /// <summary>
        /// 功能介绍Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 功能介绍
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否显示  add by lwj---2018.05.03
        /// </summary>
        public bool IsShow { get; set; }
    }
}
