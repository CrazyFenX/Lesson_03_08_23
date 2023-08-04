using Lesson_03_08_23.Model;
using Lesson_03_08_23.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_03_08_23.Helpers
{
    internal class DB_Helper
    {
        /// <summary>
        /// Создать базу данных
        /// </summary>
        public static void CreateDateBase(string dbName)
        {
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();   // открываем подключение
                SqlCommand command = new SqlCommand();
                command.CommandText = $"CREATE DATABASE {dbName}";
                command.Connection = connection;
                command.ExecuteNonQuery();

                MessageBox.Show("База данных создана");
            }
        }

        /// <summary>
        /// Создать таблицы
        /// </summary>
        public static void CreateTables()
        {
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=TestDB_Lesson_03082023;Trusted_Connection=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();   // открываем подключение

                SqlCommand command = new SqlCommand();
                command.CommandText = "CREATE TABLE Users (Id INT PRIMARY KEY IDENTITY, Name NVARCHAR(100) NOT NULL, SurName NVARCHAR(100) NOT NULL)";
                command.Connection = connection;
                command.ExecuteNonQuery();

                MessageBox.Show("Таблица Users создана");
            }
        }

        /// <summary>
        /// Добавить
        /// </summary>
        public static void Insert()
        {
            var userView = new User_DetailView();

            var result = userView.ShowDialog();

            if (result == DialogResult.Cancel)
                return;

            var userName = userView.UserNameTextBox.Text;
            var userSurName = userView.UserSurNameTextBox.Text;

            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=TestDB_Lesson_03082023;Trusted_Connection=True;";
            string sqlExpression = $"INSERT INTO Users (Name, SurName) VALUES (\'{userName}\', \'{userSurName}\')";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                int number = command.ExecuteNonQuery();
                MessageBox.Show($"Добавлено объектов: {number}");
                connection.Close();
            }
        }

        /// <summary>
        /// Изменить
        /// </summary>
        public static void Update(User inputUser)
        {
            var userView = new User_DetailView();

            userView.UserNameTextBox.Text = inputUser.Name;
            userView.UserSurNameTextBox.Text = inputUser.SurName;

            var result = userView.ShowDialog();

            if (result == DialogResult.Cancel)
                return;

            var userName = userView.UserNameTextBox.Text;
            var userSurName = userView.UserSurNameTextBox.Text;

            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=TestDB_Lesson_03082023;Trusted_Connection=True;";
            string sqlExpression = $"UPDATE Users SET Name = \'{userName}\', SurName = \'{userSurName}\' WHERE Users.Id = {inputUser.Id}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                int number = command.ExecuteNonQuery();
                MessageBox.Show($"Обновлено объектов: {number}");
                connection.Close();
            }
        }

        /// <summary>
        /// Удалить
        /// </summary>
        public static void Delete(User inputUser)
        {
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=TestDB_Lesson_03082023;Trusted_Connection=True;";
            string sqlExpression = $"DELETE FROM Users WHERE Users.Id = {inputUser.Id}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                int number = command.ExecuteNonQuery();
                MessageBox.Show($"Удалено объектов: {number}");
                connection.Close();
            }
        }

        /// <summary>
        /// Получить записи
        /// </summary>
        public static DataSet Select()
        {
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=TestDB_Lesson_03082023;Trusted_Connection=True;";
            string sqlExpression = "SELECT * FROM Users";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var dataAdapter = new SqlDataAdapter(sqlExpression, connection);
                var ds = new DataSet();
                dataAdapter.Fill(ds);

                connection.Close();

                return ds;
            }
        }
    }
}
