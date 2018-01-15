using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms;

namespace JPTLaserMarkWPF.Paint
{
    class RectangleDetail
    {
        public Rectangle rect;
        public float PenWidth;
        public bool Filled = false;
    }
    class LineDetail
    {
        public float PenWidth;
        public int Intensity;
        public List<Point> LinePt = new List<Point>();
    }
    class StraightLineDetail
    {
        public float PenWidth;
        //public float Intensity;
        public Point StartPt = new Point();
        public Point EndPt = new Point();
    }
    class LastAddedItems
    {
        public int ShapeType = -1; //0=line,1=Rect,2=Circle,3=straight Line  ,11=Image
        public int shapeIdx = -1;
    }
    class ImageDetail
    {
        public Image OriginalImg;
        public Image myImg;
        public int X;
        public int Y;        
    }

    class TextDetail
    {
        public String text;
        public int Intensity;
        public Point ctr = new Point();
        public Font myFont = new Font(FontFamily.GenericSansSerif, 20.0F, FontStyle.Regular);
    }

    public class HandPaint
    {
        bool draw = false;

        int pX = -1;
        int pY = -1;
        Canvas HPPnl = null;
        public bool PenEnabled = false;
        public bool RectEnabled = false;
        public bool FilledRectEnabled = false;
        public bool CirEnabled = false;
        public bool FilledCirEnabled = false;
        public bool StraightLineEnabled = false;
        public bool MoveImageEnabled = false;
        public bool MoveTextEnabled = false;
        Point ImgMoveStPt = new Point();
        Point TxtMoveStPt = new Point();
        bool ImgMoveClick = false;
        public bool DrawText = false;
        bool TextClick = false;

        public int mPenWidth = 8;
        public int mPenIntensity = 128; 
        Bitmap bmap;
        Rectangle mRect;
        TextDetail mText;
        StraightLineDetail stLine = new StraightLineDetail();
        List<Point> mLine = new List<Point>();
        List<RectangleDetail> rectangles = new List<RectangleDetail>();
        List<RectangleDetail> circles = new List<RectangleDetail>();
        List<LineDetail> Lines = new List<LineDetail>();
        List<ImageDetail> Images = new List<ImageDetail>();
        List<LastAddedItems> LastAddIdx = new List<LastAddedItems>();
        List<StraightLineDetail> StraightLines = new List<StraightLineDetail>();
        List<TextDetail> TextStr = new List<TextDetail>();

        Graphics display = null;

        bool exportImg = false;
        bool exportToPreview = false;
        public HandPaint(Canvas myCanvas)
        {
            HPPnl = myCanvas;
            bmap = new Bitmap(HPPnl.Width, HPPnl.Height);
            InitPanelEvents();
        }
        private void InitPanelEvents()
        {
            if (HPPnl == null)
                return;
            
            HPPnl.MouseMove += panel_MouseMove;
            HPPnl.MouseDown += panel_MouseDown;
            HPPnl.MouseUp += panel_MouseUp;
            HPPnl.Paint += panel_Paint;

            display = HPPnl.CreateGraphics();
            
        }
        
