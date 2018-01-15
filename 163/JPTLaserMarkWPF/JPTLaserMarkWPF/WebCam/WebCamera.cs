using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using Common;
using System.IO;
using System.Windows.Threading;
using System.Threading;

namespace JPTLaserMarkWPF.WebCam
{
    public class WebCamera
    {
        FilterInfoCollection videosources;
        VideoCaptureDevice VCD;
        BitmapImage myBitImg = new BitmapImage();
        Image myImgDisp;

        public WebCamera(Image myWCImg)
        {
            videosources = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            myImgDisp = myWCImg;
            //myImgDisp.Source = myBitImg;
        }
        public BitmapImage GetImageSource()
        {
            return myBitImg;
        }
        public List<String> GetAllWebCamConnected()
        {
            List<string> CamName = new List<string>();
            
            //Check if atleast one video source is available
            if (videosources != null)
            {
                for (int i = 0; i < videosources.Count; i++)
                    CamName.Add(videosources[i].Name);
            }

            return CamName;
        }
        void videoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            //Cast the frame as Bitmap object and don't forget to use ".Clone()" otherwise
            //you'll probably get access violation exceptions          
            //myBitImg = Helper.BitmapToImageSource((System.Drawing.Bitmap)eventArgs.Frame.Clone());
            //myImgDisp.Source = myBitImg;            
            try
            {
                System.Drawing.Image img = (System.Drawing.Bitmap)eventArgs.Frame.Clone();

                MemoryStream ms = new MemoryStream();
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = ms;
                bi.EndInit();

                bi.Freeze();
                myImgDisp.Dispatcher.BeginInvoke(new ThreadStart(delegate
                {
                    myImgDisp.Source = bi;
                }));
            }
            catch (Exception ex)
            {
            }

        }

        public void Start(int Idx)
        {            
            if (videosources == null)
                return;

            if (Idx >= videosources.Count)
                return;
            //For example use first video device. You may check if this is your webcam.
            VCD = new VideoCaptureDevice(videosources[Idx].MonikerString);

            try
            {
                //Check if the video device provides a list of supported resolutions
                if (VCD.VideoCapabilities.Length > 0)
                {
                    string highestSolution = "0;0";
                    //Search for the highest resolution
                    for (int i = 0; i < VCD.VideoCapabilities.Length; i++)
                    {
                        if (VCD.VideoCapabilities[i].FrameSize.Width > Convert.ToInt32(highestSolution.Split(';')[0]))
                            highestSolution = VCD.VideoCapabilities[i].FrameSize.Width.ToString() + ";" + i.ToString();
                    }
                    //Set the highest resolution as active
                    VCD.VideoResolution = VCD.VideoCapabilities[Convert.ToInt32(highestSolution.Split(';')[1])];
                }
            }
            catch { }

            //Create NewFrame event handler
            //(This one triggers every time a new frame/image is captured
            VCD.NewFrame += new AForge.Video.NewFrameEventHandler(videoSource_NewFrame);

            //Start recording
            VCD.Start();
        }

        public void Stop()
        {
            if (VCD == null)
                return;
            VCD.SignalToStop();
            VCD = null;
        }
    }
}
