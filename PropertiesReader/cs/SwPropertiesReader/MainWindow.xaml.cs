using PropertiesReader;
using PropertiesReader.ViewModels;
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
using Xarial.XCad.SolidWorks;

namespace SwPropertiesReader
{
    public partial class MainWindow : Window
    {
        private readonly PropertiesReaderService m_PrpsReader;

        public MainWindow()
        {
            InitializeComponent();

            var app = SwApplicationFactory.Create();
            m_PrpsReader = new PropertiesReaderService(app);
            var vm = new PropertiesTableVM(m_PrpsReader);
            this.DataContext = vm;
        }
        
        protected override void OnClosed(EventArgs e)
        {
            m_PrpsReader.Dispose();
        }
    }
}
