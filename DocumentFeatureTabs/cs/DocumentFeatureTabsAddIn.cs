using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.SolidWorks;

namespace Xarial.XCad.Examples.DocumentFeatureTabs
{
    [ComVisible(true)]
    [Guid("3A232F92-4C17-4364-8D0B-E96EE80888AA")]
    public class DocumentFeatureTabsAddIn : SwAddInEx
    {
        public override void OnConnect()
        {
            Application.Documents.RegisterHandler(
                () => new SelectionsDocumentHandler(this));
        }
    }
}
