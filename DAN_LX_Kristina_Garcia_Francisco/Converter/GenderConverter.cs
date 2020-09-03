using System;
using System.Windows.Data;

namespace DAN_LX_Kristina_Garcia_Francisco.Converter
{
    /// <summary>
    /// Converts the value of the radio button to the string representing a gender
    /// </summary>
    class GenderConverter : IValueConverter
    {
        /// <summary>
        /// Converts the parameter value into a string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((string)parameter == (string)value);
        }

        /// <summary>
        /// Reverts it back, not implemented
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? parameter : null;
        }
    }
}
