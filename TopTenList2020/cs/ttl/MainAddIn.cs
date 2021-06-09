using SolidWorks.Interop.sldworks;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using ttl.Properties;
using Xarial.XCad.Base;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Extensions;
using Xarial.XCad.Features.CustomFeature;
using Xarial.XCad.Features.CustomFeature.Delegates;
using Xarial.XCad.Geometry;
using Xarial.XCad.Geometry.Structures;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.Documents;
using Xarial.XCad.SolidWorks.Geometry;
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
        [CommandItemInfo(WorkspaceTypes_e.Assembly | WorkspaceTypes_e.Part)]
        ZoomGeometryToFit,

        [Icon(typeof(Resources), nameof(Resources.shaft_chamfer))]
        [Title("Insert Shaft Chamfer")]
        [Description("Creates a chamfer driven by base diameter and angle")]
        [CommandItemInfo(WorkspaceTypes_e.Part)]
        InsertShaftChamfer,

        [Icon(typeof(Resources), nameof(Resources.hide_show_bodies))]
        [Title("Hide/Show Bodies")]
        [Description("Displays Hide/Show Bodies Property Page")]
        [CommandItemInfo(WorkspaceTypes_e.Part)]
        HideShowBodies
    }

    [ComVisible(true)]
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

            var bbox = model.PreCreateBoundingBox();
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
            Application.Documents.Active.Features.CreateCustomFeature<ShaftChamferMacroFeatureDefinition, ShaftChamferData, ShaftChamferData>();
        }
    }
}