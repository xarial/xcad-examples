using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xarial.XCad.Examples.PMPage.CSharp.Page.Groups
{
    /// <summary>
    /// Controls is in this group update their values based on external trigger
    /// </summary>
    public class ReactiveControlsGroup : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_GuidTextBox;
        private bool m_CheckBox;

        /// <summary>
        /// TextBox rendering guid value
        /// </summary>
        public string GuidTextBox
        {
            get => m_GuidTextBox;
            set
            {
                m_GuidTextBox = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GuidTextBox)));
            }
        }

        /// <summary>
        /// Simple CheckBox
        /// </summary>
        public bool CheckBox
        {
            get => m_CheckBox;
            set 
            {
                m_CheckBox = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CheckBox)));
            }
        }

        /// <summary>
        /// Button to change the value of CheckBox and TextBox
        /// </summary>
        public Action ChangeValues { get; set; }

        public ReactiveControlsGroup() 
        {
            ChangeValues = OnChangeValues;
        }

        /// <summary>
        /// Assign new guid to TextBox and revert value of CheckBox
        /// </summary>
        private void OnChangeValues() 
        {
            GuidTextBox = Guid.NewGuid().ToString();
            CheckBox = !CheckBox;
        }
    }
}
