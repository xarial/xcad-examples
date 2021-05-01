using System.ComponentModel;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Examples.PMPage.CSharp.Page.Groups;
using Xarial.XCad.UI.PropertyPage.Attributes;

namespace Xarial.XCad.Examples.PMPage.CSharp.Page.Tabs
{
    [Tab]
    public class SimpleControlsTab
    {
        [Title("Simple Controls")]
        [Description("Group containing simple controls")]
        public SimpleControlsGroup SimpleControls { get; set; }

        public DependencyControlsGroup DependencyControls { get; set; }

        public ControlsCustomizationGroup ControlsCustomization { get; set; }
    }
}
