using System.Runtime.Serialization;

namespace XCad.Examples.IssuesManager.Models
{
    [DataContract]
    public enum Severity_e
    {
        [EnumMember]
        Low,

        [EnumMember]
        Medium,

        [EnumMember]
        High
    }
}
