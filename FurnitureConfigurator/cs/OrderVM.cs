using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCad.Examples.FurnitureConfigurator
{
    public class OrderVM
    {
        public OrderItemVM[] Items { get; }

        public OrderVM() 
        {
            Items = new OrderItemVM[]
            {
                new OrderItemVM("Frame") { Status = OrderItemStatus_e.OutOfStock },
                new OrderItemVM("Panel Base"),
                new OrderItemVM("Panel End LH") { Status = OrderItemStatus_e.Custom },
                new OrderItemVM("Panel End RH"),
                new OrderItemVM("Panel Top"),
                new OrderItemVM("Panel Rear"),
                new OrderItemVM("Panel Internal"),
                new OrderItemVM("Door"),
                new OrderItemVM("Drawer"),
                new OrderItemVM("Handle")
            };
        }
    }
}
