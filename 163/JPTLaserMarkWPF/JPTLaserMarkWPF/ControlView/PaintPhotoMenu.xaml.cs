using Common;
using JPTLaserMarkWPF.PaintObject;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JPTLaserMarkWPF.ControlView
{
    /// <summary>
    /// Interaction logic for PaintPhotoMenu.xaml
    /// </summary>
    public partial class PaintPhotoMenu : UserControl
    {
        PaintObj myPaint;
        bool isOpenImgMd = false;
        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        public PaintPhotoMenu(PaintObj ParPaint)
        {
            InitializeComponent();
            myPaint = ParPaint;
            InitUI();            

            dlg.Title = "Open Image";
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

        }

        private void InitUI()
        {
            ImgGalleryCtrl.ImageFileDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "Gallery";
            ImgGalleryCtrl.LayoutRoot.MouseLeftButtonDown += ImgGalleryCtr_MouseLeftButtonDown;

            StickerGalleryCtrl.Text = "Sticker";
            StickerGalleryCtrl.ImageFileDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "Stickers";
            StickerGalleryCtrl.LayoutRoot.MouseLeftButtonDown += StickerGalleryCtrl_MouseLeftButtonDown;
            //ImgScaleSlider.Text = "Scale";            
            //ImgScaleSlider.LeftImage.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/zoom-out-icon-png.png"));
            //ImgScaleSlider.RightImage.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Zoom In-01.png"));
            //ImgScaleSlider.Minimum = 20;
            //ImgScaleSlider.Maximum = 180;
            //ImgScaleSlider.Value = 100;
            //ImgScaleSlider.GetSliderControl.ValueChanged += ScaleSlider_ValueChanged;
        }

        private void StickerGalleryCtrl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
            {
                // Do double-click code  
                string fileName = StickerGalleryCtrl.GetSelectedImageFilePath();
                myPaint.ImportImageGreyScale(fileName);
                e.Handled = true;
            }
        }   

        private void ImgGalleryCtr_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
            {
                // Do double-click code  
                string fileName = ImgGalleryCtrl.GetSelectedImageFilePath();
                myPaint.ImportImageGreyScale(fileName);    
                e.Handled = true;
            }
        }   

        public Image GetOKBtnControl() //Idx 0 =photo, 1= hand, 2= type
        {            
            return OKImgBtn;
        }
        private void ScaleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //myPaint.CurImgScale = (double)ImgScaleSlider.Value / 100;
        }

        private void OpenImgBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isOpenImgMd = true;
            //this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            //    new Action(delegate()
            //    {
            //        // Create OpenFileDialog 
            //        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            //        dlg.Title = "Open Image";
            //        dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            //        Nullable<bool> result = dlg.ShowDialog();

            //        // Get the selected file name and display in a TextBox 
            //        if (result == true)
            //        {
            //            myPaint.ImportImageGreyScale(dlg.FileName);
            //            //ImgScaleSlider.Value = 100;               
            //        }       
            //    }));

            //Thread.Sleep(500);
            // Create OpenFileDialog 
            
            //Nullable<bool> result = dlg.ShowDialog();

            //// Get the selected file name and display in a TextBox 
            //if (result == true)
            //{
            //    myPaint.ImportImageGreyScale(dlg.FileName);
            //    //ImgScaleSlider.Value = 100;               
            //}            
        }

        private void OKImgBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void AddImgBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string fileName = ImgGalleryCtrl.GetSelectedImageFilePath();
            myPaint.ImportImageGreyScale(fileName);                    
            //ImgScaleSlider.Value = 100;            
        }

        private void AddStickerImgBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string fileName = StickerGalleryCtrl.GetSelectedImageFilePath();
            myPaint.ImportImageGreyScale(fileName);  
        }

        private void AlignImageBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            myPaint.AlignLastSelectedItem();
        }

        private void OpenImgBtn_TouchDown(object sender, TouchEventArgs e)
        {
            // Create OpenFileDialog 
            //Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            //dlg.Title = "Open Image";
            //dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                myPaint.ImportImageGreyScale(dlg.FileName);
                //ImgScaleSlider.Value = 100;               
            }            
        }

        private void OpenImgBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //if (isOpenImgMd)
            //{
            //    Nullable<bool> result = dlg.ShowDialog();

            //    // Get the selected file name and display in a TextBox 
            //    if (result == true)
            //    {
            //        myPaint.ImportImageGreyScale(dlg.FileName);
            //        //ImgScaleSlider.Value = 100;               
            //    }
            //}
            //isOpenImgMd = false;
        }

        private void OpenImgBtn_LostFocus(object sender, RoutedEventArgs e)
        {
            isOpenImgMd = false;
        }

        private void ServerImgBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ServerForm mySvr = new ServerForm(myPaint);
            mySvr.ShowDialog();

        }

        private void StickerImgBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StickersForm mySvr = new StickersForm(myPaint);
            mySvr.ShowDialog();
        }

        public void UpdateUILanguage()
        {
            ImgGalleryCtrl.Text = MultiLanguage.GetImageLblString();
        }
    }
}
