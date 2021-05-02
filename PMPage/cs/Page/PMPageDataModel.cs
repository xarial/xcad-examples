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
    /// <summary>
    /// This is a base data model describing the property manager page. Data is bound in two directions
    /// (proeprty manager page will use values from the instance of this type to set defaults upon opening of the page
    /// and will write the values back after close)
    /// </summary>
    [ComVisible(true)]
    [Description("Property manager controls with xCAD.NET. Change control values and click green tick. All the values are bound to data model")]
    [Icon(typeof(Resources), nameof(Resources.xarial))]
    [Title("xCAD.NET PMPage")]
    public class PMPageDataModel : SwPropertyManagerPageHandler
    {
        /// <summary>
        /// <see cref="SimpleControlsTab"/> class has is decorated with <see cref="TabAttribute"/>
        /// which will render this nested class as tab in Property Manager Page
        /// <see cref="TitleAttribute"/> allows to assign the user-friendly name for the tab
        /// </summary>
        [Title("Tab1")]
        public SimpleControlsTab SimpleControls { get; set; }

        /// <summary>
        /// <see cref="TabAttribute"/> can also be assigned to the property
        /// while <see cref="TitleAttribute"/>, <see cref="DesignerAttribute"/> and <see cref="IconAttribute"/>
        /// can be assigned in the class
        /// </summary>
        [Tab]
        public SecondTabCustomIcon Tab2 { get; set; }

        /// <summary>
        /// By default all public properties will be considered as the sources for the controls of property manager page
        /// <see cref="ExcludeControlAttribute"/> tells the frawework to skip this property and do not create control for it
        /// </summary>
        [ExcludeControl]
        public string SystemText { get; set; }

        /// <summary>
        /// All nested structures will be rendered as groups in property manager page
        /// This group will have a toggle button as its class is decorated with <see cref="CheckableGroupBoxAttribute"/>
        /// </summary>
        public ToggleGroup ToggleGroup { get; set; }

        /// <summary>
        /// In most cases the controls will be created based on the structure of the class (static controls)
        /// In some cases it might be required to create controls dynamically. The followin ggroup will contain such controls
        /// </summary>
        public DynamicControlsGroup DynamicControlsGroup { get; set; }

        /// <summary>
        /// This property will be rendered as check box directly in the property manager page
        /// If this check box is checked and green tick is clicked - property manager page wil lnot be closed
        /// and instead the message in the <see cref="CloseErrorMessage"/> text box will be displayed
        /// </summary>
        public bool DisableClosing { get; set; }

        /// <summary>
        /// Error message to display when closing property manager page when <see cref="DisableClosing"/> is checked
        /// </summary>
        public string CloseErrorMessage { get; set; }
    }
}
