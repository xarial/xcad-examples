using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Examples.SwTaskPaneAddIn.ViewModels;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.UI;

namespace Xarial.XCad.Examples.SwTaskPaneAddIn
{
    [ComVisible(true)]
    [Title("SOLIDWORKS Task Pane Add-In Example")]
    [Guid("C56E0AFF-0BD3-4364-90CB-1A581046CD7D")]
    public class MainAddIn : SwAddInEx
    {
        private ISwTaskPane<SwTaskPaneControl> m_TaskPane;
        private SwTaskPaneControl m_WpfControl;

        public override void OnConnect()
        {
            m_TaskPane = this.CreateTaskPaneWpf<SwTaskPaneControl>();
            m_WpfControl = m_TaskPane.Control;
            m_WpfControl.DataContext = new TaskPaneVM();
        }

        public override void OnDisconnect()
        {
            m_TaskPane.Close();
        }
    }
}
