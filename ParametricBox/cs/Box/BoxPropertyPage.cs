using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Base.Enums;
using Xarial.XCad.Examples.Sw.ParametricBox.Properties;
using Xarial.XCad.Features;
using Xarial.XCad.Geometry;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.Geometry;
using Xarial.XCad.SolidWorks.UI.PropertyPage;
using Xarial.XCad.UI.PropertyPage.Attributes;
using Xarial.XCad.UI.PropertyPage.Base;
using Xarial.XCad.UI.PropertyPage.Enums;
using Xarial.XCad.UI.PropertyPage.Services;
using Xarial.XCad.UI.PropertyPage.Structures;

namespace Xarial.XCad.Examples.Sw.ParametricBox
{
    [ComVisible(true)]
    [Guid("77BCB4E3-D789-41E2-AD3B-9E3A053B2B19")]
    [Icon(typeof(Resources), nameof(Resources.box_icon))]
    [Title("Create Box")]
    public class BoxPropertyPage : SwPropertyManagerPageHandler
    {
        private class PlanarRegionSelectionFilter : ISelectionCustomFilter
        {
            public void Filter(IControl selBox, IXSelObject selection, SelectionCustomFilterArguments args)
            {
                args.Filter = selection is IXPlanarRegion;

                if (!args.Filter)
                {
                    args.Reason = "Select planar face or plane";
                }
            }
        }

        public class LocationGroup
        {
            [StandardControlIcon(BitmapLabelType_e.SelectFace)]
            [Description("Face or plane to place box on")]
            [SelectionBoxOptions(Filters = new SelectType_e[] { SelectType_e.Faces, SelectType_e.Planes },
                CustomFilter = typeof(PlanarRegionSelectionFilter))]
            public IXEntity PlaneOrFace { get; set; }
        }

        public class FilletEnabledHandler : IDependencyHandler
        {
            public void UpdateState(IXApplication app, IControl source, IControl[] dependencies)
            {
                source.Enabled = (bool)dependencies.First().GetValue();
            }
        }

        public class SizeGroup
        {
            [NumberBoxOptions(NumberBoxUnitType_e.Length, 0, 1000, 0.01, false, 0.02, 0.001)]
            [Description("Width of the box")]
            [Icon(typeof(Resources), nameof(Resources.width_icon))]
            public double Width { get; set; }

            [NumberBoxOptions(NumberBoxUnitType_e.Length, 0, 1000, 0.01, false, 0.02, 0.001)]
            [Description("Height of the box")]
            [Icon(typeof(Resources), nameof(Resources.height_icon))]
            public double Height { get; set; }

            [NumberBoxOptions(NumberBoxUnitType_e.Length, 0, 1000, 0.01, false, 0.02, 0.001)]
            [Description("Length of the box")]
            [Icon(typeof(Resources), nameof(Resources.length_icon))]
            public double Length { get; set; }
        }

        public class ParametersGroup 
        {
            [ControlOptions(align: ControlLeftAlign_e.Indent)]
            public bool Reverse { get; set; }

            [Title("Add Fillet")]
            [ControlOptions(align: ControlLeftAlign_e.Indent)]
            [ControlTag(nameof(AddFillet))]
            public bool AddFillet { get; set; }

            [NumberBoxOptions(NumberBoxUnitType_e.Length, 0, 1000, 0.01, false, 0.02, 0.001)]
            [Description("Radius of the fillet")]
            [StandardControlIcon(BitmapLabelType_e.Radius)]
            [DependentOn(typeof(FilletEnabledHandler), nameof(AddFillet))]
            public double FilletRadius { get; set; }
        }

        public LocationGroup Location { get; }
        public SizeGroup Size { get; }
        public ParametersGroup Parameters { get; }

        public BoxPropertyPage()
        {
            Location = new LocationGroup();
            Size = new SizeGroup();
            Parameters = new ParametersGroup();
        }
    }
}
