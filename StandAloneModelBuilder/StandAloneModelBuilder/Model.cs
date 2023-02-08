using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xarial.XCad.Examples.Sw.StandAloneModelBuilder
{
    public class DrawerModel : IDisposable
    {
        public string FilePath { get; }

        internal DrawerModel(string filePath) 
        {
            FilePath = filePath;
        }

        public void SaveAs(string filePath) 
        {
            File.Copy(FilePath, filePath);
        }

        public void Dispose()
        {
            try
            {
                File.Delete(FilePath);
            }
            catch 
            {
            }
        }
    }
}
