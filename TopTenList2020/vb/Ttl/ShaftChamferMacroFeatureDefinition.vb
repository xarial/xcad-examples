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

        Public Overrides Function CreateGeometry(ByVal app As ISwApplication, ByVal model As ISwDocument, ByVal data As ShaftChamferData, ByVal isPreview As Boolean, <Out> ByRef alignDim As AlignDimensionDelegate(Of ShaftChamferData)) As ISwBody()

            Dim planarFace = data.Edge.AdjacentEntities.OfType(Of ISwPlanarFace)().First()
            Dim sense As Boolean = planarFace.Face.FaceInSurfaceSense()

            Dim dir = planarFace.Definition.Plane.Normal * IIf(sense, 1, -1)
            Dim centerPt = data.Edge.Definition.Center
            Dim largeRad = data.Edge.Definition.Diameter / 2

            If data.Radius >= largeRad Then
                Throw New Exception($"Specified radius must not exceed {Math.Round(largeRad * 1000, 2)} mm")
            End If

            Dim x = largeRad - data.Radius
            Dim height = x / Math.Tan(data.Angle)
            Dim coneBody = CType(app.MemoryGeometryBuilder.CreateSolidCone(centerPt, dir, data.Radius * 2, largeRad * 2, height).Bodies.First(), ISwBody)
            Dim cylBody = CType(app.MemoryGeometryBuilder.CreateSolidCylinder(centerPt, dir, largeRad * 2, height).Bodies.First(), ISwBody)
            Dim targBody = data.Body

            If isPreview Then
                targBody = targBody.Copy()
            End If

            Dim result = targBody.Substract(cylBody.Substract(coneBody).First()).First()

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

            Return New ISwBody() {result}

        End Function
    End Class

End Namespace
