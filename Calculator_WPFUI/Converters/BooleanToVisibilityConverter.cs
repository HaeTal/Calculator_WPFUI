using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Calculator_WPFUI.Converters
{
    class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            string visible = string.Empty;


            // param : inversion
            if ((string)parameter == "true")
            {
                // Hidden
                visible = (bool)value == false ? "Visible" : "Collapsed";
            }
            else
            {
                visible = (bool)value == false ? "Collapsed" : "Visible";
            }

            return visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
