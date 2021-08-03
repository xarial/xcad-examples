using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad;
using Xarial.XCad.Documents;
using Xarial.XCad.Features.CustomFeature;
using Xarial.XCad.Features.CustomFeature.Delegates;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.Documents;
using Xarial.XCad.SolidWorks.Features.CustomFeature;
using Xarial.XCad.SolidWorks.Geometry;
using Xarial.XCad.SolidWorks.UI.PropertyPage;
using Xarial.XCad.UI.PropertyPage.Attributes;
using Xarial.XCad.UI.PropertyPage.Enums;
using static XCad.Examples.FurnitureConfigurator.CabinetConfiguratorData;

namespace XCad.Examples.FurnitureConfigurator
{
    public class CabinetSizeData
    {
        [NumberBoxOptions(NumberBoxUnitType_e.Length, 1.5, 2.4, 0.1, true, 0.2, 0.1)]
        [Description("Cabinet Width")]
        [StandardControlIcon(BitmapLabelType_e.LinearDistance1)]
        public double Width { get; set; } = 1.8;

        [NumberBoxOptions(NumberBoxUnitType_e.Length, 0.6, 1.2, 0.1, true, 0.2, 0.1)]
        [Description("Cabinet Height")]
        [StandardControlIcon(BitmapLabelType_e.Thickness1)]
        public double Height { get; set; } = 0.9;

        [NumberBoxOptions(NumberBoxUnitType_e.Length, 0.3, 0.6, 0.1, true, 0.2, 0.1)]
        [Description("Cabinet Depth")]
        [StandardControlIcon(BitmapLabelType_e.Depth)]
        public double Depth { get; set; } = 0.5;

        [NumberBoxOptions(NumberBoxUnitType_e.UnitlessInteger, 1, 6, 1, true, 2, 1)]
        [StandardControlIcon(BitmapLabelType_e.LinearPattern)]
        public int NumberOfDrawers { get; set; } = 3;

        [NumberBoxOptions(NumberBoxUnitType_e.Length, 0.3, 0.6, 0.1, true, 0.2, 0.1)]
        public double DrawerWidth { get; set; } = 0.5;

        [StandardControlIcon(BitmapLabelType_e.LinearDistance2)]
        public HandleType_e DrawerHandleType { get; set; } = HandleType_e.D;
    }

    [ComVisible(true)]
    [PageOptions(PageOptions_e.OkayButton | PageOptions_e.CancelButton | PageOptions_e.PushpinButton | PageOptions_e.LockedPage)]
    public class CabinetConfiguratorData : SwPropertyManagerPageHandler
    {
        public class OrderGroup 
        {
            public string Dummy { get; set; }
        }

        public CabinetSizeData Size { get; set; }

        public OrderGroup Order { get; set; }

        public CabinetConfiguratorData() 
        {
            Size = new CabinetSizeData();
            Order = new OrderGroup();
        }
    }

    [ComVisible(true)]
    public class CabinetConfiguratorMacroFeatureDefinition : SwMacroFeatureDefinition<CabinetSizeData, CabinetConfiguratorData>
    {
        protected override void OnEditingCompleting(IXApplication app, IXDocument doc, IXCustomFeature<CabinetSizeData> feat, CabinetSizeData data, PageCloseReasons_e reason)
        {
            if (reason == PageCloseReasons_e.Okay || reason == PageCloseReasons_e.Apply)
            {
                var svc = new CabinetConfiguratorService();
                svc.Configure((IXAssembly)doc, data.Width, data.Height, data.Depth, 
                    data.NumberOfDrawers, data.DrawerWidth, data.DrawerHandleType);
            }
        }

        public override CabinetSizeData ConvertPageToParams(CabinetConfiguratorData par)
            => par.Size;

        public override CabinetConfiguratorData ConvertParamsToPage(CabinetSizeData par)
            => new CabinetConfiguratorData() 
            {
                Size = par 
            };
    }
}
