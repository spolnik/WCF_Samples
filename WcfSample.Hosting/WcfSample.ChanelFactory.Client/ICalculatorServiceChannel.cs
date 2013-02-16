using System.ServiceModel;
using WcfSample.Hosting.CalculatorService.Common;

namespace WcfSample.ChannelFactory.Client
{
    public interface ICalculatorServiceChannel : ICalculatorService, IClientChannel
    {
        //empty
    }
}