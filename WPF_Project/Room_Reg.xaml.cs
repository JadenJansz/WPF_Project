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
    /// Interaction logic for Room_Reg.xaml
    /// </summary>
    public partial class Room_Reg : Window
    {
        public string cusId, cusName, cusAdd, cusEmail, cusTel,recID;

        public Room_Reg(string a, string b, string c, string d, string e, string f)
        {
            cusId = a; cusName = b; cusAdd = c; cusEmail = d; cusTel = e; recID = f;

            InitializeComponent();

            txt_room.IsReadOnly = true;
            txt_price.Text = "0.00";
            pick_in.SelectedDate = DateTime.Today;
        }
        Database data = new Database();



        private void txt_price_TextChanged(object sender, TextChangedEventArgs e)
        {
            txt_price.IsReadOnly = true;

            
        }

        private void price_picker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            decimal price;

            if(price_picker.SelectedIndex == 0 || price_picker.SelectedIndex == 1)
            {
                price = Convert.ToDecimal((pick_out.SelectedDate.Value.Day - pick_in.SelectedDate.Value.Day));

                if ((pick_out.SelectedDate.Value.Day - pick_in.SelectedDate.Value.Day) == 0)
                {
                    if(type_picker.SelectedIndex == -1)
                    {
                        price = 0;
                    }
                    else if (type_picker.SelectedIndex == 0)
                    {
                        price = 6000;
                    }
                    else if (type_picker.SelectedIndex == 1)
                    {
                        price = 8000;
                    }
                    else if (type_picker.SelectedIndex == 2)
                    {
                        price = 10000;
                    }
                    else
                    {
                        price = 0;
                    }

                }
                else if(type_picker.SelectedIndex == -1)
                {
                    price = 0;
                }
                else if (type_picker.SelectedIndex == 0)
                {
                    price = price * 6000;
                }
                else if (type_picker.SelectedIndex == 1)
                {
                    price = price * 8000;
                }
                else if(type_picker.SelectedIndex == 2)
                {
                    price = price * 10000;
                }
                else
                {
                    price = 0;
                }

                txt_price.Text = (price.ToString() + ".00");
            }



        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            txt_count.Clear();
            txt_price.Clear();
            txt_room.Clear();
            type_picker.SelectedIndex = -1;
            pick_in.SelectedDate = null;
            pick_out.SelectedDate = null;
            price_picker.SelectedIndex = -1;
            grid_room.ItemsSource = null;
            blk_count.Visibility = Visibility.Hidden;
            blk_inOut.Visibility = Visibility.Hidden;
            blk_method.Visibility = Visibility.Hidden;
            blk_out.Visibility = Visibility.Hidden;
            blk_room.Visibility = Visibility.Hidden;
            blk_select.Visibility = Visibility.Hidden;
        }

        private void pick_in_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(pick_in.IsEnabled == true)
            {
                price_picker.SelectedIndex = -1;
            }
        }

        private void pick_out_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pick_out.IsEnabled == true)
            {
                price_picker.SelectedIndex = -1;
            }
        }

        private void type_picker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(type_picker.SelectedIndex == 0 || type_picker.SelectedIndex == 1)
            {
                pick_out.SelectedDate = null;
                price_picker.SelectedIndex = -1;
            }

        }

        private void type_picker_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_room.Visibility = Visibility.Hidden;
        }

        private void txt_count_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_count.Visibility = Visibility.Hidden;
        }

        private void txt_room_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_room.Visibility = Visibility.Hidden;
        }

        private void pick_in_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_inOut.Visibility = Visibility.Hidden;
        }

        private void pick_out_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_out.Visibility = Visibility.Hidden;
        }

        private void price_picker_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_method.Visibility = Visibility.Hidden;
        }


        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Customer_Reg obj = new Customer_Reg();
            obj.Show();
            this.Close();
        }

        private void grid_room_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView drv = gd.SelectedItem as DataRowView;

            if (drv != null)
            {
                txt_room.Text = drv["RoomID"].ToString();

            }
        }


        private void btn_availability_Click(object sender, RoutedEventArgs e)
        {
            grid_room.IsReadOnly = true;

            if (type_picker.SelectedIndex == -1)
            {
                blk_room.Visibility = Visibility.Visible;
                blk_room.Text = "*select a room type";
            }
            else
            {
                blk_room.Visibility = Visibility.Hidden;

                string q = "Select RoomID,Room_type,Room_Price,Room_Floor,Room_Availability from Hotel.Room where Room_type = '" + type_picker.Text + "' and Room_Availability = 'Yes' ";

                data.displayData(q);

                grid_room.ItemsSource = data.displayData(q).DefaultView;

                if (data.displayData(q).Rows.Count > 0)
                {
                    lbl_null.Visibility = Visibility.Hidden;

                    grid_room.Visibility = Visibility.Visible;
                }
                else if (data.displayData(q).Rows.Count == 0)
                {
                    lbl_null.Visibility = Visibility.Visible;

                    lbl_null.Content = "*No Records To show";

                    grid_room.Visibility = Visibility.Hidden;
                }
            }

        }

        private void btn_next_Click(object sender, RoutedEventArgs e)
        {
            bool type = false, pickin = false, pickout = false, room = false, count = false,pricepick = false, bill = false;

            if (string.IsNullOrEmpty(type_picker.Text))
            {
                blk_room.Visibility = Visibility.Visible;
                blk_room.Text = "*please select a room type";
            }
            else
            {
                blk_room.Visibility = Visibility.Hidden;
                type = true;
            }

            if (string.IsNullOrEmpty(pick_in.Text))
            {
                blk_inOut.Visibility = Visibility.Visible;
                blk_inOut.Text = "*please select check in date";

            }
            else
            {
                blk_inOut.Visibility = Visibility.Hidden;
                pickin = true;
            }

            if (string.IsNullOrEmpty(pick_out.Text))
            {
                blk_out.Visibility = Visibility.Visible;
                blk_out.Text = "*please select check out date";

            }
            else
            {
                blk_out.Visibility = Visibility.Hidden;
                pickout = true;
            }

            if (string.IsNullOrEmpty(txt_room.Text))
            {
                blk_select.Visibility = Visibility.Visible;
                blk_select.Text = "*please select a room";

            }
            else
            {
                blk_select.Visibility = Visibility.Hidden;
                room = true;
            }

            if (string.IsNullOrEmpty(price_picker.Text))
            {
                blk_method.Visibility = Visibility.Visible;
                blk_method.Text = "*please select a payment method";

            }
            else
            {
                blk_method.Visibility = Visibility.Hidden;
                pricepick = true;
            }


            if (string.IsNullOrEmpty(txt_count.Text))
            {
                blk_count.Visibility = Visibility.Visible;
                blk_count.Text = "*cannot be blank";

            }
            else if (txt_count.Text.Any(char.IsLetter))
            {
                blk_count.Visibility = Visibility.Visible;
                blk_count.Text = "*enter a count";
               
            }
            else
            {
                blk_count.Visibility = Visibility.Hidden;
                count = true;
            }

            double price = Convert.ToDouble(txt_price.Text);

            if(price < 0)
            {
                blk_out.Visibility = Visibility.Visible;
                blk_out.Text = "*invalid date";
            }
            else if(price >= 0)
            {
                blk_out.Visibility = Visibility.Hidden;
                bill = true;

            }

            if (txt_room.Text.Length > 0 && count == true && bill == true && txt_price.Text.Length > 0 && pick_in.Text.Length> 0 && pick_out.Text.Length > 0 && type_picker.SelectedIndex!=-1 && price_picker.SelectedIndex != -1)
            {
                Confirmation obj = new Confirmation(cusId, cusName, cusAdd, cusEmail, cusTel, txt_room.Text, txt_price.Text, pick_in.Text, pick_out.Text,recID,type_picker.Text,txt_count.Text);
                obj.Show();
                this.Close();
            }
        }

        private void txt_room_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}


