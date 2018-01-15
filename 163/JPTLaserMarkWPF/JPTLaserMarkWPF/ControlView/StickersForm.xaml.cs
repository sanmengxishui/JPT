using JPTLaserMarkWPF.PaintObject;
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
    /// Interaction logic for StickersForm.xaml
    /// </summary>
    public partial class StickersForm : Window
    {
        private PaintObj myPaint;
        public StickersForm(PaintObj ParPaint)
        {
            InitializeComponent();
            myPaint = ParPaint;
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            StickerGalleryCtrl.Text = "Sticker";
            StickerGalleryCtrl.ImageFileDirectory = System.AppDomain.CurrentDomain.BaseDirectory + "Stickers";
            StickerGalleryCtrl.LayoutRoot.MouseLeftButtonDown += StickerGalleryCtrl_MouseLeftButtonDown;
        }

        private void StickerGalleryCtrl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
            {
                // Do double-click code  
                string fileName = StickerGalleryCtrl.GetSelectedImageFilePath();
                myPaint.ImportImageGreyScale(fileName);
                Close();
                e.Handled = true;
            }
        }   
    }
}
