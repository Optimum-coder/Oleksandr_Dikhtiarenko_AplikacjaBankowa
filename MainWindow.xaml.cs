using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Oleksandr_Dikhtiarenko_AplikacjaBankowa
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Load();
        }

        public static SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-K3JSVD1;Initial Catalog=Bank;Integrated Security=True;");

        public void Load()
        {
            SqlCommand sqlc = new SqlCommand("select * from BankAka", connection);
            DataTable dataTable = new DataTable();
            connection.Open();
            SqlDataReader sqldr = sqlc.ExecuteReader();
            dataTable.Load(sqldr);
            connection.Close();
            DataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-K3JSVD1;Initial Catalog=Bank;Integrated Security=True;"))
                {
                    connection.Open();
                    SqlCommand sqlc = new SqlCommand("Delete from BankAka where PeopleId = @PeopleId", connection);
                    sqlc.Parameters.AddWithValue("@PeopleId", tbSerch.Text);

                    sqlc.ExecuteNonQuery();
                    MessageBox.Show("Record has been deleted", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                Load(); 
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Not Deleted: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddWin window = new AddWin(this);
            window.Show();
        }
    }
}
