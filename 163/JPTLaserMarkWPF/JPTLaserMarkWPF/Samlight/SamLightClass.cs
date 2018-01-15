using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAMLIGHT_CLIENT_CTRL_OCXLib;
using Common;

namespace SamLight
{
    public class SamLightClass
    {
        public static double CalibCCDtoSamKx = 0.0;
        public static double CalibCCDtoSamKy = 0.0;
        public static double SamBx = 0.0;
        public static double SamBy = 0.0;
        
        public AxSAMLIGHT_CLIENT_CTRL_OCXLib.AxScSamlightClientCtrl axScSamlightClientCtrl1;

     
        public SamLightClass(AxSAMLIGHT_CLIENT_CTRL_OCXLib.AxScSamlightClientCtrl myCtrl)
        {
            axScSamlightClientCtrl1 = myCtrl;
        }

       
        public int mark_entity(String entity_name_to_mark)
        {
            if (axScSamlightClientCtrl1.ScIsRunning() == 0)
            {
                MessageBox.Show(MultiLanguage.GeSamlightNotFoundString(), MultiLanguage.GeSamlightWarningString(), MessageBoxButtons.OK);
                return 0;
            }

            Set_Output(3, true);

            long mark_flags = 0x4;//0x0;
            axScSamlightClientCtrl1.ScSetMarkFlags((int)mark_flags);

            axScSamlightClientCtrl1.ScMarkEntityByName(entity_name_to_mark, 0);

            int i = 0;
            while (true)
            {
                i++;
                Application.DoEvents();
                if (i % 10 == 0)
                {
                    if (axScSamlightClientCtrl1.ScIsMarking() == 0)
                        break;
                }
            }
            axScSamlightClientCtrl1.ScStopMarking();

            Set_Output(3, false);
            return 1;
        }

        //public String TestConnectSamlight() //测试客户端连接Samlight服务器端
        //{
        //    if (axScSamlightClientCtrl1.ScIsRunning() == 0)
        //    {
        //        MessageBox.Show("SAMLight not found", "Warning", MessageBoxButtons.OK);
        //        return "SAMLight not found";
        //    }
        //    //弹出“hello from samlight"对话框说明连接成功
        //    long t=0;
        //    t= axScSamlightClientCtrl1.ScExecCommand(1);
        //    return "";
        //}
        public bool TestConnectSamlight() //测试客户端连接Samlight服务器端
        {
            if (axScSamlightClientCtrl1.ScIsRunning() == 0)
            {
                MessageBox.Show(MultiLanguage.GeSamlightNotFoundString(), MultiLanguage.GeSamlightWarningString(), MessageBoxButtons.OK);
                //MessageBox.Show("SAMLight not found", "Warning", MessageBoxButtons.OK);
                return false;
            }
            //弹出“hello from samlight"对话框说明连接成功           
            return true;
        }
        public void HideSamlightWindow()
        {
            return;
            axScSamlightClientCtrl1.ScShowApp(0);
        }
        //文件内所有实体均打标
        //public void MarkAllEntity()
        //{
        //    if (axScSamlightClientCtrl1.ScIsRunning() == 0)
        //    {
        //        MessageBox.Show("SAMLight not found", "Warning", MessageBoxButtons.OK);
        //        return;
        //    }

        //    mark_entity("");
        //}

        public void StopSamlight()  //停止打标
        {
            if (axScSamlightClientCtrl1.ScIsRunning() == 0)
            {
                MessageBox.Show(MultiLanguage.GeSamlightNotFoundString(), MultiLanguage.GeSamlightWarningString(), MessageBoxButtons.OK);
                //MessageBox.Show("SAMLight not found", "Warning", MessageBoxButtons.OK);
                return;
            }
            axScSamlightClientCtrl1.ScStopMarking();
        }

        public void MarkAllSamlight()//打标函数（打标所有的）
        {
            if (axScSamlightClientCtrl1.ScIsRunning() == 0)
            {
                MessageBox.Show(MultiLanguage.GeSamlightNotFoundString(), MultiLanguage.GeSamlightWarningString(), MessageBoxButtons.OK);
                //MessageBox.Show("SAMLight not found", "Warning", MessageBoxButtons.OK);
                return;
            }

            mark_entity("");
        }

