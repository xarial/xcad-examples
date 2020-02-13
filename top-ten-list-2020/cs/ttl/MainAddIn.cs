using SolidWorks.Interop.sldworks;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using ttl.Properties;
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

namespace ttl
{
    [Title("Top 10 List 2020")]
    public enum Commands_e
    {
        [Icon(typeof(Resources), nameof(Resources.zoom_to_geometry))]
        [Title("Zoom Geometry To Fit")]
        [Description("Zooms geometry to fit excluding sketches and reference geometry")]
        ZoomGeometryToFit,

        [Icon(typeof(Resources), nameof(Resources.shaft_chamfer))]
        [Title("Insert Shaft Chamfer")]
        [Description("Creates a chamfer driven by base diameter and angle")]
        InsertShaftChamfer,

        [Icon(typeof(Resources), nameof(Resources.hide_show_bodies))]
        [Title("Hide/Show Bodies")]
        [Description("Displays Hide/Show Bodies Property Page")]
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
            var model = Application.Documents.Active as SwDocument3D;

            var bbox = model.CalculateBoundingBox();

            model.ActiveView.ZoomToBox(bbox);
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