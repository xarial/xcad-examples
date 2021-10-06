using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xarial.XCad.UI.PropertyPage;

namespace Xarial.XCad.Examples.PMPage.CSharp.Controls
{
    public partial class WinFormsUserControl : UserControl, IXCustomControl
    {
        public event CustomControlValueChangedDelegate ValueChanged;

        public WinFormsUserControl()
        {
            InitializeComponent();
        }

        public object Value 
        {
            get => lblMsg.Text; 
            set => lblMsg.Text = value?.ToString(); 
        }

        private void OnClick(object sender, EventArgs e)
        {
            Value = Guid.NewGuid().ToString();
            ValueChanged?.Invoke(this, Value);
        }
    }
}
