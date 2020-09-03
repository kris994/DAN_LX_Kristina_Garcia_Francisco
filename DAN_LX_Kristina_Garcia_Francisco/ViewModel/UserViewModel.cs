using DAN_LX_Kristina_Garcia_Francisco.Commands;
using DAN_LX_Kristina_Garcia_Francisco.DataAccess;
using DAN_LX_Kristina_Garcia_Francisco.Helper;
using DAN_LX_Kristina_Garcia_Francisco.Model;
using DAN_LX_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DAN_LX_Kristina_Garcia_Francisco.ViewModel
{
    /// <summary>
    /// User Window
    /// </summary>
    class UserViewModel : BaseViewModel
    {
        #region Global Variables
        readonly UserWindow userWindow;
        UserData userData = new UserData();
        LocationData locationData = new LocationData();
        private readonly BackgroundWorker bgWorker = new BackgroundWorker();
        #endregion

        #region Constuctor
        /// <summary>
        /// Constructor with User Window param
        /// </summary>
        /// <param name="userOpen">opens the user window</param>
        public UserViewModel(UserWindow userOpen)
        {
            userWindow = userOpen;
            UserList = new ObservableCollection<tblUser>(userData.GetAllUsers().ToList());
            locationData.GetAllLocations().ToList();

            bgWorker.DoWork += WorkerOnDoWorkDelete;
        }
        #endregion

        #region Property
        /// <summary>
        /// List of all Users
        /// </summary>
        private ObservableCollection<tblUser> userList;
        public ObservableCollection<tblUser> UserList
        {
            get
            {
                return userList;
            }
            set
            {
                userList = value;
                OnPropertyChanged("UserList");
            }
        }


        /// <summary>
        /// Specific User info
        /// </summary>
        private tblUser user;
        public tblUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }

        /// <summary>
        /// Specific Sector info
        /// </summary>
        private tblSector sector;
        public tblSector Sector
        {
            get
            {
                return sector;
            }
            set
            {
                sector = value;
                OnPropertyChanged("Sector");
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Delete User button
        /// </summary>
        private ICommand deleteUser;
        public ICommand DeleteUser
        {
            get
            {
                if (deleteUser == null)
                {
                    deleteUser = new RelayCommand(param => DeleteUserExecute(), param => CanDeleteUserExecute());
                }
                return deleteUser;
            }
        }

        /// <summary>
        /// Executes the delete command
        /// </summary>
        public void DeleteUserExecute()
        {
            // Checks if the user really wants to delete the selected User
            var result = Xceed.Wpf.Toolkit.MessageBox.Show("Are you sure you want to delete the user?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (User != null)
                    {
                        if (!bgWorker.IsBusy)
                        {
                            // This method will start the execution asynchronously in the background
                            bgWorker.RunWorkerAsync();
                        }

                        int userID = User.UserID;
                        userData.DeleteUser(userID);
                        UserList.Remove(User);
                        UserList = new ObservableCollection<tblUser>(userData.GetAllUsers().ToList());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Can only execute this command if the User list has the users data
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanDeleteUserExecute()
        {
            if (UserList == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Edit User Button
        /// </summary>
        private ICommand editUser;
        public ICommand EditUser
        {
            get
            {
                if (editUser == null)
                {
                    editUser = new RelayCommand(param => EditUserExecute(), param => CanEditUserExecute());
                }
                return editUser;
            }
        }

        /// <summary>
        /// Executes the edit command
        /// </summary>
        public void EditUserExecute()
        {
            try
            {
                if (User != null)
                {
                    AddUserWindow addUser = new AddUserWindow(User);
                    addUser.ShowDialog();
                    UserList = new ObservableCollection<tblUser>(userData.GetAllUsers().ToList());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Can only execute this command if the User list has the users data
        /// </summary>
        /// <returns>true if possible</returns>
        public bool CanEditUserExecute()
        {
            if (UserList == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Add User Button
        /// </summary>
        private ICommand addUser;
        public ICommand AddUser
        {
            get
            {
                if (addUser == null)
                {
                    addUser = new RelayCommand(param => AddUserExecute(), param => CanAddUserExecute());
                }
                return addUser;
            }
        }

        /// <summary>
        /// Executes the add user command
        /// </summary>
        private void AddUserExecute()
        {
            try
            {
                AddUserWindow addUser = new AddUserWindow();
                addUser.ShowDialog();
                UserList = new ObservableCollection<tblUser>(userData.GetAllUsers().ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if its possible to add the new User
        /// </summary>
        /// <returns>true</returns>
        private bool CanAddUserExecute()
        {
            return true;
        }
        #endregion

        #region Background Worker
        /// <summary>
        /// Writes the message to the log file.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">DoWorkEventArgs e</param>
        public void WorkerOnDoWorkDelete(object sender, DoWorkEventArgs e)
        {
            LogMessage log = new LogMessage();
            string message = log.Message("Deleted", User.FirstName, User.LastName);

            log.LogFile(message);
        }
        #endregion
    }
}
