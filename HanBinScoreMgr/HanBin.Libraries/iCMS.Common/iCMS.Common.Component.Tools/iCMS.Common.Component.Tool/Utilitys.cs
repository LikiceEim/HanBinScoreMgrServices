using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Tool
{
    /// <summary>
    /// 公用方法
    /// </summary>
    public static class Utilitys
    {
        /// <summary>
        /// 获取配置节点数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppConfig(string key)
        {
            //张辽阔 2016-12-23 注释
            //return System.Configuration.ConfigurationManager.AppSettings[key].ToString();
            //张辽阔 2016-12-23 修改
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

        public static string GetAppConfigForExe(string key)
        {
            //Modified by QXM, 2017/02/21
            //做手动推送时候，为了时修改的App.config文件能够立即起效 而不用重新启动配置文件，故而做此修改
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            KeyValueConfigurationElement connectElement = config.AppSettings.Settings["iCMS"];
            if (null == connectElement)
            {
                return string.Empty;
            }
            return connectElement.Value;
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-08-19
        /// 创建记录：验证字符串是否等于空
        /// </summary>
        /// <param name="source">要验证的字符串</param>
        /// <returns></returns>
        public static bool ValidateStringEmpty(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        /// <summary>
        /// 创建人：张辽阔
        /// 创建时间：2016-08-19
        /// 创建记录：验证字符串是否等于空或者等于0
        /// </summary>
        /// <param name="source">要验证的字符串</param>
        /// <returns></returns>
        public static bool ValidateStringEmptyOrZero(this string source)
        {
            return string.IsNullOrWhiteSpace(source) || source.Trim() == "0";
        }


        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2017-02-24
        /// 创建记录：验证MAC地址
        /// </summary>
        /// <param name="source">验证字符串</param>
        /// <returns></returns>
        public static bool ValidateMAC(this string source)
        {
            return Regex.IsMatch(source.Trim(), @"^[A-Fa-f0-9]{2}[A-Fa-f0-9]{2}[A-Fa-f0-9]{2}[A-Fa-f0-9]{2}[A-Fa-f0-9]{2}[A-Fa-f0-9]{2}[A-Fa-f0-9]{2}[A-Fa-f0-9]{2}$");
        }

        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2017-02-24
        /// 创建记录：验证电话号码
        /// </summary>
        /// <param name="source">验证电话号码</param>
        /// <returns></returns>
        public static bool ValidatePhone(this string source)
        {

            var regex1 = @"^(1\d{10})$";
            // var rex=/^(([0\+]\d{2,3}-)?(0\d{2,3})-)(\d{7,8})(-(\d{3,}))?$/;
            //区号：前面一个0，后面跟2-3位数字 ： 0\d{2,3}
            //电话号码：7-8位数字： \d{7,8
            //分机号：一般都是3位数字： \d{3,}
            //这样连接起来就是验证电话的正则表达式了：/^((0\d{2,3})-)(\d{7,8})(-(\d{3,}))?$/		 
            var regex2 = @"^((0\d{2,3})-)(\d{7,8})(-(\d{3,}))?$";

            //1、可以是1开头的11位数字（手机号）
            //2、可以是"区号 -电话号-分机号"或者是"(区号)电话号-分机号"格式
            //3、区号是0开头的3～4位数字，可以没有区号
            //4、电话号是5～8位数字，不能以0开头
            //5、分机号是1～8位数字，可以没有分机号

            //合法数据示例：
            //13812341234
            //010-12345678
            //(0432)1234567-1234
            var regex3 = @"^1\d{10}$|^(0\d{2,3}-?|\(0\d{ 2,3}\))?[1-9]\d{4,7}(-\d{1,8})?$";
            // return Regex.IsMatch(source, @"^(\d{3,4}-)?\d{6,8}$");
            return Regex.IsMatch(source.Trim(), regex1) || Regex.IsMatch(source.Trim(), regex2) || Regex.IsMatch(source.Trim(), regex3);
        }

        /// <summary>
        /// 创建人：王颖辉
        /// 创建时间：2017-02-24
        /// 创建记录：验证电话号码
        /// </summary>
        /// <param name="source">验证电话号码</param>
        /// <returns></returns>
        public static bool ValidateLength(this string source, int maxLength)
        {
            return source.Trim().Length <= maxLength;
        }


        /// <summary>
        /// 验证身份证号码
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool CheckIDCard(string Id)
        {
            if (Id.Length == 18)
            {
                bool check = CheckIDCard18(Id);
                return check;
            }
            else if (Id.Length == 15)
            {
                bool check = CheckIDCard15(Id);
                return check;
            }
            else
            {
                return false;
            }
        }

        private static bool CheckIDCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return true;//移除校验码校验，过于严格
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }
        private static bool CheckIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;//符合15位身份证标准
        }
        /// <summary>
        /// 根据身份证号获取生日
        /// </summary>
        /// <param name="IdCard"></param>
        /// <returns></returns>
        public static string GetBrithdayFromIdCard(string IdCard)
        {
            string rtn = "1900-01-01";
            if (IdCard.Length == 15)
            {
                rtn = IdCard.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            }
            else if (IdCard.Length == 18)
            {
                rtn = IdCard.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            }
            return rtn;
        }
        /// <summary>
        /// 根据身份证获取性别
        /// </summary>
        /// <param name="IdCard"></param>
        /// <returns></returns>
        public static string GetSexFromIdCard(string IdCard)
        {
            string rtn;
            string tmp = "";
            if (IdCard.Length == 15)
            {
                tmp = IdCard.Substring(IdCard.Length - 3);
            }
            else if (IdCard.Length == 18)
            {
                tmp = IdCard.Substring(IdCard.Length - 4);
                tmp = tmp.Substring(0, 3);
            }
            int sx = int.Parse(tmp);
            int outNum;
            Math.DivRem(sx, 2, out outNum);
            if (outNum == 0)
            {
                rtn = "女";
            }
            else
            {
                rtn = "男";
            }
            return rtn;
        }

        public static string DecodeBase64(string code_type, string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = Encoding.GetEncoding(code_type).GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }
    }
}