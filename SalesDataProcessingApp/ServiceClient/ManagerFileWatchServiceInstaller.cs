using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace ServiceApp
{
    [RunInstaller(true)]
    public partial class ManagerFileWatchServiceInstaller : Installer
    {
        ServiceInstaller serviceInstaller;

        ServiceProcessInstaller processInstaller;

        public ManagerFileWatchServiceInstaller()
        {
            InitializeComponent();

            serviceInstaller = new ServiceInstaller();

            processInstaller = new ServiceProcessInstaller();

            processInstaller.Account = ServiceAccount.LocalSystem;

            serviceInstaller.StartType = ServiceStartMode.Manual;

            serviceInstaller.ServiceName = "ManagerFileWatchService";

            Installers.Add(processInstaller);

            Installers.Add(serviceInstaller);
        }
    }
}
