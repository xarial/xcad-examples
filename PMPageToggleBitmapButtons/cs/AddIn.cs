using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.UI.PropertyPage;
using Xarial.XCad.UI.Commands;

namespace PMPageToggleBitmapButtons
{
    [ComVisible(true)]
    public class AddIn : SwAddInEx
    {
        [Title("PMPageToggleBitmapButtons")]
        private enum Commands_e 
        {
            ShowPMPage
        }

        private ISwPropertyManagerPage<PMPage> m_Page;
        private PMPage m_Model;

        public override void OnConnect()
        {
            m_Model = new PMPage();

            this.CommandManager.AddCommandGroup<Commands_e>().CommandClick += OnButtonClick; ;

            m_Page = this.CreatePage<PMPage>();
            m_Page.Closed += OnPageClosed;
        }

        private void OnButtonClick(Commands_e spec)
        {
            switch (spec) 
            {
                case Commands_e.ShowPMPage:
                    m_Page.Show(m_Model);
                    break;
            }
        }

        private void OnPageClosed(Xarial.XCad.UI.PropertyPage.Enums.PageCloseReasons_e reason)
        {
            if (reason == Xarial.XCad.UI.PropertyPage.Enums.PageCloseReasons_e.Okay)
            {
                Application.ShowMessageBox($"CheckBoxA: {m_Model.ToggleOptions.CheckBoxA}\r\nCheckBoxB: {m_Model.ToggleOptions.CheckBoxB}\r\nCheckBoxC: {m_Model.ToggleOptions.CheckBoxC}\r\nCheckBoxD: {m_Model.ToggleOptions.CheckBoxD}\r\nCheckBoxE: {m_Model.ToggleOptions.CheckBoxE}\r\nCheckBoxF: {m_Model.ToggleOptions.CheckBoxF}\r\n");
            }
        }
    }
}
