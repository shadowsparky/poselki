using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace poselki
{
    public partial class villagesEditForm : Window
    {
        private WorkForm _WF;

        public MySqlConnection Connection { set; get; }
        public WorkForm WFSet
        {
            set { _WF = value; }
        }
        public villagesEditForm()
        {
            InitializeComponent();
        }

        // TODO: не работает обновление
        private void Village_Edit_BUTTON_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;
            var UpToDateCommand = new MySqlCommand("call villagesstoredproc_UPDATE(@Village_Number_IN, @Village_Name_IN, @Village_Area_IN, @Residents_Count_IN, @Developer_Number_IN)", Connection);
            UpToDateCommand.Parameters.AddWithValue("@Village_Number_IN", Village_Number_TB.Text);
            UpToDateCommand.Parameters.AddWithValue("@Village_Name_IN", Village_Name_TB.Text);
            UpToDateCommand.Parameters.AddWithValue("@Village_Area_IN", Village_Area_TB.Text);
            UpToDateCommand.Parameters.AddWithValue("@Residents_Count_IN", Village_Residents_Count_TB.Text);
            UpToDateCommand.Parameters.AddWithValue("@Developer_Number_IN", Village_Developer_Number.Text);
            try
            {
                UpToDateCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка");
                error = true;
            }
            if (!error)
                MessageBox.Show("Запись отредактирована.");
            _WF.UpdateVillages();
        }
        private void Village_Add_BUTTON_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;
            var AddCommand = new MySqlCommand("select villagesstoredproc_INSERT(@Village_Number_IN, @Village_Name_IN, @Village_Area_IN, @Residents_Count_IN, @Developer_Number_IN)", Connection);
            AddCommand.Parameters.AddWithValue("@Village_Number_IN", Village_Number_TB.Text);
            AddCommand.Parameters.AddWithValue("@Village_Name_IN", Village_Name_TB.Text);
            AddCommand.Parameters.AddWithValue("@Village_Area_IN", Village_Area_TB.Text);
            AddCommand.Parameters.AddWithValue("@Residents_Count_IN", Village_Residents_Count_TB.Text);
            AddCommand.Parameters.AddWithValue("@Developer_Number_IN", Village_Developer_Number.Text);
            try
            {
                AddCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка");
                error = true;
            }
            if (!error)
                MessageBox.Show("Запись добавлена.");
            _WF.UpdateVillages();
        }
        private void Village_Developer_Number_Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;
            var DeleteCommand = new MySqlCommand("call villagesstoredproc_DELETE(@Village_Number_IN)", Connection);
            DeleteCommand.Parameters.AddWithValue("@Village_Number_IN", Village_Developer_Number_Delete_TB.Text);
            try
            {
                DeleteCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка");
                error = true;
            }
            if (!error)
                MessageBox.Show("Запись добавлена.");
            _WF.UpdateVillages();
        }
    } 
}
