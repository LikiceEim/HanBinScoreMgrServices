/***********************************************************************
 * Copyright (c) 2016@ILine All Rights Reserved.
 *命名空间：iCMS.WG.Agent.Common
 *文件名：  CommonConst
 *创建人：  LF
 *创建时间：2016/2/14 10:10:19
 *描述：常量
 *
 *=====================================================================**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iCMS.WG.Agent.Common
{
    public class CommonConst
    {


        #region 报文长度
        //收到的采集数据报文中的波形数据的长度
        public const int ReceiveWaveDataLength = 66;
        #region Mesh网络上传消息报文
        public const int Message_Length_Info = 10;
        //发送报文
        //自报告响应
        //自报告增加校时（秒：8位，微秒：4位）
        //public const int Message_Length_Send_Report_Response = 12;
        //自报告增加mac地址（8位）
        //public const int Message_Length_Send_Report_Response = 24;
        public const int Message_Length_Send_Report_Response = 32;
        //校时
        public const int Message_Length_Send_Timer = 16;
        //修改WSID
        public const int Message_Length_Send_Config_WSID = 19;
        //配置WS NetID
        public const int Message_Length_Send_Config_WSNetID = 12;
        //配置WG NetID
        public const int Message_Length_Send_Config_WGNetID = 12;
        //配置SNCode
        public const int Message_Length_Send_Config_WSSNCode = 10;
        //取得SNCode
        public const int Message_Length_Send_Get_WSSNCode = 10;
        //发送测量定义
        
        //public const int Message_Length_Send_Meas_Define = 66;
        //根据通讯协议V2.9修改 midify by 20151116
        //public const int Message_Length_Send_Meas_Define = 74;
        public const int Message_Length_Send_Meas_Define = 61;
        //重启
     
        //public const int Message_Length_Send_Restart = 12;
        public const int Message_Length_Send_Restart = 20;
        //恢复出厂设置
        public const int Message_Length_Send_Factory_Default = 12;
        //升级信息
       
        //public const int Message_Length_Send_Update_Info = 29;
        public const int Message_Length_Send_Update_Info = 37;
        // 发送测量定义添加8位mac地址
        //升级数据
        //public const int Message_Length_Send_Update_Data = 87;
        public const int Message_Length_Send_Update_Data = 95;
        //波形数据
        public const int Message_Length_Send_Wave_Data_Response = 12;

        //接收报文
        //自报告
        public const int Message_Length_Receive_Report = 25;
        //校时
        public const int Message_Length_Receive_Timer = 16;
        //修改WSID
        public const int Message_Length_Receive_Config_WSID_Response = 12;
        //配置WS NetID
        public const int Message_Length_Receive_Config_WSNetID_Response = 12;
        //配置WG NetID
        public const int Message_Length_Receive_Config_WGNetID_Response = 12;
        //配置SNCode
        public const int Message_Length_Receive_Config_WSSNCode_Response = 10;
        //取得SNCode
        public const int Message_Length_Receive_Get_WSSNCode = 16;
        //发送测量定义
        public const int Message_Length_Receive_Meas_Define_Response = 12;
        //重启
        public const int Message_Length_Receive_Restart_Response = 12;
        //恢复出厂设置
        public const int Message_Length_Receive_Factory_Default_Response = 12;
        //升级信息
        public const int Message_Length_Receive_Update_Info_Response = 12;
        //升级数据
        public const int Message_Length_Receive_Update_Data_Response = 12;
        //波形数据
        public const int Message_Length_Receive_Wave_Data = 12;
        //波形信息
        public const int Message_Length_Receive_Wave_Data_Info = 49;
        #endregion

        #region  Agent下发报文命令
        /// <summary>
        /// MAC地址长度
        /// </summary>
        public const int Length_MAC = 8;
        /// <summary>
        /// 版本号长度
        /// </summary>
        public const int Length_Version = 4;
        /// <summary>
        /// 报文头长度
        /// </summary>
        public const int Length_Message_Info = 10;
        /// <summary>
        /// 配置NetID报文长度
        /// </summary>
        public const int Length_Message_Config_NetID = 12;
        /// <summary>
        /// 配置WS编号报文长度
        /// </summary>
        public const int Length_Message_Config_WS_NO = 19;
        /// <summary>
        /// 校准WS报文长度
        /// </summary>
        public const int Length_Message_Config_WS_Calibration = 10;

        #endregion

        #endregion

        #region Mesh网络上传报文内容位置
        //WSID
        public const int Message_WSID_Index = 0;
        //SessionID
        public const int Message_SessionID_Index = 1;
        //主命令
        public const int Message_MainCommand_Index = 6;
        //子命令
        public const int Message_SubCommand_Index = 7;
        //是否请求
        public const int Message_Request_Index = 8;
        //长度
        public const int Message_Data_Length_Index = 9;
        //报文数据
        public const int Message_DataStart_Index = 10;
        #endregion

        #region 报文命令
        #region Mesh上传报文命令
        //主命令
        /// <summary>
        /// 主命令 - 通知类
        /// </summary>
        public const int Message_MainCommand_Report = 1;
        /// <summary>
        /// 主命令 - 配置参数
        /// </summary>
        public const int Message_MainCommand_Config_Parameter = 2;
        /// <summary>
        /// 主命令 - 取得参数
        /// </summary>        
        public const int Message_MainCommand_Get_Parameter = 3;
        /// <summary>
        /// 主命令 - 恢复出厂设置
        /// </summary>
        public const int Message_MainCommand_Factory_Default = 4;
        /// <summary>
        /// 主命令 - 重启
        /// </summary>
        public const int Message_MainCommand_ReStart = 5;
        /// <summary>
        /// 主命令 - 升级WS
        /// </summary>
        public const int Message_MainCommand_Update = 6;
        /// <summary>
        /// 主命令 - 辅助测试
        /// </summary>
        public const int Message_MainCommand_Test = 7;
        /// <summary>
        /// 主命令 - 响应Agent
        /// </summary>
        public const int Message_MainCommand_Response_Agent = 8;

        //子命令
        /// <summary>
        /// 子命令 - 通知类 - WS自报告
        /// </summary>
        public const int Message_MainCommand_Report_SubCommand_Self_Report = 1;
        /// <summary>
        /// 子命令 - 通知类 - WS健康数据
        /// </summary>
        public const int Message_MainCommand_Report_SubCommand_WS_Health = 2;
        /// <summary>
        /// 子命令 - 通知类 - 设备健康数据
        /// </summary>
        //delete by iLine 20150329
        //public const int Message_MainCommand_Report_SubCommand_Dev_Health = 3;
        /// <summary>
        /// 子命令 - 通知类 - 波形信息
        /// </summary>
        public const int Message_MainCommand_Report_SubCommand_WaveInfo = 3;
        /// <summary>
        /// 子命令 - 通知类 - 波形数据
        /// </summary>
        public const int Message_MainCommand_Report_SubCommand_Wave = 4;
        /// <summary>
        /// 子命令 - 通知类 - WG连接
        /// </summary>
        public const int Message_MainCommand_Report_SubCommand_WG_Connected = 5;
        /// <summary>
        /// 子命令 - 通知类 - 特征值
        /// </summary>
        public const int Message_MainCommand_Report_SubCommand_CharacterValue = 6;
        /// <summary>
        /// 子命令 - 通知类 - WG报告数据 - WS连接状态
        /// </summary>
        public const int Message_MainCommand_Report_SubCommand_WG_Report = 7;
        //根据通讯协议V2.9修改 add by 20151116
        /// <summary>
        /// 子命令 - 通知类 - 温度、电池电压
        /// </summary>
        public const int Message_MainCommand_Report_SubCommand_Temperature_Volatage = 10;
        /// <summary>
        /// 子命令 - 配置参数 - 校时
        /// </summary>
        public const int Message_MainCommand_Config_SubCommand_Time = 1;
        /// <summary>
        /// 子命令 - 配置参数 - 配置New WSID
        /// </summary>
        public const int Message_MainCommand_Config_SubCommand_NewWSID = 2;
        /// <summary>
        /// 子命令 - 配置参数 - 配置WG NetID
        /// </summary>
        public const int Message_MainCommand_Config_SubCommand_WG_NetID = 3;
        /// <summary>
        /// 子命令 - 配置参数 - 发送特征值测量定义
        /// </summary>
        //根据通讯协议V2.9修改 delete by 20151116
        //public const int Message_MainCommand_Config_SubCommand_MeasDefine_Character = 4;
        /// <summary>
        /// 子命令 - 配置参数 - 发送测量定义
        /// </summary>
        //根据通讯协议V2.9，测量定义子码变更为4
        //public const int Message_MainCommand_Config_SubCommand_MeasDefine = 5;
        public const int Message_MainCommand_Config_SubCommand_MeasDefine = 4;
        /// <summary>
        /// 子命令 - 配置参数 - 配置SN Code
        /// </summary>
        public const int Message_MainCommand_Config_SubCommand_SNCode = 6;
        /// <summary>
        /// 子命令 - 配置参数 - 校准传感器
        /// </summary>
        public const int Message_MainCommand_Config_SubCommand_CheckSensor = 7;
        /// <summary>
        /// 子命令 - 配置参数 - 配置网关状态
        /// </summary>
        public const int Message_MainCommand_Config_SubCommand_ConfigWGStatus = 8;
        /// <summary>
        /// 子命令 - 获取参数 - 取得WS信息
        /// </summary>
        public const int Message_MainCommand_Get_Parameter_SubCommand_WG_Info = 1;
        /// <summary>
        /// 子命令 - 获取参数 - 取得SN Code
        /// </summary>
        public const int Message_MainCommand_Get_Parameter_SubCommand_SNCode = 2;
        /// <summary>
        /// 子命令 - 恢复出厂设置 - WS
        /// </summary> 
        public const int Message_MainCommand_Factory_Default_SubCommand_WS = 1;
        /// <summary>
        /// 子命令 - 恢复出厂设置 - WG
        /// </summary>
        public const int Message_MainCommand_Factory_Default_SubCommand_WG = 2;
        /// <summary>
        /// 子命令 - 重启 - WS
        /// </summary>
        public const int Message_MainCommand_ReStart_SubCommand_WS = 1;
        /// <summary>
        /// 子命令 - 重启 - WG
        /// </summary>
        public const int Message_MainCommand_ReStart_SubCommand_WG = 2;

        //升级WS
        /// <summary>
        /// 子命令 - 升级 - WS升级信息
        /// </summary>
        public const int Message_MainCommand_Update_SubCommand_WS_Info = 1;
        /// <summary>
        /// 子命令 - 升级 - WS升级数据
        /// </summary>
        public const int Message_MainCommand_Update_SubCommand_WS_Data = 2;

        /// <summary>
        /// 子命令 - 响应Agent - 
        /// </summary>
        public const int Message_MainCommand_Response_Agent_SubCommand_Response = 1;
        /// <summary>
        /// 子命令 - Agent 的响应 - 
        /// </summary>
        public const int Message_MainCommand_Response_Agent_SubCommand_Agent_Response = 2;

        /// <summary>
        /// 请求
        /// </summary>
        public const int Message_Request_True = 1;
        /// <summary>
        /// 非请求
        /// </summary>
        public const int Message_Request_False = 0;

        //WS的总秒数
        public const Int64 Common_WS_ALLSeconds = 1025665200;

        public const int HDLCPAYLOAD_Index_MAC = 17;
        #endregion

        #region  Agent下发报文命令
        //主命令
        /// <summary>
        /// 主命令 - 通知类
        /// </summary>
        public const int MainCommand_Report = 1;
        /// <summary>
        /// 主命令 - 配置参数
        /// </summary>
        public const int MainCommand_Config_Parameter = 2;
        /// <summary>
        /// 主命令 - 取得参数
        /// </summary>        
        public const int MainCommand_Get_Parameter = 3;
        /// <summary>
        /// 主命令 - 恢复出厂设置
        /// </summary>
        public const int MainCommand_Factory_Default = 4;
        /// <summary>
        /// 主命令 - 重启
        /// </summary>
        public const int MainCommand_ReStart = 5;
        /// <summary>
        /// 主命令 - 升级WS
        /// </summary>
        public const int MainCommand_Update = 6;
        /// <summary>
        /// 主命令 - 辅助测试
        /// </summary>
        public const int MainCommand_Test = 7;

        //子命令
        /// <summary>
        /// 子命令 - 通知类 - WS自报告
        /// </summary>
        public const int SubCommand_Report_Self_Report = 1;
        /// <summary>
        /// 子命令 - 通知类 - WS健康数据
        /// </summary>
        public const int SubCommand_Report_WS_Health = 2;
        /// <summary>
        /// 子命令 - 通知类 - 波形信息
        /// </summary>
        public const int SubCommand_Report_WaveInfo = 3;
        /// <summary>
        /// 子命令 - 通知类 - 波形数据
        /// </summary>
        public const int SubCommand_Report_Wave = 4;
        /// <summary>
        /// 子命令 - 通知类 - WG连接
        /// </summary>
        public const int SubCommand_Report_WG_Connected = 5;
        /// <summary>
        /// 子命令 - 通知类 - 特征值
        /// </summary>
        public const int SubCommand_Report_CharacterValue = 6;
        /// <summary>
        /// 子命令 - 通知类 - WG报告数据 - WS连接状态
        /// </summary>
        public const int SubCommand_Report_WG_Report = 7;
        /// <summary>
        /// 子命令 - 通知类 - WS连接状态
        /// </summary>
        public const int SubCommand_Report_WS_Report = 17;
        /// <summary>
        /// 子命令 - 配置参数 - 校时
        /// </summary>
        public const int SubCommand_Config_Time = 1;
        /// <summary>
        /// 子命令 - 配置参数 - 配置New WSID
        /// </summary>
        public const int SubCommand_Config_NewWSNO = 2;
        /// <summary>
        /// 子命令 - 配置参数 - 配置WG NetID
        /// </summary>
        public const int SubCommand_Config_WG_NetID = 3;
        /// <summary>
        /// 子命令 - 配置参数 - 发送特征值测量定义
        /// </summary>
        public const int SubCommand_Config_MeasDefine_Character = 4;
        /// <summary>
        /// 子命令 - 配置参数 - 发送测量定义
        /// </summary>
        public const int SubCommand_Config_MeasDefine = 5;
        /// <summary>
        /// 子命令 - 配置参数 - 配置SN Code
        /// </summary>
        public const int SubCommand_Config_SNCode = 6;
        /// <summary>
        /// 子命令 - 配置参数 - 校准传感器
        /// </summary>
        public const int SubCommand_Config_CheckSensor = 7;
        /// <summary>
        /// 子命令 - 获取参数 - 取得WS信息
        /// </summary>
        public const int SubCommand_Get_Parameter_WG_Info = 1;
        /// <summary>
        /// 子命令 - 获取参数 - 取得SN Code
        /// </summary>
        public const int SubCommand_Get_Parameter_SNCode = 6;
        /// <summary>
        /// 子命令 - 恢复出厂设置 - WS
        /// </summary> 
        public const int SubCommand_Factory_Default_WS = 1;
        /// <summary>
        /// 子命令 - 恢复出厂设置 - WG
        /// </summary>
        public const int SubCommand_Factory_Default_WG = 2;
        /// <summary>
        /// 子命令 - 重启 - WS
        /// </summary>
        public const int SubCommand_ReStart_WS = 1;
        /// <summary>
        /// 子命令 - 重启 - WG
        /// </summary>
        public const int SubCommand_ReStart_WG = 2;

        //升级WS
        /// <summary>
        /// 子命令 - 升级 - WS升级信息
        /// </summary>
        public const int SubCommand_Update_Info = 1;
        /// <summary>
        /// 子命令 - 升级 - WS升级数据
        /// </summary>
        public const int SubCommand_Update_Data = 2;


        #endregion

        #endregion

        #region 升级信息各字段开始位置
        //魔术字
        public const int Update_Info_MagicWord = 0;
        //版本号
        public const int Update_Info_Version = 4;
        //固件大小
        public const int Update_Info_FWSize = 8;
        //下发数据包总数
        public const int Update_Info_TotalDataPacketNum = 12;
        //单个数据包长度
        public const int Update_Info_SinglePacketSize = 14;
        //镜像文件进入点
        public const int Update_Info_EntryPoint = 15;
        #endregion


        #region 波形数据属性
        /// <summary>
        /// 报文中波形数据的长度
        /// </summary>
        public static int Wave_Length_InMessage = 66;
        #endregion

        #region 响应状态
        /// <summary>
        /// 响应状态 - 成功
        /// </summary>
        public const string Response_States_Succeed = "成功";
        /// <summary>
        /// 响应状态 - 失败
        /// </summary>
        public const string Response_States_Failed = "失败";
        #endregion

        /// <summary>
        /// GetMoteConfig参数
        /// </summary>
        public const string RequestGetMoteConfig = "0000000000000000";
    }
}
