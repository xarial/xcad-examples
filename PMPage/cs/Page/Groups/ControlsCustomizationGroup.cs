using System.ComponentModel;
using System.Drawing;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.UI.PropertyPage.Attributes;
using Xarial.XCad.UI.PropertyPage.Enums;

namespace Xarial.XCad.Examples.PMPage.CSharp.Page.Groups
{
    public enum Options_e
    {
        Option1,
        Option2,
        [Title("Option 3")]
        [Description("Option Number Three")]
        Option3
    }

    public class ControlsCustomizationGroup
    {
        [ControlOptions(align: ControlLeftAlign_e.Indent, backgroundColor: KnownColor.Green, textColor: KnownColor.Yellow)]
        public string TextBoxColorIndent { get; set; }

        [StandardControlIcon(BitmapLabelType_e.Diameter)]
        public int NumberBoxStandardIcon { get; set; }

        [TextBoxOptions(TextBoxStyle_e.Multiline | TextBoxStyle_e.NoBorder)]
        [ControlOptions(height: 30)]
        public string TextBoxStyle { get; set; } = "Line1" + System.Environment.NewLine + "Line2";

        [NumberBoxOptions(NumberBoxUnitType_e.Length, 0.1, 100, 0.01, true, 0.1, 0.001, NumberBoxStyle_e.Thumbwheel)]
        public double NumberBoxStyle { get; set; } = 0.2;

        [ComboBoxOptions(ComboBoxStyle_e.EditableText)]
        public Options_e ComboBoxStyle { get; set; }

        [ListBoxOptions(ListBoxStyle_e.Sorted)]
        [ListBox("C", "D", "E", "A", "B")]
        public string ListBoxStyle { get; set; }
    }
}
