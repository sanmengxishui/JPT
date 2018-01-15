using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaction logic for LoadingForm.xaml
    /// </summary>
    public partial class ResSelectForm : Window
    {
        public int ResIdx = 2;

        public ResSelectForm()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        
        private void gifP_MediaEnded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ResIdx = 1;
            Close();
        }

        private IntPtr GetWindowHandle(string Name)
        {
            Process[] processes = Process.GetProcessesByName(Name);
            foreach (Process p in processes)
            {
                return p.Handle;//p.MainWindowHandle;
            }
            return (IntPtr)0;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            IntPtr WndHdl = GetWindowHandle("stritz");
            ShowWindow(WndHdl, 2);
            //CandyCrushPro.WindowStyle = ProcessWindowStyle.Minimized;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ResIdx = 2;
            Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ResIdx = 3;
            Close();
        }

        private void Button_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            ResIdx = 1;
            Close();
        }

        private void Button_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            ResIdx = 2;
            Close();
        }

        private void Button_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            ResIdx = 3;
            Close();
        }

        private void Button_TouchDown_1(object sender, TouchEventArgs e)
        {
            ResIdx = 2;
            Close();
        }

        private void Button_TouchDown_2(object sender, TouchEventArgs e)
        {
            ResIdx = 1;
            Close();
        }

        private void Button_TouchDown_3(object sender, TouchEventArgs e)
        {
            ResIdx = 3;
            Close();
        }
    }
}
