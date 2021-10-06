using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Examples.DimensionWatcher.Properties;
using Xarial.XCad.SolidWorks.Annotations;
using Xarial.XCad.SolidWorks.UI.PropertyPage;

namespace Xarial.XCad.Examples.DimensionWatcher
{
    [ComVisible(true)]
    [Icon(typeof(Resources), nameof(Resources.dimension_watch_icon))]
    [Title("Dimension Watcher")]
    [Description("Select dimension to watch. Error will be displayed in the featureduring rebuild if dimension is not found")]
    public class DimensionWatcherData : SwPropertyManagerPageHandler
    {
        public ISwDimension Dimension { get; set; }
    }
}
