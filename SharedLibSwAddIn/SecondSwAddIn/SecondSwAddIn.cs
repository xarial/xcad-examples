using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Base.Enums;
using Xarial.XCad.SolidWorks;

namespace Xarial.XCad.Examples
{
    [ComVisible(true)]
    [Title("2nd AddIn")]
    [Description("Sample SOLIDWORKS add-in loading second")]
    [Guid("F72EB977-E41F-468D-9F58-3CE70F5A3D93")]
    public class SecondSwAddIn : SwAddInEx
    {
        //static SecondSwAddIn()
        //{
        //    AppDomain.CurrentDomain.AssemblyResolve += (s, a) =>
        //    {
        //        var assmName = new AssemblyName(a.Name);
        //        var assmFilePath = Path.Combine(Path.GetDirectoryName(typeof(SecondSwAddIn).Assembly.Location), assmName.Name + ".dll");

        //        if (File.Exists(assmFilePath))
        //        {
        //            //var candAssmName = AssemblyName.GetAssemblyName(assmFilePath);

        //            //if (candAssmName.Version >= assmName.Version)
        //            //{
        //            return Assembly.LoadFrom(assmFilePath);
        //            //}
        //        }

        //        return null;
        //    };
        //}

        //private readonly Xarial.XToolkit.Reflection.AssemblyReferenceResolver m_AssmRefsResolver;

        //public SecondSwAddIn()
        //{
        //    m_AssmRefsResolver = new Xarial.XToolkit.Reflection.AssemblyReferenceResolver(
        //        AppDomain.CurrentDomain, Path.GetDirectoryName(typeof(SecondSwAddIn).Assembly.Location), "2nd xCAD AddIn");
        //}

        public override void OnConnect()
        {
            try
            {
                ShowMessage();
            }
            catch (Exception ex)
            {
                Application.ShowMessageBox(ex.Message, MessageBoxIcon_e.Error);
            }
        }

        private void ShowMessage()
        {
            Application.ShowMessageBox($"2nd AddIn: {new SharedUtil().GetMessage()}");

            //1.1.0.0
            //Application.ShowMessageBox($"2nd AddIn: {new SharedUtil().GetMessage("User1")}");
        }
    }
}
