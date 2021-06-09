using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Xarial.XToolkit.Wpf;
using Xarial.XToolkit.Wpf.Utils;

namespace PropertiesReader.ViewModels
{
    public class PropertiesTableVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DataTable m_PropertiesTable;
        
        private readonly PropertiesReaderService m_PrpsReader;

        public ICommand BrowseAssemblyFileCommand { get; }

        public DataTable PropertiesTable 
        {
            get => m_PropertiesTable;
            set 
            {
                m_PropertiesTable = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PropertiesTable)));
            }
        }       

        public PropertiesTableVM(PropertiesReaderService prpsReader) 
        {
            m_PrpsReader = prpsReader;
            BrowseAssemblyFileCommand = new RelayCommand(OnBrowseAssemblyFile);
        }

        private void OnBrowseAssemblyFile() 
        {
            if (FileSystemBrowser.BrowseFileOpen(out string path, "Select assembly",
                FileSystemBrowser.BuildFilterString(new FileFilter("SOLIDWORKS Assembly File", "*.sldasm"), FileFilter.AllFiles))) 
            {
                try
                {
                    PropertiesTable = m_PrpsReader.ReadProperties(path);
                }
                catch 
                {
                    MessageBox.Show("Failed to read properties", "Properties Reader", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
