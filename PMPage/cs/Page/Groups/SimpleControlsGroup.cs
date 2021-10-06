using System;
using System.Drawing;
using System.Windows;
using Xarial.XCad.Examples.PMPage.CSharp.Properties;
using Xarial.XCad.Geometry;
using Xarial.XCad.UI.PropertyPage.Attributes;
using Xarial.XCad.UI.PropertyPage.Enums;

namespace Xarial.XCad.Examples.PMPage.CSharp.Page.Groups
{
    /// <summary>
    /// This group contains definitions for simple controls
    /// </summary>
    public class SimpleControlsGroup 
    {
        /// <summary>
        /// <see cref="string"/> property will be rendered as TextBox controls
        /// </summary>
        public string TextBox { get; set; }

        /// <summary>
        /// <see cref="int"/> property will be rendered as NumberBox control with label
        /// </summary>
        [Label("Number Box Control with label")]
        public int NumberBox { get; set; }

        /// <summary>
        /// <see cref="double"/> property will be rendered as NumberBox control
        /// </summary>
        public double FloatingNumberBox { get; set; }

        /// <summary>
        /// <see cref="Enum"/> properties will be rendered as ComboBox conrol,
        /// unless explicitly decorated with <see cref="ListBoxAttribute"/> or <see cref="OptionBoxAttribute"/>,
        /// and in this case will be rendered as ListBox or OptionBox controls correspondingly
        /// </summary>
        public Options_e ComboBox { get; set; }

        /// <summary>
        /// <see cref="bool"/> property will be rendered as CheckBox control
        /// </summary>
        public bool CheckBox { get; set; }

        /// <summary>
        /// <see cref="Action"/> property will be rendered as Button control
        /// and the handler of this delegate will be called on button click
        /// </summary>
        public Action Button { get; set; }

        /// <summary>
        /// Properties assignable to <see cref="IXSelObject"/> will be rendered as SelectionBox control
        /// </summary>
        public IXFace SelectionBox { get; set; }

        /// <summary>
        /// Text block property
        /// </summary>
        [TextBlock]
        [TextBlockOptions(TextAlignment_e.Center)]
        public string TextBlock => "Sample Text Block";

        /// <summary>
        /// This control will be rendered as picture with the default size
        /// </summary>
        public Image Picture { get; set; } = Resources.xarial;

        public SimpleControlsGroup() 
        {
            Button = new Action(() => 
            {
                MessageBox.Show("Button is clicked");
            });
        }
    }
}
