/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 * 命名空间：iCMS.Presentation.Server.Common
 * 文件名：  BaseService
 * 创建人：  王颖辉
 * 创建时间：2016-11-16
 * 描述：基类
 *
 * 修改人：张辽阔
 * 修改时间：2016-12-09
 * 修改记录：迁移基类
/************************************************************************************/

using System;

using Newtonsoft.Json.Linq;

using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Tool;

namespace iCMS.Presentation.Common
{
    #region 基类

    /// <summary>
    /// 基类
    /// </summary>
    public class BaseService
    {
        #region 验证数据是否通过

        /// <summary>
        /// 验证数据是否通过
        /// </summary>
        /// <typeparam name="T">参数类</typeparam>
        /// <param name="json">json</param>
        /// <returns></returns>
        public bool ValidateData<T>(BaseRequest request)
        {
            if (request == null)
                return false;

            bool isPass = false;
            try
            {
                var clientSign = request.Sign;
                var key = request.Key;
                string json = request.ToServerString();
                //替换 key ,sign  
                string sign1 = ",\"Sign\":\"" + clientSign + "\"";
                string sign2 = "\"Sign\":\"" + clientSign + "\",";
                string sign3 = "\"Sign\":\"" + clientSign + "\"";
                string tempJson = json.Replace(sign1, "").Replace(sign2, "").Replace(sign3, "");

                //通过key找到secret
                string secret = Utilitys.GetAppConfig("Secret");
                //排序
                tempJson = JsonSort.SortJson(JToken.Parse(tempJson), null);
                string sign = MD5Helper.GetMD5(tempJson + secret);
                string serverSign = sign;


                //通过计算出来的正确密钥
                string correctSecret = EcanSecurity.GetClientSecret(key);
                //签名正确，密钥正确
                if (clientSign == serverSign && secret == correctSecret)
                {
                    isPass = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }
            return isPass;
        }

        #endregion
    }

    #endregion
}