        public bool MarkSamlight(string edtmarkName) //打标函数（打指定的）
        {
            if (axScSamlightClientCtrl1.ScIsRunning() == 0)
            {
                MessageBox.Show(MultiLanguage.GeSamlightNotFoundString(), MultiLanguage.GeSamlightWarningString(), MessageBoxButtons.OK);
                //MessageBox.Show("SAMLight not found", "Warning", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                mark_entity(edtmarkName);
                return true;
            }
            
        }


        public void LoadjobSamlight(string filename, int load_entities, int overwrite_entities, int load_materials)  //载入文件函数
        {
            if (axScSamlightClientCtrl1.ScIsRunning() == 0)
            {
                MessageBox.Show(MultiLanguage.GeSamlightNotFoundString(), MultiLanguage.GeSamlightWarningString(), MessageBoxButtons.OK);
                //MessageBox.Show("SAMLight not found", "Warning", MessageBoxButtons.OK);
                return;
            }
            axScSamlightClientCtrl1.ScLoadJob(filename, load_entities, overwrite_entities, load_materials);
        }

        public void GetCenterXYSamlight(string edtEntityName,out double center_x,out double center_y)  //获取某个实体的中心坐标X,y
        {
            center_x = 0;
            center_y = 0;
            if (axScSamlightClientCtrl1.ScIsRunning() == 0)
            {
                MessageBox.Show(MultiLanguage.GeSamlightNotFoundString(), MultiLanguage.GeSamlightWarningString(), MessageBoxButtons.OK);
                //MessageBox.Show("SAMLight not found", "Warning", MessageBoxButtons.OK);
                return;
            }
            
            double min_x, min_y, max_x, max_y;
            min_x = axScSamlightClientCtrl1.ScGetEntityOutline(edtEntityName, 0);
            min_y = axScSamlightClientCtrl1.ScGetEntityOutline(edtEntityName, 1);
            max_x = axScSamlightClientCtrl1.ScGetEntityOutline(edtEntityName, 3);
            max_y = axScSamlightClientCtrl1.ScGetEntityOutline(edtEntityName, 4);
            center_x = (min_x + max_x) / 2;
            center_y = (min_y + max_y) / 2;
           //GetCenterX = center_x;
           //GetCenterY = center_y;
           
        }
        public void GetSizeXYSamlight(string edtEntityName, out double sizeX, out double sizeY)  //获取某个实体的中心坐标X,y
        {
            sizeX = 0;
            sizeY = 0;
            if (axScSamlightClientCtrl1.ScIsRunning() == 0)
            {
                MessageBox.Show(MultiLanguage.GeSamlightNotFoundString(), MultiLanguage.GeSamlightWarningString(), MessageBoxButtons.OK);
                //MessageBox.Show("SAMLight not found", "Warning", MessageBoxButtons.OK);
                return;
            }

            double min_x, min_y, max_x, max_y;
            min_x = axScSamlightClientCtrl1.ScGetEntityOutline(edtEntityName, 0);
            min_y = axScSamlightClientCtrl1.ScGetEntityOutline(edtEntityName, 1);
            max_x = axScSamlightClientCtrl1.ScGetEntityOutline(edtEntityName, 3);
            max_y = axScSamlightClientCtrl1.ScGetEntityOutline(edtEntityName, 4);
            sizeX = (max_x - min_x);
            sizeY = (max_y - min_y);
            //GetCenterX = center_x;
            //GetCenterY = center_y;

        }
        //x,y 为绝对坐标,将某个实体移动到x,y 位置
        public bool AbsMoveTo(string entityName, double x, double y)
        {
            if (axScSamlightClientCtrl1.ScIsRunning() == 0)
            {
                MessageBox.Show(MultiLanguage.GeSamlightNotFoundString(), MultiLanguage.GeSamlightWarningString(), MessageBoxButtons.OK);
                //MessageBox.Show("SAMLight not found", "Warning", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                double center_x, center_y;
                double min_x, min_y, max_x, max_y;
                min_x = axScSamlightClientCtrl1.ScGetEntityOutline(entityName, 0);
                min_y = axScSamlightClientCtrl1.ScGetEntityOutline(entityName, 1);
                max_x = axScSamlightClientCtrl1.ScGetEntityOutline(entityName, 3);
                max_y = axScSamlightClientCtrl1.ScGetEntityOutline(entityName, 4);
                center_x = (min_x + max_x) / 2;
                center_y = (min_y + max_y) / 2;

                double disx = x - center_x;// Convert.ToDouble(edtTargetX.Text) - Convert.ToDouble(edt_cenX.Text);
                double disy = y - center_y;// Convert.ToDouble(edtTargetY.Text) - Convert.ToDouble(edt_cenY.Text);
                axScSamlightClientCtrl1.ScTranslateEntity(entityName, disx, disy, 0);
                return true;
            }
            
        }
        public bool RelMoveTo(string entityName, double x, double y)
        {
            if (axScSamlightClientCtrl1.ScIsRunning() == 0)
            {
                MessageBox.Show(MultiLanguage.GeSamlightNotFoundString(), MultiLanguage.GeSamlightWarningString(), MessageBoxButtons.OK);
                //MessageBox.Show("SAMLight not found", "Warning", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                double center_x, center_y;
                double min_x, min_y, max_x, max_y;
                min_x = axScSamlightClientCtrl1.ScGetEntityOutline(entityName, 0);
                min_y = axScSamlightClientCtrl1.ScGetEntityOutline(entityName, 1);
                max_x = axScSamlightClientCtrl1.ScGetEntityOutline(entityName, 3);
                max_y = axScSamlightClientCtrl1.ScGetEntityOutline(entityName, 4);
                center_x = (min_x + max_x) / 2;
                center_y = (min_y + max_y) / 2;

                double disx = x;// +center_x;// Convert.ToDouble(edtTargetX.Text) - Convert.ToDouble(edt_cenX.Text);
                double disy = y;// +center_y;// Convert.ToDouble(edtTargetY.Text) - Convert.ToDouble(edt_cenY.Text);
                axScSamlightClientCtrl1.ScTranslateEntity(entityName, disx, disy, 0);
                return true;
            }

        }
        public bool Rotate(string entityName, double angle)
        {
            if (axScSamlightClientCtrl1.ScIsRunning() == 0)
            {
                MessageBox.Show(MultiLanguage.GeSamlightNotFoundString(), MultiLanguage.GeSamlightWarningString(), MessageBoxButtons.OK);
                //MessageBox.Show("SAMLight not found", "Warning", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                double center_x, center_y;
                double min_x, min_y, max_x, max_y;
                min_x = axScSamlightClientCtrl1.ScGetEntityOutline(entityName, 0);
                min_y = axScSamlightClientCtrl1.ScGetEntityOutline(entityName, 1);
                max_x = axScSamlightClientCtrl1.ScGetEntityOutline(entityName, 3);
                max_y = axScSamlightClientCtrl1.ScGetEntityOutline(entityName, 4);
                center_x = (min_x + max_x) / 2;
                center_y = (min_y + max_y) / 2;

                axScSamlightClientCtrl1.ScRotateEntity(entityName, center_x, center_y, angle);
                return true;
            }
        }
        public bool ScaleSize(string entityName, double ScaleX, double ScaleY)
        {
            if (axScSamlightClientCtrl1.ScIsRunning() == 0)
            {
                MessageBox.Show(MultiLanguage.GeSamlightNotFoundString(), MultiLanguage.GeSamlightWarningString(), MessageBoxButtons.OK);
                //MessageBox.Show("SAMLight not found", "Warning", MessageBoxButtons.OK);
                return false;
            }
            else
            {
                axScSamlightClientCtrl1.ScScaleEntity(entityName, ScaleX, ScaleY, 0);
                return true;
            }
        }

