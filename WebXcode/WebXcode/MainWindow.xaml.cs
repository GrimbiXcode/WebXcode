using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebXcode
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /***********************************************************************
         * Definition of attributes
         * ********************************************************************/
        private string mAdress;
        private string mDatabase;
        private string mUser;
        private string mPassword;
        private string mPasswordEncoded;
        private string mKey;

        private MySQLClient sqlClient;

        /***********************************************************************
         * Main Code / System Methodes
         * ********************************************************************/
        public MainWindow()
        {
            InitializeComponent();

            /* Load Settings */
            mAdress = Properties.Settings.Default.adress;
            _tboAdress.Text = mAdress;
            mDatabase = Properties.Settings.Default.database;
            _tboDatabase.Text = mDatabase;
            mUser = Properties.Settings.Default.user;
            _tboUser.Text = mUser;
            mPassword = Properties.Settings.Default.password;
            mKey = Properties.Settings.Default.key;
            mPasswordEncoded = decodePassword(mPassword, mKey);
            _pboPassword.Password = mPasswordEncoded;

            sqlClient = new MySQLClient(mAdress, mDatabase, mUser, mPasswordEncoded);
        }

        private string decodePassword(string password, string key) 
        {
            return password;
        }

        private void renewSqlData()
        {
            mAdress = _tboAdress.Text;
            mDatabase = _tboDatabase.Text;
            mUser = _tboUser.Text;
            mPasswordEncoded = _pboPassword.Password;
        }

        /***********************************************************************
         * Main Code / Event Methodes
         * ********************************************************************/
        private void _btnCheckConnection_Click(object sender, RoutedEventArgs e)
        {
            renewSqlData();

            if (sqlClient.ConnectionActive == true)
            {
                _tboConnectionState.Text = "Verbunden";
                    _tboConnectionState.Background = Brushes.LightGreen;
            }
            else
            {
                sqlClient.setUp(mAdress, mDatabase, mUser, mPasswordEncoded);
                if (sqlClient.requestConnection() == true)
                {
                    _tboConnectionState.Text = "Verbunden";
                    _tboConnectionState.Background = Brushes.LightGreen;
                }
                else
                {
                    _tboConnectionState.Text = "Getrennt";
                    _tboConnectionState.Background = Brushes.Salmon;
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            /* Save Settings */
            Properties.Settings.Default.adress = mAdress;
            Properties.Settings.Default.user = mUser;
            Properties.Settings.Default.password = mPasswordEncoded;
            Properties.Settings.Default.key = mKey;
            Properties.Settings.Default.Save();
            sqlClient.close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            /* Save Settings *
            Properties.Settings.Default.adress = mAdress;
            Properties.Settings.Default.user = mUser;
            Properties.Settings.Default.password = mPasswordEncoded;
            Properties.Settings.Default.key = mKey;
            Properties.Settings.Default.Save();*/
            sqlClient.close();
        }
    }
}
