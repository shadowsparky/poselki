using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;

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
    }
}