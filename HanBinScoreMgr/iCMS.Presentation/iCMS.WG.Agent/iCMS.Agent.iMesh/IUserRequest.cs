using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMesh
{
    interface IUserRequest
    {
        #region reset
        /// <summary>
        /// 重启系统(Manager)
        /// </summary>
        /// <returns>调用错误码</returns>
        //enURErrCode ResetSystem(bool urgent = false);
        /// <summary>
        /// 重启Mote
        /// </summary>
        /// <param name="mac">被重启Mote的MAC地址</param>
        /// <returns>调用错误码</returns>
        //enURErrCode ResetMote(tMAC mac, bool urgent = false);
        #endregion

        #region subscribe
        /// <summary>
        /// 向Manager订阅通知，对接收的通知必须进行响应
        /// </summary>
        /// <param name="filter">订阅的通知类型</param>
        /// <returns>调用错误码</returns>
        //enURErrCode Subscribe(enSubFilters filter, bool urgent = false);
        /// <summary>
        /// 向Manager订阅通知，对接收的通知不必进行响应
        /// </summary>
        /// <param name="filter">订阅的通知类型</param>
        /// <returns></returns>
        //enURErrCode SubscribeUnack(enSubFilters filter, bool urgent = false);
        /// <summary>
        /// 取消向Manager已经订阅的所有通知类型
        /// </summary>
        /// <param name="filter">取消订阅的通知类型</param>
        /// <returns></returns>
        //enURErrCode Unsubscribe(enSubFilters filter, bool urgent = false);
        #endregion

        /// <summary>
        /// 获取当前Manager的UTC时间和ASN。由于入队列出队列及串行传输延迟的存在
        /// 通过此方法获取的时间信息不够准确
        /// </summary>
        /// <returns>调用错误码</returns>
        //enURErrCode GetTime(bool urgent = false);

        #region setNetworkConfig
        /// <summary>
        /// 设置网络ID
        /// </summary>
        /// <param name="networkId">设置的ID</param>
        /// <returns>调用错误码</returns>
        enURErrCode SetNetworkId(ushort networkId, bool urgent = false);
        /// <summary>
        /// 设置接入点发送功率
        /// </summary>
        /// <param name="txPower">Transmit Power is a signed byte (INT8) with the values 0, 8</param>
        /// <returns>调用错误码</returns>
        enURErrCode SetAPTxPower(SByte txPower, bool urgent = false);
        /// <summary>
        /// 设置Frame Profile
        /// </summary>
        /// <param name="profile"></param>
        /// <returns>调用错误码</returns>
        enURErrCode SetFrameProfile(enFrameProfile profile, bool urgent = false);
        /// <summary>
        /// 设置网络最大Mote数
        /// The maximum number of motes allowed in the network.
        /// The value can be 1-16 or 1-32, depending on the installed license
        /// </summary>
        /// <param name="maxMotes">最大Mote数</param>
        /// <returns>调用错误码</returns>
        enURErrCode SetMaxMotes(ushort maxMotes, bool urgent = false);
        /// <summary>
        /// 设置基础带宽
        /// Base bandwidth is the default bandwidth allocated to each mote that joins, 
        /// defined as expected interval between packets, in ms. 0=no allocation
        /// </summary>
        /// <param name="baseBandwidth"></param>
        /// <returns>调用错误码</returns>
        enURErrCode SetBaseBandwidth(ushort baseBandwidth, bool urgent = false);
        /// <summary>
        /// 设置下行帧倍数因子
        /// Downstream frame multiplier is a multiplier for the length of the primary
        /// downstream frame. Valid values are 1,2 or 4.
        /// </summary>
        /// <param name="downFrameMultVal"></param>
        /// <returns>调用错误码</returns>
        enURErrCode SetDownFrameMultVal(byte downFrameMultVal, bool urgent = false);
        /// <summary>
        /// 设置每个Mote的父节点数
        /// Number of parents to assign each mote (1-4).
        /// </summary>
        /// <param name="numParents">父节点数</param>
        /// <returns>调用错误码</returns>
        enURErrCode SetNumParents(byte numParents, bool urgent = false);
        /// <summary>
        /// 设置信道评估方式
        /// </summary>
        /// <param name="mode">评估方式</param>
        /// <returns>调用错误码</returns>
        enURErrCode SetCcaMode(enCcaMode mode, bool urgent = false);
        /// <summary>
        /// 设置通道列表
        /// Bitmap of channels to use for communication. Bit 0×0001 corresponds to
        /// channel 1 and bit 0×8000 corresponds to channel 16. (0=not used, 1=used)
        /// </summary>
        /// <param name="channelList">通道列表</param>
        /// <returns>调用错误码</returns>
        enURErrCode SetChannelList(ushort channelList, bool urgent = false);
        /// <summary>
        /// 设置网络自动启动功能
        /// Auto start network tells the manager whether to start the network as soon as the
        /// device is booted.
        /// </summary>
        /// <param name="auto">自动重启与否</param>
        /// <returns>调用错误码</returns>
        enURErrCode SetAutoStartNetwork(bool auto, bool urgent = false);
        /// <summary>
        /// 设置网络主干网功能
        /// Backbone frame mode (0=off,1=up,2=bidirectional)
        /// </summary>
        /// <param name="mode">主干网模式</param>
        /// <returns>调用错误码</returns>
        enURErrCode SetBackboneMode(enBBMode mode, byte size, bool urgent = false);
        /// <summary>
        /// 设置网络主干网帧大小
        /// Backbone frame size, in time slots (if bbmode=1, bbsize=1,2,4,8. If bbmode=2,
        /// bbsize=2)
        /// </summary>
        /// <param name="bbSize">帧时隙大小</param>
        /// <returns>调用错误码</returns>
        enURErrCode SetBackboneSize(byte bbSize, bool urgent = false);
        /// <summary>
        /// Controls whether the manager boots up in radiotest mode.
        /// </summary>
        /// <param name="mode"></param>
        /// <returns>调用错误码</returns>
        enURErrCode SetRadioTest(byte mode, bool urgent = false);
        /// <summary>
        /// Bandwidth over-provisioning multiplier: over-provision by value/100 (100-1000)
        /// </summary>
        /// <param name="bwMult"></param>
        /// <returns>调用错误码</returns>
        enURErrCode SetBwMult(ushort bwMult, bool urgent = false);
        /// <summary>
        /// Channel number for One Channel mode. (0-15; 255=OFF). This mode is used for
        /// rf testing only.
        /// </summary>
        /// <param name="oneChannel"></param>
        /// <returns>调用错误码</returns>
        enURErrCode SetOneChannel(byte oneChannel, bool urgent = false);
        #endregion

        /// <summary>
        /// The ClearStatistics command clears the accumulated network statistics.
        /// The command does not clear path quality or mote statistics.
        /// </summary>
        /// <returns>调用错误码</returns>
        enURErrCode ClearStatistics(bool urgent = false);

        /// <summary>
        /// The ExchangeMoteJoinKey command triggers the manager to send a new join key to the specified mote and update the
        /// manager's ACL entry for the mote.
        /// </summary>
        /// <param name="mac">指定的Mote mac地址</param>
        /// <param name="key">新的安全键值</param>
        /// <returns>调用错误码</returns>
        enURErrCode ExchangeMoteJoinKey(tMAC mac, tSECKEY key, bool urgent = false);

        /// <summary>
        /// The ExchangeNetworkId command triggers the manager to distribute a new network ID to all the motes in the network. 
        /// </summary>
        /// <param name="networkId">新的网络ID</param>
        /// <returns>调用错误码</returns>
        enURErrCode ExchangeNetworkId(ushort networkId, bool urgent = false);

        /// <summary>
        /// The RadiotestTx command allows the user to initiate a radio transmission test
        /// </summary>
        /// <param name="param">test parameter</param>
        /// <returns>调用错误码</returns>
        enURErrCode RadiotestTx(tRADIOTESTTX param, bool urgent = false);

        /// <summary>
        /// The radiotestRx command clears all previously collected statistics and initiates radio reception on the specified channel.
        /// It may only be executed if the manager has been booted up in radiotest mode (see setNetworkConfig command)
        /// </summary>
        /// <param name="chanMask">Mask of RF channel to use for the test</param>
        /// <param name="duration">Duration of test (in seconds)</param>
        /// <param name="stationId">Unique (0-255) station ID of this device</param>
        /// <returns>调用错误码</returns>
        enURErrCode RadiotestRx(ushort chanMask, ushort duration, byte stationId, bool urgent = false);

        /// <summary>
        /// This command retrieves statistics from a previously run radiotestRx command
        /// </summary>
        /// <returns>调用错误码</returns>
        enURErrCode GetRadiotestStatistics(bool urgent = false);

        /// <summary>
        /// The setACLEntry command adds a new entry or updates an existing entry in the 
        /// Access Control List (ACL).
        /// </summary>
        /// <param name="mac">MOTE MAC</param>
        /// <param name="key">join Key</param>
        /// <returns>调用错误码</returns>
        enURErrCode SetACLEntry(tMAC mac,tSECKEY key, bool urgent = false);

        /// <summary>
        /// The GetNextACLEntry command returns information about next mote entry in the access control list (ACL)
        /// To begin a search (find the first mote in ACL), a zero MAC address (0000000000000000) should be sent.
        /// </summary>
        /// <param name="mac">Mote MAC address</param>
        /// <returns>调用错误码</returns>
        enURErrCode GetNextACLEntry(tMAC mac, bool urgent = false);

        /// <summary>
        /// The DeleteACLEntry command deletes the specified mote from the access control list (ACL).
        /// If the macAddress parameter is set to all 0xFFs or all 0x00s, the entire ACL is cleared
        /// </summary>
        /// <param name="mac">Mote MAC address</param>
        /// <returns>调用错误码</returns>
        enURErrCode DeleteACLEntry(tMAC mac, bool urgent = false);

        /// <summary>
        /// The PingMote command sends a ping (echo request) to the mote specified by MAC address. 
        /// </summary>
        /// <param name="mac">Mote MAC address</param>
        /// <returns>调用错误码</returns>
        enURErrCode PingMote(tMAC mac, bool urgent = false);

        /// <summary>
        /// The GetLog command retrieves diagnostic logs from the manager or a mote specified by MAC address.
        /// </summary>
        /// <param name="mac">Mote MAC address</param>
        /// <returns>调用错误码</returns>
        enURErrCode GetLog(tMAC mac, bool urgent = false);

        /// <summary>
        /// The SendData command sends a packet to a mote in the network.
        /// </summary>
        /// <param name="mac">MAC address of the destination mote. 0xFFFFFFFFFFFFFFFF can be used to broadcast to all motes</param>
        /// <param name="priority">Priority of the packet</param>
        /// <param name="data">The payload data of the packet</param>
        /// <returns>调用错误码</returns>
        //enURErrCode SendData(tMAC mac, byte[] data, bool urgent = false, enPktPriority priority = enPktPriority.Medium);

        /// <summary>
        /// The StartNetwork command tells the manager to allow the network to start forming
        /// (begin accepting join requests from devices)
        /// </summary>
        /// <returns>调用错误码</returns>
        enURErrCode StartNetwork(bool urgent = false);

        /// <summary>
        /// The getSystemInfo command returns system-level information about the hardware and software versions. 
        /// </summary>
        /// <returns>调用错误码</returns>
        enURErrCode GetSystemInfo(bool urgent = false);

        /// <summary>
        /// The GetMoteConfig command returns a single mote description as the response.
        /// </summary>
        /// <param name="mac">mac must be a actual mac address</param>
        /// <returns>调用错误码</returns>
        //enURErrCode GetMoteConfig(tMAC mac, bool urgent = false);

        /// <summary>
        /// The GetNextMoteConfig command returns the next single mote description as the response.
        /// </summary>
        /// <param name="mac">mac can be 0 which indicate to start query</param>
        /// <returns>调用错误码</returns>
        enURErrCode GetNextMoteConfig(tMAC mac, bool urgent = false);

        /// <summary>
        /// The getPathInfo command returns parameters of requested path.
        /// </summary>
        /// <param name="source">MAC address of source mote</param>
        /// <param name="dest">MAC address of destination mote</param>
        /// <returns>调用错误码</returns>
        enURErrCode GetPathInfo(tMAC source, tMAC dest, bool urgent = false);

        /// <summary>
        /// The GetNextPathInfo command allows iteration across paths connected to a particular mote.
        /// </summary>
        /// <param name="mac"></param>
        /// <param name="filter"></param>
        /// <param name="pathId">
        /// the previous value in the iteration, Setting pathId to 0 returns the first path.
        /// </param>
        /// <returns>调用错误码</returns>
        enURErrCode GetNextPathInfo(tMAC mac, enPathDirection filter, ushort pathId, bool urgent = false);

        /// <summary>
        /// The setAdvertising command tells the manager to activate or deactivate advertising.
        /// </summary>
        /// <param name="state"></param>
        /// <returns>调用错误码</returns>
        enURErrCode SetAdvertising(enAdvState state, bool urgent = false);

        /// <summary>
        /// The setDownstreamFrameMode command tells the manager to shorten or extend the downstream slotframe.
        /// The base slotframe length will be multiplied by the downFrameMultVal for "normal" speed.  For "fast"
        /// speed the downstream slotframe is the base length. Once this command is executed, the manager switches
        /// to manual mode and no longer changes slotframe size automatically. 
        /// </summary>
        /// <param name="mode">Downstream slotframe mode</param>
        /// <returns>调用错误码</returns>
        enURErrCode SetDownstreamFrameMode(enDnstreamFrameMode mode, bool urgent = false);

        /// <summary>
        /// The GetManagerStatistics command returns dynamic information and statistics
        /// about the manager API. The statistics counts are cleared together with all
        /// current statistics using ClearStatistics.
        /// </summary>
        /// <returns>调用错误码</returns>
        enURErrCode GetManagerStatistics(bool urgent = false);

        /// <summary>
        /// The SetTime command sets the UTC time on the manager, This command may only be executed 
        /// when the network is not running.
        /// </summary>
        /// <param name="trig">0=set time immediately; 1=set time as using last timepin trigger</param>
        /// <param name="time">Time to set on the Manager</param>
        /// <returns>调用错误码</returns>
        enURErrCode SetTime(enSetimeTrigMode trig, tUTCTIMEL time, bool urgent = false);

        /// <summary>
        /// The getLicense command returns the current license key. 
        /// </summary>
        /// <returns>调用错误码</returns>
        enURErrCode GetLicense(bool urgent = false);

        /// <summary>
        /// The SetLicense command validates and updates the software license key stored in flash.
        /// Features enabled or disabled by the license key change will take effect after the device
        /// is restarted. If the license parameter is set to all 0x0s, the manager restores the default
        /// license.
        /// </summary>
        /// <param name="license">license</param>
        /// <returns>调用错误码</returns>
        enURErrCode SetLicense(tLICENSE license, bool urgent = false);

        /// <summary>
        /// The SetCLIUser command sets the password that must be used to log into the command line for
        /// a particular user role.
        /// </summary>
        /// <param name="role">user role</param>
        /// <param name="password">password</param>
        /// <returns>调用错误码</returns>
        enURErrCode SetCLIUser(enCLIUserRole role, tUSERPW password, bool urgent = false);

        /// <summary>
        /// The sendIP command sends a 6LoWPAN packet to a mote in the network.
        /// </summary>
        /// <param name="mac">
        /// The MAC address of the destination mote. 0xFFFFFFFFFFFFFFFF can be used to broadcast to all motes.
        /// </param>
        /// <param name="priority">Priority of the packet</param>
        /// <param name="options">Reserved for future use. The options field must be set to 0.</param>
        /// <param name="encryptedOffset">Offset encrypted part of data. 0xFF - data is not encrypted</param>
        /// <param name="data">The complete 6LoWPAN packet.</param>
        /// <returns>调用错误码</returns>
        enURErrCode SendIP(tMAC mac, enPktPriority priority, byte options, byte encryptedOffset, byte[] data, bool urgent = false);

        /// <summary>
        /// The RestoreFactoryDefaults command restores the default configuration and clears the ACL.
        /// This command does not affect the license.
        /// </summary>
        /// <returns>调用错误码</returns>
        //enURErrCode RestoreFactoryDefaults(bool urgent = false);

        /// <summary>
        /// The getMoteInfo command returns dynamic information for the specified mote.
        /// </summary>
        /// <param name="mac">Mote MAC address</param>
        /// <returns>调用错误码</returns>
        enURErrCode GetMoteInfo(tMAC mac, bool urgent = false);

        /// <summary>
        /// The GetNetworkConfig command returns general network configuration parameters, 
        /// including the Network ID, bandwidth parameters and number of motes. 
        /// </summary>
        /// <returns>调用错误码</returns>
        //enURErrCode GetNetworkConfig(bool urgent = false);

        /// <summary>
        /// The getNetworkInfo command returns dynamic network information and statistics. 
        /// </summary>
        /// <returns>调用错误码</returns>
        enURErrCode GetNetworkInfo(bool urgent = false);

        /// <summary>
        /// The GetMoteConfigById command returns a single mote description as the response.
        /// </summary>
        /// <param name="moteId">short address of a mote</param>
        /// <returns>调用错误码</returns>
        enURErrCode GetMoteConfigById(ushort moteId, bool urgent = false);

        /// <summary>
        /// The SetCommonJoinKey command will set a new value for the common join key. 
        /// The common join key is used to decrypt join messages only if the ACL is empty.
        /// </summary>
        /// <param name="key"> Common join key</param>
        /// <returns>调用错误码</returns>
        enURErrCode SetCommonJoinKey(tSECKEY key, bool urgent = false);

        /// <summary>
        /// The getIPConfig command returns the manager's IP configuration parameters,
        /// including the IPv6 address and mask. 
        /// </summary>
        /// <returns>调用错误码</returns>
        enURErrCode GetIPConfig(bool urgent = false);

        /// <summary>
        /// The SetIPConfig command sets the IPv6 prefix of the mesh network.
        /// </summary>
        /// <param name="ipv6Address">IPv6 address</param>
        /// <param name="mask">Subnet mask</param>
        /// <returns>调用错误码</returns>
        enURErrCode SetIPConfig(tIPV6ADDR ipv6Address,tIPV6MASK mask, bool urgent = false);

        /// <summary>
        /// The DeleteMote command deletes a mote from the manager's list. A mote can only 
        /// be deleted if it in the Lost or Unknown states.
        /// </summary>
        /// <param name="mac"></param>
        /// <returns></returns>
        enURErrCode DeleteMote(tMAC mac, bool urgent = false);

        /// <summary>
        /// The GetMoteLinks command returns information about links assigned to the mote. 
        /// </summary>
        /// <param name="mac"></param>
        /// <param name="idx"></param>
        /// <returns></returns>
        enURErrCode GetMoteLinks(tMAC mac, ushort idx, bool urgent = false);
    }
}
