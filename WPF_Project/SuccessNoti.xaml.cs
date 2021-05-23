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
using System.Windows.Threading;
using System.Threading;

namespace WPF_Project
{
    /// <summary>
    /// Interaction logic for SuccessNoti.xaml
    /// </summary>
    public partial class SuccessNoti : Window
    {
        DispatcherTimer timer = new DispatcherTimer();

        public SuccessNoti()
        {
            InitializeComponent();

            timer.Tick += new EventHandler(WaitingEvent);
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();
        }

        public void WaitingEvent(object source, EventArgs e)
        {
            this.Close();
            timer.Stop();
        }

    }
}
