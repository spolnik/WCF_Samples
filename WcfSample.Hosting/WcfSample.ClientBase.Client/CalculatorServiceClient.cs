using System;
using System.ServiceModel;
using WcfSample.Hosting.CalculatorService.Common;

namespace WcfSample.ClientBase.Client
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

        #region Implementation of ICalculatorService

        public double Eval(CalculatorEntity entity)
        {
            return Channel.Eval(entity);
        }

        #endregion
    }
}