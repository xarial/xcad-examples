
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports Xarial.XCad.Base.Attributes
Imports Xarial.XCad.Features.CustomFeature
Imports Xarial.XCad.Features.CustomFeature.Delegates
Imports Xarial.XCad.Geometry
Imports Xarial.XCad.Geometry.Structures
Imports Xarial.XCad.SolidWorks
Imports Xarial.XCad.SolidWorks.Documents
Imports Xarial.XCad.SolidWorks.Features.CustomFeature
Imports Xarial.XCad.SolidWorks.Geometry

<ComVisible(True)>
<Title("Swept Edges")>
Public Class SweepSketchMacroFeatureEditor
    Inherits SwMacroFeatureDefinition(Of SweepSketchData, SweepSketchData)

    Public Overrides Function CreateGeometry(ByVal app As ISwApplication, ByVal model As ISwDocument, ByVal data As SweepSketchData, ByVal isPreview As Boolean, <Out> ByRef alignDim As AlignDimensionDelegate(Of SweepSketchData)) As ISwBody()

        Dim result = New List(Of ISwBody)()
        Dim firstCenterPt As Structures.Point = Nothing
        Dim firstDir As Vector = Nothing

        'TODO: implement merge

        For Each edge In data.Edges

            Dim path = edge.Definition
            Dim startPt = path.StartCoordinate
            Dim endPt = path.EndCoordinate
            Dim dir = startPt - endPt

            If firstCenterPt Is Nothing Then
                firstCenterPt = startPt
            End If

            If firstDir Is Nothing Then
                firstDir = dir
            End If

            Dim profile = app.MemoryGeometryBuilder.CreateCircle(startPt, dir, data.Radius * 2)
            Dim profileRegion = app.MemoryGeometryBuilder.CreateRegionFromSegments(profile)
            Dim region = app.MemoryGeometryBuilder.CreatePlanarSheet(profileRegion).Bodies.First()
            Dim sweep = app.MemoryGeometryBuilder.CreateSolidExtrusion(path.Length, dir, region)
            Dim body = sweep.Bodies.OfType(Of ISwBody)().First()

            result.Add(body)
        Next

        alignDim = Sub(name, [dim])
                       Select Case name
                           Case NameOf(SweepSketchData.Radius)
                               Me.AlignRadialDimension([dim], firstCenterPt, firstDir)
                       End Select
                   End Sub

        Return result.ToArray()
    End Function

    Public Overrides Function OnShouldHidePreviewEditBody(body As IXBody, data As SweepSketchData, page As SweepSketchData) As Boolean
        Return Not data.Merge
    End Function

    Public Overrides Sub AssignPreviewBodyColor(body As IXBody, ByRef color As Color)
        color = Color.FromArgb(100, Color.Blue)
    End Sub

End Class
