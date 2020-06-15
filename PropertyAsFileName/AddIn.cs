using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad;
using Xarial.XCad.Data;
using Xarial.XCad.Documents;
using Xarial.XCad.Documents.Services;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.Data;
using Xarial.XCad.SolidWorks.Documents;
using Xarial.XCad.SolidWorks.Documents.Services;
using Xarial.XCad.Toolkit.Services;

namespace PropertyAsFileName
{
    public class SaveDocHandler : SwDocumentHandler
    {
        private const string FILE_NAME_PRP = "Title";

        protected override void AttachAssemblyEvents(AssemblyDoc assm)
        {
            assm.FileSaveAsNotify2 += OnFileSaveAsNotify2;
        }

        protected override void AttachDrawingEvents(DrawingDoc drw)
        {
            drw.FileSaveAsNotify2 += OnFileSaveAsNotify2;
        }

        protected override void AttachPartEvents(PartDoc part)
        {
            part.FileSaveAsNotify2 += OnFileSaveAsNotify2;
        }

        private int OnFileSaveAsNotify2(string fileName)
        {
            var tempDir = Path.GetTempPath();
            var ext = Path.GetExtension(fileName);

            IXProperty titlePrp = null;

            if (Document is SwDocument3D) 
            {
                titlePrp = (Document as SwDocument3D).Configurations.Active.Properties.GetOrPreCreate(FILE_NAME_PRP);
            }

            if (titlePrp == null || !titlePrp.Exists) 
            {
                titlePrp = Document.Properties.GetOrPreCreate(FILE_NAME_PRP);
            }

            var prpVal = "";

            if (titlePrp.Exists) 
            {
                prpVal = titlePrp.Value?.ToString();
            }
            
            if (string.IsNullOrEmpty(prpVal)) 
            {
                prpVal = Guid.NewGuid().ToString();
            }

            var destFile = Path.Combine(tempDir, prpVal + ext);

            Model.SetSaveAsFileName(destFile);

            return S_FAIL;
        }

        protected override void DetachAssemblyEvents(AssemblyDoc assm)
        {
            assm.FileSaveAsNotify2 -= OnFileSaveAsNotify2;
        }

        protected override void DetachDrawingEvents(DrawingDoc drw)
        {
            drw.FileSaveAsNotify2 -= OnFileSaveAsNotify2;
        }

        protected override void DetachPartEvents(PartDoc part)
        {
            part.FileSaveAsNotify2 -= OnFileSaveAsNotify2;
        }
    }

    [ComVisible(true)]
    public class AddIn : SwAddInEx
    {
        public override void OnConnect()
        {
            Application.Documents.RegisterHandler<SaveDocHandler>();
        }
    }
}
