using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xarial.XToolkit.Wpf;

namespace Xarial.XCad.Examples.SwTaskPaneAddIn.ViewModels
{
    public class TaskPaneVM
    {
        public string Message { get; set; }
        public ICommand OpenLinkCommand { get; }

        public TaskPaneVM() 
        {
            Message = "This example demonstrates how to create Task pane and host WPF control with custom View Model\r\nThis example also contais 2 MSI-installer projects: Windows Installer XML (WiX) and Visual Studio (VSI)";
            OpenLinkCommand = new RelayCommand(OpenLink);
        }

        private void OpenLink()
        {
            try
            {
                Process.Start(@"https://github.com/xarial/xcad-examples/tree/master/TaskPaneAddIn");
            }
            catch 
            {
            }
        }
    }
}
