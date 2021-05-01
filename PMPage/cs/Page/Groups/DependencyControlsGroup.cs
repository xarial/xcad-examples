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

    public class DependencyControlsGroup 
    {
        [ControlTag("Enable")]
        public bool EnableNext { get; set; }
        
        [DependentOn(typeof(EnabledDependencyHandler), "Enable")]
        public string Text1 { get; set; }

        [ControlTag("State")]
        public ControlState_e StateNext { get; set; }

        [DependentOn(typeof(StateDependencyHandler), "State")]
        public string Text2 { get; set; }
    }
}
