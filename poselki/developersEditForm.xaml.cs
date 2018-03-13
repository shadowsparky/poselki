using MySql.Data.MySqlClient;
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
using System.Windows.Shapes;

namespace poselki
{
    /// <summary>
    /// Логика взаимодействия для developersEditForm.xaml
    /// </summary>
    public partial class developersEditForm : Window
    {

        private static MySqlConnection _connection;
        private static WorkForm _WF; 

        public MySqlConnection Connection
        {
            set { _connection = value; }
            get { return _connection; }
        }

        public WorkForm WFSet
        {
            set { _WF = value; }
        }

        public developersEditForm()
        {
            InitializeComponent();
        }

        private int returnCompanyType()
        {
            int result = 0;

            if (Private_Company_Type_RB.IsChecked == true)
            {
                result = 2;
            }
            else
                result = 1;
            return result;
        }

        private void Private_Company_Type_RB_Checked(object sender, RoutedEventArgs e)
        {

        }



        private void Developers_Update_Button_Click(object sender, RoutedEventArgs e)
        {
            var UpToDateCommand = new MySqlCommand("call developerstoredproc_UPDATE(@DevNum, @Dev, @AI, @DevCorpNum, @Street, @HN)", _connection);
            UpToDateCommand.Parameters.AddWithValue("@DevNum", Dev_ID_TB.Text);
            UpToDateCommand.Parameters.AddWithValue("@Dev", DevName_TB.Text);
            UpToDateCommand.Parameters.AddWithValue("@DevCorpNum", returnCompanyType());
            UpToDateCommand.Parameters.AddWithValue("@AI", Dev_Income_TB.Text);
            UpToDateCommand.Parameters.AddWithValue("@Street", Village_Street_TB.Text);
            UpToDateCommand.Parameters.AddWithValue("@HN", Home_Number_TB.Text);
            UpToDateCommand.ExecuteScalarAsync();
            _WF.UpdateDevelopers();
        }

        private void DelDevButton_Click(object sender, RoutedEventArgs e)
        {
            var DelCommand = new MySqlCommand("call developerstoredproc_DELETE(@DevNum)", _connection);
            DelCommand.Parameters.AddWithValue("@DevNum", DevNumDel_TB.Text);
            DelCommand.ExecuteScalarAsync();
            _WF.UpdateDevelopers();
        }

        private void Developers_Add_Button_Click(object sender, RoutedEventArgs e)
        {
            var AddCommand = new MySqlCommand("select developerstoredfunc_INSERT(@DevNum, @Dev, @AI, @DevCorpNum, @Street, @HN)", _connection);
            AddCommand.Parameters.AddWithValue("@DevNum", Dev_ID_TB.Text);
            AddCommand.Parameters.AddWithValue("@Dev", DevName_TB.Text);
            AddCommand.Parameters.AddWithValue("@DevCorpNum", returnCompanyType());
            AddCommand.Parameters.AddWithValue("@AI", Dev_Income_TB.Text);
            AddCommand.Parameters.AddWithValue("@Street", Village_Street_TB.Text);
            AddCommand.Parameters.AddWithValue("@HN", Home_Number_TB.Text);
            AddCommand.ExecuteScalarAsync();
            _WF.UpdateDevelopers();
        }
    }
}
