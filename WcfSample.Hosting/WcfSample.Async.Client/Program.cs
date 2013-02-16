using System;
using System.ServiceModel;

namespace WcfSample.Async.Client
{
    class Program
    {
        private const string OutputInfo = "3.33 + 4.44 = {0}";

        static void Main()
        {
            Console.WriteLine("Welcome in WcfSample Hosting client \n\t#Service Reference Sample");
            Console.ReadLine();

            var entityIIS = new CalculatorServiceIISReference.CalculatorEntity
            {
                FirstValue = 3.33,
                Calculation = CalculatorServiceIISReference.Calculation.Add,
                SecondValue = 4.44
            };

            var entitySelfHosted = new CalculatorServiceSelfHostReference.CalculatorEntity
            {
                FirstValue = 3.33,
                Calculation = CalculatorServiceSelfHostReference.Calculation.Add,
                SecondValue = 4.44
            };

            ExecuteIISSample(entityIIS);
            ExecuteSelfHostedSample(entitySelfHosted);
            ExecuteDivideByZero(entityIIS);

            Console.WriteLine("Press [Enter] to exit...");
            Console.ReadLine();
        }

        private static void ExecuteDivideByZero(CalculatorServiceIISReference.CalculatorEntity entity)
        {
            Console.WriteLine("\n\tSelf-hosting sample - divide by zero");
            Console.WriteLine();

            var iisServiceClient = new CalculatorServiceIISReference.CalculatorServiceClient("WSHttpBinding_CalculatorService");
            entity.Calculation = CalculatorServiceIISReference.Calculation.Divide;
            entity.SecondValue = 0;
            
            iisServiceClient.EvalCompleted += IISServiceClientEvalCompleted;
            iisServiceClient.EvalAsync(entity);
        }

        private static void IISServiceClientEvalCompleted(object sender, CalculatorServiceIISReference.EvalCompletedEventArgs e)
        {
            try
            {
                Console.WriteLine(OutputInfo, e.Result);
                ((ICommunicationObject)sender).Close();
            }
            catch (FaultException fe)
            {
                Console.WriteLine("FaultException: {0}", fe);
                ((ICommunicationObject)sender).Abort();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("CommunicationException: {0}", ce);
                ((ICommunicationObject)sender).Abort();
            }
            catch (TimeoutException te)
            {
                Console.WriteLine("TimeoutException: {0}", te);
                ((ICommunicationObject)sender).Abort();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ((ICommunicationObject)sender).Abort();
            }
        }

        private static void ExecuteSelfHostedSample(CalculatorServiceSelfHostReference.CalculatorEntity entity)
        {
            Console.WriteLine("\n\tSelf-hosting sample.");
            Console.WriteLine();

            var selfHostServiceClient = new CalculatorServiceSelfHostReference.CalculatorServiceClient("WSHttpBinding_CalculatorService1");

            selfHostServiceClient.EvalCompleted += SelfHostServiceClientEvalCompleted;
            selfHostServiceClient.EvalAsync(entity);
        }

        private static void SelfHostServiceClientEvalCompleted(object sender, CalculatorServiceSelfHostReference.EvalCompletedEventArgs e)
        {
            try
            {
                Console.WriteLine(OutputInfo, e.Result);
                ((ICommunicationObject)sender).Close();
            }
            catch (FaultException fe)
            {
                Console.WriteLine("FaultException: {0}", fe);
                ((ICommunicationObject)sender).Abort();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("CommunicationException: {0}", ce);
                ((ICommunicationObject)sender).Abort();
            }
            catch (TimeoutException te)
            {
                Console.WriteLine("TimeoutException: {0}", te);
                ((ICommunicationObject)sender).Abort();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ((ICommunicationObject)sender).Abort();
            }
        }

        private static void ExecuteIISSample(CalculatorServiceIISReference.CalculatorEntity entity)
        {
            Console.WriteLine("\n\tIIS Sample.");
            Console.WriteLine();

            var iisServiceClient = new CalculatorServiceIISReference.CalculatorServiceClient("WSHttpBinding_CalculatorService");
            iisServiceClient.EvalCompleted += IISServiceClientEvalCompleted;
            iisServiceClient.EvalAsync(entity);
        }
    }
}
