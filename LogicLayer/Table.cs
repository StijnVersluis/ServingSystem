using InterfaceLayer;
using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class Table
    {
        public int Id { private set; get; }
        public string Name { private set; get; }
        public DateTime Time_Arrived { private set; get; }

        public Table(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Table(TableDTO table)
        {
            Id = table.Id;
            Name = table.Name;
        }

        public Table(TableDTO table, DateTime timearrived)
        {
            Id = table.Id;
            Name = table.Name;
            Time_Arrived = timearrived;
        }

        public TableDTO ToDTOWithoutId()
        {
            return new TableDTO(this.Id, this.Name);
        }

        /// <summary>
        /// Get all the orders from Table.
        /// </summary>
        /// <returns>A List of Orders</returns>
        public List<Order> GetOrders(ITable iTable)
        {
            return iTable.GetOrders(this.Id).ConvertAll(x => new Order(x));
        }

        /// <summary>
        /// Get the total price of all orders from the table.
        /// </summary>
        /// <returns>A double with the total price of the items ordered.</returns>
        public double GetTotalPrice(ITable iTable)
        {
            return iTable.GetTotalPrice(this.Id);
        }

        /// <summary>
        /// Get the total price of all orders from the table.
        /// </summary>
        /// <returns>A double with the total price of the items ordered.</returns>
        public string GetLastOrderText(ITable iTable)
        {
            return iTable.GetLastOrderText(this.Id);
        }

        /// <summary>
        /// Get last order from table 
        /// </summary>
        /// <returns>A double with the total price of the items ordered.</returns>
        public Order GetOpenOrder(ITable iTable)
        {
            var order = iTable.GetOpenOrder(this.Id);
            if (order == null) return null;
            else return new Order(order);
        }

        ///<summary>
        /// Create order and add to table.
        /// </summary>
        /// <param name="staffId">The id of the staff member logged in.</param>
        /// <returns>The newly created order.</returns>
        public Order CreateOrder(ITable iTable, int staffId)
        {
            return new Order(iTable.CreateOrder(this.Id, staffId));
        }

        ///<summary>
        /// Edit Table in database
        /// </summary>
        /// <returns>The recently edited Table</returns>
        public Table Edit(ITable iTable, string name)
        {
            return new Table(iTable.Edit(new TableDTO(this.Id, name)));
        }
    }
}
