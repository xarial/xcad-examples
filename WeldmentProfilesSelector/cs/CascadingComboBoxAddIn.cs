using System.Runtime.InteropServices;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.UI.PropertyPage;
using Xarial.XCad.UI.Commands;

namespace CascadingComboBox
{
    [ComVisible(true)]
    public partial class CascadingComboBoxAddIn : SwAddInEx
    {
        public enum Commands_e
        {
            ShowWeldmentProfiles
        }

        private PMPData m_Data;
        private ISwPropertyManagerPage<PMPData> m_Page;

        public override void OnConnect()
        {
            m_Page = CreatePage<PMPData>();
            m_Data = new PMPData();
            CommandManager.AddCommandGroup<Commands_e>().CommandClick += OnCommandClick;
        }

        private void OnCommandClick(Commands_e spec)
        {
            switch (spec) 
            {
                case Commands_e.ShowWeldmentProfiles:
                    m_Page.Show(m_Data);
                    break;
            }
        }
    }
}
