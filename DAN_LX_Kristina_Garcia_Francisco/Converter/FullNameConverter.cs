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
    /// Convertes the id of the manager to the managers first and last name
    /// </summary>
    class FullNameConverter : IValueConverter
    {
        /// <summary>
        /// Converts the parameter value into first and last name
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            UserData userData = new UserData();
            List<tblUser> allUsers = userData.GetAllUsers().ToList();

            if (value != null)
            {
                for (int i = 0; i < allUsers.Count; i++)
                {
                    if (allUsers[i].UserID == (int)value)
                    {
                        return allUsers[i].FirstName + " " + allUsers[i].LastName;
                    }
                }
            }

            return "";
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
