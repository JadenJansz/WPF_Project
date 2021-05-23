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


namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for Rec_Start.xaml
    /// </summary>
    public partial class Rec_Start : Window
    {
        public Rec_Start()
        {
            InitializeComponent();
        }
        Customer_Reg cus_reg = new Customer_Reg();
        Edit_Customer cus_edit = new Edit_Customer();
     

        private void btn_book_Click(object sender, RoutedEventArgs e)
        {
            cus_reg.Show();
            this.Close();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_available_Click(object sender, RoutedEventArgs e)
        {
            Availability aval = new Availability();
            aval.Show();
            this.Close();
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            cus_edit.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //sif(MessageBox.Show("Do you want to sign out ?","Select Here",MessageBoxButton.YesNo,MessageBoxImage.Question)==MessageBoxResult.Yes)
            //{
            //  MainWindow obj = new MainWindow();
            //obj.Show();
            //this.Close();
            //}
            //else
            //{
            //  this.Show();
            //}

            MBox obj = new MBox();
            obj.Show();
            obj.Owner = this;

        }

        
    }
}
