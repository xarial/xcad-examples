using System;
using System.Runtime.InteropServices;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.UI.PropertyPage;
using Xarial.XCad.UI.Commands;
using Xarial.XCad.UI.Commands.Attributes;
using Xarial.XCad.UI.Commands.Enums;
using Xarial.XCad.UI.PropertyPage.Attributes;
using FormsAndWpfControls;

[ComVisible(true)]
[Guid("13FBA728-F4A9-4CDA-A628-9CD7ED8719E3")]
public class ControlsCsAddIn : SwAddInEx
{
    enum ControlCommands_e
    {
        [CommandItemInfo(WorkspaceTypes_e.Part | WorkspaceTypes_e.Assembly)]
        CreateWinFormModelViewTab,
        [CommandItemInfo(WorkspaceTypes_e.Part | WorkspaceTypes_e.Assembly)]
        CreateWpfModelViewTab,
        [CommandItemInfo(WorkspaceTypes_e.AllDocuments)]
        CreateWinFormFeatMgrTab,
        [CommandItemInfo(WorkspaceTypes_e.AllDocuments)]
        CreateWpfFeatMgrTab,
        CreateWinFormTaskPane,
        CreateWpfTaskPane,
        [CommandItemInfo(WorkspaceTypes_e.AllDocuments)]
        CreateWinFormPmPage,
        [CommandItemInfo(WorkspaceTypes_e.AllDocuments)]
        CreateWpfPmPage
    }

    [ComVisible(true)]
    public class WinFormsPMPage : SwPropertyManagerPageHandler
    {
        [CustomControl(typeof(WinFormsUserControl))]
        public object WinFormCtrl { get; set; }
    }

    [ComVisible(true)]
    public class WpfPMPage : SwPropertyManagerPageHandler
    {
        [CustomControl(typeof(WpfUserControl))]
        public object WpfCtrl { get; set; }
    }

    private ISwPropertyManagerPage<WinFormsPMPage> m_WinFormsPMPage;
    private ISwPropertyManagerPage<WpfPMPage> m_WpfPMPage;

    public override void OnConnect()
    {
        CommandManager.AddCommandGroup<ControlCommands_e>().CommandClick += OnButtonClick;

        m_WinFormsPMPage = CreatePage<WinFormsPMPage>();
        m_WpfPMPage = CreatePage<WpfPMPage>();
    }

    private void OnButtonClick(ControlCommands_e cmd)
    {
        var activeDoc = Application.Documents.Active;

        switch (cmd)
        {
            case ControlCommands_e.CreateWinFormModelViewTab:
                {
                    this.CreateDocumentTabWinForm<WinFormsUserControl>(activeDoc);
                    break;
                }

            case ControlCommands_e.CreateWpfModelViewTab:
                {
                    this.CreateDocumentTabWpf<WpfUserControl>(activeDoc);
                    break;
                }

            case ControlCommands_e.CreateWinFormFeatMgrTab:
                {
                    this.CreateFeatureManagerTabWinForm<WinFormsUserControl>(activeDoc);
                    break;
                }

            case ControlCommands_e.CreateWpfFeatMgrTab:
                {
                    this.CreateFeatureManagerTabWpf<WpfUserControl>(activeDoc);
                    break;
                }

            case ControlCommands_e.CreateWinFormTaskPane:
                {
                    this.CreateTaskPaneWinForm<WinFormsUserControl>();
                    break;
                }

            case ControlCommands_e.CreateWpfTaskPane:
                {
                    this.CreateTaskPaneWpf<WpfUserControl>();
                    break;
                }

            case ControlCommands_e.CreateWinFormPmPage:
                {
                    m_WinFormsPMPage.Show(new WinFormsPMPage());
                    break;
                }

            case ControlCommands_e.CreateWpfPmPage:
                {
                    m_WpfPMPage.Show(new WpfPMPage());
                    break;
                }
        }
    }
}
