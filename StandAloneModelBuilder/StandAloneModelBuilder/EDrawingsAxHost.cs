using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xarial.XCad.UI;

namespace Xarial.XCad.Examples.Sw.StandAloneModelBuilder
{
    public class EDrawingsAxHost : AxHost
    {
        private const string DEFAULT_EDRAWINGS_OCX = "22945A69-1191-4DCF-9E6F-409BDE94D101";

        private bool m_IsLoaded;
        private object m_EDrawingsControl;

        public object EDrawingsControl
        {
            get
            {
                if (m_IsLoaded)
                {
                    return m_EDrawingsControl;
                }
                else
                {
                    throw new Exception("eDrawings control is not loaded");
                }
            }
        }

        public EDrawingsAxHost()
            : base(DEFAULT_EDRAWINGS_OCX)
        {
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (!m_IsLoaded) //this function is called twice
            {
                m_IsLoaded = true;
                var ocx = GetOcx();

                if (ocx != null)
                {
                    m_EDrawingsControl = ocx;
                }
                else
                {
                    throw new Exception("eDrawings control OCX is null");
                }
            }
        }

        public void OpenFile(string filePath)
            => ((dynamic)EDrawingsControl).OpenDoc(filePath, false, false, true, "");
    }
}
