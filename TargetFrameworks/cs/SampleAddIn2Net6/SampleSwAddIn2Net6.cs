using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.Base;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Base.Enums;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.UI.PropertyPage;
using Xarial.XCad.UI.Commands;
using Xarial.XCad.UI.PropertyPage;

namespace Xarial.XCad.Samples.SampleAddIn2Net6
{
    [ComVisible(true)]
    [Guid("8267BC7B-0962-4DE9-9311-5137A2C45FD5")]
    public class PageSample : SwPropertyManagerPageHandler
    {
        public string TextBox { get; set; } = ".NET6 - 2";
    }

    public class SampleLogger : IXLogger
    {
        public void Log(string msg, LoggerMessageSeverity_e severity = LoggerMessageSeverity_e.Information)
            => this.Trace(msg, "xCAD.VersionConflict.SampleSwAddIn2Net6", severity);
    }

    [ComVisible(true)]
    [Guid("FA6212B5-E4C7-4EBE-AE9B-BD36861639A1")]
    public class SampleSwAddIn2Net6 : SwAddInEx
    {
        [Title(".NET6 SampleSwAddIn - 2")]
        private enum Commands_e
        {
            ShowPage
        }

        private IXPropertyPage<PageSample> m_Page;

        public override void OnConnect()
        {
            m_Page = this.CreatePage<PageSample>();
            this.CommandManager.AddCommandGroup<Commands_e>().CommandClick += OnCommandClick;
        }

        protected override void OnConfigureServices(IXServiceCollection collection)
        {
            collection.Add<IXLogger, SampleLogger>();
        }

        private void OnCommandClick(Commands_e spec)
        {
            switch (spec)
            {
                case Commands_e.ShowPage:
                    m_Page.Show(new PageSample());
                    break;
            }
        }
    }
}
