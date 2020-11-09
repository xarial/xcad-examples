Imports System
Imports System.Runtime.InteropServices
Imports Ttl.My.Resources
Imports Xarial.XCad.Base.Attributes
Imports Xarial.XCad.Features.CustomFeature.Attributes
Imports Xarial.XCad.Features.CustomFeature.Enums
Imports Xarial.XCad.SolidWorks.Geometry
Imports Xarial.XCad.SolidWorks.UI.PropertyPage
Imports Xarial.XCad.UI.PropertyPage.Attributes
Imports Xarial.XCad.UI.PropertyPage.Enums

Namespace ttl

    <Title("Insert Shaft Chamfer")>
    <Icon(GetType(Resources), NameOf(Resources.shaft_chamfer))>
    <ComVisible(True)>
    Public Class ShaftChamferData
        Inherits SwPropertyManagerPageHandler

        Private m_Edge As ISwCircularEdge

        <ExcludeControl>
        <ParameterEditBody>
        Public Property Body As ISwBody

        Public Property Edge As ISwCircularEdge
            Get
                Return m_Edge
            End Get
            Set(ByVal value As ISwCircularEdge)
                m_Edge = value
                Body = value?.Body
            End Set
        End Property

        <ParameterDimension(CustomFeatureDimensionType_e.Radial)>
        <NumberBoxOptions(NumberBoxUnitType_e.Length, 0, 1000, 0.001, True, 0.01, 0.005)>
        <StandardControlIcon(BitmapLabelType_e.Radius)>
        <Title("Radius")>
        Public Property Radius As Double = 0.02

        <ParameterDimension(CustomFeatureDimensionType_e.Angular)>
        <NumberBoxOptions(NumberBoxUnitType_e.Angle, 0, Math.PI / 2, Math.PI / 180, False, Math.PI / 90, Math.PI / 360)>
        <StandardControlIcon(BitmapLabelType_e.AngularDistance)>
        <Title("Angle")>
        Public Property Angle As Double = Math.PI / 9

    End Class

End Namespace
