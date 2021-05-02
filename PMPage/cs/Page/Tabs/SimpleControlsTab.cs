using System.ComponentModel;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Examples.PMPage.CSharp.Page.Groups;
using Xarial.XCad.UI.PropertyPage.Attributes;

namespace Xarial.XCad.Examples.PMPage.CSharp.Page.Tabs
{
    /// <summary>
    /// This tab contains groups for simple controls rendering
    /// </summary>
    [Tab]
    public class SimpleControlsTab
    {
        /// <summary>
        /// Collection of controls created automatically from the input property
        /// <see cref="TitleAttribute"/> allow to define the user friendly name for the group
        /// </summary>
        [Title("Simple Controls")]
        [Description("Group containing simple controls")]
        public SimpleControlsGroup SimpleControls { get; set; }

        /// <summary>
        /// This group contains advanced controls customizations (i.e. icons, tooltips, alignments, colors, etc.)
        /// </summary>
        public ControlsCustomizationGroup ControlsCustomization { get; set; }

        /// <summary>
        /// This group demonstrates how to enable dependencies between controls
        /// </summary>
        public DependencyControlsGroup DependencyControls { get; set; }
    }
}
