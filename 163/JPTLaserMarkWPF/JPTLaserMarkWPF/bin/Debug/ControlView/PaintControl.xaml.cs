using Common;
using JPTLaserMarkWPF.PaintObject;
using SamLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Windows.Threading;

namespace JPTLaserMarkWPF.ControlView
{
    /// <summary>
    /// Interaction logic for PaintControl.xaml
    /// </summary>
    public partial class PaintControl : UserControl
    {
        SamLightClass SLMgr;
        PaintMainMenu myMainMenu = new PaintMainMenu();
        PaintPhotoMenu myPaintMenu;
        PaintHandMenu myHandMenu;
        PaintTypeMenu myTypeMenu;
        WebCamControl myWebCamMenu;
        //2017/7/10
        public Processing myProcess;
        private readonly BackgroundWorker SLImportWorker = new BackgroundWorker();
        private readonly BackgroundWorker SLMarkingWorker = new BackgroundWorker();
        //private readonly BackgroundWorker ProcessWorker = new BackgroundWorker();

        PaintObj myPaint;
        public PaintControl(SamLightClass mySL)
        {
            InitializeComponent();
            SLMgr = mySL;
            myPaint = new PaintObj(inkC, MainImg);
            myPaintMenu = new PaintPhotoMenu(myPaint);
            myHandMenu = new PaintHandMenu(myPaint);
            myTypeMenu = new PaintTypeMenu(myPaint);
            myWebCamMenu = new WebCamControl();
            //2017/7/10
            
            myProcess = new Processing();
            PaintMenuPnl.Children.Add(myMainMenu);
            AssignMenuEvent();
            StartJPTWebCam();
            MinimizedWEbCam();
            BackImgPnl.Visibility = System.Windows.Visibility.Hidden;
            AlignImgPnl.Visibility = System.Windows.Visibility.Hidden;
        }

        private void PaintCtrl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void AssignMenuEvent()
        {
            //Photo
            Image tmp = myMainMenu.GetMenuControl(0);
            tmp.MouseDown += MainMenuPhoto_MouseClick;
            //Hand
            tmp = myMainMenu.GetMenuControl(1);
            tmp.MouseDown += MainMenuHand_MouseClick;
            //Type
            tmp = myMainMenu.GetMenuControl(2);
            tmp.MouseDown += MainMenuType_MouseClick;

            //Photo Menu
            tmp = myPaintMenu.GetOKBtnControl();
            tmp.MouseDown += OKSubMenuBtn_MouseClick;

            //Hand Menu
            tmp = myHandMenu.GetOKBtnControl();
            tmp.MouseDown += OKSubMenuBtn_MouseClick;

            //Type Menu
            tmp = myTypeMenu.GetOKBtnControl();
            tmp.MouseDown += OKSubMenuBtn_MouseClick;

            //WebCam Menu
            tmp = myWebCamMenu.GetPrintBtnControl();
            tmp.MouseDown += WCPrintBtn_MouseClick;
            //2017/7/7
            tmp = myWebCamMenu.GetLightControl();
            tmp.MouseDown += WCPlight_MouseClick;

            tmp = myWebCamMenu.GetPrintCardBtnControl();
            tmp.MouseDown += WCPrintCardBtn_MouseClick;
            tmp = myWebCamMenu.GetOKBtnControl();
            tmp.MouseDown += OKSubMenuBtn_MouseClick;
            tmp = myWebCamMenu.GetArrowBtn(0);
            tmp.MouseDown += WCUpBtn_MouseClick;
            tmp = myWebCamMenu.GetArrowBtn(1);
            tmp.MouseDown += WCDownBtn_MouseClick;
            tmp = myWebCamMenu.GetArrowBtn(2);
            tmp.MouseDown += WCLeftBtn_MouseClick;
            tmp = myWebCamMenu.GetArrowBtn(3);
            tmp.MouseDown += WCRightBtn_MouseClick;
            tmp = myWebCamMenu.GetArrowBtn(4);
            tmp.MouseDown += WCSizeDownBtn_MouseClick;
            tmp = myWebCamMenu.GetArrowBtn(5);
            tmp.MouseDown += WCSizeUpBtn_MouseClick;

            //Thread
            //2017/7/11
            //ProcessWorker.DoWork += Process_Work;

            SLImportWorker.DoWork += SLImport_DoWork;
            SLImportWorker.RunWorkerCompleted += SLImport_RunWorkerCompleted;
            SLMarkingWorker.DoWork += SLMarking_DoWork;
            SLMarkingWorker.RunWorkerCompleted += SLMarking_RunWorkerCompleted;
        }
        float tmpDither = Para.SMDitherStepStandard;

