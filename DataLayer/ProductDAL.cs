using InterfaceLayer;
using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;

namespace DataLayer
{
    public class ProductDAL : SqlConnect, IProductContainer, IProduct
    {
        public ProductDAL() { InitializeDB(); }

        #region Methods from IProduct
        #endregion

        #region Methods from IProductContainer
        public List<ProductDTO> GetAll()
        {
            OpenCon();

            DbCom.CommandText = "SELECT * FROM Products";

            var reader = DbCom.ExecuteReader();

            var list = new List<ProductDTO>();

            while (reader.Read())
            {
                list.Add(new ProductDTO((int)reader["Id"], (string)reader["Name"], (double)reader["Price"], (int)reader["Type"]));
            }

            CloseCon();

            return list;
        }

        public List<ProductDTO> GetAllOfType(int type)
        {
            OpenCon();

            DbCom.CommandText = "SELECT * FROM Products WHERE Type=@type";
            DbCom.Parameters.AddWithValue("type", type);

            var reader = DbCom.ExecuteReader();

            var list = new List<ProductDTO>();

            while (reader.Read())
            {
                list.Add(new ProductDTO((int)reader["Id"], (string)reader["Name"], (double)reader["Price"], (int)reader["Type"]));
            }

            CloseCon();

            return list;
        }

        public ProductDTO GetProduct(int Id)
        {
            OpenCon();

            DbCom.CommandText = "SELECT * FROM Products WHERE Id=@id";
            DbCom.Parameters.AddWithValue("id", Id);

            var reader = DbCom.ExecuteReader();

            ProductDTO product = null;

            while (reader.Read())
            {
                product = new ProductDTO((int)reader["Id"], (string)reader["Name"], (double)reader["Price"], (int)reader["Type"]);
            }

            CloseCon();

            return product;
        }

        public ProductDTO CreateProduct(ProductDTO product)
        {
            OpenCon();

            DbCom.CommandText = "INSERT INTO Products (Name, Price, Product_Type) Values (@name, @price, @type)";
            DbCom.Parameters.AddWithValue("name", product.Name);
            DbCom.Parameters.AddWithValue("price", product.Price);
            DbCom.Parameters.AddWithValue("type", product.Type);

            DbCom.ExecuteNonQuery();

            CloseCon();

            return product;
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                OpenCon();

                DbCom.CommandText = "DELETE FROM Products WHERE Id=@id";
                DbCom.Parameters.AddWithValue("id", id);

                DbCom.ExecuteNonQuery();

                CloseCon();

                return true;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion
    }
}
