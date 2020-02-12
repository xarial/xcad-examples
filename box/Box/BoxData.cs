using CubeExample.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Features.CustomFeature.Attributes;
using Xarial.XCad.Features.CustomFeature.Enums;
using Xarial.XCad.SolidWorks.UI.PropertyPage;
using Xarial.XCad.UI.PropertyPage.Attributes;
using Xarial.XCad.UI.PropertyPage.Enums;

namespace CubeExample
{
    [ComVisible(true)]
    [Icon(typeof(Resources), nameof(Resources.box_icon))]
    [Title("Create Box")]
    public class BoxData : SwPropertyManagerPageHandler
    {
        [StandardIcon(BitmapLabelType_e.Width)]
        [NumberBoxOptions(NumberBoxUnitType_e.Length, 0, 1000, 0.001, true, 0.01, 0.005)]
        [ParameterDimension(CustomFeatureDimensionType_e.Linear)]
        public double Width { get; set; } = 0.01;

        [Icon(typeof(Resources), nameof(Resources.height_icon))]
        [NumberBoxOptions(NumberBoxUnitType_e.Length, 0, 1000, 0.001, true, 0.01, 0.005)]
        [ParameterDimension(CustomFeatureDimensionType_e.Linear)]
        public double Height { get; set; } = 0.01;

        [StandardIcon(BitmapLabelType_e.LinearDistance)]
        [NumberBoxOptions(NumberBoxUnitType_e.Length, 0, 1000, 0.001, true, 0.01, 0.005)]
        [ParameterDimension(CustomFeatureDimensionType_e.Linear)]
        public double Length { get; set; } = 0.01;
    }
}
