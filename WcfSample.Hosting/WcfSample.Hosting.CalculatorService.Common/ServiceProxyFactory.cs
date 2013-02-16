using System;

namespace WcfSample.Hosting.CalculatorService.Common
{
    public class ServiceProxyFactory<TService> where TService : class
    {
        private readonly string _endpointName;

        private ServiceProxyFactory(string endpointName)
        {
            _endpointName = endpointName;
        }

        public static ServiceProxyFactory<TService> Create(string endpointName = null)
        {
            return new ServiceProxyFactory<TService>(endpointName);
        }

        public void Invoke(Action<TService> operation)
        {
            using (ServiceProxy<TService> proxy = GetServiceProxy())
            {
                operation(proxy.GetChannel());
            }
        }

        private ServiceProxy<TService> GetServiceProxy()
        {
            return _endpointName == null ? new ServiceProxy<TService>() : new ServiceProxy<TService>(_endpointName);
        }

        public TResult Invoke<TResult>(Func<TService, TResult> operation)
        {
            TResult result = default(TResult);

            Invoke(x => { result = operation(x); });

            return result;
        }
    }
}
