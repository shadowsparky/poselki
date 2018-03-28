using MySql.Data.MySqlClient;
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

        private void Village_Add_BUTTON_Click(object sender, RoutedEventArgs e)
        {
            string[] dataArgs = { Village_Number_TB.Text, Village_Name_TB.Text, Village_Area_TB.Text, Village_Residents_Count_TB.Text, Village_Developer_Number.Text };
            _WF.MagicUniversalControlData("select villagesstoredfunc_INSERT", dataArgs, "Add");
        }
    }
}
