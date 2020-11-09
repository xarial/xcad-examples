using System;
using System.IO;
using System.Runtime.InteropServices;
using Xarial.XCad;
using Xarial.XCad.Data;
using Xarial.XCad.Documents;
using Xarial.XCad.Documents.Enums;
using Xarial.XCad.Documents.Services;
using Xarial.XCad.Documents.Structures;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.Documents;

namespace PropertyAsFileName
{
    public class SaveDocHandler : IDocumentHandler
    {
        private const string FILE_NAME_PRP = "Title";

        private IXDocument m_Model;

        public void Init(IXApplication app, IXDocument model)
        {
            m_Model = model;
            m_Model.Saving += OnModelSaving;
        }

        private void OnModelSaving(IXDocument doc, DocumentSaveType_e type, DocumentSaveArgs args)
        {
            if (type == DocumentSaveType_e.SaveAs) 
            {
                var tempDir = Path.GetTempPath();
                var ext = Path.GetExtension(args.FileName);

                IXProperty titlePrp = null;

                if (doc is ISwDocument3D)
                {
                    titlePrp = (doc as ISwDocument3D).Configurations.Active.Properties.GetOrPreCreate(FILE_NAME_PRP);
                }

                if (titlePrp == null || !titlePrp.Exists())
                {
                    titlePrp = doc.Properties.GetOrPreCreate(FILE_NAME_PRP);
                }

                var prpVal = "";

                if (titlePrp.Exists())
                {
                    prpVal = titlePrp.Value?.ToString();
                }

                if (string.IsNullOrEmpty(prpVal))
                {
                    prpVal = Guid.NewGuid().ToString();
                }

                var destFile = Path.Combine(tempDir, prpVal + ext);

                args.FileName = destFile;
            }
        }

        public void Dispose()
        {
            m_Model.Saving -= OnModelSaving;
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
