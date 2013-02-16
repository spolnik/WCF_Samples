using System.ServiceModel;
using System.ServiceModel.Channels;
using WcfSample.Hosting.CalculatorService.Common;

namespace WcfSample.Mex.Client
{
    public class CalculatorServiceClient : ClientBase<ICalculatorService>, ICalculatorService
    {
        public CalculatorServiceClient()
        {
        }

        public CalculatorServiceClient(string endpointConfigurationName)
            : base(endpointConfigurationName)
        {
        }

        public CalculatorServiceClient(Binding binding, EndpointAddress remoteAddress) 
            : base(binding, remoteAddress)
        {
        }

        #region Implementation of ICalculatorService

        public double Eval(CalculatorEntity entity)
        {
            return Channel.Eval(entity);
        }

        #endregion
    }
}