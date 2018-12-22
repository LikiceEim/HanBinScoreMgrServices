using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace iCMS.Common.Component.Tool.Extensions
{
    class NewtonsoftJsonDispatchFormatter : IDispatchMessageFormatter
    {
        OperationDescription operation;
        Dictionary<string, int> parameterNames;
        public NewtonsoftJsonDispatchFormatter(OperationDescription operation, bool isRequest)
        {
            this.operation = operation;
            if (isRequest)
            {
                int operationParameterCount = operation.Messages[0].Body.Parts.Count;
                if (operationParameterCount > 1)
                {
                    this.parameterNames = new Dictionary<string, int>();
                    for (int i = 0; i < operationParameterCount; i++)
                    {
                        this.parameterNames.Add(operation.Messages[0].Body.Parts[i].Name, i);
                    }
                }
            }
        }

        public void DeserializeRequest(Message message, object[] parameters)
        {
            //如果是上传的content-type,则不作json处理
            var headers = ((HttpRequestMessageProperty)(message.Properties[HttpRequestMessageProperty.Name])).Headers;
            string contenttype = headers["Content-type"];
            string contentLength = headers["Content-Length"];
            if (contenttype != null && contenttype.StartsWith("multipart/form-data"))
            {
                //获得附件分割边界字符串
                string boundary = contenttype.Substring(contenttype.IndexOf("boundary=") + "boundary=".Length);
                int len = int.Parse(contentLength);
                //获得方法的参数
                var paramts = operation.SyncMethod.GetParameters();
                int streamtypeIndx = -1;
                int bodyheaderlen = 0;
                //找到Stream类型的参数
                for (streamtypeIndx = 0; streamtypeIndx < paramts.Length && streamtypeIndx < parameters.Length; streamtypeIndx++)
                {
                    if (paramts[streamtypeIndx].ParameterType == typeof(Stream))
                    {
                        var stream = message.GetBody<Stream>();
                        //定位到第一个0D0A0D0A)
                        //sb= new StringBuilder(512);
                        int datimes = 0;//回车换行次数
                        int c = 0;
                        while (datimes != 4)
                        {
                            c = stream.ReadByte();
                            if (c == -1)
                                break;
                            if (c == 0x0d && (datimes == 0 || datimes == 2))
                            {
                                datimes++;
                            }
                            else if (c == 0x0a && (datimes == 1 || datimes == 3))
                            {
                                datimes++;
                            }
                            else
                                datimes = 0;

                            bodyheaderlen++;
                        }
                        if (c == -1)
                            continue;
                        //计算实际附件大小
                        int fileLength = len - bodyheaderlen - boundary.Length - 6;
                        int remain = fileLength;
                        MemoryStream filestream = new MemoryStream(fileLength);
                        byte[] buffer = new byte[8192];
                        int readed = 0;
                        while (remain > 0)
                        {
                            readed = stream.Read(buffer, 0, remain > 8192 ? 8192 : remain);
                            remain -= readed;
                            filestream.Write(buffer, 0, readed);
                        }
                        stream.Close();
                        filestream.Seek(0, SeekOrigin.Begin);
                        //MemoryStream stream = new MemoryStream(message.GetReaderAtBodyContents().ReadElementContentAsBinHex());
                        parameters[streamtypeIndx] = filestream;
                    }
                    else
                    {
                        parameters[streamtypeIndx] = headers[paramts[streamtypeIndx].Name];
                    }
                }

                return;
            }

            object bodyFormatProperty;
            if (!message.Properties.TryGetValue(WebBodyFormatMessageProperty.Name, out bodyFormatProperty) ||
                (bodyFormatProperty as WebBodyFormatMessageProperty).Format != WebContentFormat.Raw)
            {
                throw new InvalidOperationException("Incoming messages must have a body format of Raw. Is a ContentTypeMapper set on the WebHttpBinding?");
            }

            XmlDictionaryReader bodyReader = message.GetReaderAtBodyContents();
            //begin 张辽阔 2017-01-06 添加
            bodyReader.Quotas.MaxArrayLength = 163840 * 2;
            //end 张辽阔 2017-01-06 添加
            bodyReader.ReadStartElement("Binary");
            byte[] rawBody = bodyReader.ReadContentAsBase64();
            MemoryStream ms = new MemoryStream(rawBody);

            StreamReader sr = new StreamReader(ms);
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer()
            {
                DateFormatString = "yyyy-MM-dd HH:mm:ss"
            };
            if (parameters.Length == 1)
            {
                // single parameter, assuming bare
                parameters[0] = serializer.Deserialize(sr, operation.Messages[0].Body.Parts[0].Type);
            }
            else
            {
                // multiple parameter, needs to be wrapped
                Newtonsoft.Json.JsonReader reader = new Newtonsoft.Json.JsonTextReader(sr);
                reader.Read();
                if (reader.TokenType != Newtonsoft.Json.JsonToken.StartObject)
                {
                    throw new InvalidOperationException("Input needs to be wrapped in an object");
                }

                reader.Read();
                while (reader.TokenType == Newtonsoft.Json.JsonToken.PropertyName)
                {
                    string parameterName = reader.Value as string;
                    reader.Read();
                    if (this.parameterNames.ContainsKey(parameterName))
                    {
                        int parameterIndex = this.parameterNames[parameterName];
                        parameters[parameterIndex] = serializer.Deserialize(reader, this.operation.Messages[0].Body.Parts[parameterIndex].Type);
                    }
                    else
                    {
                        reader.Skip();
                    }

                    reader.Read();
                }

                reader.Close();
            }

            sr.Close();
            ms.Close();
        }

        public Message SerializeReply(MessageVersion messageVersion, object[] parameters, object result)
        {
            byte[] body;
            Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(ms, Encoding.UTF8))
                {
                    using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw) { DateFormatString = "yyyy-MM-dd HH:mm:ss" })
                    {
                        //writer.Formatting = Newtonsoft.Json.Formatting.Indented;
                        serializer.Serialize(writer, result);
                        sw.Flush();
                        body = ms.ToArray();
                    }
                }
            }

            Message replyMessage = Message.CreateMessage(messageVersion, operation.Messages[1].Action, new RawBodyWriter(body));
            replyMessage.Properties.Add(WebBodyFormatMessageProperty.Name, new WebBodyFormatMessageProperty(WebContentFormat.Raw));
            HttpResponseMessageProperty respProp = new HttpResponseMessageProperty();
            respProp.Headers[HttpResponseHeader.ContentType] = "application/json";
            replyMessage.Properties.Add(HttpResponseMessageProperty.Name, respProp);
            return replyMessage;
        }
    }
}