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
    /// Interaction logic for Admin_start.xaml
    /// </summary>
    public partial class Admin_start : Window
    {
        public Admin_start()
        {
            InitializeComponent();
        }
        Edit_User edituser = new Edit_User();
        Edit_Room editroom = new Edit_Room();
        Ck_DB check_data = new Ck_DB();
        User user = new User();
        Room room = new Room();

        private void btn_user_Click(object sender, RoutedEventArgs e)
        {
            edituser.Show();
            this.Close();
        }

        private void btn_room_Click(object sender, RoutedEventArgs e)
        {
            editroom.Show();
            this.Close();
        }

        private void btn_data_Click(object sender, RoutedEventArgs e)
        {
            check_data.Show();
            this.Close();
        }

        private void btn_adduser_Click(object sender, RoutedEventArgs e)
        {
            user.Show();
            this.Close();
        }

        private void btn_addroom_Click(object sender, RoutedEventArgs e)
        {
            room.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MBox obj = new MBox();
            obj.Show();
            obj.Owner = this;
        }
    }
}
