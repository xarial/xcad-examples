Imports System.Runtime.InteropServices
Imports Xarial.XCad.SolidWorks
Imports Xarial.XCad.SolidWorks.UI.PropertyPage
Imports Xarial.XCad.UI.Commands
Imports Xarial.XCad.UI.Commands.Attributes
Imports Xarial.XCad.UI.Commands.Enums
Imports Xarial.XCad.UI.PropertyPage.Attributes

<ComVisible(True)>
<Guid("CF020044-21D1-4D83-9441-67F4B2985356")>
Public Class ControlsAddIn
    Inherits SwAddInEx

    Enum ControlCommands_e
        <CommandItemInfo(WorkspaceTypes_e.Part Or WorkspaceTypes_e.Assembly)>
        CreateWinFormModelViewTab
        <CommandItemInfo(WorkspaceTypes_e.Part Or WorkspaceTypes_e.Assembly)>
        CreateWpfModelViewTab
        <CommandItemInfo(WorkspaceTypes_e.AllDocuments)>
        CreateWinFormFeatMgrTab
        <CommandItemInfo(WorkspaceTypes_e.AllDocuments)>
        CreateWpfFeatMgrTab
        CreateWinFormTaskPane
        CreateWpfTaskPane
        <CommandItemInfo(WorkspaceTypes_e.AllDocuments)>
        CreateWinFormPmPage
        <CommandItemInfo(WorkspaceTypes_e.AllDocuments)>
        CreateWpfPmPage
    End Enum

    <ComVisible(True)>
    Public Class WinFormsPMPage
        Inherits SwPropertyManagerPageHandler

        <CustomControl(GetType(WinFormsUserControl))>
        Public Property WinFormCtrl As Object

    End Class

    <ComVisible(True)>
    Public Class WpfPMPage
        Inherits SwPropertyManagerPageHandler

        <CustomControl(GetType(WpfUserControl))>
        Public Property WpfCtrl As Object

    End Class

    Dim m_WinFormsPMPage As ISwPropertyManagerPage(Of WinFormsPMPage)
    Dim m_WpfPMPage As ISwPropertyManagerPage(Of WpfPMPage)

    Public Overrides Sub OnConnect()
        AddHandler CommandManager.AddCommandGroup(Of ControlCommands_e).CommandClick, AddressOf OnButtonClick

        m_WinFormsPMPage = CreatePage(Of WinFormsPMPage)()
        m_WpfPMPage = CreatePage(Of WpfPMPage)()

    End Sub

    Sub OnButtonClick(cmd As ControlCommands_e)

        Dim activeDoc = Application.Documents.Active

        Select Case cmd
            Case ControlCommands_e.CreateWinFormModelViewTab
                CreateDocumentTabWinForm(Of WinFormsUserControl)(activeDoc)
            Case ControlCommands_e.CreateWpfModelViewTab
                CreateDocumentTabWpf(Of WpfUserControl)(activeDoc)
            Case ControlCommands_e.CreateWinFormFeatMgrTab
                CreateFeatureManagerTabWinForm(Of WinFormsUserControl)(activeDoc)
            Case ControlCommands_e.CreateWpfFeatMgrTab
                CreateFeatureManagerTabWpf(Of WpfUserControl)(activeDoc)
            Case ControlCommands_e.CreateWinFormTaskPane
                CreateTaskPaneWinForm(Of WinFormsUserControl)()
            Case ControlCommands_e.CreateWpfTaskPane
                CreateTaskPaneWpf(Of WpfUserControl)()
            Case ControlCommands_e.CreateWinFormPmPage
                m_WinFormsPMPage.Show(New WinFormsPMPage())
            Case ControlCommands_e.CreateWpfPmPage
                m_WpfPMPage.Show(New WpfPMPage())
        End Select
    End Sub

End Class