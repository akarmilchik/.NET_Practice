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

            serviceInstaller = new ServiceInstaller
            {
                StartType = ServiceStartMode.Manual,
                ServiceName = "ManagerFileWatchService"
            };

            processInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            };

            Installers.Add(processInstaller);

            Installers.Add(serviceInstaller);
        }
    }
}