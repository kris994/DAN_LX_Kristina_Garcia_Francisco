using DAN_LX_Kristina_Garcia_Francisco.Commands;
using DAN_LX_Kristina_Garcia_Francisco.DataAccess;
using DAN_LX_Kristina_Garcia_Francisco.Helper;
using DAN_LX_Kristina_Garcia_Francisco.Model;
using DAN_LX_Kristina_Garcia_Francisco.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DAN_LX_Kristina_Garcia_Francisco.ViewModel
{
    /// <summary>
    /// Add User Window
    /// </summary>
    class AddUserViewModel : BaseViewModel
    {
        #region Global VariablesC:\Users\krist\source\repos\DAN_LX_Kristina_Garcia_Francisco\DAN_LX_Kristina_Garcia_Francisco\ViewModel\AddUserViewModel.cs
        readonly AddUserWindow addUserWindow;
        UserData userData = new UserData();
        LocationData locationData = new LocationData();
        SectorData sectorData = new SectorData();
        private readonly BackgroundWorker bgWorker = new BackgroundWorker();
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor with edit user window opening
        /// </summary>
        /// <param name="addUserOpen">opens the add user window</param>
        /// <param name="userEdit">gets the user info that is being edited</param>
        public AddUserViewModel(AddUserWindow addUserOpen, tblUser userEdit)
        {
            user = userEdit;
            sector = new tblSector();
            addUserWindow = addUserOpen;
            LocationList = locationData.GetAllLocations().ToList();
            SectorList = sectorData.GetAllSectors().ToList();
            ManagerList = userData.GetAllManagers(User.UserID).ToList();
            LocationIDList = locationData.GetAllLocationIDs().ToList();
            bgWorker.DoWork += WorkerOnDoWorkUpdate;
        }

        /// <summary>
        /// Constructor with Add User param
        /// </summary>
        /// <param name="addUserOpen">opens the add user window</param>
        public AddUserViewModel(AddUserWindow addUserOpen)
        {
            user = new tblUser();
            sector = new tblSector();
            addUserWindow = addUserOpen;
            LocationList = locationData.GetAllLocations().ToList();
            SectorList = sectorData.GetAllSectors().ToList();
            ManagerList = userData.GetAllManagers(User.UserID).ToList();
            LocationIDList = locationData.GetAllLocationIDs().ToList();
            bgWorker.DoWork += WorkerOnDoWorkCreate;
        }
        #endregion

        #region Property
        /// <summary>
        /// Information about the specific user
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
        /// Information about the specific manager
        /// </summary>
        private tblUser manager;
        public tblUser Manager
        {
            get
            {
                return manager;
            }
            set
            {
                manager = value;
                OnPropertyChanged("Manager");
            }
        }
      
        /// <summary>
        /// Information about the specific location
        /// </summary>
        private tblLocation location;
        public tblLocation Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                OnPropertyChanged("Location");
            }
        }

        /// <summary>
        /// List of all locations
        /// </summary>
        private List<tblLocation> locationList;
        public List<tblLocation> LocationList
        {
            get
            {
                return locationList;
            }
            set
            {
                locationList = value;
                OnPropertyChanged("LocationList");
            }
        }

        /// <summary>
        /// List of all location ids
        /// </summary>
        private List<int> locationIDList;
        public List<int> LocationIDList
        {
            get
            {
                return locationIDList;
            }
            set
            {
                locationIDList = value;
                OnPropertyChanged("LocationIDList");
            }
        }

        /// <summary>
        /// Information about the specific sector
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

        /// <summary>
        /// List of all sectors
        /// </summary>
        private List<tblSector> sectorList;
        public List<tblSector> SectorList
        {
            get
            {
                return sectorList;
            }
            set
            {
                sectorList = value;
                OnPropertyChanged("SectorList");
            }
        }

        /// <summary>
        /// List of all managers
        /// </summary>
        private List<int> managerList;
        public List<int> ManagerList
        {
            get
            {
                return managerList;
            }
            set
            {
                managerList = value;
                OnPropertyChanged("ManagerList");
            }
        }
        #endregion
        
        #region Commands
        /// <summary>
        /// Save Button
        /// </summary>
        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => this.CanSaveExecute);
                }
                return save;
            }
        }

        /// <summary>
        /// Executes the save command
        /// </summary>
        private void SaveExecute()
        {
            try
            {
                // Creating the sector
                tblSector sec = new tblSector
                {
                    SectorName = User.SectorName,
                };
                sectorData.AddSector(sec);
                userData.AddUser(User);

                if (!bgWorker.IsBusy)
                {
                    // This method will start the execution asynchronously in the background
                    bgWorker.RunWorkerAsync();
                }

                addUserWindow.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Can only execute the field are filled up
        /// </summary>
        /// <returns>true if possible</returns>
        private bool CanSaveExecute
        {
            get
            {
                return User.IsValid;
            }
        }

        /// <summary>
        /// Command that tries to close the user window
        /// </summary>
        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }

        /// <summary>
        /// Executes the close command
        /// </summary>
        private void CloseExecute()
        {
            var result = Xceed.Wpf.Toolkit.MessageBox.Show("Are you sure you want to exit?\nAll Data will be lost.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    addUserWindow.Close();
                }
                catch (Exception ex)
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Checks if its possible to close the window
        /// </summary>
        /// <returns>true</returns>
        private bool CanCloseExecute()
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
        public void WorkerOnDoWorkUpdate(object sender, DoWorkEventArgs e)
        {
            LogMessage log = new LogMessage();
            string message = log.Message("Update", User.FirstName, User.LastName);

            log.LogFile(message);
        }

        /// <summary>
        /// Writes the message to the log file.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">DoWorkEventArgs e</param>
        public void WorkerOnDoWorkCreate(object sender, DoWorkEventArgs e)
        {
            LogMessage log = new LogMessage();
            string message = log.Message("Create", User.FirstName, User.LastName);

            log.LogFile(message);
        }
        #endregion
    }
}
