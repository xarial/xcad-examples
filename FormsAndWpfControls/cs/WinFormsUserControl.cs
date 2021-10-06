using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xarial.XCad.Base.Attributes;
using FormsAndWpfControls.Properties;
using Xarial.XCad.UI.PropertyPage;

namespace FormsAndWpfControls
{
    [Icon(typeof(Resources), nameof(Resources.winforms_icon))]
    public partial class WinFormsUserControl : UserControl, IXCustomControl
    {
        public WinFormsUserControl()
        {
            InitializeComponent();
        }

        public object Value { get; set; }

        public event CustomControlValueChangedDelegate ValueChanged;
    }
}