        public void panel_Paint(object sender, PaintEventArgs e)
        {
            
            for (int i = 0; i < Images.Count; i++)
            {
                e.Graphics.DrawImage(Images[i].myImg, Images[i].X, Images[i].Y);
            }

            if (mImg.myImg != null)
            {
                e.Graphics.DrawImage(mImg.myImg, mImg.X, mImg.Y);
            }

            if (StraightLineEnabled)
            {
                if (draw)
                {
                    float width = GetSelectedSize();
                    Pen pen = new Pen(Color.Black, width);
                    e.Graphics.DrawLine(pen, stLine.StartPt, stLine.EndPt);
                }
            }
            if ((RectEnabled))
            {                                               
                if (draw)
                {
                    float width = GetSelectedSize();
                    Pen pen = new Pen(Color.Black, width);
                    if (FilledRectEnabled)
                        e.Graphics.FillRectangle(Brushes.Black, mRect);
                    else
                        e.Graphics.DrawRectangle(pen, mRect);
                }                
            }

            if ((CirEnabled))
            {
                if (draw)
                {
                    float width = GetSelectedSize();
                    Pen pen = new Pen(Color.Black, width);
                    if (FilledCirEnabled)
                        e.Graphics.FillEllipse(Brushes.Black, mRect);
                    else
                        e.Graphics.DrawEllipse(pen, mRect);
                }
            }

            if (DrawText)
            {
                if (mText != null)
                {
                    Color baseColor = Color.Black;  // Or whatever, from the color picker
                    Color highlighter = Color.FromArgb(mPenIntensity, baseColor);
                    SolidBrush myBrush = new SolidBrush(highlighter);

                    e.Graphics.DrawString(mText.text, mText.myFont, myBrush, mText.ctr);
                }
            }            

            for (int i = 0; i < TextStr.Count; i++)
            {
                Color baseColor = Color.Black;  // Or whatever, from the color picker
                Color highlighter = Color.FromArgb(TextStr[i].Intensity, baseColor);
                SolidBrush myBrush = new SolidBrush(highlighter);

                e.Graphics.DrawString(TextStr[i].text, TextStr[i].myFont, myBrush, TextStr[i].ctr);
            }

            for (int i = 0; i < Lines.Count; i++)
            {
                //float width = mPenWidth;//GetSelectedSize();
                Color baseColor = Color.Black;  // Or whatever, from the color picker
                Color highlighter = Color.FromArgb(Lines[i].Intensity, baseColor);

                float width = Lines[i].PenWidth;
                Pen pen = new Pen(highlighter, width);
                e.Graphics.DrawLines(pen, Lines[i].LinePt.ToArray());
            }

            for (int i = 0; i < StraightLines.Count; i++)
            {
                float width = StraightLines[i].PenWidth;
                Pen pen = new Pen(Color.Black, width);
                e.Graphics.DrawLine(pen, StraightLines[i].StartPt, StraightLines[i].EndPt);
            }

            for (int i = 0; i < rectangles.Count; i++)
            {
                float width = rectangles[i].PenWidth;
                Pen pen = new Pen(Color.Black, width);
                if (rectangles[i].Filled)
                    e.Graphics.FillRectangle(Brushes.Black, rectangles[i].rect);
                else
                    e.Graphics.DrawRectangle(pen, rectangles[i].rect);
            }

            for (int i = 0; i < circles.Count; i++)
            {
                float width = circles[i].PenWidth;
                Pen pen = new Pen(Color.Black, width);
                if (circles[i].Filled)
                    e.Graphics.FillEllipse(Brushes.Black, circles[i].rect);
                else
                    e.Graphics.DrawEllipse(pen, circles[i].rect);
            }
            
            if (exportImg)
            {
                using (Graphics g = Graphics.FromImage(bmap))
                {
                    g.FillRectangle(Brushes.White, new Rectangle(0, 0, bmap.Width, bmap.Height));

                    for (int i = 0; i < Images.Count; i++)
                    {
                        g.DrawImage(Images[i].myImg, Images[i].X, Images[i].Y);
                    }

                    for (int i = 0; i < TextStr.Count; i++)
                    {
                        g.DrawString(TextStr[i].text, TextStr[i].myFont, Brushes.Black, TextStr[i].ctr);
                    }

                    for (int i = 0; i < Lines.Count; i++)
                    {
                        float width = Lines[i].PenWidth;
                        Color baseColor = Color.Black;  // Or whatever, from the color picker
                        Color highlighter = Color.FromArgb(Lines[i].Intensity, baseColor);

                        Pen pen = new Pen(highlighter, width);
                        g.DrawLines(pen, Lines[i].LinePt.ToArray());
                    }

                    for (int i = 0; i < StraightLines.Count; i++)
                    {
                        float width = StraightLines[i].PenWidth;
                        Pen pen = new Pen(Color.Black, width);
                        g.DrawLine(pen, StraightLines[i].StartPt, StraightLines[i].EndPt);
                    }

                    for (int i = 0; i < rectangles.Count; i++)
                    {
                        float width = rectangles[i].PenWidth;
                        Pen pen = new Pen(Color.Black, width);
                        if (rectangles[i].Filled)
                            g.FillRectangle(Brushes.Black, rectangles[i].rect);
                        else
                            g.DrawRectangle(pen, rectangles[i].rect);
                    }

                    for (int i = 0; i < circles.Count; i++)
                    {
                        float width = circles[i].PenWidth;
                        Pen pen = new Pen(Color.Black, width);
                        if (circles[i].Filled)
                            g.FillEllipse(Brushes.Black, circles[i].rect);
                        else
                            g.DrawEllipse(pen, circles[i].rect);
                    }                    
                }                
                //HPPnl.DrawToBitmap(bmap, new Rectangle(0, 0, HPPnl.Width, HPPnl.Height));  
                //e.Graphics.DrawImage(bmap, Point.Empty);
            }

            if (exportToPreview)
            {
                using (Graphics g = Graphics.FromImage(bmap))
                {
                    //g.FillRectangle(Brushes.White, new Rectangle(0, 0, bmap.Width, bmap.Height));

                    for (int i = 0; i < Images.Count; i++)
                    {
                        g.DrawImage(Images[i].myImg, Images[i].X, Images[i].Y);
                    }

                    for (int i = 0; i < TextStr.Count; i++)
                    {
                        g.DrawString(TextStr[i].text, TextStr[i].myFont, Brushes.Black, TextStr[i].ctr);
                    }

                    for (int i = 0; i < Lines.Count; i++)
                    {
                        float width = Lines[i].PenWidth;
                        Pen pen = new Pen(Color.Black, width);
                        g.DrawLines(pen, Lines[i].LinePt.ToArray());
                    }

                    for (int i = 0; i < StraightLines.Count; i++)
                    {
                        float width = StraightLines[i].PenWidth;
                        Pen pen = new Pen(Color.Black, width);
                        g.DrawLine(pen, StraightLines[i].StartPt, StraightLines[i].EndPt);
                    }

                    for (int i = 0; i < rectangles.Count; i++)
                    {
                        float width = rectangles[i].PenWidth;
                        Pen pen = new Pen(Color.Black, width);
                        if (rectangles[i].Filled)
                            g.FillRectangle(Brushes.Black, rectangles[i].rect);
                        else
                            g.DrawRectangle(pen, rectangles[i].rect);
                    }

                    for (int i = 0; i < circles.Count; i++)
                    {
                        float width = circles[i].PenWidth;
                        Pen pen = new Pen(Color.Black, width);
                        if (circles[i].Filled)
                            g.FillEllipse(Brushes.Black, circles[i].rect);
                        else
                            g.DrawEllipse(pen, circles[i].rect);
                    }
                }  
            }
                
        }

