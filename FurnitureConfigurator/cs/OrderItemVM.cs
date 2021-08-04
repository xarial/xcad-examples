using System.ComponentModel;

namespace XCad.Examples.FurnitureConfigurator
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
                NotifyChanged(nameof(IsSelected));
            }
        }

        public string Name { get; }

        public OrderItemStatus_e Status
        {
            get => m_Status;
            set
            {
                m_Status = value;
                NotifyChanged(nameof(Status));
            }
        }

        public OrderItemVM(string name) 
        {
            Name = name;
            IsSelected = true;
            Status = OrderItemStatus_e.Available;
        }

        private void NotifyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
