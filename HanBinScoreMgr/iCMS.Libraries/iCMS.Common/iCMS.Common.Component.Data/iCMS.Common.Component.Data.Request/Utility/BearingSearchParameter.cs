/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Utility
 *文件名：  BearingSearchParameter
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：轴承库
/************************************************************************************/
using iCMS.Common.Component.Data.Base;
using System.Collections.Generic;

namespace iCMS.Common.Component.Data.Request.Utility
{
    #region 轴承库信息
    #region 轴承库搜索参数
    /// <summary>
    /// 轴承库搜索参数
    /// </summary>
    public class BearingSearchParameter : BaseRequest
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UID { get; set; }
        /// <summary>
        /// 用户钥匙
        /// </summary>
        public string UPW { get; set; }
        /// <summary>
        /// 厂商名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 厂商编号
        /// </summary>
        public string FactoryID { get; set; }

        /// <summary>
        /// 轴承型号
        /// </summary>
        public string BearingNum { get; set; }
        /// <summary>
        ///   轴承描述
        /// </summary>
        public string BearingDescribe { get; set; }
        /// <summary>
        /// 查询类型 0：精确查询；1：模糊查询
        /// </summary>
        public int SearchType { get; set; }
        /// <summary>
        /// 当前请求的控件编号
        /// </summary>
        public string ChartID { get; set; }
        /// <summary>
        /// 显示数据条数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; }
        /// <summary>
        /// 顺序 desc/asc
        /// </summary>
        public string Order { get; set; }
    }
    #endregion

    #region 获取工厂信息
    /// <summary>
    /// 获取工厂信息
    /// </summary>
    public class GetFactoriesParameter : BaseRequest
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UID { get; set; }
        /// <summary>
        /// 用户钥匙
        /// </summary>
        public string UPW { get; set; }
        /// <summary>
        /// 厂商名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 厂商编号
        /// </summary>
        public string FactoryID { get; set; }
        /// <summary>
        /// 当前请求的控件编号
        /// </summary>
        public string ChartID { get; set; }
        /// <summary>
        ///  排序名
        /// </summary>    	
        public string Sort { get; set; }
        /// <summary>
        /// 排序方式，desc/asc
        /// </summary>
        public string Order { get; set; }
        /// <summary>
        /// Int	页数，从1开始,若为-1返回所有的轴承库记录
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// Int	页面行数，从1开始
        /// </summary>
        public int PageSize { get; set; }
    }
    #endregion

    #region 添加轴承振动类型
    /// <summary>
    /// 添加轴承振动类型
    /// </summary>
    public class AddBearinEigenvalueParameter : BaseRequest
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UID { get; set; }
        /// <summary>
        /// 用户钥匙
        /// </summary>
        public string UPW { get; set; }
        /// <summary>
        /// 厂商名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 厂商编号
        /// </summary>
        public string FactoryID { get; set; }
        /// <summary>
        /// 轴承型号
        /// </summary>
        public string BearingNum { get; set; }
        /// <summary>
        /// 轴承描述
        /// </summary>
        public string BearingDescribe { get; set; }
        /// <summary>
        /// 外圈故障特征频率
        /// </summary>
        public float BPFO { get; set; }
        /// <summary>
        /// 内圈故障特征频率
        /// </summary>
        public float BPFI { get; set; }
        /// <summary>
        /// 滚动体故障特征频率
        /// </summary>
        public float BSF { get; set; }
        /// <summary>
        /// 保持架故障特征频率
        /// </summary>
        public float FTF { get; set; }
        /// <summary>
        /// 滚球/柱数
        /// </summary>
        public int? BallsNumber { get; set; }
        /// <summary>
        /// 滚球/柱直径
        /// </summary>
        public float? BallDiameter { get; set; }
        /// <summary>
        /// 节圆直径
        /// </summary>
        public float? PitchDiameter { get; set; }
        /// <summary>
        /// 接触角
        /// </summary>
        public float? ContactAngle { get; set; }
    }
    #endregion

    #region 更新轴承
    /// <summary>
    /// 更新轴承
    /// </summary>
    public class UpdateBearinEigenvalueParameter : BaseRequest
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UID { get; set; }
        /// <summary>
        /// 用户钥匙
        /// </summary>
        public string UPW { get; set; }
        /// <summary>
        /// 轴承特征频率表示ID
        /// </summary>
        public int BearingEigenID { get; set; }
        /// <summary>
        /// 厂商名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 厂商编号
        /// </summary>
        public string FactoryID { get; set; }
        /// <summary>
        /// 轴承型号
        /// </summary>
        public string BearingNum { get; set; }
        /// <summary>
        /// 轴承描述
        /// </summary>
        public string BearingDescribe { get; set; }
        /// <summary>
        /// 外圈故障特征频率
        /// </summary>
        public float BPFO { get; set; }
        /// <summary>
        /// 内圈故障特征频率
        /// </summary>
        public float BPFI { get; set; }
        /// <summary>
        /// 滚动体故障特征频率
        /// </summary>
        public float BSF { get; set; }
        /// <summary>
        /// 保持架故障特征频率
        /// </summary>
        public float FTF { get; set; }
        /// <summary>
        /// 滚球/柱数
        /// </summary>
        public int? BallsNumber { get; set; }
        /// <summary>
        /// 滚球/柱直径
        /// </summary>
        public float? BallDiameter { get; set; }
        /// <summary>
        /// 节圆直径
        /// </summary>
        public float? PitchDiameter { get; set; }
        /// <summary>
        /// 接触角
        /// </summary>
        public float? ContactAngle { get; set; }
    }
    #endregion

    #region 删除轴承
    /// <summary>
    /// 删除轴承
    /// </summary>
    public class DeleteBearinEigenvalueParameter : BaseRequest
    {
        public string UID { get; set; }

        public string UPW { get; set; }

        public List<int> BearingEigenID { get; set; }
    }
    #endregion

    #region 添加工厂
    /// <summary>
    /// 添加工厂
    /// </summary>
    public class AddFactoryParameter : BaseRequest
    {
        public string UID { get; set; }

        public string UPW { get; set; }

        public string FactoryID { get; set; }

        public string FactoryName { get; set; }
    }
    #endregion

    #region 更新工厂
    /// <summary>
    /// 更新工厂
    /// </summary>
    public class UpdateFactoryParameter : BaseRequest
    {
        public string UID { get; set; }

        public string UPW { get; set; }

        public string FactoryID { get; set; }

        public string FactoryName { get; set; }
    }
    #endregion

    #region 删除工厂
    /// <summary>
    /// 删除工厂
    /// </summary>
    public class DeleteFactoryParameter : BaseRequest
    {
        public string UID { get; set; }

        public string UPW { get; set; }

        public string FactoryID { get; set; }

        public List<string> FactoryIDList { get; set; }
    }
    #endregion

    #region 通过工厂id获取轴承信息
    /// <summary>
    /// 通过工厂id获取轴承信息
    /// </summary>
    public class GetBearingByFactoryIDParameter : BaseRequest
    {
        public string FactoryID { get; set; }
    }
    #endregion

    #region 验证轴承惟一
    /// <summary>
    /// 验证轴承惟一
    /// </summary>
    public class CheckFidAndBearingNumUniqueParameter : BaseRequest
    {
        public string UID { get; set; }

        public string UPW { get; set; }

        public string FactoryID { get; set; }

        public string BearingNum { get; set; }

        public int? BearingID { get; set; }
    }
    #endregion

    #region 添加系统日志
    /// <summary>
    /// 添加系统日志
    /// </summary>

    public class AddSysLogParameter : BaseRequest
    {
        public int UserID { get; set; }

        public string Record { get; set; }

        public System.DateTime AddDate { get; set; }

        public string IPAddress { get; set; }
    }
    #endregion
    #endregion
}
