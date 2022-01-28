using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.UI.Commands;
using Xarial.XCad.UI.Commands.Attributes;
using Xarial.XCad.UI.Commands.Enums;
using Xarial.XCad.Examples.CoordinateSystemMacroFeature.Properties;

namespace Xarial.XCad.Examples.CoordinateSystemMacroFeature
{
    [Title("Coordinate System Ex")]
    public enum Commands_e
    {
        [Title("Insert Coordinate System")]
        [CommandItemInfo(WorkspaceTypes_e.Part)]
        [Icon(typeof(Resources), nameof(Resources.coord_system))]
        InsertCoordinateSystem
    }

    [ComVisible(true)]
    [Guid("0F6CC01C-C1EB-4634-A659-4871191AEDEA")]
    public class CoordinateSystemAddIn : SwAddInEx
    {
        public override void OnConnect()
        {
            this.CommandManager.AddCommandGroup<Commands_e>().CommandClick += OnButtonClick;
        }

        private void OnButtonClick(Commands_e spec)
        {
            switch (spec)
            {
                case Commands_e.InsertCoordinateSystem:
                    {
                        Application.Documents.Active.Features.CreateCustomFeature<CoordinateSystemMacroFeatureDefinition, CoordinateSystemData, CoordinateSystemData>();
                        break;
                    }
            }
        }
    }
}



