using JPTLaserMarkWPF.ControlView;
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
using JPTLaserMarkWPF.PaintObject;
using SamLight;
using JPTLaserMarkWPF.Samlight;
using System.IO;
using Common;
using System.Diagnostics;

namespace JPTLaserMarkWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PaintControl myPaintCtrl;
        SettingsMenu mySet;
        public SamLightClass SLMgr;        
        public MainWindow()
        {
            InitializeComponent();
            LoadMachineSettings();
            SamlightForm smFrm = new SamlightForm();
            SLMgr = new SamLightClass(smFrm.GetControl());
            mySet = new SettingsMenu(this);
            mySet.GetSaveBtnControl().MouseDown += SettingSaveBtn_MouseDown;
            myPaintCtrl = new PaintControl(SLMgr);
            UpdateLanguage();
            UpdateUIColor();
            if (Para.AutoStartSamlightSW)
            {
                string exeString = Para.SamlightAppPath;//@"D:\scaps\samlight\sam_light.exe";
                if (!Helper.IsProcessOpen("sam_light"))
                {
                    Process.Start(exeString);
                    //using (Process exeProcess = Process.Start(exeString))
                    //{
                    //    exeProcess.WaitForExit();
                    //}
                }

            }
            SLMgr.HideSamlightWindow();
            
            //SLMgr.Get_Output(5);
            //SLMgr.Get_Output(0);
            //SLMgr.Get_Output(1);
            //SLMgr.Get_Output(2);
            //SLMgr.Get_Output(3);
            //SLMgr.Get_Output(4);

            VersionLbl.Content = Para.SWVersion;
        }
        private void SettingSaveBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MenuPnl.Children.Remove(mySet);
            MenuPnl.Children.Add(myPaintCtrl);
            mySet.ApplyAllSettings();
            SaveMachineSettings();
        }
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {            
            MenuPnl.Children.Add(myPaintCtrl);
        }
        private void LoadMachineSettings()
        {
            string MchFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "Settings.xml";

            if (!File.Exists(MchFilePath))
                return;

            string strread = "";

            FileOperation.ReadData(MchFilePath, "Machine", "WebCamToLaserScale", ref strread);
            if (strread != "0")
                Para.WebCamToLaserHeadScale = float.Parse(strread);

            FileOperation.ReadData(MchFilePath, "Machine", "SoftwareImageToLaserScaleX", ref strread);
            if (strread != "0")
                Para.SWImgToSamlightXScale = float.Parse(strread);

            FileOperation.ReadData(MchFilePath, "Machine", "SoftwareImageToLaserScaleY", ref strread);
            if (strread != "0")
                Para.SWImgToSamlightYScale = float.Parse(strread);

            FileOperation.ReadData(MchFilePath, "Machine", "SoftwareImageToLaserScaleXS", ref strread);
            if (strread != "0")
                Para.SWImgToSamlightXSScale = float.Parse(strread);

            FileOperation.ReadData(MchFilePath, "Machine", "SoftwareImageToLaserScaleYS", ref strread);
            if (strread != "0")
                Para.SWImgToSamlightYSScale = float.Parse(strread);

            FileOperation.ReadData(MchFilePath, "Machine", "SoftwareImageToLaserScaleXL", ref strread);
            if (strread != "0")
                Para.SWImgToSamlightXLScale = float.Parse(strread);

            FileOperation.ReadData(MchFilePath, "Machine", "SoftwareImageToLaserScaleYL", ref strread);
            if (strread != "0")
                Para.SWImgToSamlightYLScale = float.Parse(strread);

            FileOperation.ReadData(MchFilePath, "Machine", "SamlightOnline", ref strread);
            if (strread != "0")
                Para.SamlightOnline = bool.Parse(strread);

            FileOperation.ReadData(MchFilePath, "Machine", "SamlightGreyScale", ref strread);
            if (strread != "0")
                Para.SamlightGreyScale = bool.Parse(strread);

            FileOperation.ReadData(MchFilePath, "Machine", "AutoStartSamlight", ref strread);
            if (strread != "0")
                Para.AutoStartSamlightSW = bool.Parse(strread);

            FileOperation.ReadData(MchFilePath, "Machine", "AutoConnectWebCam", ref strread);
            if (strread != "0")
                Para.WebCamStartAppStart = bool.Parse(strread);

            FileOperation.ReadData(MchFilePath, "Machine", "ShowMarkingProcess", ref strread);
            if (strread != "0")
                Para.ShowMarkingProcess = bool.Parse(strread);

            FileOperation.ReadData(MchFilePath, "Machine", "SMDitherStepHigh", ref strread);
            if (strread != "0")
                Para.SMDitherStepHigh = float.Parse(strread);

            FileOperation.ReadData(MchFilePath, "Machine", "SMDitherStepLow", ref strread);
            if (strread != "0")
                Para.SMDitherStepLow = float.Parse(strread);
            
            FileOperation.ReadData(MchFilePath, "Machine", "SMDitherStepStandard", ref strread);
            if (strread != "0")
                Para.SMDitherStepStandard = float.Parse(strread);

            FileOperation.ReadData(MchFilePath, "Machine", "SMBitmapInverted", ref strread);
            if (strread != "0")
                Para.SMBitmapInverted = bool.Parse(strread);

            FileOperation.ReadData(MchFilePath, "Machine", "SMCenterToActualCenterX", ref strread);
            if (strread != "0")
                Para.SMCenterToActualCenterX = float.Parse(strread);

            FileOperation.ReadData(MchFilePath, "Machine", "SMCenterToActualCenterY", ref strread);
            if (strread != "0")
                Para.SMCenterToActualCenterY = float.Parse(strread);
            
            FileOperation.ReadData(MchFilePath, "Machine", "DefaultWebCamIndex", ref strread);
            if (strread != "0")
                Para.DefaultWebCamIndex = int.Parse(strread);

            FileOperation.ReadData(MchFilePath, "Machine", "SamlightAppPath", ref strread);
            if (strread != "0")
                Para.SamlightAppPath = strread;

            FileOperation.ReadData(MchFilePath, "Marking", "PhoneSetPower", ref strread);
            if (strread != "0")
                Para.PhoneSetLaserPower = double.Parse(strread);
            FileOperation.ReadData(MchFilePath, "Marking", "PhoneSetFreq", ref strread);
            if (strread != "0")
                Para.PhoneSetFreq = double.Parse(strread);
            FileOperation.ReadData(MchFilePath, "Marking", "PhoneSetSpeed", ref strread);
            if (strread != "0")
                Para.PhoneSetSpeed = double.Parse(strread);

            FileOperation.ReadData(MchFilePath, "Marking", "CardSetPower", ref strread);
            if (strread != "0")
                Para.CardSetLaserPower = double.Parse(strread);
            FileOperation.ReadData(MchFilePath, "Marking", "CardSetFreq", ref strread);
            if (strread != "0")
                Para.CardSetFreq = double.Parse(strread);
            FileOperation.ReadData(MchFilePath, "Marking", "CardSetSpeed", ref strread);
            if (strread != "0")
                Para.CardSetSpeed = double.Parse(strread);

            FileOperation.ReadData(MchFilePath, "Language", "Language", ref strread);
            if (strread != "0")
            {
                LanguageType lg = LanguageType.English;
                Enum.TryParse(strread, out lg);
                MultiLanguage.AppLanguage = lg;
            }

            FileOperation.ReadData(MchFilePath, "Color", "Color", ref strread);
            if (strread != "0")
            {
                ColorType ct = ColorType.Blue;
                Enum.TryParse(strread, out ct);
                Para.appColor = ct;
            }
            
        }

        private void SaveMachineSettings()
        {
            string MchFilePath = System.AppDomain.CurrentDomain.BaseDirectory + "Settings.xml";

            FileOperation.SaveData(MchFilePath, "Machine", "WebCamToLaserScale", Para.WebCamToLaserHeadScale.ToString("F2"));

            FileOperation.SaveData(MchFilePath, "Machine", "SoftwareImageToLaserScaleX", Para.SWImgToSamlightXScale.ToString("F3"));
            FileOperation.SaveData(MchFilePath, "Machine", "SoftwareImageToLaserScaleY", Para.SWImgToSamlightYScale.ToString("F3"));

            FileOperation.SaveData(MchFilePath, "Machine", "SoftwareImageToLaserScaleXS", Para.SWImgToSamlightXSScale.ToString("F3"));
            FileOperation.SaveData(MchFilePath, "Machine", "SoftwareImageToLaserScaleYS", Para.SWImgToSamlightYSScale.ToString("F3"));

            FileOperation.SaveData(MchFilePath, "Machine", "SoftwareImageToLaserScaleXL", Para.SWImgToSamlightXLScale.ToString("F3"));
            FileOperation.SaveData(MchFilePath, "Machine", "SoftwareImageToLaserScaleYL", Para.SWImgToSamlightYLScale.ToString("F3"));

            FileOperation.SaveData(MchFilePath, "Machine", "SamlightOnline", Para.SamlightOnline.ToString());
            FileOperation.SaveData(MchFilePath, "Machine", "SamlightGreyScale", Para.SamlightGreyScale.ToString());

            FileOperation.SaveData(MchFilePath, "Machine", "AutoStartSamlight", Para.AutoStartSamlightSW.ToString());

            FileOperation.SaveData(MchFilePath, "Machine", "AutoConnectWebCam", Para.WebCamStartAppStart.ToString());

            FileOperation.SaveData(MchFilePath, "Machine", "ShowMarkingProcess", Para.ShowMarkingProcess.ToString());

            FileOperation.SaveData(MchFilePath, "Machine", "SMDitherStepHigh", Para.SMDitherStepHigh.ToString());
            FileOperation.SaveData(MchFilePath, "Machine", "SMDitherStepLow", Para.SMDitherStepLow.ToString());
            FileOperation.SaveData(MchFilePath, "Machine", "SMDitherStepStandard", Para.SMDitherStepStandard.ToString());

            FileOperation.SaveData(MchFilePath, "Machine", "SMBitmapInverted", Para.SMBitmapInverted.ToString());

            FileOperation.SaveData(MchFilePath, "Machine", "SMCenterToActualCenterX", Para.SMCenterToActualCenterX.ToString("F3"));
            FileOperation.SaveData(MchFilePath, "Machine", "SMCenterToActualCenterY", Para.SMCenterToActualCenterY.ToString("F3"));
                        
            FileOperation.SaveData(MchFilePath, "Machine", "DefaultWebCamIndex", Para.DefaultWebCamIndex.ToString());

            FileOperation.SaveData(MchFilePath, "Marking", "PhoneSetPower", Para.PhoneSetLaserPower.ToString("F1"));
            FileOperation.SaveData(MchFilePath, "Marking", "PhoneSetFreq", Para.PhoneSetFreq.ToString("F1"));
            FileOperation.SaveData(MchFilePath, "Marking", "PhoneSetSpeed", Para.PhoneSetSpeed.ToString("F1"));
            FileOperation.SaveData(MchFilePath, "Marking", "CardSetPower", Para.CardSetLaserPower.ToString("F1"));
            FileOperation.SaveData(MchFilePath, "Marking", "CardSetFreq", Para.CardSetFreq.ToString("F1"));
            FileOperation.SaveData(MchFilePath, "Marking", "CardSetSpeed", Para.CardSetSpeed.ToString("F1"));

            FileOperation.SaveData(MchFilePath, "Machine", "SamlightAppPath", Para.SamlightAppPath);

            FileOperation.SaveData(MchFilePath, "Language", "Language", MultiLanguage.AppLanguage.ToString());
            FileOperation.SaveData(MchFilePath, "Color", "Color", Para.appColor.ToString());
            
        }
        private void Window_ManipulationStarting_1(object sender, ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = this;
            e.Handled = true;
        }

        private void Window_ManipulationDelta_1(object sender, ManipulationDeltaEventArgs e)
        {
            if (e.OriginalSource is Rectangle)
            {
                Rectangle rectToMove;
                //
                rectToMove = e.OriginalSource as Rectangle;
                //else
                //    rectToMove = ((MatrixTransform)(e.OriginalSource as TextBlock).RenderTransform).Matrix;

                Matrix rectsMatrix = ((MatrixTransform)rectToMove.RenderTransform).Matrix;

                rectsMatrix.RotateAt(e.DeltaManipulation.Rotation, e.ManipulationOrigin.X, e.ManipulationOrigin.Y);

                rectsMatrix.ScaleAt(e.DeltaManipulation.Scale.X, e.DeltaManipulation.Scale.X,
                   e.ManipulationOrigin.X, e.ManipulationOrigin.Y);

                rectsMatrix.Translate(e.DeltaManipulation.Translation.X,
                   e.DeltaManipulation.Translation.Y);

                rectToMove.RenderTransform = new MatrixTransform(rectsMatrix);
                Rect containingRect = new Rect(((FrameworkElement)e.ManipulationContainer).RenderSize);

                Rect shapeBounds = rectToMove.RenderTransform.TransformBounds(new Rect(rectToMove.RenderSize));

                if (e.IsInertial && !containingRect.Contains(shapeBounds))
                {
                    e.Complete();
                }

                e.Handled = true;
            }
            if (e.OriginalSource is Border)
            {
                var element = e.Source as FrameworkElement;
                var transformation = element.RenderTransform as MatrixTransform;
                var matrix = transformation == null ? Matrix.Identity : transformation.Matrix;

                matrix.RotateAt(e.DeltaManipulation.Rotation, e.ManipulationOrigin.X, e.ManipulationOrigin.Y);

                matrix.ScaleAt(e.DeltaManipulation.Scale.X, e.DeltaManipulation.Scale.X,
                   e.ManipulationOrigin.X, e.ManipulationOrigin.Y);

                matrix.Translate(e.DeltaManipulation.Translation.X,
                   e.DeltaManipulation.Translation.Y);

                element.RenderTransform = new MatrixTransform(matrix);
                e.Handled = true;
            }
        }

        private void Window_ManipulationInertiaStarting_1(object sender, ManipulationInertiaStartingEventArgs e)
        {
            e.TranslationBehavior.DesiredDeceleration = 10.0 * 96.0 / (1000.0 * 1000.0);
            e.ExpansionBehavior.DesiredDeceleration = 0.1 * 96 / (1000.0 * 1000.0);
            e.RotationBehavior.DesiredDeceleration = 720 / (1000.0 * 1000.0);
            e.Handled = true;
        }

        private void SettingImgBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MenuPnl.Children.Contains(mySet))
                return;
            MenuPnl.Children.Remove(myPaintCtrl);
            MenuPnl.Children.Add(mySet);
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //SLMgr.Set_Output(5, false);
            myPaintCtrl.myProcess.DestroyForm();
            SLMgr.Set_Output(4, false);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SLMgr.SetLaserPower(67);
            SLMgr.SetFrequency(490000);
            SLMgr.SetMarkSpeed(1000);
        }

        private void PrintBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bool val = SLMgr.Get_Output(4);
            SLMgr.Set_Output(4, !val);
        }

        private void Window_Closed_1(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        public void UpdateLanguage()
        {
            myPaintCtrl.UpdateUILanguage();
            mySet.UpdateUILanguage();
        }

        public void UpdateUIColor()
        {
            switch (Para.appColor)
            {
                case ColorType.Blue: 
                        HeadImg.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/head.png"));
                        MenuPnl.Background = new ImageBrush(new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/background.png")));
                    break;
                case ColorType.Pink:
                    HeadImg.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Head_Pink.png"));
                    MenuPnl.Background = new ImageBrush(new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/BG_Pink.png")));
                    break;
                case ColorType.Green:
                    HeadImg.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Head_Green.png"));
                    MenuPnl.Background = new ImageBrush(new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/BG_Green.png")));
                    break;
                case ColorType.Orange:
                    HeadImg.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Head_Orange.png"));
                    MenuPnl.Background = new ImageBrush(new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/BG_Orange.png")));
                    break;
                case ColorType.Purple:
                    HeadImg.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/Head_Purple.png"));
                    MenuPnl.Background = new ImageBrush(new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/BG_Purple.png")));
                    break;
            }

            myPaintCtrl.UpdateUIColor();
            
        }
    }
}
