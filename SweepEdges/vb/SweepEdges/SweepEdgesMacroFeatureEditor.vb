
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Controls
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel
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
Imports Xarial.XCad.SolidWorks.Geometry.Curves

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

    Public Overrides Function CreateGeometry(app As ISwApplication, doc As ISwDocument, feat As ISwMacroFeature(Of SweepEdgesData), ByRef alignDim As AlignDimensionDelegate(Of SweepEdgesData)) As ISwBody()

        Dim firstCenterPt As Structures.Point = Nothing
        Dim firstDir As Vector = Nothing

        Dim result = CreateSweep(app.MemoryGeometryBuilder, feat, firstCenterPt, firstDir)

        Dim data = feat.Parameters

        'SW API Bug wheer edit body cannot be removed, instead always acquiring the edit bodies and merge or return as is depending on the setting
        If Not data.Merge Then
            result = result.Union(data.EditBodies.Select(Function(b) b.Copy).Cast(Of ISwTempBody)).ToArray()
        End If

        alignDim = Sub(name, [dim])
                       Select Case name
                           Case NameOf(SweepEdgesData.Radius)
                               Me.AlignRadialDimension([dim], firstCenterPt, firstDir)
                       End Select
                   End Sub

        Return result

    End Function

    Public Overrides Function CreatePreviewGeometry(app As ISwApplication, doc As ISwDocument, feat As ISwMacroFeature(Of SweepEdgesData),
                                                    page As SweepEdgesData, ByRef shouldHidePreviewEdit As ShouldHidePreviewEditBodyDelegate(Of SweepEdgesData, SweepEdgesData),
                                                    ByRef assignPreviewColor As AssignPreviewBodyColorDelegate) As ISwTempBody()

        Dim firstCenterPt As Structures.Point = Nothing
        Dim firstDir As Vector = Nothing

        Dim result = CreateSweep(app.MemoryGeometryBuilder, feat, firstCenterPt, firstDir)

        shouldHidePreviewEdit = Function(body As IXBody, data As SweepEdgesData, prpPage As SweepEdgesData) As Boolean
                                    Return data.Merge
                                End Function

        assignPreviewColor = Sub(body As IXBody, ByRef color As Color)
                                 color = Color.FromArgb(100, Color.Blue)
                             End Sub

        Return result

    End Function

    Private Function CreateSweep(geomBuilder As IXMemoryGeometryBuilder, feat As ISwMacroFeature(Of SweepEdgesData), ByRef firstCenterPt As Structures.Point, ByRef firstDir As Vector) As ISwTempBody()

        Dim result = New List(Of ISwTempBody)()
        firstCenterPt = Nothing
        firstDir = Nothing

        Dim data = feat.Parameters

        For Each edgeGroup In data.Edges.GroupBy(Function(e) e.Body, New SweepEdgesBodyEqualityComparer(data.Merge))

            Dim mergedBody As ISwTempBody = Nothing

            If data.Merge Then
                'Edit bodies are permanent bodies that can be cast to memory bodies (for boolean operations) - need to get corresponding edit body to perform boolean operations
                mergedBody = data.EditBodies.OfType(Of ISwTempBody).FirstOrDefault(Function(b) b.Equals(edgeGroup.Key))
            End If

            'Need to get curves of edges before mergin as object might be disconnected
            For Each path In edgeGroup.Select(Function(e) e.Definition).ToArray()

                Dim startPt = path.StartPoint.Coordinate
                Dim endPt = path.EndPoint.Coordinate
                Dim dir = startPt - endPt

                If firstCenterPt Is Nothing Then
                    firstCenterPt = startPt
                End If

                If firstDir Is Nothing Then
                    firstDir = dir
                End If

                Dim profile = geomBuilder.CreateCircle(startPt, dir, data.Radius * 2)
                Dim profileRegion = geomBuilder.CreateRegionFromSegments(profile)
                Dim region = geomBuilder.CreatePlanarSheet(profileRegion).Bodies.First()
                Dim sweep = geomBuilder.CreateSolidExtrusion(path.Length, dir, region)
                Dim body = sweep.Bodies.OfType(Of ISwTempBody)().First()

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

        Return result.ToArray()

    End Function

End Class
