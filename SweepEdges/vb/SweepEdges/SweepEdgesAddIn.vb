Imports System.Runtime.InteropServices
Imports Xarial.XCad.Base.Attributes
Imports Xarial.XCad.SolidWorks
Imports Xarial.XCad.UI.Commands
Imports Xarial.XCad.UI.Commands.Attributes
Imports Xarial.XCad.UI.Commands.Enums

<Title("Sweep Edges")>
Public Enum Commands_e
    <Title("Insert Sweep Edges")>
    <CommandItemInfo(WorkspaceTypes_e.Part)>
    InsertSweepEdges
End Enum

<ComVisible(True)>
Public Class SweepEdgesAddIn
    Inherits SwAddInEx

    Public Overrides Sub OnConnect()
        AddHandler Me.CommandManager.AddCommandGroup(Of Commands_e)().CommandClick, AddressOf OnButtonClick
    End Sub

    Private Sub OnButtonClick(ByVal spec As Commands_e)
        Select Case spec
            Case Commands_e.InsertSweepEdges
                Application.Documents.Active.Features.CreateCustomFeature(Of SweepEdgesMacroFeatureEditor, SweepEdgesData, SweepEdgesData)()
        End Select
    End Sub

End Class
