using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMesh
{
    #region Response Codes
    public enum eRC
    {
        RC_OK = 0,                  // The application layer has processed the command correctly
        RC_INVALID_COMMAND = 1,     // Invalid command
        RC_INVALID_ARGUMENT = 2,    // Invalid argument
        RC_END_OF_LIST = 11,        // End of list is returned when an iteration reaches the end of the list of objects
        RC_NO_RESOURCES = 12,       // Reached maximum number of items
        RC_IN_PROGRESS = 13,        // Operation is in progress
        RC_NACK = 14,               // Negative acknowledgment
        RC_WRITE_FAIL = 15,         // Flash write failed
        RC_VALIDATION_ERROR = 16,   // Parameter validation error
        RC_INV_STATE = 17,          // Object has inappropriate state
        RC_NOT_FOUND = 18,          // Object is not found
    }
    #endregion

    public enum enErrCode
    {
        ERR_NONE = 0,
        ERR_BUSY,
        ERR_NOT_CONNECTED, // only used in SmartMesh IP Manager
        ERR_ALREADY,
        ERR_MALFORMED,
        ERR_INVALID_PARAM,
        ERR_MGR_RESPONSE_TIMEOUT,
        ERR_MGR_STRESSFUL, // 表示当前Manager吞吐压力大，源头为RC_NACK
    }

    public enum enURErrCode
    {
        ERR_NONE = 0,
        ERR_BUSY,
        ERR_NOT_CONNECTED, // only used in SmartMesh IP Manager
        ERR_ALREADY,
        ERR_MALFORMED,
        ERR_INVALID_PARAM,
        ERR_MORE_REQUEST,  // 用户请求队列
        ERR_TIMEOUT,
    }

    public enum enCommandFinishedResult
    {
        RC_OK = 0,
        RC_NOACK = 1,
        RC_COMMANDTIMEOUT = 2,
    }
    public enum enResetType
    {
        System = 0,
        Mote = 2,
    }

    public enum enSubFilters
    {
        Event = 0x02,
        Log = 0x04,
        Data = 0x10,
        IPData = 0x20,
        HealthReports = 0x40,
        /// <summary>
        /// Event | Data | HealthReports,
        /// </summary>
        ICmsFocus = Event | Data | HealthReports,
        /// <summary>
        /// Event | Log | Data | IPData | HealthReports,
        /// </summary>
        All = Event | Log | Data | IPData | HealthReports,
    }

    public enum enFrameProfile
    {
        Profile_01 = 1,
    }

    public enum enCcaMode
    {
        Off = 0,       // CCA disabled
        Energy = 1,    // Energy detect
        Carrier = 2,   // Carrier detect
        Both = 3,      // Energy detect and Carrier detect
    }

    public enum enBBMode
    {
        Off = 0,          // Backbone frame is off
        Upstream = 1,     // Backbone frame is activated for upstream frames
        Bidirectional = 2,// Backbone frame is activated for both upstream and downstream frames
    }

    public enum enRadioTestType
    {
        Packet = 0, // Transmit packets
        CM = 1,     // Continuous modulation
        CW = 2,     // Continuous wave
        Pkcca = 3,  // Packet test with clear channel assessment (CCA) enabled
    }

    public enum enPktPriority
    {
        Low = 0,    // Default packet priority
        Medium = 1, // Higher packet priority
        High = 2,   // Highest packet priority
    }

    public enum enPathDirection
    {
        None = 0,      // No path
        Unused = 1,    // Path is not used
        Upstream = 2,  // Upstream path
        Downstream = 3,// Downstream path 
    }

    public enum enAdvState
    {
        On = 0,     // Advertisement is on
        Off = 1,    // Advertisement is off
    }

    public enum enDnstreamFrameMode
    {
        Normal = 0,  // Normal downstream bandwidth
        Fast = 1,    // Fast downstream bandwidth
    }

    public enum enSetimeTrigMode
    {
        Immediate = 0,// Normal downstream bandwidth
        Fast = 1,     // Fast downstream bandwidth
    }

    public enum enCLIUserRole
    {
        Viewer = 0,  // Viewer-role user has read-only access to non-sensitive network information
        User = 1,    // User-role user has read-write privileges
    }

    public enum enMoteState
    {
        Lost = 0,           // Mote is not currently part of the network
        Negotiating = 1,    // Mote is in the process of joining the network
        Operational = 4,    // Mote is operational
    }

    public enum enNetState
    {
        Operational = 0,    // Network is operating normally
        Radiotest = 1,      // Manager is in radiotest mode
        NotStarted = 2,     // Waiting for startNetwork API command
        ErrorStartup = 3,   // Unexpected error occurred at startup
        ErrorConfig = 4,    // Invalid or not licensed configuration found at startup
        ErrorLicense = 5,   // Invalid license file found at startup
    }

    public enum enLinkFlags
    {
        Transmit = 0x01,
        Receive = 0x02,
        Shared = 0x04,
        Reserved = 0x08,
        Join = 0x10,
        Advertisement = 0x20,
        Discovery = 0x40,
        NoPathFailureDetection = 0x80,
    }

    public enum enCmd
    {
        CMID_START = 0x00,
        CMID_HELLO = 0x01,
        CMID_HELLO_RESPONSE = 0x02,
        CMID_MGR_HELLO = 0x03,
        /* 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x10, 0x11, 0x12, 0x13 */
        CMDID_NOTIFICATION = 0x14,
        CMDID_RESET = 0x15,
        CMDID_SUBSCRIBE = 0x16,
        CMDID_GETTIME = 0x17,
        /* 0x18, 0x19 */
        CMDID_SETNETWORKCONFIG = 0x1a,
        /* 0x1B, 0x1C, 0x1D, 0x1E */
        CMDID_CLEARSTATISTICS = 0x1f,
        /* 0x20 */
        CMDID_EXCHANGEMOTEJOINKEY = 0x21,
        CMDID_EXCHANGENETWORKID = 0x22,
        CMDID_RADIOTESTTX = 0x23,
        /* 0x24 */
        CMDID_RADIOTESTRX = 0x25,
        CMDID_GETRADIOTESTSTATISTICS = 0x26,
        CMDID_SETACLENTRY = 0x27,
        CMDID_GETNEXTACLENTRY = 0x28,
        CMDID_DELETEACLENTRY = 0x29,
        CMDID_PINGMOTE = 0x2a,
        CMDID_GETLOG = 0x2b,
        CMDID_SENDDATA = 0x2c,
        CMDID_STARTNETWORK = 0x2d,
        CMDID_GETSYSTEMINFO = 0x2e,
        CMDID_GETMOTECONFIG = 0x2f,
        CMDID_GETPATHINFO = 0x30,
        CMDID_GETNEXTPATHINFO = 0x31,
        CMDID_SETADVERTISING = 0x32,
        CMDID_SETDOWNSTREAMFRAMEMODE = 0x33,
        /* 0x34 */
        CMDID_GETMANAGERSTATISTICS = 0x35,
        CMDID_SETTIME = 0x36,
        CMDID_GETLICENSE = 0x37,
        CMDID_SETLICENSE = 0x38,
        /* 0x39 */
        CMDID_SETCLIUSER = 0x3a,
        CMDID_SENDIP = 0x3b,
        /* 0x3C */
        CMDID_RESTOREFACTORYDEFAULTS = 0x3d,
        CMDID_GETMOTEINFO = 0x3e,
        CMDID_GETNETWORKCONFIG = 0x3f,
        CMDID_GETNETWORKINFO = 0x40,
        CMDID_GETMOTECONFIGBYID = 0x41,
        CMDID_SETCOMMONJOINKEY = 0x42,
        CMDID_GETIPCONFIG = 0x43,
        CMDID_SETIPCONFIG = 0x44,
        CMDID_DELETEMOTE = 0x45,
        CMDID_GETMOTELINKS = 0x46,
        CMDID_END = 0x47,
    }
    /// <summary>
    /// 网络通知类型定义
    /// </summary>
    public enum enNotifyType
    {
        NOTIFID_NOTIFEVENT = 0x01,
        NOTIFID_NOTIFLOG = 0x02,
        NOTIFID_NOTIFDATA = 0x04,
        NOTIFID_NOTIFIPDATA = 0x05,
        NOTIFID_NOTIFHEALTHREPORT = 0x06,
        NOTIFID_NULL = 0xFF,
    }
    /// <summary>
    /// 通知事件类型定义
    /// </summary>
    public enum enEventType
    {
        EVENTID_EVENTMOTERESET = 0x00,
        EVENTID_EVENTNETWORKRESET = 0x01,
        EVENTID_EVENTCOMMANDFINISHED = 0x02,
        EVENTID_EVENTMOTEJOIN = 0x03,
        EVENTID_EVENTMOTEOPERATIONAL = 0x04,
        EVENTID_EVENTMOTELOST = 0x05,
        EVENTID_EVENTNETWORKTIME = 0x06,
        EVENTID_EVENTPINGRESPONSE = 0x07,
        EVENTID_EVENTPATHCREATE = 0x0a,
        EVENTID_EVENTPATHDELETE = 0x0b,
        EVENTID_EVENTPACKETSENT = 0x0c,
        EVENTID_EVENTMOTECREATE = 0x0d,
        EVENTID_EVENTMOTEDELETE = 0x0e,
        EVENTID_NULL = 0xFF,
    }

    public enum enHRID
    {
        HRID_DEVICE = 0x80,                 // Device Health Report
        HRID_NEIGHBOR = 0x81,               // Neighbors' Health Report
        HRID_DISCOVERED_NEIGHBOR = 0x82,    // Discovered Neighbors Health Report
        HRID_NULL = 0xFF,
    }
}
