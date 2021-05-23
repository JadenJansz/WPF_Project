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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        Rec_Start Reception = new Rec_Start();
        Admin_start admin = new Admin_start();
        Database data = new Database();
        Manager_Start manager = new Manager_Start();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(txt_user.Text.Length == 0)
            {
                blk_user.Visibility = Visibility.Visible;
                blk_user.Text = "*enter a valid username";
            }
            else if(txt_user.Text.Length > 0)
            {
                blk_user.Visibility = Visibility.Hidden;
            }
            if(txt_pass.Password.Length < 8)
            {
                blk_pass.Visibility = Visibility.Visible;
                blk_pass.Text = "*enter a valid password";
            }


            string q = "select RoleID,UserID,Password from Hotel.UserT where UserID = '" + txt_user.Text + "' and Password = '" + txt_pass.Password + "'";

            if (data.displayData(q).Rows.Count > 0)
            {
                for(int i= 0; i< data.displayData(q).Rows.Count; i++)
                {
                    if(data.displayData(q).Rows[i]["RoleID"].ToString() == "R")
                    {
                        Reception.Show();
                        this.Close();
                    }
                    else if (data.displayData(q).Rows[i]["RoleID"].ToString() == "A")
                    {
                        admin.Show();
                        this.Close();
                    }
                    else if(data.displayData(q).Rows[i]["RoleID"].ToString() == "M")
                    {
                        manager.Show();
                        this.Close();
                    }
                }

            }
            else if (data.displayData(q).Rows.Count == 0)
            {
                blk_pass.Visibility = Visibility.Visible;
                blk_pass.Text = "*user name and password do not match";
            }

        }

        private void txt_user_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_user.Visibility = Visibility.Hidden;
        }

        private void txt_pass_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_pass.Visibility = Visibility.Hidden;
        }

    }
}
