using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace poselki
{
    public partial class WorkForm : Window
    {
        public MySqlConnection Connection { set; get; }

        public WorkForm()
        {
            InitializeComponent();
        }

        public bool UpdateDevelopers()
        {
            try
            {
                MySqlDataAdapter ad = new MySqlDataAdapter();
                ad.SelectCommand = new MySqlCommand("call developerstoredproc_SELECT", Connection);
                DataTable table = new DataTable();
                ad.Fill(table);
                table.Columns[0].ColumnName = "Номер девелопера";
                table.Columns[1].ColumnName = "Название девелопера";
                table.Columns[2].ColumnName = "Годовой доход";
                table.Columns[3].ColumnName = "Номер компании девелопера";
                table.Columns[4].ColumnName = "Улица";
                table.Columns[5].ColumnName = "Номер улицы";

                testos.ItemsSource = table.DefaultView;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateVillages()
        {
            try
            { 
                MySqlDataAdapter ad = new MySqlDataAdapter();
                ad.SelectCommand = new MySqlCommand("call villagesstoredproc_SELECT", Connection);
                DataTable table = new DataTable();
                ad.Fill(table);
                table.Columns[0].ColumnName = "Номер поселка";
                table.Columns[1].ColumnName = "Название";
                table.Columns[2].ColumnName = "Площадь";
                table.Columns[3].ColumnName = "Количество жителей";
                table.Columns[4].ColumnName = "Номер девелопера";
                Villages_Grid_Table.ItemsSource = table.DefaultView;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateVillageHouses()
        {
            try
            {
                MySqlDataAdapter ad = new MySqlDataAdapter();
                ad.SelectCommand = new MySqlCommand("call villagehousesstoredproc_SELECT", Connection);
                DataTable table = new DataTable();
                ad.Fill(table);
                table.Columns[0].ColumnName = "Идентификатор дома";
                table.Columns[1].ColumnName = "Номер поселка";
                table.Columns[2].ColumnName = "Улица";
                table.Columns[3].ColumnName = "Номер дома";
                table.Columns[4].ColumnName = "№ типа дома";
                table.Columns[5].ColumnName = "Площадь дома";
                table.Columns[6].ColumnName = "Количество этажей";
                VillageHouses_Grid_Table.ItemsSource = table.DefaultView;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateHouseTypes()
        {
            try
            {
                MySqlDataAdapter ad = new MySqlDataAdapter();
                ad.SelectCommand = new MySqlCommand("call housetypesstoredproc_SELECT", Connection);
                DataTable table = new DataTable();
                ad.Fill(table);
                table.Columns[0].ColumnName = "№ типа дома";
                table.Columns[1].ColumnName = "Тип дома";
                House_Types_DataGRID.ItemsSource = table.DefaultView;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateCompanyTypes()
        {
            try
            {
                MySqlDataAdapter ad = new MySqlDataAdapter();
                ad.SelectCommand = new MySqlCommand("call companytypesstoredproc_SELECT", Connection);
                DataTable table = new DataTable();
                ad.Fill(table);
                table.Columns[0].ColumnName = "№ типа компании";
                table.Columns[1].ColumnName = "Тип компании";
                Company_Types_DataGRID.ItemsSource = table.DefaultView;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdateAccountList()
        {
            try
            {
                //MySqlDataAdapter ad = new MySqlDataAdapter();
                //ad.SelectCommand = new MySqlCommand("call adminusersstoredproc_SELECT", Connection);
                //DataTable table = new DataTable();
                //ad.Fill(table);
                //AdminAccountsGRID.ItemsSource = table.DefaultView;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool RefreshAllTables()
        {
            bool error = false;
            if (!UpdateDevelopers()) error = true;
            if (!UpdateVillages()) error = true;
            if (!UpdateVillageHouses()) error = true;
            if (!UpdateHouseTypes()) error = true;
            if (!UpdateCompanyTypes()) error = true;
            if (error) return false;
            else
                return true;
        }

        private void testos_Loaded(object sender, RoutedEventArgs e)
        {
            if (!RefreshAllTables())
                MessageBox.Show("При загрузке данных произошла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void testEditButton_Click(object sender, RoutedEventArgs e)
        {
            developersEditForm DEF = new developersEditForm();
            DEF.Connection = Connection;
            DEF.WFSet = this;
            DEF.Show();
        }
        private void Villages_Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            villagesEditForm VEF = new villagesEditForm();
            VEF.Connection = Connection;
            VEF.WFSet = this;
            VEF.Show();
        }
        private void VillageHouses_Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            villagehousesEditForm VHEF = new villagehousesEditForm();
            VHEF.Connection = Connection;
            VHEF.WFSet = this;
            VHEF.Show();
        }
        private void ADMIN_EditUsersBUTTON_Click(object sender, RoutedEventArgs e)
        {

        }
        // Тестовые рабочие процедуры
        private void testos_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var r = e.Key.ToString();
            if (r == "Delete")
            {
                var t = (DataRowView)testos.CurrentItem;
                var DelCommand = new MySqlCommand("call developerstoredproc_DELETE(@DevNum)", Connection);
                DelCommand.Parameters.AddWithValue("DevNum", t[0].ToString());
                try
                {
                    DelCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MessageBox.Show("Запись удалена", "ОК", MessageBoxButton.OK, MessageBoxImage.Information);
                UpdateDevelopers();
            }
        }

        // Тестовые нерабочие процедуры
        private void testos_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        { 
            //e.EditingElement.SetValue();
            var t = (DataRowView)testos.CurrentItem;
            var UpToDateCommand = new MySqlCommand("call developerstoredproc_UPDATE(@DevNum, @Dev, @AI, @DevCorpNum, @Street, @HN)", Connection);
            try
            {
                UpToDateCommand.Parameters.AddWithValue("@DevNum", t[0].ToString());
                UpToDateCommand.Parameters.AddWithValue("@Dev", t[1].ToString());
                UpToDateCommand.Parameters.AddWithValue("@DevCorpNum", t[2].ToString());
                UpToDateCommand.Parameters.AddWithValue("@AI", t[3].ToString());
                UpToDateCommand.Parameters.AddWithValue("@Street", t[4].ToString());
                UpToDateCommand.Parameters.AddWithValue("@HN", t[5].ToString());
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //UpToDateCommand.Parameters.AddWithValue("@DevNum", Convert.ToInt64(t[0].ToString()));
            //UpToDateCommand.Parameters.AddWithValue("@Dev", t[1].ToString());
            //UpToDateCommand.Parameters.AddWithValue("@DevCorpNum", Convert.ToInt64(t[2].ToString()));
            //UpToDateCommand.Parameters.AddWithValue("@AI", Convert.ToInt64(t[3].ToString()));
            //UpToDateCommand.Parameters.AddWithValue("@Street", t[4].ToString());
            //UpToDateCommand.Parameters.AddWithValue("@HN", Convert.ToInt64(t[5].ToString()));
            try
            {
                UpToDateCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            UpdateDevelopers();
            UpdateCompanyTypes();
        }

    }
}