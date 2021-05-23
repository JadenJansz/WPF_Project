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
    /// Interaction logic for Customer_Reg.xaml
    /// </summary>
    public partial class Customer_Reg : Window
    {
        public Customer_Reg()
        {
            InitializeComponent();

            txt_inc.Visibility = Visibility.Hidden;

            string a = "select isnull(max(cast(Rec_ID as int)),0)+1 from Hotel.Customer";

            txt_inc.Text = data.displayData(a).Rows[0][0].ToString();
            txt_name.Focus();

            txt_id.Text = "C" + txt_inc.Text;
            txt_id.IsReadOnly = true;
        }

        Database data = new Database();

        private void btn_next_Click(object sender, RoutedEventArgs e)
        {
            Room_Reg obj = new Room_Reg(txt_id.Text, txt_name.Text, txt_add.Text, txt_email.Text, txt_tel.Text, txt_inc.Text);

            bool id = false, name = false, address = false, tel = false, email = false;

            if (string.IsNullOrEmpty(txt_id.Text))
            {
                blk_id.Visibility = Visibility.Visible;
                blk_id.Text = "*enter ID";

            }
            else
            {
                blk_id.Visibility = Visibility.Hidden;
                id = true;
            }

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

            if (string.IsNullOrEmpty(txt_add.Text))
            {
                blk_add.Visibility = Visibility.Visible;
                blk_add.Text = "*address cannot be blank";
            }
            else
            {
                blk_add.Visibility = Visibility.Hidden;
                address = true;
            }

            if (!Regex.IsMatch(txt_tel.Text, @"^7|0|(?:\+94)[0-9]{9,10}$") || txt_tel.Text.Length < 9 || txt_tel.Text.Any(char.IsLetter) || txt_tel.Text.Length > 10) 
            {
                blk_tel.Visibility = Visibility.Visible;
                blk_tel.Text = "*enter valid telephone number";
            }
            else if(txt_tel.Text.All(char.IsDigit))
            {
                blk_tel.Visibility = Visibility.Hidden;
                tel = true;
            }
            else
            {
                blk_tel.Visibility = Visibility.Visible;
                blk_tel.Text = "*enter valid telephone number";
            }

            if (!Regex.IsMatch(txt_email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                blk_email.Visibility = Visibility.Visible;
                blk_email.Text = "*enter a valid email ex:name@gmail.com";
            }
            else
            {
                blk_email.Visibility = Visibility.Hidden;
                email = true;
            }

            if(tel == true && id == true && name == true && address == true && email == true)
            {
                obj.Show();
                this.Close();
            }

        }

        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            txt_add.Clear();
            txt_email.Clear();
            txt_name.Clear();
            txt_tel.Clear();
            blk_add.Visibility = Visibility.Hidden;
            blk_email.Visibility = Visibility.Hidden;
            blk_id.Visibility = Visibility.Hidden;
            blk_name.Visibility = Visibility.Hidden;
            blk_tel.Visibility = Visibility.Hidden;

        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Rec_Start obj = new Rec_Start();
            obj.Show();
            this.Close();
        }

        private void txt_id_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_id.Visibility = Visibility.Hidden;
        }

        private void txt_name_GotFocus(object sender, RoutedEventArgs e)
        {
            blk_name.Visibility = Visibility.Hidden;
        }

        private void txt_add_GotFocus(object sender, RoutedEventArgs e)
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