/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Request.Utility
 *文件名：  AllImagesParameter
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：添加模块数据
/************************************************************************************/
using iCMS.Common.Component.Data.Base;
namespace iCMS.Common.Component.Data.Request.Utility
{
    #region 图片
    #region 图片类型
    /// <summary>
    ///0：全部类型(返回全部)
    ///1：系统登录页
    ///2：系统首页
    ///3：系统“关于我们”
    ///4：监测树
    ///5：设备树
    ///6：无线网关（WG）
    ///7：无线传感器（WS）
    ///8：形貌图
    /// </summary>
    public class AllImagesParameter : BaseRequest
    {
        public int Type { get; set; }
    }
    #endregion

    #region 保存图片
    /// <summary>
    /// 保存图片
    /// </summary>
    public class SaveImageParameter : BaseRequest
    {
        public string ImageName { get; set; }

        public string ImagePath { get; set; }

        public int ImageType { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
    #endregion

    #region 删除图片
    /// <summary>
    /// 删除图片
    /// </summary>
    public class DeleteImageParameter : BaseRequest
    {
        public int ImageID { get; set; }
    }
    #endregion

    #endregion
}
