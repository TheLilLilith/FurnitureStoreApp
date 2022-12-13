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
using MaterialDesignThemes;
using FurnitureStoreApp.bin;

namespace FurnitureStoreApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new LoginPage());
            BackButton.Visibility = Visibility.Hidden;
           
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new RegisterPage());
            RegisterButton.Visibility = Visibility.Hidden;
            GuestButton.Visibility = Visibility.Hidden;
            BackButton.Visibility = Visibility.Visible;
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            ProductWindow pw = new ProductWindow();
            pw.Show();
            MainWindow main = Application.Current.MainWindow as MainWindow;
            if (main != null)
            {
                main.Close();
            }
        }
        private void HideButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Environment.Exit(1);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new LoginPage());
            RegisterButton.Visibility = Visibility.Visible;
            GuestButton.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Hidden;
        }
    }
}
