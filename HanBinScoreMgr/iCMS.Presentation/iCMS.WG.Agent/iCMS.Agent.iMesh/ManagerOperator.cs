using iMesh.IO.SUSI;
using iMesh.IO.ZLAN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iMesh
{
    internal class ManagerOperator
    {
        private Serial m_Ser = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ser">绑定的串行层对象</param>
        public ManagerOperator(Serial ser = null)
        {
            m_Ser = ser;
        }

        /// <summary>
        /// 硬件重启Manager
        /// </summary>
        public void ResetManager()
        {
            int cfgHwType = int.Parse(System.Configuration.ConfigurationManager.AppSettings["WgHwType"].ToString());
            int cfgResetType = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ResetType"].ToString());

            // WG硬件方案是卓岚串转以太网模块ZLSN2002
            if (cfgHwType == 0)
            {
                // IO硬件方式重启Manager
                if (cfgResetType == 0)
                {
                    ControlZlanGPIO ZlanGPIO = new ControlZlanGPIO();
                    ZlanGPIO.SetLowLevel();
                    Thread.Sleep(3);
                    ZlanGPIO.SetHighLevel();
                }
                // ZLSN2002只支持IO硬件方式重启Manager
                else
                {
                    throw new Exception("Invalid ResetType when ZLSN2002 in configure file");
                }
            }
            // WG硬件方案是研华工业单板MIO5251EW
            else if (cfgHwType == 1)
            {
                // IO硬件方式重启Manager
                if (cfgResetType == 0)
                {
                    ControlSusiGPIO SusiGPIO = new ControlSusiGPIO();
                    SusiGPIO.SetLowLevel();
                    Thread.Sleep(3);
                    SusiGPIO.SetHighLevel();
                }
                // 软件命令方式重启Manager
                else if (cfgResetType == 1)
                {
                    m_Ser.ResetManager();
                }
                else
                {
                    throw new Exception("Invalid ResetType in configure file");
                }
            }
            // WG硬件方案是有人串转以太网模块USRTCP232E2，或者为Manager评估板
            else if (cfgHwType == 2)
            {
                if (cfgResetType == 1)
                {
                    m_Ser.ResetManager();
                }
                else
                {
                    throw new Exception("Invalid ResetType when USRTCP232E2 in configure file");
                }
            }
            else if (cfgHwType == 3)
            {
                if (cfgResetType == 1)
                {
                    m_Ser.ResetManager();
                }
                else
                {
                    throw new Exception("Invalid ResetType when manager evo board in configure file");
                }
            }
            else
            {
                throw new Exception("Invalid WgHwType in configure file");
            }
        }

        /// <summary>
        /// 硬件使能稳定Manager
        /// </summary>
        public void StabilizeManager()
        {
            int cfgHwType = int.Parse(System.Configuration.ConfigurationManager.AppSettings["WgHwType"].ToString());
            if (cfgHwType == 0)
            {
                ControlZlanGPIO ZlanGPIO = new ControlZlanGPIO();
                ZlanGPIO.SetHighLevel();
            }
            else if (cfgHwType == 1)
            {
                ControlSusiGPIO SusiGPIO = new ControlSusiGPIO();
                SusiGPIO.SetHighLevel();
            }
            else if (cfgHwType == 2 || cfgHwType == 3)
            {
                ;
            }
            else
            {
                throw new Exception("Invalid WgHwType in configure file");
            }
        }
    }
}