using System;
using System.Collections.Generic;
using System.Drawing;
using Xarial.XCad.UI;
using Xarial.XCad.UI.PropertyPage.Attributes;
using Xarial.XCad.UI.PropertyPage.Base;

namespace Xarial.XCad.Examples.PMPage.CSharp.Page.Groups
{
    public class DictionaryControlDescriptor : IControlDescriptor
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public IXImage Icon { get; set; }
        public Type DataType { get; set; }
        public IAttribute[] Attributes { get; set; }

        public object GetValue(object context)
        {
            var dict = context as Dictionary<string, object>;

            if (!dict.TryGetValue(Name, out object val))
            {
                return null;
            }

            return val;
        }

        public void SetValue(object context, object value)
        {
            var dict = context as Dictionary<string, object>;
            dict[Name] = value;
        }
    }

    public class DynamicControlsGroup 
    {
        /// <summary>
        /// This property is defined as the context of the dynamic controls
        /// Context is used to read ad write data while getting and setting the values of the control
        /// In this example the values of the controls will be written to the dictionary
        /// </summary>
        [DynamicControls("DictionaryDynamicControls")]
        public Dictionary<string, object> DynamicControls { get; } = new Dictionary<string, object>();

        /// <summary>
        /// This function will be called on property page creation
        /// and it is passed to <see cref="Extensions.IXExtension.CreatePage{TData}(UI.PropertyPage.Delegates.CreateDynamicControlsDelegate)"/>
        /// as the parameter
        /// </summary>
        /// <param name="tag">Tag of dynamic control</param>
        /// <returns>Collection of control descriptors</returns>
        public IControlDescriptor[] CreateDynamicControls(object tag)
        {
            if (tag.ToString() == "DictionaryDynamicControls")
            {
                return new IControlDescriptor[]
                {
                    new DictionaryControlDescriptor() 
                    {
                        DataType = typeof(string),
                        Name = "Text1",
                        Attributes = new IAttribute[]
                        {
                            new ControlOptionsAttribute(backgroundColor: KnownColor.Blue, textColor: KnownColor.White)
                        }
                    },
                    new DictionaryControlDescriptor()
                    {
                        DataType = typeof(double),
                        Name = "Number1",
                        Description = "Dynamic number box"
                    }
                };
            }
            else 
            {
                return null;
            }
        }
    }
}
