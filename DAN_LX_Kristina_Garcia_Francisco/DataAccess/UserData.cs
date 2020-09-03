using DAN_LX_Kristina_Garcia_Francisco.Helper;
using DAN_LX_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DAN_LX_Kristina_Garcia_Francisco.DataAccess
{
    /// <summary>
    /// Class that includes all User CRUD functions of the application
    /// </summary>
    class UserData
    {
        /// <summary>
        /// Gets all information about users
        /// </summary>
        /// <returns>a list of found users</returns>
        public List<tblUser> GetAllUsers()
        {
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    List<tblUser> list = new List<tblUser>();
                    list = (from x in context.tblUsers select x).ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Gets all managers that arent the current user
        /// </summary>
        /// <returns>a list of found users</returns>
        public List<tblUser> GetAllManagers(int userID)
        {
            List<tblUser> tblUsers = GetAllUsers();
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    List<tblUser> listButSelected = new List<tblUser>();
                    listButSelected = (from x in context.tblUsers select x).ToList();

                    bool isUser = IsUserID(userID);

                    if (isUser == true)
                    {
                        // find the user before removing them from the list
                        tblUser userToDelete = (from r in context.tblUsers where r.UserID == userID select r).First();

                        listButSelected.Remove(userToDelete);

                    }

                    return listButSelected;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Search if user with that ID exists in the user table
        /// </summary>
        /// <param name="userID">Takes the user id that we want to search for</param>
        /// <returns>True if the user exists</returns>
        public bool IsUserID(int userID)
        {
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    int result = (from x in context.tblUsers where x.UserID == userID select x.UserID).FirstOrDefault();

                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception " + ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// Edits a new user depending if the uderID already exists
        /// </summary>
        /// <param name="user">the user that is being added</param>
        /// <param name="sector">the sector that is being added</param>
        /// <returns>a new user</returns>
        public tblUser AddUser(tblUser user, tblSector sector)
        {
            Validations val = new Validations();

            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    if (user.UserID == 0)
                    {
                        user.DateOfBirth = val.CountDateOfBirth(user.JMBG);

                        tblUser newUser = new tblUser();
                        newUser.FirstName = user.FirstName;
                        newUser.LastName = user.LastName;
                        newUser.JMBG = user.JMBG;
                        newUser.IDCard = user.IDCard;
                        newUser.DateOfBirth = user.DateOfBirth;
                        newUser.Gender = user.Gender;
                        newUser.PhoneNumber = user.PhoneNumber;
                        newUser.SectorID = sector.SectorID;
                        newUser.LocationID = user.LocationID;
                        newUser.ManagerID = user.ManagerID;

                        context.tblUsers.Add(newUser);
                        context.SaveChanges();
                        user.UserID = newUser.UserID;

                        return user;
                    }
                    else
                    {
                        tblUser usersToEdit = (from ss in context.tblUsers where ss.UserID == user.UserID select ss).First();
                        // Get the date of birth
                        user.DateOfBirth = val.CountDateOfBirth(user.JMBG);

                        usersToEdit.FirstName = user.FirstName;
                        usersToEdit.LastName = user.LastName;
                        usersToEdit.JMBG = user.JMBG;
                        usersToEdit.IDCard = user.IDCard;
                        usersToEdit.DateOfBirth = user.DateOfBirth;
                        usersToEdit.Gender = user.Gender;
                        usersToEdit.PhoneNumber = user.PhoneNumber;
                        usersToEdit.SectorID = sector.SectorID;
                        usersToEdit.LocationID = user.LocationID;
                        usersToEdit.ManagerID = user.ManagerID;
                        usersToEdit.UserID = user.UserID;

                        context.SaveChanges();
                        return user;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Deletes user if the uderID exists
        /// </summary>
        /// <param name="userID">the user that is being deleted</param>
        public void DeleteUser(int userID)
        {
            List<tblUser> tblUsers = GetAllUsers();
            try
            {
                using (WorkerContext context = new WorkerContext())
                {
                    bool isUser = IsUserID(userID);

                    if (isUser == true)
                    {
                        // find the user before removing them
                        tblUser userToDelete = (from r in context.tblUsers where r.UserID == userID select r).First();

                        context.tblUsers.Remove(userToDelete);
                        context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Cannot delete the user");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
    }
}
