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
using System.Transactions;
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

        public List<StaffDTO> GetAll()
        {
            List<StaffDTO> list = new List<StaffDTO>();
            try
            {
                OpenCon();
                DbCom.CommandText = "SELECT * FROM Staff";
                var reader = DbCom.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new StaffDTO((int)reader["Id"], (string)reader["Name"], (string)reader["UName"], (bool)reader["Is_Admin"]));
                }
            }
            catch (Exception e) { }
            finally { CloseCon(); }
            return list;
        }

        public bool DeleteUser(int id)
        {
            bool success = false;
            try
            {
                OpenCon();
                DbCom.CommandText = "UPDATE Staff SET Deleted_at = GETDATE() WHERE Id = @id";
                DbCom.Parameters.AddWithValue("id", id);
                if (DbCom.ExecuteNonQuery() > 0)
                {
                    success = true;
                }
            }
            catch (Exception e) { }
            finally { CloseCon(); }
            return success;
        }

        public StaffDTO GetUserById(int id)
        {
            StaffDTO staff = null;
            try
            {
                OpenCon();
                DbCom.CommandText = "SELECT * FROM Staff WHERE Id = @id";
                DbCom.Parameters.AddWithValue("id", id);

                reader = DbCom.ExecuteReader();
                while (reader.Read())
                {
                    staff = new StaffDTO((int)reader["Id"], (string)reader["Name"], (string)reader["UName"], (bool)reader["Is_Admin"]);
                }
            }
            catch (Exception e) { }
            finally { CloseCon(); }
            return staff;
        }

        public StaffDTO GetUserByUserName(string userName)
        {
            StaffDTO staff = null;
            try
            {
                OpenCon();
                DbCom.CommandText = "SELECT * FROM Staff WHERE UName = @username";
                DbCom.Parameters.AddWithValue("username", userName);

                reader = DbCom.ExecuteReader();
                while (reader.Read())
                {
                    staff = new StaffDTO((int)reader["Id"], (string)reader["Name"], (string)reader["UName"], (bool)reader["Is_Admin"]);
                }
            }
            catch (Exception e) { }
            finally { CloseCon(); }
            return staff;
        }

        public StaffDTO Edit(StaffDTO newStaff)
        {
            StaffDTO result = null;
            try
            {
                var clause = "";
                if (!string.IsNullOrEmpty(newStaff.Code))
                {
                    clause = ", Code = @code";
                }
                OpenCon();
                DbCom.CommandText = $"UPDATE Staff SET Name = @name, UName = @uname{clause}, Is_Admin = @admin WHERE Id = @id";
                DbCom.Parameters.AddWithValue("id", newStaff.Id);
                DbCom.Parameters.AddWithValue("name", newStaff.Name);
                DbCom.Parameters.AddWithValue("uname", newStaff.UName);
                if (!string.IsNullOrEmpty(newStaff.Code))
                {
                    DbCom.Parameters.AddWithValue("code", newStaff.Code);
                }
                DbCom.Parameters.AddWithValue("admin", newStaff.IsAdmin);

                if (DbCom.ExecuteNonQuery() > 0)
                {
                    result = newStaff;
                }
            }
            catch (Exception e) { throw new Exception(e.Message, e); }
            finally { CloseCon(); }

            return result;
        }

        public StaffDTO CreateUser(StaffDTO newStaff)
        {
            StaffDTO staffDTO = null;
            try
            {
                OpenCon();
                DbCom.CommandText = "INSERT INTO Staff (Name, UName, Code, Is_Admin) Values (@name, @uname, @code, @admin) SELECT SCOPE_IDENTITY()";
                DbCom.Parameters.AddWithValue("name", newStaff.Name);
                DbCom.Parameters.AddWithValue("uname", newStaff.UName);
                DbCom.Parameters.AddWithValue("code", newStaff.Code);
                DbCom.Parameters.AddWithValue("admin", newStaff.IsAdmin);
                decimal lastid = 0;
                lastid = (decimal)DbCom.ExecuteScalar();
                if (lastid != 0)
                {
                    staffDTO = new StaffDTO(Convert.ToInt32(lastid), newStaff.Name, newStaff.UName, newStaff.IsAdmin);
                }
            }
            catch (Exception e)
            {
                return new StaffDTO(0, "Error", e.Message, false);
            }
            finally { CloseCon(); }
            return staffDTO;
        }
    }
}
