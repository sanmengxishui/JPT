using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Windows.Shapes;
using Common;
using System.IO;
using System.Windows.Threading;

namespace JPTLaserMarkWPF.PaintObject
{
    class ImageDetail
    {
        public Image OriginalImg = new Image();
        public Rectangle myImg = new Rectangle();
        public double Scale = 1.0;
        public int X;
        public int Y;
    }
    class TextDetail
    {
        public Border myTextObj = new Border();
        public FontFamily myFontFam;
        public string myOrgStr = "";
    }
    class LastAddedItems
    {
        public int ShapeType = -1; //1= image, 2 = hand, 3= Type
        public int shapeIdx = -1;
    }
    public class PaintObj
    {
        InkCanvas myInkCanvas;
        Canvas mainImg;
        RenderTargetBitmap rtb;
        int pX = -1;
        int pY = -1;
        int ImgSelIdx = -1;
        bool bMouseDown = false;
        public bool bMoveImage = false;
        public int mPenWidth = 6;
        public int mPenIntensity = 128;

        Border SelectedBor;
        Rectangle SelectedRec;

        //ImageDetail mImg = new ImageDetail();
        List<ImageDetail> Images = new List<ImageDetail>();
        List<TextDetail> Texts = new List<TextDetail>();
        List<LastAddedItems> LastAddIdx = new List<LastAddedItems>();
        public double CurImgScale
        {
            get {  
                    if ((ImgSelIdx == -1) || (Images.Count ==0) )
                        return 1.0;
                    else
                        return Images[ImgSelIdx].Scale; 
            }
            set
            {
                if ((ImgSelIdx == -1) || (Images.Count == 0))
                    return;
                Images[ImgSelIdx].Scale = value;
                Redraw();
            }
        } 

        public PaintObj(InkCanvas myINK, Canvas myImg)
        {
            myInkCanvas = myINK;
            mainImg = myImg;
            rtb = new RenderTargetBitmap((int)(mainImg.Width),
                                                            (int)(mainImg.Height),
                                                            96,
                                                            96,
                                                            PixelFormats.Pbgra32);

            //mainImg.Source = rtb;            
            SetPenEnabled(false);
            InitCanvasEvents();
            SetManipulation(true);
        }
        public void SetManipulation(bool val)
        {
            if (val)
            {
                mainImg.ManipulationStarting += Window_ManipulationStarting_1;
                mainImg.ManipulationDelta += Window_ManipulationDelta_1;
                mainImg.ManipulationInertiaStarting += Window_ManipulationInertiaStarting_1;
            }
            else
            {
                mainImg.ManipulationStarting -= Window_ManipulationStarting_1;
                mainImg.ManipulationDelta -= Window_ManipulationDelta_1;
                mainImg.ManipulationInertiaStarting -= Window_ManipulationInertiaStarting_1;
            }
        }

        public void SetPenEnabled(bool val)
        {
            if (val)
            {
                myInkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                myInkCanvas.MouseDown -= MainImg_MouseDown;
                myInkCanvas.MouseMove -= MainImg_MouseMove;
                myInkCanvas.MouseUp -= MainImg_MouseUp;
                //LastAddedItems tmpItem = new LastAddedItems();
                //tmpItem.ShapeType = 2;
                //tmpItem.shapeIdx = 0;
                //LastAddIdx.Add(tmpItem);
                ChangeInkIntensity(125);
            }
            else
            {
                myInkCanvas.EditingMode = InkCanvasEditingMode.None;
                myInkCanvas.MouseDown += MainImg_MouseDown;
                myInkCanvas.MouseMove += MainImg_MouseMove;
                myInkCanvas.MouseUp += MainImg_MouseUp;
            }
        }

        private void InitCanvasEvents()
        {            
            mainImg.MouseDown += MainImg_MouseDown;
            mainImg.MouseMove += MainImg_MouseMove;
            mainImg.MouseUp += MainImg_MouseUp;
        }

