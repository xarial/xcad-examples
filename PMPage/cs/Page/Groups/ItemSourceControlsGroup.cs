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

        /// <summary>
        /// Override <see cref="ToString"/> method to provide a display name for the item
        /// </summary>
        /// <returns></returns>
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

    /// <summary>
    /// This group demonstrates different options of binding the data source to ComboBox, ListBox and OptionBox controls
    /// </summary>
    public class ItemSourceControlsGroup
    {
        /// <summary>
        /// This ComboBox will use items from the enumeration as items source
        /// <see cref="ControlTagAttribute"/> marks the control so it can be used for dependencies
        /// </summary>
        [ControlTag("MyComboBox")]
        public EnumItemsSource_e ComboBox { get; set; }

        /// <summary>
        /// This ComboBox will use items source defined by <see cref="CustomItemsProvider"/>.
        /// This ComboBox will also depend on the values of <see cref="ComboBox"/> as it is linked via dependency
        /// See <see cref="CustomItemsProvider.ProvideItems(IXApplication, IControl[])"/> for information how the items are created
        /// </summary>
        [ComboBox(typeof(CustomItemsProvider), "MyComboBox")]
        public string DependencyComboBox { get; set; }

        /// <summary>
        /// <see cref="ListBoxAttribute"/> attribute instructs the framework to render this enumeration as ListBoxControl
        /// Selected item will be assigned to the value of the <see cref="ListBox"/> property
        /// </summary>
        [ListBox]
        public EnumItemsSource_e ListBox { get; set; }

        /// <summary>
        /// <see cref="OptionBoxAttribute"/> attribute instructs the framework to render this enumeration as OptionBox control
        /// Selected option will be assigned to the value of the <see cref="OptionBox"/> property
        /// </summary>
        [OptionBox]
        public EnumItemsSource_e OptionBox { get; set; }

        /// <summary>
        /// This property will not be rendered as control, instead it will contain the data source of the items
        /// used in the <see cref="ComboBoxItem"/> ComboBox
        /// </summary>
        [Metadata("ItemsDataSource")]
        public string[] ItemsSource { get; set; }

        /// <summary>
        /// This property will be rendered as ComboBox where items will be read from the <see cref="ItemsSource"/> property
        /// </summary>
        [ComboBox(ItemsSource = "ItemsDataSource")]
        public string ComboBoxItem { get; set; }

        /// <summary>
        /// Custom types can be used as the data source. In this case data source contains the items of the custom <see cref="CustomItem"/> type
        /// </summary>
        [Metadata("CustomItemsDataSource")]
        public CustomItem[] CustomItemsSource { get; set; }

        /// <summary>
        /// This ListBox control will contain the items from <see cref="CustomItemsSource"/> property
        /// As the type of this property is <see cref="List{CustomItem}"/>, the corresponding ListBox will allow multi selection
        /// </summary>
        [ListBox(ItemsSource = "CustomItemsDataSource")]
        public List<CustomItem> ListBoxItem { get; set; }

        /// <summary>
        /// Items source for ListBox and ComboBox can be specified as static values
        /// </summary>
        [ListBox(1, 2, 3, 4, 5)]
        public int StaticListBox { get; set; } = 4;

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
