using System.Runtime.Serialization;

namespace XCad.Examples.IssuesManager.Models
{
    [DataContract]
    public enum Status_e
    {
        [EnumMember]
        Open,

        [EnumMember]
        Closed
    }
}
