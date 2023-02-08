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

namespace SampleAddIn1Core
{
    [ComVisible(true)]
    [Guid("7D680B2B-EC81-4F3C-973D-D165210B25F2")]
    public class PageSample : SwPropertyManagerPageHandler
    {
        public string TextBox { get; set; } = ".NET Core - 1";
    }

    public class SampleLogger : IXLogger
    {
        public void Log(string msg, LoggerMessageSeverity_e severity = LoggerMessageSeverity_e.Information)
            => this.Trace(msg, "xCAD.VersionConflict.SampleSwAddIn1Core", severity);
    }

    [ComVisible(true)]
    [Guid("BE63E950-811F-4359-81B7-2225424FA9DD")]
    public class SampleSwAddIn1Core : SwAddInEx
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

        [Title(".NET Core SampleSwAddIn - 1")]
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
