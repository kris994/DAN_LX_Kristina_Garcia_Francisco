using System;
using System.Globalization;

namespace DAN_LX_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    /// User input validations and preview
    /// </summary>
    class Validations
    {
        /// <summary>
        /// Calculates the date of birth for the given jmbg
        /// </summary>
        /// <param name="jmbg">given jmbg</param>
        /// <returns>the date of birth</returns>
        public DateTime CountDateOfBirth(string jmbg)
        {
            DateTime dt = default(DateTime);

            // Get the date of birth
            if (jmbg[4] == '0')
            {
                string date = jmbg.Substring(0, 2) + "/" + jmbg.Substring(2, 2) + "/" + "2" + jmbg.Substring(4, 3);
                try
                {
                    dt = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    return dt;
                }
                catch (FormatException)
                {
                    dt = default(DateTime);
                    return dt;
                }
            }
            if (jmbg[4] == '9')
            {
                string date = jmbg.Substring(0, 2) + "/" + jmbg.Substring(2, 2) + "/" + "1" + jmbg.Substring(4, 3);
                try
                {
                    dt = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    return dt;
                }
                catch (FormatException)
                {
                    dt = default(DateTime);
                    return dt;
                }
            }
            return dt;
        }
    }
}
