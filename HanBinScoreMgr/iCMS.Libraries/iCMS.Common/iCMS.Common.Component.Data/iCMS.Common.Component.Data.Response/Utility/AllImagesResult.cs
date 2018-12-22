/************************************************************************************
 * Copyright (c) 2016iLine All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response.Utility
 *文件名：  AllImagesResult
 *创建人：  王颖辉
 *创建时间：2016-11-15
 *描述：所有图片返回结果
/************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.Utility
{
    #region 所有图片返回结果
    /// <summary>
    /// 所有图片返回结果
    /// </summary>
    public class AllImagesResult
    {
        public List<ImageInfo> ImageInfoList { get; set; }

        public AllImagesResult()
        {
            ImageInfoList = new List<ImageInfo>();
        }
    }
    #endregion

    #region 图片类
    /// <summary>
    /// 图片类
    /// </summary>
    public class ImageInfo
    {
        /// <summary>
        /// 图片ID
        /// </summary>
        public int ImageID { get; set; }
        /// <summary>
        /// 图片名称
        /// </summary>
        public string ImageName { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// 图片类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 图片宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 图片高度
        /// </summary>
        public int Height { get; set; }
    }
    #endregion
}
