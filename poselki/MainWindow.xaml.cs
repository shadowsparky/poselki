using MySql.Data.MySqlClient;
using System;
using System.Reflection;
using System.Resources;
using System.Windows;


namespace poselki
{
    public partial class MainWindow : Window
    {
        private static MySqlConnection _connection;
        private WorkForm WF = new WorkForm(); 

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
                _connection = new MySqlConnection("Database = " + database + "; DataSource = " + datasource + "; User Id = " + userid + "; charset=cp866; Password = " + password);
                _connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                var t = ex.Message; 
                return false;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void authButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConnInit())
            {
                WF.Connection = _connection;
                string CheckRole = "#####";
                MySqlCommand checkrolecommand = new MySqlCommand("Select current_role", _connection);
                MySqlDataReader r = checkrolecommand.ExecuteReader();
                r.Read();
                try
                {
                    CheckRole = r.GetString(0);
                }
                catch (Exception)
                { } 
                if (CheckRole == "Developer")
                {
                    WF.control.Items.RemoveAt(0);
                    WF.control.Items.RemoveAt(2);
                    WF.control.Items.RemoveAt(2);
                    WF.control.Items.RemoveAt(2);
                    WF.control.Items.RemoveAt(2);
                }
                else if (CheckRole == "Dispatcher")
                {
                    WF.control.Items.RemoveAt(1);
                    WF.control.Items.RemoveAt(1);
                    WF.control.Items.RemoveAt(3);
                    WF.control.Items.RemoveAt(3);
                }
                else if (CheckRole == "JustUser")
                {
                    WF.control.Items.RemoveAt(3);
                    WF.control.Items.RemoveAt(3);
                    WF.control.Items.RemoveAt(3);
                    WF.control.Items.RemoveAt(3);
                    WF.testEditButton.Visibility = Visibility.Collapsed;
                    WF.VillageHouses_Edit_Button.Visibility = Visibility.Collapsed;
                    WF.Villages_Edit_Button.Visibility = Visibility.Collapsed;

                    Thickness SetMargin = WF.testos.Margin;
                    SetMargin.Bottom = 0;
                    WF.testos.Margin = SetMargin;
                    WF.VillageHouses_Grid_Table.Margin = SetMargin;
                    WF.Villages_Grid_Table.Margin = SetMargin;

                    WF.testos.IsReadOnly = true;
                    WF.VillageHouses_Grid_Table.IsReadOnly = true;
                    WF.Villages_Grid_Table.IsReadOnly = true;
                }
                else if (CheckRole == "Admin")
                {
                    WF.control.Items.RemoveAt(0);
                    WF.control.Items.RemoveAt(0);
                    WF.control.Items.RemoveAt(0);
                    WF.control.Items.RemoveAt(0);
                    WF.control.Items.RemoveAt(0);
                }
                else if (CheckRole == "Blocked")
                {
                    MessageBox.Show("Ваша учетная запись была заблокирована", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else 
                {
                    MessageBox.Show("Ваша учетная запись неправильно настроена. Дайте по голове своему системному администратору", "Внимательнее", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                MessageBox.Show("OK", "Все намана", MessageBoxButton.OK, MessageBoxImage.Information);
                WF.CurrentRole = CheckRole;
                r.Close();
                this.Close();
                WF.Show();
            }
            else
                MessageBox.Show("Неправильный логин или пароль", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}