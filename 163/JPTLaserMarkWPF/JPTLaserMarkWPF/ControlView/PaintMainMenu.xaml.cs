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
    /// Interaction logic for PaintMainMenu.xaml
    /// </summary>
    public partial class PaintMainMenu : UserControl
    {
        public PaintMainMenu()
        {
            InitializeComponent();
        }

        public Image GetMenuControl(int Idx) //Idx 0 =photo, 1= hand, 2= type
        {
            switch (Idx)
            {
                case 0: return PhotoImgBtn;
                case 1: return HandImgBtn;
                case 2: return TypeImgBtn;               
            }
            return null;
        }

        public void UpdateUILanguage()
        {
            TypeLbl.Content = MultiLanguage.GetTypeLblString();
            HandLbl.Content = MultiLanguage.GetHandLblString();
            PhotoLbl.Content = MultiLanguage.GetPhotoLblString();
        }
        
    }
}
