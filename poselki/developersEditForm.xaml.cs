using MySql.Data.MySqlClient;
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

        private void Developers_Add_Button_Click(object sender, RoutedEventArgs e)
        {
            string[] dataArgs = { DevName_TB.Text, Dev_Income_TB.Text, Developers_Company_Type_TB.Text, Village_Street_TB.Text, Home_Number_TB.Text };
            _WF.MagicUniversalControlData("select developerstoredfunc_INSERT", dataArgs, "Add");
        }
    }
}