        private void MainImg_MouseUp(object sender, MouseEventArgs e)
        {
            bMouseDown = false;                        
        }
        private void MainImg_MouseMove(object sender, MouseEventArgs e)
        {            
            if (bMouseDown)
            {
                //Point p = e.GetPosition(mainImg);
                //if (bMoveImage)
                //{                    
                //    if ((ImgSelIdx == -1) || (Images.Count == 0))
                //        return;

                //    if (Images[ImgSelIdx].myImg.Source != null)
                //    {                       
                //        Images[ImgSelIdx].X = Images[ImgSelIdx].X + ((int)p.X - pX);
                //        Images[ImgSelIdx].Y = Images[ImgSelIdx].Y + ((int)p.Y - pY);                        
                //        Redraw();
                //    }
                //}
                //pX = (int)p.X;
                //pY = (int)p.Y;
            }            
        }
        private void MainImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bMouseDown = true;
            Point p = e.GetPosition(mainImg);
            HitTest(p);
            pX = (int)p.X;
            pY = (int)p.Y;
        }
        private void HitTest(Point p)
        {
            //ImgSelIdx = -1;
            //Rect tmpRect;
            //for (int i = Images.Count - 1; i >= 0; i--)
            //{
            //    tmpRect = CalculateImageRect(Images[i].myImg, Images[i].X, Images[i].Y, Images[i].Scale);
            //    if (tmpRect.Contains(p))
            //    {
            //        ImgSelIdx = i;
            //        return;
            //    }
            //}
        }
        private void DrawLine()
        {                        
        }
        private void DrawImages()
        {
            //var drawingVisual = new DrawingVisual();
            //var dc = drawingVisual.RenderOpen();

            //for (int i = 0; i < Images.Count; i++)
            //{
            //    dc.DrawImage(Images[i].myImg.Source, CalculateImageRect(Images[i].myImg, Images[i].X, Images[i].Y, Images[i].Scale));
            //}                      
            //dc.Close();
            //rtb.Render(drawingVisual);
            //mainImg.Source = rtb;
        }
        private void Redraw()
        {
            rtb.Clear();
            DrawLine();
            DrawImages();
            //mainImg.Source = rtb;
        }
        public void NewPaint()
        {
            Images.Clear();
            Texts.Clear();
            mainImg.Children.Clear();
            myInkCanvas.Strokes.Clear();
            LastAddIdx.Clear();           
        }

        public void Undo()
        {
            if (LastAddIdx.Count == 0)
                return;

            int idx = LastAddIdx.Count - 1;
            LastAddedItems tmpItem = new LastAddedItems();
            tmpItem = LastAddIdx[idx];            

            switch (tmpItem.ShapeType)
            {
                case 1:
                    {
                        mainImg.Children.Remove(Images[tmpItem.shapeIdx].myImg);
                        Images.RemoveAt(tmpItem.shapeIdx);
                        LastAddIdx.RemoveAt(idx); break; 
                    }
                case 2:
                    {
                        int cnt = myInkCanvas.Strokes.Count();
                        if (cnt != 0)
                            myInkCanvas.Strokes.RemoveAt(cnt-1);                         
                        break;
                    }
                case 3:
                    {
                        mainImg.Children.Remove(Texts[tmpItem.shapeIdx].myTextObj);
                        Texts.RemoveAt(tmpItem.shapeIdx);
                        LastAddIdx.RemoveAt(idx); break;
                    }            
            }
        }
        public void UndoHand()
        {
            int cnt = myInkCanvas.Strokes.Count();
            if (cnt != 0)
                myInkCanvas.Strokes.RemoveAt(cnt - 1);  
        }
        //////////////////////////////Images
        private Rect CalculateImageRect(Image img, double PosX, double PosY, double scale)
        {
            img.Width = img.Source.Width * scale;
            img.Height = img.Source.Height * scale;
            Rect tmpRect = new Rect((PosX - img.Width / 2), (PosY - img.Height / 2), img.Width, img.Height);           
            return tmpRect;
        }
        public void ImportImageGreyScale(ImageSource myImg)
        {
            MatrixTransform myT = new MatrixTransform();
            Matrix mt = myT.Matrix;
            mt.OffsetY = 200;
            mt.OffsetX = 200;

            ImageBrush ib = new ImageBrush();
            ib.ImageSource = myImg;

            Rectangle image = new Rectangle();
            //image.Width = ib.ImageSource.Width;
            //image.Height = ib.ImageSource.Height;
            image.Width = mainImg.Width;
            image.Height = mainImg.Height;
            image.Fill = ib;
            image.IsManipulationEnabled = true;
            image.RenderTransform = myT;
            ImageDetail tmpImg = new ImageDetail();
            tmpImg.myImg = image;
            tmpImg.OriginalImg.Source = myImg;
            tmpImg.X = (int)(tmpImg.OriginalImg.Source.Width / 2);
            tmpImg.Y = (int)(tmpImg.OriginalImg.Source.Height / 2);
            Images.Add(tmpImg);

            mainImg.Children.Add(image);

            LastAddedItems tmpItem = new LastAddedItems();
            tmpItem.ShapeType = 1;
            tmpItem.shapeIdx = Images.Count() - 1;
            LastAddIdx.Add(tmpItem);

            SelectedRec = image;
            ImgSelIdx = Images.Count() - 1;
            //DrawImages();
        }
        public void ImportImageGreyScale(string FileName)
        {
            MatrixTransform myT = new MatrixTransform();
            Matrix mt = myT.Matrix;
            mt.OffsetY = 200;
            mt.OffsetX = 200;

            ImageBrush ib = new ImageBrush();
            ib.ImageSource = Helper.ConvertToGrayScale(FileName);

            Rectangle image = new Rectangle();
            //image.Width = ib.ImageSource.Width;
            //image.Height = ib.ImageSource.Height;
            image.Width = mainImg.Width;
            image.Height = mainImg.Height;
            image.Fill = ib;
            image.IsManipulationEnabled = true;
            image.RenderTransform = myT;           
            ImageDetail tmpImg = new ImageDetail();
            tmpImg.myImg = image;
            tmpImg.OriginalImg.Source = Helper.ConvertToGrayScale(FileName);
            tmpImg.X = (int)(tmpImg.OriginalImg.Source.Width / 2);
            tmpImg.Y = (int)(tmpImg.OriginalImg.Source.Height / 2);
            Images.Add(tmpImg);

            mainImg.Children.Add(image);

            LastAddedItems tmpItem = new LastAddedItems();
            tmpItem.ShapeType = 1;
            tmpItem.shapeIdx = Images.Count() - 1;
            LastAddIdx.Add(tmpItem);

            SelectedRec = image;
            ImgSelIdx = Images.Count() - 1;
            //DrawImages();
        }
        public void ExportToImage(string fileName)
        {
            //myInkCanvas.
            //Rectangle fillBackground = new Rectangle
            //{
            //    Width = 6646,
            //    Height = 3940,
            //    Fill = Brushes.White
            //};



            var rtb = new RenderTargetBitmap((int)myInkCanvas.ActualWidth,
                                               (int)myInkCanvas.ActualHeight,
                                               96d, 96d,
                PixelFormats.Pbgra32 // pixelformat 
                );
            //rtb.Render(fillBackground);
            rtb.Render(myInkCanvas);

            //filename += ".jpg";
            var enc = new System.Windows.Media.Imaging.JpegBitmapEncoder();
            enc.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(rtb));

