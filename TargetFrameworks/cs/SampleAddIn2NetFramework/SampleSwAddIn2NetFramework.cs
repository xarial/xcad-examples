using System;
using System.Runtime.InteropServices;
using Xarial.XCad;
using Xarial.XCad.Base;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Base.Enums;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.UI.PropertyPage;
using Xarial.XCad.UI.Commands;
using Xarial.XCad.UI.PropertyPage;

namespace SampleAddIn2
{
    [ComVisible(true)]
    public class PageSample : SwPropertyManagerPageHandler
    {
        public string TextBox { get; set; } = ".NET Framework - 2";
    }

    public class SampleLogger : IXLogger
    {
        public void Log(string msg, LoggerMessageSeverity_e severity = LoggerMessageSeverity_e.Information)
            => this.Trace(msg, "xCAD.VersionConflict.SampleAddIn2NetFramework", severity);
    }

    [ComVisible(true)]
    [Guid("70D4A40C-0395-4DF7-9838-1523B99781E2")]
    public class SampleSwAddIn2NetFramework : SwAddInEx
    {
        [Title(".NET Framework SampleSwAddIn - 2")]
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
