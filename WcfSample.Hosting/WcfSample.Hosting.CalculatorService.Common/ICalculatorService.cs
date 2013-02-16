using System.ServiceModel;

namespace WcfSample.Hosting.CalculatorService.Common
{
    [ServiceContract(Name = "CalculatorService", Namespace = CalculatorServiceHelper.Namespace)]
    public interface ICalculatorService
    {
        /// <summary>
        /// Eval calculation: add/subtract/multiply/divide for both values, square/sqrt for first value.
        /// </summary>
        /// <param name="entity">Entity contains a kind of calculation and values to compute result.</param>
        /// <returns>Computation result.</returns>
        [OperationContract()]
        double Eval(CalculatorEntity entity);
    }
}
