using System;
using WcfSample.Hosting.CalculatorService.Common;

namespace WcfSample.ClientBase.Client
{
    class Program
    {
        private const string OutputInfo = "3.33 + 4.44 = {0}";

        static void Main()
        {
            Console.WriteLine("Welcome in WcfSample Hosting client \n\t#Client Base Sample");
            Console.ReadLine();

            var entity = new CalculatorEntity
            {
                FirstValue = 3.33,
                Calculation = Calculation.Add,
                SecondValue = 4.44
            };

            ExecuteIISSample(entity);
            ExecuteDynamicProxy(entity);
            ExecuteSelfHostedSample(entity);
            ExecuteDivideByZeroSample(entity);

            Console.WriteLine("Press [Enter] to exit...");
            Console.ReadLine();
        }

        private static void ExecuteDivideByZeroSample(CalculatorEntity entity)
        {
            Console.WriteLine("\tSelf-hosting sample - divide by zero");
            Console.WriteLine();

            CalculatorServiceClient clientSelfHosting = new CalculatorServiceClient("WSHttpBinding_CalculatorService_SelfHosted");
            entity.Calculation = Calculation.Divide;
            entity.SecondValue = 0;
            ServiceInvoker.ServiceResult<double> result = clientSelfHosting.Invoke(clientSelfHosting.Eval, entity);

            if (result.Success)
                Console.WriteLine(OutputInfo, result.Output);
        }

        private static void ExecuteSelfHostedSample(CalculatorEntity entity)
        {
            Console.WriteLine("\n\tSelf-hosting sample.");
            Console.WriteLine();
            
            
            var clientSelfHosting = new CalculatorServiceClient("WSHttpBinding_CalculatorService_SelfHosted");
            ServiceInvoker.ServiceResult<double> result = clientSelfHosting.Invoke(clientSelfHosting.Eval, entity);
            Console.WriteLine(OutputInfo, result);
        }

        private static void ExecuteDynamicProxy(CalculatorEntity entity)
        {
            Console.WriteLine("\n\tService - using dynamic proxy");
            Console.WriteLine();

            var serviceResult =
                ServiceProxyFactory<ICalculatorService>.Create("WSHttpBinding_CalculatorService_IIS").Invoke(x => x.Eval(entity));
            Console.WriteLine(OutputInfo, serviceResult);
        }

        private static void ExecuteIISSample(CalculatorEntity entity)
        {
            Console.WriteLine("\n\tIIS Sample.");
            Console.WriteLine();
            
            var clientIIS = new CalculatorServiceClient("WSHttpBinding_CalculatorService_IIS");

            var result = clientIIS.Invoke(clientIIS.Eval, entity);
            
            if (result.Success)
                Console.WriteLine(OutputInfo, result.Output);
        }
    }
}
