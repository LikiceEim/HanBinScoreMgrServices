using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// 有关程序集的常规信息通过以下
// 特性集控制。更改这些特性值可修改
// 与程序集关联的信息。
[assembly: AssemblyTitle("iCMS.WG.Agent.iMesh")]
[assembly: AssemblyDescription("iCMS系统中Agent与Manager通信协议栈及应用协议封装")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Xi'an iLine Information Technology Co., Ltd.")]
[assembly: AssemblyProduct("iCMS.WG.Agent.iMesh")]
[assembly: AssemblyCopyright("Copyright © 2016 iLine")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// 将 ComVisible 设置为 false 使此程序集中的类型
// 对 COM 组件不可见。如果需要从 COM 访问此程序集中的类型，
// 则将该类型上的 ComVisible 特性设置为 true。
[assembly: ComVisible(false)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("45410cbf-bb71-4e4c-b9a5-e30c5b1e3f94")]

// 程序集的版本信息由下面四个值组成:
//
//      主版本
//      次版本 
//      修订号
//      生成号
//
// 可以指定所有这些值，也可以使用“修订号”和“生成号”的默认值，
/*
 * 1.5.0.x: x=0，基于做这个发布的1.4版本
 *          x=1，取消无用的组件配置项，主要涉及日志压缩开关；
 *          x=2，添加网关硬件方案可配置项，可选项为ZLSN2002/MIO5251EW
 *          x=3，添加ZLSN2002的IO控制实现方案
 *          x=4，① 在线程heartbeatHandler中添加m_Serial.RxFrameCnt = 0;保证网络中无传感器时，心跳线程的有效性
 *               ② 增加线程moniterNewSessinWorker，使得监测Manager失连更加有效
 *          x=5，修正等待传感器校准响应的超时时间为其他命令的3倍
 *          x=6，添加对于有人TCP232E2串转以太网模块的支持
 *          x=7，修正1.4版本中BUG-1007，Mote掉线不能及时检测的问题，原因在于代码逻辑缺陷，原逻辑如下：
 *          if (cmdId == enCmd.CMDID_GETMOTECONFIG && echo.RC == eRC.RC_END_OF_LIST)
                return;
            因为当前系统遍历所有在线WS是通过getMoteConfig实现，当遇到CMDID_GETMOTECONFIG时表示，遍历网络完成。而系统心跳动作也是使用
            getMoteConfig实现，当心跳检查某一WS得到的返回码是RC_END_OF_LIST时，原逻辑会过滤掉此条WS掉线事件，逻辑修改如下：
            if (IsQueryingAllWs && cmdId == enCmd.CMDID_GETMOTECONFIG && echo.RC == eRC.RC_END_OF_LIST)
                return;
            x=8，进一步精确WS掉线的检测，添加变量m_macCurTarget，以及围绕m_macCurTarget的相关代码
            x=9，① 对函数stopHeartbeat添加返回值，对应优化心跳线程heartbeatHandler的暂停逻辑；
 *               ② 有关修改Netid，形成三个函数，分别是设置SetWsNwID，SetWgNwId，ExchangeNetworkId，并完善各自的正常响应和
 *                  异常响应通知事件；
 *               ③ ExchangeNetworkId的异步通知响应不能沿用SendData的超时模式
 * 1.5.1.x: 1，修正函数MeshAPI
 *             ① 放开lock
 *             ② 将ResetWaitResponse位置提前到下发命令之前
 *             ③ 将返回码判断放在lock中
 *             ④ 添加代码及时清除m_bStressfull
 *          2，修正函数appCmdResponseHandler
 *             ① 将listDelElem的空间申请放置在while循环之外
 *             ② 修改日志输出信息
 *          3，修正函数asynCmdResponseHandler
 *             ① 将listDelCbid的空间申请放置在while循环之外
 *             ② 修改日志输出信息
 *          4，重新建立会话时，添加对m_dicWaitAppRespCmds和m_Mesh.m_dicAsyncCmdRespRecords的清空操作
 *          5，添加信号量m_asigEased，保证Manager繁忙后及时处理下一条指令
 * 1.5.2.x: 1，精细化序列化及反序列化
 *          2, 添加时间补偿
 *          3, 适配1.4系统
 *             ① 把1.5系统的AppProtocolParamDef文件的文件名改为MeshAdapter,其中需要适配1.4系统的类和方法更改名称
 *             ② 增加AppProtocolParamDef文件，仅包含与1.5不同的类和方法
 *             ③ 增加MeshAdapte_adapter文件，对所有通知进行再次解析，解析为适配1.4的通知。
 *             ④ 对MeshAdapter文件中的所有应用层下发命令与1.5不匹配的做再次封装下发。
 *          4,启停机命令响应完后，休眠1s后再上报给上层。
 *             ① WS在线的判断从OPERATIONAL通知改为发送自描述报告
 *          5,在升级过程中如果WS掉线，则认为升级失败！
 *          6,接收数据时，描述信息响应命令放到快速发送队列！
 *          7,①重新建立会话删了缓存队列时需要加锁。
 *            ②asynCmdResponseHandler异步命令处理函数中，当先收到packetsend，等待ACK一段时间，如果没有ACK则删除该等待异步命令。
 *          8,把tAppHeader中u16SSNID拆分为u8DevTotal和u8DevNum，分别为设备总数和设备编号。
 *          9，删除对WS健康报告，特征值，波形描述的应用层响应。
 */
[assembly: AssemblyVersion("1.5.2.9")]
[assembly: AssemblyFileVersion("1.5.2.9")]