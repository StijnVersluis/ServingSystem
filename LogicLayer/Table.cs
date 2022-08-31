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
        private ITable itable;
        public int Id { private set; get; }
        public string Name { private set; get; }

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
        public List<Product> GetProducts()
        {
            var products = new List<Product>();
            itable.GetProducts().ForEach(product => products.Add(new Product(product)));
            return products;
        }
        public double GetTotalPrice()
        {
            return itable.GetTotalPrice();
        }

        /// public bool EditOrder(int productId, int newAmount)
        /// {
        ///     if (newAmount > 0) GetOrder(this.Id, productId).Edit(ProductId, newAmount)
        ///     else GetOrder(this.Id, productId).Remove();
        /// }
        
        public Table Edit(string name)
        {
            return new Table(itable.Edit(new TableDTO(this.Id, name)));
        }
        public bool Remove()
        {
            return itable.Remove(new TableDTO(this.Id, this.Name));
        }
    }
}
