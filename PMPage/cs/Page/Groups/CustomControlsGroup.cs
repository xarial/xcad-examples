using Xarial.XCad.Examples.PMPage.CSharp.Controls;
using Xarial.XCad.UI.PropertyPage.Attributes;

namespace Xarial.XCad.Examples.PMPage.CSharp.Page.Groups
{
    public class WpfControlVM
    {
        public string Message { get; set; }
    }

    public class CustomControlsGroup 
    {
        [CustomControl(typeof(WinFormsUserControl))]
        public string WinFormControl { get; set; } = "Click on label to change the value";

        [CustomControl(typeof(WpfUserControl))]
        public WpfControlVM WpfControl { get; set; } = new WpfControlVM() { Message = "Custom WPF Control in Property Manager Page" };
    }
}
