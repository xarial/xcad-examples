using System;
using System.Collections.Generic;
using System.Drawing;
using Xarial.XCad.UI;
using Xarial.XCad.UI.PropertyPage.Attributes;
using Xarial.XCad.UI.PropertyPage.Base;

namespace Xarial.XCad.Examples.PMPage.CSharp.Page.Groups
{
    public class DynamicControlsGroup 
    {
        public class DictionaryControlDescriptor : IControlDescriptor
        {
            public string DisplayName { get; set; }
            public string Description { get; set; }
            public string Name { get; set; }
            public IXImage Icon { get; set; }
            public Type DataType { get; set; }
            public UI.PropertyPage.Base.IAttribute[] Attributes { get; set; }

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

        [DynamicControls("DictionaryDynamicControls")]
        public Dictionary<string, object> DynamicControls { get; } = new Dictionary<string, object>();

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
                        Attributes = new UI.PropertyPage.Base.IAttribute[]
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
