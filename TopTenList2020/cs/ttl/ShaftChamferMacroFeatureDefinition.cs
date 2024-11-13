using System;
using System.Linq;
using System.Runtime.InteropServices;
using ttl.Properties;
using Xarial.XCad.Annotations;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Features.CustomFeature;
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
    public partial class ShaftChamferMacroFeatureDefinition : SwMacroFeatureDefinition<ShaftChamferData, ShaftChamferData>
    {
        private static class FeatureParameters 
        {
            internal const string Direction = "Direction";
            internal const string CenterPoint = "CenterPoint";
            internal const string LargeRadius = "LargeRadius";
            internal const string Height = "Height";
        }

        public override ISwBody[] CreateGeometry(ISwApplication app, ISwDocument doc, ISwMacroFeature<ShaftChamferData> feat)
        {
            var data = feat.Parameters;

            if (data.Edge != null)
            {
                var planarFace = data.Edge.AdjacentEntities.OfType<ISwPlanarFace>().FirstOrDefault();

                if (planarFace != null)
                {
                    var dir = planarFace.GetNormal() * -1;

                    var centerPt = data.Edge.Definition.Geometry.CenterAxis.Point;
                    var largeRad = data.Edge.Definition.Geometry.Diameter / 2;

                    if (data.Radius < largeRad)
                    {
                        var height = (largeRad - data.Radius) / Math.Tan(data.Angle);

                        var coneBody = (ISwTempBody)app.MemoryGeometryBuilder.CreateSolidCone(centerPt, dir, data.Radius * 2, largeRad * 2, height).Bodies.First();

                        var cylBody = (ISwTempBody)app.MemoryGeometryBuilder.CreateSolidCylinder(centerPt, dir, largeRad * 2, height).Bodies.First();

                        var targBody = (ISwTempBody)data.Body;

                        var result = targBody.Substract(cylBody.Substract(coneBody).First()).First();

                        feat.Tags.Put(FeatureParameters.Direction, dir);
                        feat.Tags.Put(FeatureParameters.CenterPoint, centerPt);
                        feat.Tags.Put(FeatureParameters.LargeRadius, largeRad);
                        feat.Tags.Put(FeatureParameters.Height, height);

                        return new ISwBody[] { result };
                    }
                    else 
                    {
                        throw new UserException($"Specified radius must not exceed {Math.Round(largeRad * 1000, 2)} mm");
                    }
                }
                else 
                {
                    throw new UserException("Failed to find the planar face adjacent to edge");
                }
            }
            else 
            {
                throw new UserException("Select circular edge");
            }
        }

        public override void OnAlignDimension(IXCustomFeature<ShaftChamferData> feat, string paramName, IXDimension dim)
        {
            var dir = feat.Tags.Get<Vector>(FeatureParameters.Direction);
            var centerPt = feat.Tags.Get<Point>(FeatureParameters.CenterPoint);
            var largeRad = feat.Tags.Get<double>(FeatureParameters.LargeRadius);
            var height = feat.Tags.Get<double>(FeatureParameters.Height);

            switch (paramName)
            {
                case nameof(ShaftChamferData.Radius):
                    this.AlignRadialDimension(dim, centerPt, dir);
                    break;

                case nameof(ShaftChamferData.Angle):
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

                    this.AlignAngularDimension(dim, anglCenterPt, refPt, dir.Cross(refVec));
                    break;
            }
        }
    }
}