using System;
using System.ServiceModel;
using WcfSample.Hosting.CalculatorService.Common;

namespace WcfSample.ChannelFactory.Client
{
    class Program
    {
        private const string OutputInfo = "3.33 + 4.44 = {0}";

        static void Main()
        {
            Console.WriteLine("Welcome in WcfSample Hosting client \n\t#Channel Factory Sample");
            Console.ReadLine();

            var entity = new CalculatorEntity
            {
                FirstValue = 3.33,
                Calculation = Calculation.Add,
                SecondValue = 4.44
            };

            ExecuteIISSample(entity);
            SelfHostedSample(entity);
            DivideByZeroSample(entity);

            Console.WriteLine("Press [Enter] to exit...");
            Console.ReadLine();
        }

        private static void DivideByZeroSample(CalculatorEntity entity)
        {
            Console.WriteLine("\n\tSelf-hosting sample - divide by zero");
            Console.WriteLine();

            var factorySelfHosted = new ChannelFactory<ICalculatorServiceChannel>("WSHttpBinding_CalculatorService_SelfHosted");
            ICalculatorServiceChannel channelSelfHosted = factorySelfHosted.CreateChannel();
            entity.Calculation = Calculation.Divide;
            entity.SecondValue = 0;
            ServiceInvoker.ServiceResult<double> result = channelSelfHosted.Invoke(channelSelfHosted.Eval, entity);

            if (result.Success)
                Console.WriteLine(OutputInfo, result.Output);
        }

        private static void SelfHostedSample(CalculatorEntity entity)
        {
            Console.WriteLine("\n\tSelf-hosting sample.");
            Console.WriteLine();
            
            var factorySelfHosted = new ChannelFactory<ICalculatorServiceChannel>("WSHttpBinding_CalculatorService_SelfHosted");
            var channelSelfHosted = factorySelfHosted.CreateChannel();

            ServiceInvoker.ServiceResult<double> result = channelSelfHosted.Invoke(channelSelfHosted.Eval, entity);
            
            if (result.Success)
                Console.WriteLine(OutputInfo, result);
        }

        private static void ExecuteIISSample(CalculatorEntity entity)
        {
            Console.WriteLine("\n\tIIS Sample.");
            Console.WriteLine();
            
            var factoryIIS = new ChannelFactory<ICalculatorServiceChannel>("WSHttpBinding_CalculatorService_IIS");
            var channelIIS = factoryIIS.CreateChannel();

            var result = channelIIS.Invoke(channelIIS.Eval, entity);

            if (result.Success)
                Console.WriteLine(OutputInfo, result);
        }
    }
}
