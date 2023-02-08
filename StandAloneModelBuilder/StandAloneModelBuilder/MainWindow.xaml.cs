using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Xarial.XCad.Examples.Sw.StandAloneModelBuilder
{
    public partial class MainWindow : Window
    {
        private readonly ConfiguratorVM m_Vm;

        public MainWindow()
        {
            Dispatcher.UnhandledException += OnUnhandledException;

            InitializeComponent();
            m_Vm = new ConfiguratorVM(new Configurator(SolidWorks.Enums.SwVersion_e.Sw2022));
            m_Vm.DisplayModelPreview += OnDisplayModelPreview;
            this.DataContext = m_Vm;

            this.Closed += OnWindowClosed;
        }

        private void OnDisplayModelPreview(string filePath)
        {
            edrwHostControl.OpenFile(filePath);
        }

        private void OnWindowClosed(object sender, EventArgs e)
        {
            m_Vm.Dispose();
        }

        private void OnUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
        }
    }
}
