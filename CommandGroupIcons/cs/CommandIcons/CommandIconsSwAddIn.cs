using System;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.UI.Commands;
using System.Runtime.InteropServices;
using Xarial.Examples.XCad.CommandIcons.Properties;

namespace Xarial.Examples.XCad.CommandIcons
{
    [ComVisible(true)]
    [Guid("D370C945-98D0-46DD-A04B-FCB7D3C15D41")]
    public class CommandIconsSwAddIn : SwAddInEx
    {
        [Title("Power Tools")]
        public enum Commands_e
        {
            [Icon(typeof(Resources), nameof(Resources.electric_drill))]
            Drill,
            [Icon(typeof(Resources), nameof(Resources.electric_screwdriver))]
            Screwdriver,
            [Icon(typeof(Resources), nameof(Resources.power_saw))]
            Saw
        }

        public override void OnConnect()
        {
            CommandManager.AddCommandGroup<Commands_e>().CommandClick += OnButtonClick;
        }

        private void OnButtonClick(Commands_e cmd)
        {
            switch (cmd)
            {
                case Commands_e.Drill:
                    {
                        Application.ShowMessageBox("Drill buttons is clicked");
                        break;
                    }

                case Commands_e.Screwdriver:
                    {
                        Application.ShowMessageBox("Screwdriver buttons is clicked");
                        break;
                    }

                case Commands_e.Saw:
                    {
                        Application.ShowMessageBox("Saw buttons is clicked");
                        break;
                    }
            }
        }
    }
}
