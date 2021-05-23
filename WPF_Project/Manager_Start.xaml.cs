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
    /// Interaction logic for Manager_Start.xaml
    /// </summary>
    public partial class Manager_Start : Window
    {
        public Manager_Start()
        {
            InitializeComponent();
        }
        Report rep = new Report();
        Logs log = new Logs();

        private void btn_report_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            rep.Show();
        }

        private void btn_log_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            log.Show();
        }

        private void btn_signout_Click(object sender, RoutedEventArgs e)
        {
            MBox obj = new MBox();
            obj.Show();
            obj.Owner = this;
        }
    }
}