        private void WCSizeDownBtn_MouseClick(object sender, MouseButtonEventArgs e)
        {
            double SizeX, SizeY;
            SLMgr.GetSizeXYSamlight("", out SizeX, out SizeY);
            if ((SizeX < Para.SWImgToSamlightXSScale) || (SizeY < Para.SWImgToSamlightYSScale))
                return;

            SLMgr.ScaleSize("", 0.9, 0.9);
            SLMgr.ShowRedPointer();
        }

        private void WCSizeUpBtn_MouseClick(object sender, MouseButtonEventArgs e)
        {
            double SizeX, SizeY;
            SLMgr.GetSizeXYSamlight("", out SizeX, out SizeY);
            if ((SizeX > Para.SWImgToSamlightXLScale) || (SizeY > Para.SWImgToSamlightYLScale))
                return;

            SLMgr.ScaleSize("", 1.1, 1.1);
            SLMgr.ShowRedPointer();
        }

        private void WCUpBtn_MouseClick(object sender, MouseButtonEventArgs e)
        {
            SLMgr.RelMoveTo("", 0, Para.SMDefaultMoveSteps);
            SLMgr.ShowRedPointer();
        }
        private void WCDownBtn_MouseClick(object sender, MouseButtonEventArgs e)
        {
            SLMgr.RelMoveTo("", 0, -Para.SMDefaultMoveSteps);
            SLMgr.ShowRedPointer();
        }
        private void WCLeftBtn_MouseClick(object sender, MouseButtonEventArgs e)
        {
            SLMgr.RelMoveTo("", -Para.SMDefaultMoveSteps, 0);
            SLMgr.ShowRedPointer();
        }
        private void WCRightBtn_MouseClick(object sender, MouseButtonEventArgs e)
        {
            SLMgr.RelMoveTo("", Para.SMDefaultMoveSteps, 0);
            SLMgr.ShowRedPointer();
        }
        private void SLImport_DoWork(object sender, DoWorkEventArgs e)
        {
            //Process Marking
            //ProcessWorker.RunWorkerAsync();

            inkC.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(delegate()
                {
                    inkC.Background = Brushes.White;
                }));

