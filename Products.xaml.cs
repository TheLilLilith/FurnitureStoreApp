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
    /// Логика взаимодействия для Products.xaml
    /// </summary>
    public partial class Products : Page
    {
        BaseContext con = new BaseContext();
        MySqlCommand com = null;
        public Products()
        {
            InitializeComponent();
            loadData();
            checkUser();
            fillData();
            SortBox.Items.Add("Нет");
            SortBox.Items.Add("По возрастанию");
            SortBox.Items.Add("По убыванию");
        }
        public void checkUser()
        {
            if (StaticData.userID == 0)
            {
               ProductsListView.IsEnabled = false;
            }
        }
        public void loadData()
        {
           
            con.openConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            com = new MySqlCommand("SELECT t1.ProductID, t1.Title, t1.Amount, t1.Price, t1.ImageRoot, t1.Color, t2.ProviderName FROM products t1 INNER JOIN providers t2 on t1.Provider = t2.ProviderID", con.getConnection());
            uploadData();
        }
        public void uploadData()
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = com;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            ProductsListView.DataContext = ds.Tables[0].DefaultView;
            con.closeConnection();
        }
        public void fillData()
        {
            FilterBox.Items.Clear();
            FilterBox.Items.Add("Нет");
            
            MySqlCommand com = new MySqlCommand("SELECT ProviderName FROM Providers", con.getConnection());
            con.openConnection();
            MySqlDataReader sqlReader = com.ExecuteReader();
            while (sqlReader.Read())
            {
                FilterBox.Items.Add(sqlReader["ProviderName"].ToString());
            }
            con.closeConnection();
        }

        private void AddCartButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)ProductsListView.SelectedItem;
                String productID = drv["ProductID"].ToString();
                String productTitle = drv["Title"].ToString();
              
                MessageBox.Show("Товар: "+ productTitle+ " добавлен в корзину.");
            }
            catch
            {

            }
        }

        private void FilterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (FilterBox.SelectedIndex != 0)
            {
               com = new MySqlCommand("SELECT t1.ProductID, t1.Title, t1.Amount, t1.Price, t1.ImageRoot, t1.Color, t2.ProviderName FROM products t1 INNER JOIN providers t2 on t1.Provider = t2.ProviderID WHERE t1.Provider = " + FilterBox.SelectedIndex, con.getConnection());
               uploadData();
            }
            else
            {
                loadData();
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand com = new MySqlCommand("SELECT t1.ProductID, t1.Title, t1.Amount, t1.Price, t1.ImageRoot, t1.Color, t2.ProviderName FROM products t1 INNER JOIN providers t2 on t1.Provider = t2.ProviderID WHERE t1.Title LIKE '%" + SearchTextBox.Text + "%'", con.getConnection());
            adapter.SelectCommand = com;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            ProductsListView.DataContext = ds.Tables[0].DefaultView;
            con.closeConnection();

        }

        private void SortBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
          if (SortBox.SelectedIndex == 0)
            {
                loadData();
            }
          if (SortBox.SelectedIndex == 1)
            {
               com = new MySqlCommand("SELECT t1.ProductID, t1.Title, t1.Amount, t1.Price, t1.ImageRoot, t1.Color, t2.ProviderName FROM products t1 INNER JOIN providers t2 on t1.Provider = t2.ProviderID ORDER BY Price ASC", con.getConnection());
                uploadData();
            }
          if (SortBox.SelectedIndex == 2)
            {
               com = new MySqlCommand("SELECT t1.ProductID, t1.Title, t1.Amount, t1.Price, t1.ImageRoot, t1.Color, t2.ProviderName FROM products t1 INNER JOIN providers t2 on t1.Provider = t2.ProviderID ORDER BY Price DESC", con.getConnection());
                uploadData();
            }
            
        }
    }
}
