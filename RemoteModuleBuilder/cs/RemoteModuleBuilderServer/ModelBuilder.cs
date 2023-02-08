using SolidWorks.Interop.sldworks;
using System;
using System.Linq;
using Xarial.XCad.Enums;
using Xarial.XCad.Geometry;
using Xarial.XCad.Geometry.Structures;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.SolidWorks.Enums;

namespace RemoteModuleBuilder
{
    public interface IModelBuilder : IDisposable
    {
        double Build(double width, double height, double length);
    }

    public class SwModelBuilder : IModelBuilder
    {
        private readonly ISwApplication m_App;

        public SwModelBuilder()
        {
            m_App = SwApplicationFactory.Create(SwVersion_e.Sw2021, ApplicationState_e.Safe | ApplicationState_e.Silent);
        }

        public double Build(double width, double height, double length)
        {
            var box = (IXSolidBody)m_App.MemoryGeometryBuilder.CreateSolidBox(
                new Point(0, 0, 0), 
                new Vector(0, 0, 1), 
                new Vector(1, 0, 0), width, length, height).Bodies.First();
            
            return box.Volume;
        }

        public void Dispose()
        {
            m_App.Close();
        }
    }
}
