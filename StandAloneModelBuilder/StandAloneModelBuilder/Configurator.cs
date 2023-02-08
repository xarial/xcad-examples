using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Xarial.XCad.Documents;
using Xarial.XCad.Documents.Enums;
using Xarial.XCad.Documents.Extensions;
using Xarial.XCad.Enums;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.Enums;

namespace Xarial.XCad.Examples.Sw.StandAloneModelBuilder
{
    public class Configurator
    {
        private const string HEIGHT_DIM_NAME = "Height@Boss-Extrude1";
        private const string DEPTH_DIM_NAME = "Depth@Sketch1";
        private const string WIDTH_DIM_NAME = "Width@Sketch1";
        private const string NO_DRAWERS_DIM_NAME = "D1@LPattern1";

        private readonly SwVersion_e? m_SwVers;
        private ApplicationState_e m_State;

        private readonly string m_TemplateFilePath;

        public Configurator(SwVersion_e? swVers = null, 
            ApplicationState_e state = ApplicationState_e.Safe | ApplicationState_e.Background | ApplicationState_e.Silent) 
        {
            m_SwVers = swVers;
            m_State = state;

            m_TemplateFilePath = Path.Combine(Path.GetDirectoryName(typeof(Configurator).Assembly.Location), "ChestOfDrawers.SLDPRT");
        }

        public DrawerModel Generate(double height, double width, double depth, int numberOfDrawers) 
        {
            DrawerModel res;

            using (var app = SwApplicationFactory.Create(m_SwVers, m_State)) 
            {
                using (var doc = app.Documents.Open(m_TemplateFilePath, DocumentState_e.ReadOnly))
                {
                    doc.Dimensions[HEIGHT_DIM_NAME].Value = doc.Units.ConvertLengthToSystemValue(height);
                    doc.Dimensions[WIDTH_DIM_NAME].Value = doc.Units.ConvertLengthToSystemValue(width);
                    doc.Dimensions[DEPTH_DIM_NAME].Value = doc.Units.ConvertLengthToSystemValue(depth);
                    doc.Dimensions[NO_DRAWERS_DIM_NAME].Value = numberOfDrawers;

                    doc.Rebuild();

                    var tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".sldprt");

                    doc.SaveAs(tempFilePath);

                    res = new DrawerModel(tempFilePath);
                }

                //TODO: remove this and made the App disposabel in xCAD.NET
                app.Close();
            }

            return res;
        }
    }
}
