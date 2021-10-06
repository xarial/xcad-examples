using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Examples.DimensionWatcher.Properties;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.UI.Commands;

namespace Xarial.XCad.Examples.DimensionWatcher
{
    [ComVisible(true)]
    [Guid("03A30687-DDF2-4614-AB36-25DEBF8A67E6")]
    public class DimensionWatcherAddIn : SwAddInEx
    {
        [Title("Dimension Watcher")]
        public enum Commands_e 
        {
            [Icon(typeof(Resources), nameof(Resources.dimension_watch_icon))]
            [Title("Insert Dimension Watcher")]
            InsertDimensionWatcher
        }

        public override void OnConnect()
        {
            this.CommandManager.AddCommandGroup<Commands_e>().CommandClick += OnCommandClick;
        }

        private void OnCommandClick(Commands_e spec)
        {
            switch (spec) 
            {
                case Commands_e.InsertDimensionWatcher:
                    Application.Documents.Active.Features.CreateCustomFeature<DimensionWatcherMacroFeatureDefinition, DimensionWatcherData, DimensionWatcherData>();
                    break;
            }
        }
    }
}
