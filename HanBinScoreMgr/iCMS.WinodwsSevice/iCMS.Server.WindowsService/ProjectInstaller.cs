using HanBin.Common.Component.Tool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace HanBin.Server.WindowsService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();

            using (SettingHelper setting = new SettingHelper())
            {
                //系统用于标志此服务名称(唯一性)
                ServerInstaller.ServiceName = setting.ServiceName;
                //向用户标志服务的显示名称(可以重复)
                ServerInstaller.DisplayName = setting.DisplayName;
                //服务的说明(描述)
                ServerInstaller.Description = setting.Description;
            }
        }
    }
}
