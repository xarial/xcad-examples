using PMPageToggleBitmapButtons.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.SolidWorks.UI.PropertyPage;
using Xarial.XCad.UI.PropertyPage.Attributes;

namespace PMPageToggleBitmapButtons
{
    [ComVisible(true)]
    public class PMPage : SwPropertyManagerPageHandler
    {
        public OptionToggleButtons ToggleOptions { get; set; }
    }

    public class OptionToggleButtons : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool m_CheckBoxA;
        private bool m_CheckBoxB;
        private bool m_CheckBoxC;
        private bool m_CheckBoxD;
        private bool m_CheckBoxE;
        private bool m_CheckBoxF;

        [BitmapButton(typeof(Resources), nameof(Resources.a), 48, 48)]
        [ControlOptions(left: 0, top: 0)]
        public bool CheckBoxA
        {
            get => m_CheckBoxA;
            set
            {
                m_CheckBoxA = value;
                this.HandleCheckChanged();
            }
        }

        [BitmapButton(typeof(Resources), nameof(Resources.b), 48, 48)]
        [ControlOptions(left: 60, top: 0)]
        public bool CheckBoxB
        {
            get => m_CheckBoxB;
            set
            {
                m_CheckBoxB = value;
                this.HandleCheckChanged();
            }
        }

        [BitmapButton(typeof(Resources), nameof(Resources.c), 48, 48)]
        [ControlOptions(left: 0, top: 60)]
        public bool CheckBoxC
        {
            get => m_CheckBoxC;
            set
            {
                m_CheckBoxC = value;
                this.HandleCheckChanged();
            }
        }

        [BitmapButton(typeof(Resources), nameof(Resources.d), 48, 48)]
        [ControlOptions(left: 60, top: 60)]
        public bool CheckBoxD
        {
            get => m_CheckBoxD;
            set
            {
                m_CheckBoxD = value;
                this.HandleCheckChanged();
            }
        }

        [BitmapButton(typeof(Resources), nameof(Resources.e), 48, 48)]
        [ControlOptions(left: 0, top: 120)]
        public bool CheckBoxE
        {
            get => m_CheckBoxE;
            set
            {
                m_CheckBoxE = value;
                this.HandleCheckChanged();
            }
        }

        [BitmapButton(typeof(Resources), nameof(Resources.f), 48, 48)]
        [ControlOptions(left: 60, top: 120)]
        public bool CheckBoxF
        {
            get => m_CheckBoxF;
            set
            {
                m_CheckBoxF = value;
                this.HandleCheckChanged();
            }
        }

        private string m_CurCheckPrpName;

        private void HandleCheckChanged([CallerMemberName]string prpName = "")
        {
            if (!string.IsNullOrEmpty(m_CurCheckPrpName))
            {
                var prpInfo = this.GetType().GetProperty(m_CurCheckPrpName);

                if ((bool)prpInfo.GetValue(this) != false)
                {
                    prpInfo.SetValue(this, false, null);
                }
            }

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prpName));

            m_CurCheckPrpName = prpName;
        }
    }
}
