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
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;

namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : Window
    {
        public User()
        {
            InitializeComponent();


            txt_inc.Visibility = Visibility.Hidden;
        }
        Database data = new Database();

        private void btn_next_Click(object sender, RoutedEventArgs e)
        {
            bool name = false, email = false, pass = false, cpass = false;

            if(role_type.SelectedIndex == -1)
            {
                blk_Rid.Visibility = Visibility.Visible;
                blk_Rid.Text = "*select a role type";
            }
            else
            {
                blk_Rid.Visibility = Visibility.Hidden;
            }

            if (string.IsNullOrEmpty(txt_user.Text))
            {
                blk_Uid.Visibility = Visibility.Visible;
                blk_Uid.Text = "*cannot be blank";
            }
            else
            {
                blk_Uid.Visibility = Visibility.Hidden;
            }

            if (string.IsNullOrEmpty(txt_username.Text))
            {
                blk_name.Visibility = Visibility.Visible;
                blk_name.Text = "*name cannot be blank";

            }
            else if (txt_username.Text.Any(char.IsDigit))
            {
                blk_name.Visibility = Visibility.Visible;
                blk_name.Text = "*name cannot have numbers";

            }
            else if (Regex.IsMatch(txt_username.Text, @"^[a-zA-Z ]+$"))
            {
                blk_name.Visibility = Visibility.Hidden;
                name = true;
            }
            else if (!Regex.IsMatch(txt_username.Text, @"^[a-zA-Z ]+$"))
            {
                blk_name.Visibility = Visibility.Visible;
                blk_name.Text = "*name can only have letters";
            }

            if (string.IsNullOrEmpty(txt_pass1.Password))
            {
                blk_pass.Visibility = Visibility.Visible;
                blk_pass.Text = "*cannot be blank";
            }
            else if(txt_pass1.Password.Length < 8)
            {
                blk_pass.Visibility = Visibility.Visible;
                blk_pass.Text = "*cannot be less than 8 characters";
            }
            else
            {
                blk_pass.Visibility = Visibility.Hidden;
                pass = true;
            }

            if (string.IsNullOrEmpty(txt_confirm1.Password))
            {
                blk_confirm.Visibility = Visibility.Visible;
                blk_confirm.Text = "*cannot be blank";
            }
            else if (txt_pass1.Password != txt_confirm1.Password)
            {
                blk_confirm.Visibility = Visibility.Visible;
                blk_confirm.Text = "*passwords do not match";

            }
            else
            {
                blk_confirm.Visibility = Visibility.Hidden;
                cpass = true;
            }

            if (!Regex.IsMatch(txt_email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$") || string.IsNullOrEmpty(txt_email.Text))
            {
                blk_email.Visibility = Visibility.Visible;
                blk_email.Text = "*enter a valid email  ex:name@gmail.com";
            }
            else
            {
                blk_email.Visibility = Visibility.Hidden;
                email = true;
            }


            if(role_type.SelectedIndex != -1 && txt_user.Text.Length > 0 && email == true && name == true && pass == true && cpass == true)
            {
              try
              {
                    string q = "Insert into Hotel.UserT values('" + role_type.Text + "','" + txt_user.Text + "','" + txt_username.Text + "','" + txt_email.Text + "','" + txt_pass1.Password + "','"+txt_inc.Text+"')";

                    int o = data.save_update_delete(q);

                    if (o == 2)
                    {
                        
                        SuccessNoti obj = new SuccessNoti();
                        obj.lbl_save.Content = "User Added Successfully !";
                        obj.Show();

                        txt_confirm1.Clear();
                        txt_email.Clear();
                        txt_pass1.Clear();
                        role_type.SelectedIndex = -1;
                        txt_user.Clear();
                        txt_username.Clear();
                    }
                    else
                    {
                       
                        ErrorNoti obj = new ErrorNoti();
                        obj.lbl_error.Content = "Cannot Add User !";
                        obj.Show();
                    }
             }
             catch(SqlException)
             {
                    
                   ErrorNoti obj = new ErrorNoti();
                   obj.lbl_error.Content = "Data Has Already Been Entered !";
                   obj.Show();
             }

             catch(Exception)
            {
                ErrorNoti obj = new ErrorNoti();
                obj.lbl_error.Content = "Error! Please try again";
                obj.Show();
            }
            }

        }

        private void txt_user_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            txt_confirm1.Clear();
            txt_email.Clear();
            txt_pass1.Clear();
            role_type.SelectedIndex = -1;
            txt_user.Clear();
            txt_username.Clear();
            blk_confirm.Visibility = Visibility.Hidden;
            blk_email.Visibility = Visibility.Hidden;
            blk_name.Visibility = Visibility.Hidden;
            blk_pass.Visibility = Visibility.Hidden;
            blk_Rid.Visibility = Visibility.Hidden;
            blk_Uid.Visibility = Visibility.Hidden;

        }

        private void role_type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (role_type.SelectedIndex == 0)
            {
                string a = "select isnull(max(cast(URec_ID as int)),0)+1 from Hotel.UserT";

                txt_inc.Text = data.displayData(a).Rows[0][0].ToString();
                txt_user.Focus();

                txt_user.Text = "ADM" + txt_inc.Text;
                txt_user.IsReadOnly = true;
            }
            else if (role_type.SelectedIndex == 1)
            {
                string a = "select isnull(max(cast(URec_ID as int)),0)+1 from Hotel.UserT";

                txt_inc.Text = data.displayData(a).Rows[0][0].ToString();
                txt_user.Focus();

                txt_user.Text = "RECP" + txt_inc.Text;
                txt_user.IsReadOnly = true;
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Admin_start obj = new Admin_start();
            obj.Show();
            this.Close();
        }

        private void role_type_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_Rid.Visibility = Visibility.Hidden;
        }

        private void txt_user_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_Uid.Visibility = Visibility.Hidden;
        }

        private void txt_username_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_name.Visibility = Visibility.Hidden;
        }

        private void txt_email_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_email.Visibility = Visibility.Hidden;
        }

        private void txt_pass1_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_pass.Visibility = Visibility.Hidden;
        }

        private void txt_confirm1_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_confirm.Visibility = Visibility.Hidden;
        }

    }
}
