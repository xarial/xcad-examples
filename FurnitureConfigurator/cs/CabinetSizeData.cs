using Xarial.XCad.Features.CustomFeature.Attributes;
using Xarial.XCad.Features.CustomFeature.Enums;
using XCad.Examples.FurnitureConfigurator.Enums;

namespace XCad.Examples.FurnitureConfigurator
{
    public class CabinetSizeData 
    {
        [ParameterDimension(CustomFeatureDimensionType_e.Linear)]
        public double Width { get; set; }

        [ParameterDimension(CustomFeatureDimensionType_e.Linear)]
        public double Height { get; set; }

        [ParameterDimension(CustomFeatureDimensionType_e.Linear)]
        public double Depth { get; set; }

        public int NumberOfDrawers { get; set; }

        public double DrawerWidth { get; set; }

        public HandleType_e DrawerHandleType { get; set; }
    }
}
