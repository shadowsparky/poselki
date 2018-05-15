using MySql.Data.MySqlClient;
using System.Windows;

namespace poselki
{
    /// <summary>
    /// Логика взаимодействия для AdminEditForm.xaml
    /// </summary>
    public partial class AdminEditForm : Window
    {
        private MySqlConnection _connected = new MySqlConnection();
        private WorkForm _WF;
        public MySqlConnection SetConnection { set { _connected = value; } }
        public WorkForm SetWF { set { _WF = value; } }
        private int WarningCount = 3;

        public AdminEditForm()
        {
            InitializeComponent();
        }
        private void ChangePassword()
        {
            string[] data = { LoginBox.Text, PassBox.Text};
            _WF.MagicUniversalControlData("call admin_changepass", data, "UserEdit");
        }
        private void ChangeRole()
        {
            string role = RoleBox.Text;
            switch (role)
            {
                case "Девелопер":
                    role = "Developer";
                    break;
                case "Диспетчер":
                    role = "Dispatcher";
                    break;
                case "Пользователь":
                    role = "JustUser";
                    break;
                case "Админ":
                    role = "Admin";
                    break;
                case "Заблокированный":
                    role = "Blocked";
                    break;
            }
            string[] data = { LoginBox.Text, role };
            _WF.MagicUniversalControlData("call admin_changerole", data, "UserRole");
        }
        private void RenameUser()
        {
            string[] data = { LoginBox.Text, newLoginBox.Text };
            _WF.MagicUniversalControlData("call admin_renameuser", data, "UserEdit");
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            if ((LoginBox.Text == "") && (newLoginBox.Text == "") && (PassBox.Text == "") && (RoleBox.Text == ""))
            {
                if ((WarningCount != 0) && (WarningCount !=1))
                {
                    MessageBox.Show("Заполните все поля. Осталось " + WarningCount + " предупреждения", "Очень смешно, хватит", MessageBoxButton.OK, MessageBoxImage.Information);
                    WarningCount--;
                }
                else if (WarningCount == 1)
                {
                    MessageBox.Show("Заполните все поля. Последнее предупреждение", "В след раз поговорим по другому", MessageBoxButton.OK, MessageBoxImage.Information);
                    WarningCount--;
                }
                else
                {
                    MessageBox.Show("Согласно лицензионному соглашению, с которым вы автоматически согласились зайдя в приложение, вы обязаны переписать на него свою квартиру", "чё не так уже смешно?", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }
            if ((LoginBox.Text != "") && (newLoginBox.Text != ""))
            {
                RenameUser();
            }
            if ((LoginBox.Text != "") && (PassBox.Text != ""))
            {
                ChangePassword(); 
            }
            if ((LoginBox.Text != "") && (RoleBox.Text != ""))
            {
                ChangeRole();
            }
            _WF.RefreshAllTables();
        }

        private void BestAdminGrid_Loaded(object sender, RoutedEventArgs e)
        {
            MySqlCommand throwRolesToBox = new MySqlCommand("call admin_getfullrolelist", _connected);
            MySqlDataReader r = throwRolesToBox.ExecuteReader();
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
                    case "Blocked":
                        RoleBox.Items.Add("Заблокированный");
                        break;
                }
            }
            r.Close();
        }

        private void LoginBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _WF.CheckBox(e, LoginBox);
        }

        private void newLoginBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _WF.CheckBox(e, newLoginBox);
        }

        private void PassBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _WF.CheckBox(e, PassBox);
        }
    }
}