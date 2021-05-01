using System.Runtime.InteropServices;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.SolidWorks.UI.PropertyPage;
using Xarial.XCad.UI.PropertyPage.Attributes;

namespace CascadingComboBox
{
    [ComVisible(true)]
    [Title("Weldment Profiles")]
    public class PMPData : SwPropertyManagerPageHandler
    {
        [ComboBox(typeof(StandardItemsProvider))]
        [ControlTag(nameof(Standard))]
        public FolderItem Standard { get; set; }

        [ComboBox(typeof(TypeItemsProvider), nameof(Standard))]
        [ControlTag(nameof(Type))]
        public FileItem Type { get; set; }

        [ComboBox(typeof(SizeItemsProvider), nameof(Type))]
        public ConfigurationItem Size { get; set; }
    }
}
