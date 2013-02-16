using System;
using System.ServiceModel;
using WcfSample.Hosting.CalculatorService.Common;

namespace WcfSample.Hosting.CalculatorService
{
    [ServiceBehavior(Namespace = CalculatorServiceHelper.Namespace, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CalculatorService : ICalculatorService
    {
        #region Implementation of ICalculatorService

        public double Eval(CalculatorEntity entity)
        {
            switch (entity.Calculation)
            {
                case Calculation.Add:
                    return entity.FirstValue + entity.SecondValue;
                case Calculation.Subtract:
                    return entity.FirstValue - entity.SecondValue;
                case Calculation.Multiply:
                    return entity.FirstValue * entity.SecondValue;
                case Calculation.Divide:
                    if (entity.SecondValue == 0)
                        throw new FaultException("You cannot divide by 0.");

                    return entity.FirstValue / entity.SecondValue;
                case Calculation.Square:
                    return entity.FirstValue * entity.FirstValue;
                case Calculation.Sqrt:
                    return Math.Sqrt(entity.FirstValue);
            }

            return 0.0;
        }

        #endregion
    }
}
