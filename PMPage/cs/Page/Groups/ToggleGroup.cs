using Xarial.XCad.UI.PropertyPage.Attributes;

namespace Xarial.XCad.Examples.PMPage.CSharp.Page.Groups
{
    [CheckableGroupBox("ToggleGroupIsChecked")]
    public class ToggleGroup
    {
        [Metadata("ToggleGroupIsChecked")]
        public bool IsChecked { get; set; }

        public string TextBox { get; set; }
    }
}
