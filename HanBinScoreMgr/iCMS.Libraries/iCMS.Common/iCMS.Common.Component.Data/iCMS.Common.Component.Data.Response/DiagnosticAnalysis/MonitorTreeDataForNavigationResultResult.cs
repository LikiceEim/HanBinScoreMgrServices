/************************************************************************************
 * Copyright (c) 2016Microsoft All Rights Reserved.
 *命名空间：iCMS.Common.Component.Data.Response
 *文件名：  MonitorTreeResult
 *创建人：  王颖辉
 *创建时间：2016-07-27
 *描述：监测树返回值
/************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.Common.Component.Data.Response.DiagnosticAnalysis
{
    #region 监测树返回值
    /// <summary>
    /// 创建人：王颖辉
    /// 创建时间：2016-07-27
    /// 创建记录：监测树返回值
    /// </summary>
    public class MonitorTreeDataForNavigationResult
    {
        /// <summary>
        /// 最小刷新时间，单位：分钟
        /// </summary>
        public string MinFreshTime { get; set; }
        /// <summary>
        /// 监测树设备状态信息集合
        /// </summary>
        public List<MTStatusInfo> MTDevStatusInfos { get; set; }
        /// <summary>
        /// 监测树状态检测设备信息集合
        /// </summary>
        public List<MTStatusInfo> MTWSNStatusInfos { get; set; }
        /// <summary>
        /// 从根到子节点（设备以上），以#号隔开。例如：西安因联#测试工厂#测试车间
        /// </summary>
        public string TreeNode { get; set; }
        /// <summary>
        /// ID列表
        /// </summary>
        public List<int> TreeNodeID { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
    }
    #endregion

    #region 监测树状态信息
    /// <summary>
    /// 监测树信息
    /// </summary>
    public class MTStatusInfo
    {
        /// <summary>
        /// 监测树当前记录ID
        /// </summary>
        public string MTId
        {
            get;
            set;
        }


        /// <summary>
        /// 监测树当前记录的父节点ID
        /// </summary>
        public string MTPid
        {
            get;
            set;
        }

        /// <summary>
        /// 检测树记录名称
        /// </summary>
        public string MTName
        {
            get;
            set;
        }

        /// <summary>
        /// 监测树当前记录状态
        /// </summary>
        public string MTStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 监测树当前记录的类型:
        /// (监测树类型Code)：监测树
        /// DEVICE：设备
        /// MEASURESITE：测量位置
        /// VIBSINGAL：振动信号
        /// EIGEN_VALUE ：振动特征值
        /// TEMPE_DEVICE ：设备温度
        /// TEMPE_WS ：WS温度
        /// VOLTAGE_WS ：WS电池电压
        /// SERVER：Server（网关的父节点）
        /// WIRELESS_GATE：网关WG
        /// WIRELESS_SENSOR：传感器WS
        /// WIRELESS：无线节点
        /// WIRED：有线节点 
        /// WIRED_GATE : 有线网关WG
        /// WIRED_SENSOR: 有线传感器WS（关联isRelated）
        /// </summary>
        public string MTType
        {
            get;
            set;
        }

        /// <summary>
        /// MTType 为 (监测树)，为监测树级别 Describe；
        /// MTType为设备，区分主用和备用设备
        /// 0：主用；1：备用
        /// MTType 为 MEASURESITE (测量位置)，为WS名称；
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 数据库ID，即数据库中的ID。如：类型为设备，则该字段表示设备ID。
        /// </summary>
        public string RecordID
        {
            get;
            set;
        }

        /// <summary>
        /// 设备类型，1备用 （以实际数据库为准）
        /// </summary>
        public int? DeviceType
        {
            get;
            set;
        }

        /// <summary>
        /// 是否是监测树类型 0：否 1：是
        /// </summary>
        public int? IsMonitorTree
        {
            get;
            set;
        }

        #region 解决方案融合 添加字段， Added by QXM, 2018/05/03
        /// <summary>
        /// 设备形态类型，仅针对DEVICE, WG节点有效
        /// </summary>
        public int? DevFormType { get; set; }
        /// <summary>
        /// 是否是转速测点
        /// </summary>
        public bool IsSpeed { get; set; }
        /// <summary>
        /// 关联的转速测点ID
        /// </summary>
        public int? RelationSpeedMSiteID { get; set; }
        /// <summary>
        /// 关联的转速测点名称
        /// </summary>
        public string RelationSpeedMSiteName { get; set; }
        #endregion
    }
    #endregion

    #region 监测树状态信息
    /// <summary>
    /// 监测树信息
    /// </summary>
    public class MTStatusDBInfo
    {
        /// <summary>
        /// 监测树当前记录ID
        /// </summary>
        public int MTId
        {
            get;
            set;
        }


        /// <summary>
        /// 监测树当前记录的父节点ID
        /// </summary>
        public int MTPid
        {
            get;
            set;
        }

        /// <summary>
        /// 检测树记录名称
        /// </summary>
        public string MTName
        {
            get;
            set;
        }

        /// <summary>
        /// 监测树当前记录状态
        /// </summary>
        public int MTStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 监测树当前记录的类型
        /// </summary>
        public string MTType
        {
            get;
            set;
        }

        /// <summary>
        /// MTType 为6(测量位置)，为WS名称
        /// </summary>
        public string Remark
        {
            get;
            set;
        }

        /// <summary>
        /// 数据库ID，即数据库中的ID。如：类型为设备，则该字段表示设备ID。
        /// </summary>
        public int RecordID
        {
            get;
            set;
        }
    }
    #endregion

    #region 监测树数据
    /// <summary>
    /// 监测树数据
    /// </summary>
    public class MonitorTreeData
    {
        public List<MTInfo> MTInfo { get; set; }

        public string Reason { get; set; }

        public MonitorTreeData()
        {
            MTInfo = new List<MTInfo>();

            Reason = string.Empty;
        }
    }
    #endregion

    #region 监测树信息
    /// <summary>
    /// 监测树信息
    /// </summary>
    public class MTInfo
    {
        /// <summary>
        ///  监测树ID	
        /// </summary>
        public int MonitorTreeID { get; set; }

        /// <summary>
        ///	监测树父ID	 
        /// </summary>
        public int PID { get; set; }

        /// <summary>
        /// 源ID	
        /// </summary>
        public int OID { get; set; }

        /// <summary>
        /// 是否系统默认
        /// </summary>
        public int IsDefault { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///  描述
        /// </summary>
        public string Des { get; set; }

        public int Type { get; set; }//Int	类型	
        public string TypeName { get; set; }	//String	类型名称	
        public string AddDate { get; set; }//	String	添加日期	
        public int ImageID { get; set; }	//int	对应图片ID	
        public int Status { get; set; }//	int	状态	0:未采集;1正常;2:高报;3:高高报
        public string Address { get; set; }	//String	地址	
        public string URL { get; set; }	//String	URL	
        public string TelphoneNO { get; set; }//String	电话	
        public string FaxNO { get; set; }//	String	传真	
        public string Latitude { get; set; }//String	维度	
        public string Longtitude { get; set; }	//String	精度	
        public string Length { get; set; }//	String	长	
        public string Width { get; set; }	//String	宽	
        public string Area { get; set; }//String	面积	
        public string PersonInCharge { get; set; }	//String	负责人	
        public string PersonInChargeTel { get; set; }//String	负责人电话	
        public string Remark { get; set; }//	String	备注	
        public int ChildCount { get; set; }	//Int	孩子数量	

    }
    #endregion

    #region 树
    /// <summary>
    /// 树
    /// </summary>
    public class Tree
    {
        /// <summary>
        /// 树ID
        /// </summary>
        public int TreeId
        {
            get;
            set;
        }

        /// <summary>
        /// 树名称
        /// </summary>
        public string TreeName
        {
            get;
            set;
        }

        /// <summary>
        /// 父Id
        /// </summary>
        public int? ParentId
        {
            get;
            set;
        }

        /// <summary>
        /// 真实Id
        /// </summary>

        public int TrueId
        {
            get;
            set;
        }

        /// <summary>
        /// 级别
        /// </summary>
        public int Level
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int MTStatus
        {
            set;
            get;
        }

        /// <summary>
        /// 使用类型
        /// </summary>
        public int UseType
        {
            set;
            get;
        }
    }

    /// <summary>
    /// 试图tree对应实体
    /// </summary>
    public class viewMonitorTree
    {
        /// <summary>
        /// 树ID
        /// </summary>
        public int MonitorTreeID
        {
            get;
            set;
        }

        /// <summary>
        /// 树名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 父Id
        /// </summary>
        public int? PId
        {
            get;
            set;
        }

        /// <summary>
        /// 真实Id
        /// </summary>

        public int TrueId
        {
            get;
            set;
        }

        /// <summary>
        /// 级别
        /// </summary>
        public int Level
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int MTStatus
        {
            set;
            get;
        }

        /// <summary>
        /// 使用类型
        /// </summary>
        public int UseType
        {
            set;
            get;
        }
    }
    #endregion
}