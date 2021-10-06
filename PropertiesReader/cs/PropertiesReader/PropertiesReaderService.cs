using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad;
using Xarial.XCad.Base;
using Xarial.XCad.Documents;

namespace PropertiesReader
{
    public class PropertiesReaderService : IDisposable
    {
        private readonly IXApplication m_App;

        public PropertiesReaderService(IXApplication app) 
        {
            m_App = app;
        }

        public DataTable ReadProperties(string assmFilePath) 
        {
            const string FILE_PATH_COLUMN_NAME = "File Path";

            var prpsTable = new DataTable();
            prpsTable.Columns.Add(FILE_PATH_COLUMN_NAME);

            var assm = (IXAssembly)m_App.Documents.Open(assmFilePath, Xarial.XCad.Documents.Enums.DocumentState_e.ReadOnly);

            var refDocs = assm.Configurations.Active.Components.Flatten()
                .GroupBy(x =>
                {
                    try
                    {
                        return x.Path;
                    }
                    catch
                    {
                        return "";
                    }
                }).Where(x => !string.IsNullOrEmpty(x.Key))
                .Select(x => x.First().ReferencedDocument)
                .Where(x => x.IsCommitted).ToArray();

            foreach (var refDoc in refDocs) 
            {
                var row = prpsTable.NewRow();
                row[FILE_PATH_COLUMN_NAME] = refDoc.Path;

                foreach (var prp in refDoc.Properties) 
                {
                    var col = prpsTable.Columns[prp.Name];

                    if (col == null) 
                    {
                        col = prpsTable.Columns.Add(prp.Name);
                    }

                    row[prp.Name] = prp.Value;
                }

                prpsTable.Rows.Add(row);
            }

            return prpsTable;
        }

        public void Dispose()
        {
            m_App.Close();
        }
    }
}
