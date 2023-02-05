using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Windows;
using System.Windows.Input;
using Microsoft.Data.Sqlite;

namespace SQLite_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string database_name = "Database.db";
        public MainWindow()
        {
            InitializeComponent();

            Ini();
        }

        void Ini()
        {
            if (!File.Exists("Database.db"))
                using (var connection = new SqliteConnection("Data Source = Database.db"))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand("Data Source = Database.db");
                    command.Connection = connection;

                    command.CommandText = "CREATE TABLE Userrs(_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL, Price INTEGER NOT NULL,Count INTEGER NOT NULL)";
                    command.ExecuteNonQuery();
                }
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(database_name))
                using (var connection = new SqliteConnection("Data Source = " + database_name))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand("Data Source = " + database_name);
                    command.Connection = connection;

                    command.CommandText = "INSERT INTO Userrs(Name,Price,Count) VALUES('"+name_tb.Text+"','"+price_tb.Text+ "','"+count_tb.Text+"')";
                    command.ExecuteNonQuery();
                    result_tb.Text = "Добавлено.";
                }
        }

        private void count_tb_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //MessageBox.Show();
            //if (!char.IsDigit())
            //{
            //    e.Handled = true;
            //}
        }

        private void name_tb_GotFocus(object sender, RoutedEventArgs e)
        {
            if (name_tb.Text == "Name") name_tb.Text = "";
        }
        private void name_tb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (name_tb.Text == "") name_tb.Text = "Name";
        }
        private void price_tb_GotFocus(object sender, RoutedEventArgs e)
        {
            if (price_tb.Text == "Price") price_tb.Text = "";
        }
        private void price_tb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (price_tb.Text == "") price_tb.Text = "Price";
        }
        private void count_tb_GotFocus(object sender, RoutedEventArgs e)
        {
            if (count_tb.Text == "Count") count_tb.Text = "";
        }
        private void count_tb_LostFocus(object sender, RoutedEventArgs e)
        {
            if (count_tb.Text == "") count_tb.Text = "Count";
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(database_name))
                using (var connection = new SqliteConnection("Data Source = " + database_name))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand("Data Source = " + database_name);
                    command.Connection = connection;

                    SqliteDataReader sqlite_datareader;
                    command = connection.CreateCommand();
                    command.CommandText = "SELECT SUM(Price * Count) FROM Userrs";

                    sqlite_datareader = command.ExecuteReader();
                    while (sqlite_datareader.Read())
                    {
                        result_tb.Text = "summ: " + sqlite_datareader.GetString(0);
                    }
                }
        }

        private void btn_baza_click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(database_name))
                using (var connection = new SqliteConnection("Data Source = " + database_name))
                {
                    connection.Open();

                    SqliteCommand command = new SqliteCommand("Data Source = " + database_name);
                    command.Connection = connection;

                    SqliteDataReader sqlite_datareader;
                    command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM Userrs";

                    sqlite_datareader = command.ExecuteReader();
                    for (int i = 0; i < sqlite_datareader.FieldCount; i++)
                    {
                        result_tb.Text += sqlite_datareader.GetName(i) + "\t";
                    }
                    result_tb.Text += "\n";

                    while (sqlite_datareader.Read())
                    {
                        for (int i = 0; i < sqlite_datareader.FieldCount; i++)
                        {
                            result_tb.Text += sqlite_datareader.GetValue(i) + "\t";
                        }
                        result_tb.Text += "\n";
                    }
                }
        }
    }
}
