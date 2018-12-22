/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Service.Common
 *文件名：  Paging
 *创建人：  王颖辉
 *创建时间：2016-11-23
 *描述：存储过程分页
/************************************************************************************/
using System.Data.SqlClient;
using iCMS.Common.Component.Data.Base;
using iCMS.Common.Component.Data.Enum;
using iCMS.Frameworks.Core.DB;
using iCMS.Frameworks.Core.DB.Models;
using System.Collections.Generic;
using iCMS.Frameworks.Core.Repository;
using System.Data;
using System;

namespace iCMS.Service.Common
{
    #region 存储过程分页
    /// <summary>
    /// 存储过程分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Paging<T>: IPaging<T> where T : class
    {
        #region 变量
        private readonly IRepository<Device> deviceRepository;
        #endregion

        #region 构造函数
        public Paging(IRepository<Device> deviceRepository)
        {
            this.deviceRepository = deviceRepository;
        }
        #endregion

        #region 分页方法
        /// <summary>
        /// 分页方法
        /// </summary>
        /// <param name="parameter">分布参数</param>
        /// <returns></returns>
        public IList<T> GetPaging(PagingParameter parameter)
        {
            SqlParameter[] parameter1 = new[]{
                new SqlParameter("@Tables",parameter.TableName),
                new SqlParameter("@PK",parameter.PrimaryKey),
                new SqlParameter("@Sort",parameter.Sort),
                new SqlParameter("@PageNumber",parameter.PageIndex),
                new SqlParameter("@PageSize",parameter.PageSize),
                new SqlParameter("@Fields",parameter.Fields),
                new SqlParameter("@Filter",parameter.Filter),
                new SqlParameter("@Group",parameter.Group),
                new SqlParameter("@IsCount",parameter.isCount),
                new SqlParameter("@DataCount",parameter.RecordTotal),
            };
            parameter1[9].Direction = ParameterDirection.Output;　　//参数类型为Output
            OperationResult operationResult = deviceRepository
                  .SqlQuery<T>("EXEC SP_Paging @Tables,@PK,@Sort,@PageNumber,@PageSize,@Fields,@Filter,@Group,@IsCount,@DataCount = @DataCount OUTPUT", parameter1);
            if (operationResult.ResultType == EnumOperationResultType.Success)
            {
                parameter.RecordTotal = Convert.ToInt32(parameter1[9].Value) ;
                return (IList<T>)operationResult.AppendData;
            }
            return default(IList<T>);
        }
        #endregion
    }
    #endregion

    #region 分页参数
    /// <summary>
    /// 分页参数
    /// </summary>
    public class PagingParameter
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get;
            set;
        }

        /// <summary>
        /// 主键
        /// </summary>
        public string PrimaryKey
        {
            get;
            set;
        }

        /// <summary>
        /// 排序字段 
        /// </summary>
        public string Sort
        {
            get;
            set;
        }

        /// <summary>
        /// 开始页码 
        /// </summary>
        public int PageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 页大小 
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }


        /// <summary>
        /// 读取字段，默认为*,可为空 
        /// </summary>
        public string Fields
        {
            get;
            set;
        }

        /// <summary>
        /// 条件 ,可空
        /// </summary>
        public string Filter
        {
            get;
            set;
        }

        /// <summary>
        /// 分组 ,可空
        /// </summary>
        public string Group
        {
            get;
            set;
        }

        /// <summary>
        /// 是否获得总记录数
        /// </summary>
        public int isCount
        {
            get;
            set;
        }

        /// <summary>
        /// 记录总数
        /// </summary>
        public int RecordTotal
        {
            get;
            set;
        }
    }
    #endregion
}
