using System.ComponentModel;
using System.Runtime.InteropServices;
using ttl.Properties;
using Xarial.XCad.Base;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Features;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.Documents;
using Xarial.XCad.UI.Commands;
using Xarial.XCad.UI.Commands.Attributes;
using Xarial.XCad.UI.Commands.Enums;

namespace ttl
{
    [Title("Top 10 List 2020")]
    public enum Commands_e
    {
        [Icon(typeof(Resources), nameof(Resources.zoom_to_geometry))]
        [Title("Zoom Geometry To Fit")]
        [Description("Zooms geometry to fit excluding sketches and reference geometry")]
        [CommandItemInfo(true, true, WorkspaceTypes_e.Assembly | WorkspaceTypes_e.Part, true)]
        ZoomGeometryToFit,

        [Icon(typeof(Resources), nameof(Resources.shaft_chamfer))]
        [Title("Insert Shaft Chamfer")]
        [Description("Creates a chamfer driven by base diameter and angle")]
        [CommandItemInfo(true, true, WorkspaceTypes_e.Part, true)]
        InsertShaftChamfer,

        [Icon(typeof(Resources), nameof(Resources.hide_show_bodies))]
        [Title("Hide/Show Bodies")]
        [Description("Displays Hide/Show Bodies Property Page")]
        [CommandItemInfo(true, true, WorkspaceTypes_e.Part, true)]
        HideShowBodies
    }

    [ComVisible(true)]
    [Title("TTL 2020")]
    [Description("Top 10 List 2020")]
    public class MainAddIn : SwAddInEx
    {
        public override void OnConnect()
        {
            this.CommandManager.AddCommandGroup<Commands_e>().CommandClick += OnButtonClick;
        }

        private void OnButtonClick(Commands_e cmd)
        {
            switch (cmd)
            {
                case Commands_e.ZoomGeometryToFit:
                    ZoomGeometryToFit();
                    break;

                case Commands_e.InsertShaftChamfer:
                    InsertShaftChamfer();
                    break;

                case Commands_e.HideShowBodies:
                    HideShowBodies();
                    break;
            }
        }

        private void ZoomGeometryToFit()
        {
            var model = Application.Documents.Active as ISwDocument3D;

            var bbox = model.Evaluation.PreCreateBoundingBox();
            bbox.Precise = true;
            bbox.Commit();

            model.ModelViews.Active.ZoomToBox(bbox.Box);
        }

        private void HideShowBodies()
        {
            const int swCommands_View_Hideshow = 1390;
            Application.Sw.RunCommand(swCommands_View_Hideshow, "");
        }

        private void InsertShaftChamfer()
        {
            Application.Documents.Active.Features.InsertCustomFeature<ShaftChamferMacroFeatureDefinition, ShaftChamferData, ShaftChamferData>();
        }
    }
}