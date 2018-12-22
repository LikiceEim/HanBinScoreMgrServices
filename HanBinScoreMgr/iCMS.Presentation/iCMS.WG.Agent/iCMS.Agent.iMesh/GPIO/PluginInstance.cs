using System;
using System.Collections.Generic;
using System.Text;

namespace Susi4.Plugin
{
    public class SusiPlugin : ISusiPlugin
    {
        private string PluginPath = "";

        public SusiPlugin(string path)
        {
            PluginPath = path;
        }

        #region ISusiPlugin Variables
        private ISusiHost myHost = null;
        private string myName = "GPIO";
        private string myDescription = "";
        private string myPluginVersion = "1.0.0.0";
        private string myInterfaceVersion = "1.0.0.0";
        private bool myEnable = true;
        //private System.Windows.Forms.UserControl myMainInterface = new ctlMain();
        private System.Drawing.Image myIcon = null;

        public string Name
        {
            get { return myName; }
        }
       
        public string Description
        {
            get { return myDescription; }
        }

        public string PluginVersion
        {
            get { return myPluginVersion; }
        }
                
        public string InterfaceVersion
        {
            get { return myInterfaceVersion; }
        }

        public ISusiHost Host
        {
            set { myHost = value; }
            get { return myHost; }
        }

        //public System.Windows.Forms.UserControl MainInterface
        //{
        //    get { return myMainInterface; }
        //}

        public bool Enable
        {
            get { return myEnable; }
        }

        public System.Drawing.Image Icon
        {
            get { return myIcon; }
        }
        #endregion

        #region ISusiPlugin State Mechine Functions
        public void OnCreate()
        {
            if (Host.Config.HideGPIO)
            {
                this.myEnable = false;
            }
            else
            {
                //ctlMain main = MainInterface as ctlMain;

                //if (main.Available == false)
                //{
                //    this.myEnable = false;
                //}
            }
        }

        public void OnStart()
        {

        }

        public void OnStop()
        {

        }
        #endregion
    }
}
