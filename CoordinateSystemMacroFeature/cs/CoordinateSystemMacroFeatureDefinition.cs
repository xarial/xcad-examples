using System;
using System.Linq;
using System.Runtime.InteropServices;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Features.CustomFeature;
using Xarial.XCad.Features.CustomFeature.Delegates;
using Xarial.XCad.Geometry;
using Xarial.XCad.Geometry.Structures;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.Documents;
using Xarial.XCad.SolidWorks.Features.CustomFeature;
using Xarial.XCad.SolidWorks.Geometry;
using Xarial.XCad.SolidWorks.Geometry.Curves;
using Xarial.XCad.Base;
using SolidWorks.Interop.sldworks;
using Xarial.XCad.Documents;
using SolidWorks.Interop.swconst;
using Xarial.XCad.SolidWorks.Features;
using Xarial.XCad.Examples.CoordinateSystemMacroFeature.Properties;

namespace Xarial.XCad.Examples.CoordinateSystemMacroFeature
{
    [ComVisible(true)]
    [Title("Coordinate System Ex")]
    [Icon(typeof(Resources), nameof(Resources.coord_system))]
    public class CoordinateSystemMacroFeatureDefinition : SwMacroFeatureDefinition<CoordinateSystemData, CoordinateSystemData>
    {
        private int m_TrackingId;
        private const int X_BODY_TRACKING_ID = 1;
        private const int Y_BODY_TRACKING_ID = 2;
        private const int Z_BODY_TRACKING_ID = 3;

        public override ISwBody[] CreateGeometry(ISwApplication app, ISwDocument model, CoordinateSystemData data, bool isPreview, out AlignDimensionDelegate<CoordinateSystemData> alignDim)
        {
            alignDim = null;

            const double SCALE = 0.1;

            var origin = new Point(data.X, data.Y, data.Z);
            var x = new Vector(1, 0, 0);
            var y = new Vector(0, 1, 0);
            var z = new Vector(0, 0, 1);

            var rotation = TransformMatrix.Identity
                .Multiply(TransformMatrix.CreateFromRotationAroundAxis(x, data.RotationX, origin))
                .Multiply(TransformMatrix.CreateFromRotationAroundAxis(y, data.RotationY, origin))
                .Multiply(TransformMatrix.CreateFromRotationAroundAxis(z, data.RotationZ, origin));

            x = x.Transform(rotation);
            y = y.Transform(rotation);
            z = z.Transform(rotation);

            var xLine = (ISwLineCurve)app.MemoryGeometryBuilder.CreateLine(origin, origin.Move(x, SCALE / 3));
            var yLine = (ISwLineCurve)app.MemoryGeometryBuilder.CreateLine(origin, origin.Move(y, SCALE / 2));
            var zLine = (ISwLineCurve)app.MemoryGeometryBuilder.CreateLine(origin, origin.Move(z, SCALE));

            var xBody = (ISwBody)xLine.CreateBody();
            var yBody = (ISwBody)yLine.CreateBody();
            var zBody = (ISwBody)zLine.CreateBody();

            if (!isPreview)
            {
                m_TrackingId = app.Sw.RegisterTrackingDefinition("_CoordinateSystemEx_Bodies_");

                if (xBody.Body.SetTrackingID(m_TrackingId, X_BODY_TRACKING_ID) != (int)swTrackingIDError_e.swTrackingIDError_NoError)
                {
                    throw new Exception("Failed to track body");
                }

                if (yBody.Body.SetTrackingID(m_TrackingId, Y_BODY_TRACKING_ID) != (int)swTrackingIDError_e.swTrackingIDError_NoError)
                {
                    throw new Exception("Failed to track body");
                }

                if (zBody.Body.SetTrackingID(m_TrackingId, Z_BODY_TRACKING_ID) != (int)swTrackingIDError_e.swTrackingIDError_NoError)
                {
                    throw new Exception("Failed to track body");
                }
            }

            return new ISwBody[] { xBody, yBody, zBody };
        }

        public override void OnFeatureInserted(IXApplication app, IXDocument doc, IXCustomFeature<CoordinateSystemData> feat,
            CoordinateSystemData data, CoordinateSystemData page)
        {
            var bodies = ((object[])((ISwDocument)doc).Model.Extension.FindTrackedObjects(m_TrackingId, null,
                new int[] { (int)swTopoEntity_e.swTopoBody }, new int[] { X_BODY_TRACKING_ID, Y_BODY_TRACKING_ID, Z_BODY_TRACKING_ID })).Cast<IBody2>().ToArray();

            var xBody = bodies[0];
            var yBody = bodies[1];
            var zBody = bodies[2];

            var xEdge = (IEntity)((object[])xBody.GetEdges()).First();
            var yEdge = (IEntity)((object[])yBody.GetEdges()).First();
            var zEdge = (IEntity)((object[])zBody.GetEdges()).First();
            var orig = (IEntity)((IEdge)xEdge).GetStartVertex();

            if (xBody.RemoveTrackingID(m_TrackingId) != (int)swTrackingIDError_e.swTrackingIDError_NoError)
            {
                throw new Exception("Failed to clear track body");
            }

            if (yBody.RemoveTrackingID(m_TrackingId) != (int)swTrackingIDError_e.swTrackingIDError_NoError)
            {
                throw new Exception("Failed to clear track body");
            }
            
            if (zBody.RemoveTrackingID(m_TrackingId) != (int)swTrackingIDError_e.swTrackingIDError_NoError)
            {
                throw new Exception("Failed to clear track body");
            }

            orig.SelectByMark(false, 1);
            xEdge.SelectByMark(true, 2);
            yEdge.SelectByMark(true, 4);
            zEdge.SelectByMark(true, 8);

            var coordSysFeat = ((ISwDocument)doc).Model.FeatureManager.InsertCoordinateSystem(false, false, false);

            ((ISwFeature)feat).Feature.MakeSubFeature(coordSysFeat);
        }
    }
}
