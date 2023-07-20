using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Examples.Properties;
using Xarial.XCad.Features;
using Xarial.XCad.Features.CustomFeature;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.UI.Commands;
using Xarial.XCad.UI.Commands.Attributes;

namespace Xarial.XCad.Examples
{
    [Title("Custom Holes")]
    [Description("Sample add-in demonstrating using of cut macro features with custom preview handling")]
    [ComVisible(true), Guid("ED2FE6E1-79EE-43B8-90BD-5FFFB7E5F2A4")]
    public class CustomHolesSwAddIn : SwAddInEx
    {
        [Icon(typeof(Resources), nameof(Resources.custom_hole))]
        [Title("Custom Holes")]
        public enum Commands_e 
        {
            [CommandItemInfo(true, true, UI.Commands.Enums.WorkspaceTypes_e.Part, true)]
            [Icon(typeof(Resources), nameof(Resources.custom_hole))]
            [Title("Insert Custom Hole")]
            InserCustomHole
        }

        public override void OnConnect()
        {
            this.CommandManager.AddCommandGroup<Commands_e>().CommandClick += OnCommandClick;
        }

        private void OnCommandClick(Commands_e spec)
        {
            switch (spec) 
            {
                case Commands_e.InserCustomHole:
                    Application.Documents.Active.Features.CreateCustomFeature<CustomHoleMacroFeatureDefinition, CustomHoleData, CustomHolePage>();
                    break;
            }
        }
    }
}
