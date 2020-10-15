using DBC.Infrastructure.IoC;
using DBC.WcfServiceLibrary;
using System;

namespace DBC.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var provider = Startup.GetProvider();

            var MunicipalityTaxHost = new IocServiceHost(provider, typeof(MunicipalityTaxService));
            MunicipalityTaxHost.Open();

            var MunicipalityTaxRuleHost = new IocServiceHost(provider, typeof(MunicipalityTaxRuleService));
            MunicipalityTaxRuleHost.Open();

            Console.ReadLine();
        }
    }
}
