using MySql.Data.MySqlClient;
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
        // TODO: не работает смена названий шапки
        public void UpdateDevelopers()
        {
            MySqlDataAdapter ad = new MySqlDataAdapter();
            ad.SelectCommand = new MySqlCommand("call developerstoredproc_SELECT", Connection);
            DataTable table = new DataTable();
            ad.Fill(table);
            testos.ItemsSource = table.DefaultView;
            //Villages_Grid_Table.Columns[1].Header = "Номер девелопера";
            //Villages_Grid_Table.Columns[1].Header = "Название девелопера";
            //Villages_Grid_Table.Columns[2].Header = "Годовой доход";
            //Villages_Grid_Table.Columns[3].Header = "Номер типа компании";
            //Villages_Grid_Table.Columns[4].Header = "Улица";
            //Villages_Grid_Table.Columns[5].Header = "Номер дома";
        }
        public void UpdateVillages()
        {
            MySqlDataAdapter ad = new MySqlDataAdapter();
            ad.SelectCommand = new MySqlCommand("call villagesstoredproc_SELECT", Connection);
            DataTable table = new DataTable();
            ad.Fill(table);
            //Villages_Grid_Table.ItemsSource = table.DefaultView;
            //Villages_Grid_Table.Columns[0].Header = "Номер поселка";
            //Villages_Grid_Table.Columns[1].Header = "Название поселка";
            //Villages_Grid_Table.Columns[2].Header = "Площадь поселка";
            //Villages_Grid_Table.Columns[3].Header = "Количество жителей";
            //Villages_Grid_Table.Columns[4].Header = "Номер девелопера";
        }
        public void UpdateVillageHouses()
        {
            MySqlDataAdapter ad = new MySqlDataAdapter();
            ad.SelectCommand = new MySqlCommand("call villagehousesstoredproc_SELECT", Connection);
            DataTable table = new DataTable();
            ad.Fill(table);
            VillageHouses_Grid_Table.ItemsSource = table.DefaultView;
        }
        public void UpdateHouseTypes()
        {
            MySqlDataAdapter ad = new MySqlDataAdapter();
            ad.SelectCommand = new MySqlCommand("call housetypesstoredproc_SELECT", Connection);
            DataTable table = new DataTable();
            ad.Fill(table);
            House_Types_DataGRID.ItemsSource = table.DefaultView;
        }
        public void UpdateCompanyTypes()
        {
            MySqlDataAdapter ad = new MySqlDataAdapter();
            ad.SelectCommand = new MySqlCommand("call companytypesstoredproc_SELECT", Connection);
            DataTable table = new DataTable();
            ad.Fill(table);
            Company_Types_DataGRID.ItemsSource = table.DefaultView;
        }
        public void UpdateAccountList()
        {
            MySqlDataAdapter ad = new MySqlDataAdapter();
            ad.SelectCommand = new MySqlCommand("call adminusersstoredproc_SELECT", Connection);
            DataTable table = new DataTable();
            ad.Fill(table);
            AdminAccountsGRID.ItemsSource = table.DefaultView;
        }
        public void RefreshAllTables()
        {
            UpdateDevelopers();
            UpdateVillages();
            UpdateVillageHouses();
            UpdateCompanyTypes();
            UpdateHouseTypes();
            UpdateAccountList();
        }

        private void testos_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshAllTables();
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