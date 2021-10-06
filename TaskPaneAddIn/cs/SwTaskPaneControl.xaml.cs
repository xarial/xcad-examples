using System.ComponentModel;
using System.Windows.Controls;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Examples.SwTaskPaneAddIn.Properties;

namespace Xarial.XCad.Examples.SwTaskPaneAddIn
{
    [Icon(typeof(Resources), nameof(Properties.Resources.cd_icon))]
    [Title("WPF Task Pane Example")]
    [Description("Example of WPF control hosted in SOLIDWORKS Task Pane control")]
    public partial class SwTaskPaneControl : UserControl
    {
        public SwTaskPaneControl()
        {
            InitializeComponent();
        }
    }
}
