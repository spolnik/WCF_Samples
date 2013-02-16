using System;
using System.ServiceModel;

namespace WcfSample.Hosting.CalculatorService.Common
{
    public static class ServiceInvoker
    {
        public static ServiceResult<TOut> Invoke<TIn, TOut>(this ICommunicationObject service, 
            Func<TIn, TOut> func, TIn arg)
        {
            try
            {
                TOut result = func(arg);
                service.Close();

                return ServiceResult<TOut>.CreateSuccess(result);
            }
            catch (FaultException fe)
            {
                Console.WriteLine("FaultException: {0}", fe);
                service.Abort();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("CommunicationException: {0}", ce);
                service.Abort();
            }
            catch (TimeoutException te)
            {
                Console.WriteLine("TimeoutException: {0}", te);
                service.Abort();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                service.Abort();
            }

            return ServiceResult<TOut>.CreateFailure();
        }

        public static ServiceResult<TOut> Invoke<TIn0, TIn1, TOut>(this ICommunicationObject service,
            Func<TIn0, TIn1, TOut> func, TIn0 arg0, TIn1 arg1)
        {
            try
            {
                TOut result = func(arg0, arg1);
                service.Close();

                return ServiceResult<TOut>.CreateSuccess(result);
            }
            catch (FaultException fe)
            {
                Console.WriteLine("FaultException: {0}", fe);
                service.Abort();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("CommunicationException: {0}", ce);
                service.Abort();
            }
            catch (TimeoutException te)
            {
                Console.WriteLine("TimeoutException: {0}", te);
                service.Abort();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                service.Abort();
            }

            return ServiceResult<TOut>.CreateFailure();
        }

        public static ServiceResult<TOut> Invoke<TIn0, TIn1, TIn2, TOut>(this ICommunicationObject service,
            Func<TIn0, TIn1, TIn2, TOut> func, TIn0 arg0, TIn1 arg1, TIn2 arg2)
        {
            try
            {
                TOut result = func(arg0, arg1, arg2);
                service.Close();

                return ServiceResult<TOut>.CreateSuccess(result);
            }
            catch (FaultException fe)
            {
                Console.WriteLine("Fault Exception: {0}", fe);
                service.Abort();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("Communication Exception: {0}", ce);
                service.Abort();
            }
            catch (TimeoutException te)
            {
                Console.WriteLine("Timeout Exception: {0}", te);
                service.Abort();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                service.Abort();
            }

            return ServiceResult<TOut>.CreateFailure();
        }

        #region Nested type: ServiceResult

        public class ServiceResult<T>
        {
            private readonly T _output;
            private readonly bool _success;

            private ServiceResult()
            {
                _success = false;
                _output = default(T);
            }

            private ServiceResult(T output, bool success)
            {
                _success = success;
                _output = output;
            }

            public bool Success
            {
                get { return _success; }
            }

            public T Output
            {
                get { return _output; }
            }

            public static ServiceResult<T> CreateSuccess(T output)
            {
                return new ServiceResult<T>(output, true);
            }

            public static ServiceResult<T> CreateFailure()
            {
                return new ServiceResult<T>();
            }

            public override string ToString()
            {
                return _output.GetType().IsValueType 
                    ? _output.ToString() 
// ReSharper disable CompareNonConstrainedGenericWithNull
                    : _output != null 
// ReSharper restore CompareNonConstrainedGenericWithNull
                        ? _output.ToString() 
                        : "(null)";
            }
        }

        #endregion
    }
}
