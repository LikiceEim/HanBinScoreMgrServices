using HanBin.Common.Component.Tool;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace HanBin.Server.WindowsService
{
    partial class WCFStartupService : ServiceBase
    {
        public WCFStartupService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
            UnityServiceHostGroup.StartAllConfigureService();
        }

        protected override void OnStop()
        {
            UnityServiceHostGroup.CloseAllService();
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
        }
    }
}
