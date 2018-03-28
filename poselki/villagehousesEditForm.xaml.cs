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
            string[] dataArgs = { villagehouses_House_ID_TB.Text, villagehouses_VillageNumber_TB.Text, villagehouses_Street_TB.Text, villagehouses_VillageHouseNumber_TB.Text,
                villagehouses_VillageHouseType_TB.Text, villagehouses_VillageHouseArea_TB.Text, villagehouses_VillageHouseFloorNumber_TB.Text };
            _WF.MagicUniversalAddToTable("select villagehousesstoredfunc_INSERT", dataArgs);
        }
    }
}
