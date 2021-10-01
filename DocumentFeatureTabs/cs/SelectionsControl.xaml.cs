using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Examples.DocumentFeatureTabs.Properties;

namespace Xarial.XCad.Examples.DocumentFeatureTabs
{
    [Title("Selections Watcher")]
    [Icon(typeof(Resources), nameof(Properties.Resources.selections_icon))]
    public partial class SelectionsControl : UserControl
    {
        public SelectionsControl()
        {
            InitializeComponent();
        }
    }
}