            using (var stm = System.IO.File.Create(fileName))
            {
                enc.Save(stm);
            }

            //Rect bounds = VisualTreeHelper.GetDescendantBounds(myInkCanvas);
            //double dpi = 96d;

            //RenderTargetBitmap rtb = new RenderTargetBitmap((int)bounds.Width, (int)bounds.Height, dpi, dpi, System.Windows.Media.PixelFormats.Default);
            //DrawingVisual dv = new DrawingVisual();
            //using (DrawingContext dc = dv.RenderOpen())
            //{
            //    VisualBrush vb = new VisualBrush(myInkCanvas);
            //    dc.DrawRectangle(vb, null, new Rect(new Point(), bounds.Size));
            //}
            //rtb.Render(dv);

            //BitmapEncoder pngEncoder = new PngBitmapEncoder();
            //pngEncoder.Frames.Add(BitmapFrame.Create(rtb));
            //using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            //{
            //    pngEncoder.Save(ms);
            //    System.IO.File.WriteAllBytes(fileName, ms.ToArray());
            //}

            //RenderTargetBitmap targetBitmap =
            //        new RenderTargetBitmap((int)myInkCanvas.ActualWidth,
            //                               (int)myInkCanvas.ActualHeight,
            //                               96d, 96d,
            //                               PixelFormats.Default);                
            //    targetBitmap.Render(myInkCanvas);

