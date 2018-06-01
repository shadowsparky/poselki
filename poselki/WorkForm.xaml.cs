using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace poselki
{
    public partial class WorkForm : Window, INterfaceInToFace
    {
        public MySqlConnection Connection { set; get; }
        private DataRowView BestCurrentItem;
        private BestErrors errors = new BestErrors();
        public string CurrentRole { get; set; }

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

        public bool UpdateLogs()
        {
            try
            {
                MySqlDataAdapter ad = new MySqlDataAdapter();
                ad.SelectCommand = new MySqlCommand("call admin_getlogs()", Connection);
                DataTable table = new DataTable();
                ad.Fill(table);
                table.Columns[0].ColumnName = "Номер лога";
                table.Columns[1].ColumnName = "Логин";
                table.Columns[2].ColumnName = "Действие";
                table.Columns[3].ColumnName = "Номер идентфикатора";
                table.Columns[4].ColumnName = "Новые данные";
                table.Columns[5].ColumnName = "Старые данные";
                table.Columns[6].ColumnName = "Дата и время";
                AdminLogsGRID.ItemsSource = table.DefaultView;
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
                MySqlDataAdapter ad = new MySqlDataAdapter();
                ad.SelectCommand = new MySqlCommand("call useradmin_select", Connection);
                DataTable table = new DataTable();
                ad.Fill(table);
                table.Columns[0].ColumnName = "Хост";
                table.Columns[1].ColumnName = "Логин";
                table.Columns[2].ColumnName = "Роль";
                AdminAccountsGRID.ItemsSource = table.DefaultView;
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
            if (CurrentRole == "Developer")
            {
                if (!UpdateVillages()) error = true;
                if (!UpdateVillageHouses()) error = true;
            }
            if (CurrentRole == "Dispatcher")
            {
                if (!UpdateHouseTypes()) error = true;
                if (!UpdateCompanyTypes()) error = true;
                if (!UpdateDevelopers()) error = true;
            }
            if (CurrentRole == "JustUser")
            {
                if (!UpdateDevelopers()) error = true;
                if (!UpdateVillages()) error = true;
                if (!UpdateVillageHouses()) error = true;
            }
            if (CurrentRole == "Admin")
            {
                if (!UpdateAccountList()) error = true;
                if (!UpdateLogs()) error = true;
            }
            if (error) return false;
            else
                return true;
        }

        // Разнообразные 
        private void testos_Loaded(object sender, RoutedEventArgs e)
        {
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
            AdminForm AF = new AdminForm();
            AF.SetConnection = Connection;
            AF.SetWF = this;
            AF.Show();
        }

        private void ADMIN_BestEditUsersBUTTON_Click(object sender, RoutedEventArgs e)
        {
            AdminEditForm AEF = new AdminEditForm();
            AEF.SetConnection = Connection;
            AEF.SetWF = this;
            AEF.Show();
        }

        /*Шаблон*/
        public void MagicUniversalControlData(string QueryString, string[] DataArgs, string userControl)
        {
            if (userControl != "Delete")
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
                var BestCommand = new MySqlCommand(QueryString, Connection);
                for (int i = 0; i < DataArgs.Length; i++)
                {
                    BestCommand.Parameters.AddWithValue(ParameterArg[i], DataArgs[i]);
                }
                try
                {
                    BestCommand.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(errors.getError(ex.Number.ToString()), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                RefreshAllTables();
                if (userControl == "Add")
                    MessageBox.Show("Запись добавлена", "ОК", MessageBoxButton.OK, MessageBoxImage.Information);
                else if (userControl == "UserAdd")
                    MessageBox.Show("Пользователь добавлен", "ОК", MessageBoxButton.OK, MessageBoxImage.Information);
                else if (userControl == "UserEdit")
                    MessageBox.Show("Пользователь отредактирован", "ОК", MessageBoxButton.OK, MessageBoxImage.Information);
                else if (userControl == "UserRole")
                    MessageBox.Show("Роль пользователя обновлена", "ОК", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Запись отредактирована", "ОК", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                var DelCommand = new MySqlCommand(QueryString, Connection);
                DelCommand.Parameters.AddWithValue("Num", DataArgs[0]);
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
        }

        // Удаление + редактирование
        private void testos_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                var r = e.Key.ToString();
                if (r == "Delete")
                {
                    var t = (DataRowView)testos.CurrentItem;
                    string[] args = { t[0].ToString() };
                    MagicUniversalControlData("call developerstoredproc_DELETE(@Num)", args, "Delete");
                }
                else if (r == "Return")
                {
                    var t = BestCurrentItem;
                    if (t != null)
                    {
                        string[] args = new string[t.Row.ItemArray.Length];
                        for (int i = 0; i < t.Row.ItemArray.Length; i++)
                            args[i] = t.Row.ItemArray[i].ToString();
                        MagicUniversalControlData("call developerstoredproc_UPDATE", args, "Edit");
                    }
                    else
                        MessageBox.Show("Редактировать ключи запрещено", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    var t = (DataRowView)Villages_Grid_Table.CurrentItem;
                    string[] args = { t[0].ToString() };
                    MagicUniversalControlData("call villagesstoredproc_DELETE(@Num)", args, "Delete");
                }
                else if (r == "Return")
                {
                    var t = BestCurrentItem;
                    if (t != null)
                    {
                        string[] args = new string[t.Row.ItemArray.Length];
                        for (int i = 0; i < t.Row.ItemArray.Length; i++)
                            args[i] = t.Row.ItemArray[i].ToString();
                        MagicUniversalControlData("call villagesstoredproc_UPDATE", args, "Edit");
                    }
                    else
                        MessageBox.Show("Редактировать ключи запрещено", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    var t = (DataRowView)VillageHouses_Grid_Table.CurrentItem;
                    string[] args = { t[0].ToString() };
                    MagicUniversalControlData("call villagehousesstoredproc_DELETE(@Num)", args, "Delete");
                }
                else if (r == "Return")
                {
                    var t = BestCurrentItem;
                    if (t != null)
                    {
                        string[] args = new string[t.Row.ItemArray.Length];
                        for (int i = 0; i < t.Row.ItemArray.Length; i++)
                            args[i] = t.Row.ItemArray[i].ToString();
                        MagicUniversalControlData("call villagehousesstoredproc_UPDATE", args, "Edit");
                    }
                    else
                        MessageBox.Show("Редактировать ключи запрещено", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    var t = (DataRowView)Company_Types_DataGRID.CurrentItem;
                    string[] args = { t[0].ToString() };
                    MagicUniversalControlData("call companytypesstoredproc_DELETE(@Num)", args, "Delete");
                }
                else if (r == "Return")
                {
                    var t = BestCurrentItem;
                    if (t != null)
                    {
                        string[] args = new string[t.Row.ItemArray.Length];
                        for (int i = 0; i < t.Row.ItemArray.Length; i++)
                            args[i] = t.Row.ItemArray[i].ToString();
                        MagicUniversalControlData("call companytypesproc_UPDATE", args, "Edit");
                    }
                    else
                        MessageBox.Show("Редактировать ключи запрещено", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    var t = (DataRowView)House_Types_DataGRID.CurrentItem;
                    string[] args = { t[0].ToString() };
                    MagicUniversalControlData("call housetypesstoredproc_DELETE(@Num)", args, "Delete");
                }
                else if (r == "Return")
                {
                    var t = BestCurrentItem;
                    if (t != null)
                    {
                        string[] args = new string[t.Row.ItemArray.Length];
                        for (int i = 0; i < t.Row.ItemArray.Length; i++)
                            args[i] = t.Row.ItemArray[i].ToString();
                        MagicUniversalControlData("call housetypesproc_UPDATE", args, "Edit");
                    }
                    else
                    {
                        MessageBox.Show("Редактировать ключи запрещено", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else if (r == "Escape")
                {
                    RefreshAllTables();
                }
            } 
            catch(Exception)
            { errors.ExceptionProtector(); }
        }
        private void AdminAccountsGRID_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                var r = e.Key.ToString();
                if (r == "Delete")
                {
                    var t = (DataRowView)AdminAccountsGRID.CurrentItem;
                    string[] args = { t[1].ToString() };
                    MagicUniversalControlData("call admin_dropuser(@Num)", args, "Delete");
                }
                else if (r == "Escape")
                {
                    RefreshAllTables();
                }
            }
            catch (Exception)
            { errors.ExceptionProtector(); }
        }
        private DataRowView BlockUpdate(object sender, DataGridCellEditEndingEventArgs e, int[] itemarr)
        {
            DataRowView TMPGridRow = null;
            if (itemarr[0] != -1)
            {
                for (int i = 0; i < itemarr.Length; i++)
                {
                    if (e.Column.DisplayIndex != i)
                    {
                        try
                        {
                            TMPGridRow = (DataRowView)(sender as DataGrid).CurrentItem;
                        }
                        catch (Exception)
                        { }
                    }
                    else
                    {
                        TMPGridRow = null;
                        return TMPGridRow;
                    }
                }
            }
            else
            {
                try
                {
                    TMPGridRow = (DataRowView)(sender as DataGrid).CurrentItem;
                }
                catch (Exception)
                { }
            }
            return TMPGridRow;
        }
        // Костыли, которые не нужны в паскале
        private void VillageHouses_Grid_Table_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                int[] itemarr = { 1 };
                BestCurrentItem = BlockUpdate(sender, e, itemarr);
            }   
            catch (Exception)
            { errors.ExceptionProtector(); }
        }
        private void Company_Types_DataGRID_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            { 
                int[] itemarr = { 1 };
                BestCurrentItem = BlockUpdate(sender, e, itemarr);
            }
            catch (Exception)
            { errors.ExceptionProtector(); }
        }
        private void House_Types_DataGRID_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                int[] itemarr = { 1 };
                BestCurrentItem = BlockUpdate(sender, e, itemarr);
            }
            catch (Exception)
            { errors.ExceptionProtector(); }
        }
        private void AdminAccountsGRID_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                int[] itemarr = { 1 };
                BestCurrentItem = BlockUpdate(sender, e, itemarr);
            }
            catch (Exception)
            { errors.ExceptionProtector(); }
        }
        private void testos_CellEditEnding_1(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                int[] itemarr = { 1 };
                BestCurrentItem = BlockUpdate(sender, e, itemarr);
            }
            catch (Exception)
            { errors.ExceptionProtector();}
        }
        private void Villages_Grid_Table_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                int[] itemarr = { 1 };
                BestCurrentItem = BlockUpdate(sender, e, itemarr);
            }
            catch (Exception)
            { errors.ExceptionProtector(); }
        }

        private void BestGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (!RefreshAllTables())
                MessageBox.Show("При загрузке данных произошла ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void AdminAccountsGRID_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        { 
        }

        public void CheckBox(System.Windows.Controls.TextChangedEventArgs e, TextBox TB)
        {
            TB.Text = TB.Text.Replace(" ", string.Empty);
            TB.Text = TB.Text.Replace("'", string.Empty);
            TB.Text = TB.Text.Replace('"', ' ');
            TB.Text = TB.Text.Replace("*", string.Empty);
            TB.Text = TB.Text.Replace("/", string.Empty);
            TB.Text = TB.Text.Replace(";", string.Empty);
            TB.Text = TB.Text.Replace("@", string.Empty);
            TB.Text = TB.Text.Replace("!", string.Empty);
            TB.Text = TB.Text.Replace("#", string.Empty);
            TB.Text = TB.Text.Replace("$", string.Empty);
            TB.Text = TB.Text.Replace("№", string.Empty);
            TB.Text = TB.Text.Replace("%", string.Empty);
            TB.Text = TB.Text.Replace("^", string.Empty);
            TB.Text = TB.Text.Replace(":", string.Empty);
            TB.Text = TB.Text.Replace("?", string.Empty);
            TB.Text = TB.Text.Replace("*", string.Empty);
            TB.Text = TB.Text.Replace("(", string.Empty);
            TB.Text = TB.Text.Replace(")", string.Empty);
            TB.Text = TB.Text.Replace(",", string.Empty);
            TB.Text = TB.Text.Replace(".", string.Empty);
            TB.Text = TB.Text.Replace("<", string.Empty);
            TB.Text = TB.Text.Replace(">", string.Empty);
            TB.Text = TB.Text.Replace("[", string.Empty);
            TB.Text = TB.Text.Replace("]", string.Empty);
            TB.Text = TB.Text.Replace("{", string.Empty);
            TB.Text = TB.Text.Replace("}", string.Empty);
            TB.Text = TB.Text.Replace("-", string.Empty);
            TB.Text = TB.Text.Replace("|", string.Empty);
            TB.Text = TB.Text.Replace("\\", string.Empty);
            TB.SelectionStart = TB.Text.Length;
        }

        private void ExportXml()
        {
            try
            {
                SaveFileDialog SFD = new SaveFileDialog();
                SFD.Filter = "xml файл (*.xml)|*.xml";
                if (SFD.ShowDialog() == true)
                {
                    var r = SFD.FileName.Split('.');
                    if (r[r.Length-1] == "xml")
                    {
                        using (XmlWriter writer = XmlWriter.Create(SFD.FileName))
                        {
                            /*var t = "н" + "е" + " " + "с" + "м" + "о" + "т" + "р" + "и" + "т" + "e" +
                             * " " + "с" + "ю" + "д" + "а" + " " + 
                             * "п" + "о" + "ж" + "а" + "л" + "у" + "й" + "с" + "т" + "а" 
                             *                                                                                                                                              ;*/
                            try
                            {
                                // у меня аутизм
                                writer.WriteStartElement("Data");
                                // не смотрите на этот код
                                writer.WriteStartElement("Developers");
                                fillXml(writer, "DevElement", "call developerstoredproc_SELECT");
                                writer.WriteEndElement();
                                // вам станет плохо
                                writer.WriteStartElement("CompanyTypes");
                                fillXml(writer, "CompanyTypesElement", "call companytypesstoredproc_SELECT");
                                writer.WriteEndElement();
                                // -_-
                                writer.WriteStartElement("VillageHouses");
                                fillXml(writer, "VillageHousesElement", "call villagehousesstoredproc_SELECT");
                                writer.WriteEndElement();
                                // хватит смотреть
                                writer.WriteStartElement("Villages");
                                fillXml(writer, "VillagesElement", "call villagesstoredproc_SELECT");
                                writer.WriteEndElement();
                                // пожалуйста
                                writer.WriteStartElement("HouseTypes");
                                fillXml(writer, "HouseTypesElement", "call housetypesstoredproc_SELECT");
                                writer.WriteEndElement();
                                // убейте меня
                                writer.WriteEndElement();
                                writer.Flush();
                                //=))))
                            }
                            catch (Exception ex)
                            {
                                // помогите
                                var e = ex.Message;
                                MessageBox.Show("Во время сохранения произошла ошибка", "ошибка");
                                // я как Гарольд
                            }
                            // который 
                        }
                        // скрывает
                    }
                    // боль
                    else
                    // за
                    {
                        // улыбкой
                        MessageBox.Show("не xml", "ошибка");
                        // курсовой
                        // меня
                        // убивает
                    }
                }
                else
                {
                    MessageBox.Show("отменено", "ошибка");
                }
            }
            catch(Exception)
            {
                MessageBox.Show("AAAAAAAAaaaaaaaaAAaAaaAaaAAAaAaaaaaaAAAAaaaaaAAAAAAAaaaaaAAAAaA");
            }
        }

        private void fillXml(XmlWriter writer, string nameHeader, string queryString)
        {
            string data = "";
            try
            {
                MySqlCommand query = new MySqlCommand(queryString, Connection);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query);
                DataSet sqlDataSet = new DataSet();
                dataAdapter.Fill(sqlDataSet);
                for (int i = 0; i <= sqlDataSet.Tables[0].Rows.Count - 1; i++)
                {
                    writer.WriteStartElement(nameHeader);
                    for (int j = 0; j <= sqlDataSet.Tables[0].Columns.Count - 1; j++)
                    {
                        data = sqlDataSet.Tables[0].Rows[i].ItemArray[j].ToString();
                        writer.WriteElementString(sqlDataSet.Tables[0].Columns[j].Caption, data);
                    }
                    writer.WriteEndElement();
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void ADMIN_XMLBUTTON_Click(object sender, RoutedEventArgs e)
        {
            ExportXml();
        }
    }
}