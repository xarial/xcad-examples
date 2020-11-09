using System.Collections.Generic;
using System.Linq;
using Xarial.XCad;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.UI.PropertyPage.Base;
using Xarial.XCad.UI.PropertyPage.Services;

namespace CascadingComboBox
{
    public class SizeItemsProvider : CustomItemsProvider<ConfigurationItem>
    {
        public override IEnumerable<ConfigurationItem> ProvideItems(IXApplication app, IControl[] dependencies)
        {
            var parentFile = dependencies[0]?.GetValue() as FileItem;

            if (parentFile != null)
            {
                return ((app as ISwApplication).Sw.GetConfigurationNames(parentFile.Path) as string[])
                    .Select(c => new ConfigurationItem(c));
            }
            else
            {
                return Enumerable.Empty<ConfigurationItem>();
            }
        }
    }
}
