using SolidWorks.Interop.swconst;
using System.Collections.Generic;
using System.Linq;
using Xarial.XCad;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.UI.PropertyPage.Base;
using Xarial.XCad.UI.PropertyPage.Services;

namespace CascadingComboBox
{
    public class StandardItemsProvider : CustomItemsProvider<FolderItem>
    {
        public override IEnumerable<FolderItem> ProvideItems(IXApplication app, IControl[] dependencies)
        {
            var weldmentProfilesLoc = (app as ISwApplication).Sw.GetUserPreferenceStringValue(
                (int)swUserPreferenceStringValue_e.swFileLocationsWeldmentProfiles)
                .Split(new string[] { System.Environment.NewLine }, System.StringSplitOptions.RemoveEmptyEntries).First();

            return System.IO.Directory.GetDirectories(weldmentProfilesLoc)
                .Select(d => new FolderItem(d));
        }
    }
}
