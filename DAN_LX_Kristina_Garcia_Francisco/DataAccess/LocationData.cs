using DAN_LX_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace DAN_LX_Kristina_Garcia_Francisco.DataAccess
{
    /// <summary>
    /// Class that includes all Location CRUD functions of the application
    /// </summary>
    class LocationData
    {
        /// <summary>
        /// Folder containing text files
        /// </summary>
        private readonly string locationFolder = @"..\..\TextFile";

        /// <summary>
        /// Gets all information about locations from the database
        /// </summary>
        /// <returns>a list of found locations</returns>
        public List<tblLocation> GetAllLocations()
        {
            List<tblLocation> list = new List<tblLocation>();

            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    list = (from x in context.tblLocations select x).OrderBy(x => x.LocationAddress).ToList();

                    // If the location list is empty
                    if (!list.Any())
                    {
                        ReadLocationFromFile(list);
                    }

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
        /// Gets all location ids from the database
        /// </summary>
        /// <returns>a list of found location ids</returns>
        public List<int> GetAllLocationIDs()
        {
            List<int> list = new List<int>();

            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    list = (from x in context.tblLocations select x).OrderBy(x => x.LocationID).Select(x => x.LocationID).ToList();
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
        /// Reads all locations from the file and adds them to the list
        /// </summary>
        /// <param name="location">list of locations</param>
        public void ReadLocationFromFile(List<tblLocation> location)
        {
            // Create folder if it does not exist
            Directory.CreateDirectory(locationFolder);

            string file = locationFolder + @"\Locations.txt";
            int id = 0;

            using (EmployeeDBEntities context = new EmployeeDBEntities())
            {
                if (File.Exists(file))
                {
                    string[] readFile = File.ReadAllLines(file);

                    for (int i = 0; i < readFile.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(readFile[i]))
                        {
                            string[] trim = readFile[i].Split(',');
                            string address = trim[0];
                            string city = trim[1];
                            string country = trim[2];
                            id++;

                            tblLocation loc = new tblLocation()
                            {
                                LocationID = id,
                                LocationAddress = address,
                                City = city,
                                Country = country
                            };

                            location.Add(loc);

                            context.tblLocations.Add(loc);
                            context.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
