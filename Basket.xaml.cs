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
    /// Логика взаимодействия для Basket.xaml
    /// </summary>
    public partial class Basket : Page
    {
        BaseContext con = new BaseContext();
        MySqlCommand com = null;
        int count = 0;


        public Basket()
        {
            InitializeComponent();
            basketLoad();
            calcLoad();
            
        }
        public void basketLoad()
        {
            BasketListView.Items.Clear();
            con.openConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            com = new MySqlCommand("SELECT DISTINCT t1.BasketID,  t1.ProductID, t1.Amount, t2.Title, t2.Price, t2.Color, t2.ImageRoot FROM Basket t1 INNER JOIN Products t2 on t1.ProductID = t2.ProductID WHERE UserID = " + StaticData.userID, con.getConnection());
            uploadData();
        }
        public void calcLoad()
        {
       
        }
        public void uploadData()
        {
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = com;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            BasketListView.DataContext = ds.Tables[0].DefaultView;
            con.closeConnection();
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView drv = (DataRowView)BasketListView.SelectedItem;
                String basID = drv["BasketID"].ToString();
                String amo = drv["Amount"].ToString();
                count = Convert.ToInt32(amo) + 1;
                com = new MySqlCommand("UPDATE Basket SET Amount = @amo WHERE BasketID = @basID", con.getConnection());
                com.Parameters.Add("@amo", MySqlDbType.VarChar).Value = count;
                com.Parameters.Add("@basID", MySqlDbType.VarChar).Value = basID;
                com.ExecuteNonQuery();

                basketLoad();
            }
            catch
            {

            }
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
