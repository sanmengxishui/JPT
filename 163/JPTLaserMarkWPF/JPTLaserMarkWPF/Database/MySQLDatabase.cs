using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace JPTLaserMarker.Database
{
    public class MyData
    {
        public int id = 0;
        public Image myImg = null;
    }
    public class MySQLDatabase
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public MySQLDatabase()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "www.jptlaser.tk";//"localhost";
            database = "jpt";
            uid = "jptuser";//"root";
            password = "jptpassword";//"jpt123";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + 
		    database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Update statement
        public void Update(int id)
        {
            string query = "UPDATE record SET printed='1' WHERE id='" + id.ToString() + "'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {

            System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
            Image img = (Image)converter.ConvertFrom(byteArrayIn);

            return img;
        }

        //Select statement
        public List<MyData> Select()
        {
            string query = "SELECT * FROM record WHERE printed ='0'";

            //Create a list to store the result
            List<MyData> list = new List<MyData>();
            
            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    MyData tmpData = new MyData();
                    tmpData.id = Convert.ToInt32(dataReader.GetString(0));//dataReader["id"]
                    string base64String;
                    base64String = dataReader.GetString(1);//dataReader["image"];
                    base64String = base64String.Substring(base64String.IndexOf(',')+1);
                    byte[] imageBytes = Convert.FromBase64String(base64String);
                    tmpData.myImg = byteArrayToImage(imageBytes);
                    list.Add(tmpData);                    
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }
    }
}
