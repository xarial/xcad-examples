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
        public override SwBody[] CreateGeometry(SwApplication app, SwDocument model, ShaftChamferData data, bool isPreview, out AlignDimensionDelegate<ShaftChamferData> alignDim)
        {
            var dir = data.Edge.AdjacentEntities.OfType<SwPlanarFace>().First().Normal * -1;

            var centerPt = data.Edge.Center;
            var largeRad = data.Edge.Radius;

            if (data.Radius >= largeRad)
            {
                throw new Exception($"Specified radius must not exceed {Math.Round(largeRad * 1000, 2)} mm");
            }

            var x = largeRad - data.Radius;
            var height = x / Math.Tan(data.Angle);

            var coneBody = (SwBody)app.GeometryBuilder.CreateCone(centerPt, dir, data.Radius, largeRad, height);

            var cylBody = (SwBody)app.GeometryBuilder.CreateCylinder(centerPt, dir, largeRad, height);

            var targBody = data.Body;

            if (isPreview)
            {
                targBody = targBody.ToTempBody();
            }

            var result = targBody - (cylBody - coneBody);

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

            return new SwBody[] { result };
        }
    }
}