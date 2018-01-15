using JPTLaserMarkWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public enum ColorType
    {
        Blue = 0,
        Pink = 1,
        Green = 2,
        Orange =3,
        Purple = 4,

    }

    public class DPoint
    {
        public double X = 0.0;
        public double Y = 0.0;
        public DPoint()
        { }

        public DPoint(double myX, double myY)
        {
            X = myX;
            Y = myY;
        }
    }
    public class WHPoint
    {
        public double X = 0.0;
        public double Y = 0.0;
        public double Theta = 0.0;
        public WHPoint()
        { }

        public WHPoint(double myX, double myY, double myT)
        {
            X = myX;
            Y = myY;
            Theta = myT;
        }
    }       
    public class Para
    {
        public static string MchName = "JPTLaserMarker";
        public static string MchAppName = "JPT Laser Marker";
        public static string defaultSettingsName = "JPT_Settings.xml";
        public static string mchSettingsFileName = "JPT_Machine_Settings.xml";
        public static string SWComment = "";
        public static string SWVersion = "JPT-A163 Version 17.07.21.18";
        public static string HWVersion = "Version 16.12.15.13";
        public static string CurLoadConfigFileName = "";
        public static string MchConfigFileName = "";
        public static string DefaultSettingsFileName = "";
        public static bool MachineOnline = true;
        public static bool WebCamStartAppStart = false;
        public static int DefaultWebCamIndex = 0;
        public static float WebCamToLaserHeadScale = 0.8f;
        public static float SWImgToSamlightXScale = 0.4f;
        public static float SWImgToSamlightYScale = 0.4f;
        public static float SWImgToSamlightXSScale = 0.4f;
        public static float SWImgToSamlightYSScale = 0.4f;
        public static float SWImgToSamlightXLScale = 0.4f;
        public static float SWImgToSamlightYLScale = 0.4f;
        public static float SMDitherStepHigh = 0.003f;
        public static float SMDitherStepLow = 0.3f;
        public static float SMDitherStepStandard = 0.03f;
        public static float SMCenterToActualCenterX = 0.0f;
        public static float SMCenterToActualCenterY = 0.0f;
        public static float SMDefaultMoveSteps = 0.5f;
        public static bool SMBitmapInverted = false;
        public static bool SamlightOnline = true;
        public static bool SamlightGreyScale = false;
        public static bool AutoStartSamlightSW = false;
        public static bool ShowMarkingProcess = true;
        public static bool ShowRedPointer = true;
        public static int MainMenuTop = 100;
        public static int SubMenuTop = 650;
        public static string Password = "JPT123";
        public static bool ByPassPhoneSen = false;
        public static bool ByPassDoorSen = false;
        public static bool UpdateDatabase = false; //Update DataBase to be Processed
        public static ColorType appColor = ColorType.Blue;

        public static double PhoneSetLaserPower = 67.0;
        public static double PhoneSetFreq = 490000;
        public static double PhoneSetSpeed = 1000;
        public static double CardSetLaserPower = 67.0;
        public static double CardSetFreq = 490000;
        public static double CardSetSpeed = 1000;

        public static string SamlightAppPath = @"D:\scaps\samlight\sam_light.exe";

        //IO
        public static int LaserInOperationIO = 1;
        public static int PhoneSensorIO = 8;
        public static int DoorSensorIO = 9;
    }
}
