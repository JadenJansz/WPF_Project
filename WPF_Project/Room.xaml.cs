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
using System.Text.RegularExpressions;


namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for Room.xaml
    /// </summary>
    public partial class Room : Window
    {
        public Room()
        {
            InitializeComponent();

            txt_inc.Visibility = Visibility.Hidden;

            string a = "select isnull(max(cast(Rec_ID as int)),0)+1 from Hotel.Room";

            txt_inc.Text = data.displayData(a).Rows[0][0].ToString();
            txt_id.Focus();

            txt_id.Text = "R" + txt_inc.Text;
            txt_id.IsReadOnly = true;

        }
        Database data = new Database();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool price = false;

            if (string.IsNullOrEmpty(txt_id.Text))
            {
                blk_id.Visibility = Visibility.Visible;
                blk_id.Text = "*enter room ID";
            }
            else
            {
                blk_id.Visibility = Visibility.Hidden;
            }

            if (type_picker.SelectedIndex == -1)
            {
                blk_type.Visibility = Visibility.Visible;
                blk_type.Text = "*select room type";
            }
            else
            {
                blk_type.Visibility = Visibility.Hidden;
            }

            if (floor_picker.SelectedIndex == -1)
            {
                blk_floor.Visibility = Visibility.Visible;
                blk_floor.Text = "*select room type";
            }
            else
            {
                blk_floor.Visibility = Visibility.Hidden;
            }

            if (string.IsNullOrEmpty(txt_price.Text))
            {
                blk_price.Visibility = Visibility.Visible;
                blk_price.Text = "*enter a price";
            }
            else if (txt_price.Text.Any(char.IsLetter))
            {
                blk_price.Visibility = Visibility.Visible;
                blk_price.Text = "*invalid price";
               
            }
            else if(Regex.IsMatch(txt_price.Text, @"^[0-9#.]+$"))
            {
                blk_price.Visibility = Visibility.Hidden;
                price = true;
            }
            else 
            {
                blk_price.Visibility = Visibility.Visible;
                blk_price.Text = "*invalid price";
            }

            if (aval_picker.SelectedIndex == -1)
            {
                blk_avail.Visibility = Visibility.Visible;
                blk_avail.Text = "*select room availability";
            }
            else
            {
                blk_avail.Visibility = Visibility.Hidden;
            }


            try
            {
                if (txt_id.Text.Length > 0 && type_picker.SelectedIndex != -1 && floor_picker.SelectedIndex != -1 && price == true && aval_picker.SelectedIndex != -1)
                {
                    string q = "Insert into Hotel.Room values ('" + txt_id.Text + "','" + type_picker.Text + "','" + txt_price.Text + "','" + floor_picker.Text + "','" + aval_picker.Text + "', '"+txt_inc.Text+"') ";

                    int o = data.save_update_delete(q);

                    if (o == 2)
                    {
                        SuccessNoti obj = new SuccessNoti();
                        obj.lbl_save.Content = "Data Added Successfully";
                        obj.Show();

                        txt_inc.Visibility = Visibility.Hidden;

                        string a = "select isnull(max(cast(Rec_ID as int)),0)+1 from Hotel.Room";

                        txt_inc.Text = data.displayData(a).Rows[0][0].ToString();
                        txt_id.Focus();

                        txt_id.Text = "R" + txt_inc.Text;
                        txt_id.IsReadOnly = true;

                        txt_price.Clear();
                        type_picker.SelectedIndex = -1;
                        floor_picker.SelectedIndex = -1;
                        aval_picker.SelectedIndex = -1;
                        
                    }
                    else
                    {
                        ErrorNoti obj = new ErrorNoti();
                        obj.lbl_error.Content = "Failed to Insert Room !";
                        obj.Show();

                    }
                }
            }
            catch(SqlException)
            {
                ErrorNoti obj = new ErrorNoti();
                obj.lbl_error.Content = "Data Has Already Been Entered !";
                obj.Show();
            }
            catch (Exception)
            {
                ErrorNoti obj = new ErrorNoti();
                obj.lbl_error.Content = "Error! Please try again";
                obj.Show();
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            txt_price.Clear();
            floor_picker.SelectedIndex = -1;
            type_picker.SelectedIndex = -1;
            aval_picker.SelectedIndex = -1;
            blk_id.Visibility = Visibility.Hidden;
            blk_avail.Visibility = Visibility.Hidden;
            blk_floor.Visibility = Visibility.Hidden;
            blk_price.Visibility = Visibility.Hidden;
            blk_type.Visibility = Visibility.Hidden;
            
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Admin_start obj = new Admin_start();
            obj.Show();
            this.Close();
        }

        private void txt_id_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_id.Visibility = Visibility.Hidden;
        }

        private void type_picker_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_type.Visibility = Visibility.Hidden;
        }

        private void floor_picker_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_floor.Visibility = Visibility.Hidden;
        }

        private void txt_price_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_price.Visibility = Visibility.Hidden;
        }

        private void aval_picker_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_avail.Visibility = Visibility.Hidden;
        }
    }
}
