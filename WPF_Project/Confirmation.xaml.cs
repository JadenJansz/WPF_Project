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
using System.Net;
using System.Net.Mail;

namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for Confirmation.xaml
    /// </summary>
    public partial class Confirmation : Window
    {
        public string cusId,cusName,cusAdd,cusEmail,cusTel,recID,roomtype,count;

        private void btn_end_Click(object sender, RoutedEventArgs e)
        {
            Rec_Start obj = new Rec_Start();
            obj.Show();
            this.Close();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Customer_Reg obj = new Customer_Reg();
            obj.Show();
            this.Close();
        }

        public Confirmation(string a, string b, string c, string d, string e,string f,string g,string h,string i,string j,string k,string l)
        {
            InitializeComponent();

            cusId = a;
            blk_name.Text = b;
            cusAdd = c;
            cusEmail = d;
            blk_mobile.Text = e;
            blk_room.Text = f;
            blk_price.Text = g;
            blk_checkin.Text = h;
            blk_checkout.Text = i;
            recID = j;
            roomtype = k;
            count = l;
        }
        Database data = new Database();

        private void btn_confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection("Server=tcp:raveeshanibm.database.windows.net,1433;Initial Catalog=Hotel_Reservation;Persist Security Info=False;User ID=RaveeshaNIBM;Password=Jj070300;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                con.Open();
                SqlCommand cmd = new SqlCommand("Insert into Hotel.Customer values ('" + recID + "', '" + cusId + "','" + blk_name.Text + "','" + cusAdd + "','" + blk_mobile.Text + "','" + cusEmail + "') ", con);

                int i = cmd.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand("Insert into Hotel.Cus_Room values ('" + blk_room.Text + "','" + cusId + "','" + blk_checkin.Text + "','" + blk_checkout.Text + "','" + blk_price.Text + "','"+count+"') ", con);

                int w = cmd1.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("Update Hotel.Room set Room_Availability = 'No' where RoomID = '" + blk_room.Text + "' ", con);

                int r = cmd2.ExecuteNonQuery();

                if (i == 2 && w == 1 && r == 3)
                {

                    SuccessNoti obj = new SuccessNoti();
                    obj.lbl_save.Content = "Data Added Successfully !";
                    obj.Show();

                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress("OceanViewColombo@gmail.com");
                    message.To.Add(new MailAddress(cusEmail));
                    message.Subject = "Thank You!";
                    message.Body = "Dear Mr/Mrs."+ blk_name.Text +", \n\n" +
                        "Thank you for choosing Ocean View Hotel - Colombo! It is our pleasure to" +
                        " confirm your reservation.\n\nRoom No     : "+blk_room.Text+" \n\nRoom Type  : "+roomtype+" \n\nCheck In      : "+blk_checkin.Text+" \n\nCheck Out   : "+blk_checkout.Text+" " +
                        "\n\nPrice            : Rs."+blk_price.Text+"\n\nPlease advice us if any changes are needed to be" +
                        " made to this reservation by calling us at 071 563 9188. \n\nHope You enjoy your stay with us!" +
                        " \n\n\n OceanView Hotel \n 51 West Oceanside, Colombo \n Phone : 071 563 9188 / 075 098 0070" +
                        "\n This is an information email from OceanView Hotel";
                    smtp.Port = 587;
                    smtp.Host = "smtp.gmail.com"; 
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("OceanViewColombo@gmail.com", "Oceanview2021");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                }
                else
                {
                    ErrorNoti obj = new ErrorNoti();
                    obj.lbl_error.Content = "Failed To Update Information";
                    obj.Show();

                }

           }
           catch (SqlException)
           {
                ErrorNoti obj = new ErrorNoti();
                obj.lbl_error.Content = "Information Already Entered";
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
}
