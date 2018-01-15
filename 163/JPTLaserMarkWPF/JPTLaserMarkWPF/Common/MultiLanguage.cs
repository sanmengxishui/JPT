using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public enum LanguageType
    {
        English=0,
        Chinese =1,
    }

    public class MultiLanguage
    {
        public static LanguageType AppLanguage = LanguageType.English;

        //Settings Menu
        public static string GetSamlightGreyScaleCBString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Samlight Bitmap GreyScale";
                case LanguageType.Chinese: return "Samlight位图灰度";
            }
            return "Samlight Hardware Connected";
        }
        public static string GetSamLightOnlineCBString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Samlight Hardware Connected";
                case LanguageType.Chinese: return "Samlight硬件连接";
            }
            return "Samlight Hardware Connected";
        }
        public static string GetSamLighInvertCBString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Samlight Bitmap Inverted ( Black/White)";
                case LanguageType.Chinese: return "Samlight位图反转 (黑/白)";
            }
            return "Samlight Bitmap Inverted ( Black/White)";
        }
        public static string GetLanguageLblString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Language";
                case LanguageType.Chinese: return "语言";
            }
            return "Language";
        }
        public static string GetSLXScaleLblString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Image To Samlight Scale X (%) (Default)";
                case LanguageType.Chinese: return "图像到Samlight的比例X(%) (默认)";
            }
            return "Image To Samlight Scale X (%) (Default)";
        }
        public static string GetSLYScaleLblString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Image To Samlight Scale Y (%) (Default)";
                case LanguageType.Chinese: return "图像到Samlight的比例Y(%) (默认)";
            }
            return "Image To Samlight Scale Y (%) (Default)";
        }
        public static string GetSLDetherLblString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Samlight Dether Steps mm/pixel (Standard)";
                case LanguageType.Chinese: return "Samlight精细度mm/pixel (标准)";
            }
            return "Samlight Dether Steps mm/pixel (Standard)";
        }
        public static string GetSLoffsetXLblString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Samlight Center to Actual Center Offset X";
                case LanguageType.Chinese: return "Samlight中心到实际中心偏移X";
            }
            return "Samlight Center to Actual Center Offset X";
        }
        public static string GetSLoffsetYLblString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Samlight Center to Actual Center Offset Y";
                case LanguageType.Chinese: return "Samlight中心到实际中心偏移Y";
            }
            return "Samlight Center to Actual Center Offset Y";
        }
        public static string GetLaserPwLblString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Marking Setting Laser Power (Watt)";
                case LanguageType.Chinese: return "设置打标激光功率(Watt)";
            }
            return "Marking Setting Laser Power (Watt)";
        }
        public static string GetLaserSpeedLblString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Marking Setting Speed (mm/s)";
                case LanguageType.Chinese: return "设置打标速度 (mm/s)";
            }
            return "Marking Setting Speed (mm/s)";
        }
        public static string GetLaserFreqLblString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Marking Setting Frequency (kHz)";
                case LanguageType.Chinese: return "设置打标频率(kHz)";
            }
            return "Marking Setting Frequency (kHz)";
        }
        public static string GetColorLblString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Color";
                case LanguageType.Chinese: return "颜色";
            }
            return "Color";
        }

        //Paint Control
        public static string GetAlignBtnString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Align";
                case LanguageType.Chinese: return "对齐"; 
            }
            return "Align";
        }
        public static string GetMenuLblString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "MENU";
                case LanguageType.Chinese: return "目录";
            }
            return "MENU";
        }
        public static string GetBackBtnString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Back";
                case LanguageType.Chinese: return "返回";
            }
            return "Back";
        }

        //Paint Main Menu
        public static string GetTypeLblString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Typing";
                case LanguageType.Chinese: return "键盘输入";
            }
            return "Typing";
        }
        public static string GetHandLblString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "HandWriting";
                case LanguageType.Chinese: return "手写输入";
            }
            return "HandWriting";
        }
        public static string GetPhotoLblString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Pictures";
                case LanguageType.Chinese: return "打开图片";
            }
            return "Pictures";
        }

        //Paint Photo Menu
        public static string GetImageLblString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Gallery";
                case LanguageType.Chinese: return "图片";
            }
            return "Gallery";
        }

        //Hand Menu
        public static string GetBrushSizeLblString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Pen Size";
                case LanguageType.Chinese: return "画笔尺寸";
            }
            return "Pen Size";
        }
        public static string GetIntensityLblString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Intensity";
                case LanguageType.Chinese: return "透明度";
            }
            return "Intensity";
        }

        //Type Menu
        public static string GetTextInputLblString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Enter Text Below";
                case LanguageType.Chinese: return "输入文字";
            }
            return "Enter Text Below";
        }

        //Message Dialog Btn
        public static string GetOKBtnString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "OK";
                case LanguageType.Chinese: return "确认";
            }
            return "OK";
        }
        public static string GetCancelBtnString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Cancel";
                case LanguageType.Chinese: return "取消";
            }
            return "Cancel";
        }

        //Message String
        public static string GeStartMarkString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Marking";
                case LanguageType.Chinese: return "打标";
            }
            return "Marking";
        }
        public static string GetMarkingTitleString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Start Marking?";
                case LanguageType.Chinese: return "是否开始打标?";
            }
            return "Start Marking?";
        }

        //Database
        public static string GeErrorConnectionString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Cannot connect to server.  Contact administrator";
                case LanguageType.Chinese: return "不能连接服务器。请联络管理员";
            }
            return "Cannot connect to server.  Contact administrator";
        }
        public static string GeErrorLoginString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Invalid username/password, please try again";
                case LanguageType.Chinese: return "无效用户名/密码";
            }
            return "Invalid username/password, please try again";
        }

        //Samlight
        public static string GeSamlightNotFoundString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "SAMLight not found.";
                case LanguageType.Chinese: return "SAMLight没运行";
            }
            return "SAMLight not found";
        }
        public static string GeSamlightWarningString()
        {
            switch (AppLanguage)
            {
                case LanguageType.English: return "Warning.";
                case LanguageType.Chinese: return "注意";
            }
            return "Warning.";
        }

    }
}