            myProcess.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
              new Action(delegate()
              {
                  myProcess.Show();
              }));
            Thread.Sleep(200);
            string path = System.IO.Path.GetTempPath();
            string fileName = path + "\\HandImage.jpg";
            //string fileName = @"D:\HandImage.bmp";
            inkC.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(delegate()
                {
                    //inkC.Background = Brushes.White;
                    myPaint.ExportToImage(fileName);
                    inkC.Background = Brushes.Transparent;
                }));

            SLMgr.NewJob();
            //Thread.Sleep(2000);
            SLMgr.ImportImage(fileName, tmpDither);
            //inkC.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            //    new Action(delegate()
            //    {
            //        inkC.Background = Brushes.Transparent;
            //    }));
        }

        

        private void SLImport_RunWorkerCompleted(object sender,
                                               RunWorkerCompletedEventArgs e)
        {
            //2017/7/10
            myProcess.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
        new Action(delegate()
        {
            myProcess.Close();
        }));
            //loadFrm.Close();
            MinimizeJPTApp();
            PaintMenuPnl.Children.Clear();
            PaintMenuPnl.Children.Add(myWebCamMenu);
            //myWebCamMenu.StartWebCam();
            ShowWebCamWin();
            if (Para.ShowRedPointer)
            {
                SLMgr.ShowRedPointer();
            }
            BackImgPnl.Visibility = System.Windows.Visibility.Visible;
        }
        private void SLMarking_DoWork(object sender, DoWorkEventArgs e)
        {
            Print();
        }
        private void SLMarking_RunWorkerCompleted(object sender,
                                               RunWorkerCompletedEventArgs e)
        {
            OKSubMenuBtn_MouseClick(sender, null);
        }
        private void WCPlight_MouseClick(object sender, MouseButtonEventArgs e)
        {
            bool val = SLMgr.Get_Output(4);
            SLMgr.Set_Output(4, !val);

        }

        private void WCPrintBtn_MouseClick(object sender, MouseButtonEventArgs e)
        {
            //if (!Para.ByPassDoorSen)
            //{
            //    if (!SLMgr.Get_Input(6))
            //    {
            //        MessageDlg myDlg = new MessageDlg("Warning", "Please Close Door Before Marking.", true);
            //        myDlg.ShowDialog();
            //        //MessageBox.Show("Please Place Phone Into The Marking Area.", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
            //        //MessageBox.Show("Please Place Phone Into The Marking Area");
            //        return;
            //    }
            //}
            string msgTitle = MultiLanguage.GetMarkingTitleString();
            string msgStr = MultiLanguage.GeStartMarkString();
            MessageDlg myConfirmDlg = new MessageDlg(msgTitle, msgStr, false);
            myConfirmDlg.ShowDialog();

            if (myConfirmDlg.Res == -1)
                return;

            if (Para.ShowRedPointer)
            {
                SLMgr.HideRedPointer();
            }

            //Mark Phone
            SLMgr.SetLaserPower(Para.PhoneSetLaserPower);
            SLMgr.SetFrequency(Para.PhoneSetFreq);
            SLMgr.SetMarkSpeed(Para.PhoneSetSpeed);

            SLMarkingWorker.RunWorkerAsync();
        }

        private void WCPrintCardBtn_MouseClick(object sender, MouseButtonEventArgs e)
        {
            //if (!Para.ByPassDoorSen)
            //{
            //    if (!SLMgr.Get_Input(6))
            //    {
            //        MessageDlg myDlg = new MessageDlg("Warning", "Please Close Door Before Marking.", true);
            //        myDlg.ShowDialog();
            //        //MessageBox.Show("Please Place Phone Into The Marking Area.", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
            //        //MessageBox.Show("Please Place Phone Into The Marking Area");
            //        return;
            //    }
            //}

            string msgTitle = MultiLanguage.GetMarkingTitleString();
            string msgStr = MultiLanguage.GeStartMarkString();
            MessageDlg myConfirmDlg = new MessageDlg(msgTitle, msgStr, false);
            //MessageDlg myConfirmDlg = new MessageDlg("Marking", "Start Marking?", false);
            myConfirmDlg.ShowDialog();

            if (Para.ShowRedPointer)
            {
                SLMgr.HideRedPointer();
            }

            if (myConfirmDlg.Res == -1)
                return;

            //Card Phone
            SLMgr.SetLaserPower(Para.CardSetLaserPower);
            SLMgr.SetFrequency(Para.CardSetFreq);
            SLMgr.SetMarkSpeed(Para.CardSetSpeed);

            SLMarkingWorker.RunWorkerAsync();
        }
        private void OKSubMenuBtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (Para.ShowRedPointer)
            {
                SLMgr.HideRedPointer();
            }
            myWebCamMenu.StopWebCam();
            PaintMenuPnl.Children.Clear();
            PaintMenuPnl.Children.Add(myMainMenu);
            myPaint.SetPenEnabled(false);
            myPaint.SetManipulation(true);
            MinimizedWEbCam();
        }
        private void MainMenuPhoto_MouseClick(object sender, MouseEventArgs e)
        {
            PaintMenuPnl.Children.Remove(myMainMenu);
            PaintMenuPnl.Children.Add(myPaintMenu);
            myPaint.bMoveImage = true;
            BackImgPnl.Visibility = System.Windows.Visibility.Visible;
            AlignImgPnl.Visibility = System.Windows.Visibility.Visible;
        }
        private void MainMenuHand_MouseClick(object sender, MouseEventArgs e)
        {
            PaintMenuPnl.Children.Remove(myMainMenu);
            PaintMenuPnl.Children.Add(myHandMenu);
            myPaint.SetPenEnabled(true);
            BackImgPnl.Visibility = System.Windows.Visibility.Visible;
            myHandMenu.IntensitySlider.Value = 128;

        }
        private Process _touchKyBrd = null;
        private void MainMenuType_MouseClick(object sender, MouseEventArgs e)
        {
            string strKeybrd = @"C:\Program Files\Common Files\microsoft shared\ink\TabTip.exe";
            _touchKyBrd = Process.Start(strKeybrd);
            //System.Diagnostics.Process.Start(strKeybrd);
            //System.Diagnostics.Process.Start("osk.exe");
            myPaint.ImportText("New", new FontFamily("Microsoft YaHei UI"));
            //new FontFamily(FontTypeCB.SelectedItem.ToString());

            PaintMenuPnl.Children.Remove(myMainMenu);
            PaintMenuPnl.Children.Add(myTypeMenu);
            BackImgPnl.Visibility = System.Windows.Visibility.Visible;
            AlignImgPnl.Visibility = System.Windows.Visibility.Visible;
            myTypeMenu.SetFocus();
        }

        private void Print()
        {
            if (Para.SamlightOnline)
                SLMgr.MarkAllSamlight();
            else
                Thread.Sleep(5000);
        }

        public void PrintImgFunction()
        {
            if (PaintMenuPnl.Children.Contains(myWebCamMenu))
                return;

            if (!SLMgr.TestConnectSamlight())
                return;

            //if (!Para.ByPassPhoneSen)
            //{
            //    if (!SLMgr.Get_Input(5))
            //    {
            //        MessageDlg myDlg = new MessageDlg("Warning", "Please Place Phone Into The Marking Area.", true);
            //        myDlg.ShowDialog();
            //        //MessageBox.Show("Please Place Phone Into The Marking Area.", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
            //        //MessageBox.Show("Please Place Phone Into The Marking Area");
            //        return;
            //    }
            //}
            MinimizeSamlightApplication();
            myPaint.SetManipulation(false);

            //ResSelectForm myRes = new ResSelectForm();
            //myRes.ShowDialog();
            //switch (myRes.ResIdx)
            //{
            //    case 1:
            //        {
            //            tmpDither = Para.SMDitherStepLow; break;
            //        }
            //    case 2: tmpDither = Para.SMDitherStepStandard; break;
            //    case 3:
            //        {
            //            tmpDither = Para.SMDitherStepHigh; break;
            //        }
            //}
            //StartJPTGameApp();

            tmpDither = Para.SMDitherStepStandard;
            //myProcess.Show();
            //ProcessWorker.RunWorkerAsync();
            SLImportWorker.RunWorkerAsync();

        }

        private void PrintImgBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        Process JPTGameHdl;
        private void StartJPTGameApp()
        {
            return;

            if (!Helper.IsProcessOpen("JPTGames"))
            {
                string JptGameDir = System.AppDomain.CurrentDomain.BaseDirectory + "JPTGames\\JPTGames.exe";
                JPTGameHdl = Process.Start(JptGameDir);
            }
            else
            {
                if (JPTGameHdl == null)
                {
                    JPTGameHdl = Helper.GetSystemProcess("JPTGames");
                }
                IntPtr hWnd = JPTGameHdl.MainWindowHandle;
                if (!hWnd.Equals(IntPtr.Zero))
                {
                    // SW_SHOWMAXIMIZED to maximize the window
                    // SW_SHOWMINIMIZED to minimize the window
                    // SW_SHOWNORMAL to make the window be normal size
                    ShowWindowAsync(hWnd, SW_SHOWNORMAL);
                }
            }
        }
        Process JPTWebCamHdl;
        private void StartJPTWebCam()
        {
            if (!Helper.IsProcessOpen("JPTWebCam"))
            {
                string JptGameDir = System.AppDomain.CurrentDomain.BaseDirectory + "JPTWebCam\\JPTWebCam.exe";
                JPTWebCamHdl = Process.Start(JptGameDir);
                MinimizedWEbCam();
            }
            else
            {
                if (JPTWebCamHdl == null)
                {
                    JPTWebCamHdl = Helper.GetSystemProcess("JPTWebCam");
                }
                IntPtr hWnd = JPTWebCamHdl.MainWindowHandle;
                if (!hWnd.Equals(IntPtr.Zero))
                {
                    // SW_SHOWMAXIMIZED to maximize the window
                    // SW_SHOWMINIMIZED to minimize the window
                    // SW_SHOWNORMAL to make the window be normal size
                    ShowWindowAsync(hWnd, SW_SHOWMINIMIZED);
                }
            }
        }
        private void MinimizedWEbCam()
        {
            IntPtr hWnd = JPTWebCamHdl.MainWindowHandle;
            if (!hWnd.Equals(IntPtr.Zero))
            {
                // SW_SHOWMAXIMIZED to maximize the window
                // SW_SHOWMINIMIZED to minimize the window
                // SW_SHOWNORMAL to make the window be normal size
                ShowWindowAsync(hWnd, SW_SHOWMINIMIZED);
            }
        }
        private void ShowWebCamWin()
        {
            IntPtr hWnd = JPTWebCamHdl.MainWindowHandle;
            if (!hWnd.Equals(IntPtr.Zero))
            {
                // SW_SHOWMAXIMIZED to maximize the window
                // SW_SHOWMINIMIZED to minimize the window
                // SW_SHOWNORMAL to make the window be normal size
                ShowWindowAsync(hWnd, SW_SHOWNORMAL);
            }
        }
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        private void MinimizeJPTApp()
        {
            return;
            IntPtr hWnd = JPTGameHdl.MainWindowHandle;
            if (!hWnd.Equals(IntPtr.Zero))
            {
                // SW_SHOWMAXIMIZED to maximize the window
                // SW_SHOWMINIMIZED to minimize the window
                // SW_SHOWNORMAL to make the window be normal size
                ShowWindowAsync(hWnd, SW_SHOWMINIMIZED);
            }

        }
        private void MinimizeSamlightApplication()
        {
            IntPtr hWnd = Helper.GetSystemProcess("sam_light").MainWindowHandle;
            if (!hWnd.Equals(IntPtr.Zero))
            {
                // SW_SHOWMAXIMIZED to maximize the window
                // SW_SHOWMINIMIZED to minimize the window
                // SW_SHOWNORMAL to make the window be normal size
                ShowWindowAsync(hWnd, SW_SHOWMINIMIZED);
            }

        }
        /// <summary>
        /// Manipulation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewJobBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            myPaint.NewPaint();
        }

        private void UndoBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (PaintMenuPnl.Children.Contains(myHandMenu))
                myPaint.UndoHand();
            else
                myPaint.Undo();
        }

        private void StackPanel_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (Para.ShowRedPointer)
            {
                SLMgr.HideRedPointer();
            }


            Process tmp = Helper.GetSystemProcess("TabTip");
            if (tmp != null)
                tmp.Kill();

            myWebCamMenu.StopWebCam();
            PaintMenuPnl.Children.Clear();
            PaintMenuPnl.Children.Add(myMainMenu);
            myPaint.SetPenEnabled(false);
            myPaint.SetManipulation(true);
            MinimizedWEbCam();

            BackImgPnl.Visibility = System.Windows.Visibility.Hidden;
            AlignImgPnl.Visibility = System.Windows.Visibility.Hidden;
        }

        private void AlignImgPnl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            myPaint.AlignLastSelectedItem();
        }

        private void PrintImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PrintImgFunction();
        }

        public void UpdateUILanguage()
        {
            AlignLbl.Text = MultiLanguage.GetAlignBtnString();
            MenuLbl.Content= MultiLanguage.GetMenuLblString();
            BackBtn.Text = MultiLanguage.GetBackBtnString();

            myMainMenu.UpdateUILanguage();
            myPaintMenu.UpdateUILanguage();
            myHandMenu.UpdateUILanguage();
            myTypeMenu.UpdateUILanguage();
        }

        public void UpdateUIColor()
        {
            BrushConverter bc = new BrushConverter();
            Brush ib;
            switch (Para.appColor)
            {
                case ColorType.Blue:
                    ib = (Brush)bc.ConvertFrom("#FF6CBEE2");
                    ib.Freeze();
                    MenuLbl.Background = ib;
                    MenuC.Background = new ImageBrush(new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/MenuImg.png")));
                    inkGrid.Background = new ImageBrush(new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/background.png")));
                    break;
                case ColorType.Pink:
                    ib = (Brush)bc.ConvertFrom("#FFF09196");
                    ib.Freeze();
                    MenuLbl.Background = ib;
                    MenuC.Background = new ImageBrush(new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/MenuImg_Pink.png")));
                    inkGrid.Background = new ImageBrush(new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/BG_Pink.png")));
                    break;
                case ColorType.Green:
                    ib = (Brush)bc.ConvertFrom("#FF6DC4B7");
                    ib.Freeze();
                    MenuLbl.Background = ib;
                    MenuC.Background = new ImageBrush(new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/MenuImg_Green.png")));
                    inkGrid.Background = new ImageBrush(new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/BG_Green.png")));
                    break;
                case ColorType.Orange:
                    ib = (Brush)bc.ConvertFrom("#FFF3B23E");
                    ib.Freeze();
                    MenuLbl.Background = ib;
                    MenuC.Background = new ImageBrush(new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/MenuImg_Orange.png")));
                    inkGrid.Background = new ImageBrush(new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/BG_Orange.png")));
                    break;
                case ColorType.Purple:
                    ib = (Brush)bc.ConvertFrom("#FF7865AB");
                    ib.Freeze();
                    MenuLbl.Background = ib;
                    MenuC.Background = new ImageBrush(new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/MenuImg_Purple.png")));
                    inkGrid.Background = new ImageBrush(new BitmapImage(new Uri("pack://siteoforigin:,,,/Resources/BG_Purple.png")));
                    break;
            }

            myHandMenu.UpdateUIColor();
        }

        private void Alig1Img_MouseDown(object sender, MouseButtonEventArgs e)
        {
            myPaint.AlignLastSelectedItem();
        }


    }
}
