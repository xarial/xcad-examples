using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.Examples.PMPage.CSharp.Page.Groups;
using Xarial.XCad.UI.PropertyPage.Attributes;

namespace Xarial.XCad.Examples.PMPage.CSharp.Page.Tabs
{
    /// <summary>
    /// This tab demonstrates how to configure behaviors of various controls
    /// </summary>
    [Tab]
    public class BehaviorTab
    {
        /// <summary>
        /// This group demonstrates how to enable dependencies between controls
        /// </summary>
        public DependencyControlsGroup DependencyControls { get; set; }

        /// <summary>
        /// This group shows how to update the values in the controls dynamically
        /// </summary>
        public ReactiveControlsGroup Reactive { get; set; }
    }
}
