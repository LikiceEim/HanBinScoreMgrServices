using System;

namespace iCMS.Common.Component.Data
{
    #region 返回基类
    /// <summary>
    /// 返回基类
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// 当前操作是否成功
        /// </summary>
        public bool IsSuccessfull
        {
            get;

            set;
        }


        /// <summary>返回结果描述信息，当isSuccessfull为True此字段为空串  null
        /// </summary>
        public string Reason
        {
            get;
            set;
        }

        /// <summary>返回结果操作代码，当isSuccessfull为True此字段为空串 null
        /// </summary>
        public string Code
        {
            get;
            set;
        }


        /// <summary>
        /// 返回当前操作数据信息，例如影响数据库的ID、查询结果实体对象等（或前端要求编号） null
        /// </summary>
        public Object Result
        {
            get;
            set;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    #endregion
}
