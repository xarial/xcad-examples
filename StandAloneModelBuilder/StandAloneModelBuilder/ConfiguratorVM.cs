using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xarial.XToolkit.Wpf;

namespace Xarial.XCad.Examples.Sw.StandAloneModelBuilder
{
    public class ConfiguratorVM : IDisposable
    {
        public event Action<string> DisplayModelPreview;

        public double Height { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public int NumberOfDrawers { get; set; }

        public ICommand GenerateCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand OpenUrlCommand { get; }

        private readonly Configurator m_Configurator;

        private DrawerModel m_CurrentDrawerModel;

        public ConfiguratorVM(Configurator configurator)
        {
            Height = 1200;
            Width = 800;
            Depth = 400;
            NumberOfDrawers = 4;

            m_Configurator = configurator;

            GenerateCommand = new RelayCommand(Generate);
            SaveCommand = new RelayCommand(Save, () => m_CurrentDrawerModel != null);
            OpenUrlCommand = new RelayCommand(OpenUrl);
        }

        private void Generate()
        {
            if (m_CurrentDrawerModel != null) 
            {
                m_CurrentDrawerModel.Dispose();
            }

            m_CurrentDrawerModel = m_Configurator.Generate(Height, Width, Depth, NumberOfDrawers);

            DisplayModelPreview?.Invoke(m_CurrentDrawerModel.FilePath);
        }

        private void Save()
        {
            m_CurrentDrawerModel.SaveAs("");
        }

        private void OpenUrl()
        {
            try
            {
                Process.Start("https://xarial.com/");
            }
            catch
            {
            }
        }

        public void Dispose()
        {
            if (m_CurrentDrawerModel != null)
            {
                m_CurrentDrawerModel.Dispose();
            }
        }
    }
}