        public void WriteBarcode(string filename,string barcodetext)
        {
            if (axScSamlightClientCtrl1.ScIsRunning() == 0)
            {
                MessageBox.Show(MultiLanguage.GeSamlightNotFoundString(), MultiLanguage.GeSamlightWarningString(), MessageBoxButtons.OK);
                //MessageBox.Show("SAMLight not found", "Warning", MessageBoxButtons.OK);
                return;
            }
            axScSamlightClientCtrl1.ScChangeTextByName(filename, barcodetext);

            //axScSamlightClientCtrl1.ScSetPen(
        }

        private bool isRedPointerShown = false;

        public void ShowRedPointer()
        {

            if (axScSamlightClientCtrl1.ScIsRunning() == 0)
            {
                MessageBox.Show(MultiLanguage.GeSamlightNotFoundString(), MultiLanguage.GeSamlightWarningString(), MessageBoxButtons.OK);
                //MessageBox.Show("SAMLight not found", "Warning", MessageBoxButtons.OK);
                return;
            }
            axScSamlightClientCtrl1.ScSetLongValue((int)65,2);
            
            axScSamlightClientCtrl1.ScExecCommand( ( int )SAMLIGHT_CLIENT_CTRL_OCXLib.ScComSAMLightClientCtrlExecCommandConstants.scComSAMLightClientCtrlExecCommandRedPointerStart );           
            //axScSamlightClientCtrl1.ScExecCommand( ( int )SAMLIGHT_CLIENT_CTRL_OCXLib.ScComSAMLightClientCtrlExecCommandConstants.scComSAMLightClientCtrlExecCommandRedPointerStop );           
            //axScSamlightClientCtrl1.ScExecCommand( ( int )SAMLIGHT_CLIENT_CTRL_OCXLib.ScComSAMLightClientCtrlExecCommandConstants.scComSAMLightClientCtrlExecCommandCloseMarkDialog );
            ////scComSAMLightClientCtrlLongValueTypeRedpointerMode
            isRedPointerShown = true;
        }

