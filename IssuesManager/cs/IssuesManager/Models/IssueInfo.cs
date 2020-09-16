using System.Runtime.Serialization;

namespace XCad.Examples.IssuesManager.Models
{
    [DataContract]
    public class IssueInfo
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Severity_e Severity { get; set; }

        [DataMember]
        public string Summary { get; set; }

        [DataMember]
        public Status_e Status { get; set; }
    }
}
