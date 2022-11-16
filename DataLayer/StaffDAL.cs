using InterfaceLayer;
using InterfaceLayer.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataLayer
{
    public class StaffDAL : SqlConnect, IStaff, IStaffContainer
    {
        private SqlDataReader reader;

        public StaffDAL()
        {
            InitializeDB();
        }

        public int AttemptLogin(string uName, string password)
        {
            OpenCon();
            DbCom.CommandText = "SELECT * FROM Staff WHERE UName = @uname";
            DbCom.Parameters.AddWithValue("uname", uName);
            
            
            reader = DbCom.ExecuteReader();

            StaffDTO staff = null;
            bool success = false;

            while (reader.Read())
            {
                staff = new StaffDTO((int)reader["Id"], (string)reader["Name"], (string)reader["UName"], (bool)reader["Is_Admin"]);
            }
            if (staff != null)
            {
                if (GetCode(staff) == password)
                {
                    success = true;
                }
            }
            CloseCon();
            if (success) return staff.Id;
            else return 0;
        }

        public string GetCode(StaffDTO staff)
        {
            if (DBConnection.State == ConnectionState.Closed) OpenCon();
            DbCom.Parameters.Clear();
            DbCom.CommandText = "SELECT Code FROM Staff WHERE Id = @id";
            DbCom.Parameters.AddWithValue("id", staff.Id);
            reader.Close();
            reader = DbCom.ExecuteReader();
            string code = "";
            while (reader.Read())
            {
                code = (string)reader[0];
            }
            CloseCon();
            return code;
        }

        public StaffDTO GetLoggedInStaff(int id)
        {
            OpenCon();
            DbCom.CommandText = "SELECT * FROM Staff WHERE Id = @id";
            DbCom.Parameters.AddWithValue("id", id);


            reader = DbCom.ExecuteReader();

            StaffDTO staff = null;

            while (reader.Read())
            {
                staff = new StaffDTO((int)reader["Id"], (string)reader["Name"], (string)reader["UName"], (bool)reader["Is_Admin"]);
            }

            CloseCon();
            return staff;
        }
    }
}
