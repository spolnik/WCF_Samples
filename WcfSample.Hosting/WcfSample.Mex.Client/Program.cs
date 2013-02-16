using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using WcfSample.Hosting.CalculatorService.Common;

namespace WcfSample.Mex.Client
{
    class Program
    {
        private const string OutputInfo = "3.33 + 4.44 = {0}";

        static void Main()
        {
            var endpoints = MetadataResolver.Resolve(typeof(ICalculatorService),
                                                    new EndpointAddress("http://localhost:8088/CalculatorService/mex"));

            Console.WriteLine("Welcome in WcfSample Hosting client \n\t#Client Base Sample");
            Console.ReadLine();

            var entity = new CalculatorEntity
            {
                FirstValue = 3.33,
                Calculation = Calculation.Add,
                SecondValue = 4.44
            };

            Console.WriteLine("\n\tMex Sample.");
            Console.WriteLine();

            var clientSelfHosting = new CalculatorServiceClient(endpoints[0].Binding, endpoints[0].Address);
            var result = clientSelfHosting.Invoke(clientSelfHosting.Eval, entity);

            if (result.Success)
                Console.WriteLine(OutputInfo, result.Output);

            Console.WriteLine("Press [Enter] to exit...");
            Console.ReadLine();
        }
    }
}
