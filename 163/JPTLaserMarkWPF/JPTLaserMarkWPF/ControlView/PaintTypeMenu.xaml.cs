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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JPTLaserMarkWPF.ControlView
{
    /// <summary>
    /// Interaction logic for PaintTypeMenu.xaml
    /// </summary>
    public partial class PaintTypeMenu : UserControl
    {
        PaintObj myPaint;
        public PaintTypeMenu(PaintObj ParPaint)
        {
            InitializeComponent();
            myPaint = ParPaint;
            InitUI();
        }
        private void InitUI()
        {            
            //FontTypeCB.ItemsSource = SystemFonts.Fa;
            foreach (FontFamily _f in Fonts.SystemFontFamilies)
            {
                LanguageSpecificStringDictionary _fontDic = _f.FamilyNames;
                if (_fontDic.ContainsKey(XmlLanguage.GetLanguage("zh-cn")))
                {
                    string _fontName = null;
                    if (_fontDic.TryGetValue(XmlLanguage.GetLanguage("zh-cn"), out _fontName))
                    {
                        FontTypeCB.Items.Add(_fontName);
                    }
                }
                else
                {
                    string _fontName = null;
                    if (_fontDic.TryGetValue(XmlLanguage.GetLanguage("en-us"), out _fontName))
                    {
                        FontTypeCB.Items.Add(_fontName);
                    }
                }
            }
            FontTypeCB.SelectionChanged += FontType_SelectionChanged;
            FontTypeCB.SelectedIndex = 0;
            //FontTypeCB.SelectedItem = FontTypeCB.Items[FontTypeCB.Items.IndexOf(TypeEB.FontFamily)];
        }
        private void FontType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TypeEB.FontFamily = new FontFamily(FontTypeCB.SelectedItem.ToString());
        }
        public Image GetOKBtnControl() //Idx 0 =photo, 1= hand, 2= type
        {
            return OKImgBtn;
        }

        private void TypeEB_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("osk.exe");
        }
        string GetString(RichTextBox rtb)
        {
            var textRange = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            return textRange.Text;
        }
        private void AddImgBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string tmpStr = GetString(TypeEB);
            myPaint.ImportText(tmpStr, TypeEB.FontFamily);
        }

        private void AlignImageBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            myPaint.AlignLastSelectedItem();
        }

        public void SetFocus()
        {
            TypeEB.Focus();
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            myPaint.UpdateText("Testing", TypeEB.FontFamily);
        }

        private void TypeEB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (myPaint != null)
                myPaint.UpdateText(GetString(TypeEB), TypeEB.FontFamily);
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            TypeEB.Document.Blocks.Clear();
            TypeEB.Focus();
        }

        public void UpdateUILanguage()
        {
            TextInputLbl.Content = MultiLanguage.GetTextInputLblString();
        }
    }
}
