using System.ComponentModel;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.UI.PropertyPage.Attributes;
using Xarial.XCad.UI.PropertyPage.Enums;
using XCad.Examples.FurnitureConfigurator.Enums;
using XCad.Examples.FurnitureConfigurator.Properties;

namespace XCad.Examples.FurnitureConfigurator
{
    public class CabinetSizeGroup
    {
        [NumberBoxOptions(NumberBoxUnitType_e.Length, 1.5, 2.4, 0.1, true, 0.2, 0.1)]
        [Description("Cabinet Width")]
        [Icon(typeof(Resources), nameof(Resources.width))]
        public double Width { get; set; } = 2.0;

        [NumberBoxOptions(NumberBoxUnitType_e.Length, 0.6, 1.2, 0.1, true, 0.2, 0.1)]
        [Description("Cabinet Height")]
        [Icon(typeof(Resources), nameof(Resources.height))]
        public double Height { get; set; } = 1.0;

        [NumberBoxOptions(NumberBoxUnitType_e.Length, 0.3, 0.6, 0.1, true, 0.2, 0.1)]
        [Description("Cabinet Depth")]
        [Icon(typeof(Resources), nameof(Resources.depth))]
        public double Depth { get; set; } = 0.5;

        [NumberBoxOptions(NumberBoxUnitType_e.UnitlessInteger, 1, 6, 1, true, 2, 1)]
        [Icon(typeof(Resources), nameof(Resources.drawers_count))]
        [Description("Number of Drawers")]
        public int NumberOfDrawers { get; set; } = 3;

        [NumberBoxOptions(NumberBoxUnitType_e.Length, 0.3, 0.6, 0.1, true, 0.2, 0.1)]
        [Icon(typeof(Resources), nameof(Resources.drawer_width))]
        [Description("Drawer Width")]
        public double DrawerWidth { get; set; } = 0.537;

        [Icon(typeof(Resources), nameof(Resources.door_handle))]
        [Description("Drawer Handle Type")]
        public HandleType_e DrawerHandleType { get; set; } = HandleType_e.D;
    }
}
