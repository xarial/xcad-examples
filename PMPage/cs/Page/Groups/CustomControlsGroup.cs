using Xarial.XCad.Examples.PMPage.CSharp.Controls;
using Xarial.XCad.UI.PropertyPage.Attributes;

namespace Xarial.XCad.Examples.PMPage.CSharp.Page.Groups
{
    public class WpfControlVM
    {
        public string Message { get; set; }
    }

    /// <summary>
    /// This group renders custom controls directly in the property manager page group
    /// </summary>
    public class CustomControlsGroup 
    {
        /// <summary>
        /// This property will be rendered as Windows Forms Control <see cref="WinFormsUserControl"/>
        /// Value of the proeprty will be bound to the control via <see cref="UI.PropertyPage.IXCustomControl.Value"/> property
        /// Click on the label to generate new random value and bound to the property value
        /// </summary>
        [CustomControl(typeof(WinFormsUserControl))]
        public string WinFormControl { get; set; } = "Click on label to change the value";

        /// <summary>
        /// This property will be rendered as WPF control <see cref="WpfUserControl"/>
        /// The property value <see cref="WpfControlVM"/> will be assigned as <see cref="System.Windows.FrameworkElement.DataContext"/> property and can be used in the binding
        /// </summary>
        [CustomControl(typeof(WpfUserControl))]
        public WpfControlVM WpfControl { get; set; } = new WpfControlVM() { Message = "Custom WPF Control in Property Manager Page" };
    }
}
