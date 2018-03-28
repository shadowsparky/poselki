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
        private DataRowView BestCurrentItem;
        private BestErrors errors = new BestErrors();

        public WorkForm()
        {
            InitializeComponent();
        }

        // Обновление данных
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
                table.Columns[3].ColumnName = "Номер типа компании";
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

        // Разнообразные 
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

        /*Шаблоны*/
        private void MagicUniversalDeletingFromTable(string QueryString, DataGrid DG)
        {
            var t = (DataRowView)DG.CurrentItem;
            var DelCommand = new MySqlCommand(QueryString, Connection);
            DelCommand.Parameters.AddWithValue("Num", t[0]);
            try
            {
                DelCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(errors.getError(ex.Number.ToString()), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Запись удалена", "ОК", MessageBoxButton.OK, MessageBoxImage.Information);
            RefreshAllTables();
        }
        private void MagicUniversalEditingFromTable(string QueryString, string[] DataArgs)
        {
            QueryString += "(";
            string[] ParameterArg = new string[DataArgs.Length];
            for (int i = 0; i < DataArgs.Length; i++)
            {
                if (i != DataArgs.Length - 1)
                    QueryString += "@ARG" + i + ", ";
                else
                    QueryString += "@ARG" + i;
                ParameterArg[i] = "@ARG" + i;
            }
            QueryString += ")";
            var UpToDateCommand = new MySqlCommand(QueryString, Connection);
            for (int i = 0; i < DataArgs.Length; i++)
            {
                UpToDateCommand.Parameters.AddWithValue(ParameterArg[i], DataArgs[i]);
            }
            try
            {
                UpToDateCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(errors.getError(ex.Number.ToString()), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            RefreshAllTables();
            MessageBox.Show("Запись отредактирована", "ОК", MessageBoxButton.OK, MessageBoxImage.Information);
            BestCurrentItem = null;
        }
        public void MagicUniversalAddToTable(string QueryString, string[] DataArgs)
        {
            QueryString += "(";
            string[] ParameterArg = new string[DataArgs.Length];
            for (int i = 0; i < DataArgs.Length; i++)
            {
                if (i != DataArgs.Length - 1)
                    QueryString += "@ARG" + i + ", ";
                else
                    QueryString += "@ARG" + i;
                ParameterArg[i] = "@ARG" + i;
            }
            QueryString += ")";
            var AddCommand = new MySqlCommand(QueryString, Connection);
            for (int i = 0; i < DataArgs.Length; i++)
            {
                AddCommand.Parameters.AddWithValue(ParameterArg[i], DataArgs[i]);
            }
            try
            {
                AddCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(errors.getError(ex.Number.ToString()), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            RefreshAllTables();
            MessageBox.Show("Запись добавлена", "ОК", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Удаление + редактирование
        private void testos_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            { 
                var r = e.Key.ToString();
                if (r == "Delete")
                {
                    MagicUniversalDeletingFromTable("call developerstoredproc_DELETE(@Num)", testos);
                }
                else if (r == "Return")
                {
                    var t = BestCurrentItem;
                    string[] args = new string[t.Row.ItemArray.Length];
                    for (int i = 0; i < t.Row.ItemArray.Length; i++)
                        args[i] = t.Row.ItemArray[i].ToString();
                    MagicUniversalEditingFromTable("call developerstoredproc_UPDATE", args);
                }
                else if (r == "Escape")
                {
                    RefreshAllTables();
                }
            }
            catch (Exception)
            { errors.ExceptionProtector(); }
        }
        private void Villages_Grid_Table_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            { 
                var r = e.Key.ToString();
                if (r == "Delete")
                {
                    MagicUniversalDeletingFromTable("call villagesstoredproc_DELETE(@Num)", VillageHouses_Grid_Table);
                }
                else if (r == "Return")
                {
                    var t = BestCurrentItem;
                    string[] args = new string[t.Row.ItemArray.Length];
                    for (int i = 0; i < t.Row.ItemArray.Length; i++)
                        args[i] = t.Row.ItemArray[i].ToString();
                    MagicUniversalEditingFromTable("call villagesstoredproc_UPDATE", args);
                }
                else if (r == "Escape")
                {
                    RefreshAllTables();
                }
            }
            catch (Exception)
            { errors.ExceptionProtector(); }
        }
        private void VillageHouses_Grid_Table_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            { 
                var r = e.Key.ToString();
                if (r == "Delete")
                {
                    MagicUniversalDeletingFromTable("call villagehousesstoredproc_DELETE(@Num)", VillageHouses_Grid_Table);
                }
                else if (r == "Return")
                {
                    var t = BestCurrentItem;
                    string[] args = new string[t.Row.ItemArray.Length];
                    for (int i = 0; i < t.Row.ItemArray.Length; i++)
                        args[i] = t.Row.ItemArray[i].ToString();
                    MagicUniversalEditingFromTable("call villagehousesstoredproc_UPDATE", args);
                }
                else if (r == "Escape")
                {
                    RefreshAllTables();
                }
            }
            catch (Exception)
            { errors.ExceptionProtector(); }
        }
        private void Company_Types_DataGRID_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                var r = e.Key.ToString();
                if (r == "Delete")
                {
                    MagicUniversalDeletingFromTable("call companytypesstoredproc_DELETE(@Num)", Company_Types_DataGRID);
                }
                else if (r == "Return")
                {
                    var t = BestCurrentItem;
                    string[] args = new string[t.Row.ItemArray.Length];
                    for (int i = 0; i < t.Row.ItemArray.Length; i++)
                        args[i] = t.Row.ItemArray[i].ToString();
                    MagicUniversalEditingFromTable("call companytypesproc_UPDATE", args);
                }
                else if (r == "Escape")
                {
                    RefreshAllTables();
                }
            }
            catch (Exception)
            { errors.ExceptionProtector(); }
        }
        private void House_Types_DataGRID_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                var r = e.Key.ToString();
                if (r == "Delete")
                {
                    MagicUniversalDeletingFromTable("call housetypesstoredproc_DELETE(@Num)", House_Types_DataGRID);
                }
                else if (r == "Return")
                {
                    var t = BestCurrentItem;
                    string[] args = new string[t.Row.ItemArray.Length];
                    for (int i = 0; i < t.Row.ItemArray.Length; i++)
                        args[i] = t.Row.ItemArray[i].ToString();
                    MagicUniversalEditingFromTable("call housetypesproc_UPDATE", args);
                }
                else if (r == "Escape")
                {
                    RefreshAllTables();
                }
            } 
            catch(Exception)
            { errors.ExceptionProtector(); }
        }

        // Костыли, которые не нужны в паскале
        private void VillageHouses_Grid_Table_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                BestCurrentItem = (DataRowView)VillageHouses_Grid_Table.CurrentItem;
            }   
            catch (Exception)
            { errors.ExceptionProtector(); }
        }
        private void Company_Types_DataGRID_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            { 
                BestCurrentItem = (DataRowView)Company_Types_DataGRID.CurrentItem;
            }
            catch (Exception)
            { errors.ExceptionProtector(); }
        }
        private void House_Types_DataGRID_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                BestCurrentItem = (DataRowView)House_Types_DataGRID.CurrentItem;
            }
            catch (Exception)
            { errors.ExceptionProtector(); }
}
        private void AdminAccountsGRID_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                BestCurrentItem = (DataRowView)AdminAccountsGRID.CurrentItem;
            }
            catch (Exception)
            { errors.ExceptionProtector(); }
        }
        private void testos_CellEditEnding_1(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                BestCurrentItem = (DataRowView)testos.CurrentItem;
            }
            catch (Exception)
            { errors.ExceptionProtector();
    }
}
        private void Villages_Grid_Table_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            { 
                BestCurrentItem = (DataRowView)Villages_Grid_Table.CurrentItem;
            }
            catch (Exception)
            { errors.ExceptionProtector(); }
        }
    }
}