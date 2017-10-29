using System;
using System.Xml.Serialization;

namespace Prescriptions.Model.Drugs
{
    [Serializable]
    public enum RefundLevel
    {
        [XmlEnum("bezpłatny")]
        Full,
        [XmlEnum("ryczałt")]
        LumpSum,
        [XmlEnum("50%")]
        FiftyPercent,
        [XmlEnum("30%")]
        ThirtyPercent
    }
}
