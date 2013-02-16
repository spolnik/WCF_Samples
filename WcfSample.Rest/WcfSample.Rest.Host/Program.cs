using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Wcf.Rest.Service;

namespace WcfSample.Rest.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new WebServiceHost(typeof(EvalService));

            try
            {
                host.Open();

                PrintServiceInfo(host);

                Console.WriteLine();
                Console.WriteLine("Press [ENTER] to shutdown the server...");
                Console.ReadLine();

                host.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                host.Abort();
            }
        }

        private static void PrintServiceInfo(ServiceHostBase host)
        {
            Console.WriteLine("{0} is up and running with these endpoints:",
                host.Description.ServiceType);

            foreach (var endpoint in host.Description.Endpoints)
                Console.WriteLine(endpoint.Address);
        }
    }
}
