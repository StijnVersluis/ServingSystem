using InterfaceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class TableContainer
    {
        private readonly ITableContainer iTableContainer;

        public TableContainer(ITableContainer dal)
        {
            iTableContainer = dal;
        }

        /// <summary>
        /// Get all Tables.
        /// </summary>
        /// <returns>A List of Tables.</returns>
        public List<Table> GetAll()
        {
            return iTableContainer.GetAll().ConvertAll(x=>new Table(x));
        }
        /// <summary>
        /// Get all seated Tables.
        /// </summary>
        /// <returns>A List of Tables that are seated.</returns>
        public List<Table> GetAllSeatedTables()
        {
            return iTableContainer.GetAllSeatedTables().ConvertAll(x=>new Table(x, x.Time_Arrived));
        }
        /// <summary>
        /// Get all seated Tables.
        /// </summary>
        /// <returns>A List of Tables that are seated.</returns>
        public List<Table> GetAllNonSeatedTables()
        {
            return iTableContainer.GetAllNonSeatedTables().ConvertAll(x=>new Table(x));
        }

        /// <summary>
        /// Get a certain Table.
        /// </summary>
        /// <param name="id">The Id of the Table.</param>
        /// <returns>The Table with the Id given.</returns>
        public Table GetTable(int id)
        {
            return new Table(iTableContainer.GetTable(id));
        }

        ///<summary>
        /// Delete Table based on Id.
        /// </summary>
        /// <returns>Succesfulness (bool) on deleting the Table.</returns>
        public bool CreateTable(string name)
        {
            return iTableContainer.CreateTable(name);
        }

        ///<summary>
        /// Delete Table based on Id.
        /// </summary>
        /// <returns>Succesfulness (bool) on deleting the Table.</returns>
        public bool DeleteTable(int id)
        {
            return iTableContainer.DeleteTable(id);
        }

        ///<summary>
        /// Open Table based on Id.
        /// </summary>
        /// <returns>Succesfulness (bool) on opening the Table.</returns>
        public bool OpenTable(int id)
        {
            return iTableContainer.OpenTable(id);
        }

        ///<summary>
        /// Close Table based on Id.
        /// </summary>
        /// <returns>Succesfulness (bool) on Closing the Table.</returns>
        public bool CloseTable(int id)
        {
            return iTableContainer.CloseTable(id);
        }
    }
}
