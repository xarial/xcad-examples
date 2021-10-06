using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.Annotations;
using Xarial.XCad.Documents;
using Xarial.XCad.SolidWorks.Documents;
using XCad.Examples.FurnitureConfigurator.Enums;

namespace XCad.Examples.FurnitureConfigurator.Services
{
    public class Cabinet 
    {
        public double DoorWidth { get; set; }
        public double DoorHeight { get; set; }
    }

    public class CabinetConfiguratorService
    {
        private const double PANEL_THICKNESS = 0.018;
        private const double DOOR_GAP = 0.002;
        private const double DRAWER_GAP = 0.0015;
        private const double DRAWER_BODY_FRONT_OFFSET = 0.037;
        private const double FRAME_HEIGHT = 0.2;
        private const double DRAWER_DEPTH_OFFSET = 0.04;

        public void Configure(IXAssembly assm, double width, double height, double depth, int drawersCount, double drawerWidth, HandleType_e handleType)
        {
            var hasChanges = SetParameters(assm, width, height, depth, drawersCount, drawerWidth);

            hasChanges |= SetHandle(assm, handleType);

            if (hasChanges)
            {
                assm.Rebuild();
            }
        }

        public Cabinet Calculate(double width, double height, double depth, int drawersCount, double drawerWidth)
        {
            var doorWidth = (width - drawerWidth - DOOR_GAP * 3) / 3;

            return new Cabinet()
            {
                DoorWidth = Math.Round(doorWidth * 1000, 2),
                DoorHeight = Math.Round(height * 1000 - FRAME_HEIGHT * 1000, 2)
            };
        }

        private bool SetParameters(IXAssembly assm, double width, double height, double depth, int drawersCount, double drawerWidth)
        {
            var cabinet = Calculate(width, height, depth, drawersCount, drawerWidth);

            var doorWidth = (width - drawerWidth - DOOR_GAP * 3) / 3;
            var drawerHeight = (height - FRAME_HEIGHT - DRAWER_GAP * (drawersCount - 1)) / drawersCount;

            var hasChanges = false;

            //panels width
            hasChanges |= SetDimension(assm, width, "D1@Sketch1", "Panel Top-1");
            hasChanges |= SetDimension(assm, width - PANEL_THICKNESS * 2, "D1@Sketch1", "Panel Rear-1");
            hasChanges |= SetDimension(assm, width, "D2@3DSketch2", "Frame-1");
            hasChanges |= SetDimension(assm, width - PANEL_THICKNESS * 2, "D1@Sketch1", "Panel Base-1");
            hasChanges |= SetDimension(assm, width, "D1@Dist Panel LH to RH Outside");
            
            //door width
            hasChanges |= SetDimension(assm, cabinet.DoorWidth * 0.001, "D1@Sketch1", "Door-5");

            //internal panels distance
            hasChanges |= SetDimension(assm, doorWidth + DOOR_GAP / 2 - PANEL_THICKNESS / 2, "D1@PLANE Panel Internal 1");
            hasChanges |= SetDimension(assm, doorWidth + DOOR_GAP + drawerWidth + DOOR_GAP / 2 - PANEL_THICKNESS / 2, "D1@PLANE Panel Internal 2");
            hasChanges |= SetDimension(assm, doorWidth + DOOR_GAP + drawerWidth + DOOR_GAP + doorWidth + DOOR_GAP / 2 - PANEL_THICKNESS / 2, "D1@PLANE Panel Internal 3");
            
            //drawer width
            hasChanges |= SetDimension(assm, drawerWidth, "D1@Sketch1", "Drawer-5", "Drawer Front-1");
            hasChanges |= SetDimension(assm, drawerWidth - DRAWER_BODY_FRONT_OFFSET, "D2@Sketch1", "Drawer-5", "Drawer Body-1");

            //panels height
            hasChanges |= SetDimension(assm, height, "D1@PLANE Top of Cabinet");
            hasChanges |= SetDimension(assm, height - FRAME_HEIGHT - PANEL_THICKNESS, "D2@Sketch1", "Panel End LH-1");
            hasChanges |= SetDimension(assm, height - FRAME_HEIGHT - PANEL_THICKNESS, "D2@Sketch1", "Panel End RH-1");
            hasChanges |= SetDimension(assm, height - FRAME_HEIGHT - PANEL_THICKNESS * 2, "D2@Sketch1", "Panel Internal-4");
            hasChanges |= SetDimension(assm, height - FRAME_HEIGHT - PANEL_THICKNESS * 2, "D2@Sketch1", "Panel Rear-1");

            //door height
            hasChanges |= SetDimension(assm, cabinet.DoorHeight * 0.001, "D2@Sketch1", "Door-5");

            //drawer height
            hasChanges |= SetDimension(assm, drawerHeight, "D2@Sketch1", "Drawer-5", "Drawer Front-1");
            hasChanges |= SetDimension(assm, drawerHeight + DRAWER_GAP, "D3@LocalLPattern1");
            hasChanges |= SetDimension(assm, drawersCount, "D1@LocalLPattern1");
            
            //panels depth
            hasChanges |= SetDimension(assm, depth, "D1@Sketch1", "Panel End LH-1");
            hasChanges |= SetDimension(assm, depth, "D1@Sketch1", "Panel End RH-1");
            hasChanges |= SetDimension(assm, depth - PANEL_THICKNESS, "D1@Sketch1", "Panel Internal-4");
            hasChanges |= SetDimension(assm, depth, "D1@PLANE Rear of Cabinet");
            hasChanges |= SetDimension(assm, depth, "D2@Sketch1", "Panel Top-1");
            hasChanges |= SetDimension(assm, depth, "D2@Sketch1", "Panel Base-1");
            hasChanges |= SetDimension(assm, depth, "D3@3DSketch2", "Frame-1");

            //drawer depth
            hasChanges |= SetDimension(assm, depth - DRAWER_DEPTH_OFFSET, "D1@Sketch1", "Drawer-5", "Drawer Body-1");

            return hasChanges;
        }

        private bool SetHandle(IXAssembly assm, HandleType_e handleType) 
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

                return true;
            }
            else 
            {
                return false;
            }
        }

        private bool SetDimension(IXAssembly assm, double value, string dimName, params string[] compsPath) 
        {
            IXDimensionRepository dims;

            if (!compsPath.Any())
            {
                dims = assm.Dimensions;
            }
            else 
            {
                var comps = assm.Configurations.Active.Components;
                
                foreach (var compName in compsPath.Take(compsPath.Length - 1))
                {
                    comps = comps[compName].Children;
                }

                dims = comps[compsPath.Last()].Dimensions;
            }

            var dim = dims[dimName];

            if (Math.Abs(dim.GetValue() - value) > double.Epsilon)
            {
                dim.SetValue(value);
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}
