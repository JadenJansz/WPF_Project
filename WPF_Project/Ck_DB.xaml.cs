using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for Ck_DB.xaml
    /// </summary>
    public partial class Ck_DB : Window
    {
        public Ck_DB()
        {
            InitializeComponent();
        }
        Database data = new Database();

        void display(string a)
        {
            data.displayData(a);

            grid_data.ItemsSource = data.displayData(a).DefaultView;
            grid_data.IsReadOnly = true;

        }

        private void btn_cus_Click(object sender, RoutedEventArgs e)
        {
            display("Select * from Hotel.Customer");

        }

        private void btn_room_Click(object sender, RoutedEventArgs e)
        {
            display("Select * from Hotel.Room");
        }

        private void btn_user_Click(object sender, RoutedEventArgs e)
        {
            display("Select * from Hotel.UserT");
        }
        private void btn_reserv_Click(object sender, RoutedEventArgs e)
        {
            display("Select * from Hotel.Cus_Room");
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Admin_start obj = new Admin_start();
            obj.Show();
            this.Close();
        }
    }
}
