using System;
using System.Runtime.InteropServices;
using ttl.Properties;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Features.CustomFeature.Attributes;
using Xarial.XCad.Features.CustomFeature.Enums;
using Xarial.XCad.SolidWorks.Geometry;
using Xarial.XCad.SolidWorks.UI.PropertyPage;
using Xarial.XCad.UI.PropertyPage.Attributes;
using Xarial.XCad.UI.PropertyPage.Enums;

namespace ttl
{
    [Title("Insert Shaft Chamfer")]
    [Icon(typeof(Resources), nameof(Resources.shaft_chamfer))]
    [ComVisible(true)]
    public class ShaftChamferData : SwPropertyManagerPageHandler
    {
        private ISwCircularEdge m_Edge;

        [ExcludeControl]
        [ParameterEditBody]
        public ISwBody Body { get; set; }

        public ISwCircularEdge Edge
        {
            get
            {
                return m_Edge;
            }
            set
            {
                m_Edge = value;
                Body = value?.Body;
            }
        }

        [ParameterDimension(CustomFeatureDimensionType_e.Radial)]
        [NumberBoxOptions(NumberBoxUnitType_e.Length, 0, 1000, 0.001, true, 0.01, 0.005)]
        [StandardControlIcon(BitmapLabelType_e.Radius)]
        [Title("Radius")]
        public double Radius { get; set; } = 0.02;

        [ParameterDimension(CustomFeatureDimensionType_e.Angular)]
        [NumberBoxOptions(NumberBoxUnitType_e.Angle, 0, Math.PI / 2, Math.PI / 180, false, Math.PI / 90, Math.PI / 360)]
        [StandardControlIcon(BitmapLabelType_e.AngularDistance)]
        [Title("Angle")]
        public double Angle { get; set; } = Math.PI / 9;
    }
}
