Imports System
Imports System.Linq
Imports System.Runtime.InteropServices
Imports Ttl.My.Resources
Imports Xarial.XCad.Annotations
Imports Xarial.XCad.Base.Attributes
Imports Xarial.XCad.Features.CustomFeature
Imports Xarial.XCad.Geometry
Imports Xarial.XCad.Geometry.Structures
Imports Xarial.XCad.SolidWorks
Imports Xarial.XCad.SolidWorks.Documents
Imports Xarial.XCad.SolidWorks.Features.CustomFeature
Imports Xarial.XCad.SolidWorks.Geometry

Namespace ttl
    <ComVisible(True)>
    <Title("Shaft-Chamfer")>
    <Icon(GetType(Resources), NameOf(Resources.shaft_chamfer))>
    Public Partial Class ShaftChamferMacroFeatureDefinition
        Inherits SwMacroFeatureDefinition(Of ShaftChamferData, ShaftChamferData)

        Private NotInheritable Class FeatureParameters
            Friend Const Direction As String = "Direction"
            Friend Const CenterPoint As String = "CenterPoint"
            Friend Const LargeRadius As String = "LargeRadius"
            Friend Const Height As String = "Height"
        End Class

        Public Overrides Function CreateGeometry(ByVal app As ISwApplication, ByVal doc As ISwDocument, ByVal feat As ISwMacroFeature(Of ShaftChamferData)) As ISwBody()
            Dim data = feat.Parameters

            If data.Edge IsNot Nothing Then
                Dim planarFace = data.Edge.AdjacentEntities.OfType(Of ISwPlanarFace)().FirstOrDefault()

                If planarFace IsNot Nothing Then
                    Dim dir = planarFace.GetNormal() * -1

                    Dim centerPt = data.Edge.Definition.Geometry.CenterAxis.Point
                    Dim largeRad = data.Edge.Definition.Geometry.Diameter / 2

                    If data.Radius < largeRad Then
                        Dim height = (largeRad - data.Radius) / Math.Tan(data.Angle)

                        Dim coneBody = CType(app.MemoryGeometryBuilder.CreateSolidCone(centerPt, dir, data.Radius * 2, largeRad * 2, height).Bodies.First(), ISwTempBody)

                        Dim cylBody = CType(app.MemoryGeometryBuilder.CreateSolidCylinder(centerPt, dir, largeRad * 2, height).Bodies.First(), ISwTempBody)

                        Dim targBody = CType(data.Body, ISwTempBody)

                        Dim result = targBody.Substract(cylBody.Substract(coneBody).First()).First()

                        feat.Tags.Put(FeatureParameters.Direction, dir)
                        feat.Tags.Put(FeatureParameters.CenterPoint, centerPt)
                        feat.Tags.Put(FeatureParameters.LargeRadius, largeRad)
                        feat.Tags.Put(FeatureParameters.Height, height)

                        Return New ISwBody() {result}
                    Else
                        Throw New UserException($"Specified radius must not exceed {Math.Round(largeRad * 1000, 2)} mm")
                    End If
                Else
                    Throw New UserException("Failed to find the planar face adjacent to edge")
                End If
            Else
                Throw New UserException("Select circular edge")
            End If
        End Function

        Public Overrides Sub OnAlignDimension(ByVal feat As IXCustomFeature(Of ShaftChamferData), ByVal paramName As String, ByVal [dim] As IXDimension)

            Dim dir = feat.Tags.Get(Of Vector)(FeatureParameters.Direction)
            Dim centerPt = feat.Tags.Get(Of Point)(FeatureParameters.CenterPoint)
            Dim largeRad = feat.Tags.Get(Of Double)(FeatureParameters.LargeRadius)
            Dim height = feat.Tags.Get(Of Double)(FeatureParameters.Height)

            Select Case paramName
                Case NameOf(ShaftChamferData.Radius)
                    AlignRadialDimension([dim], centerPt, dir)

                Case NameOf(ShaftChamferData.Angle)
                    Dim refVec As Vector
                    Dim yVec = New Vector(0, 1, 0)
                    If dir.IsSame(yVec) Then
                        refVec = New Vector(1, 0, 0)
                    Else
                        refVec = yVec.Cross(dir)
                    End If

                    Dim refPt = centerPt.Move(refVec, largeRad)
                    Dim anglCenterPt = refPt.Move(dir, height)

                    AlignAngularDimension([dim], anglCenterPt, refPt, dir.Cross(refVec))
            End Select
        End Sub
    End Class
End Namespace
