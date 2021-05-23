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
    /// Interaction logic for Edit_Room.xaml
    /// </summary>
    public partial class Edit_Room : Window
    {
        public Edit_Room()
        {
            InitializeComponent();
        }
        Database data = new Database();

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txt_ID.Text))
                {
                    blk_id.Visibility = Visibility.Visible;
                    blk_id.Text = "*enter a room ID";
                }
                else
                {
                    blk_id.Visibility = Visibility.Hidden;
                }

                if (type_picker.SelectedIndex == -1)
                {
                    blk_type.Visibility = Visibility.Visible;
                    blk_type.Text = "*select a room type";
                }
                else
                {
                    blk_type.Visibility = Visibility.Hidden;
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
                    return;
                }
                else
                {
                    blk_price.Visibility = Visibility.Hidden;
                }
                if (floor_picker.SelectedIndex == -1)
                {
                    blk_floor.Visibility = Visibility.Visible;
                    blk_floor.Text = "*select a floor";
                }
                else
                {
                    blk_floor.Visibility = Visibility.Hidden;
                }
                if (aval_picker.SelectedIndex == -1)
                {
                    blk_aval.Visibility = Visibility.Visible;
                    blk_aval.Text = "*select an availability";
                }
                else
                {
                    blk_aval.Visibility = Visibility.Hidden;
                }
                if (type_picker.SelectedIndex != -1 && floor_picker.SelectedIndex != -1 && aval_picker.SelectedIndex != -1 && txt_price.Text.Length > 1)
                {
                    string q = "Update Hotel.Room set Room_type = '" + type_picker.Text + "',Room_Price = '" + txt_price.Text + "',Room_Floor = '" + floor_picker.Text + "',Room_Availability = '" + aval_picker.Text + "' where RoomID = '" + txt_ID.Text + "' ";

                    int i = data.save_update_delete(q);

                    if (i == 3)
                    {
                        SuccessNoti obj = new SuccessNoti();
                        obj.lbl_save.Content = "Data Updated Successfully";
                        obj.Show();
                    }
                    else
                    {
                        ErrorNoti obj = new ErrorNoti();
                        obj.lbl_error.Content = "Data Was Not Updated";
                        obj.Show();
                    }
                }
            }
            catch(SqlException)
            {
                ErrorNoti obj = new ErrorNoti();
                obj.lbl_error.Content = "Please Check Again";
                obj.Show();
            }
        }

        private void btn_search_Click(object sender, RoutedEventArgs e)
        {
            grid_room.IsReadOnly = true;

            blk_id.Visibility = Visibility.Hidden;

            string w = "Select RoomID,Room_type,Room_Price,Room_Floor,Room_Availability from Hotel.Room where RoomID = '" + txt_ID.Text + "' ";

            data.displayData(w);

            if(data.displayData(w).Rows.Count < 1)
            {
                grid_room.Visibility = Visibility.Hidden;
                lbl_records.Visibility = Visibility.Visible;
                lbl_records.Text = "*No records to display";

                txt_price.Clear();
                type_picker.SelectedIndex = -1;
                floor_picker.SelectedIndex = -1;
                aval_picker.SelectedIndex = -1;
            }
            else
            {
                try
                {
                    lbl_records.Visibility = Visibility.Hidden;
                    grid_room.Visibility = Visibility.Visible;

                    SqlConnection con = new SqlConnection("Server=tcp:raveeshanibm.database.windows.net,1433;Initial Catalog=Hotel_Reservation;Persist Security Info=False;User ID=RaveeshaNIBM;Password=Jj070300;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

                    con.Open();

                    SqlCommand cmd = new SqlCommand("Select RoomID,Room_type,Room_Price,Room_Floor,Room_Availability from Hotel.Room where RoomID = @ID", con);
                    cmd.Parameters.AddWithValue("@ID", (txt_ID.Text));
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        type_picker.Text = dr.GetValue(1).ToString();
                        txt_price.Text = dr.GetValue(2).ToString();
                        floor_picker.Text = dr.GetValue(3).ToString();
                        aval_picker.Text = dr.GetValue(4).ToString();
                       
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

            grid_room.ItemsSource = data.displayData(w).DefaultView;

            if(string.IsNullOrEmpty(txt_ID.Text))
            {
                blk_id.Visibility = Visibility.Visible;
                blk_id.Text = "*enter a room ID";
            }
            else
            {
                blk_id.Visibility = Visibility.Hidden;
            }


        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_ID.Text))
            {
                blk_id.Visibility = Visibility.Visible;
                blk_id.Text = "*enter a room ID";
            }
            else
            {
                blk_id.Visibility = Visibility.Hidden;
            }

            if (txt_ID.Text.Length > 1)
            {
                try
                {
                    string q = "Delete from Hotel.Room where RoomID = '" + txt_ID.Text + "' ";

                    int o = data.save_update_delete(q);

                    if (o == 2)
                    {
                        SuccessNoti obj = new SuccessNoti();
                        obj.lbl_save.Content = "Data Deleted Successfully";
                        obj.Show();

                        txt_ID.Clear();
                        txt_price.Clear();
                        type_picker.SelectedIndex = -1;
                        floor_picker.SelectedIndex = -1;
                        aval_picker.SelectedIndex = -1;
                        grid_room.ItemsSource = null;
                    }
                    else
                    {
                        ErrorNoti obj = new ErrorNoti();
                        obj.lbl_error.Content = "Data Was Not Deleted";
                        obj.Show();
                    }
                }
                catch (SqlException)
                {
                    ErrorNoti obj = new ErrorNoti();
                    obj.lbl_error.Content = "This Room Is Use";
                    obj.Show();
                }
                catch (Exception)
                {
                    ErrorNoti obj = new ErrorNoti();
                    obj.lbl_error.Content = "Error! Please try again";
                    obj.Show();
                }
            }
            else if(string.IsNullOrEmpty(txt_ID.Text))
            {
                blk_id.Visibility = Visibility.Visible;
                blk_id.Text = "*enter a room ID";
            }
            else
            {
                blk_id.Visibility = Visibility.Hidden;
            }
        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            txt_ID.Clear();
            txt_price.Clear();
            type_picker.SelectedIndex = -1;
            floor_picker.SelectedIndex = -1;
            aval_picker.SelectedIndex = -1;
            blk_aval.Visibility = Visibility.Hidden;
            blk_floor.Visibility = Visibility.Hidden;
            blk_id.Visibility = Visibility.Hidden;
            blk_price.Visibility = Visibility.Hidden;
            blk_type.Visibility = Visibility.Hidden;
            grid_room.ItemsSource = null;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Admin_start obj = new Admin_start();
            obj.Show();
            this.Close();
        }

        private void grid_room_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
     
                DataGrid gd = (DataGrid)sender;
                DataRowView drv = gd.SelectedItem as DataRowView;

                if (drv != null)
                {
                    txt_ID.Text = drv["RoomID"].ToString();
                    type_picker.Text = drv["Room_type"].ToString();
                    txt_price.Text = drv["Room_Price"].ToString();
                    floor_picker.Text = drv["Room_Floor"].ToString();
                    aval_picker.Text = drv["Room_Availability"].ToString();

                }         
 
        }

        private void floor_picker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void txt_ID_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_id.Visibility = Visibility.Hidden;
        }

        private void type_picker_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_type.Visibility = Visibility.Hidden;
        }

        private void txt_price_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_price.Visibility = Visibility.Hidden;
        }

        private void floor_picker_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_floor.Visibility = Visibility.Hidden;
        }

        private void aval_picker_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_aval.Visibility = Visibility.Hidden;
        }
    }
}
