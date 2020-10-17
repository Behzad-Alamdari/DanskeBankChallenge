using DBC.Infrastructure.IoC;
using DBC.WcfServices;
using System.ServiceProcess;

namespace DBC.WindowsServiceHost
{
    public partial class MunicipalityTaxService : ServiceBase
    {
        private IocServiceHost _municipalityTaxHost;

        public MunicipalityTaxService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var provider = Startup.GetProvider();
            Startup.EnsureDatabaseExistance(provider);

            _municipalityTaxHost = new IocServiceHost(provider, typeof(MunicipalityTaxManager));
            _municipalityTaxHost.Open();
        }

        protected override void OnStop()
        {
            _municipalityTaxHost.Close();
        }
    }
}
