using InterfaceLayer;
using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;

namespace DataLayer
{
    public class OrderDAL : SqlConnect, IOrder
    {
        public OrderDAL() { InitializeDB(); }

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
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public List<ProductDTO> GetProducts(int id)
        {
            OpenCon();
            throw new NotImplementedException();
            CloseCon();
        }

        public bool RemoveProduct(int id)
        {
            OpenCon();
            throw new NotImplementedException();
            CloseCon();
        }

        public bool SaveOrder(int id)
        {
            OpenCon();
            throw new NotImplementedException();
            CloseCon();
        }
        #endregion
    }
}
