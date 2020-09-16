using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace XCad.Examples.IssuesManager.SwAddIn
{
    [ComVisible(true)]
    [Guid("E6781AD3-A939-4102-83DB-961FF5881985")]
    public class IssuesManagerSwAddIn : Xarial.XCad.SolidWorks.SwAddInEx
    {
        private IssuesManagerController m_Controller;

        public override void OnConnect()
        {
            m_Controller = new IssuesManagerController(this);
        }

        public override void OnDisconnect()
        {
            m_Controller.Dispose();
        }
    }
}
