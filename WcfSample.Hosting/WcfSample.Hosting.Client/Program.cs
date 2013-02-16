using System;
using WcfSample.Hosting.CalculatorService.Common;
using WcfSample.Hosting.Client.CalculatorServiceIISReference;

namespace WcfSample.Hosting.Client
{
    class Program
    {
        private const string OutputInfo = "3.33 + 4.44 = {0}";

        static void Main()
        {
            Console.WriteLine("Welcome in WcfSample Hosting client \n\t#Service Reference Sample");
            Console.ReadLine();

            var entity = new CalculatorEntity {
                                                  FirstValue = 3.33,
                                                  Calculation = Calculation.Add,
                                                  SecondValue = 4.44
                                              };

            ExecuteIISSample(entity);
            ExecuteSelfHostedSample(entity);
            ExecuteDivideByZero(entity);

            Console.WriteLine("Press [Enter] to exit...");
            Console.ReadLine();
        }

        private static void ExecuteDivideByZero(CalculatorEntity entity)
        {
            Console.WriteLine("\n\tSelf-hosting sample - divide by zero");
            Console.WriteLine();

            CalculatorServiceClient iisServiceClient = new CalculatorServiceClient("WSHttpBinding_CalculatorService");
            entity.Calculation = Calculation.Divide;
            entity.SecondValue = 0;
            ServiceInvoker.ServiceResult<double> result = iisServiceClient.Invoke(iisServiceClient.Eval, entity);

            if (result.Success)
                Console.WriteLine(OutputInfo, result.Output);
        }

        private static void ExecuteSelfHostedSample(CalculatorEntity entity)
        {
            Console.WriteLine("\n\tSelf-hosting sample.");
            Console.WriteLine();

            var selfHostServiceClient = new CalculatorServiceSelfHostReference.CalculatorServiceClient("WSHttpBinding_CalculatorService1");
            var result = selfHostServiceClient.Invoke(selfHostServiceClient.Eval, entity);

            if (result.Success)
                Console.WriteLine(OutputInfo, result);
        }

        private static void ExecuteIISSample(CalculatorEntity entity)
        {
            Console.WriteLine("\n\tIIS Sample.");
            Console.WriteLine();

            var iisServiceClient = new CalculatorServiceClient("WSHttpBinding_CalculatorService");
            var result = iisServiceClient.Invoke(iisServiceClient.Eval, entity);

            if (result.Success)
                Console.WriteLine(OutputInfo, result);
        }
    }
}
