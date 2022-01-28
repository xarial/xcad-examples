using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Features.CustomFeature.Attributes;
using Xarial.XCad.Features.CustomFeature.Enums;
using Xarial.XCad.SolidWorks.Geometry;
using Xarial.XCad.SolidWorks.UI.PropertyPage;
using Xarial.XCad.UI.PropertyPage.Attributes;
using Xarial.XCad.UI.PropertyPage.Enums;
using Xarial.XCad.Geometry;
using Xarial.XCad.Examples.CoordinateSystemMacroFeature.Properties;

namespace Xarial.XCad.Examples.CoordinateSystemMacroFeature
{
    [ComVisible(true)]
    [Title("Coordinate System Ex")]
    [Icon(typeof(Resources), nameof(Resources.coord_system))]
    public class CoordinateSystemData : SwPropertyManagerPageHandler
    {
        [NumberBoxOptions(NumberBoxUnitType_e.Length, -1000, 1000, 0.01, true, 0.02, 0.001)]
        [Label("Origin X:")]
        public double X { get; set; } = 0;

        [NumberBoxOptions(NumberBoxUnitType_e.Length, -1000, 1000, 0.01, true, 0.02, 0.001)]
        [Label("Origin Y:")]
        public double Y { get; set; } = 0;

        [NumberBoxOptions(NumberBoxUnitType_e.Length, -1000, 1000, 0.01, true, 0.02, 0.001)]
        [Label("Origin Z:")]
        public double Z { get; set; } = 0;

        [NumberBoxOptions(NumberBoxUnitType_e.Angle, 0, Math.PI * 2, Math.PI * 2 / 36, true, Math.PI * 2 / 18, Math.PI * 2 / 360)]
        [Label("Rotation X:")]
        public double RotationX { get; set; } = 0;

        [NumberBoxOptions(NumberBoxUnitType_e.Angle, 0, Math.PI * 2, Math.PI * 2 / 36, true, Math.PI * 2 / 18, Math.PI * 2 / 360)]
        [Label("Rotation Y:")]
        public double RotationY { get; set; } = 0;

        [NumberBoxOptions(NumberBoxUnitType_e.Angle, 0, Math.PI * 2, Math.PI * 2 / 36, true, Math.PI * 2 / 18, Math.PI * 2 / 360)]
        [Label("Rotation Z:")]
        public double RotationZ { get; set; } = 0;
    }
}
