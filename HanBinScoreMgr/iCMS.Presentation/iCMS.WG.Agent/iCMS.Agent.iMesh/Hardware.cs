using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace iMesh
{
    internal class Hardware
    {
        private static ControlSusiGPIO GPIO = new ControlSusiGPIO();

#if true
        /// <summary>
        /// 硬件重启Manager
        /// </summary>
        internal static void ResetManager()
        {
            GPIO.SetLowLevel();
            Thread.Sleep(3);
            GPIO.SetHighLevel();
        }
        /// <summary>
        /// 硬件使能稳定Manager
        /// </summary>
        internal static void StabilizeManager()
        {
            GPIO.SetHighLevel();
        }
#else
        /// <summary>
        /// 硬件重启Manager
        /// </summary>
        internal static void ResetManager()
        {
            GPIO.SetHighLevel();
            Thread.Sleep(3);
            GPIO.SetLowLevel();
        }
        /// <summary>
        /// 硬件使能稳定Manager
        /// </summary>
        internal static void StabilizeManager()
        {
            GPIO.SetLowLevel();
        }
#endif
    }
}
