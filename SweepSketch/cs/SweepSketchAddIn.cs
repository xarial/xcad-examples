using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Features.CustomFeature.Delegates;
using Xarial.XCad.Geometry;
using Xarial.XCad.Geometry.Structures;
using Xarial.XCad.Samples.SweepSketch.Properties;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.Documents;
using Xarial.XCad.SolidWorks.Features;
using Xarial.XCad.SolidWorks.Features.CustomFeature;
using Xarial.XCad.SolidWorks.Geometry;
using Xarial.XCad.SolidWorks.Services;
using Xarial.XCad.SolidWorks.Sketch;
using Xarial.XCad.SolidWorks.UI.PropertyPage;
using Xarial.XCad.UI.Commands;
using Xarial.XCad.UI.PropertyPage.Attributes;
using Xarial.XCad.UI.PropertyPage.Enums;

namespace Xarial.XCad.Samples.SweepSketch
{
    [ComVisible(true)]
    [Icon(typeof(Resources), nameof(Resources.sweep_sketch))]
    [Title("Swept Sketch")]
    [PageOptions(PageOptions_e.LockedPage | PageOptions_e.OkayButton | PageOptions_e.CancelButton)]
    public class SweepSketchData : SwPropertyManagerPageHandler
    {
        [Icon(typeof(Resources), nameof(Resources.sketch))]
        public SwSketchBase Sketch { get; set; }

        [NumberBoxOptions(NumberBoxUnitType_e.Length, 0.000001, 1000, 0.01, true, 0.02, 0.001)]
        [StandardControlIcon(BitmapLabelType_e.Diameter)]
        public double Diameter { get; set; } = 0.01;
    }

    [ComVisible(true)]
    [Icon(typeof(Resources), nameof(Resources.sweep_sketch))]
    [Title("Swept Sketch")]
    public class SweepSketchMacroFeatureEditor : SwMacroFeatureDefinition<SweepSketchData, SweepSketchData>
    {
        public override SwBody[] CreateGeometry(SwApplication app, SwDocument model,
            SweepSketchData data, bool isPreview, out AlignDimensionDelegate<SweepSketchData> alignDim)
        {
            alignDim = null;

            var result = new List<SwBody>();

            foreach (var seg in data.Sketch.Entities.OfType<SwSketchSegment>()) 
            {
                var path = seg.Definition;

                var startPt = path.StartPoint.Coordinate;
                var uParam = path.Curves.First().ReverseEvaluate(startPt.X, startPt.Y, startPt.Z);

                var evalData = path.Curves.First().Evaluate2(uParam, 2) as double[];

                var normalAtPoint = new Vector(evalData[3], evalData[4], evalData[5]);

                var profile = app.MemoryWireGeometryBuilder.CreateCircle(startPt, normalAtPoint, data.Diameter);

                var sweep = app.MemorySolidGeometryBuilder.CreateSweep(profile, path);

                result.AddRange(sweep.Bodies.OfType<SwBody>());
            }

            return result.ToArray();
        }
    }

    [Title("Geometry+")]
    public enum Commands_e 
    {
        [Icon(typeof(Resources), nameof(Resources.sweep_sketch))]
        [Title("Insert Sweep Sketch")]
        SweepSketch
    }

    [ComVisible(true)]
    public class SweepSketchAddIn : SwAddInEx
    {
        public override void OnConnect()
        {
            this.CommandManager.AddCommandGroup<Commands_e>().CommandClick += OnButtonClick;
        }

        private void OnButtonClick(Commands_e spec)
        {
            switch (spec) 
            {
                case Commands_e.SweepSketch:
                    Application.Documents.Active.Features.CreateCustomFeature<SweepSketchMacroFeatureEditor, SweepSketchData, SweepSketchData>();
                    break;
            }
        }

        public override void ConfigureServices(IXServiceCollection collection)
        {
            collection.AddOrReplace<IMemoryGeometryBuilderDocumentProvider>(
                () => new LazyNewDocumentGeometryBuilderDocumentProvider(Application));
        }
    }
}
