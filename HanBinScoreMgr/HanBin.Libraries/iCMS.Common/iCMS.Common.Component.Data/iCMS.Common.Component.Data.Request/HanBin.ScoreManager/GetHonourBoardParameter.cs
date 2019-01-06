using HanBin.Common.Component.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Data.Request.HanBin.ScoreManager
{
    /// <summary>
    /// 获取红榜数据请求参数
    /// </summary>
    public class GetHonourBoardParameter : BaseRequest
    {
        /// <summary>
        /// 前N名
        /// </summary>
        public int RankNumber { get; set; }
    }

    public class GetBlackBoardParameter : BaseRequest
    {
        public int RankNumber { get; set; }
    }
}
