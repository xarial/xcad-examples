using System.Collections.Generic;
using System.Linq;
using Xarial.XCad.UI.PropertyPage.Attributes;
using Xarial.XCad.UI.PropertyPage.Base;
using Xarial.XCad.UI.PropertyPage.Services;

namespace Xarial.XCad.Examples.PMPage.CSharp.Page.Groups
{
    public class CustomItem
    {
        public string Name { get; }

        public CustomItem(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class CustomItemsProvider : ICustomItemsProvider
    {
        public IEnumerable<object> ProvideItems(IXApplication app, IControl[] dependencies)
        {
            var depVal = dependencies.First()?.GetValue()?.ToString();

            return new string[]
            {
                    depVal + "_A",
                    depVal + "_B",
                    depVal + "_C"
            };
        }
    }

    public enum EnumItemsSource_e
    {
        Value1,
        Value2,
        Value3
    }

    public class ItemSourceControlsGroup
    {
        [ControlTag("MyComboBox")]
        public EnumItemsSource_e ComboBox { get; set; }

        [ComboBox(typeof(CustomItemsProvider), "MyComboBox")]
        public string DependencyComboBox { get; set; }

        [ListBox]
        public EnumItemsSource_e ListBox { get; set; }

        [OptionBox]
        public EnumItemsSource_e OptionBox { get; set; }

        [Metadata("ItemsDataSource")]
        public string[] ItemsSource { get; set; }

        [ComboBox(ItemsSource = "ItemsDataSource")]
        public string ComboBoxItem { get; set; }

        [Metadata("CustomItemsDataSource")]
        public CustomItem[] CustomItemsSource { get; set; }

        [ListBox(ItemsSource = "CustomItemsDataSource")]
        public List<CustomItem> ListBoxItem { get; set; }

        [ListBox(1, 2, 3, 4, 5)]
        public int StaticListBox { get; set; }

        public ItemSourceControlsGroup() 
        {
            ItemsSource = new string[]
            {
                "Item 1",
                "Item 2",
                "Item 3",
                "Item 4"
            };

            CustomItemsSource = new CustomItem[]
            {
                new CustomItem("Custom Item 1"),
                new CustomItem("Custom Item 2"),
                new CustomItem("Custom Item 3"),
                new CustomItem("Custom Item 4"),
                new CustomItem("Custom Item 5")
            };
        }
    }
}
