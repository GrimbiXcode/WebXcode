using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

/* http://www.codeproject.com/Tips/423233/How-to-Connect-to-MySQL-Using-Csharp 
 *Server=myServerAddress;Port=1234;Database=myDataBase;Uid=myUsername;Pwd=myPassword;
 *
 * You don't need to specify the port if you are using 3306 since that is the default.
 **/

namespace WebXcode
{
    class MySQLClient
    {
        /***********************************************************************
         * Definition of attributes
         * ********************************************************************/
        private MySqlConnection myConnection;

        private string mSqlConnectionString;
        private bool mConnectionActive = false;

        /***********************************************************************
         * Main Code / System Methodes
         * ********************************************************************/
        public MySQLClient(string adress, string database, string user, string password) 
        {
            mSqlConnectionString = "Server="+adress+";" +
                                    "Port=3306;" +
                                    "Database="+database+";" +
                                    "Uid="+user+";" +
                                    "Pwd="+password+";";
        }

        public void setUp(string adress, string database, string user, string password)
        {
            mSqlConnectionString = "Server=" + adress + ";" +
                                    "Port=3306;" +
                                    "Database=" + database + ";" +
                                    "Uid=" + user + ";" +
                                    "Pwd=" + password + ";";
        }

        public bool requestConnection() 
        {
            myConnection = new MySqlConnection(mSqlConnectionString);
            try
            {
                myConnection.Open();
                return true;
                mConnectionActive = true;
            }
            catch(Exception e) 
            {
                System.Windows.MessageBox.Show(e.ToString(), "SQL-Error", System.Windows.MessageBoxButton.OK);
                try
                {
                    myConnection.Close();
                }
                catch (Exception ee)
                {
                    System.Windows.MessageBox.Show(ee.ToString(), "SQL-Error", System.Windows.MessageBoxButton.OK);
                }
                mConnectionActive = false;
                return false;
            }
        }

        public void close()
        {
            try
            {
                myConnection.Close();
            }
            catch (Exception e) 
            {
 
            }
        }

        /***********************************************************************
         * Main Code / Properties 
         * ********************************************************************/

        public bool ConnectionActive 
        {
            get { return mConnectionActive; }
            set { }
        }
    }
}
