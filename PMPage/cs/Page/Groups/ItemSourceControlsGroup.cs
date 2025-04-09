using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.UI.PropertyPage.Attributes;
using Xarial.XCad.UI.PropertyPage.Base;
using Xarial.XCad.UI.PropertyPage.Services;

namespace Xarial.XCad.Examples.PMPage.CSharp.Page.Groups
{
    public class CustomItem
    {
        public string Name { get; }
        public int Id { get; }

        public CustomItem(string name, int id)
        {
            Name = name;
            Id = id;
        }

        /// <summary>
        /// Override <see cref="ToString"/> method to provide a display name for the item
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Name;
    }

    [DisplayName("Custom Attributed Item")]
    [Description("Custom item with predefined description")]
    public class AttributedCustomItem : CustomItem
    {
        public AttributedCustomItem(int id) : base("Custom Attributed Item", id)
        {
        }
    }

    /// <summary>
    /// Items provider which provides dynamic values based on the dependency
    /// </summary>
    public class CustomDynamicItemsProvider : ICustomItemsProvider
    {
        public IEnumerable<object> ProvideItems(IXApplication app, IControl ctrl, IControl[] dependencies, object parameter)
        {
            var depVal = dependencies.First()?.GetValue()?.ToString();

            return new string[]
            {
                depVal + "_A" + parameter,
                depVal + "_B" + parameter,
                depVal + "_C" + parameter
            };
        }
    }

    /// <summary>
    /// Custom items provider which provides predefined custom items
    /// </summary>
    public class CustomStaticItemsProvider : ICustomItemsProvider
    {
        private static readonly CustomItem[] m_CustomItems = new CustomItem[]
        {
            new CustomItem("Item A", 1),
            new CustomItem("Item B", 2),
            new CustomItem("Item C", 3),
            new AttributedCustomItem(4)
        };

        public IEnumerable<object> ProvideItems(IXApplication app, IControl ctrl, IControl[] dependencies, object parameter)
            => m_CustomItems;
    }

    public enum EnumItemsSource_e
    {
        Value1,
        Value2,
        Value3
    }

    [Flags]
    public enum FlagEnumItemsSource_e 
    {
        None = 0,
        Item1 = 1,
        Item2 = 2,

        [Title("Item1 + Item2")]
        [Description("Combined Item1 and Item2")]
        Item1_2 =  Item1 | Item2,

        Item3 = 4,
        Item4 = 8
    }

    /// <summary>
    /// This group demonstrates different options of binding the data source to ComboBox, ListBox and OptionBox controls
    /// </summary>
    public class ItemSourceControlsGroup : INotifyPropertyChanged
    {
        private class CustomItemEqualityComparer : IEqualityComparer<CustomItem>
        {
            public bool Equals(CustomItem x, CustomItem y) => x?.Id == y?.Id;

            public int GetHashCode(CustomItem obj) => 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// This ComboBox will use items from the enumeration as items source
        /// <see cref="ControlTagAttribute"/> marks the control so it can be used for dependencies
        /// </summary>
        [ControlTag(nameof(ComboBox))]
        public EnumItemsSource_e ComboBox { get; set; }

        /// <summary>
        /// This ComboBox will use items source defined by <see cref="CustomDynamicItemsProvider"/>.
        /// This ComboBox will also depend on the values of <see cref="ComboBox"/> as it is linked via dependency
        /// Value of <see cref="ItemsSourceControlAttribute.Parameter"/> is appended to all items
        /// See <see cref="CustomDynamicItemsProvider.ProvideItems(IXApplication, IControl[])"/> for information how the items are created
        /// </summary>
        [ComboBox(typeof(CustomDynamicItemsProvider), nameof(ComboBox), Parameter = "*")]
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
        [Metadata(nameof(ItemsSource))]
        public string[] ItemsSource { get; set; }

        /// <summary>
        /// This property will be rendered as ComboBox where items will be read from the <see cref="ItemsSource"/> property
        /// </summary>
        [ComboBox(ItemsSource = nameof(ItemsSource))]
        public string ComboBoxItem { get; set; }

        /// <summary>
        /// Custom types can be used as the data source. In this case data source contains the items of the custom <see cref="CustomItem"/> type
        /// </summary>
        [Metadata(nameof(CustomItemsSource))]
        public CustomItem[] CustomItemsSource { get; set; }

        /// <summary>
        /// This ListBox control will contain the items from <see cref="CustomItemsSource"/> property
        /// As the type of this property is <see cref="List{CustomItem}"/>, the corresponding ListBox will allow multi selection
        /// </summary>
        [ListBox(ItemsSource = nameof(CustomItemsSource))]
        public List<CustomItem> ListBoxItem { get; set; }

        /// <summary>
        /// This ListBox control will contain the items from <see cref="CustomItemsSource"/> property
        /// This is a single selection list box control and the selection will be persistent once the <see cref="ReloadCustomItemsSourceAction"/> button is pressed
        /// </summary>
        [ListBox(ItemsSource = nameof(CustomItemsSource), EqualityComparer = typeof(CustomItemEqualityComparer))]
        public CustomItem ListBoxItemPersistent { get; set; }

        /// <summary>
        /// Items source for ListBox, ComboBox, OptionBox, CheckBoxList can be specified as static values
        /// </summary>
        [ListBox(1, 2, 3, 4, 5)]
        public int StaticListBox { get; set; } = 4;

        /// <summary>
        /// Items source for ListBox, ComboBox, OptionBox, CheckBoxList can be specified as static values
        /// </summary>
        [ComboBox(1, 2, 3, 4, 5)]
        public int StaticComboBox { get; set; } = 2;

        [CheckBoxList]
        public FlagEnumItemsSource_e FlagEnumCheckBoxList { get; set; }

        /// <summary>
        /// Option box with the items provider
        /// </summary>
        [OptionBox(typeof(CustomStaticItemsProvider))]
        public CustomItem StaticOptionBox { get; set; }

        /// <summary>
        /// Clicking this button will reload custom items
        /// <see cref="ListBoxItem"/> will lose the current selection as new objects are selected,
        /// while <see cref="ListBoxItemPersistent"/> will keep the selection as custom <see cref="ItemsSourceControlAttribute.EqualityComparer"/> is specified
        /// </summary>
        [Title("Reload Custom Items Source")]
        public Action ReloadCustomItemsSourceAction { get; }

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
                new CustomItem("Custom Item 1", 1),
                new CustomItem("Custom Item 2", 2),
                new CustomItem("Custom Item 3", 3),
                new CustomItem("Custom Item 4", 4),
                new CustomItem("Custom Item 5", 5)
            };

            var customItemIndex = 0;

            ReloadCustomItemsSourceAction = () =>
            {
                customItemIndex++;

                CustomItemsSource = new CustomItem[]
                {
                    new CustomItem($"Custom Item 1-{customItemIndex}", 1),
                    new CustomItem($"Custom Item 2-{customItemIndex}", 2),
                    new CustomItem($"Custom Item 3-{customItemIndex}", 3),
                    new CustomItem($"Custom Item 4-{customItemIndex}", 4),
                    new CustomItem($"Custom Item 5-{customItemIndex}", 5)
                };

                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomItemsSource)));
            };
        }
    }
}
