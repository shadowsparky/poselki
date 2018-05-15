using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;

namespace poselki
{
    public partial class AdminForm : Window
    {
        private MySqlConnection _connected = new MySqlConnection();
        private WorkForm _WF;
        public MySqlConnection SetConnection { set { _connected = value; } }
        public WorkForm SetWF { set { _WF = value; } }
        public AdminForm()
        {
            InitializeComponent();
        }

        private void BestAdminGrid_Loaded(object sender, RoutedEventArgs e)
        {
            MySqlCommand punchRolesToBox = new MySqlCommand("call admin_getrolelist", _connected);
            MySqlDataReader r = punchRolesToBox.ExecuteReader();
            while (r.Read())
            {
                switch (r.GetString(0))
                {
                    case "Developer":
                        RoleBox.Items.Add("Девелопер");
                        break;
                    case "Dispatcher":
                        RoleBox.Items.Add("Диспетчер");
                        break;
                    case "JustUser":
                        RoleBox.Items.Add("Пользователь");
                        break;
                    case "Admin":
                        RoleBox.Items.Add("Админ");
                        break;
                }
            }
            r.Close();
        }
        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            if ((LoginBox.Text != "") && (PassBox.Text != "") && (RoleBox.Text != ""))
            {
                string queryString = "call admin_createuser";
                string Role = "";
                switch (RoleBox.Text)
                {
                    case "Девелопер":
                        Role = "Developer";
                        break;
                    case "Диспетчер":
                        Role = "Dispatcher";
                        break;
                    case "Пользователь":
                        Role = "JustUser";
                        break;
                    case "Админ":
                        Role = "Admin";
                        break;
                }
                string[] data = { LoginBox.Text, PassBox.Text, Role};

                _WF.MagicUniversalControlData(queryString, data, "UserAdd");
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void LoginBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
                
        }



        private void LoginBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _WF.CheckBox(e, LoginBox);
        }

        private void PassBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _WF.CheckBox(e, PassBox);
        }
    }
}