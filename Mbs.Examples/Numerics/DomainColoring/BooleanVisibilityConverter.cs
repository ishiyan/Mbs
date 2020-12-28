using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DomainColoring
{
    /// <summary>
    /// Converts a boolean to the <c>Visibility</c>.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts a boolean to the <c>Visibility</c>.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>The <c>Visibility</c>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool b;
            if (!bool.TryParse(value?.ToString(), out b))
            {
                b = true;
            }

            return b ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Intentionally not implemented.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
