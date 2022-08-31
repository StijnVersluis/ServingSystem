using InterfaceLayer;
using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;

namespace DataLayer
{
    public class TableDAL : SqlConnect, ITableContainer, ITable
    {
        public ProductDTO AddProduct(ProductDTO drink)
        {
            throw new NotImplementedException();
        }

        public List<TableDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<ProductDTO> GetProduct()
        {
            throw new NotImplementedException();
        }

        public double GetPrice()
        {
            throw new NotImplementedException();
        }

        public TableDTO GetTable(int id)
        {
            throw new NotImplementedException();
        }

        public bool RemoveProduct(ProductDTO product)
        {
            throw new NotImplementedException();
        }
    }
}
