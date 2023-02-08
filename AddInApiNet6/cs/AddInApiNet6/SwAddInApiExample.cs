using System.Runtime.InteropServices;
using Xarial.XCad.Base.Attributes;
using Xarial.XCad.Examples.Properties;
using Xarial.XCad.SolidWorks;
using Xarial.XCad.UI.Commands;

namespace Xarial.XCad.Examples
{
    [ComVisible(true)]
    [Guid("53FDB19A-D7B8-459C-9525-B52CF00FEEE4")]
    public interface ISwAddInApiExampleApi 
    {
        void SayHello(string name);
    }

    [ComVisible(true)]
    [Guid("557BB880-4F74-43C3-8244-60AEF26CB5F2")]
    [Title("SW AddIn API Example")]
    public class SwAddInApiExample : SwAddInEx, ISwAddInApiExampleApi
    {
        [Title("SW AddIn API Example")]
        public enum Commands_e 
        {
            [Title("Say Hello")]
            [Icon(typeof(Resources), nameof(Resources.hello_icon))]
            SayHello
        }

        public override void OnConnect()
        {
            CommandManager.AddCommandGroup<Commands_e>().CommandClick += OnCommandClick;
        }

        private void OnCommandClick(Commands_e spec)
        {
            switch (spec) 
            {
                case Commands_e.SayHello:
                    SayHello(Environment.UserName);
                    break;
            }
        }

        public void SayHello(string name)
        {
            Application.ShowMessageBox($"Hello {name}, From xCAD.NET SOLIDWORKS add-in in .NET 6");
        }
    }
}