using XCad.Examples.IssuesManager.Models;
using XCad.Examples.IssuesManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Xarial.XCad.Documents.Services;
using Xarial.XCad;
using Xarial.XCad.Documents;
using Xarial.XCad.Data.Enums;
using Xarial.XCad.Documents.Enums;

namespace XCad.Examples.IssuesManager
{
    public class IssuesDocument : IDocumentHandler
    {
        private const string STORAGE_NAME = "_XCadIssuesStore_";
        private const string ISSUES_SUB_STORAGE_NAME = "IssuesStore";
        private const string ISSUES_SUMMARIES_STREAM_NAME = "Summaries";

        public event Action<IssuesDocument> Destroyed;
        public event Action<IssuesVM> ShowIssues;

        private IXApplication m_App;
        private IXDocument m_Model;
            
        private IssuesVM m_IssuesVm;

        public void Init(IXApplication app, IXDocument model)
        {
            m_App = app;
            m_Model = model;

            m_App.Documents.DocumentActivated += OnDocumentActivated;
            m_Model.StorageReadAvailable += OnStorageReadAvailable;
            m_Model.StorageWriteAvailable += OnStorageWriteAvailable;
        }

        private void OnDocumentActivated(IXDocument doc)
        {
            if (doc == m_Model) 
            {
                ShowIssues?.Invoke(m_IssuesVm);
            }
        }

        private void OnStorageReadAvailable(IXDocument doc)
        {
            LoadIssuesFromStorageStore();
        }

        private void OnStorageWriteAvailable(IXDocument doc)
        {
            SaveIssuesToStorageStore();
        }

        public void CreateNewIssue()
        {
            m_IssuesVm.CreateNewIssue();
        }

        public void RemoveActiveIssue()
        {
            m_IssuesVm.RemoveActiveIssue();
        }

        private void LoadIssuesFromStorageStore()
        {
            IEnumerable<int> issuesIds = null;
            IssueInfo[] issueInfos = null;

            using (var storage = m_Model.TryOpenStorage(STORAGE_NAME, AccessType_e.Read))
            {
                if (storage != null) 
                {
                    using (var issuesStore = storage.TryOpenStorage(ISSUES_SUB_STORAGE_NAME, false))
                    {
                        if (issuesStore != null)
                        {
                            issuesIds = issuesStore.GetSubStreamNames().Select(n => int.Parse(n));
                        }
                    }

                    using (var stream = storage.TryOpenStream(ISSUES_SUMMARIES_STREAM_NAME, false))
                    {
                        if (stream != null)
                        {
                            var ser = new DataContractSerializer(typeof(IssueInfo[]));
                            issueInfos = ser.ReadObject(stream) as IssueInfo[];
                        }
                    }
                }
            }

            if (issuesIds == null)
            {
                issuesIds = Enumerable.Empty<int>();
            }

            if (issueInfos == null)
            {
                issueInfos = new IssueInfo[0];
            }

            if (!issueInfos.Select(i => i.Id).OrderBy(i => i)
                .SequenceEqual(issuesIds.OrderBy(i => i)))
            {
                throw new InvalidOperationException("Issues mismatch");
            }

            m_IssuesVm = new IssuesVM(issueInfos);
            m_IssuesVm.Modified += OnIssuesModified;
            m_IssuesVm.LoadIssue += OnLoadIssue;

            if (!m_Model.State.HasFlag(DocumentState_e.Hidden))
            {
                ShowIssues?.Invoke(m_IssuesVm);
            }
        }

        private void OnIssuesModified()
        {
            m_Model.IsDirty = true;
        }

        private Issue OnLoadIssue(int issueId)
        {
            using (var storage = m_Model.OpenStorage(STORAGE_NAME, AccessType_e.Read))
            {
                using (var issueStorage = storage.TryOpenStorage(ISSUES_SUB_STORAGE_NAME, false))
                {
                    using (var stream = issueStorage.TryOpenStream(issueId.ToString(), false))
                    {
                        var ser = new DataContractSerializer(typeof(Issue));
                        return ser.ReadObject(stream) as Issue;
                    }
                }
            }
        }

        private void SaveIssuesToStorageStore()
        {
            var loadedIssues = m_IssuesVm.Issues.Where(i => i.IsLoaded);

            if (loadedIssues.Any(i => i.IsDeleted || i.IsDirty))
            {
                using (var storage = m_Model.OpenStorage(STORAGE_NAME, AccessType_e.Write))
                {
                    using (var stream = storage.TryOpenStream(ISSUES_SUMMARIES_STREAM_NAME, true))
                    {
                        var ser = new DataContractSerializer(typeof(IssueInfo[]));
                        ser.WriteObject(stream, m_IssuesVm.Issues
                            .Select(i => i.Issue.GetInfo()).ToArray());
                    }

                    using (var issuesStore = storage.TryOpenStorage(ISSUES_SUB_STORAGE_NAME, true))
                    {
                        foreach (var removedIssue in loadedIssues.Where(i => i.IsDeleted))
                        {
                            issuesStore.RemoveSubElement(removedIssue.Id.ToString());
                        }

                        foreach (var modifiedIssue in loadedIssues.Where(i => !i.IsDeleted && i.IsDirty))
                        {
                            using (var stream = issuesStore.TryOpenStream(modifiedIssue.Id.ToString(), true))
                            {
                                var ser = new DataContractSerializer(typeof(Issue));
                                ser.WriteObject(stream, modifiedIssue.Issue);
                            }
                        }
                    }
                }
            }

            m_IssuesVm.FlushChanges();
        }

        public void Dispose()
        {
            m_App.Documents.DocumentActivated -= OnDocumentActivated;
            m_Model.StorageReadAvailable -= OnStorageReadAvailable;
            m_Model.StorageWriteAvailable -= OnStorageWriteAvailable;

            Destroyed?.Invoke(this);
        }
    }
}
