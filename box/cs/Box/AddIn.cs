using CubeExample.Properties;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.UI.Commands;

namespace CubeExample
{
    [Title("Box Example")]
    public enum Commands_e 
    {
        [Icon(typeof(Resources), nameof(Resources.box_icon))]
        [Title("Create Box")]
        [Description("Creates simple box feature")]
        CreateBox
    }

    [ComVisible(true)]
    [Title("Box Example")]
    [Description("Box example add-in based on xCAD")]
    public class AddIn : SwAddInEx
    {
        public override void OnConnect()
        {
            this.CommandManager.AddCommandGroup<Commands_e>().CommandClick += OnCommandClick;
        }

        private void OnCommandClick(Commands_e spec)
        {
            switch (spec) 
            {
                case Commands_e.CreateBox:
                    this.Application.Documents.Active.Features.CreateCustomFeature<BoxMacroFeatureDef, BoxData, BoxData>();
                    break;
            }
        }
    }
}
