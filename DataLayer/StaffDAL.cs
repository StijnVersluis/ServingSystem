using InterfaceLayer;
using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class StaffDAL : SqlConnect, IStaff, IStaffContainer
    {
        private SqlDataReader reader;
        public bool AttemptLogin(string uName, string password)
        {
            OpenCon();
            DbCom.CommandText = "SELECT * FROM Staff WHERE UName = @uname";
            DbCom.Parameters.AddWithValue("uname", uName);
            
            
            reader = DbCom.ExecuteReader();

            StaffDTO staff = null;
            bool success = false;

            while (reader.Read())
            {
                staff = new StaffDTO((int)reader["Id"], (string)reader["Name"], (string)reader["UName"]);
            }
            if (GetCode(staff) == password)
            {
                GlobalVariables.LoggedInUser = staff;
                success = true;
            }
            CloseCon();
            return success;
        }

        public string GetCode(StaffDTO staff)
        {
            if (DBConnection.State == ConnectionState.Closed) OpenCon();
            DbCom.Parameters.Clear();
            DbCom.CommandText = "SELECT Code FROM Staff WHERE Id = @id";
            DbCom.Parameters.AddWithValue("id", staff.Id);
            reader = DbCom.ExecuteReader();
            string code = "";
            while (reader.Read())
            {
                code = (string)reader[0];
            }
            return code;
        }

        public StaffDTO GetLoggedInStaff()
        {
            return GlobalVariables.LoggedInUser;
        }

        public bool IsLoggedIn()
        {
            if (GlobalVariables.LoggedInUser != null) return true;
            else return false;
        }
    }
}
