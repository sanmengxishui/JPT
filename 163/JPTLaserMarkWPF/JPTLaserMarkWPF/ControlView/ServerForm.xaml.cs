using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using DatabaseServer;
using System.Windows.Threading;
using Common;
using JPTLaserMarkWPF.PaintObject;

namespace JPTLaserMarkWPF.ControlView
{
    /// <summary>
    /// Interaction logic for ServerForm.xaml
    /// </summary>
    /// 
    /// 
    

    public partial class ServerForm : Window
    {
        public ObservableCollection<ImageSource> ImageList
        {
            get
            {
                var results = new ObservableCollection<ImageSource>();
                foreach (var card in ImgList)
                {
                    results.Add(card);
                }
                return results;
            }
        }

        MySQLDatabase mydataBase = new MySQLDatabase();
        List<MyData> myImageData = new List<MyData>();
        List<ImageSource> ImgList = new List<ImageSource>();
        private DispatcherTimer _timer = new DispatcherTimer(); // on enter frame simulator
        public int SelectedSvrImg = -1;

        void _timer_Tick(object sender, EventArgs e)
        {
            if (ProBar.Value == 100)
                ProBar.Value = 0;

            ProBar.Value = ProBar.Value + 10;           
        }

        public void StartLoading()
        {
            // start the enter frame event
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Start();
        }

        public void StopLoading()
        {
            // start the enter frame event            
            _timer.Stop();
            ProBar.Value = 100;
        }

        private PaintObj myPaint;
        public ServerForm(PaintObj ParPaint)
        {
            InitializeComponent();
            myPaint = ParPaint;
            //mydataBase.OpenConnection();            
        }

        private void ImgGalleryCtr_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
            {
                // Do double-click code  
                //ImageSource myImg 

                SelectedSvrImg = ImgGalleryCtrl.GetSelectedImageIdx();
                myPaint.ImportImageGreyScale(ImgList[SelectedSvrImg]);

                if (Para.UpdateDatabase)
                {
                    mydataBase.Update(myImageData[SelectedSvrImg].id);
                }
                this.Close();
                e.Handled = true;
            }
        }   
        private void LoadServerList()
        {
            StartLoading();

            myImageData = mydataBase.Select();

            ImgList.Clear();
            for (int i = 0; i < myImageData.Count; i++)
                ImgList.Add(Helper.BitmapToImageSource(myImageData[i].myImg));

            StopLoading();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            LoadServerList();
            ImgGalleryCtrl.ImageFileDirectory = "";// System.AppDomain.CurrentDomain.BaseDirectory + "Gallery";
            ImgGalleryCtrl.ImgList = ImgList;
            ImgGalleryCtrl.LayoutRoot.MouseLeftButtonDown += ImgGalleryCtr_MouseLeftButtonDown;
        }
    }
}
