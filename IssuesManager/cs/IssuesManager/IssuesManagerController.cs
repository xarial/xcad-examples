using XCad.Examples.IssuesManager.Properties;
using XCad.Examples.IssuesManager.ViewModels;
using XCad.Examples.IssuesManager.Views;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.UI.TaskPane.Attributes;
using Xarial.XCad.UI.TaskPane.Enums;
using Xarial.XCad.Extensions;
using Xarial.XCad.Base.Enums;
using Xarial.XCad.Documents;
using System;
using Xarial.XCad.UI;
using System.Linq;

namespace XCad.Examples.IssuesManager
{
    public class IssuesManagerController : IDisposable
    {
        [Icon(typeof(Resources), nameof(Resources.issues_icon))]
        [Title("Issues Manager")]
        private enum IssuesMgrCommands_e
        {
            [Icon(typeof(Resources), nameof(Resources.new_issue_icon))]
            [Title("Create New Issue")]
            CreateNewIssue,

            [TaskPaneStandardIcon(TaskPaneStandardIcons_e.Close)]
            [Title("Remove Selected Issue")]
            RemoveIssue
        }

        private IssuesControl m_IssuesControl;

        private readonly IXExtension m_Ext;
        private readonly IXEnumTaskPane<IssuesControl, IssuesMgrCommands_e> m_TaskPane;

        public IssuesManagerController(IXExtension ext) 
        {   
            m_Ext = ext;

            m_Ext.Application.Documents.RegisterHandler<IssuesDocument>();
            m_Ext.Application.Documents.DocumentLoaded += OnDocumentLoaded;

            m_TaskPane = m_Ext.CreateTaskPane<IssuesControl, IssuesMgrCommands_e>();
            m_IssuesControl = m_TaskPane.Control;
            m_TaskPane.ButtonClick += OnTaskPaneButtonClick;
        }

        private void OnDocumentLoaded(IXDocument doc)
        {
            var issuesDoc = m_Ext.Application.Documents.GetHandler<IssuesDocument>(doc);
            issuesDoc.Destroyed += OnIssuesDocDestroyed;
            issuesDoc.ShowIssues += OnShowIssues;
        }

        private void OnTaskPaneButtonClick(IssuesMgrCommands_e spec)
        {
            var activeDoc = m_Ext.Application.Documents.Active;

            if (activeDoc != null)
            {
                var issuesDoc = m_Ext.Application.Documents.GetHandler<IssuesDocument>(activeDoc);

                switch (spec)
                {
                    case IssuesMgrCommands_e.CreateNewIssue:
                        issuesDoc.CreateNewIssue();
                        break;

                    case IssuesMgrCommands_e.RemoveIssue:
                        issuesDoc.RemoveActiveIssue();
                        break;
                }
            }
            else
            {
                m_Ext.Application.ShowMessageBox("Open the model", 
                    MessageBoxIcon_e.Error, 
                    MessageBoxButtons_e.Ok);
            }
        }
                       
        private void OnIssuesDocDestroyed(IssuesDocument docHandler)
        {
            //destroying last document
            if (!m_Ext.Application.Documents.Any())
            {
                m_IssuesControl.DataContext = null;
            }
        }

        private void OnShowIssues(IssuesVM issues)
        {
            m_IssuesControl.DataContext = issues;
        }

        public void Dispose()
        {
            m_Ext.Application.Documents.DocumentLoaded -= OnDocumentLoaded;
            m_TaskPane.ButtonClick -= OnTaskPaneButtonClick;
        }
    }
}
