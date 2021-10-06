using System.Linq;
using System.Runtime.InteropServices;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.SolidWorks.UI.PropertyPage;
using Xarial.XCad.UI.PropertyPage.Attributes;
using XCad.Examples.FurnitureConfigurator.DAL;
using XCad.Examples.FurnitureConfigurator.Enums;
using XCad.Examples.FurnitureConfigurator.Properties;
using XCad.Examples.FurnitureConfigurator.Services;
using XCad.Examples.FurnitureConfigurator.ViewModels;

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

            [ExcludeControl]
            public FurnitureDbContext Db { get; set; }

            public void UpdateStatuses(Cabinet cabinet) 
            {
                var door = Db.Doors.FirstOrDefault(d => d.Width == cabinet.DoorWidth && d.Height == cabinet.DoorHeight);

                var item = Grid.Items[(int)OrderVM.ItemType_e.Door];

                if (door == null)
                {
                    item.Status = OrderItemStatus_e.Custom;
                }
                else
                {
                    if (door.InStock)
                    {
                        item.Status = OrderItemStatus_e.Available;
                    }
                    else
                    {
                        item.Status = OrderItemStatus_e.OutOfStock;
                    }
                }
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
