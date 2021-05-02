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
    public class ControlsTab
    {
        /// <summary>
        /// This group contains advanced controls customizations (i.e. icons, tooltips, alignments, colors, etc.)
        /// </summary>
        public ControlsCustomizationGroup ControlsCustomization { get; set; }

        /// <summary>
        /// All nested structures will be rendered as groups in property manager page
        /// This group will have a toggle button as its class is decorated with <see cref="CheckableGroupBoxAttribute"/>
        /// </summary>
        public ToggleGroup ToggleGroup { get; set; }

        /// <summary>
        /// In most cases the controls will be created based on the structure of the class (static controls)
        /// In some cases it might be required to create controls dynamically. The followin ggroup will contain such controls
        /// </summary>
        public DynamicControlsGroup DynamicControlsGroup { get; set; }
    }
}
