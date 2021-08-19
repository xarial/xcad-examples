using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xarial.XCad.Examples.ProgressBar
{
    public class ProgressVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private double m_Progress;

        public double Progress
        {
            get => m_Progress;
            set 
            {
                m_Progress = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Progress)));
            }
        }
    }
}
