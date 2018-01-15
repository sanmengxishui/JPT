using Common;
using JPTLaserMarkWPF.WebCam;
using SamLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JPTLaserMarkWPF.ControlView
{
    /// <summary>
    /// Interaction logic for WebCamControl.xaml
    /// </summary>
    public partial class WebCamControl : UserControl
    {
        //WebCamera myWebCam;
                
        public WebCamControl()
        {
            InitializeComponent();
            //myWebCam = new WebCamera(WebCImg);
            //myWebCam.GetAllWebCamConnected();   
            LeftSizeBtn.Visibility = System.Windows.Visibility.Hidden;
            RightSizeBtn.Visibility = System.Windows.Visibility.Hidden;
            SizeNameLbl.Visibility = System.Windows.Visibility.Hidden;
        }
        public Image GetArrowBtn(int Idx)
        {
            switch (Idx)
            {
                case 0: return UpImgBtn;
                case 1: return DownImgBtn;
                case 2: return LeftImageBtn;
                case 3: return RightImgBtn;
                case 4: return LeftSizeBtn;
                case 5: return RightSizeBtn;  
            }
            return null;           
        }        
        public Image GetPrintBtnControl() //Idx 0 =photo, 1= hand, 2= type
        {
            return PrintImgBtn;
        }
        public Image GetPrintCardBtnControl() //Idx 0 =photo, 1= hand, 2= type
        {
            return PrintCardImgBtn;
        }
        public Image GetOKBtnControl() //Idx 0 =photo, 1= hand, 2= type
        {
            return OKImgBtn;
        }

        public Image GetLightControl()
        {
            return lightImgBtn;
        }

        public void StartWebCam()
        {
            //myWebCam.Start(0);
        }

        public void StopWebCam()
        {
            //myWebCam.Stop();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //myWebCam.Stop();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {            
        }

        private void PrintImgBtn_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }

        private void lightImgBtn_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }
        
    }
}
