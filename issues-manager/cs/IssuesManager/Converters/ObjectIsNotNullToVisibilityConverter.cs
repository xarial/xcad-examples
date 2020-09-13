using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xarial.XToolkit.Wpf.Converters;

namespace XCad.Examples.IssuesManager.Converters
{
    public class ObjectIsNotNullToVisibilityConverter : ObjectIsNotNullUniversalConverter
    {
        public ObjectIsNotNullToVisibilityConverter() 
        {
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }
    }
}
