Imports System.Runtime.InteropServices
Imports Xarial.XCad.Base.Attributes
Imports Xarial.XCad.Features.CustomFeature.Attributes
Imports Xarial.XCad.Features.CustomFeature.Enums
Imports Xarial.XCad.SolidWorks.Geometry
Imports Xarial.XCad.SolidWorks.UI.PropertyPage
Imports Xarial.XCad.UI.PropertyPage.Attributes
Imports Xarial.XCad.UI.PropertyPage.Enums

<ComVisible(True)>
<Title("Swept Edges")>
Public Class SweepEdgesData
    Inherits SwPropertyManagerPageHandler

    Dim m_Edges As List(Of ISwLinearEdge)
    Dim m_Merge As Boolean

    <ControlOptions(AddControlOptions_e.Visible Or AddControlOptions_e.Enabled, ControlLeftAlign_e.LeftEdge, 0, 0, -1, -1, -1, 50)>
    Public Property Edges As List(Of ISwLinearEdge)
        Get
            Return m_Edges
        End Get
        Set(ByVal value As List(Of ISwLinearEdge))
            m_Edges = value
            ResolveMerge()
        End Set
    End Property

    <NumberBoxOptions(NumberBoxUnitType_e.Length, 0.000001, 1000, 0.01, True, 0.02, 0.001)>
    <StandardControlIcon(BitmapLabelType_e.Radius)>
    <ParameterDimension(CustomFeatureDimensionType_e.Radial)>
    Public Property Radius As Double = 0.005

    <ControlOptions(, ControlLeftAlign_e.Indent)>
    Public Property Merge As Boolean
        Get
            Return m_Merge
        End Get
        Set(ByVal value As Boolean)
            m_Merge = value
            ResolveMerge()
        End Set
    End Property

    <ParameterEditBody>
    <ExcludeControl>
    Public Property EditBodies As List(Of ISwBody)

    Private Sub ResolveMerge()
        EditBodies = New List(Of ISwBody)()
        If Merge Then
            If m_Edges IsNot Nothing Then
                For Each edge As ISwLinearEdge In m_Edges
                    Dim body = edge.Body
                    If Not EditBodies.Any(Function(b) b.Equals(body)) Then
                        EditBodies.Add(body)
                    End If
                Next
            End If
        End If
    End Sub

End Class
