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
    /// Convertes the id of the sector to the sector name
    /// </summary>
    class SectorConverter : IValueConverter
    {
        /// <summary>
        /// Converts the parameter value into the sector name
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SectorData sectorData = new SectorData();
            List<tblSector> allSectors = sectorData.GetAllSectors().ToList();
            for (int i = 0; i < allSectors.Count; i++)
            {
                if (allSectors[i].SectorID == (int)value)
                {
                    return allSectors[i].SectorName;
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
            throw new NotImplementedException();
        }
    }
}
