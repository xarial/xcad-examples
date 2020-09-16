using System.Collections.Generic;
using System.Linq;
using Xarial.XCad;
using Xarial.XCad.UI.PropertyPage.Base;
using Xarial.XCad.UI.PropertyPage.Services;

namespace CascadingComboBox
{
    public class TypeItemsProvider : CustomItemsProvider<FileItem>
    {
        public override IEnumerable<FileItem> ProvideItems(IXApplication app, IControl[] dependencies)
        {
            var parentFolder = dependencies[0]?.GetValue() as FolderItem;

            if (parentFolder != null)
            {
                return System.IO.Directory.GetFiles(parentFolder.Path)
                    .Select(d => new FileItem(d));
            }
            else
            {
                return Enumerable.Empty<FileItem>();
            }
        }
    }
}
