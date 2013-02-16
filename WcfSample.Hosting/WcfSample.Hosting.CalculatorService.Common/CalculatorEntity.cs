using System.Runtime.Serialization;

namespace WcfSample.Hosting.CalculatorService.Common
{
    [DataContract(Namespace = CalculatorServiceHelper.Namespace)]
    public class CalculatorEntity
    {
        [DataMember]
        public double FirstValue { get; set; }

        [DataMember]
        public double SecondValue { get; set; }

        [DataMember]
        public Calculation Calculation { get; set; }
    }
}
