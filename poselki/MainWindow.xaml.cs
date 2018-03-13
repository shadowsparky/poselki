using MySql.Data.MySqlClient;
using System;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows;


namespace poselki
{
    public partial class MainWindow : Window
    {
        private static MySqlConnection _connection;
        private static WorkForm WF; 

        public MySqlConnection DataSource
        {
            get { return _connection; }
        }

        private bool ConnInit()
        {
           try
           {
                ResourceManager RM = new ResourceManager("poselki.connection", Assembly.GetExecutingAssembly());
                string datasource = RM.GetString("Datasource");
                string database = RM.GetString("DatabaseName");
                string userid = loginBox.Text;
                string password = passBox.Password;
                RM = null;
                _connection = new MySqlConnection("Database = " + database + "; DataSource = " + datasource + "; User Id = " + userid + "; Password = " + password);
                _connection.Open();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        private void mainGrid_Initialized(object sender, EventArgs e)
        { 
        }


        public MainWindow()
        {
            InitializeComponent();
        }

        private void authButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConnInit())
            {
                MessageBox.Show("OK", "Все намана", MessageBoxButton.OK, MessageBoxImage.Information);
                WF = new WorkForm();
                WF.Connection = _connection;
                this.Close();
                WF.Show();
            }
            else
                MessageBox.Show("Неправильный логин или пароль", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
