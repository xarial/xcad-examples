using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Examples.PMPage.CSharp.Page.Groups;
using Xarial.XCad.Examples.PMPage.CSharp.Page.Tabs;
using Xarial.XCad.Examples.PMPage.CSharp.Properties;
using Xarial.XCad.SolidWorks.UI.PropertyPage;
using Xarial.XCad.UI.PropertyPage.Attributes;
using Xarial.XCad.UI.PropertyPage.Base;

namespace Xarial.XCad.Examples.PMPage.CSharp.Page
{
    [ComVisible(true)]
    [Description("Property manager controls with xCAD.NET. Change control values and click green tick. All the values are bound to data model")]
    [Icon(typeof(Resources), nameof(Resources.xarial))]
    [Title("xCAD.NET PMPage")]
    public class PMPageDataModel : SwPropertyManagerPageHandler
    {
        [Title("Tab1")]
        public SimpleControlsTab SimpleControls { get; set; }

        [Tab]
        public SecondTabCustomIcon Tab2 { get; set; }

        [ExcludeControl]
        public string SystemText { get; set; }

        public ToggleGroup ToggleGroup { get; set; }

        public DynamicControlsGroup DynamicControlsGroup { get; set; }

        public bool DisableClosing { get; set; }
        public string CloseErrorMessage { get; set; }
    }
}
