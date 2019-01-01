using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Common.Component.Tool
{
    public class MSMQHelper
    {
        private MessageQueue mq = null;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path"></param>
        public MSMQHelper(string path)
        {
            //消息队列不存在时，创建一个新的消息队列
            if (MessageQueue.Exists(path) == false)
            {
                MessageQueue.Create(path);
            }

            mq = new MessageQueue(path);
        }
        #endregion

        #region 消息队列入队
        /// <summary>
        /// 消息队列入队
        /// </summary>
        /// <returns></returns>
        public void Enqueu(object content)
        {
            try
            {
                Message message = new Message();
                message.Body = content;
                //张辽阔 2016-12-26 添加
                message.Recoverable = true;
                //发送消息
                mq.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 消息队列出队
        /// <summary>
        /// 消息队列出队
        /// </summary>
        /// <returns></returns>
        public Message Dequeue<T>()
        {
            Message message = null;
            try
            {
                mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
                message = mq.Receive();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
                LogHelper.WriteLog("3.10Log");
                throw new Exception("读取消息队列异常！3月10日 日志"); ;
            }

            return message;
        }
        #endregion

        #region  判断MSMQ中是否存在消息
        public bool IsExistMessage()
        {
            bool result = false;
            if (mq.GetAllMessages().Count() > 0)
            {
                result = true;
            }
            return result;
        }
        #endregion

        #region 清空消息队列
        public void Clear()
        {
            mq.Purge();
        }
        #endregion
    }
}