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

namespace Xarial.XCad.Samples.SampleAddIn1Net6
{
    [ComVisible(true)]
    [Guid("672AF27B-9C03-4273-9961-406803A06D10")]
    public class PageSample : SwPropertyManagerPageHandler
    {
        public string TextBox { get; set; } = ".NET6 - 1";
    }

    public class SampleLogger : IXLogger
    {
        public void Log(string msg, LoggerMessageSeverity_e severity = LoggerMessageSeverity_e.Information)
            => this.Trace(msg, "xCAD.VersionConflict.SampleSwAddIn1Net6", severity);
    }

    [ComVisible(true)]
    [Guid("632BDE86-9CEF-42E9-B575-8FEF85F81B7B")]
    public class SampleSwAddIn1Net6 : SwAddInEx
    {
        [Title(".NET6 SampleSwAddIn - 1")]
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
