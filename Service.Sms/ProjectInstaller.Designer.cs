namespace BT.Service.ResourceCount
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
            this.serviceInstallerHomorySmsService = new System.ServiceProcess.ServiceInstaller();
            this.serviceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            // 
            // serviceInstallerHomorySmsService
            // 
            this.serviceInstallerHomorySmsService.Description = "短信收发";
            this.serviceInstallerHomorySmsService.DisplayName = "LYSmsService";
            this.serviceInstallerHomorySmsService.ServiceName = "LYSmsService";
            this.serviceInstallerHomorySmsService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // serviceProcessInstaller
            // 
            this.serviceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller.Password = null;
            this.serviceProcessInstaller.Username = null;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceInstallerHomorySmsService,
            this.serviceProcessInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceInstaller serviceInstallerHomorySmsService;
        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller;
    }
}