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
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using System.Data;

namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        public Report()
        {
            InitializeComponent();
            
        }

        private DataTable GetData(string a)
        {
            DataTable dt = new DataTable();
            string conn = "Server=tcp:raveeshanibm.database.windows.net,1433;Initial Catalog=Hotel_Reservation;Persist Security Info=False;User ID=RaveeshaNIBM;Password=Jj070300;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            using (SqlConnection cn = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand(a, cn);

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dt);
            }

            return dt;
        }

        private void btn_display_Click(object sender, RoutedEventArgs e)
        {
            string q = "Select* from Hotel.UserT";

            ReportViewerDemo.Reset();
            DataTable dt = GetData(q);
            ReportDataSource ds = new ReportDataSource("DataSet1", dt);
            ReportViewerDemo.LocalReport.DataSources.Add(ds);

            ReportViewerDemo.LocalReport.ReportEmbeddedResource = "WPF_Project.UserReport.rdlc";
            ReportViewerDemo.RefreshReport();
        }

        private void btn_date_Click(object sender, RoutedEventArgs e)
        {
            string q = "Select* from Hotel.Cus_Room where Check_In >= '"+start_date.Text+"' and Check_Out <= '"+end_date.Text+"' ";

            ReportViewerDemo.Reset();
            DataTable dt = GetData(q);
            ReportDataSource ds = new ReportDataSource("DataSet1", dt);
            ReportViewerDemo.LocalReport.DataSources.Add(ds);

            ReportViewerDemo.LocalReport.ReportEmbeddedResource = "WPF_Project.ReportReservation.rdlc";
            ReportViewerDemo.RefreshReport();
        }

        private void btn_location_Click(object sender, RoutedEventArgs e)
        {
            string q = "Select* from Hotel.Customer where Cus_Address = '"+txt_location.Text+"' ";

            ReportViewerDemo.Reset();
            DataTable dt = GetData(q);
            ReportDataSource ds = new ReportDataSource("DataSet1", dt);
            ReportViewerDemo.LocalReport.DataSources.Add(ds);

            ReportViewerDemo.LocalReport.ReportEmbeddedResource = "WPF_Project.ReportLocation.rdlc";
            ReportViewerDemo.RefreshReport();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Manager_Start obj = new Manager_Start();
            obj.Show();
            this.Close();
        }
    }
}
