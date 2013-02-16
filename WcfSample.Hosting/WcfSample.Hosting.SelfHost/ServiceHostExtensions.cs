using System;
using System.ServiceModel;

namespace WcfSample.Hosting.SelfHost
{
    public static class ServiceHostExtensions
    {
        public static void PrintServiceInfo(this ServiceHost serviceHost)
        {
            Console.WriteLine("{0} is up and running whit these endpoints:",
                serviceHost.Description.ServiceType);

            foreach (var endpoint in serviceHost.Description.Endpoints)
                Console.WriteLine(endpoint.Address);
        }
    }
}