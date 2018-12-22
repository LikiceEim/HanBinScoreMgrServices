using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Reflection;
using EA86.ini;

namespace Susi4.Plugin
{
    public class DemoConfig
    {
        private string OEMPath;
        private string ConfigFile;
        private string DefaultLogoFile;

        private const string SECTION_DEMO4_PAGE = "Susi4DemoPage";
        private const string SECTION_DEMO4_LOGO = "Susi4DemoLogo";

        public bool HideWDT;
        public bool HideHWM;
        public bool HideGPIO;
        public bool HideSMBus;
        public bool HideIIC;
        public bool HideSmartFan;
        public bool HideVGA;
        public bool HideStorage;
        public bool HideThermalProtection;
        public bool HideInformation;

        public Image Logo;
        public Point LogoLocation;
        public Size LogoSize;

        public DemoConfig()
        {
            OEMPath = Path.Combine(Environment.GetEnvironmentVariable("windir"), "SUSI");
            ConfigFile = Path.Combine(OEMPath, "config.ini");
            DefaultLogoFile = Path.Combine(OEMPath, "Logo.png");

            if (LoadConfig() == false)
            {
                HideWDT = false;
                HideHWM = false;
                HideGPIO = false;
                HideSMBus = false;
                HideIIC = false;
                HideSmartFan = false;
                HideVGA = false;
                HideStorage = false;
                HideThermalProtection = false;
                HideInformation = false;
            }

            if (Logo == null && File.Exists(DefaultLogoFile))
            {
                Logo = Image.FromFile(DefaultLogoFile);
            }
        }

        private bool LoadConfig()
        {
            EZini ini = new EZini(ConfigFile);

            if (ini.Exists() == false)
                return false;

            ini.Load();

            if (ini.HasSection(SECTION_DEMO4_PAGE))
            {
                string tmp;

                if (ini[SECTION_DEMO4_PAGE].TryGetValue("HideWDT", out tmp))
                    HideWDT = tmp == "1" ? true : false;

                if (ini[SECTION_DEMO4_PAGE].TryGetValue("HideHWM", out tmp))
                    HideHWM = tmp == "1" ? true : false;

                if (ini[SECTION_DEMO4_PAGE].TryGetValue("HideGPIO", out tmp))
                    HideGPIO = tmp == "1" ? true : false;

                if (ini[SECTION_DEMO4_PAGE].TryGetValue("HideSMBus", out tmp))
                    HideSMBus = tmp == "1" ? true : false;

                if (ini[SECTION_DEMO4_PAGE].TryGetValue("HideIIC", out tmp))
                    HideIIC = tmp == "1" ? true : false;

                if (ini[SECTION_DEMO4_PAGE].TryGetValue("HideSmartFan", out tmp))
                    HideSmartFan = tmp == "1" ? true : false;

                if (ini[SECTION_DEMO4_PAGE].TryGetValue("HideVGA", out tmp))
                    HideVGA = tmp == "1" ? true : false;

                if (ini[SECTION_DEMO4_PAGE].TryGetValue("HideStorage", out tmp))
                    HideStorage = tmp == "1" ? true : false;

                if (ini[SECTION_DEMO4_PAGE].TryGetValue("HideThermalProtection", out tmp))
                    HideThermalProtection = tmp == "1" ? true : false;

                if (ini[SECTION_DEMO4_PAGE].TryGetValue("HideInformation", out tmp))
                    HideInformation = tmp == "1" ? true : false;
            }

            if (ini.HasSection(SECTION_DEMO4_LOGO))
            {
                string tmp;

                if (ini[SECTION_DEMO4_PAGE].TryGetValue("OEMEnable", out tmp))
                {
                    if (tmp == "1")
                    {
                        if (ini[SECTION_DEMO4_PAGE].TryGetValue("Path", out tmp))
                        {
                            if (tmp != "")
                            {
                                if (File.Exists(tmp))
                                {
                                    Logo = Image.FromFile(tmp);
                                }
                            }
                        }
                    }
                }

                if (Logo != null)
                {
                    try
                    {
                        if (ini[SECTION_DEMO4_PAGE].TryGetValue("X", out tmp))
                        {
                            int x = Convert.ToInt32(tmp);

                            if (ini[SECTION_DEMO4_PAGE].TryGetValue("Y", out tmp))
                            {
                                int y = Convert.ToInt32(tmp);
                                LogoLocation = new Point(x, y);
                            }
                        }

                        if (ini[SECTION_DEMO4_PAGE].TryGetValue("Width", out tmp))
                        {
                            int w = Convert.ToInt32(tmp);

                            if (ini[SECTION_DEMO4_PAGE].TryGetValue("Height", out tmp))
                            {
                                int h = Convert.ToInt32(tmp);
                                LogoSize = new Size(w, h);
                            }
                        }
                    }
                    catch
                    {
                        ;
                    }
                }
            }

            return true;
        }
    }

    public interface ISusiHost
    {
        DemoConfig Config { get; }
    }

    public interface ISusiPlugin
    {
        ISusiHost Host { get; set; }
        string Name { get; }
        string Description { get; }
        string PluginVersion { get; }
        string InterfaceVersion { get; }

        System.Drawing.Image Icon { get; }
        bool Enable { get; }

        //System.Windows.Forms.UserControl MainInterface { get; }

        void OnCreate();
        void OnStart();
        void OnStop();
    }
}
