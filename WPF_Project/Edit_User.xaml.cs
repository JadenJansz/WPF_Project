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
using System.Data.SqlClient;
using System.Data;


namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for Edit_User.xaml
    /// </summary>
    public partial class Edit_User : Window
    {
        public Edit_User()
        {
            InitializeComponent();
        }
        Database data = new Database();

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            bool name = false, email = false, pass = false, cpass = false;

            if (string.IsNullOrEmpty(txt_name.Text))
            {
                blk_name.Visibility = Visibility.Visible;
                blk_name.Text = "*name cannot be blank";

            }
            else if (txt_name.Text.Any(char.IsDigit))
            {
                blk_name.Visibility = Visibility.Visible;
                blk_name.Text = "*name cannot have numbers";

            }
            else if (Regex.IsMatch(txt_name.Text, @"^[a-zA-Z ]+$"))
            {
                blk_name.Visibility = Visibility.Hidden;
                name = true;
            }
            else if (!Regex.IsMatch(txt_name.Text, @"^[a-zA-Z ]+$"))
            {
                blk_name.Visibility = Visibility.Visible;
                blk_name.Text = "*name can only have letters";
            }

            if (!Regex.IsMatch(txt_email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$") || string.IsNullOrEmpty(txt_email.Text))
            {
                blk_email.Visibility = Visibility.Visible;
                blk_email.Text = "*enter valid email eg:name@gmail.com";
            }
            else if(Regex.IsMatch(txt_email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                blk_email.Visibility = Visibility.Hidden;
                email = true;
            }

            if (txt_pass.Password.Length < 8)
            {
                blk_pass.Visibility = Visibility.Visible;
                blk_pass.Text = "*password should be more than 8 characters";
            }
            else if(txt_pass.Password.Length > 7)
            {
                blk_pass.Visibility = Visibility.Hidden;
                pass = true;
            }
            if (txt_pass.Password != txt_confirm.Password)
            {
                blk_confirm.Visibility = Visibility.Visible;
                blk_confirm.Text = "*passwords do not match";
            }
            else if(txt_pass.Password == txt_confirm.Password)
            {
                blk_confirm.Visibility = Visibility.Hidden;
                cpass = true;
            }


            if (name == true && email == true && pass == true && cpass == true && txt_pass.Password.Length > 7 && txt_confirm.Password.Length > 7)
            {
                string q = "Update Hotel.UserT set UserName = '" + txt_name.Text + "',Email = '" + txt_email.Text + "',Password = '" + txt_pass.Password + "' where UserID = '" + txt_ID.Text + "' ";

                int i = data.save_update_delete(q);

                if (i == 3)
                {
                    SuccessNoti obj = new SuccessNoti();
                    obj.lbl_save.Content = "Data Updated Successfully !";
                    obj.Show();
                }
                else
                {
                    ErrorNoti obj = new ErrorNoti();
                    obj.lbl_error.Content = "User ID Not Available";
                    obj.Show();
                }
            }


        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            grid_user.IsReadOnly = true;

            string w = "Select RoleID,UserID,UserName,Email from Hotel.UserT where UserID like '" + txt_ID.Text + "%' or UserName like '"+txt_ID.Text+"%' ";

            data.displayData(w);

            grid_user.ItemsSource = data.displayData(w).DefaultView;


            if (data.displayData(w).Rows.Count < 1)
            {

                if(txt_ID.Text.Length < 1)
                {
                    blk_userId.Visibility = Visibility.Visible;
                    blk_userId.Text = "*enter User ID";
                }
 
                lbl_records.Visibility = Visibility.Visible;
                lbl_records.Content = "*no records to display";
                grid_user.Visibility = Visibility.Hidden;

                txt_confirm.Clear();
                txt_email.Clear();
                txt_name.Clear();
                txt_pass.Clear();
            }
            else
            {
                try
                {
                    lbl_records.Visibility = Visibility.Hidden;
                    blk_userId.Visibility = Visibility.Hidden;
                    grid_user.Visibility = Visibility.Visible;

                    SqlConnection con = new SqlConnection("Server=tcp:raveeshanibm.database.windows.net,1433;Initial Catalog=Hotel_Reservation;Persist Security Info=False;User ID=RaveeshaNIBM;Password=Jj070300;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

                    con.Open();

                    SqlCommand cmd = new SqlCommand("Select RoleID, UserID, UserName, Email from Hotel.UserT where UserID = @ID", con);
                    cmd.Parameters.AddWithValue("@ID", (txt_ID.Text));
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        txt_name.Text = dr.GetValue(2).ToString();
                        txt_email.Text = dr.GetValue(3).ToString();
                       

                    }

                    con.Close();
                }
                catch(InvalidOperationException)
                {
                    ErrorNoti obj = new ErrorNoti();
                    obj.lbl_error.Content = "Invalid Operation";
                    obj.Show();
                }
                catch (Exception)
                {
                    ErrorNoti obj = new ErrorNoti();
                    obj.lbl_error.Content = "Error! Please try again";
                    obj.Show();
                }
            }
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            string q = "Delete from Hotel.UserT where UserID = '" + txt_ID.Text + "' ";

            int o = data.save_update_delete(q);

            if (o == 2)
            {
                SuccessNoti obj = new SuccessNoti();
                obj.lbl_save.Content = "Data Deleted Successfully !";
                obj.Show();

                txt_ID.Clear();
                txt_confirm.Clear();
                txt_email.Clear();
                txt_name.Clear();
                txt_pass.Clear();
                grid_user.ItemsSource = null;

            }
            else
            {
                ErrorNoti obj = new ErrorNoti();
                obj.lbl_error.Content = "User ID Not Available";
                obj.Show();
            }
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            txt_ID.Clear();
            txt_confirm.Clear();
            txt_email.Clear();
            txt_name.Clear();
            txt_pass.Clear();
            blk_confirm.Visibility = Visibility.Hidden;
            blk_email.Visibility = Visibility.Hidden;
            blk_name.Visibility = Visibility.Hidden;
            blk_pass.Visibility = Visibility.Hidden;
            blk_userId.Visibility = Visibility.Hidden;
            grid_user.ItemsSource = null;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Admin_start obj = new Admin_start();
            obj.Show();
            this.Close();
        }

        private void grid_user_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid gd = (DataGrid)sender;
                DataRowView drv = gd.SelectedItem as DataRowView;

                if (drv != null)
                {
                    txt_ID.Text = drv["UserID"].ToString();
                    txt_name.Text = drv["UserName"].ToString();
                    txt_email.Text = drv["Email"].ToString();
                    txt_pass.Password = "";
                    txt_confirm.Password = "";
                }
            }
            catch(InvalidOperationException)
            {
                ErrorNoti obj = new ErrorNoti();
                obj.lbl_error.Content = "Invalid Operation";
                obj.Show();
            }
            catch (Exception)
            {
                ErrorNoti obj = new ErrorNoti();
                obj.lbl_error.Content = "Error! Please try again";
                obj.Show();
            }
        }

        private void txt_ID_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_userId.Visibility = Visibility.Hidden;
        }

        private void txt_name_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_name.Visibility = Visibility.Hidden;
        }

        private void txt_email_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_email.Visibility = Visibility.Hidden;
        }

        private void txt_pass_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_pass.Visibility = Visibility.Hidden;
        }

        private void txt_confirm_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_confirm.Visibility = Visibility.Hidden;
        }
    }
}
