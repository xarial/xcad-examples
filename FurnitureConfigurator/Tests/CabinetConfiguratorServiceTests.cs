using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xarial.XCad.Documents;
using Xarial.XCad.SolidWorks;
using XCad.Examples.FurnitureConfigurator.DAL;
using XCad.Examples.FurnitureConfigurator.Enums;
using XCad.Examples.FurnitureConfigurator.Services;

namespace XCad.Examples.FurnitureConfigurator.Tests
{
    public class CabinetConfiguratorServiceTests
    {
        [Test]
        public void ConfigureTest()
        {
            var app = SwApplicationFactory.FromProcess(Process.GetProcessesByName("SLDWORKS").First());
            
            var svc = new CabinetConfiguratorService();
            svc.Configure((IXAssembly)app.Documents.Active, 2.0, 1.0, 0.6, 4, 0.6, HandleType_e.C);
        }
    }
}