        public void ExportToPreviewImg(string fileName)
        {
            exportToPreview = true;
            HPPnl.Refresh();
            exportToPreview = false;

            bmap.Save(fileName);
        }
        public void ExportToImage(string fileName)
        {
            exportImg = true;
            HPPnl.Refresh();
            exportImg = false;

            bmap.Save(fileName);
            //Bitmap scaleImg = new Bitmap(Helper.ScaleImage(bmap, (int)(bmap.Width * Para.SWImgToLaserScale), (int)(bmap.Height * Para.SWImgToLaserScale)));            
            //scaleImg.Save(fileName);            
        }
        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if ((draw) && (PenEnabled))
            {
                //Graphics display = HPPnl.CreateGraphics();

                float width = mPenWidth;//GetSelectedSize();
                Color baseColor = Color.Black;  // Or whatever, from the color picker
                Color highlighter = Color.FromArgb(mPenIntensity, baseColor);

                Pen pen = new Pen(highlighter, width);

                pen.EndCap = LineCap.Round;
                pen.StartCap = LineCap.Round;

                display.DrawLine(pen, pX, pY, e.X, e.Y);
                mLine.Add(new Point(e.X, e.Y));                
            }

            if ((draw) && (StraightLineEnabled))
            {
                stLine.EndPt = new Point(e.X, e.Y);
                HPPnl.Refresh(); 
            }

            if ((draw) && (RectEnabled))
            {                
                mRect = new Rectangle(mRect.Left, mRect.Top, e.X - mRect.Left, e.Y - mRect.Top);
                HPPnl.Refresh();                                              
            }

