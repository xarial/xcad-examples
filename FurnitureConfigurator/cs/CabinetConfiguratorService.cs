using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.Documents;
using Xarial.XCad.SolidWorks.Documents;

namespace XCad.Examples.FurnitureConfigurator
{
    public enum HandleType_e 
    {
        C,
        D,
        W
    }

    public class CabinetConfiguratorService
    {
        private const double PANEL_THICKNESS = 0.018;
        private const double DOOR_GAP = 0.002;
        private const double DRAWER_GAP = 0.0015;
        private const double DRAWER_BODY_FRONT_OFFSET = 0.037;
        private const double FRAME_HEIGHT = 0.2;
        private const double DRAWER_DEPTH_OFFSET = 0.04;
        private const double MIN_DRAWER_HEIGHT = 0.2;

        public void Configure(IXAssembly assm, double width, double height, double depth, int drawersCount, double drawerWidth, HandleType_e handleType)
        {
            SetParameters(assm, width, height, depth, drawersCount, drawerWidth);

            SetHandle(assm, handleType);

            ((ISwAssembly)assm).Model.ForceRebuild3(false);
        }

        private void SetParameters(IXAssembly assm, double width, double height, double depth, int drawersCount, double drawerWidth)
        {
            var doorWidth = (width - drawerWidth - DOOR_GAP * 3) / 3;
            var drawerHeight = (height - FRAME_HEIGHT - DRAWER_GAP * (drawersCount - 1)) / drawersCount;

            if (drawerHeight < MIN_DRAWER_HEIGHT) 
            {
                throw new Exception($"Minimum drawer height is {MIN_DRAWER_HEIGHT * 1000} mm");
            }

            //panels width
            assm.Configurations.Active.Components["Panel Top-1"].Dimensions["D1@Sketch1"].SetValue(width);
            assm.Configurations.Active.Components["Panel Rear-1"].Dimensions["D1@Sketch1"].SetValue(width - PANEL_THICKNESS * 2);
            assm.Configurations.Active.Components["Frame-1"].Dimensions["D2@3DSketch2"].SetValue(width);
            assm.Configurations.Active.Components["Panel Base-1"].Dimensions["D1@Sketch1"].SetValue(width - PANEL_THICKNESS * 2);
            assm.Dimensions["D1@Dist Panel LH to RH Outside"].SetValue(width);

            //door width
            assm.Configurations.Active.Components["Door-5"].Dimensions["D1@Sketch1"].SetValue(doorWidth);

            //internal panels distance
            assm.Dimensions["D1@PLANE Panel Internal 1"].SetValue(doorWidth + DOOR_GAP / 2 - PANEL_THICKNESS / 2);
            assm.Dimensions["D1@PLANE Panel Internal 2"].SetValue(doorWidth + DOOR_GAP + drawerWidth + DOOR_GAP / 2 - PANEL_THICKNESS / 2);
            assm.Dimensions["D1@PLANE Panel Internal 3"].SetValue(doorWidth + DOOR_GAP + drawerWidth + DOOR_GAP + doorWidth + DOOR_GAP / 2 - PANEL_THICKNESS / 2);

            //drawer width
            assm.Configurations.Active.Components["Drawer-5"].Children["Drawer Front-1"].Dimensions["D1@Sketch1"].SetValue(drawerWidth);
            assm.Configurations.Active.Components["Drawer-5"].Children["Drawer Body-1"].Dimensions["D2@Sketch1"].SetValue(drawerWidth - DRAWER_BODY_FRONT_OFFSET);

            //panels height
            assm.Dimensions["D1@PLANE Top of Cabinet"].SetValue(height);
            assm.Configurations.Active.Components["Panel End LH-1"].Dimensions["D2@Sketch1"].SetValue(height - FRAME_HEIGHT - PANEL_THICKNESS);
            assm.Configurations.Active.Components["Panel End RH-1"].Dimensions["D2@Sketch1"].SetValue(height - FRAME_HEIGHT - PANEL_THICKNESS);
            assm.Configurations.Active.Components["Panel Internal-4"].Dimensions["D2@Sketch1"].SetValue(height - FRAME_HEIGHT - PANEL_THICKNESS * 2);
            assm.Configurations.Active.Components["Panel Rear-1"].Dimensions["D2@Sketch1"].SetValue(height - FRAME_HEIGHT - PANEL_THICKNESS * 2);

            //door height
            assm.Configurations.Active.Components["Door-5"].Dimensions["D2@Sketch1"].SetValue(height - FRAME_HEIGHT);

            //drawer height
            assm.Configurations.Active.Components["Drawer-5"].Children["Drawer Front-1"].Dimensions["D2@Sketch1"].SetValue(drawerHeight);
            assm.Dimensions["D3@LocalLPattern1"].SetValue(drawerHeight + DRAWER_GAP);
            assm.Dimensions["D1@LocalLPattern1"].SetValue(drawersCount);

            //panels depth
            assm.Configurations.Active.Components["Panel End LH-1"].Dimensions["D1@Sketch1"].SetValue(depth);
            assm.Configurations.Active.Components["Panel End RH-1"].Dimensions["D1@Sketch1"].SetValue(depth);
            assm.Configurations.Active.Components["Panel Internal-4"].Dimensions["D1@Sketch1"].SetValue(depth - PANEL_THICKNESS);
            assm.Dimensions["D1@PLANE Rear of Cabinet"].SetValue(depth);
            assm.Configurations.Active.Components["Panel Top-1"].Dimensions["D2@Sketch1"].SetValue(depth);
            assm.Configurations.Active.Components["Panel Base-1"].Dimensions["D2@Sketch1"].SetValue(depth);
            assm.Configurations.Active.Components["Frame-1"].Dimensions["D3@3DSketch2"].SetValue(depth);

            //drawer depth
            assm.Configurations.Active.Components["Drawer-5"].Children["Drawer Body-1"].Dimensions["D1@Sketch1"].SetValue(depth - DRAWER_DEPTH_OFFSET);
        }

        private void SetHandle(IXAssembly assm, HandleType_e handleType) 
        {
            var assmDir = Path.GetDirectoryName(assm.Path);

            var handleFileName = "";

            switch (handleType) 
            {
                case HandleType_e.C:
                    handleFileName = "Handle-C.SLDPRT";
                    break;

                case HandleType_e.D:
                    handleFileName = "Handle-D.SLDPRT";
                    break;

                case HandleType_e.W:
                    handleFileName = "Handle-W.SLDPRT";
                    break;

                default:
                    throw new NotSupportedException("Handle type is not supported");
            }

            var handleFilePath = Path.Combine(assmDir, handleFileName);

            var handleComps = assm.Configurations.Active.Components.Where(
                c => c.Name.StartsWith("Handle-", StringComparison.CurrentCultureIgnoreCase)
                && !string.Equals(c.Path, handleFilePath, StringComparison.CurrentCultureIgnoreCase)).ToArray();

            if (handleComps.Any())
            {
                assm.Selections.AddRange(handleComps);

                if (!((ISwAssembly)assm).Assembly.ReplaceComponents2(handleFilePath,
                    "Default", true, (int)swReplaceComponentsConfiguration_e.swReplaceComponentsConfiguration_MatchName, true))
                {
                    throw new Exception("Failed to replace handle");
                }
            }
        }
    }
}
