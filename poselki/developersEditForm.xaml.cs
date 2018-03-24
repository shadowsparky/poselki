using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace poselki
{
    public partial class developersEditForm : Window
    {
        private static WorkForm _WF;

        public MySqlConnection Connection { set; get; }
        public WorkForm WFSet
        {
            set { _WF = value; }
        }
        public developersEditForm()
        {
            InitializeComponent();
        }

        private void Developers_Update_Button_Click(object sender, RoutedEventArgs e)
        {
            var error = false;
            var UpToDateCommand = new MySqlCommand("call developerstoredproc_UPDATE(@DevNum, @Dev, @AI, @DevCorpNum, @Street, @HN)", Connection);
            UpToDateCommand.Parameters.AddWithValue("@DevNum", Dev_ID_TB.Text);
            UpToDateCommand.Parameters.AddWithValue("@Dev", DevName_TB.Text);
            UpToDateCommand.Parameters.AddWithValue("@DevCorpNum", Developers_Company_Type_TB.Text);
            UpToDateCommand.Parameters.AddWithValue("@AI", Dev_Income_TB.Text);
            UpToDateCommand.Parameters.AddWithValue("@Street", Village_Street_TB.Text);
            UpToDateCommand.Parameters.AddWithValue("@HN", Home_Number_TB.Text);
            try
            {
                UpToDateCommand.ExecuteScalar();
                MessageBox.Show("Запись отредактирована", "ОК", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                error = true;
            }
            if (!error)
            {
                _WF.UpdateDevelopers();
                _WF.UpdateCompanyTypes();
            }
        }
        private void Developers_Add_Button_Click(object sender, RoutedEventArgs e)
        {
            var error = false;
            var AddCommand = new MySqlCommand("select developerstoredfunc_INSERT(@DevNum, @Dev, @AI, @DevCorpNum, @Street, @HN)", Connection);
            AddCommand.Parameters.AddWithValue("@DevNum", Dev_ID_TB.Text);
            AddCommand.Parameters.AddWithValue("@Dev", DevName_TB.Text);
            AddCommand.Parameters.AddWithValue("@DevCorpNum", Developers_Company_Type_TB.Text);
            AddCommand.Parameters.AddWithValue("@AI", Dev_Income_TB.Text);
            AddCommand.Parameters.AddWithValue("@Street", Village_Street_TB.Text);
            AddCommand.Parameters.AddWithValue("@HN", Home_Number_TB.Text);
            try
            {
                AddCommand.ExecuteScalar();
                MessageBox.Show("Запись добавлена", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (!error)
            {
                _WF.UpdateDevelopers();
                _WF.UpdateCompanyTypes();
            }
        }
    }
}
