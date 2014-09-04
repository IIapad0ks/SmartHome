namespace SmartHome.SHService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.shProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.shInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // shProcessInstaller
            // 
            this.shProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.shProcessInstaller.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.shInstaller});
            this.shProcessInstaller.Password = null;
            this.shProcessInstaller.Username = null;
            // 
            // shInstaller
            // 
            this.shInstaller.Description = "Very good service ;)";
            this.shInstaller.DisplayName = "Smart Home Service";
            this.shInstaller.ServiceName = "SHService";
            this.shInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.shProcessInstaller});

        }

        #endregion

        public System.ServiceProcess.ServiceProcessInstaller shProcessInstaller;
        public System.ServiceProcess.ServiceInstaller shInstaller;
    }
}