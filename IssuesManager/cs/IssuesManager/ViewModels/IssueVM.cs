using XCad.Examples.IssuesManager.Models;
using System;
using System.ComponentModel;
using Xarial.XToolkit.Wpf.Extensions;

namespace XCad.Examples.IssuesManager.ViewModels
{
    public class IssueVM : INotifyPropertyChanged
    {
        public event Action<IssueVM> Modified;

        public event PropertyChangedEventHandler PropertyChanged;

        private bool m_IsDirty;
        private bool m_IsDeleted;
        
        public IssueVM(Issue issue)
        {
            Issue = issue;
        }

        public string Summary
        {
            get
            {
                return Issue.Summary;
            }
            set
            {
                if (Issue.Summary != value)
                {
                    Issue.Summary = value;
                    this.NotifyChanged();
                    IsDirty = true;
                }
            }
        }

        public string Description
        {
            get
            {
                return Issue.Description;
            }
            set
            {
                if (Issue.Description != value)
                {
                    Issue.Description = value;
                    this.NotifyChanged();
                    IsDirty = true;
                }
            }
        }

        public string Author
        {
            get
            {
                return Issue.Author;
            }
        }

        public DateTime DateCreated
        {
            get
            {
                return Issue.DateCreated;
            }
        }

        public Severity_e Severity
        {
            get
            {
                return Issue.Severity;
            }
            set
            {
                if (Issue.Severity != value)
                {
                    Issue.Severity = value;
                    this.NotifyChanged();
                    IsDirty = true;
                }
            }
        }

        public Status_e Status
        {
            get
            {
                return Issue.Status;
            }
            set
            {
                if (Issue.Status != value)
                {
                    Issue.Status = value;
                    this.NotifyChanged();
                    IsDirty = true;
                }
            }
        }

        public bool IsDirty
        {
            get
            {
                return m_IsDirty;
            }
            set
            {
                m_IsDirty = value;
                this.NotifyChanged();
                if (value)
                {
                    Modified?.Invoke(this);
                }
            }
        }
        
        public bool IsDeleted
        {
            get
            {
                return m_IsDeleted;
            }
            set
            {
                m_IsDeleted = value;
                this.NotifyChanged();
            }
        }
        
        public bool IsLoaded { get; set; }

        internal Issue Issue { get; set; }

        public int Id
        {
            get
            {
                return Issue.Id;
            }
        }
    }
}
