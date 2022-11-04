﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataLayer
{
    public class SqlConnect
    {
        //variabele aanmaken die in de DAL kunnen worden gebuikt.
        internal SqlCommand DbCom;
        internal SqlConnection DBConnection;

        //Methode om connection tussen database op te zetten.
        public void InitializeDB()
        {
            string connectionString = @"Data Source=mssqlstud.fhict.local;Database=dbi482774_servingsys;User Id=dbi482774_servingsys;Password=ServingSystem;";
            DBConnection = new SqlConnection(connectionString);
            DbCom = DBConnection.CreateCommand();
        }

        //Connectie opzetten met database.
        public bool OpenCon()
        {
            try
            {
                if (DBConnection.State == ConnectionState.Closed) DBConnection.Open();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        //Connectie afsluiten met database.
        public bool CloseCon()
        {
            try
            {
                DbCom.Parameters.Clear();
                DBConnection.Close();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }
    }
}
