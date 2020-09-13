using XCad.Examples.IssuesManager.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace XCad.Examples.IssuesManager.Converters
{
    public class IssueForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Severity_e)
            {
                switch ((Severity_e)value)
                {
                    case Severity_e.Low:
                        return Brushes.Green;

                    case Severity_e.Medium:
                        return Brushes.Black;

                    case Severity_e.High:
                        return Brushes.DarkRed;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
