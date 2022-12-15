using InterfaceLayer;
using InterfaceLayer.DTO;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;

namespace DataLayer
{
    public class TableDAL : SqlConnect, ITableContainer, ITable
    {
        public TableDAL() { InitializeDB(); }

        private SqlDataReader reader;

        #region Methods from ITable
        //Done
        public OrderDTO CreateOrder(int id, int staffId)
        {
            int sTableId = 0;
            DateTime dt = DateTime.Now;
            OrderDTO order = null;
            try
            {

                OpenCon();

                DbCom.CommandText = "SELECT Id, Time_Arrived FROM SeatedTables WHERE Table_Id = @id and Time_Left is null";
                DbCom.Parameters.AddWithValue("id", id);
                reader = DbCom.ExecuteReader();

                while (reader.Read())
                {
                    sTableId = (int)reader["Id"];
                    dt = (DateTime)reader["Time_Arrived"];
                }
                if (sTableId == 0) throw new Exception("Table has not been Seated! Or another problem occured.");

            }
            catch (Exception e) {
                Console.Error.WriteLine(e.Message);
            }
            finally { CloseCon(); }
            try
            {
                OpenCon();

                DbCom.Parameters.Clear();
                var now = DateTime.Now;
                DbCom.CommandText = "INSERT INTO Orders (SeatedTable_Id, Staff_Id, Created_At) VALUES (@table, @staff, @now) SELECT SCOPE_IDENTITY()";
                DbCom.Parameters.AddWithValue("table", sTableId);
                DbCom.Parameters.AddWithValue("staff", staffId);
                DbCom.Parameters.AddWithValue("now", now);
                decimal idDec = (decimal)DbCom.ExecuteScalar();
                order = new((int)idDec, sTableId, staffId, now);

            }
            catch (Exception e) { }
            finally { CloseCon(); }
            return order;
        }
        public bool RemoveOrder(int id)
        {
            var success = false;
            try
            {
                OpenCon();

                DbCom.CommandText = "DELETE FROM Orders WHERE Id = @id";
                DbCom.Parameters.AddWithValue("id", id);

                if (DbCom.ExecuteNonQuery() > 0)
                {
                    success = true;
                }
            }
            catch (Exception e) { }
            finally
            {
                CloseCon();
            }
            return success;
        }
        //TODO
        public TableDTO Edit(TableDTO newTable)
        {
            throw new NotImplementedException();
        }
        //Done
        public List<OrderDTO> GetOrders(int id)
        {
            int sTableId = 0;
            var list = new List<OrderDTO>();
            try
            {

                OpenCon();

                DbCom.CommandText = "SELECT Id FROM SeatedTables WHERE Table_Id = @id";

                DbCom.Parameters.AddWithValue("id", id);
                reader = DbCom.ExecuteReader();
                while (reader.Read())
                {
                    sTableId = (int)reader[0];
                }
                if (sTableId == 0) throw new Exception("Table has not been Seated! Or another problem occured.");

            }
            catch (Exception e) { }
            finally { CloseCon(); }
            try
            {

                OpenCon();

                DbCom.CommandText = "SELECT * FROM Orders Where SeatedTable_id = @sTableId and Saved_At is not NULL";
                DbCom.Parameters.AddWithValue("sTableId", sTableId);

                reader = DbCom.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new OrderDTO((int)reader["Id"], (int)reader["SeatedTable_Id"], (int)reader["Staff_Id"], (DateTime)reader["Created_AT"]));
                }

            }
            catch (Exception e) { }
            finally { CloseCon(); }
            return list;
        }
        //Done
        public double GetTotalPrice(int id)
        {
            double TotalPrice = 0;
            try
            {
                OpenCon();

                DbCom.CommandText = "SELECT Orders.Id, SeatedTable_Id, Staff_Id, Orders.Created_At, Saved_At, Product_Id, Amount, Product_Price, OrderRules.Created_At as AddedRuleDate FROM Orders " +
                                "INNER JOIN OrderRules on Order_Id = Orders.Id " +
                                "INNER JOIN SeatedTables on SeatedTable_Id = SeatedTables.Id " +
                                "WHERE SeatedTables.Table_Id = @id and " +
                                "Time_Left is null and " +
                                "OrderRules.Created_At >= Orders.Created_At";
                DbCom.Parameters.AddWithValue("id", id);
                reader = DbCom.ExecuteReader();

                while (reader.Read())
                {
                    TotalPrice += ((double)reader["Product_Price"] * (int)reader["Amount"]);
                }

            }
            catch (Exception e) { }
            finally { CloseCon(); }
            return TotalPrice;
        }
        //Done
        public string GetLastOrderText(int id)
        {
            string lastOrderString = "";
            int tableId = 0;
            try
            {
                OpenCon();

                DbCom.CommandText = "SELECT Table_Id FROM SeatedTables WHERE Id = @id";
                DbCom.Parameters.AddWithValue("id", id);

                reader = DbCom.ExecuteReader();
                while (reader.Read())
                {
                    tableId = (int)reader[0];
                }

            }
            catch (Exception e) { }
            finally { CloseCon(); }
            try
            {
                OpenCon();

                DbCom.CommandText = "SELECT TOP 1 Orders.Id, Orders.Staff_Id, Staff.Name, Orders.Saved_At, OrderRules.Created_At as AddedRuleDate, SeatedTables.Time_Arrived, SeatedTables.Time_Left FROM Orders " +
                                "INNER JOIN OrderRules on Order_Id = Orders.Id " +
                                "INNER JOIN Staff on Staff.Id = Orders.Staff_Id " +
                                "INNER JOIN SeatedTables on SeatedTable_Id = SeatedTables.Id " +
                                "WHERE SeatedTables.Table_Id = @id and " +
                                "Orders.Saved_At >= SeatedTables.Time_Arrived and "+
                                "Time_Left is null " +
                                "order by AddedRuleDate desc";
                DbCom.Parameters.AddWithValue("id", tableId);
                reader = DbCom.ExecuteReader();

                while (reader.Read())
                {
                    lastOrderString = (string)reader["Name"] + " - " + ((DateTime)reader["AddedRuleDate"]).ToString("HH:mm:ss");
                }
                if (string.IsNullOrEmpty(lastOrderString)) lastOrderString = "No Orders Yet.";
            }
            catch (Exception e) { }
            finally { CloseCon(); }
            return lastOrderString;
        }
        //Done
        public OrderDTO GetOpenOrder(int id)
        {
            OrderDTO lastOrder = null;
            try
            {
                OpenCon();

                DbCom.CommandText = "SELECT * FROM Orders" +
                " INNER JOIN SeatedTables on SeatedTable_Id = SeatedTables.Id" +
                " WHERE SeatedTables.Table_Id = @id and Saved_At is null";
                DbCom.Parameters.AddWithValue("id", id);
                reader = DbCom.ExecuteReader();

                while (reader.Read())
                {
                    lastOrder = new OrderDTO((int)reader["Id"], (int)reader["SeatedTable_Id"], (int)reader["Staff_Id"], (DateTime)reader["Created_At"]);
                }
            }
            catch (Exception e) { }
            finally { CloseCon(); }
            return lastOrder;
        }
        #endregion

        #region Methods from ITableContainer
        //Done
        public List<TableDTO> GetAll()
        {
            var tables = new List<TableDTO>();
            try
            {
                OpenCon();

                DbCom.CommandText = "SELECT * FROM Tables ORDER BY Id";

                reader = DbCom.ExecuteReader();

                while (reader.Read())
                {
                    tables.Add(new TableDTO((int)reader["Id"], (string)reader["Name"]));
                }
            }
            catch (Exception e) { }
            finally { CloseCon(); }
            return tables;
        }
        //Done
        public List<TableDTO> GetAllNonSeatedTables()
        {
            var nonSeatedTables = new List<TableDTO>();
            try
            {
                OpenCon();

                DbCom.CommandText = "SELECT Table_Id, Tables.Name as 'Table_Name', MAX(Time_Arrived), MAX(Time_Left) FROM SeatedTables" +
            " INNER JOIN Tables on SeatedTables.Table_Id = Tables.Id" +
            " GROUP BY Table_Id, Tables.Name" +
            " HAVING count(Time_Arrived) = count(Time_Left)";

                reader = DbCom.ExecuteReader();


                while (reader.Read())
                {
                    nonSeatedTables.Add(new TableDTO((int)reader["Table_Id"], (string)reader["Table_Name"]));
                }
            }
            catch (Exception e) { }
            finally { CloseCon(); }
            try
            {
                OpenCon();

                // select all the tables that have never been in SeatedTables table
                // this is because of the left join that the "Table_id is null"
                DbCom.CommandText = "SELECT * FROM Tables" +
            " LEFT JOIN SeatedTables on Tables.Id = SeatedTables.Table_Id" +
            " WHERE SeatedTables.Table_Id is null";

                reader = DbCom.ExecuteReader();

                while (reader.Read())
                {
                    nonSeatedTables.Add(new TableDTO((int)reader["Id"], (string)reader["Name"]));
                }
            }
            catch (Exception e) { }
            finally { CloseCon(); }
            return nonSeatedTables;
        }
        //Done
        public List<TableDTO> GetAllSeatedTables()
        {
            var tables = new List<TableDTO>();
            try
            {
                OpenCon();

                DbCom.CommandText = "SELECT Tables.Id as 'Table_Id', Tables.Name as 'Table_Name', Time_Arrived, Time_Left FROM SeatedTables" +
            " INNER JOIN Tables on SeatedTables.Table_Id = Tables.Id" +
            " WHERE Time_Left is NULL";

                reader = DbCom.ExecuteReader();


                while (reader.Read())
                {
                    tables.Add(new TableDTO((int)reader["Table_Id"], (string)reader["Table_Name"], (DateTime)reader["Time_Arrived"]));
                }
            }
            catch (Exception e) { }
            finally { CloseCon(); }
            return tables;
        }
        //Done
        public TableDTO GetTable(int id)
        {
            TableDTO table = null;
            try
            {
                OpenCon();

                DbCom.CommandText = "SELECT * FROM Tables WHERE Id = @id";

                DbCom.Parameters.AddWithValue("id", id);
                reader = DbCom.ExecuteReader();

                while (reader.Read())
                {
                    table = new TableDTO((int)reader["Id"], (string)reader["Name"]);
                }
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
            finally { CloseCon(); }
            return table;
        }
        //Done
        public bool CreateTable(string name)
        {
            bool succeeded = false;
            try
            {
                OpenCon();

                DbCom.CommandText = "INSERT INTO Tables (Name) VALUES (@name)";

                DbCom.Parameters.AddWithValue("name", name);

                try
                {
                    if (DbCom.ExecuteNonQuery() > 0) succeeded = true; else succeeded = false;
                }
                catch (Exception e)
                {
                    succeeded = false;
                }
            }
            catch (Exception e) { }
            finally { CloseCon(); }
            return succeeded;
        }
        //Done
        public bool DeleteTable(int id)
        {
            bool succeeded = false;
            try
            {
                OpenCon();

                DbCom.CommandText = "DELETE FROM Tables WHERE Id = @id";

                DbCom.Parameters.AddWithValue("id", id);

                if (DbCom.ExecuteNonQuery() > 0) succeeded = true; else succeeded = false;

            }
            catch (Exception e) { }
            finally { CloseCon(); }
            return succeeded;
        }
        //Done
        public bool OpenTable(int id)
        {
            bool succeeded = false;
            TableDTO table = null;
            try
            {
                OpenCon();

                DbCom.CommandText = "SELECT TOP 1 * FROM SeatedTables WHERE Table_Id = @id and Time_Left = null";

                DbCom.Parameters.AddWithValue("id", id);


                reader = DbCom.ExecuteReader();


                while (reader.Read())
                {
                    table = new TableDTO((int)reader["Id"], (string)reader["Name"]);
                }
            }
            catch (Exception e) { }
            finally { CloseCon(); }
            try
            {
                OpenCon();
                if (table != null)
                {
                    succeeded = false;
                }
                else
                {

                    DbCom.CommandText = "INSERT INTO SeatedTables (Table_Id, Time_Arrived) VALUES (@id, @now)";

                    DbCom.Parameters.AddWithValue("id", id);
                    DbCom.Parameters.AddWithValue("now", DateTime.Now);

                    if (DbCom.ExecuteNonQuery() > 0)
                    {
                        succeeded = true;
                    }
                }
            }
            catch (Exception e) { }
            finally { CloseCon(); }
            return succeeded;
        }
        //Done
        public bool CloseTable(int id)
        {
            bool succeeded = false;
            TableDTO table = null;
            int SeatedTableId = 0;

            try
            {
                OpenCon();

                DbCom.CommandText = "SELECT TOP 1 SeatedTables.Id, SeatedTables.Table_Id, Time_Arrived, Time_Left, Tables.Id as 'TableId', Tables.Name FROM SeatedTables" +
            " INNER JOIN Tables on SeatedTables.Table_Id = Tables.Id" +
            " WHERE Table_Id = @id and Time_Left is null";

                DbCom.Parameters.AddWithValue("id", id);


                reader = DbCom.ExecuteReader();

                while (reader.Read())
                {
                    table = new TableDTO((int)reader["Table_Id"], (string)reader["Name"]);
                    SeatedTableId = (int)reader["Id"];
                }
            }
            catch (Exception e) { }
            finally { CloseCon(); }
            try
            {
                OpenCon();
                if (table == null)
                {
                    succeeded = false;
                }
                else
                {

                    DbCom.CommandText = "UPDATE SeatedTables SET Time_Left = @now WHERE id = @id";

                    DbCom.Parameters.AddWithValue("id", SeatedTableId);
                    DbCom.Parameters.AddWithValue("now", DateTime.Now);

                    if (DbCom.ExecuteNonQuery() > 0)
                    {
                        succeeded = true;
                    }
                }
            }
            catch (Exception e) { }
            finally { CloseCon(); }
            return succeeded;
        }
        #endregion
    }
}
