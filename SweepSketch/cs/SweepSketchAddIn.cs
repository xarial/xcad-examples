using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Features.CustomFeature;
using Xarial.XCad.Features.CustomFeature.Attributes;
using Xarial.XCad.Features.CustomFeature.Delegates;
using Xarial.XCad.Features.CustomFeature.Enums;
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
        [ControlOptions(height: 50)]
        public List<ISwSketchBase> Sketches { get; set; }

        [NumberBoxOptions(NumberBoxUnitType_e.Length, 0.000001, 1000, 0.01, true, 0.02, 0.001)]
        [StandardControlIcon(BitmapLabelType_e.Radius)]
        [ParameterDimension(CustomFeatureDimensionType_e.Radial)]
        public double Radius { get; set; } = 0.005;

        [Title("Add Length custom property")]
        public bool AddLengthPropety { get; set; } = false;
    }
    
    [ComVisible(true)]
    [Icon(typeof(Resources), nameof(Resources.sweep_sketch))]
    [Title("Swept Sketch")]
    public class SweepSketchMacroFeatureEditor : SwMacroFeatureDefinition<SweepSketchData, SweepSketchData>
    {
        private static ISwApplication m_App;

        private const string LENGTH_PRP_NAME = "LENGTH";
        private const string CUT_LIST_LENGTH_TRACKING_DEF_NAME = "__Xarial__SweepSketch__CutListLength__";
        
        public override ISwBody[] CreateGeometry(ISwApplication app, ISwDocument model,
            SweepSketchData data, bool isPreview, out AlignDimensionDelegate<SweepSketchData> alignDim)
        {
            m_App = app;

            var cutListLengthTrackingId = app.Sw.RegisterTrackingDefinition(CUT_LIST_LENGTH_TRACKING_DEF_NAME);

            var result = new List<ISwBody>();

            Point firstCenterPt = null;
            Vector firstDir = null;

            int index = 0;
            var lengths = new List<double>();

            var setLengthPrp = !isPreview && data.AddLengthPropety && (model as ISwPart).Part.IsWeldment();

            foreach (var sketch in data.Sketches)
            {
                foreach (var seg in sketch.Entities.OfType<ISwSketchSegment>().Where(s => !s.Segment.ConstructionGeometry))
                {
                    var path = seg.Definition;

                    var startPt = path.StartPoint.Coordinate;

                    if (firstCenterPt == null)
                    {
                        firstCenterPt = startPt;
                    }

                    var uParam = path.Curves.First().ReverseEvaluate(startPt.X, startPt.Y, startPt.Z);

                    var evalData = path.Curves.First().Evaluate2(uParam, 2) as double[];

                    var normalAtPoint = new Vector(evalData[3], evalData[4], evalData[5]);

                    if (firstDir == null)
                    {
                        firstDir = normalAtPoint;
                    }

                    var profile = app.MemoryGeometryBuilder.CreateCircle(startPt, normalAtPoint, data.Radius * 2);

                    var profileRegion = app.MemoryGeometryBuilder.CreateRegionFromSegments(profile);

                    var region = app.MemoryGeometryBuilder.CreatePlanarSheet(profileRegion).Bodies.First();

                    var sweep = app.MemoryGeometryBuilder.CreateSolidSweep(path, region);

                    var body = sweep.Bodies.OfType<ISwBody>().First();

                    if (setLengthPrp)
                    {
                        body.Body.SetTrackingID(cutListLengthTrackingId, index++);

                        lengths.Add(path.Length);
                    }

                    result.Add(body);
                }
            }

            alignDim = (name, dim) =>
            {
                switch (name)
                {
                    case nameof(SweepSketchData.Radius):
                        this.AlignRadialDimension(dim, firstCenterPt, firstDir);
                        break;
                }
            };

            if (setLengthPrp) 
            {
                model.Tags.Put(CUT_LIST_LENGTH_TRACKING_DEF_NAME, lengths);
                (model as ISwPart).CutListRebuild += OnCutListRebuild;
            }

            return result.ToArray();
        }

        private void OnCutListRebuild(Documents.IXPart part)
        {
            part.CutListRebuild -= OnCutListRebuild;

            SetLengthProperties((ISwPart)part);
        }

        private void SetLengthProperties(ISwPart part) 
        {
            var lengths = part.Tags.Pop<List<double>>(CUT_LIST_LENGTH_TRACKING_DEF_NAME);

            var sw = m_App.Sw;

            var cutListLengthTrackingId = sw.RegisterTrackingDefinition(CUT_LIST_LENGTH_TRACKING_DEF_NAME);

            foreach (ISwFeature feat in part.Features)
            {
                if (feat.Feature.GetTypeName2() == "CutListFolder")
                {
                    var bodyFolderFeat = feat.Feature.GetSpecificFeature2() as IBodyFolder;
                    var bodies = bodyFolderFeat.GetBodies() as object[];

                    if (bodies != null)
                    {
                        foreach (IBody2 body in bodies)
                        {
                            object trackingIds;
                            body.GetTrackingIDs(cutListLengthTrackingId, out trackingIds);

                            if (trackingIds is int[])
                            {
                                var index = (trackingIds as int[]).First();
                                var length = lengths[index];

                                var userUnit = part.Model.IGetUserUnit((int)swUserUnitsType_e.swLengthUnit);
                                length = length * userUnit.GetConversionFactor();

                                feat.Feature.CustomPropertyManager.Add3(LENGTH_PRP_NAME,
                                    (int)swCustomInfoType_e.swCustomInfoDouble,
                                    length.ToString(),
                                    (int)swCustomPropertyAddOption_e.swCustomPropertyReplaceValue);
                            }
                        }
                    }
                }
                else if (feat.Feature.GetTypeName2() == "RefPlane")
                {
                    break;
                }
            }
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

        public override void OnConfigureServices(IXServiceCollection collection)
        {
            collection.AddOrReplace<IMemoryGeometryBuilderDocumentProvider>(
                () => new LazyNewDocumentGeometryBuilderDocumentProvider(Application));
        }
    }
}
