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
    [Title("1st AddIn")]
    [Description("Sample SOLIDWORKS add-in loading first")]
    [Guid("89367F8C-EAC0-4144-9940-801CC8A10000")]
    public class FirstSwAddIn : SwAddInEx
    {
        //static FirstSwAddIn()
        //{
        //    AppDomain.CurrentDomain.AssemblyResolve += (s, a) =>
        //    {
        //        var assmName = new AssemblyName(a.Name);
        //        var assmFilePath = Path.Combine(Path.GetDirectoryName(typeof(FirstSwAddIn).Assembly.Location), assmName.Name + ".dll");

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

        //public FirstSwAddIn() 
        //{
        //    m_AssmRefsResolver = new Xarial.XToolkit.Reflection.AssemblyReferenceResolver(
        //        AppDomain.CurrentDomain, Path.GetDirectoryName(typeof(FirstSwAddIn).Assembly.Location), "1st xCAD AddIn");
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
            Application.ShowMessageBox($"1st AddIn: {new SharedUtil().GetMessage()}");

            //1.1.0.0
            //Application.ShowMessageBox($"1st AddIn: {new SharedUtil().GetMessage("User1")}");
        }

        //public override void OnDisconnect()
        //{
        //    m_AssmRefsResolver.Dispose();
        //}
    }
}
