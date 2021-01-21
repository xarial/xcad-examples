using FormsAndWpfControls.Properties;
using System.Windows.Controls;
using Xarial.XCad.Base.Attributes;

namespace FormsAndWpfControls
{
    [Icon(typeof(Resources), nameof(Properties.Resources.wpf_icon))]
    public partial class WpfUserControl : UserControl
    {
        public WpfUserControl()
        {
            InitializeComponent();
        }
    }
}
