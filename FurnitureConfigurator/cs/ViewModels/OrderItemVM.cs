using System.ComponentModel;
using Xarial.XToolkit.Wpf.Extensions;
using XCad.Examples.FurnitureConfigurator.Enums;

namespace XCad.Examples.FurnitureConfigurator.ViewModels
{
    public class OrderItemVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool m_IsSelected;
        private OrderItemStatus_e m_Status;

        public bool IsSelected
        {
            get => m_IsSelected;
            set 
            {
                m_IsSelected = value;
                this.NotifyChanged();
            }
        }

        public string Name { get; }

        public OrderItemStatus_e Status
        {
            get => m_Status;
            set
            {
                m_Status = value;
                this.NotifyChanged();
            }
        }

        public OrderItemVM(string name) 
        {
            Name = name;
            IsSelected = true;
            Status = OrderItemStatus_e.Available;
        }
    }
}
