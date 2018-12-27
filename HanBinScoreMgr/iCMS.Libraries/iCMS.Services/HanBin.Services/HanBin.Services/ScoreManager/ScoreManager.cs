using HanBin.Core.DB.Models;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Request.HanBin.ScoreManager;
using iCMS.Frameworks.Core.Repository;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Services.ScoreManager
{
    public class ScoreManager : IScoreManager
    {
        [Dependency]
        public IRepository<ScoreItem> scoreItemRepository { get; set; }

        public BaseResponse<bool> AddScoreItem(AddScoreItemParameter parameter)
        {
            try
            {
                var scoreItem = new ScoreItem();
                scoreItem.ItemID = parameter.ItemScore;
                scoreItem.ItemDescription = parameter.ItemDescription;

            }
            catch (Exception e)
            {

                throw;
            }
            return null;
        }
    }
}