        public void HideRedPointer()
        {
            if (!isRedPointerShown)
                return;

            if (axScSamlightClientCtrl1.ScIsRunning() == 0)
            {
                //MessageBox.Show("SAMLight not found", "Warning", MessageBoxButtons.OK);
                return;
            }

            axScSamlightClientCtrl1.ScExecCommand((int)SAMLIGHT_CLIENT_CTRL_OCXLib.ScComSAMLightClientCtrlExecCommandConstants.scComSAMLightClientCtrlExecCommandRedPointerStop);

            axScSamlightClientCtrl1.ScExecCommand((int)SAMLIGHT_CLIENT_CTRL_OCXLib.ScComSAMLightClientCtrlExecCommandConstants.scComSAMLightClientCtrlExecCommandCloseMarkDialog);

            isRedPointerShown = false;
        }

        public void NewJob()
        {
            if (axScSamlightClientCtrl1.ScIsRunning() == 0)
            {
                MessageBox.Show(MultiLanguage.GeSamlightNotFoundString(), MultiLanguage.GeSamlightWarningString(), MessageBoxButtons.OK);
                //MessageBox.Show("SAMLight not found", "Warning", MessageBoxButtons.OK);
                return;
            }
            axScSamlightClientCtrl1.ScExecCommand(3);
        }

