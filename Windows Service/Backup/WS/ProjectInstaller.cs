using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace WS
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;

            serviceInstaller1.DisplayName = "Haiver_ServicioDePrueba";
            serviceInstaller1.ServiceName = "Haiver_ServicioDePrueba";
            serviceInstaller1.Description = "Este es Un Servicio de Prueba de Haiver";
            serviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
        }
    }
}