            if ((draw) && (CirEnabled))
            {
                mRect = new Rectangle(mRect.Left, mRect.Top, e.X - mRect.Left, e.Y - mRect.Top);
                HPPnl.Refresh();
            }

            if ((MoveImageEnabled) && (ImgMoveClick))
            {
                if (mImg.myImg != null)
                {
                    mImg.X = mImg.X + (e.X - ImgMoveStPt.X);
                    mImg.Y = mImg.Y + (e.Y - ImgMoveStPt.Y);
                    ImgMoveStPt.X = e.X;
                    ImgMoveStPt.Y = e.Y;
                    HPPnl.Refresh();
                }
            }

            //if ((MoveImageEnabled) && (ImgMoveClick))
            //{
            //    if (Images.Count > 0)
            //    {
            //        Images[Images.Count - 1].X = Images[Images.Count - 1].X + (e.X - ImgMoveStPt.X);
            //        Images[Images.Count - 1].Y = Images[Images.Count - 1].Y + (e.Y - ImgMoveStPt.Y);
            //        ImgMoveStPt.X = e.X;
            //        ImgMoveStPt.Y = e.Y;
            //        HPPnl.Refresh();
            //    }
            //}

            if ((MoveTextEnabled) && (TextClick))
            {
                if (mText != null)
                {
                    mText.ctr.X = mText.ctr.X + (e.X - TxtMoveStPt.X);
                    mText.ctr.Y = mText.ctr.Y + (e.Y - TxtMoveStPt.Y);
                    TxtMoveStPt.X = e.X;
                    TxtMoveStPt.Y = e.Y;
                    HPPnl.Refresh();
                }
            }

            pX = e.X;
            pY = e.Y;
        }

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            draw = true;

            if ((RectEnabled) || (CirEnabled))
            {
                mRect = new Rectangle(e.X, e.Y, 0, 0);
            }

            if (PenEnabled)
            {
               mLine.Add(new Point(e.X,e.Y));
            }

            if (StraightLineEnabled)
            {
                stLine.StartPt = new Point(e.X, e.Y);
            }

            if (MoveImageEnabled)
            {
                if (mImg.myImg != null)
                {
                    ImgMoveClick = true;
                    ImgMoveStPt.X = e.X;
                    ImgMoveStPt.Y = e.Y;
                }
            }

