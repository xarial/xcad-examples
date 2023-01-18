using System;
using System.Linq;
using System.Runtime.InteropServices;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Documents;
using Xarial.XCad.Features.CustomFeature;
using Xarial.XCad.Features.CustomFeature.Delegates;
using Xarial.XCad.Geometry;
using Xarial.XCad.Geometry.Structures;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.Documents;
using Xarial.XCad.SolidWorks.Features.CustomFeature;
using Xarial.XCad.SolidWorks.Geometry;
using SolidWorks.Interop.sldworks;
using Xarial.XCad.Examples.Sw.ParametricBox.Properties;

namespace Xarial.XCad.Examples.Sw.ParametricBox
{
    [ComVisible(true)]
    [Guid("ADECF50C-EC85-4266-8FF5-DFE39763EEB6")]
    [Icon(typeof(Resources), nameof(Resources.box_icon))]
    [Title("Box")]
    public class BoxMacroFeatureDefinition : SwMacroFeatureDefinition<BoxMacroFeatureData, BoxPropertyPage>
    {
        public override BoxMacroFeatureData ConvertPageToParams(IXApplication app, IXDocument doc, BoxPropertyPage page, BoxMacroFeatureData cudData)
            => new BoxMacroFeatureData()
            {
                Height = page.Size.Height,
                Length = page.Size.Length,
                Width = page.Size.Width,
                PlaneOrFace = page.Location.PlaneOrFace,
                Reverse = page.Parameters.Reverse,
                FilletRadius = page.Parameters.AddFillet ? page.Parameters.FilletRadius: 0
            };

        public override BoxPropertyPage ConvertParamsToPage(IXApplication app, IXDocument doc, BoxMacroFeatureData par)
        {
            var page = new BoxPropertyPage();
            page.Size.Height = par.Height;
            page.Size.Width = par.Width;
            page.Size.Length = par.Length;
            page.Parameters.Reverse = par.Reverse;
            page.Parameters.AddFillet = par.FilletRadius > 0;
            page.Parameters.FilletRadius = par.FilletRadius > 0 ? par.FilletRadius : 0.01;
            page.Location.PlaneOrFace = par.PlaneOrFace;

            return page;
        }

        public override ISwBody[] CreateGeometry(ISwApplication app, ISwDocument model, ISwMacroFeature<BoxMacroFeatureData> feat,
            out AlignDimensionDelegate<BoxMacroFeatureData> alignDim)
        {
            var data = feat.Parameters;

            var face = data.PlaneOrFace;

            Point pt;
            Vector dir;
            Vector refDir;

            if (face is IXPlanarRegion)
            {
                var plane = ((IXPlanarRegion)face).Plane;

                pt = plane.Point;
                dir = plane.Normal;
                refDir = plane.Reference;
            }
            else
            {
                throw new UserException("Select planar face or plane for the location");
            }

            if (data.Reverse)
            {
                dir *= -1;
            }

            var box = (ISwBody)app.MemoryGeometryBuilder.CreateSolidBox(
                pt, dir, refDir,
                data.Width, data.Length, data.Height).Bodies.First();

            if (data.FilletRadius > 0) 
            {
                var edges = (box.Body.GetEdges() as object[]).Cast<IEdge>().ToArray();
                    
                if (((object[])box.Body.AddConstantFillets(data.FilletRadius, edges))?.Any() != true) 
                {
                    throw new UserException("Failed to apply fillet");
                }
            }

            var secondRefDir = refDir.Cross(dir);

            alignDim = (n, d) =>
            {
                switch (n)
                {
                    case nameof(BoxMacroFeatureData.Width):
                        this.AlignLinearDimension(d,
                            pt
                            .Move(refDir * -1, data.Width / 2)
                            .Move(secondRefDir * -1, data.Length / 2),
                            refDir);
                        break;

                    case nameof(BoxMacroFeatureData.Length):
                        this.AlignLinearDimension(d,
                            pt
                            .Move(refDir, data.Width / 2)
                            .Move(secondRefDir * -1, data.Length / 2),
                            secondRefDir);
                        break;

                    case nameof(BoxMacroFeatureData.Height):
                        this.AlignLinearDimension(d,
                            pt
                            .Move(refDir, data.Width / 2)
                            .Move(secondRefDir * -1, data.Length / 2),
                            dir);
                        break;
                }
            };


            return new ISwBody[] { box };
        }
    }
}
