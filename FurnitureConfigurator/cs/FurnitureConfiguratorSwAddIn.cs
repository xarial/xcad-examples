using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.UI.Commands;
using Xarial.XCad.UI.Commands.Attributes;
using Xarial.XCad.UI.Commands.Enums;

namespace XCad.Examples.FurnitureConfigurator
{
    [ComVisible(true)]
    public class FurnitureConfiguratorSwAddIn : SwAddInEx
    {
        public enum Commands_e 
        {
            [CommandItemInfo(WorkspaceTypes_e.Assembly)]
            InsertCabinetConfigurator
        }

        public override void OnConnect()
        {
            this.CommandManager.AddCommandGroup<Commands_e>().CommandClick += OnCommandClick;
        }

        private void OnCommandClick(Commands_e spec)
        {
            switch (spec) 
            {
                case Commands_e.InsertCabinetConfigurator:
                    Application.Documents.Active.Features.CreateCustomFeature<CabinetConfiguratorMacroFeatureDefinition, CabinetSizeData, CabinetConfiguratorPage>();
                    break;
            }
        }
    }
}
