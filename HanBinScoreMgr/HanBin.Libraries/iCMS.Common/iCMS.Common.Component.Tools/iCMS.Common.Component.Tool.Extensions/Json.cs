using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace HanBin.Common.Component.Tool.Extensions
{
    public class Json
    {

        #region JSON解析
        /// <summary>
        /// JSON解析
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T Parse<T>(string jsonString)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }
        }
        #endregion

        #region JSON生成
        /// <summary>
        /// JSON生成
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        public static string Stringify(object jsonObject)
        {
            using (var ms = new MemoryStream())
            {
                new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        #endregion
        #region JSON序列化对象
        /// <summary>    
        /// JSON序列化 
        /// </summary>    
        public static string JsonSerializer<T>(T t)
        {
            IsoDateTimeConverter iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(t, iso);

            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            //MemoryStream ms = new MemoryStream();
            //ser.WriteObject(ms, t);
            //string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            //ms.Close();
            ////替换Json的Date字符串    
            //string p = @"///Date/((/d+)/+/d+/)///"; /*////Date/((([/+/-]/d+)|(/d+))[/+/-]/d+/)////*/
            //MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
            //Regex reg = new Regex(p);
            //jsonString = reg.Replace(jsonString, matchEvaluator);
            //return jsonString;
        }
        /// <summary>    
        /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串    
        /// </summary>    
        private static string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }

        #endregion

        #region JSON反序列化
        /// <summary>    
        /// JSON反序列化 
        /// </summary>    
        public static T JsonDeserialize<T>(string jsonString)
        {

            IsoDateTimeConverter iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

            return JsonConvert.DeserializeObject<T>(jsonString, iso);

            ////将"yyyy-MM-dd HH:mm:ss"格式的字符串转为"//Date(1294499956278+0800)//"格式    
            //string p = @"/d{4}-/d{2}-/d{2}/s/d{2}:/d{2}:/d{2}";
            //MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);
            //Regex reg = new Regex(p);
            //jsonString = reg.Replace(jsonString, matchEvaluator);
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            //MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            //T obj = (T)ser.ReadObject(ms);
            //return obj;
        }
        /// <summary>    
        /// 将时间字符串转为Json时间    
        /// </summary>    
        private static string ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("///Date({0}+0800)///", ts.TotalMilliseconds);
            return result;
        }
        #endregion
    }
}