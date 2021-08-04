using System.Runtime.InteropServices;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.SolidWorks.UI.PropertyPage;
using Xarial.XCad.UI.PropertyPage.Attributes;
using XCad.Examples.FurnitureConfigurator.Properties;

namespace XCad.Examples.FurnitureConfigurator
{
    [ComVisible(true)]
    [Icon(typeof(Resources), nameof(Resources.cabinet_icon))]
    [Title("Cabinet")]
    public class CabinetConfiguratorPage : SwPropertyManagerPageHandler
    {
        public class OrderGroup 
        {
            [CustomControl(typeof(OrderControl))]
            [ControlOptions(height: 200)]
            public OrderVM Grid { get; set; }

            public OrderGroup() 
            {
                Grid = new OrderVM();
            }
        }

        public CabinetSizeGroup Size { get; set; }

        public OrderGroup Order { get; set; }

        public CabinetConfiguratorPage() 
        {
            Size = new CabinetSizeGroup();
            Order = new OrderGroup();
        }
    }
}
