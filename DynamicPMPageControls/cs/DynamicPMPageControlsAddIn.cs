using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.UI.Commands;
using Xarial.XCad.UI.PropertyPage;

namespace DynamicPMPageControls
{
    [ComVisible(true)]
    [ProgId("C42FC147-D5D5-4E2B-9FB9-FB3CB5489C59")]
    public class DynamicPMPageControlsAddIn : SwAddInEx
    {
        [Title("Dynamic PMPage Controls")]
        private enum Commands_e 
        {
            ShowPage
        }

        private IXPropertyPage<PMPageData> m_Page;

        public override void OnConnect()
        {
            m_Page = this.CreatePage<PMPageData>();
            this.CommandManager.AddCommandGroup<Commands_e>().CommandClick += DOnCommandClick;
        }

        private void DOnCommandClick(Commands_e spec)
        {
            switch (spec) 
            {
                case Commands_e.ShowPage:
                    m_Page.Show(new PMPageData());
                    break;
            }
        }
    }
}
