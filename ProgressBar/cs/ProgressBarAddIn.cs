using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.UI.PropertyPage;
using Xarial.XCad.UI.Commands;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.UI.Commands.Attributes;
using Xarial.XCad.UI.Commands.Enums;
using Xarial.XCad.Examples.ProgressBar.Properties;

namespace Xarial.XCad.Examples.ProgressBar
{
    [Title("Progress Bar Example")]
    public enum Commands_e 
    {
        [Title("Show Progress Page")]
        [CommandItemInfo(WorkspaceTypes_e.AllDocuments)]
        [Icon(typeof(Resources), nameof(Resources.progres_bar))]
        ShowProgressPage
    }

    [ComVisible(true)]
    public class ProgressBarAddIn : SwAddInEx
    {
        private IXPropertyPage<PMPage> m_Page;
        private PMPage m_PageData;
        
        public override void OnConnect()
        {
            m_Page = CreatePage<PMPage>();
            m_PageData = new PMPage(Application);

            CommandManager.AddCommandGroup<Commands_e>().CommandClick += OnCommandClick;
        }

        private void OnCommandClick(Commands_e spec)
        {
            switch (spec) 
            {
                case Commands_e.ShowProgressPage:
                    m_Page.Show(m_PageData);
                    break;
            }
        }
    }
}
