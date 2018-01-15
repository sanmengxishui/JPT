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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JPTLaserMarkWPF.ControlView
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class SettingsMenu : UserControl
    {
        MainWindow hMyMain;
        public SettingsMenu(MainWindow myMain)
        {
            InitializeComponent();
            UpdateUI();
            hMyMain = myMain;

            Set1TP.Visibility = System.Windows.Visibility.Hidden;
            BoolTP.Visibility = System.Windows.Visibility.Hidden;
            Set2TP.Visibility = System.Windows.Visibility.Hidden;
        }

        public Image GetSaveBtnControl()
        {
            return SaveImgBtn;
        }

        private void UpdateUI()
        {
            ImgToSLScaleXEB.Text = Para.SWImgToSamlightXScale.ToString("F3");
            ImgToSLScaleYEB.Text = Para.SWImgToSamlightYScale.ToString("F3");
            ImgToSLScaleXSEB.Text = Para.SWImgToSamlightXSScale.ToString("F3");
            ImgToSLScaleYSEB.Text = Para.SWImgToSamlightYSScale.ToString("F3");
            ImgToSLScaleXLEB.Text = Para.SWImgToSamlightXLScale.ToString("F3");
            ImgToSLScaleYLEB.Text = Para.SWImgToSamlightYLScale.ToString("F3");
            SLDetherStepHEB.Text = Para.SMDitherStepHigh.ToString("F3");
            SLDetherStepLEB.Text = Para.SMDitherStepLow.ToString("F3");
            SLDetherStepSEB.Text = Para.SMDitherStepStandard.ToString("F3");
            SLCtrToActCtrXEB.Text = Para.SMCenterToActualCenterX.ToString("F3");
            SLCtrToActCtrYEB.Text = Para.SMCenterToActualCenterY.ToString("F3");
            SamLOnlineCB.IsChecked = Para.SamlightOnline;
            SamLGSCB.IsChecked = Para.SamlightGreyScale;
            SMBitInvertedCB.IsChecked = Para.SMBitmapInverted;
            ByPassDoorSensorCB.IsChecked = Para.ByPassDoorSen;
            ByPassSampleSensorCB.IsChecked = Para.ByPassPhoneSen;

            PhoneSetPowerEB.Text = Para.PhoneSetLaserPower.ToString("F1");
            PhoneSetSpeedEB.Text = Para.PhoneSetSpeed.ToString("F1");
            PhoneSetFreqEB.Text = (Para.PhoneSetFreq/1000).ToString("F1");
            CardSetPowerEB.Text = Para.CardSetLaserPower.ToString("F1");
            CardSetSpeedEB.Text = Para.CardSetSpeed.ToString("F1");
            CardSetFreqEB.Text = Para.CardSetFreq.ToString("F1");

            LangCB.SelectedIndex = (int)MultiLanguage.AppLanguage;
            ColorCB.SelectedIndex = (int)Para.appColor;

        }
        public void ApplyAllSettings()
        {
            Para.SWImgToSamlightXScale = float.Parse(ImgToSLScaleXEB.Text);
            Para.SWImgToSamlightYScale = float.Parse(ImgToSLScaleYEB.Text);
            Para.SWImgToSamlightXSScale = float.Parse(ImgToSLScaleXSEB.Text);
            Para.SWImgToSamlightYSScale = float.Parse(ImgToSLScaleYSEB.Text);
            Para.SWImgToSamlightXLScale = float.Parse(ImgToSLScaleXLEB.Text);
            Para.SWImgToSamlightYLScale = float.Parse(ImgToSLScaleYLEB.Text);
            Para.SMDitherStepHigh = float.Parse(SLDetherStepHEB.Text);
            Para.SMDitherStepLow = float.Parse(SLDetherStepLEB.Text);
            Para.SMDitherStepStandard = float.Parse(SLDetherStepSEB.Text);
            Para.SamlightOnline = (bool)SamLOnlineCB.IsChecked;
            Para.SamlightGreyScale = (bool)SamLGSCB.IsChecked;
            Para.SMBitmapInverted = (bool)SMBitInvertedCB.IsChecked;
            Para.SMCenterToActualCenterX = float.Parse(SLCtrToActCtrXEB.Text);
            Para.SMCenterToActualCenterY = float.Parse(SLCtrToActCtrYEB.Text);
            Para.PhoneSetLaserPower = double.Parse(PhoneSetPowerEB.Text);
            Para.PhoneSetSpeed = double.Parse(PhoneSetSpeedEB.Text);
            Para.PhoneSetFreq = double.Parse(PhoneSetFreqEB.Text)*1000;
            Para.CardSetLaserPower = double.Parse(CardSetPowerEB.Text);
            Para.CardSetSpeed = double.Parse(CardSetSpeedEB.Text);
            Para.CardSetFreq = double.Parse(CardSetFreqEB.Text);

            Para.ByPassPhoneSen = (bool)ByPassSampleSensorCB.IsChecked;
            Para.ByPassDoorSen = (bool)ByPassDoorSensorCB.IsChecked;
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (SetMenuPC.SelectedIndex == 0)
                return;
            SetMenuPC.SelectedIndex = SetMenuPC.SelectedIndex - 1;
            UpdatePageLbl();
        }

        private void Image_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            if (SetMenuPC.SelectedIndex == SetMenuPC.Items.Count-1)
                return;
            SetMenuPC.SelectedIndex = SetMenuPC.SelectedIndex + 1;
            UpdatePageLbl();
        }

        private void UpdatePageLbl()
        {
            PageLbl.Content = "Page "+ (SetMenuPC.SelectedIndex + 1).ToString() + " / " + SetMenuPC.Items.Count.ToString();
        }

        private void SLDetherStepSEB_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LangCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (hMyMain == null)
                return;
            MultiLanguage.AppLanguage = (LanguageType)LangCB.SelectedIndex;
            hMyMain.UpdateLanguage();
        }

        public void UpdateUILanguage()
        {
            SamLOnlineCB.Content = MultiLanguage.GetSamLightOnlineCBString();
            SamLGSCB.Content = MultiLanguage.GetSamlightGreyScaleCBString();

            SMBitInvertedCB.Content = MultiLanguage.GetSamLighInvertCBString();
            LangaugeLB.Content = MultiLanguage.GetLanguageLblString();
            SamLightXScaleLbl.Content = MultiLanguage.GetSLXScaleLblString();
            SamLightYScaleLbl.Content = MultiLanguage.GetSLYScaleLblString();
            SLDetherStepLbl.Content = MultiLanguage.GetSLDetherLblString();
            SLOffsetXLbl.Content = MultiLanguage.GetSLoffsetXLblString();
            SLOffsetYLbl.Content = MultiLanguage.GetSLoffsetYLblString();
            PhonePwLbl.Content = MultiLanguage.GetLaserPwLblString();
            PhoneSpeedLbl.Content = MultiLanguage.GetLaserSpeedLblString();
            PhoneFreqLbl.Content = MultiLanguage.GetLaserFreqLblString();
        }

        private void ColorCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (hMyMain == null)
                return;
            Para.appColor = (ColorType)ColorCB.SelectedIndex;
            hMyMain.UpdateUIColor();
            
        }
    }
}
