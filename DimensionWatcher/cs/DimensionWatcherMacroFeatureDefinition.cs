using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Examples.DimensionWatcher.Properties;
using Xarial.XCad.Features.CustomFeature.Delegates;
using Xarial.XCad.Features.CustomFeature.Structures;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.Documents;
using Xarial.XCad.SolidWorks.Features.CustomFeature;

namespace Xarial.XCad.Examples.DimensionWatcher
{
    [ComVisible(true)]
    [Icon(typeof(Resources), nameof(Resources.dimension_watch_icon))]
    [Title("DimensionWatcher")]
    public class DimensionWatcherMacroFeatureDefinition : SwMacroFeatureDefinition<DimensionWatcherData, DimensionWatcherData>
    {
        public override CustomFeatureRebuildResult OnRebuild(ISwApplication app, ISwDocument model,
            ISwMacroFeature<DimensionWatcherData> feature, DimensionWatcherData parameters,
            out AlignDimensionDelegate<DimensionWatcherData> alignDim)
        {
            alignDim = null;

            var res = parameters.Dimension != null;

            return new CustomFeatureRebuildResult()
            {
                Result = parameters.Dimension != null,
                ErrorMessage = !res ? "Dimension not found" : ""
            };
        }
    }
}
