using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace poselki
{
    /// <summary>
    /// Логика взаимодействия для villagehousesEditForm.xaml
    /// </summary>
    public partial class villagehousesEditForm : Window
    {
        private static WorkForm _WF;

        public MySqlConnection Connection { set; get; }

        public WorkForm WFSet
        {
            set { _WF = value; }
        }

        public villagehousesEditForm()
        {
            InitializeComponent();
        }

        private void villagehouses_Add_BUTTON_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;
            var AddCommand = new MySqlCommand("select villagehousesstoredfunc_INSERT(@House_ID_IN, @Village_Number_IN, @Street_IN, @Village_House_Number_IN, @Village_House_Type_ID, @Village_House_Area_IN, @House_Floor_Num_IN)", Connection);
            AddCommand.Parameters.AddWithValue("@House_ID_IN", villagehouses_House_ID_TB.Text);
            AddCommand.Parameters.AddWithValue("@Village_Number_IN", villagehouses_VillageNumber_TB.Text);
            AddCommand.Parameters.AddWithValue("@Street_IN", villagehouses_Street_TB.Text);
            AddCommand.Parameters.AddWithValue("@Village_House_Number_IN", villagehouses_VillageHouseNumber_TB.Text);
            AddCommand.Parameters.AddWithValue("@Village_House_Type_ID", villagehouses_VillageHouseType_TB.Text);
            AddCommand.Parameters.AddWithValue("@Village_House_Area_IN", villagehouses_VillageHouseArea_TB.Text);
            AddCommand.Parameters.AddWithValue("@House_Floor_Num_IN", villagehouses_VillageHouseFloorNumber_TB.Text);
            try
            {
                AddCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                error = true;
            }
            if (!error)
                MessageBox.Show("Дом добавлен", "ОК", MessageBoxButton.OK, MessageBoxImage.Information);
            _WF.UpdateVillageHouses();
            _WF.UpdateHouseTypes();
        }
        private void villagehouses_Edit_BUTTON_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;
            var UpdateCommand = new MySqlCommand("call villagehousesstoredproc_UPDATE(@House_ID_IN, @Village_Number_IN, @Street_IN, @Village_House_Number_IN, @Village_House_Type_ID, @Village_House_Area_IN, @House_Floor_Num_IN)", Connection);
            UpdateCommand.Parameters.AddWithValue("@House_ID_IN", villagehouses_House_ID_TB.Text);
            UpdateCommand.Parameters.AddWithValue("@Village_Number_IN", villagehouses_VillageNumber_TB.Text);
            UpdateCommand.Parameters.AddWithValue("@Street_IN", villagehouses_Street_TB.Text);
            UpdateCommand.Parameters.AddWithValue("@Village_House_Number_IN", villagehouses_VillageHouseNumber_TB.Text);
            UpdateCommand.Parameters.AddWithValue("@Village_House_Type_ID", villagehouses_VillageHouseType_TB.Text);
            UpdateCommand.Parameters.AddWithValue("@Village_House_Area_IN", villagehouses_VillageHouseArea_TB.Text);
            UpdateCommand.Parameters.AddWithValue("@House_Floor_Num_IN", villagehouses_VillageHouseFloorNumber_TB.Text);
            try
            {
                UpdateCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                error = true;
            }
            if (!error)
                MessageBox.Show("Дом отредактирован", "ОК", MessageBoxButton.OK, MessageBoxImage.Information);
            _WF.UpdateVillageHouses();
            _WF.UpdateHouseTypes();
        }
        private void villagehouses_DeleteHouse_BUTTON_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;
            var DeleteCommand = new MySqlCommand("call villagehousesstoredproc_DELETE(@House_ID_IN)", Connection);
            DeleteCommand.Parameters.AddWithValue("@House_ID_IN", villagehouses_DeleteHouse_ID_TB.Text);
            try
            {
                DeleteCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                error = true;
            }
            if (!error)
                MessageBox.Show("Дом удален", "ОК", MessageBoxButton.OK, MessageBoxImage.Information);
            _WF.UpdateVillageHouses();
        }
    }
}
