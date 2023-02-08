using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Examples.PMPage.CSharp.Properties;
using Xarial.XCad.UI.PropertyPage.Attributes;
using Xarial.XCad.UI.PropertyPage.Enums;

namespace Xarial.XCad.Examples.PMPage.CSharp.Page.Groups
{
    public enum Options_e
    {
        Option1,
        Option2,

        /// <summary>
        /// Enumeration fields can be decorated with <see cref="TitleAttribute"/> and <see cref="DescriptionAttribute"/>
        /// to provide user friendly title and tooltips
        /// </summary>
        [Title("Option 3")]
        [Description("Option Number Three")]
        Option3
    }

    /// <summary>
    /// This group contains simple controls with additional customization
    /// </summary>
    public class ControlsCustomizationGroup
    {
        /// <summary>
        /// <see cref="ControlOptionsAttribute"/> attribute allows to assign common properties to all controls
        /// This text box will have a single indent, green background and yellow color for the text
        /// </summary>
        [ControlOptions(align: ControlLeftAlign_e.Indent, backgroundColor: KnownColor.Green, textColor: KnownColor.Yellow)]
        public string TextBoxColorIndent { get; set; }

        /// <summary>
        /// <see cref="StandardControlIconAttribute"/> attribute allows to assign the standard icon to any control
        /// This control will have standard diameter icon in the NumberBox
        /// </summary>
        [StandardControlIcon(BitmapLabelType_e.Diameter)]
        [Label("Number Box:")]
        public int NumberBoxStandardIcon { get; set; }

        /// <summary>
        /// Most of the controls will have specific customization options
        /// <see cref="TextBoxOptionsAttribute"/> allows to assign additional options for TextBox controls
        /// This TextBox will support multi lines without a border around control
        /// </summary>
        [TextBoxOptions(TextBoxStyle_e.Multiline | TextBoxStyle_e.NoBorder)]
        [ControlOptions(height: 30)]
        public string TextBoxStyle { get; set; } = "Line1" + Environment.NewLine + "Line2";

        /// <summary>
        /// <see cref="NumberBoxOptionsAttribute"/> provides additional customization options for NumberBox controls
        /// This control will be contain length in system units and will automatically convert value based on the current unit settings
        /// Control will also have minumum and maximum values and thumbwheel
        /// </summary>
        [NumberBoxOptions(NumberBoxUnitType_e.Length, 0.1, 100, 0.01, true, 0.1, 0.001, NumberBoxStyle_e.Thumbwheel)]
        public double NumberBoxStyle { get; set; } = 0.2;

        /// <summary>
        /// <see cref="ComboBoxOptionsAttribute"/> provides additional options for ComboBox
        /// This control will allow to enter the text
        /// </summary>
        [ComboBoxOptions(ComboBoxStyle_e.EditableText)]
        public Options_e ComboBoxStyle { get; set; }

        /// <summary>
        /// <see cref="ListBoxOptionsAttribute"/> provides additional options for the ListBox control
        /// Thsi list box will sort the value in alphabetical order
        /// </summary>
        [ListBoxOptions(ListBoxStyle_e.Sorted)]
        [ListBox("C", "D", "E", "A", "B")]
        public string ListBoxStyle { get; set; }

        /// <summary>
        /// <see cref="Image"/> property will be rendered as PictureBox control
        /// </summary>
        [BitmapOptions(64, 64)]
        public Image Picture { get; set; } = Resources.xarial;

        /// <summary>
        /// <see cref="Action"/> property which is decorated with <see cref="BitmapButtonAttribute"/>
        /// will be rendered as BitmapButton where the handler of the delegate will be called on button click.
        /// The button below will have a standard icon
        /// </summary>
        [BitmapButton(BitmapButtonLabelType_e.ReverseDirection)]
        public Action PictureButton { get; set; }

        /// <summary>
        /// Thsi BitmapButton will have a custom icon
        /// </summary>
        [BitmapButton(typeof(Resources), nameof(Resources.xarial))]
        public bool TogglePictureButton { get; set; }

        public ControlsCustomizationGroup() 
        {
            PictureButton = new Action(() =>
            {
                MessageBox.Show("Picture Button is clicked");
            });
        }
    }
}
