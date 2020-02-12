using CubeExample.Properties;
using System.Runtime.InteropServices;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Features.CustomFeature;
using Xarial.XCad.Features.CustomFeature.Delegates;
using Xarial.XCad.Geometry;
using Xarial.XCad.Geometry.Structures;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.Documents;
using Xarial.XCad.SolidWorks.Features.CustomFeature;
using Xarial.XCad.SolidWorks.Geometry;

namespace CubeExample
{
    [ComVisible(true)]
    [Title("Box")]
    [Icon(typeof(Resources), nameof(Resources.box_icon))]
    public class BoxMacroFeatureDef : SwMacroFeatureDefinition<BoxData, BoxData>
    {
        public override SwBody[] CreateGeometry(SwApplication app, SwDocument model, 
            BoxData data, bool isPreview, out AlignDimensionDelegate<BoxData> alignDim)
        {
            var baseCenter = new Point(0, 0, 0);

            var box = (SwBody)app.GeometryBuilder.CreateBox(baseCenter,
                new Vector(0, 1, 0), data.Width, data.Length, data.Height);

            alignDim = (n, d) =>
            {
                switch (n)
                {
                    case nameof(BoxData.Width):
                        this.AlignLinearDimension(d,
                            baseCenter
                            .Move(new Vector(-1, 0, 0), data.Width / 2)
                            .Move(new Vector(0, 0, -1), data.Length / 2),
                            new Vector(1, 0, 0));
                        break;

                    case nameof(BoxData.Length):
                        this.AlignLinearDimension(d,
                            baseCenter
                            .Move(new Vector(1, 0, 0), data.Width / 2)
                            .Move(new Vector(0, 0, 1), data.Length / 2),
                            new Vector(0, 0, -1));
                        break;

                    case nameof(BoxData.Height):
                        this.AlignLinearDimension(d,
                            baseCenter
                            .Move(new Vector(1, 0, 0), data.Width / 2)
                            .Move(new Vector(0, 0, -1), data.Length / 2),
                            new Vector(0, 1, 0));
                        break;
                }
            };

            return new SwBody[] { box };
        }
    }
}
