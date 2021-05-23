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

namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for Logs.xaml
    /// </summary>
    public partial class Logs : Window
    {
        public Logs()
        {
            InitializeComponent();
            grid_log.IsReadOnly = true;
        }
        Database data = new Database();

        private void btn_cus_Click(object sender, RoutedEventArgs e)
        {
            string q = "Select Cus_ID,Cus_Name,Cus_Address,Cus_Tel,Cus_Email,Updated_at,Operation from Hotel.Customer_Audits";

            data.displayData(q);

            grid_log.ItemsSource = data.displayData(q).DefaultView;
        }

        private void btn_logs_Click(object sender, RoutedEventArgs e)
        {
            string q = "Select RoomID,Room_type,Room_Price,Room_Floor,Room_Availability,Updated_at,Operation from Hotel.Room_Audits";

            data.displayData(q);

            grid_log.ItemsSource = data.displayData(q).DefaultView;
        }

        private void btn_user_Click_1(object sender, RoutedEventArgs e)
        {
            string q = "Select UserID,UserName,Email,Updated_at,Operation from Hotel.User_Audits";

            data.displayData(q);

            grid_log.ItemsSource = data.displayData(q).DefaultView;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Manager_Start obj = new Manager_Start();
            obj.Show();
            this.Close();
        }
    }
}
