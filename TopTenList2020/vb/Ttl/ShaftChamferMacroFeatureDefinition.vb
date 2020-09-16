Imports System.Runtime.InteropServices
Imports Ttl.My.Resources
Imports Xarial.XCad.Base.Attributes
Imports Xarial.XCad.Features.CustomFeature
Imports Xarial.XCad.Features.CustomFeature.Delegates
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
    Public Class ShaftChamferMacroFeatureDefinition
        Inherits SwMacroFeatureDefinition(Of ShaftChamferData, ShaftChamferData)

        Public Overrides Function CreateGeometry(ByVal app As SwApplication, ByVal model As SwDocument, ByVal data As ShaftChamferData, ByVal isPreview As Boolean, <Out> ByRef alignDim As AlignDimensionDelegate(Of ShaftChamferData)) As SwBody()

            Dim dir = data.Edge.AdjacentEntities.OfType(Of SwPlanarFace)().First().Normal * -1
            Dim centerPt = data.Edge.Center
            Dim largeRad = data.Edge.Radius

            If data.Radius >= largeRad Then
                Throw New Exception($"Specified radius must not exceed {Math.Round(largeRad * 1000, 2)} mm")
            End If

            Dim x = largeRad - data.Radius
            Dim height = x / Math.Tan(data.Angle)
            Dim coneBody = CType(app.GeometryBuilder.CreateCone(centerPt, dir, data.Radius, largeRad, height), SwBody)
            Dim cylBody = CType(app.GeometryBuilder.CreateCylinder(centerPt, dir, largeRad, height), SwBody)
            Dim targBody = data.Body

            If isPreview Then
                targBody = targBody.ToTempBody()
            End If

            Dim result = targBody - (cylBody - coneBody)
            alignDim = New AlignDimensionDelegate(Of ShaftChamferData)(
                Sub(p, d)
                    Select Case p
                        Case NameOf(data.Radius)
                            Me.AlignRadialDimension(d, centerPt, dir)
                        Case NameOf(data.Angle)
                            Dim refVec As Vector
                            Dim yVec = New Vector(0, 1, 0)

                            If dir.IsSame(yVec) Then
                                refVec = New Vector(1, 0, 0)
                            Else
                                refVec = yVec.Cross(dir)
                            End If

                            Dim refPt = centerPt.Move(refVec, largeRad)
                            Dim anglCenterPt = refPt.Move(dir, height)
                            Me.AlignAngularDimension(d, anglCenterPt, refPt, dir.Cross(refVec))
                    End Select
                End Sub)

            Return New SwBody() {result}

        End Function
    End Class

End Namespace
