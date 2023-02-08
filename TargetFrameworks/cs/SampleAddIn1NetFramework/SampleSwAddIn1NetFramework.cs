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

namespace SampleAddIn1
{
    [ComVisible(true)]
    public class PageSample : SwPropertyManagerPageHandler
    {
        public string TextBox { get; set; } = ".NET Framework - 1";
    }

    public class SampleLogger : IXLogger
    {
        public void Log(string msg, LoggerMessageSeverity_e severity = LoggerMessageSeverity_e.Information)
            => this.Trace(msg, "xCAD.VersionConflict.SampleAddIn1NetFramework", severity);
    }

    [ComVisible(true)]
    [Guid("021678DA-D28E-4772-BFE6-113497593C47")]
    public class SampleSwAddIn1NetFramework : SwAddInEx
    {
        [Title(".NET Framework SampleSwAddIn - 1")]
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

        public override void OnConfigureServices(IXServiceCollection collection)
        {
            collection.AddOrReplace<IXLogger, SampleLogger>();
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
