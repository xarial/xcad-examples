Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports Ttl.My.Resources
Imports Xarial.XCad.Base.Attributes
Imports Xarial.XCad.SolidWorks
Imports Xarial.XCad.SolidWorks.Documents
Imports Xarial.XCad.UI.Commands
Imports Xarial.XCad.UI.Commands.Attributes
Imports Xarial.XCad.UI.Commands.Enums
Imports Xarial.XCad.Base

Namespace ttl

    <Title("Top 10 List 2020")>
    Public Enum Commands_e

        <Icon(GetType(Resources), NameOf(Resources.zoom_to_geometry))>
        <Title("Zoom Geometry To Fit")>
        <Description("Zooms geometry to fit excluding sketches and reference geometry")>
        <CommandItemInfo(WorkspaceTypes_e.Assembly Or WorkspaceTypes_e.Part)>
        ZoomGeometryToFit

        <Icon(GetType(Resources), NameOf(Resources.shaft_chamfer))>
        <Title("Insert Shaft Chamfer")>
        <Description("Creates a chamfer driven by base diameter and angle")>
        <CommandItemInfo(WorkspaceTypes_e.Part)>
        InsertShaftChamfer

        <Icon(GetType(Resources), NameOf(Resources.hide_show_bodies))>
        <Title("Hide/Show Bodies")>
        <Description("Displays Hide/Show Bodies Property Page")>
        <CommandItemInfo(WorkspaceTypes_e.Part)>
        HideShowBodies

    End Enum

    <ComVisible(True)>
    Public Class MainAddIn
        Inherits SwAddInEx

        Public Overrides Sub OnConnect()
            AddHandler CommandManager.AddCommandGroup(Of Commands_e)().CommandClick, AddressOf OnButtonClick
        End Sub

        Private Sub OnButtonClick(ByVal cmd As Commands_e)
            Select Case cmd
                Case Commands_e.ZoomGeometryToFit
                    ZoomGeometryToFit()
                Case Commands_e.InsertShaftChamfer
                    InsertShaftChamfer()
                Case Commands_e.HideShowBodies
                    HideShowBodies()
            End Select

        End Sub

        Private Sub ZoomGeometryToFit()
            Dim model = TryCast(Application.Documents.Active, ISwDocument3D)
            Dim bbox = model.PreCreateBoundingBox()
            bbox.Precise = True
            bbox.Commit()
            model.ModelViews.Active.ZoomToBox(bbox.Box)
        End Sub

        Private Sub HideShowBodies()
            Const swCommands_View_Hideshow As Integer = 1390
            Application.Sw.RunCommand(swCommands_View_Hideshow, "")
        End Sub

        Private Sub InsertShaftChamfer()
            Application.Documents.Active.Features.CreateCustomFeature(Of ShaftChamferMacroFeatureDefinition, ShaftChamferData, ShaftChamferData)()
        End Sub
    End Class
End Namespace
