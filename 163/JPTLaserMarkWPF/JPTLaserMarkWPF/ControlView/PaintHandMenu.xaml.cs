using Common;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JPTLaserMarkWPF.ControlView
{
    /// <summary>
    /// Interaction logic for PaintHandMenu.xaml
    /// </summary>
    public partial class PaintHandMenu : UserControl
    {
        PaintObj myPaint;
        public PaintHandMenu(PaintObj ParPaint)
        {
            InitializeComponent();
            myPaint = ParPaint;
            InitUI();
        }

        private void InitUI()
        {
            SetPenSize(1);
            //ThicknessSlider.Text = "Thickness";
            //ThicknessSlider.LeftImage.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Thickness_Small.png"));
            //ThicknessSlider.RightImage.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Thickness_Big.png"));
            //ThicknessSlider.Minimum = 2;
            //ThicknessSlider.Maximum = 16;
            //ThicknessSlider.Value = 8;
            //ThicknessSlider.GetSliderControl.ValueChanged += ThicknessSlider_ValueChanged;

            IntensitySlider.Text = "透明度";
            //IntensitySlider.LeftImage.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Intensity_Light.png"));
            //IntensitySlider.RightImage.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Intensity_Dark.png"));
            IntensitySlider.Minimum = 10;
            IntensitySlider.Maximum = 255;
            IntensitySlider.Value = 128;
            IntensitySlider.GetSliderControl.ValueChanged += IntensitySlider_ValueChanged;
        }

        private void ThicknessSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //myPaint.ChangeInkThickness(ThicknessSlider.Value);
            //myPaint.CurImgScale = (double)ImgScaleSlider.Value / 100;
        }

        private void IntensitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            myPaint.ChangeInkIntensity(IntensitySlider.Value);
        }
        public Image GetOKBtnControl() //Idx 0 =photo, 1= hand, 2= type
        {
            return OKImgBtn;
        }

        private void SetPenSize(int Idx)
        {
            Canvas.SetTop(img2Brd, 126);
            Canvas.SetTop(img1Brd, 126);
            Canvas.SetTop(img3Brd, 126);
            //2017/7/10添加
            Canvas.SetTop(img4Brd, 126);
            Canvas.SetTop(img5Brd, 126);

            img2Brd.BorderThickness = new Thickness(0, 0, 0, 0);
            img1Brd.BorderThickness = new Thickness(0, 0, 0, 0);
            img3Brd.BorderThickness = new Thickness(0, 0, 0, 0);
            //2017/7/10添加
            img4Brd.BorderThickness = new Thickness(0, 0, 0, 0);
            img5Brd.BorderThickness = new Thickness(0, 0, 0, 0);

            switch (Idx)
            {
                case 0:
                    {
                        myPaint.ChangeInkThickness(5);
                        Canvas.SetTop(img1Brd, 100);
                        img1Brd.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
                        break;
                    }
                case 1:
                    {
                        myPaint.ChangeInkThickness(10);
                        Canvas.SetTop(img2Brd, 100);
                        img2Brd.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
                        break;
                    }
                case 2:
                    {
                        myPaint.ChangeInkThickness(20);
                        Canvas.SetTop(img3Brd, 100);
                        img3Brd.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
                        break;
                    }
                case 3:
                    {
                        myPaint.ChangeInkThickness(8);
                        Canvas.SetTop(img4Brd, 100);
                        img4Brd.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
                        break;
                    }
                case 4:
                    {
                        myPaint.ChangeInkThickness(15);
                        Canvas.SetTop(img5Brd, 100);
                        img5Brd.BorderThickness = new Thickness(0.5, 0.5, 0.5, 0.5);
                        break;
                    }

            }
        }

        private void P1Img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetPenSize(0);
        }

        private void P2Img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetPenSize(1);
        }

        private void P3Img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetPenSize(2);
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void P4Img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetPenSize(3);
        }

        private void P5Img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SetPenSize(4);
        }

        public void UpdateUILanguage()
        {
            BrushSizeLbl.Content = MultiLanguage.GetBrushSizeLblString();
            IntensitySlider.Text = MultiLanguage.GetIntensityLblString();
        }
        public void UpdateUIColor()
        {
            switch (Para.appColor)
            {
                case ColorType.Blue:
                    P1Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/BlueP1.png"));
                    P2Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/BlueP2.png"));
                    P3Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/BlueP3.png"));
                    P4Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/BlueP4.png"));
                    P5Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/BlueP5.png"));
                    break;
                case ColorType.Pink:
                    P1Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/PinkP1.png"));
                    P2Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/PinkP2.png"));
                    P3Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/PinkP3.png"));
                    P4Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/PinkP4.png"));
                    P5Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/PinkP5.png"));
                    break;
                case ColorType.Green:
                    P1Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/GreenP1.png"));
                    P2Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/GreenP2.png"));
                    P3Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/GreenP3.png"));
                    P4Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/GreenP4.png"));
                    P5Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/GreenP5.png"));
                    break;
                case ColorType.Orange:
                    P1Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/OrangeP1.png"));
                    P2Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/OrangeP2.png"));
                    P3Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/OrangeP3.png"));
                    P4Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/OrangeP4.png"));
                    P5Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/OrangeP5.png"));
                    break;
                case ColorType.Purple:
                    P1Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/PurpleP1.png"));
                    P2Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/PurpleP2.png"));
                    P3Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/PurpleP3.png"));
                    P4Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/PurpleP4.png"));
                    P5Img.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/PurpleP5.png"));
                    break;
            }
        }
    }
}
