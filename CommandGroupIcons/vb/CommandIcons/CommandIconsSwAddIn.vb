Imports CommandIcons.My.Resources
Imports Xarial.XCad.Base.Attributes
Imports Xarial.XCad.SolidWorks
Imports Xarial.XCad.UI.Commands

<Runtime.InteropServices.ComVisible(True)>
Public Class CommandIconsSwAddIn
    Inherits SwAddInEx

    <Title("Power Tools")>
    Public Enum Commands_e
        <Icon(GetType(Resources), NameOf(Resources.electric_drill))>
        Drill

        <Icon(GetType(Resources), NameOf(Resources.electric_screwdriver))>
        Screwdriver

        <Icon(GetType(Resources), NameOf(Resources.power_saw))>
        Saw
    End Enum

    Public Overrides Sub OnConnect()
        AddHandler CommandManager.AddCommandGroup(Of Commands_e).CommandClick, AddressOf OnButtonClick
    End Sub

    Private Sub OnButtonClick(cmd As Commands_e)
        Select Case cmd
            Case Commands_e.Drill
                Application.ShowMessageBox("Drill buttons is clicked")
            Case Commands_e.Screwdriver
                Application.ShowMessageBox("Screwdriver buttons is clicked")
            Case Commands_e.Saw
                Application.ShowMessageBox("Saw buttons is clicked")
        End Select
    End Sub

End Class