        public void ImportImage(string FilePath, float DitherSteps)
        {
            if (axScSamlightClientCtrl1.ScIsRunning() == 0)
            {
                MessageBox.Show(MultiLanguage.GeSamlightNotFoundString(), MultiLanguage.GeSamlightWarningString(), MessageBoxButtons.OK);
                //MessageBox.Show("SAMLight not found", "Warning", MessageBoxButtons.OK);
                return;
            }
            
            //flags = ( int )ScComSAMLightClientCtrlFlags.scComSAMLightClientCtrlImportFlagKeepOrder |
            //        ( int )ScComSAMLightClientCtrlFlags.scComSAMLightClientCtrlImportFlagReadPenInfo |
            //        ( int )ScComSAMLightClientCtrlFlags.scComSAMLightClientCtrlImportFlagCenterToField |
            //        ( int )ScComSAMLightClientCtrlFlags.scComSAMLightClientCtrlImportFlagCreateOneGroup;
            long flags = 0;
            axScSamlightClientCtrl1.ScImport("", FilePath, "jpg", 1, (int)flags);

            ScaleSize("", Para.SWImgToSamlightXScale, Para.SWImgToSamlightYScale);

            axScSamlightClientCtrl1.ScSetEntityDoubleData("", (int)ScComSAMLightClientCtrlFlags.scComSAMLightClientCtrlDoubleDataIdBitmapDitherstep, Math.Round(DitherSteps,3));
            //axScSamlightClientCtrl1.ScSetEntityLongData("", (int)ScComSAMLightClientCtrlFlags.scComSAMLightClientCtrlLongDataIdBitmapMode, 49);


            if (Para.SMBitmapInverted)
            {
                flags = (int)ScComSAMLightClientCtrlFlags.scComSAMLightClientCtrlLongDataIdBitmapModeInvert |
                    (int)ScComSAMLightClientCtrlFlags.scComSAMLightClientCtrlLongDataIdBitmapModeShowScanner |
                    (int)ScComSAMLightClientCtrlFlags.scComSAMLightClientCtrlLongDataIdBitmapModeShowBitmap;
            }
            else
            {
                flags = //(int)ScComSAMLightClientCtrlFlags.scComSAMLightClientCtrlLongDataIdBitmapModeInvert |
                        (int)ScComSAMLightClientCtrlFlags.scComSAMLightClientCtrlLongDataIdBitmapModeShowScanner |
                        (int)ScComSAMLightClientCtrlFlags.scComSAMLightClientCtrlLongDataIdBitmapModeShowBitmap;
            }

            if (Para.SamlightGreyScale)
            //if (true)
                flags = flags | (int)ScComSAMLightClientCtrlFlags.scComSAMLightClientCtrlLongDataIdBitmapModeGreyscale;

            axScSamlightClientCtrl1.ScSetEntityLongData("", (int)ScComSAMLightClientCtrlFlags.scComSAMLightClientCtrlLongDataIdBitmapMode, (int)flags);

            AbsMoveTo("", 0+Para.SMCenterToActualCenterX, 0+Para.SMCenterToActualCenterY);

            //Rotate("",45);
        }

        public void SetLaserPower(double Power)
        {
            axScSamlightClientCtrl1.ScSetDoubleValue((int)ScComSAMLightClientCtrlValueTypes.scComSAMLightClientCtrlDoubleValueTypeLaserPower, Power);
            //axScSamlightClientCtrl1.ScSetEntityDoubleData("", (int)ScComSAMLightClientCtrlValueTypes.scComSAMLightClientCtrlDoubleValueTypeLaserPower, Power);
        }
        public void SetFrequency(double Freq)
        {
            axScSamlightClientCtrl1.ScSetDoubleValue((int)ScComSAMLightClientCtrlValueTypes.scComSAMLightClientCtrlDoubleValueTypeFrequency, Freq);
        }
        public void SetMarkSpeed(double speed)
        {
            axScSamlightClientCtrl1.ScSetDoubleValue((int)ScComSAMLightClientCtrlValueTypes.scComSAMLightClientCtrlDoubleValueTypeMarkSpeed, speed);
        }
        //////////////////IO//////////////////////////
        public void Set_Output(int OutputBit, bool High)
        {
            int Flag = 1 << OutputBit - 1;
            int Mask = ~Flag << 16;
            if (!High)
                Flag = 0;
            int OptoIO = Mask | Flag;
            axScSamlightClientCtrl1.ScSetLongValue(
              (int)SAMLIGHT_CLIENT_CTRL_OCXLib.ScComSAMLightClientCtrlValueTypes.scComSAMLightClientCtrlLongValueTypeOptoIO,
              OptoIO);
            return;
        }

        public bool Get_Output(int OutputBit) // USC only
        {
            int Flag = 1 << OutputBit - 1;
            int GetOptoOut = axScSamlightClientCtrl1.ScGetLongValue(
               (int)SAMLIGHT_CLIENT_CTRL_OCXLib.ScComSAMLightClientCtrlValueTypes.scComSAMLightClientCtrlLongValueTypeGetOptoOut);
            if ((GetOptoOut & Flag) == Flag)
                return true;
            else
                return false;
        }

        public bool Get_Input(int InputBit)
        {
            int Flag = 1 << InputBit - 1;
            int OptoIO = axScSamlightClientCtrl1.ScGetLongValue(
               (int)SAMLIGHT_CLIENT_CTRL_OCXLib.ScComSAMLightClientCtrlValueTypes.scComSAMLightClientCtrlLongValueTypeOptoIO);
            if ((OptoIO & Flag) == Flag)
                return true;
            else
                return false;
        }
    }
}
