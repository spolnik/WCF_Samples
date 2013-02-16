using System.Runtime.Serialization;

namespace WcfSample.Hosting.CalculatorService.Common
{
    [DataContract(Namespace = CalculatorServiceHelper.Namespace)]
    public enum Calculation
    {
        [EnumMember] Add,
        [EnumMember] Subtract,
        [EnumMember] Multiply,
        [EnumMember] Divide,
        [EnumMember] Square,
        [EnumMember] Sqrt
    }
}