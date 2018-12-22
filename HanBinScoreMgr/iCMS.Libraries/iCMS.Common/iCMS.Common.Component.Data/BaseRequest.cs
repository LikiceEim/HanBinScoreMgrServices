using System;

namespace iCMS.Common.Component.Data
{
    #region 请求基类
    /// <summary>
    /// 请求基类
    /// </summary>
    public class BaseRequest
    {
        private string key = "123456789";
        private string secret = "987654321";
        /// <summary>
        /// key
        /// </summary>
        //[DataMember]
        public string Key
        {
            get
            {
                return key;
            }
        }

        /// <summary>
        /// 签名
        /// </summary>
        // [DataMember]
        public string Sign
        {
            get;
            set;
        }

        public override string ToString()
        {
            var json = JsonConvert.SerializeObject(this);
            //替换 key ,sign  
            string sign1 = ",\"Sign\":null";
            string sign2 = "\"Sign\":null,";
            string sign3 = "\"Sign\":null";
            string tempJson = json.Replace(sign1, "").Replace(sign2, "").Replace(sign3, "");
            tempJson = JsonSort.SortJson(JToken.Parse(tempJson), null);
            string sign = MD5Helper.GetMD5(tempJson + secret);
            this.Sign = sign;
            json = JsonConvert.SerializeObject(this);
            return json;
        }
    }
    #endregion
}
