using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;


namespace poselki
{
    public partial class WorkForm : Window
    {
        private static MySqlConnection _connection;

        public MySqlConnection Connection
        {
            set { _connection = value; }
            get { return _connection; }
        }

        public WorkForm()
        {
            InitializeComponent();
        }

        public void UpdateDevelopers()
        {
            MySqlDataAdapter ad = new MySqlDataAdapter();
            ad.SelectCommand = new MySqlCommand("call developerstoredproc_SELECT", _connection);
            DataTable table = new DataTable();
            ad.Fill(table);
            testos.ItemsSource = table.DefaultView;
        }

        private void testos_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDevelopers();
        }

        private void testEditButton_Click(object sender, RoutedEventArgs e)
        {
            developersEditForm DEF = new developersEditForm();
            DEF.Connection = _connection;
            DEF.WFSet = this;
            DEF.Show();
        }
    }
}