            if (MoveTextEnabled)
            {                
                    TextClick = true;
                    TxtMoveStPt.X = e.X;
                    TxtMoveStPt.Y = e.Y;               
            }
            pX = e.X;
            pY = e.Y;
        }

        private void panel_MouseUp(object sender, MouseEventArgs e)
        {
            draw = false;
            ImgMoveClick = false ;
            TextClick = false;

            if (PenEnabled)
            {
                mLine.Add(new Point(e.X, e.Y));
                LineDetail tmp = new LineDetail();
                tmp.PenWidth = mPenWidth;//GetSelectedSize();
                tmp.Intensity = mPenIntensity;
                tmp.LinePt.AddRange(mLine);
                Lines.Add(tmp);
                mLine.Clear();

                LastAddedItems tmpItem = new LastAddedItems();
                tmpItem.ShapeType = 0;
                tmpItem.shapeIdx = Lines.Count() - 1;
                LastAddIdx.Add(tmpItem);
            }

            if (StraightLineEnabled)
            {
                stLine.EndPt = new Point(e.X, e.Y);
                StraightLineDetail tmp = new StraightLineDetail();
                tmp.PenWidth = GetSelectedSize();
                tmp.StartPt = stLine.StartPt;
                tmp.EndPt = stLine.EndPt;
                StraightLines.Add(tmp);                

                LastAddedItems tmpItem = new LastAddedItems();
                tmpItem.ShapeType = 3;
                tmpItem.shapeIdx = StraightLines.Count() - 1;
                LastAddIdx.Add(tmpItem);
            }

            if (RectEnabled)
            {
                RectangleDetail tmp = new RectangleDetail();
                tmp.PenWidth = GetSelectedSize();
                tmp.rect = mRect;
                if (FilledRectEnabled)
                    tmp.Filled = true;
                rectangles.Add(tmp);

                LastAddedItems tmpItem = new LastAddedItems();
                tmpItem.ShapeType = 1;
                tmpItem.shapeIdx = rectangles.Count() - 1;
                LastAddIdx.Add(tmpItem);
            }

            if (CirEnabled)
            {
                RectangleDetail tmp = new RectangleDetail();
                tmp.PenWidth = GetSelectedSize();
                tmp.rect = mRect;
                if (FilledCirEnabled)
                    tmp.Filled = true;
                circles.Add(tmp);

                LastAddedItems tmpItem = new LastAddedItems();
                tmpItem.ShapeType = 2;
                tmpItem.shapeIdx = circles.Count() - 1;
                LastAddIdx.Add(tmpItem);
            }

            HPPnl.Refresh();
        }

        public void NewPaint()
        {
            rectangles.Clear();
            circles.Clear();
            Lines.Clear();
            Images.Clear();
            StraightLines.Clear();
            TextStr.Clear();
            LastAddIdx.Clear();
            bmap = new Bitmap(HPPnl.Width, HPPnl.Height);
            HPPnl.Refresh();            
        }

        public void Undo()
        {
            if (LastAddIdx.Count == 0)
                return;

            int idx = LastAddIdx.Count-1;
            LastAddedItems tmpItem = new LastAddedItems();
            tmpItem = LastAddIdx[idx];
            LastAddIdx.RemoveAt(idx);

            switch (tmpItem.ShapeType)
            {
                case 0: Lines.RemoveAt(tmpItem.shapeIdx); break;
                case 1: rectangles.RemoveAt(tmpItem.shapeIdx); break;
                case 2: circles.RemoveAt(tmpItem.shapeIdx); break;
                case 3: StraightLines.RemoveAt(tmpItem.shapeIdx); break;
                case 11: Images.RemoveAt(tmpItem.shapeIdx); break;
                case 12: TextStr.RemoveAt(tmpItem.shapeIdx); break;  
            }
            HPPnl.Refresh();
        }

        public void UpdateTextStr(string myText,Font myFont)
        {
            if (mText == null)
            {
                mText = new TextDetail();
                mText.text = myText;
                mText.ctr.X = (HPPnl.Width / 2);
                mText.ctr.Y = (HPPnl.Height / 2);
            }
            else
            {
                mText.text = myText;
                mText.myFont = myFont;
            }

            HPPnl.Refresh(); 
        }

        public void AddTextStr()
        {
            if (mText == null)
                return;

            mText.Intensity = mPenIntensity;
            TextStr.Add(mText);

            LastAddedItems tmpItem = new LastAddedItems();
            tmpItem.ShapeType = 12;            
            tmpItem.shapeIdx = TextStr.Count() - 1;
            LastAddIdx.Add(tmpItem);

            mText = null;

            HPPnl.Refresh(); 
        }
        public void ImportImage(string FileName)
        {
            ImageDetail tmpImg = new ImageDetail();
            tmpImg.myImg = new Bitmap(FileName);
            tmpImg.X = (HPPnl.Width/2)-(tmpImg.myImg.Width/2);
            tmpImg.Y = (HPPnl.Height/2)-(tmpImg.myImg.Height/2);
            Images.Add(tmpImg);

            LastAddedItems tmpItem = new LastAddedItems();
            tmpItem.ShapeType = 11;
            tmpItem.shapeIdx = Images.Count() - 1;
            LastAddIdx.Add(tmpItem);
        }

        public void ImportImage(Image Img)
        {
            ImageDetail tmpImg = new ImageDetail();
            tmpImg.myImg = new Bitmap(Img);
            tmpImg.X = (HPPnl.Width / 2) - (tmpImg.myImg.Width / 2);
            tmpImg.Y = (HPPnl.Height / 2) - (tmpImg.myImg.Height / 2);
            Images.Add(tmpImg);

            LastAddedItems tmpItem = new LastAddedItems();
            tmpItem.ShapeType = 11;
            tmpItem.shapeIdx = Images.Count() - 1;
            LastAddIdx.Add(tmpItem);
        }

        public void ImportImageGreyScale(string FileName)
        {
            mImg.myImg = Helper.MakeGrayscale(new Bitmap(FileName));
            mImg.OriginalImg = Helper.MakeGrayscale(new Bitmap(FileName));
            mImg.X = (HPPnl.Width / 2) - (mImg.myImg.Width / 2);
            mImg.Y = (HPPnl.Height / 2) - (mImg.myImg.Height / 2);
            HPPnl.Refresh(); 

            //ImageDetail tmpImg = new ImageDetail();
            //tmpImg.myImg = Helper.MakeGrayscale( new Bitmap(FileName));
            //tmpImg.OriginalImg = Helper.MakeGrayscale(new Bitmap(FileName));
            //tmpImg.X = (HPPnl.Width / 2) - (tmpImg.myImg.Width / 2);
            //tmpImg.Y = (HPPnl.Height / 2) - (tmpImg.myImg.Height / 2);
            //Images.Add(tmpImg);

            //LastAddedItems tmpItem = new LastAddedItems();
            //tmpItem.ShapeType = 11;
            //tmpItem.shapeIdx = Images.Count() - 1;
            //LastAddIdx.Add(tmpItem);
            //HPPnl.Refresh(); 
        }
        ImageDetail mImg = new ImageDetail();

        public void ImportImageGreyScale(Image Img)
        {
            mImg.myImg = Helper.MakeGrayscale(new Bitmap(Img));
            mImg.OriginalImg = Helper.MakeGrayscale(new Bitmap(Img));
            mImg.X = (HPPnl.Width / 2) - (mImg.myImg.Width / 2);
            mImg.Y = (HPPnl.Height / 2) - (mImg.myImg.Height / 2);
            HPPnl.Refresh(); 
        }

        public void AddImageGreyScale()
        {
            if (mImg.myImg == null)
                return;
            ImageDetail tmpImg = new ImageDetail();
            tmpImg.myImg = new Bitmap(mImg.myImg);//Helper.MakeGrayscale(new Bitmap(mImg.myImg));
            tmpImg.OriginalImg = Helper.MakeGrayscale(new Bitmap(mImg.myImg));
            tmpImg.X = mImg.X;
            tmpImg.Y = mImg.Y;
            Images.Add(tmpImg);

            mImg.myImg = null;
            LastAddedItems tmpItem = new LastAddedItems();
            tmpItem.ShapeType = 11;
            tmpItem.shapeIdx = Images.Count() - 1;
            LastAddIdx.Add(tmpItem);
            HPPnl.Refresh();
        }

        public void ResizeLastImage(float Scale)
        {
            if (mImg.myImg == null)
                return;

            mImg.myImg = Helper.ScaleImage(mImg.OriginalImg, (int)(mImg.OriginalImg.Width * Scale), (int)(mImg.OriginalImg.Height * Scale));
            mImg.X = (HPPnl.Width / 2) - (mImg.myImg.Width / 2);
            mImg.Y = (HPPnl.Height / 2) - (mImg.myImg.Height / 2);
            HPPnl.Refresh();

            //if (Images.Count == 0)
            //    return;

            //int Idx = Images.Count - 1;
            //Images[Idx].myImg = Helper.ScaleImage(Images[Idx].OriginalImg, (int)(Images[Idx].OriginalImg.Width * Scale), (int)(Images[Idx].OriginalImg.Height * Scale));
            //Images[Idx].X = (HPPnl.Width / 2) - (Images[Idx].myImg.Width / 2);
            //Images[Idx].Y = (HPPnl.Height / 2) - (Images[Idx].myImg.Height / 2);
            //HPPnl.Refresh();
        }
        //////////////////////////Image ComboBox////////////////////////
        private const int MarginWidth = 4;
        private const int MarginHeight = 4;
        ComboBox SizeImgCB = null;        

        private int GetSelectedSize()
        {
            if (SizeImgCB == null)
                return 3;

            switch (SizeImgCB.SelectedIndex)
            {
                case 0: return 1;
                case 1: return 3;
                case 2: return 5;
                case 3: return 8;
                default: return 3;
            }
        }

        ///////////////////////Shapes//////////////////////////////
        
    }
}
