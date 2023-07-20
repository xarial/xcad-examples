using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Xarial.XCad.Base;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Documents;
using Xarial.XCad.Examples.Properties;
using Xarial.XCad.Features;
using Xarial.XCad.Features.CustomFeature;
using Xarial.XCad.Features.CustomFeature.Delegates;
using Xarial.XCad.Geometry;
using Xarial.XCad.Geometry.Structures;
using Xarial.XCad.Sketch;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.Documents;
using Xarial.XCad.SolidWorks.Features;
using Xarial.XCad.SolidWorks.Features.CustomFeature;
using Xarial.XCad.SolidWorks.Geometry;

namespace Xarial.XCad.Examples
{
    [ComVisible(true), Guid("06AE2F4A-CFD8-4D94-B83C-84B26D84A72A")]
    [Icon(typeof(Resources), nameof(Resources.custom_hole))]
    [Title("Custom-Hole")]
    public class CustomHoleMacroFeatureDefinition : SwMacroFeatureDefinition<CustomHoleData, CustomHolePage>
    {
        public override CustomHoleData ConvertPageToParams(IXApplication app, IXDocument doc, CustomHolePage page, CustomHoleData curParams)
            => new CustomHoleData()
            {
                Sketch = page.Input.Sketch,
                Depth = page.Input.Depth,
                Radius = page.Input.Radius,
                EditBody = (page.Input.Sketch?.ReferenceEntity as IXPlanarFace)?.Body
            };

        public override CustomHolePage ConvertParamsToPage(IXApplication app, IXDocument doc, CustomHoleData par)
        {
            var page = new CustomHolePage();

            page.Input.Sketch = par.Sketch;
            page.Input.Depth = par.Depth;
            page.Input.Radius = par.Radius;

            return page;
        }

        public override ISwBody[] CreateGeometry(ISwApplication app, ISwDocument doc, ISwMacroFeature<CustomHoleData> feat)
        {
            var parameters = feat.Parameters;

            var cuttingBodies = CreateCuttingCylinders(app.MemoryGeometryBuilder, parameters.Sketch, parameters.Radius, parameters.Depth);

            var resBody = (IXMemoryBody)parameters.EditBody;

            foreach (var cutBody in cuttingBodies) 
            {
                var cutResult = resBody.Substract(cutBody);

                if (cutResult.Length == 1)
                {
                    resBody = cutResult.First();
                }
                else if (cutResult.Length > 1) 
                {
                    throw new Exception("Cut produces multiple bodies");
                }
                else if (cutResult.Length == 0)
                {
                    throw new Exception("Cut does not intersect body");
                }
            }

            return new ISwBody[] { (ISwBody)resBody };
        }

        public override ISwTempBody[] CreatePreviewGeometry(ISwApplication app, ISwDocument doc, ISwMacroFeature<CustomHoleData> feat,
            CustomHolePage page, out ShouldHidePreviewEditBodyDelegate<CustomHoleData, CustomHolePage> shouldHidePreviewEdit,
            out AssignPreviewBodyColorDelegate assignPreviewColor)
        {
            var parameters = feat.Parameters;

            var cuttingBodies = CreateCuttingCylinders(app.MemoryGeometryBuilder, parameters.Sketch, parameters.Radius, parameters.Depth);

            //creting a copy of the original body and it will be used for the preview
            var editBodyPreview = parameters.EditBody.Copy();

            shouldHidePreviewEdit = (b, d, p) => true;
            assignPreviewColor = (IXBody b, out System.Drawing.Color c) =>
            {
                if (b.Equals(editBodyPreview))
                {
                    //edit body color is semi-transparent yellow
                    c = System.Drawing.Color.FromArgb(100, System.Drawing.Color.Yellow);
                }
                else 
                {
                    //cut bodies are red
                    c = System.Drawing.Color.Red;
                }
            };

            //returning edit body copy and all cutting bodies for the preview
            return new ISwTempBody[] { (ISwTempBody)editBodyPreview }.Union(cuttingBodies.Cast<ISwTempBody>()).ToArray();
        }
        
        private IXMemoryBody[] CreateCuttingCylinders(IXMemoryGeometryBuilder memGeomBuilder, IXSketch2D sketch, double radius, double depth) 
        {
            if (sketch == null)
            {
                //throwing this as UserException so its content can be displayed to the user in the tooltip
                throw new UserException("Select sketch");
            }

            if (!(sketch.ReferenceEntity is IXPlanarRegion))
            {
                throw new UserException("Only faces on the planar faces are supported");
            }

            if (!sketch.Entities.Filter<IXSketchPoint>().Any())
            {
                throw new UserException("No points are found in the sketch");
            }

            var cylinders = new List<IXMemoryBody>();

            var baseFace = (IXPlanarFace)sketch.ReferenceEntity;

            var dir = baseFace.GetNormal() * -1; //GetNormal will return the normal of the face (considering the face in surface sense) and the cut direction is always opposite

            var transform = sketch.Plane.GetTransformation();//sketch transformation realtive to the part

            foreach (var pt in sketch.Entities.Filter<IXSketchPoint>()) 
            {
                var centerPt = pt.Coordinate.Transform(transform);
                var extr = memGeomBuilder.CreateSolidCylinder(centerPt, dir, radius, depth);
                cylinders.Add((IXMemoryBody)extr.Bodies.First());
            }

            return cylinders.ToArray();
        }
    }
}
