using System;
using System.Drawing;
using System.Windows;
using Xarial.XCad.Examples.PMPage.CSharp.Properties;
using Xarial.XCad.Geometry;
using Xarial.XCad.UI.PropertyPage.Attributes;
using Xarial.XCad.UI.PropertyPage.Enums;

namespace Xarial.XCad.Examples.PMPage.CSharp.Page.Groups
{
    public class SimpleControlsGroup 
    {
        public string TextBox { get; set; }
        public int NumberBox { get; set; }
        public double FloatingNumberBox { get; set; }
        public Options_e ComboBox { get; set; }
        public bool CheckBox { get; set; }
        public Action Button { get; set; }
        public IXFace SelectionBox { get; set; }

        [BitmapOptions(48, 48)]
        public Image Picture { get; set; } = Resources.xarial;

        [BitmapButton(BitmapButtonLabelType_e.ReverseDirection)]
        public Action PictureButton { get; set; }

        [BitmapButton(typeof(Resources), nameof(Resources.xarial))]
        public bool TogglePictureButton { get; set; }

        public SimpleControlsGroup() 
        {
            Button = new Action(() => 
            {
                MessageBox.Show("Button is clicked");
            });

            PictureButton = new Action(() =>
            {
                MessageBox.Show("Picture Button is clicked");
            });
        }
    }
}
