using InterfaceLayer;
using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace DataLayer
{
    public class OrderDAL : SqlConnect, IOrder
    {
        public OrderDAL() { InitializeDB(); }

        private SqlDataReader reader;

        #region Methods from IOrder
        public bool AddProduct(int orderId, ProductDTO product)
        {
            try
            {
                OpenCon();

                //TODO: Check if already exist then add to amount.

                DbCom.CommandText = "INSERT INTO OrderRules (Order_Id, Product_Id, Amount, Product_Price) Values (@orderId, @productId, 1, @price)";
                DbCom.Parameters.AddWithValue("orderId", orderId);
                DbCom.Parameters.AddWithValue("price", product.Id);
                DbCom.Parameters.AddWithValue("price", product.Price);

                var reader = DbCom.ExecuteNonQuery();

                CloseCon();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public List<OrderRuleDTO> GetProducts(int id)
        {
            OpenCon();

            TableDAL dal = new TableDAL();
            List<OrderRuleDTO> orderRules = new List<OrderRuleDTO>();
            List<ProductDTO> products = new List<ProductDTO>();

            DbCom.CommandText = "SELECT * FROM OrderRules WHERE Order_Id = @id";
            DbCom.Parameters.AddWithValue("id", id);
            reader = DbCom.ExecuteReader();
            while (reader.Read())
            {
                orderRules.Add(new OrderRuleDTO((int)reader["Id"], (int)reader["Order_Id"], (int)reader["Product_Id"], (int)reader["Amount"], (double)reader["Product_Price"]));
            }

            CloseCon();

            return orderRules;
        }

        public bool RemoveProduct(int id, int productId)
        {
            OpenCon();
            throw new NotImplementedException();
            CloseCon();
        }

        public bool SaveOrder(int id)
        {
            var success = false;
            OpenCon();

            DbCom.CommandText = "INSERT INTO Orders (Saved_At) Values (@dt) WHERE Id = @id";
            DbCom.Parameters.AddWithValue("dt", DateTime.Now);
            DbCom.Parameters.AddWithValue("id", id);

            if (DbCom.ExecuteNonQuery() > 0)
            {
                success = true;
            }

            CloseCon();
            return success;
        }
        #endregion
    }
}
