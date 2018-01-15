using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for MessageDlg.xaml
    /// </summary>
    public partial class MessageDlg : Window
    {
        public int Res = 0;
        public MessageDlg(string Title, string Message, bool isError)
        {
            InitializeComponent();            
            this.Title = Title;
            MsgBox.Text = Message;
            CancelBtn.IsEnabled = false;

            OKBtn.Content = MultiLanguage.GetOKBtnString();
            CancelBtn.Content = MultiLanguage.GetCancelBtnString();

            if (!isError)
            {
                Img.Visibility = System.Windows.Visibility.Hidden;
                InformationImg.Visibility = System.Windows.Visibility.Visible;
                CancelBtn.IsEnabled = true;
            }
        }

        private void Button_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Button_TouchDown_1(object sender, TouchEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CancelBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Res = -1;
            Close();
        }

        private void CancelBtn_TouchDown(object sender, TouchEventArgs e)
        {
            Res = -1;
            Close();
        }
    }
}
