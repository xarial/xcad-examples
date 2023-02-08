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

namespace SampleAddIn2Core
{
    [ComVisible(true)]
    [Guid("CD24E6A1-AED3-42F2-A90B-57524026EF33")]
    public class PageSample : SwPropertyManagerPageHandler
    {
        public string TextBox { get; set; } = ".NET Core - 2";
    }

    public class SampleLogger : IXLogger
    {
        public void Log(string msg, LoggerMessageSeverity_e severity = LoggerMessageSeverity_e.Information)
            => this.Trace(msg, "xCAD.VersionConflict.SampleSwAddIn2Core", severity);
    }

    [ComVisible(true)]
    [Guid("2C71A5B1-1843-4134-B4C1-D8C8A5B62D2B")]
    public class SampleSwAddIn2Core : SwAddInEx
    {
        [ComRegisterFunction]
        public static void RegisterFunction(Type t)
        {
            SwAddInEx.RegisterFunction(t);
        }

        [ComUnregisterFunction]
        public static void UnregisterFunction(Type t)
        {
            SwAddInEx.UnregisterFunction(t);
        }

        [Title(".NET Core SampleSwAddIn - 2")]
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
