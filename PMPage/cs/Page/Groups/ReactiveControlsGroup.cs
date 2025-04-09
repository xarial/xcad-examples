using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.UI.PropertyPage.Attributes;

namespace Xarial.XCad.Examples.PMPage.CSharp.Page.Groups
{
    /// <summary>
    /// Controls is in this group update their values based on external trigger
    /// </summary>
    public class ReactiveControlsGroup : INotifyPropertyChanged
    {
        public class DataItem : INotifyPropertyChanged 
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public string Name 
            {
                get => m_Name;
                set 
                {
                    m_Name = value;
                    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }

            private string m_Name;

            public DataItem(string name)
            {
                m_Name = name;
            }

            public override string ToString() => Name;
        }

        /// <summary>
        /// Shared items source for ComboBox, ListBox, CheckBoxList and OptionBox controls
        /// </summary>
        [Metadata(nameof(DataItemsSource))]
        public static DataItem[] DataItemsSource { get; }

        private int m_Index;

        private static string[] m_ItemBaseNames;

        static ReactiveControlsGroup() 
        {
            m_ItemBaseNames = new string[]
            {
                "A", "B", "C", "D", "E"
            };

            DataItemsSource = new DataItem[m_ItemBaseNames.Length];

            for (int i = 0; i < m_ItemBaseNames.Length; i++)
            {
                DataItemsSource[i] = new DataItem(m_ItemBaseNames[i]);
            }
        }

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
        /// Option box control
        /// </summary>
        [OptionBox(ItemsSource = nameof(DataItemsSource), DisplayMemberPath = nameof(DataItem.Name))]
        public DataItem OptionBox { get; set; }

        /// <summary>
        /// ComboBox control
        /// </summary>
        [ComboBox(ItemsSource = nameof(DataItemsSource), DisplayMemberPath = nameof(DataItem.Name))]
        public DataItem ComboBox { get; set; }

        /// <summary>
        /// ListBoxControl
        /// </summary>
        [ListBox(ItemsSource = nameof(DataItemsSource), DisplayMemberPath = nameof(DataItem.Name))]
        public List<DataItem> ListBox { get; set; }

        /// <summary>
        /// CheckBoxList control
        /// </summary>
        [CheckBoxList(ItemsSource = nameof(DataItemsSource), DisplayMemberPath = nameof(DataItem.Name))]
        public List<DataItem> CheckBoxList { get; set; }

        /// <summary>
        /// Button to change the value of CheckBox and TextBox
        /// </summary>
        public Action ChangeValues { get; set; }

        public ReactiveControlsGroup() 
        {
            ChangeValues = OnChangeValues;
        }

        /// <summary>
        /// Assign new guid to TextBox and revert value of CheckBox, updates index of items control items
        /// </summary>
        private void OnChangeValues() 
        {
            GuidTextBox = Guid.NewGuid().ToString();
            CheckBox = !CheckBox;

            m_Index++;

            for (int i = 0; i < m_ItemBaseNames.Length; i++)
            {
                var item = DataItemsSource[i];
                item.Name = $"{m_ItemBaseNames[i]}-{m_Index}";
            }
        }
    }
}
