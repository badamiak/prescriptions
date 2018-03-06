using System;
using System.Xml.Serialization;

namespace Prescriptions.API.Model.Drugs
{
    [Serializable]
    public enum RefundLevel
    {
        [XmlEnum("bezpłatny")]
        Bezpłatny,
        [XmlEnum("bezpłatny do limitu")]
        Limited,
        [XmlEnum("ryczałt")]
        LumpSum,
        [XmlEnum("50%")]
        FiftyPercent,
        [XmlEnum("30%")]
        ThirtyPercent
    }
}
