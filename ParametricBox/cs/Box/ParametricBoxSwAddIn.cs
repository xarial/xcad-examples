using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Examples.Sw.ParametricBox.Properties;
using Xarial.XCad.Features;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.UI.Commands;
using Xarial.XCad.UI.Commands.Attributes;
using Xarial.XCad.UI.Commands.Enums;

namespace Xarial.XCad.Examples.Sw.ParametricBox
{
    [ComVisible(true)]
    [Guid("C2AE2FD5-0AA2-4476-BB11-A47AD9CCB494")]
    [Title("Parametric Box")]
    [Description("Parametric box macro feature sample")]
    public class ParametricBoxSwAddIn : SwAddInEx
    {
        [Title("ParametricBox")]
        [Description("Commands of ParametricBox")]
        private enum Commands_e
        {
            [Icon(typeof(Resources), nameof(Resources.parametric_box_icon))]
            [Title("Create Parametric Box")]
            [Description("Creates parametric macro feature")]
            [CommandItemInfo(true, true, WorkspaceTypes_e.Part | WorkspaceTypes_e.InContextPart, true)]
            CreateParametricBox
        }

        public override void OnConnect()
        {
            CommandManager.AddCommandGroup<Commands_e>().CommandClick += OnCommandClick;
        }

        private void OnCommandClick(Commands_e spec)
        {
            switch (spec)
            {
                case Commands_e.CreateParametricBox:
                    Application.Documents.Active.Features.CreateCustomFeature<BoxMacroFeatureDefinition, BoxMacroFeatureData, BoxPropertyPage>();
                    break;
            }
        }
    }
}
