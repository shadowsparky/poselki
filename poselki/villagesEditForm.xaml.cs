using MySql.Data.MySqlClient;
using System.Windows;

namespace poselki
{
    public partial class villagesEditForm : Window
    {
        private MySqlConnection _connection;
        private WorkForm _WF;

        public MySqlConnection Connection
        {
            set { _connection = value; }
            get { return _connection; }
        }

        public WorkForm WFSet
        {
            set { _WF = value; }
        }

        public villagesEditForm()
        {
            InitializeComponent();
        }
    } // asdasdda asdasdsdasd
}
