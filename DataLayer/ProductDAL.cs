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
        public string GetProductTypeName(int typeId)
        {
            string result = "";
            try
            {
                OpenCon();
                DbCom.CommandText = "SELECT Name FROM ProductTypes WHERE Id = @id";
                DbCom.Parameters.AddWithValue("id", typeId);

                var reader = DbCom.ExecuteReader();
                while (reader.Read())
                {
                    result = (string)reader[0];
                }
            }
            catch (Exception e) { }
            finally { CloseCon(); }
            return result;
        }
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
                list.Add(new ProductDTO((int)reader["Id"], (string)reader["Name"], (double)reader["Price"], (int)reader["Product_Type"]));
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
                list.Add(new ProductDTO((int)reader["Id"], (string)reader["Name"], (double)reader["Price"], (int)reader["Product_Type"]));
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
                product = new ProductDTO((int)reader["Id"], (string)reader["Name"], (double)reader["Price"], (int)reader["Product_Type"]);
            }

            CloseCon();

            return product;
        }

        public bool CreateProduct(ProductDTO product)
        {
            var success = false;
            try
            {
                OpenCon();

                DbCom.CommandText = "INSERT INTO Products (Name, Price, Product_Type) Values (@name, @price, @type)";
                DbCom.Parameters.AddWithValue("name", product.Name);
                DbCom.Parameters.AddWithValue("price", product.Price);
                DbCom.Parameters.AddWithValue("type", product.Type);

                success = DbCom.ExecuteNonQuery() > 0;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
            finally { CloseCon(); }

            return success;
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

        public List<ProductTypeDTO> GetAllTypes()
        {
            List<ProductTypeDTO> list = new List<ProductTypeDTO>();
            try
            {
                OpenCon();
                DbCom.CommandText = "SELECT Id, Name FROM ProductTypes";
                var reader = DbCom.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new ProductTypeDTO((int)reader[0], (string)reader[1]));
                }
            } catch (Exception e) { }
            finally { CloseCon(); }
            return list;
        }

        public bool Edit(ProductDTO productDTO)
        {
            bool result = false;
            try
            {
                OpenCon();
                DbCom.CommandText = "UPDATE Products SET Name = @name, Price = @price, Product_Type = @type WHERE Id = @id";
                DbCom.Parameters.AddWithValue("@id", productDTO.Id);
                DbCom.Parameters.AddWithValue("@name", productDTO.Name);
                DbCom.Parameters.AddWithValue("@price", productDTO.Price);
                DbCom.Parameters.AddWithValue("@type", productDTO.Type);
                if (DbCom.ExecuteNonQuery() > 0) result = true;
            } catch(Exception e)
            {
                return result;
            }
            finally
            {
                CloseCon();
            }
            return result;
        }
        #endregion
    }
}
