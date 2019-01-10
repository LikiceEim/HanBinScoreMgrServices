/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Base
 *文件名：  BaseRequest
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：请求基类
/************************************************************************************/

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.ServiceModel;

namespace HanBin.Common.Component.Data.Base
{
    #region 请求基类
    /// <summary>
    /// 请求基类
    /// </summary>
    [Serializable]
    public class BaseRequest
    {

        public string Token { get; set; }
        //private string key = Utilitys.GetAppConfig("Key");
        //private string secret = Utilitys.GetAppConfig("Secret");

        ///// <summary>
        ///// key
        ///// </summary>
        //public string Key
        //{
        //    get;
        //    set;
        //}

        ///// <summary>
        ///// 签名
        ///// </summary>
        //public string Sign
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 客户端转换为字符串
        /// </summary>
        /// <returns></returns>
        public string ToClientString()
        {
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

            //this.Key = key;//客户端Key

            //var json = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.None, timeFormat);
            ////替换 key ,sign  
            //string sign1 = ",\"Sign\":null";
            //string sign2 = "\"Sign\":null,";
            //string sign3 = "\"Sign\":null";
            //string tempJson = json.Replace(sign1, "").Replace(sign2, "").Replace(sign3, "");
            //tempJson = JsonSort.SortJson(JToken.Parse(tempJson), null);
            //string sign = MD5Helper.GetMD5(tempJson + secret);
            //this.Sign = sign;

            //timeFormat = new IsoDateTimeConverter();
            //timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            var json = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.None, timeFormat);
            return json;
        }

        /// <summary>
        /// 服务端转换为字符串
        /// </summary>
        /// <returns></returns>
        public string ToServerString()
        {
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            var json = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.None, timeFormat);
            return json;
        }
    }
    #endregion
}