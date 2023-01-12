using DataLayer;
using InterfaceLayer;
using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UnitTest.STUB
{
    internal class TableSTUB : ITable, ITableContainer
    {
        public List<TableDTO> tables;
        public List<OrderDTO> orders;
        public List<OrderRuleDTO> orderRules;
        /// <summary>A List of products.</summary>
        public static List<ProductDTO> products = new List<ProductDTO>()
        {
                new ProductDTO(1, "Pizza", 5.99, 1),
                new ProductDTO(2, "Pasta", 6.99, 1),
                new ProductDTO(3, "Sushi", 5.99, 1),
                new ProductDTO(4, "Cola", 2.99, 2),
                new ProductDTO(5, "Ice Tea", 3.49, 2),
                new ProductDTO(6, "Fanta", 2.99, 2),
                new ProductDTO(7, "Fries", 5.99, 3),
                new ProductDTO(8, "Salad", 5.99, 3),
        };
        /// <summary>A List of product types.</summary>
        public List<ProductTypeDTO> typeList = new List<ProductTypeDTO>()
            {
                new ProductTypeDTO(1, "Food"),
                new ProductTypeDTO(2, "Drink"),
                new ProductTypeDTO(3, "Extra"),
            };
        public TableSTUB()
        {
            this.tables = new List<TableDTO>()
            {
                new TableDTO(1, "T-01", new DateTime(2022, 12, 23, 10, 0,0)),
                new TableDTO(2, "T-02", new DateTime(2022, 12, 23, 10, 2,0)),
                new TableDTO(3, "T-03"),
                new TableDTO(4, "TER-01"),
                new TableDTO(5, "TER-02"),
            };
            orders = new List<OrderDTO>()
            {
                new OrderDTO(1, tables[0].Id, 1, new(2022, 12, 23, 10, 1, 0)),
                new OrderDTO(2, tables[0].Id, 1, new(2022, 12, 23, 10, 30, 0)),
                new OrderDTO(3, tables[1].Id, 1, new(2022, 12, 23, 10, 2, 0)),
            };
            orderRules = new List<OrderRuleDTO>()
            {
                new OrderRuleDTO(1, orders[0].Id, products[0].Id, 2, products[0].Price),
                new OrderRuleDTO(2, orders[0].Id, products[3].Id, 4, products[3].Price),
                new OrderRuleDTO(3, orders[1].Id, products[0].Id, 3, products[0].Price),
                new OrderRuleDTO(4, orders[1].Id, products[4].Id, 1, products[4].Price),
                new OrderRuleDTO(5, orders[2].Id, products[3].Id, 1, products[3].Price),
            };
        }

        public bool CloseTable(int id)
        {
            tables.Where(table => table.Id == id).ToList().ForEach(table => table = new(table.Id, table.Name));
            return true;
        }

        public OrderDTO CreateOrder(int id, int staffId)
        {
            OrderDTO order = new(orders.Count, id, staffId, DateTime.Now);
            orders.Add(order);
            return order;
        }

        public bool CreateTable(string name)
        {
            tables.Add(new(tables[tables.Count - 1].Id + 1, name));
            return true;
        }

        public bool DeleteTable(int id)
        {
            var table = tables.Single(table => table.Id == id);
            return tables.Remove(table);
        }

        public TableDTO Edit(TableDTO newTable)
        {
            tables.Where(table => table.Id == newTable.Id).ToList().ForEach(table =>
            {
                table.Id = newTable.Id;
                table.Name = newTable.Name;
            });
            return tables.FindLast(table => table.Id == newTable.Id);
        }

        public List<TableDTO> GetAll()
        {
            return tables;
        }

        public List<TableDTO> GetAllNonSeatedTables()
        {
            return tables.Where(table => table.Time_Arrived.Year != 2022).ToList();
        }

        public List<TableDTO> GetAllSeatedTables()
        {
            return tables.Where(table => table.Time_Arrived.Year == 2022).ToList();
        }

        public string GetLastOrderText(int id)
        {
            var table = tables.FindLast(table => table.Id == id);
            var tOrders = orders.Where(order => order.SeatedTableId == id).OrderBy(order => order.CreatedAt).ToList();
            return table.Name + " - " + tOrders[0].CreatedAt.ToString("HH:mm:ss");
        }

        public OrderDTO GetOpenOrder(int id)
        {
            return orders.OrderByDescending(order => order.CreatedAt).ToList().Find(order => order.SeatedTableId == id);
        }

        public List<OrderDTO> GetOrders(int id)
        {
            return orders.Where(order => order.SeatedTableId == id).ToList();
        }

        public TableDTO GetTable(int id)
        {
            return tables.Single(table => table.Id == id);
        }

        public double GetTotalPrice(int id)
        {
            double price = 0;
            var tableorders = orders.FindAll(order => order.SeatedTableId == id);
            var tablerules = new List<OrderRuleDTO>();
            tableorders.ForEach(order =>
            {
                tablerules.AddRange(orderRules.FindAll(rule => rule.OrderId == order.Id));
            });
            tablerules.ForEach(rule => price += rule.ProductPrice * rule.Amount);
            return price;
        }

        public bool OpenTable(int id)
        {
            tables.Find(table => table.Id == id).Time_Arrived = DateTime.Now;
            return true;
        }

        public bool RemoveOrder(int id)
        {
            orderRules.FindAll(rule => rule.OrderId == id).ToList().ForEach(rule => orderRules.Remove(rule));
            orders.Remove(orders.Find(order => order.Id == id));
            return true;
        }
    }
}
