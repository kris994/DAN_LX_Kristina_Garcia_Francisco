using DAN_LX_Kristina_Garcia_Francisco.DataAccess;
using DAN_LX_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace DAN_LX_Kristina_Garcia_Francisco.Converter
{
    /// <summary>
    /// Convertes the id of the location to the location address, city and country
    /// </summary>
    class LocationConverter : IValueConverter
    {
        /// <summary>
        /// Converts the parameter value into the location address, city and country
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            LocationData locData = new LocationData();
            List<tblLocation> allLocations = locData.GetAllLocations().ToList();

            for (int i = 0; i < allLocations.Count; i++)
            {
                if (allLocations[i].LocationID == (int)value)
                {
                    string name = allLocations[i].LocationAddress + ", " +
                        allLocations[i].City + ", " + allLocations[i].Country;
                    return name;
                }
            }

            return value;
        }

        /// <summary>
        /// Converts back
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
