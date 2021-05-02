using System.Linq;
using Xarial.XCad.UI.PropertyPage.Attributes;
using Xarial.XCad.UI.PropertyPage.Base;
using Xarial.XCad.UI.PropertyPage.Services;

namespace Xarial.XCad.Examples.PMPage.CSharp.Page.Groups
{
    public class EnabledDependencyHandler : IDependencyHandler
    {
        public void UpdateState(IXApplication app, IControl source, IControl[] dependencies)
        {
            source.Enabled = (bool)dependencies?.First().GetValue();
        }
    }

    public class StateDependencyHandler : IDependencyHandler
    {
        public void UpdateState(IXApplication app, IControl source, IControl[] dependencies)
        {
            source.Visible = ((ControlState_e)dependencies?.First().GetValue() == ControlState_e.Visible);
        }
    }

    public enum ControlState_e
    {
        Visible,
        Hidden
    }

    /// <summary>
    /// This group contains controls which change their state based on the other control value
    /// </summary>
    public class DependencyControlsGroup 
    {
        /// <summary>
        /// This CheckBox control drives the enable state of the <see cref="Text1"/> control
        /// </summary>
        [ControlTag("Enable")]
        public bool EnableNext { get; set; }
        
        /// <summary>
        /// The enable state of this control is driven by the value of <see cref="EnableNext"/> CheckBox
        /// </summary>
        [DependentOn(typeof(EnabledDependencyHandler), "Enable")]
        public string Text1 { get; set; }

        /// <summary>
        /// This ComboBox control drives the visibility state of the <see cref="Text2"/> control
        /// </summary>
        [ControlTag("State")]
        public ControlState_e StateNext { get; set; }

        /// <summary>
        /// The enable state of this control is driven by the value of <see cref="StateNext"/> ComboBox
        /// </summary>
        [DependentOn(typeof(StateDependencyHandler), "State")]
        public string Text2 { get; set; }
    }
}
