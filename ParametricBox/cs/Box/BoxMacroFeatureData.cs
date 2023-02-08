using Xarial.XCad.Features.CustomFeature.Attributes;
using Xarial.XCad.Features.CustomFeature.Enums;
using Xarial.XCad.Geometry;

namespace Xarial.XCad.Examples.Sw.ParametricBox
{
    public class BoxMacroFeatureData
    {
        public IXEntity PlaneOrFace { get; set; }

        [ParameterDimension(CustomFeatureDimensionType_e.Linear)]
        public double Width { get; set; } = 0.1;

        [ParameterDimension(CustomFeatureDimensionType_e.Linear)]
        public double Height { get; set; } = 0.1;

        [ParameterDimension(CustomFeatureDimensionType_e.Linear)]
        public double Length { get; set; } = 0.1;

        public bool Reverse { get; set; } = false;

        public double FilletRadius { get; set; } = 0;
    }
}
