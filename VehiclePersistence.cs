using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MitchellCodingChallenge.Models;
using MySql.Data;

namespace MitchellCodingChallenge
{
    public class VehiclePersistence : VehiclePersistenceInterface
    {
        private MySql.Data.MySqlClient.MySqlConnection conn;
        public VehiclePersistence()   //creates a connection to our database
        {
            string myConnectionString;
            myConnectionString = "server=127.0.0.1;uid=root;pwd=root;database=vehicledb";
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw ex;
            }

        }
        public List<Vehicle> getAllVehicles()
        {
            List<Vehicle> vehicleList = new List<Vehicle>();           
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;
            String sqlString = "SELECT * FROM vehicle_details";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySqlReader = cmd.ExecuteReader();
            while (mySqlReader.Read())
            {
                Vehicle v = new Vehicle();
                v.Id = mySqlReader.GetInt32(0);
                v.Year = mySqlReader.GetInt32(1);
                v.Make = mySqlReader.GetString(2);
                v.Model = mySqlReader.GetString(3);
                vehicleList.Add(v);
            }
            return vehicleList;
        }

        public Vehicle getVehicle(int id)
        {
            Vehicle v = new Models.Vehicle();
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;
            String sqlString = "SELECT * FROM vehicle_details WHERE id = " + id.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString,conn);
            mySqlReader = cmd.ExecuteReader();
            if(mySqlReader.Read())
            {
                v.Id = mySqlReader.GetInt32(0);
                v.Year = mySqlReader.GetInt32(1);
                v.Make = mySqlReader.GetString(2);
                v.Model = mySqlReader.GetString(3);
                return v;
            }
            else
            {
                return null;
            }

        }

        public int saveVehicle(Vehicle vehicleToSave)
        {
            String sqlString = "INSERT INTO vehicle_details (year, make, model) VALUES ("+ vehicleToSave.Year +",'"+vehicleToSave.Make + "','" + vehicleToSave.Model +"')";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            cmd.ExecuteNonQuery();
            int id = (int) cmd.LastInsertedId;
            return id;
        }

        public bool updateVehicle(int id, Vehicle vehicleToUpdate)
        {
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;
            String sqlString = "SELECT * FROM vehicle_details WHERE id =" + id.ToString();  // TO first check if the record exists. It will return false if record does not exist.
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySqlReader = cmd.ExecuteReader();
            if (mySqlReader.Read())
            {
                mySqlReader.Close();
                sqlString = "UPDATE vehicle_details SET year =" + vehicleToUpdate.Year + ", make ='" + vehicleToUpdate.Make + "', model ='" + vehicleToUpdate.Model + "'WHERE id=" + id.ToString();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
                cmd.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool deleteVehicle(int id)
        {
            MySql.Data.MySqlClient.MySqlDataReader mySqlReader = null;
            String sqlString = "SELECT * FROM vehicle_details WHERE id = " + id.ToString();  // TO first check if the record exists. It will return false if record does not exist.
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
            mySqlReader = cmd.ExecuteReader();
            if (mySqlReader.Read())
            {
                mySqlReader.Close();
                sqlString = "DELETE FROM vehicle_details WHERE id = " + id.ToString();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, conn);
                cmd.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }      
    }
}
