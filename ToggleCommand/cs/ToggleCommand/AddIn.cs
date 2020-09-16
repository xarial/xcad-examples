using System.ComponentModel;
using System.Runtime.InteropServices;
using ToggleCommand.Properties;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.UI.Commands;
using Xarial.XCad.UI.Commands.Structures;

namespace ToggleCommand
{
    [Title("Toggle Button")]
    public enum Commands_e 
    {
        [Icon(typeof(Resources), nameof(Resources.power_button))]
        [Title("Power")]
        [Description("Example of toggle button")]
        Power
    }

    [ComVisible(true)]
    public class AddIn : SwAddInEx
    {
        private bool m_IsPowerOn;

        public override void OnConnect()
        {
            var cmdGrp = CommandManager.AddCommandGroup<Commands_e>();

            m_IsPowerOn = false;

            cmdGrp.CommandClick += OnCommandClick;
            cmdGrp.CommandStateResolve += OnCommandStateResolve;
        }

        private void OnCommandClick(Commands_e spec)
        {
            switch (spec) 
            {
                case Commands_e.Power:
                    m_IsPowerOn = !m_IsPowerOn;
                    break;
            }
        }

        private void OnCommandStateResolve(Commands_e spec, CommandState state)
        {
            switch (spec)
            {
                case Commands_e.Power:
                    state.Checked = m_IsPowerOn;
                    break;
            }
        }
    }
}
