using DBC.Infrastructure.IoC;
using DBC.WcfServices;
using System;

namespace DBC.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var provider = Startup.GetProvider();
            Startup.EnsureDatabaseExistance(provider);

            var MunicipalityTaxHost = new IocServiceHost(provider, typeof(MunicipalityTaxService));
            MunicipalityTaxHost.Open();

            Console.WriteLine("The host is listening on http://localhost:8733/Design_Time_Addresses/MunicipalityTaxs/");
            Console.ReadLine();

            MunicipalityTaxHost.Close();
        }
    }
}
