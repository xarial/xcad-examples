using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Examples.PMPage.CSharp.Page.Groups;
using Xarial.XCad.Examples.PMPage.CSharp.Properties;

namespace Xarial.XCad.Examples.PMPage.CSharp.Page.Tabs
{
    [DisplayName("Advanced Controls")]
    [Description("Second tab with custom title and icon")]
    [Icon(typeof(Resources), nameof(Resources.xarial))]
    public class SecondTabCustomIcon 
    {
        public AdvancedSelectionBoxGroup AdvancedSelectionBox { get; set; }

        public ItemSourceControlsGroup ItemsSourceControls { get; set; }

        public CustomControlsGroup CustomControls { get; set; }
    }
}
