using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace FurnitureStoreApp
{
    /// <summary>
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
            System.Globalization.CultureInfo culInfo = new System.Globalization.CultureInfo("en-us");
            System.Globalization.DateTimeFormatInfo dtinfo = new System.Globalization.DateTimeFormatInfo();
            dtinfo.ShortDatePattern = "yyyy/MM/dd";
        }


        private void HideCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (HideCheckBox.IsChecked == true)
                PatronymicTextBox.IsEnabled = false;
            else
                PatronymicTextBox.IsEnabled = true;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (LogTextBox.Text != " " || PassTextBox.Password != " " || SurTextBox.Text != " " || NameTextBox.Text == " ")
            {
                try //Проверка подключения к БД
                {

                    if (isUserExists())
                        return;
                    BaseContext con = new BaseContext();
                    MySqlCommand comm = new MySqlCommand("INSERT INTO users (UserLogin, UserPassword, UserSurname, UserName, UserPatronymic, Adress, Phone, BirthDate, Role) " +
                        "VALUES (@log, @pass, @surname, @name, @patronymic, @adress, @phone, @birth, @role)", con.getConnection());
                    comm.Parameters.Add("@log", MySqlDbType.VarChar).Value = LogTextBox.Text;
                    comm.Parameters.Add("@pass", MySqlDbType.VarChar).Value = PassTextBox.Password;
                    comm.Parameters.Add("@surname", MySqlDbType.VarChar).Value = SurTextBox.Text;
                    comm.Parameters.Add("@name", MySqlDbType.VarChar).Value = NameTextBox.Text;
                    comm.Parameters.Add("@patronymic", MySqlDbType.VarChar).Value = PatronymicTextBox.Text;
                    comm.Parameters.Add("@adress", MySqlDbType.VarChar).Value = AdressTextBox.Text;
                    comm.Parameters.Add("@phone", MySqlDbType.VarChar).Value = PhoneTextBox.Text;
                    comm.Parameters.Add("@birth", MySqlDbType.VarChar).Value = BirthDate.SelectedDate.ToString();
                    comm.Parameters.Add("@role", MySqlDbType.VarChar).Value = "Клиент";
                    con.openConnection();
                    if (comm.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("Аккаунт успешно создан", "Успех", MessageBoxButton.OK);
                    }
                    else
                        MessageBox.Show("Аккаунт не был создан.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    con.closeConnection();
                }
                catch
                {
                    MessageBox.Show("Ошибка добавления, Аккаунт не был создан.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Введите данные в поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public Boolean isUserExists()
        {
            BaseContext con = new BaseContext();
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand comm = new MySqlCommand("SELECT * FROM users WHERE UserLogin = @uL", con.getConnection());
            comm.Parameters.Add("@uL", MySqlDbType.VarChar).Value = LogTextBox.Text;

            adapter.SelectCommand = comm;
            adapter.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже используется, введите другой.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }

            else
                return false;
        }
    }
}
