using DAN_LX_Kristina_Garcia_Francisco.DataAccess;
using DAN_LX_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DAN_LX_Kristina_Garcia_Francisco.Helper
{
    /// <summary>
    /// User input validations and preview
    /// </summary>
    class Validations
    {
        /// <summary>
        /// Get all data from the user from the database
        /// </summary>
        UserData userData = new UserData();

        /// <summary>
        /// Checks if the jmbg is correct
        /// </summary>
        /// <param name="jmbg">the jmbg we are checking</param>
        /// <param name="id">for the specific user</param>
        /// <returns></returns>
        public string JMBGChecker(string jmbg, int id)
        {
            List<tblUser> AllUsers = userData.GetAllUsers();
            DateTime dt = default(DateTime);

            string currentJbmg = "";

            if (jmbg == null)
            {
                return "Please enter a number with 13 characters.";
            }

            // Get the current users jmbg
            for (int i = 0; i < AllUsers.Count; i++)
            {
                if (AllUsers[i].UserID == id)
                {
                    currentJbmg = AllUsers[i].JMBG;
                    break;
                }
            }

            // Check if the jmbg already exists, but it is not the current user jmbg
            for (int i = 0; i < AllUsers.Count; i++)
            {
                if (AllUsers[i].JMBG == jmbg && currentJbmg != jmbg)
                {
                    return "This JMBG already exists!";
                }
            }

            if (!(jmbg.Length == 13))
            {
                return "Please enter a number with 13 characters.";
            }

            // Get date
            dt = CountDateOfBirth(jmbg);

            if (dt == default(DateTime))
            {
                return "Incorrect JMBG Format.";
            }

            return null;
        }

        /// <summary>
        /// The input cannot be empty
        /// </summary>
        /// <param name="name">name of the input</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string CannotBeEmpty(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "Cannot be empty";
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Input cannot be shorter than expected
        /// </summary>
        /// <param name="name">name of the input</param>
        /// <param name="number">length of the input</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string TooShort(string name, int number)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < number)
            {
                return "Cannot be shorter than " + number + " characters.";
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// The input cannot be zero
        /// </summary>
        /// <param name="name">name of the input</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string CannotBeZero(int name)
        {
            if (name <= 0)
            {
                return "Cannot be empty";
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Checks if the id card is valid
        /// </summary>
        /// <param name="idCard">name of the input</param>
        /// <param name="id">the users id who has the id card</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string IDCardChecker(string idCard, int id)
        {
            List<tblUser> AllUsers = userData.GetAllUsers();
            string currentIDCard = "";

            if (idCard == null)
            {
                return "Identification Card cannot be empty.";
            }

            // Get the current id card
            for (int i = 0; i < AllUsers.Count; i++)
            {
                if (AllUsers[i].UserID == id)
                {
                    currentIDCard = AllUsers[i].IDCard;
                    break;
                }
            }

            // Check if thelength of the id card is correct
            if (string.IsNullOrWhiteSpace(idCard) || idCard.Length != 9)
            {
                return "The input length has to be 9 characters.";
            }

            // Check if the id card already exists, but it is not the current user jmbg
            for (int i = 0; i < AllUsers.Count; i++)
            {
                if (AllUsers[i].IDCard == idCard && currentIDCard != idCard)
                {
                    return "This ID Card already exists!";
                }
            }

            return null;
        }

        /// <summary>
        /// Checks if the phone is valid
        /// </summary>
        /// <param name="phone">name of the input</param>
        /// <param name="id">the users id who has the id card</param>
        /// <returns>null if the input is correct or string error message if its wrong</returns>
        public string PhoneNumberChecker(string phone, int id)
        {
            List<tblUser> AllUsers = userData.GetAllUsers();
            string currentPhone = "";

            if (phone == null)
            {
                return "Phone Number cannot be empty.";
            }

            // Get the current phone number
            for (int i = 0; i < AllUsers.Count; i++)
            {
                if (AllUsers[i].UserID == id)
                {
                    currentPhone = AllUsers[i].PhoneNumber;
                    break;
                }
            }

            // Check if thelength of the id card is correct
            if (string.IsNullOrWhiteSpace(phone) || phone.Length < 4)
            {
                return "Phone number cannot be shorter than 4 characters";
            }

            // Check if the phone number already exists, but it is not the current phone number
            for (int i = 0; i < AllUsers.Count; i++)
            {
                if (AllUsers[i].PhoneNumber == phone && currentPhone != phone)
                {
                    return "This Phone Number already exists!";
                }
            }

            return null;
        }

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
