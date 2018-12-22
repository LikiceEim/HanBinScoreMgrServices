using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace iCMS.Common.Component.Tool
{
    public class RestClient
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="baseUrl"></param>
        public RestClient(string baseUri)
        {
            this.BaseUri = baseUri;
        }

        /// <summary>
        /// 基地址
        /// </summary>
        private string BaseUri;

        /// <summary>
        /// Post调用
        /// </summary>
        /// <param name="data"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public string Post(string data, string method)
        {
            HttpWebRequest myRequest = null;
            Stream newStream = null;
            string returnValue = string.Empty;
            string serviceUrl = string.Empty;
            try
            {
                //Web访问对象
                serviceUrl = string.Format("{0}/{1}", this.BaseUri, method);
                myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);

                //转成网络流
                byte[] buf = UnicodeEncoding.UTF8.GetBytes(data);

                //设置
                myRequest.Method = "POST";
                myRequest.ContentLength = buf.Length;
                myRequest.ContentType = "application/json";

                // 发送请求
                newStream = myRequest.GetRequestStream();
                newStream.Write(buf, 0, buf.Length);
                newStream.Close();

                // 获得接口返回值
                using (HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse())
                {
                    StreamReader reader = null;
                    try
                    {
                        reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                        returnValue = reader.ReadToEnd();
                        //  string returnValue = HttpUtility.HtmlDecode(reader.ReadToEnd());

                        reader.Close();
                        myResponse.Close();
                    }
                    catch(Exception ex)
                    {                     
                        LogHelper.WriteLog(ex);
                    }
                    finally
                    {
                        if (reader != null)
                            reader.Close();
                        if (myResponse != null)
                            myResponse.Close();
                    }

                    return returnValue;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("请求地址：" + serviceUrl);
                LogHelper.WriteLog(ex);
            
                //if (myRequest != null)
                //    myRequest.;
            }
            finally
            {
                if (newStream != null)
                    newStream.Close();

                if (myRequest != null)
                    myRequest.Abort();
            }

            return serviceUrl;
        }

        /// <summary>
        /// Post调用
        /// </summary>
        /// <param name="data"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public string WebPost(string data, string method)
        {
            HttpWebRequest myRequest = null;
            Stream newStream = null;
            string returnValue = string.Empty;
            string serviceUrl = string.Empty;
            try
            {
                //Web访问对象
                serviceUrl = string.Format("{0}/{1}", this.BaseUri, method);
                myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);

                //转成网络流
                byte[] buf = UnicodeEncoding.UTF8.GetBytes(data);

                //设置
                myRequest.Method = "POST";
                myRequest.ContentLength = buf.Length;
                myRequest.ContentType = "application/json";

                // 发送请求
                newStream = myRequest.GetRequestStream();
                newStream.Write(buf, 0, buf.Length);
                newStream.Close();

                // 获得接口返回值
                using (HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse())
                {
                    StreamReader reader = null;
                    try
                    {
                        reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                        returnValue = reader.ReadToEnd();
                        //  string returnValue = HttpUtility.HtmlDecode(reader.ReadToEnd());

                        reader.Close();
                        myResponse.Close();
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog("请求地址：" + serviceUrl);
                        LogHelper.WriteLog(ex);
                    }
                    finally
                    {
                        if (reader != null)
                            reader.Close();
                        if (myResponse != null)
                            myResponse.Close();
                    }

                    return returnValue;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("请求地址：" + serviceUrl);
                LogHelper.WriteLog(ex);

                //if (myRequest != null)
                //    myRequest.;
            }
            finally
            {
                if (newStream != null)
                    newStream.Close();

                if (myRequest != null)
                    myRequest.Abort();
            }

            return "qwertyuiop,"+serviceUrl;
        }

        /// <summary>
        /// Post调用
        /// </summary>
        /// <param name="data"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public string Post(string data)
        {
            HttpWebRequest myRequest = null;
            Stream newStream = null;
            string returnValue = string.Empty;
            string serviceUrl = string.Empty;
            try
            {
                //Web访问对象
                serviceUrl = string.Format("{0}", this.BaseUri);
                myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);

                //转成网络流
                byte[] buf = UnicodeEncoding.UTF8.GetBytes(data);

                //设置
                myRequest.Method = "POST";
                myRequest.ContentLength = buf.Length;
                myRequest.ContentType = "application/json";

                // 发送请求
                newStream = myRequest.GetRequestStream();
                newStream.Write(buf, 0, buf.Length);
                newStream.Close();

                // 获得接口返回值
                using (HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse())
                {
                    StreamReader reader = null;
                    try
                    {
                        reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                        returnValue = reader.ReadToEnd();
                        //  string returnValue = HttpUtility.HtmlDecode(reader.ReadToEnd());

                        reader.Close();
                        myResponse.Close();
                    }
                    catch
                    {
                        if (reader != null)
                            reader.Close();
                        if (myResponse != null)
                            myResponse.Close();
                    }

                    return returnValue;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                if (newStream != null)
                    newStream.Close();
                if (myRequest != null)
                    myRequest.Abort();

                return serviceUrl;
            }


        }

        /// <summary>
        /// Post调用重载
        /// </summary>
        /// <param name="data"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public string Post(string data, string method,int timeout)
        {
            HttpWebRequest myRequest = null;
            Stream newStream = null;
            string returnValue = string.Empty;
            string serviceUrl = string.Empty;
            try
            {
                //Web访问对象
                serviceUrl = string.Format("{0}/{1}", this.BaseUri, method);
                myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);

                //转成网络流
                byte[] buf = UnicodeEncoding.UTF8.GetBytes(data);

                //设置
                myRequest.Method = "POST";
                myRequest.ContentLength = buf.Length;
                myRequest.ContentType = "application/json";
                myRequest.Timeout = timeout;

                // 发送请求
                newStream = myRequest.GetRequestStream();
                newStream.Write(buf, 0, buf.Length);
                newStream.Close();

                // 获得接口返回值
                using (HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse())
                {
                    StreamReader reader = null;
                    try
                    {
                        reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                        returnValue = reader.ReadToEnd();
                        reader.Close();
                        myResponse.Close();
                    }
                    catch
                    {
                        if (reader != null)
                            reader.Close();
                        if (myResponse != null)
                            myResponse.Close();
                    }

                    return returnValue;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("请求地址："+serviceUrl);
                LogHelper.WriteLog(ex);
                if (newStream != null)
                    newStream.Close();
                if (myRequest != null)
                    myRequest.Abort();

                return serviceUrl;
            }


        }

        #region 同步通过POST方式发送数据
        /// <summary>
        /// 通过POST方式发送数据
        /// </summary>
        /// <param name="Url">url</param>
        /// <param name="postDataStr">Post数据</param>
        /// <param name="cookie">Cookie容器</param>
        /// <returns></returns>
        public string Post(string postDataStr, ref CookieContainer cookie)
        {
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.BaseUri);


            //request.Method = "POST";
            //request.ContentType = "application/json";
            //// request.ContentLength = postDataStr.Length;
            ////  request.ContentType = "application/x-www-form-urlencoded";
            //request.Referer = "http://localhost";
            //  Stream myRequestStream = request.GetRequestStream();
            //StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
            //myStreamWriter.Write(postDataStr);
            //myStreamWriter.Close();

            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //Stream myResponseStream = response.GetResponseStream();
            //StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            //string retString = myStreamReader.ReadToEnd();
            //myStreamReader.Close();
            //myResponseStream.Close();

            ////  CookieContainer CookieContainer = new CookieContainer();
            ////response.Cookies.

            //cookie = new CookieContainer();
            //using (HttpWebResponse wr = (HttpWebResponse)request.GetResponse())
            //{
            //    cookie.Add(wr.Cookies);
            //}

            //return retString;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("POST请求的地址");
            request.CookieContainer = new CookieContainer();
          //  CookieContainer cookie = request.CookieContainer;//如果用不到Cookie，删去即可  
                                                             //以下是发送的http头，随便加，其中referer挺重要的，有些网站会根据这个来反盗链  
            //request.Referer = “http://localhost/”;  
            request.Accept = "Accept:text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.Headers["Accept-Language"] = "zh-CN,zh;q=0.";
            request.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";
            request.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1";
            request.KeepAlive = true;
            //上面的http头看情况而定，但是下面俩必须加  
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";

            Encoding encoding = Encoding.UTF8;//根据网站的编码自定义  
            byte[] postData = encoding.GetBytes(postDataStr);//postDataStr即为发送的数据，格式还是和上次说的一样  
            request.ContentLength = postData.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(postData, 0, postData.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            //如果http头中接受gzip的话，这里就要判断是否为有压缩，有的话，直接解压缩即可  
            if (response.Headers["Content-Encoding"] != null && response.Headers["Content-Encoding"].ToLower().Contains("gzip"))
            {
                responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
            }

            StreamReader streamReader = new StreamReader(responseStream, encoding);
            string retString = streamReader.ReadToEnd();

            streamReader.Close();
            responseStream.Close();

            return retString;
        }
        #endregion

        /// <summary>
        /// Get调用
        /// </summary>
        /// <param name="method">方法名</param>
        /// <param name="json">json数据</param>
        /// <returns></returns>
        public string Get(string method, string json)
        {
            string returnValue = string.Empty;
            HttpWebRequest myRequest = null;
            HttpWebResponse myResponse = null;
            StreamReader reader = null;
            try
            {
                //Web访问对象
                string serviceUrl = string.Format("{0}/{1}/{2}", this.BaseUri, method, json);
                myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);

                // 获得接口返回值
                myResponse = (HttpWebResponse)myRequest.GetResponse();
                reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                returnValue = HttpUtility.UrlDecode(reader.ReadToEnd());
                reader.Close();
                myResponse.Close();

                return returnValue;
            }
            catch
            {
                if (myRequest != null)
                    myRequest.Abort();
                if (reader != null)
                    reader.Close();
                if (myResponse != null)
                    myResponse.Close();

                return returnValue;
            }
        }

        /// <summary>
        /// Get调用
        /// </summary>
        /// <param name="method">方法名</param>
        /// <param name="json">json数据</param>
        /// <returns></returns>
        public string Get()
        {
            string returnValue = string.Empty;
            HttpWebRequest myRequest = null;
            HttpWebResponse myResponse = null;
            StreamReader reader = null;
            try
            {
                //Web访问对象
                string serviceUrl = string.Format("{0}", this.BaseUri);
                myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);

                // 获得接口返回值
                myResponse = (HttpWebResponse)myRequest.GetResponse();
                reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                returnValue = HttpUtility.UrlDecode(reader.ReadToEnd());
                reader.Close();
                myResponse.Close();

                return returnValue;
            }
            catch(Exception ex)
            {
                LogHelper.WriteLog(ex);
                if (myRequest != null)
                    myRequest.Abort();
                if (reader != null)
                    reader.Close();
                if (myResponse != null)
                    myResponse.Close();

                return returnValue;
            }
        }

        #region 同步通过GET方式发送数据
        /// <summary>
        /// 通过GET方式发送数据
        /// </summary>
        /// <param name="Url">url</param>
        /// <param name="postDataStr">GET数据</param>
        /// <param name="cookie">GET容器</param>
        /// <returns></returns>
        public string Get(string Url, string postDataStr, ref CookieContainer cookie)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            if (cookie.Count == 0)
            {
                request.CookieContainer = new CookieContainer();
                cookie = request.CookieContainer;
            }
            else
            {
                request.CookieContainer = cookie;
            }

            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
        #endregion

    }
}
