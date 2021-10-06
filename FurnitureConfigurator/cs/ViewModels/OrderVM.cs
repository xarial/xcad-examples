using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xarial.XToolkit.Wpf;
using XCad.Examples.FurnitureConfigurator.Enums;
using XCad.Examples.FurnitureConfigurator.Properties;

namespace XCad.Examples.FurnitureConfigurator.ViewModels
{
    public class OrderVM
    {
        public enum ItemType_e 
        {
            Frame,
            PanelBase,
            PanelEndLH,
            PanelEndRH,
            PanelTop,
            PanelRear,
            PanelInternal,
            Door,
            Drawer,
            Handle
        }

        public OrderItemVM[] Items { get; }

        public ICommand OrderCommand { get; }

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

            OrderCommand = new RelayCommand(OnOrder);
        }

        private void OnOrder()
        {
            try
            {
                Process.Start(Settings.Default.OrderPageUrl);
            }
            catch 
            {
            }
        }
    }
}
