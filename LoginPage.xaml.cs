using FurnitureStoreApp.bin;
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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        int userID;
        public LoginPage()
        {
            InitializeComponent();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            string loginUser = NameTextBox.Text;
            string passUser = PassTextBox.Password;

            BaseContext con = new BaseContext();
            DataTable dt = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand comm = new MySqlCommand("SELECT * FROM users WHERE UserLogin = @uL AND UserPassword = @uP", con.getConnection());
            comm.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
            comm.Parameters.Add("@uP", MySqlDbType.VarChar).Value = passUser;

            adapter.SelectCommand = comm;
            adapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                con.openConnection();
                userID = (int)comm.ExecuteScalar();
                StaticData.userID = userID;
                con.closeConnection();
                ProductWindow pw = new ProductWindow();
                pw.Show();
                MainWindow main = Application.Current.MainWindow as MainWindow;
                if (main != null)
                {   
                    main.Close();
                }

            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
