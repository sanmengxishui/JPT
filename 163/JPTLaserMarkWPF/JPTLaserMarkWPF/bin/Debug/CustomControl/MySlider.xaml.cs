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

namespace JPTLaserMarkWPF.CustomControl
{
    /// <summary>
    /// Interaction logic for MySlider.xaml
    /// </summary>
    public partial class MySlider : UserControl
    {
        public MySlider()
        {
            InitializeComponent();
            //ButtonColor();
        }
        
        public int Minimum
        {
            get { return (int)mSlider.Minimum; }
            set
            {
                if ((value > 0) && (value < Maximum))
                {
                    mSlider.Minimum = value;                    
                }
            }
        }
        public int Maximum
        {
            get { return (int)mSlider.Maximum; }
            set
            {
                if (value > 0)
                {
                    mSlider.Maximum = value;
                }
            }
        }
        public int Value
        {
            get { return (int)mSlider.Value; }
            set
            {
                if (value > 0)
                {
                    mSlider.Value = value;
                }
            }
        }
        public string Text
        {
            get { return (string)SilderNameLbl.Content; }
            set
            {
                SilderNameLbl.Content = value;
            }
        } 
        public Slider GetSliderControl
        {
            get { return mSlider; }
        }
        public Image LeftImage
        {
            get { return LeftImg; }
        }
        public Image RightImage
        {
            get {return RightImg;}
        }

        public void ButtonColor()
        {
            
        }
    }
}
