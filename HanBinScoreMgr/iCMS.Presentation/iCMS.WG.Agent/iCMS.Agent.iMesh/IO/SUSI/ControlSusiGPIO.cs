﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Susi4.APIs;
using System.Threading;

namespace iMesh.IO.SUSI
{
    public class ControlSusiGPIO
    {
        private bool bSusiExits = false;
        private bool bSusiIoctl = false;

        private class DeviceInfo
        {
            public UInt32 ID;
            public UInt32 SupportInput;
            public UInt32 SupportOutput;

            public DeviceInfo(UInt32 DeviceID)
            {
                ID = DeviceID;
                SupportInput = 0;
                SupportOutput = 0;
            }
        }

        private class DevPinInfo
        {
            public UInt32 ID;

            private string _Name = "";
            public string Name
            {
                get { return _Name; }
            }
            override public string ToString()
            {
                return String.Format("{0} ({1})", ID, Name);
            }

            public DevPinInfo(UInt32 DeviceID)
            {
                ID = DeviceID;

                UInt32 Length = 32;
                StringBuilder sb = new StringBuilder((int)Length);
                if (SusiBoard.SusiBoardGetStringA(SusiBoard.SUSI_ID_MAPPING_GET_NAME_GPIO(ID), sb, ref Length) == SusiStatus.SUSI_STATUS_SUCCESS)
                {
                    _Name = sb.ToString();
                }
            }
        }

        List<DeviceInfo> DevList = new List<DeviceInfo>();
        List<DevPinInfo> DevPinList = new List<DevPinInfo>();

        /// <summary>
        /// 构造函数初始化
        /// </summary>
        public ControlSusiGPIO()
        {
            bSusiExits = (int.Parse(System.Configuration.ConfigurationManager.AppSettings["WgHwType"].ToString()) == 0) ? false : true;
            bSusiIoctl = (int.Parse(System.Configuration.ConfigurationManager.AppSettings["IoCntl"].ToString()) == 0) ? false : true;

            if (bSusiExits)
            {
                InitSusiLib();
                InitializeGPIO();
                InitializePins();
            }
        }

        /// <summary>
        /// 初始化Susi
        /// </summary>
        /// <returns></returns>
        private void InitSusiLib()
        {
            UInt32 Status = SusiStatus.SUSI_STATUS_SUCCESS;
            try
            {
                Status = SusiLib.SusiLibInitialize();
            }
            catch
            {
                throw new Exception("Call SusiLibInitialize exception, maybe susi is not installed");
            }

            if (Status != SusiStatus.SUSI_STATUS_SUCCESS)
                throw new Exception("Call SusiLibInitialize Failed");
        }

        /// <summary>
        /// 初始化GPIO输出信息
        /// </summary>
        private void InitializeGPIO()
        {
            UInt32 Status;
            int bankNum = 0;
            DeviceInfo info = new DeviceInfo(SusiGPIO.SUSI_ID_GPIO_BANK((UInt32)bankNum));

            Status = SusiGPIO.SusiGPIOGetCaps(info.ID, SusiGPIO.SUSI_ID_GPIO_OUTPUT_SUPPORT, out info.SupportOutput);
            if (Status != SusiStatus.SUSI_STATUS_SUCCESS)
                throw new Exception("Call SusiGPIOGetCaps Failed");
            else
                DevList.Add(info);
        }

        /// <summary>
        /// 初始化PIN口信息
        /// </summary>
        private void InitializePins()
        {
            StringBuilder sb = new StringBuilder(32);
            UInt32 mask;
            // 32 pins per bank
            for (int j = 0; j < 32; j++)
            {
                mask = (UInt32)(1 << 0);
                if ((DevList[0].SupportInput & mask) > 0 || (DevList[0].SupportOutput & mask) > 0)
                {
                    DevPinInfo pinInfo = new DevPinInfo((UInt32)((0 << 5) + j));
                    DevPinList.Add(pinInfo);
                }
            }
        }

        /// <summary>
        /// 设置引脚电平低位
        /// </summary>
        /// <param name="ID">引脚ID</param>
        /// <returns>设置成功与否</returns>
        public bool SetLowLevel(UInt32 ID = 0)
        {
            if (bSusiIoctl)
            {
                UInt32 Status;
                UInt32 Value = Convert.ToUInt32("1", 2);
                Status = SusiGPIO.SusiGPIOSetLevel(ID, 1, Value);
                if (Status != SusiStatus.SUSI_STATUS_SUCCESS)
                    return false;
                Thread.Sleep(500);
            }
            return true;
        }

        /// <summary>
        /// 设置引脚电平高位
        /// </summary>
        /// <param name="ID">引脚ID</param>
        /// <returns>设置成功与否</returns>
        public bool SetHighLevel(UInt32 ID = 0)
        {
            if (bSusiIoctl)
            {
                UInt32 Status;
                UInt32 Value = Convert.ToUInt32("0", 2);
                Status = SusiGPIO.SusiGPIOSetLevel(ID, 1, Value);
                if (Status != SusiStatus.SUSI_STATUS_SUCCESS)
                    return false;
                Thread.Sleep(500);
            }
            return true;
        }
    }
}