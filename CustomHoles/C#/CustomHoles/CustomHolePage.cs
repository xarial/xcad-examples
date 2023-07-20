using System.Runtime.InteropServices;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Examples.Properties;
using Xarial.XCad.Features;
using Xarial.XCad.SolidWorks.UI.PropertyPage;
using Xarial.XCad.UI.PropertyPage.Attributes;
using Xarial.XCad.UI.PropertyPage.Enums;

namespace Xarial.XCad.Examples
{
    [ComVisible(true)]
    [Icon(typeof(Resources), nameof(Resources.custom_hole))]
    [Title("Insert Custom Hole")]
    public class CustomHolePage : SwPropertyManagerPageHandler
    {
        public class InputGroup 
        {
            [Icon(typeof(Resources), nameof(Resources.sketch))]
            public IXSketch2D Sketch { get; set; }

            [NumberBoxOptions(NumberBoxUnitType_e.Length, 0, 1000, 0.01, false, 0.01, 0.001)]
            [StandardControlIcon(BitmapLabelType_e.Radius)]
            public double Radius { get; set; }

            [StandardControlIcon(BitmapLabelType_e.Depth)]
            [NumberBoxOptions(NumberBoxUnitType_e.Length, 0, 1000, 0.01, false, 0.01, 0.001)]
            public double Depth { get; set; }
        }

        public InputGroup Input { get; }

        public CustomHolePage() 
        {
            Input = new InputGroup();
        }
    }
}