            //    BitmapEncoder encoder = new BmpBitmapEncoder();                
            //    encoder.Frames.Add(BitmapFrame.Create(targetBitmap));
            //    using (FileStream fs = File.Open(fileName, FileMode.OpenOrCreate))
            //    {
            //        encoder.Save(fs);
            //    }    
        }

        //////////////Ink Canvas
        public void ChangeInkThickness(double thick)
        {
            myInkCanvas.DefaultDrawingAttributes.Width = thick;
            myInkCanvas.DefaultDrawingAttributes.Height = thick;
            //<InkCanvas.DefaultDrawingAttributes>
            //<DrawingAttributes x:Name="attribute" Width="40" Height="40" Color="Green"/>
            //</InkCanvas.DefaultDrawingAttributes>
        }        
        public void ChangeInkIntensity(double Intensity)
        {
            float tmp = (float)Intensity / 255;
            Color baseColor = Helper.ChangeColorBrightness(Colors.Black, 1-tmp);
            //Color baseColor = Colors.Black;  // Or whatever, from the color picker
            //Color highlighter = Color.FromArgb(baseColor.A, (byte)(baseColor.R * tmp), (byte)(baseColor.G * tmp), (byte)(baseColor.B * tmp));
            myInkCanvas.DefaultDrawingAttributes.Color = baseColor;
        }

        //////////////Text
        public void ImportText(string myStr, FontFamily myFamily)
        {
            MatrixTransform myT = new MatrixTransform();
            Matrix mt = myT.Matrix;
            mt.OffsetY = 200;
            mt.OffsetX = 200;

            int size = 48;
            TextBlock tmpBrd = new TextBlock();
            tmpBrd.Text = myStr;
            tmpBrd.FontFamily = myFamily;
            tmpBrd.FontSize = size;
            
            Border myBrd = new Border();
            myBrd.Child = tmpBrd;
            myBrd.IsManipulationEnabled = true;
            myBrd.RenderTransform = myT;
            mainImg.Children.Add(myBrd);

            TextDetail tmpTxt = new TextDetail();
            tmpTxt.myTextObj = myBrd;
            tmpTxt.myOrgStr = myStr;
            tmpTxt.myFontFam = myFamily;
            Texts.Add(tmpTxt);

            LastAddedItems tmpItem = new LastAddedItems();
            tmpItem.ShapeType = 3;
            tmpItem.shapeIdx = Texts.Count() - 1;
            LastAddIdx.Add(tmpItem);

            SelectedBor = myBrd;

            AlignLastSelectedItem();
        }

        public void UpdateText(string newText, FontFamily myFamily)
        {
            if (SelectedBor != null)
            {
                (SelectedBor.Child as TextBlock).Text = newText;
                //(SelectedBor.Child as TextBlock).FontStyle = FontStyles.Italic;
                (SelectedBor.Child as TextBlock).FontFamily = myFamily;
                //(SelectedBor.Child as TextBlock).FontFamily = new FontFamily("楷体");
            }
        }
        private Rectangle CreateNewRectangle(string txt, FontFamily myFontFamily)
        {
            Rectangle r = new Rectangle();
            r.Width = txt.Length * 24;
            r.Height = 24;
            //r.RenderTransform = new TranslateTransform(100, 100);
            TextBlock TB = new TextBlock();
            TB.FontFamily = myFontFamily;
            TB.Text = txt;
            //The next two magical lines create a special brush that contains a bitmap rendering of the UI element that can then be used like any other brush and its in hardware and is almost the text book example for utilizing all hardware rending performances in WPF unleashed 4.5
            BitmapCacheBrush bcb = new BitmapCacheBrush(TB);
            r.Fill = bcb;
            return r;
        }


        /////////////Manipulation
        public Rect GetBounds(FrameworkElement of, FrameworkElement from)
        {
            GeneralTransform transform = null;
            // Might throw an exception if of and from are not in the same visual tree
            try
            {
                transform = of.TransformToVisual(from);
                return transform.TransformBounds(new Rect(0, 0, of.ActualWidth, of.ActualHeight));
            }
            catch (Exception ex)
            {
                return new Rect();
            }            
        }        
        public void AlignLastSelectedItem()
        {
            if (SelectedRec != null)
            {
                Matrix rectsMatrix = ((MatrixTransform)SelectedRec.RenderTransform).Matrix;
                
                var x = new Vector(1, 0);
                Vector rotated = Vector.Multiply(x, rectsMatrix);
                double angleBetween = Vector.AngleBetween(x, rotated);
                rectsMatrix.Rotate(-angleBetween);
                SelectedRec.RenderTransform = new MatrixTransform(rectsMatrix);

                SelectedRec.UpdateLayout();

                rectsMatrix = ((MatrixTransform)SelectedRec.RenderTransform).Matrix;
                Rect myTmpRec = GetBounds(SelectedRec, mainImg);
                double X = (mainImg.Width - myTmpRec.Width) / 2;
                double Y = (mainImg.Height - myTmpRec.Height) / 2;                
                rectsMatrix.Translate(-rectsMatrix.OffsetX, -rectsMatrix.OffsetY);
                rectsMatrix.Translate(X, Y);
                SelectedRec.RenderTransform = new MatrixTransform(rectsMatrix);
                
                //SelectedRec.RenderTransform = new RotateTransform(0, SelectedRec.Width / 2, SelectedRec.Height / 2);
                
            }
            if (SelectedBor != null)
            {
                var element = SelectedBor as FrameworkElement;
                var transformation = element.RenderTransform as MatrixTransform;
                var matrix = transformation == null ? Matrix.Identity : transformation.Matrix;

                var x = new Vector(1, 0);
                Vector rotated = Vector.Multiply(x, matrix);
                double angleBetween = Vector.AngleBetween(x, rotated);
                matrix.Rotate(-angleBetween);
                SelectedBor.RenderTransform = new MatrixTransform(matrix);

                SelectedBor.UpdateLayout();

                element = SelectedBor as FrameworkElement;
                transformation = element.RenderTransform as MatrixTransform;
                matrix = transformation == null ? Matrix.Identity : transformation.Matrix;

                Rect myTmpRec = GetBounds(SelectedBor, mainImg);
                double X = (mainImg.Width - myTmpRec.Width) / 2;
                double Y = (mainImg.Height - myTmpRec.Height) / 2;
                matrix.Translate(-matrix.OffsetX, -matrix.OffsetY);
                matrix.Translate(X, Y);
                SelectedBor.RenderTransform = new MatrixTransform(matrix);
            }
        }
        private void Window_ManipulationStarting_1(object sender, ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = mainImg;//this;
            e.Handled = true;
        }

        private void Window_ManipulationDelta_1(object sender, ManipulationDeltaEventArgs e)
        {
            if (e.OriginalSource is Rectangle)
            {
                Rectangle rectToMove;
                //
                rectToMove = e.OriginalSource as Rectangle;
                //else
                //    rectToMove = ((MatrixTransform)(e.OriginalSource as TextBlock).RenderTransform).Matrix;

                Matrix rectsMatrix = ((MatrixTransform)rectToMove.RenderTransform).Matrix;

                //rectsMatrix.RotateAt(e.DeltaManipulation.Rotation, e.ManipulationOrigin.X, e.ManipulationOrigin.Y);

                rectsMatrix.ScaleAt(e.DeltaManipulation.Scale.X, e.DeltaManipulation.Scale.X,
                   e.ManipulationOrigin.X, e.ManipulationOrigin.Y);

                rectsMatrix.Translate(e.DeltaManipulation.Translation.X,
                   e.DeltaManipulation.Translation.Y);

                rectToMove.RenderTransform = new MatrixTransform(rectsMatrix);
                Rect containingRect = new Rect(((FrameworkElement)e.ManipulationContainer).RenderSize);

                Rect shapeBounds = rectToMove.RenderTransform.TransformBounds(new Rect(rectToMove.RenderSize));

                if (e.IsInertial && !containingRect.Contains(shapeBounds))
                {
                    e.Complete();
                }

                e.Handled = true;
                SelectedRec = e.OriginalSource as Rectangle;
                SelectedBor = null;
            }
            if (e.OriginalSource is Border)
            {
                var element = e.Source as FrameworkElement;
                var transformation = element.RenderTransform as MatrixTransform;
                var matrix = transformation == null ? Matrix.Identity : transformation.Matrix;

                matrix.RotateAt(e.DeltaManipulation.Rotation, e.ManipulationOrigin.X, e.ManipulationOrigin.Y);

                matrix.ScaleAt(e.DeltaManipulation.Scale.X, e.DeltaManipulation.Scale.X,
                   e.ManipulationOrigin.X, e.ManipulationOrigin.Y);

                matrix.Translate(e.DeltaManipulation.Translation.X,
                   e.DeltaManipulation.Translation.Y);

                element.RenderTransform = new MatrixTransform(matrix);
                e.Handled = true;

                SelectedRec = null;
                SelectedBor = e.OriginalSource as Border;
            }
        }        

        private void Window_ManipulationInertiaStarting_1(object sender, ManipulationInertiaStartingEventArgs e)
        {
            //e.TranslationBehavior.DesiredDeceleration = 10.0 * 96.0 / (1000.0 * 1000.0);
            //e.ExpansionBehavior.DesiredDeceleration = 0.1 * 96 / (1000.0 * 1000.0);
           // e.RotationBehavior.DesiredDeceleration = 720 / (1000.0 * 1000.0);
            e.Handled = true;
        }

    }
}
