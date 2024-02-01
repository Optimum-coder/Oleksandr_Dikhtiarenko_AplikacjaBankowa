using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace Oleksandr_Dikhtiarenko_AplikacjaBankowa
{
    public partial class AddWin : Window
    {
        private MainWindow mainWindow;

        public AddWin(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        public bool isValid()
        {
            string[] textBoxes = { tbfirst.Text, tblast.Text, tbpesel.Text, tbnumber.Text };
            string[] fieldNames = { "FirstName", "LastName", "Pesel", "AkaNumber" };

            for (int i = 0; i < textBoxes.Length; i++)
            {
                if (string.IsNullOrEmpty(textBoxes[i]))
                {
                    MessageBox.Show($"Failed: There are empty or invalid values in the {fieldNames[i]} field.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
            return true;
        }

        private void SaveBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    using (SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-K3JSVD1;Initial Catalog=Bank;Integrated Security=True;"))
                    {
                        connection.Open();

                        using (SqlCommand sqlc = new SqlCommand("INSERT INTO BankAka (FirstName, LastName, Pesel, AkaNumber) VALUES (@FirstName, @LastName, @Pesel, @AkaNumber)", connection))
                        {
                            sqlc.CommandType = CommandType.Text;
                            sqlc.Parameters.AddWithValue("@FirstName", tbfirst.Text);
                            sqlc.Parameters.AddWithValue("@LastName", tblast.Text);
                            sqlc.Parameters.AddWithValue("@Pesel", tbpesel.Text);
                            sqlc.Parameters.AddWithValue("@AkaNumber", tbnumber.Text);

                            sqlc.ExecuteNonQuery();
                        }
                    }

                    mainWindow.Load();
                    MessageBox.Show("Successfully registered", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}

