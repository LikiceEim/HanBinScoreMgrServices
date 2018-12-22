namespace iCMS.Server.WindowsService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ServerProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.ServerInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // ServerProcessInstaller
            // 
            this.ServerProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.ServerProcessInstaller.Password = null;
            this.ServerProcessInstaller.Username = null;
            // 
            // ServerInstaller
            // 
            this.ServerInstaller.DelayedAutoStart = true;
            this.ServerInstaller.Description = "西安因联信息科技有限公司iCMS.Server服务 ®V 1.4.1.5";
            this.ServerInstaller.DisplayName = "iCMS.Server";
            this.ServerInstaller.ServiceName = "iCMS.ServerService";
            this.ServerInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ServerProcessInstaller,
            this.ServerInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ServerProcessInstaller;
        private System.ServiceProcess.ServiceInstaller ServerInstaller;
    }
}