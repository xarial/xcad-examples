
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports Xarial.XCad
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
Public Class SweepEdgesMacroFeatureEditor
    Inherits SwMacroFeatureDefinition(Of SweepEdgesData, SweepEdgesData)

    Private Class SweepEdgesBodyEqualityComparer
        Implements IEqualityComparer(Of IXBody)

        Dim m_GroupByBodies As Boolean

        Public Sub New(groupByBodies As Boolean)
            m_GroupByBodies = groupByBodies
        End Sub

        Public Function Equals(x As IXBody, y As IXBody) As Boolean Implements IEqualityComparer(Of IXBody).Equals
            If m_GroupByBodies Then
                Return x.Equals(y)
            Else
                Return False
            End If
        End Function

        Public Function GetHashCode(obj As IXBody) As Integer Implements IEqualityComparer(Of IXBody).GetHashCode
            Return 0
        End Function

    End Class

    Public Overrides Function CreateGeometry(ByVal app As ISwApplication, ByVal model As ISwDocument, ByVal data As SweepEdgesData, ByVal isPreview As Boolean, <Out> ByRef alignDim As AlignDimensionDelegate(Of SweepEdgesData)) As ISwBody()

        Dim result = New List(Of ISwBody)()
        Dim firstCenterPt As Structures.Point = Nothing
        Dim firstDir As Vector = Nothing

        For Each edgeGroup In data.Edges.GroupBy(Function(e) e.Body, New SweepEdgesBodyEqualityComparer(data.Merge))

            Dim mergedBody As ISwBody

            mergedBody = edgeGroup.Key

            If isPreview Then
                mergedBody = mergedBody.Copy()
            End If

            For Each edge In edgeGroup
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

                If data.Merge Then
                    mergedBody = mergedBody.Add(body)
                Else
                    result.Add(body)
                End If
            Next

            If data.Merge Then
                result.Add(mergedBody)
            End If
        Next

            alignDim = Sub(name, [dim])
                       Select Case name
                           Case NameOf(SweepEdgesData.Radius)
                               Me.AlignRadialDimension([dim], firstCenterPt, firstDir)
                       End Select
                   End Sub

        Return result.ToArray()
    End Function

    Public Overrides Function ShouldHidePreviewEditBody(body As IXBody, data As SweepEdgesData, page As SweepEdgesData) As Boolean
        Return Not data.Merge
    End Function

    Public Overrides Sub AssignPreviewBodyColor(body As IXBody, ByRef color As Color)
        color = Color.FromArgb(100, Color.Blue)
    End Sub

End Class
