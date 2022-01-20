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
Public Class SweepSketchData
    Inherits SwPropertyManagerPageHandler

    <ControlOptions(AddControlOptions_e.Visible Or AddControlOptions_e.Enabled, ControlLeftAlign_e.LeftEdge, 0, 0, -1, -1, -1, 50)>
    Public Property Edges As List(Of ISwLinearEdge)

    <NumberBoxOptions(NumberBoxUnitType_e.Length, 0.000001, 1000, 0.01, True, 0.02, 0.001)>
    <StandardControlIcon(BitmapLabelType_e.Radius)>
    <ParameterDimension(CustomFeatureDimensionType_e.Radial)>
    Public Property Radius As Double = 0.005

End Class
