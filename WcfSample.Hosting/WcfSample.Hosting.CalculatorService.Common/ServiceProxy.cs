using System;
using System.ServiceModel;

namespace WcfSample.Hosting.CalculatorService.Common
{
    public class ServiceProxy<T> : ClientBase<T>, IDisposable where T : class
    {
        // empty ctor for default config if only one available
        public ServiceProxy()
        {
        }

        public ServiceProxy(string endpointConfigurationName) 
            : base(endpointConfigurationName)
        {
        }

        public T GetChannel()
        {
            return Channel;
        }

        public void Dispose()
        {
            try
            {
                if (Channel != null)
                {
                    if (State != CommunicationState.Faulted)
                        Close();
                    else
                        Abort();
                }
            }
            catch (CommunicationException)
            {
                Abort();
            }
            catch (TimeoutException)
            {
                Abort();
            }
            catch (Exception)
            {
                Abort();
                throw;
            }
        }
    }
}
