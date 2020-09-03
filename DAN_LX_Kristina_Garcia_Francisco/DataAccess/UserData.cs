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
        /// Gets all manager ids that arent the current user
        /// </summary>
        /// <returns>a list of found users</returns>
        public List<int> GetAllManagers(int userID)
        {
            List<tblUser> tblUsers = GetAllUsers();
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    List<int> listButSelected = new List<int>();
                    listButSelected = (from x in context.tblUsers select x.UserID).ToList();

                    bool isUser = IsUserID(userID);

                    // Remove the editing user from the list
                    if (isUser == true)
                    {
                        // find the user before removing them from the list
                        int userToDelete = (from r in context.tblUsers where r.UserID == userID select r.UserID).First();

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
        /// <returns>a new user</returns>
        public tblUser AddUser(tblUser user)
        {
            Validations val = new Validations();

            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    if (user.UserID == 0)
                    {
                        user.DateOfBirth = val.CountDateOfBirth(user.JMBG);

                        tblUser newUser = new tblUser
                        {
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            JMBG = user.JMBG,
                            IDCard = user.IDCard,
                            DateOfBirth = user.DateOfBirth,
                            Gender = user.Gender,
                            PhoneNumber = user.PhoneNumber,
                            SectorName = user.SectorName,
                            LocationID = user.LocationID,
                            ManagerID = user.ManagerID
                        };

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
                        usersToEdit.SectorName = user.SectorName;
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
            List<tblUser> allUsers = GetAllUsers();
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    bool isUser = IsUserID(userID);

                    if (isUser == true)
                    {
                        // find the user before removing them
                        tblUser userToDelete = (from r in context.tblUsers where r.UserID == userID select r).First();

                        // Update the manager id for users where we deleted their manager
                        for (int i = 0; i < allUsers.Count; i++)
                        {
                            if (userToDelete.UserID == allUsers[i].ManagerID)
                            {
                                int? managerID = allUsers[i].ManagerID;
                                tblUser usersToEdit = (from ss in context.tblUsers where ss.ManagerID == managerID select ss).First();
                                usersToEdit.ManagerID = null;
                                context.SaveChanges();
                            }
                        }

                        context.tblUsers.Remove(userToDelete);
                        context.SaveChanges();
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
