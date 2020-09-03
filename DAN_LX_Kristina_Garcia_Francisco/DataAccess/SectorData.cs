using DAN_LX_Kristina_Garcia_Francisco.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DAN_LX_Kristina_Garcia_Francisco.DataAccess
{
    /// <summary>
    /// Class that includes all Sector CRUD functions of the application
    /// </summary>
    class SectorData
    {
        /// <summary>
        /// Gets all information about sectors from the database
        /// </summary>
        /// <returns>a list of found sectors</returns>
        public List<tblSector> GetAllSectors()
        {
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    List<tblSector> list = new List<tblSector>();
                    list = (from x in context.tblSectors select x).ToList();
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
        /// Adds a new sector to the database
        /// </summary>
        /// <param name="sector">the sector that is being searched if it exists</param>
        /// <returns>a new sector</returns>
        public tblSector AddSector(tblSector sector)
        {
            try
            {
                using (EmployeeDBEntities context = new EmployeeDBEntities())
                {
                    // Check if that Sector name already exists
                    for (int i = 0; i < GetAllSectors().Count; i++)
                    {
                        if (sector.SectorName == GetAllSectors()[i].SectorName)
                        {
                            sector.SectorName = GetAllSectors()[i].SectorName;
                            sector.SectorID = GetAllSectors()[i].SectorID;
                            return sector;
                        }
                    }

                    if (sector.SectorID == 0)
                    {
                        tblSector newSector = new tblSector
                        {
                            SectorName = sector.SectorName
                        };

                        context.tblSectors.Add(newSector);
                        context.SaveChanges();
                        sector.SectorID = newSector.SectorID;

                        return sector;
                    }
                    else
                    {
                        return sector;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
