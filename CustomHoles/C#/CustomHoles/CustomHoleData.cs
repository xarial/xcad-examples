using Xarial.XCad.Features;
using Xarial.XCad.Features.CustomFeature.Attributes;
using Xarial.XCad.Geometry;

namespace Xarial.XCad.Examples
{
    public class CustomHoleData 
    {
        [ParameterEditBody]
        public IXBody EditBody { get; set; }

        public IXSketch2D Sketch { get; set; }

        public double Radius { get; set; } = 0.01;
        public double Depth { get; set; } = 0.01;
    }
}
