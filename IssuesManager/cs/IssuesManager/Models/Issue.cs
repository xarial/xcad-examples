using System;
using System.Runtime.Serialization;

namespace XCad.Examples.IssuesManager.Models
{
    [DataContract]
    public class Issue : IssueInfo
    {
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Author { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        public Issue(int id)
        {
            Id = id;
        }

        public Issue(IssueInfo info) : this(info.Id)
        {
            Summary = info.Summary;
            Severity = info.Severity;
            Status = info.Status;
        }

        public IssueInfo GetInfo()
        {
            return new IssueInfo()
            {
                Id = Id,
                Summary = Summary,
                Severity = Severity,
                Status = Status
            };
        }
    }
}
