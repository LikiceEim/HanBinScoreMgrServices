


using iCMS.Common.Component.Data.Base.DB;
using iCMS.Common.Component.Tool;
using iCMS.Frameworks.Core.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Frameworks.Core.DB
{
    public static class YunyiThreadHelper
    {
        /// <summary>
        /// 云翼 云平台添加
        /// </summary>
        /// <param name="entity"></param>
        public static void YunyiAdd(object entity)
        {
            try
            {
                EntityBase entityBase = entity as EntityBase;
                if (entityBase != null)
                {
                    //BaseCloudService cloud = BaseCloudService.GetInstance();
                    //cloud.SyncInfo(entityBase, EnumCloudDataOperation.Add);
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog(e);
            }
        }
        /// <summary>
        /// 云翼 云平台修改
        /// </summary>
        /// <param name="entity"></param>
        public static void YunyiUpdate(object entity)
        {
            try
            {
                EntityBase entityBase = entity as EntityBase;
                if (entityBase != null)
                {
                    //BaseCloudService cloud = BaseCloudService.GetInstance();
                    //cloud.SyncInfo(entityBase, EnumCloudDataOperation.Update);
                }
            }
            catch(Exception e)
            {
                LogHelper.WriteLog(e);
            }
        }

        /// <summary>
        /// 云翼 云平台删除
        /// </summary>
        /// <param name="entity"></param>
        public static void YunyiDelete(object entity)
        {
            try
            {
                EntityBase entityBase = entity as EntityBase;
                if (entityBase != null)
                {
                    //BaseCloudService cloud = BaseCloudService.GetInstance();
                    //cloud.SyncInfo(entityBase, EnumCloudDataOperation.Delete);
                }
            }
            catch(Exception e)
            {
                LogHelper.WriteLog(e);
            }
        }
    }
}
