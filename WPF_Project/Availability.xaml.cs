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
    /// Interaction logic for Availability.xaml
    /// </summary>
    public partial class Availability : Window
    {
        public Availability()
        {
            InitializeComponent();
        }
        SqlDataAdapter da;
        SqlConnection con;
        Database data = new Database();



        private void txt_check_Click(object sender, RoutedEventArgs e)
        {
            if (txt_id.Text.Length == 0 && rtype_picker.SelectedIndex == -1 && ftype_picker.SelectedIndex == -1)
            {
                lbl_null.Visibility = Visibility.Visible;
                grid_room.Visibility = Visibility.Hidden;
                grid_room.IsReadOnly = true;
                lbl_null.Content = "select one or more options to display records";
            }

            con = new SqlConnection("Server=tcp:raveeshanibm.database.windows.net,1433;Initial Catalog=Hotel_Reservation;Persist Security Info=False;User ID=RaveeshaNIBM;Password=Jj070300;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            con.Open();

            CheckAvailability();

            con.Close();
        }


        public void ShowData()
        {
            DataTable dt = new DataTable();

            da.Fill(dt);

            grid_room.ItemsSource = dt.DefaultView;
            grid_room.IsReadOnly = true;

            if (dt.Rows.Count > 0)
            {
                lbl_null.Visibility = Visibility.Hidden;

                grid_room.Visibility = Visibility.Visible;
            }
            else if (dt.Rows.Count == 0)
            {
                lbl_null.Visibility = Visibility.Visible;

                lbl_null.Content = "*No Records To show";

                grid_room.Visibility = Visibility.Hidden;
            }
        }

        public void CheckAvailability()
        {
            if (txt_id.Text.Length > 0 && rtype_picker.SelectedIndex == -1 && ftype_picker.SelectedIndex == -1)
            {
                da = new SqlDataAdapter("Select RoomID,Room_Availability,Room_Floor,Room_Price,Room_Type from Hotel.Room where RoomID = '" + txt_id.Text + "'", con);

                ShowData();
            }
            else if (txt_id.Text.Length > 0 && (rtype_picker.SelectedIndex == 0 || rtype_picker.SelectedIndex == 1 || rtype_picker.SelectedIndex == 2) && ftype_picker.SelectedIndex == -1)
            {
                da = new SqlDataAdapter("Select RoomID,Room_Availability,Room_Floor,Room_Price,Room_Type from Hotel.Room where RoomID = '" + txt_id.Text + "' and Room_type = '" + rtype_picker.Text + "' ", con);

                ShowData();
            }
            else if (txt_id.Text.Length > 0 && rtype_picker.SelectedIndex == -1 && (ftype_picker.SelectedIndex == 0 || ftype_picker.SelectedIndex == 1 || ftype_picker.SelectedIndex == 2))
            {
                da = new SqlDataAdapter("Select RoomID,Room_Availability,Room_Floor,Room_Price,Room_Type from Hotel.Room where RoomID = '" + txt_id.Text + "' and Room_Floor = '" + ftype_picker.Text + "' ", con);

                ShowData();
            }
            else if (txt_id.Text.Length == 0 && (rtype_picker.SelectedIndex == 0 || rtype_picker.SelectedIndex == 1 || rtype_picker.SelectedIndex == 2) && ftype_picker.SelectedIndex == -1)
            {
                da = new SqlDataAdapter("Select RoomID,Room_Availability,Room_Floor,Room_Price,Room_Type from Hotel.Room where  Room_type = '" + rtype_picker.Text + "' ", con);

                ShowData();
            }
            else if (txt_id.Text.Length == 0 && rtype_picker.SelectedIndex == -1 && (ftype_picker.SelectedIndex == 0 || ftype_picker.SelectedIndex == 1 || ftype_picker.SelectedIndex == 2))
            {
                da = new SqlDataAdapter("Select RoomID,Room_Availability,Room_Floor,Room_Price,Room_Type from Hotel.Room where Room_Floor = '" + ftype_picker.Text + "' ", con);

                ShowData();
            }
            else if (txt_id.Text.Length == 0 && (rtype_picker.SelectedIndex == 0 || rtype_picker.SelectedIndex == 1 || rtype_picker.SelectedIndex == 2) && (ftype_picker.SelectedIndex == 0 || ftype_picker.SelectedIndex == 1 || ftype_picker.SelectedIndex == 2))
            {
                da = new SqlDataAdapter("Select RoomID,Room_Availability,Room_Floor,Room_Price,Room_Type from Hotel.Room where Room_Floor = '" + ftype_picker.Text + "' and Room_type = '" + rtype_picker.Text + "' ", con);

                ShowData();
            }
            else if (txt_id.Text.Length > 0 && (rtype_picker.SelectedIndex == 0 || rtype_picker.SelectedIndex == 1 || rtype_picker.SelectedIndex == 2) && (ftype_picker.SelectedIndex == 0 || ftype_picker.SelectedIndex == 1 || ftype_picker.SelectedIndex == 2))
            {
                da = new SqlDataAdapter("Select RoomID,Room_Availability,Room_Floor,Room_Price,Room_Type from Hotel.Room where RoomID = '" + txt_id.Text + "'and Room_Floor = '" + ftype_picker.Text + "' and Room_type = '" + rtype_picker.Text + "' ", con);

                ShowData();
            }

            blk_id.Visibility = Visibility.Hidden;

        }

        private void btn_clear_id_Click(object sender, RoutedEventArgs e)
        {
            txt_id.Clear();
        }

        private void btn_clear_type_Click(object sender, RoutedEventArgs e)
        {
            rtype_picker.SelectedIndex = -1;
        }

        private void btn_clear_floor_Click(object sender, RoutedEventArgs e)
        {
            ftype_picker.SelectedIndex = -1;
        }

        private void btn_aval_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_id.Text))
            {
                blk_id.Visibility = Visibility.Visible;
                blk_id.Text = "*enter a room ID";
            }
            else
            {
                blk_id.Visibility = Visibility.Hidden;

                string q = "Update Hotel.Room set Room_Availability = 'Yes' where RoomID = '" + txt_id.Text + "'";

                int i = data.save_update_delete(q);

                if (i == 3)
                {
                    SuccessNoti obj = new SuccessNoti();
                    obj.lbl_save.Content = "Data Updated Successfully";
                    obj.Show();

                    grid_room.ItemsSource = null;
                }
                else
                {
                    ErrorNoti obj = new ErrorNoti();
                    obj.lbl_error.Content = "Data Was Not Updated";
                    obj.Show();
                }
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Rec_Start obj = new Rec_Start();
            obj.Show();
            this.Close();
        }

        private void rtype_picker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ftype_picker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void txt_id_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_id.Visibility = Visibility.Hidden;
        }

        private void grid_room_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid gd = (DataGrid)sender;
                DataRowView drv = gd.SelectedItem as DataRowView;

                if (drv != null)
                {
                    txt_id.Text = drv["RoomID"].ToString();


                }
            }
            catch (InvalidOperationException)
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
}
