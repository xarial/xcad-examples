using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xarial.XCad.Examples.DocumentFeatureTabs
{
    public class SelectionsVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int m_SelectionsCount;

        public int SelectionsCount 
        {
            get => m_SelectionsCount;
            set 
            {
                m_SelectionsCount = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectionsCount)));
            }
        }
    }
}
