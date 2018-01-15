using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JPTLaserMarkWPF.ControlView
{
    /// <summary>
    /// Processing.xaml 的交互逻辑
    /// </summary>
    public partial class Processing : Window
    {
        int bufferStr = 0;
        System.Windows.Forms.Timer myTimer;
        public Processing()
        {
            InitializeComponent();
        }

        bool bClose = false;

        public void DestroyForm()
        {
            bClose = true;
            //mediaEnded.LoadedBehavior = MediaState.Manual;
            //mediaEnded.Stop();
            //mediaEnded.Source = null;

            Thread.Sleep(200);
            //this.free();
        }
        private void process_Loaded(object sender, RoutedEventArgs e)
        {
            statusView.Content = "Loading......";
            //myTimer = new System.Windows.Forms.Timer();
            //myTimer.Tick += new EventHandler(timer1_Tick);
            //myTimer.Enabled = true;
            //myTimer.Interval = 10;
            //myTimer.Start();
        }
        private void timer1_Tick(object sender,EventArgs e)
        {

            pb.Value += 1;
            if (pb.Value > 100)
                pb.Value = 0;
        }

        private void process_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           // myTimer.Enabled = false;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void mediaEnded_MediaEnded_1(object sender, RoutedEventArgs e)
        {
            if (bClose)
                return;
            ((MediaElement)sender).Position = ((MediaElement)sender).Position.Add(TimeSpan.FromMilliseconds(1));
        }
    }
}
