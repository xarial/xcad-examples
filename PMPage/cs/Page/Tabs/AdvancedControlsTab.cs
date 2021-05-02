using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Examples.PMPage.CSharp.Page.Groups;
using Xarial.XCad.Examples.PMPage.CSharp.Properties;

namespace Xarial.XCad.Examples.PMPage.CSharp.Page.Tabs
{
    /// <summary>
    /// This tab contains custom icon and display name
    /// </summary>
    [DisplayName("Advanced Controls")]
    [Description("Second tab with custom title and icon")]
    [Icon(typeof(Resources), nameof(Resources.xarial))]
    public class AdvancedControlsTab 
    {
        /// <summary>
        /// This group contains controls for advanced SelectionBox
        /// </summary>
        public AdvancedSelectionBoxGroup AdvancedSelectionBox { get; set; }

        /// <summary>
        /// This group demonstrates how data source can be bound to the ListBox and ComboBox controls
        /// </summary>
        public ItemSourceControlsGroup ItemsSourceControls { get; set; }

        /// <summary>
        /// This group contains custom WPF and Windows Forms controls inside property manager page
        /// </summary>
        public CustomControlsGroup CustomControls { get; set; }
    }
}
