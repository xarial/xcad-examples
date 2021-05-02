using Xarial.XCad.UI.PropertyPage.Attributes;

namespace Xarial.XCad.Examples.PMPage.CSharp.Page.Groups
{
    /// <summary>
    /// <see cref="CheckableGroupBoxAttribute"/> indicates that this group should have a check box
    /// Parameter is a unique name of the boolean property which wil be bound to check box value
    /// This property must be decorated with <see cref="MetadataAttribute"/>
    /// </summary>
    [CheckableGroupBox("ToggleGroupIsChecked")]
    public class ToggleGroup
    {
        /// <summary>
        /// This is a metadata property (control will not be created for this proeprty)
        /// Instead it will be used as a binding for the toggle state of the group.
        /// Note, that the name of the metadata matches the name of the parameter in the <see cref="CheckableGroupBoxAttribute"/>,
        /// This is how this property is bound
        /// </summary>
        [Metadata("ToggleGroupIsChecked")]
        public bool IsChecked { get; set; }

        /// <summary>
        /// This property will be rendered as a simple TextBox
        /// </summary>
        public string TextBox { get; set; }
    }
}
