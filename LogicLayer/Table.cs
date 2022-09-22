using InterfaceLayer;
using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Table
    {
        public int Id { private set; get; }
        public string Name { private set; get; }

        private ITable iTable;

        public Table(ITable itable, int id, string name)
        {
            iTable = itable;
            Id = id;
            Name = name;
        }

        public Table(TableDTO table)
        {
            Id = table.Id;
            Name = table.Name;
        }

        /// <summary>
        /// Get all the orders from Table.
        /// </summary>
        /// <returns>A List of Orders<returns>
        public List<Order> GetOrders()
        {
            return iTable.GetOrders().ConvertAll(x => new Order(x));
        }

        /// <summary>
        /// Get the total price of all orders from the table.
        /// </summary>
        /// <returns>A double with the total price of the items ordered.</returns>
        public double GetTotalPrice()
        {
            return iTable.GetTotalPrice();
        }

        ///<summary>
        /// Create order and add to table.
        /// </summary>
        /// <param name="staffId">The id of the staff member logged in.</param>
        /// <returns>The newly created order.</returns>
        public Order CreateOrder(int staffId)
        {
            return new Order(iTable.CreateOrder(this.Id, staffId));
        }

        ///<summary>
        /// Edit Table in database
        /// </summary>
        /// <returns>The recently edited Table</returns>
        public Table Edit(string name)
        {
            return new Table(iTable.Edit(new TableDTO(this.Id, name)));
        }

        ///<summary>
        /// Remove Table from database
        /// </summary>
        /// <returns>True or false based on if it has been successful</returns>
        public bool Remove()
        {
            return iTable.Remove(new TableDTO(this.Id, this.Name));
        }
    }
}
