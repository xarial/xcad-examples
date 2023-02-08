using System;
using System.Linq;
using System.Runtime.InteropServices;
using ttl.Properties;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Features.CustomFeature;
using Xarial.XCad.Features.CustomFeature.Delegates;
using Xarial.XCad.Geometry;
using Xarial.XCad.Geometry.Structures;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.Documents;
using Xarial.XCad.SolidWorks.Features.CustomFeature;
using Xarial.XCad.SolidWorks.Geometry;

namespace ttl
{
    [ComVisible(true)]
    [Title("Shaft-Chamfer")]
    [Icon(typeof(Resources), nameof(Resources.shaft_chamfer))]
    public class ShaftChamferMacroFeatureDefinition : SwMacroFeatureDefinition<ShaftChamferData, ShaftChamferData>
    {
        public override ISwBody[] CreateGeometry(ISwApplication app, ISwDocument model, ShaftChamferData data, bool isPreview, out AlignDimensionDelegate<ShaftChamferData> alignDim)
        {
            var planarFace = data.Edge.AdjacentEntities.OfType<ISwPlanarFace>().First();
            var sense = planarFace.Face.FaceInSurfaceSense();

            var dir = planarFace.Definition.Plane.Normal * (sense ? 1 : -1);

            var centerPt = data.Edge.Definition.Center;
            var largeRad = data.Edge.Definition.Diameter / 2;

            if (data.Radius >= largeRad)
            {
                throw new Exception($"Specified radius must not exceed {Math.Round(largeRad * 1000, 2)} mm");
            }

            var x = largeRad - data.Radius;
            var height = x / Math.Tan(data.Angle);

            var coneBody = (ISwBody)(app.MemoryGeometryBuilder.CreateSolidCone(centerPt, dir, data.Radius * 2, largeRad * 2, height).Bodies.First());

            var cylBody = (ISwBody)(app.MemoryGeometryBuilder.CreateSolidCylinder(centerPt, dir, largeRad * 2, height).Bodies.First());

            var targBody = data.Body;

            if (isPreview)
            {
                targBody = (ISwBody)targBody.Copy();
            }

            var result = targBody.Substract(cylBody.Substract(coneBody).First()).First();

            alignDim = new AlignDimensionDelegate<ShaftChamferData>((p, d) =>
            {
                switch (p)
                {
                    case nameof(data.Radius):
                        this.AlignRadialDimension(d, centerPt, dir);
                        break;

                    case nameof(data.Angle):
                        Vector refVec;
                        var yVec = new Vector(0, 1, 0);
                        if (dir.IsSame(yVec))
                        {
                            refVec = new Vector(1, 0, 0);
                        }
                        else
                        {
                            refVec = yVec.Cross(dir);
                        }

                        var refPt = centerPt.Move(refVec, largeRad);
                        var anglCenterPt = refPt.Move(dir, height);

                        this.AlignAngularDimension(d, anglCenterPt, refPt, dir.Cross(refVec));
                        break;
                }
            });

            return new ISwBody[] { result };
        }
    }
}