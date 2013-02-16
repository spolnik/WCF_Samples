using System;
using System.ServiceModel;

namespace WcfSample.Hosting.SelfHost
{
    class Program
    {
        static void Main()
        {
            var serviceHost = new ServiceHost(typeof(CalculatorService.CalculatorService));

            try
            {
                serviceHost.Open();
                serviceHost.PrintServiceInfo();

                Console.WriteLine("Type [Enter] to close...");
                Console.ReadLine();
                serviceHost.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                serviceHost.Abort();
            }
        }
    }
}
