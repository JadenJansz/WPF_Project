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


namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for Edit_Customer.xaml
    /// </summary>
    public partial class Edit_Customer : Window
    {
        public Edit_Customer()
        {
            InitializeComponent();
        }
        Database data = new Database();

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            grid_cus.IsReadOnly = true;

            blk_name.Visibility = Visibility.Hidden;

            string q = "Select Cus_ID,Cus_Name,Cus_Address,Cus_Tel,Cus_Email from Hotel.Customer where Cus_Name like '" + txt_name.Text +"%'";

            data.displayData(q);

            if(data.displayData(q).Rows.Count < 1)
            {
                lbl_records.Visibility = Visibility.Visible;
                lbl_records.Content = "No Records To Display";

                grid_cus.Visibility = Visibility.Hidden;
            }
            else
            {
                lbl_records.Visibility = Visibility.Hidden;
                grid_cus.Visibility = Visibility.Visible;
            }

            grid_cus.ItemsSource = data.displayData(q).DefaultView;

            if(string.IsNullOrEmpty(txt_name.Text))
            {
                blk_name.Visibility = Visibility.Visible;
                blk_name.Text = "*enter a customer name";
            }

        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            bool name = false, id = false, address =false, tel =false, email =false;

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
            else if(Regex.IsMatch(txt_name.Text, @"^[a-zA-Z ]+$"))
            {
                blk_name.Visibility = Visibility.Hidden;
                name = true;
            }
            else if(!Regex.IsMatch(txt_name.Text, @"^[a-zA-Z ]+$"))
            {
                blk_name.Visibility = Visibility.Visible;
                blk_name.Text = "*name can only have letters";
            }

            if (string.IsNullOrEmpty(txt_id.Text))
            {
                blk_id.Visibility = Visibility.Visible;
                blk_id.Text = "*enter relevant Customer ID";
            }
            else
            {
                blk_id.Visibility = Visibility.Hidden;
                id = true;
            }
            if(string.IsNullOrEmpty(txt_address.Text))
            {
                blk_add.Visibility = Visibility.Visible;
                blk_add.Text = "*enter Customer address";
            }
            else
            {
                blk_add.Visibility = Visibility.Hidden;
                address = true;
            }
            
            if(!Regex.IsMatch(txt_email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$") || string.IsNullOrEmpty(txt_email.Text))
            {
                blk_email.Visibility = Visibility.Visible;
                blk_email.Text = "*enter a valid email";
            }
            else
            {
                blk_email.Visibility = Visibility.Hidden;
                email = true;
            }

            if (!Regex.IsMatch(txt_tel.Text, @"^7|0|(?:\+94)[0-9]{9,10}$") || txt_tel.Text.Length < 9 || txt_tel.Text.Any(char.IsLetter))
            {
                blk_tel.Visibility = Visibility.Visible;
                blk_tel.Text = "*enter valid telephone number";
            }
            else if (txt_tel.Text.All(char.IsDigit))
            {
                blk_tel.Visibility = Visibility.Hidden;
                tel = true;
            }

            if (name == true && address == true && tel == true && email == true && id == true)
            {
                string w = "Update Hotel.Customer set Cus_Name = '"+txt_name.Text+"' ,Cus_Address = '" + txt_address.Text + "',Cus_Tel = '" + txt_tel.Text + "',Cus_Email = '" + txt_email.Text + "' where Cus_ID = '" + txt_id.Text + "' ";

                int i = data.save_update_delete(w);

                if (i == 3)
                {
                    SuccessNoti obj = new SuccessNoti();
                    obj.lbl_save.Content = "Data Updated Successfully !";
                    obj.Show();
                }
                else
                {
                    ErrorNoti obj = new ErrorNoti();
                    obj.lbl_error.Content = "Failed to Update Information";
                    obj.Show();

                }
            }
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            txt_address.Clear();
            txt_email.Clear();
            txt_id.Clear();
            txt_name.Clear();
            txt_tel.Clear();
            grid_cus.ItemsSource = null;
            blk_add.Visibility = Visibility.Hidden;
            blk_email.Visibility = Visibility.Hidden; ;
            blk_id.Visibility = Visibility.Hidden;
            blk_name.Visibility = Visibility.Hidden;
            blk_tel.Visibility = Visibility.Hidden;
        }

        private void grid_cus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DataGrid gd = (DataGrid)sender;
            DataRowView drv = gd.SelectedItem as DataRowView;

            if(drv != null)
            {
                txt_id.Text = drv["Cus_ID"].ToString();
                txt_address.Text = drv["Cus_Address"].ToString();
                txt_email.Text = drv["Cus_Email"].ToString();
                txt_tel.Text = drv["Cus_Tel"].ToString();
                txt_name.Text = drv["Cus_Name"].ToString();
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Rec_Start obj = new Rec_Start();
            obj.Show();
            this.Close();
        }

        private void txt_name_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_name.Visibility = Visibility.Hidden;
        }

        private void txt_id_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_id.Visibility = Visibility.Hidden;
        }

        private void txt_address_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_add.Visibility = Visibility.Hidden;
        }

        private void txt_email_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_email.Visibility = Visibility.Hidden;
        }

        private void txt_tel_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_tel.Visibility = Visibility.Hidden;
        }
    }
}
