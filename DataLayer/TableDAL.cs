using InterfaceLayer;
using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace DataLayer
{
    public class TableDAL : SqlConnect, ITableContainer, ITable
    {
        public TableDAL() { InitializeDB(); }

        private SqlDataReader reader;

        #region Methods from ITable
        public OrderDTO CreateOrder(int id, int staffId)
        {
            int sTableId = 0;
            DateTime dt = DateTime.Now;
            OrderDTO order = null;

            OpenCon();

            DbCom.CommandText = "SELECT Id, Time_Arrived FROM SeatedTables WHERE Table_Id = @id and Time_Left = null";
            DbCom.Parameters.AddWithValue("id", id);
            reader = DbCom.ExecuteReader();

            while (reader.Read())
            {
                sTableId = (int)reader["Id"];
                dt = (DateTime)reader["Time_Arrived"];
            }
            if (sTableId == 0) throw new Exception("Table has not been Seated! Or another problem occured.");

            DbCom.Parameters.Clear();

            DbCom.CommandText = "SELECT * FROM Orders Where SeatedTable_id = @sTableId and Created_At >= @timeArrived";
            DbCom.Parameters.AddWithValue("sTableId", sTableId);
            DbCom.Parameters.AddWithValue("timeArrived", dt);


            reader = DbCom.ExecuteReader();
            while (reader.Read())
            {
                order = new OrderDTO((int)reader["Id"], (int)reader["SeatedTable_Id"], (int)reader["Staff_Id"], (DateTime)reader["Created_At"]);
            }

            CloseCon();

            return order;
        }

        public TableDTO Edit(TableDTO newTable)
        {
            throw new NotImplementedException();
        }

        public List<OrderDTO> GetOrders(int id)
        {
            OpenCon();

            DbCom.CommandText = "SELECT Id FROM SeatedTables WHERE Table_Id = @id";

            DbCom.Parameters.AddWithValue("id", id);
            reader = DbCom.ExecuteReader();
            int sTableId = 0;
            while (reader.Read())
            {
                sTableId = (int)reader[0];
            }
            if (sTableId == 0) throw new Exception("Table has not been Seated! Or another problem occured.");


            DbCom.Parameters.Clear();

            DbCom.CommandText = "SELECT * FROM Orders Where SeatedTable_id = @sTableId";
            DbCom.Parameters.AddWithValue("sTableId", sTableId);

            reader = DbCom.ExecuteReader();
            var list = new List<OrderDTO>();

            while (reader.Read())
            {
                list.Add(new OrderDTO((int)reader["Id"], (int)reader["SeatedTable_Id"], (int)reader["Staff_Id"], (DateTime)reader["Created_AT"]));
            }

            CloseCon();
            return list;
        }

        public double GetTotalPrice(int id)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Methods from ITableContainer
        public List<TableDTO> GetAll()
        {
            OpenCon();

            DbCom.CommandText = "SELECT * FROM Tables ORDER BY Id";

            reader = DbCom.ExecuteReader();

            var tables = new List<TableDTO>();
            while (reader.Read())
            {
                tables.Add(new TableDTO((int)reader["Id"], (string)reader["Name"]));
            }

            CloseCon();
            return tables;
        }

        //Done
        public List<TableDTO> GetAllSeatedTables()
        {
            OpenCon();

            DbCom.CommandText = "SELECT * FROM SeatedTables WHERE Time_Left is NULL ORDER BY Id";

            reader = DbCom.ExecuteReader();

            var tables = new List<TableDTO>();

            var sTables = new Dictionary<int, DateTime>();

            while (reader.Read())
            {
                sTables.Add((int)reader["Table_Id"], (DateTime)reader["Time_Arrived"]);
            }

            while (reader.Read())
            {
                tables.Add(new TableDTO((int)reader["Table_Id"], (string)reader["Name"], (DateTime)reader["Time_Arrived"]));
            }

            CloseCon();
            return tables;
        }
        //Done
        public TableDTO GetTable(int id)
        {
            OpenCon();

            DbCom.CommandText = "SELECT * FROM Tables WHERE Id = @id";

            DbCom.Parameters.AddWithValue("id", id);

            reader = DbCom.ExecuteReader();

            var table = new TableDTO(0, "NON-EX");
            while (reader.Read())
            {
                table = new TableDTO((int)reader["Id"], (string)reader["Name"]);
            }

            CloseCon();
            return table;
        }
        //Done
        public bool CreateTable(TableDTO table)
        {
            OpenCon();

            DbCom.CommandText = "INSERT INTO Tables (Name) VALUES (@name)";

            DbCom.Parameters.AddWithValue("name", table.Name);

            bool succeeded = false;
            try
            {
                if (DbCom.ExecuteNonQuery() > 0) succeeded = true; else succeeded = false;
            } catch (Exception e)
            {
                succeeded = false;
            }


            CloseCon();
            return succeeded;
        }
        //Done
        public bool DeleteTable(int id)
        {
            OpenCon();

            DbCom.CommandText = "DELETE FROM Tables WHERE Id = @id";

            DbCom.Parameters.AddWithValue("id", id);

            bool succeeded = false;
            if (DbCom.ExecuteNonQuery() > 0) succeeded = true; else succeeded = false;


            CloseCon();
            return succeeded;
        }
        #endregion
    }
}
