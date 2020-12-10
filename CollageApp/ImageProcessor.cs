using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CollageApp
{
    public class ImageProcessor
    {
        public List<Image> Images;
        public Image SelectedImage;
        public Point RelativeSelectedPoint;
        public Bitmap Collage;
        public int Width;
        public int Height;
        public float Multiplier;

        private Pen selectionPen;
        private SolidBrush cutFieldBrush;

        public ImageProcessor()
        {
            Images = new List<Image>();

            selectionPen = new Pen(Color.Blue, 2);
            Color lg = Color.LightGreen;
            lg = Color.FromArgb(120, lg.R, lg.G, lg.B);
            cutFieldBrush = new SolidBrush(lg);
        }   

        public Bitmap GetFullCollage(int maxImages)
        {
            Collage = new Bitmap(Width, Height);
            Graphics collageGraphics = Graphics.FromImage(Collage);

            for (int i = 0; i < Images.Count && i < maxImages; i++)
            {
                Image image = Images[i];
                collageGraphics.DrawImage(image.bitmap, image.OriginalRect, image.SrcRect, GraphicsUnit.Pixel);
            }

            return Collage;
        }

        public void RenderImages(Graphics graphics, int maxImages)
        {
            Collage = new Bitmap(Width, Height);
            Graphics collageGraphics = Graphics.FromImage(Collage);

            for (int i = 0; i < Images.Count && i < maxImages; i++)
            {
                Image image = Images[i];
                if (image != SelectedImage)
                    collageGraphics.DrawImage(image.bitmap, image.OriginalRect, image.SrcRect, GraphicsUnit.Pixel);
            }

            graphics.DrawImage(Collage, 0, 0, Width * Multiplier, Height * Multiplier);
        }

        public void DrawSavedCollage(Graphics graphics)
        {
            graphics.DrawImage(Collage, 0, 0, Width * Multiplier, Height * Multiplier);
        }

        internal void DrawSelectedImage(Graphics graphics)
        {
            if (SelectedImage != null)
            {
                graphics.DrawImage(SelectedImage.bitmap, SelectedImage.Rect, SelectedImage.SrcRect, GraphicsUnit.Pixel);
            }
        }

        public void RenderPreview(Graphics graphics, Rectangle originalRect)
        {
            if (SelectedImage != null)
            {
                float multiplier = SelectedImage.bitmap.Width > originalRect.Width ? originalRect.Width / (float)SelectedImage.bitmap.Width : 1;
                multiplier = originalRect.Height / SelectedImage.bitmap.Height < multiplier ? originalRect.Height / (float)SelectedImage.bitmap.Height : multiplier;
                graphics.DrawImage(SelectedImage.bitmap, 0, 0, SelectedImage.bitmap.Width * multiplier, SelectedImage.bitmap.Height * multiplier);

                graphics.FillRectangle(cutFieldBrush, SelectedImage.SrcRect.X * multiplier, SelectedImage.SrcRect.Y * multiplier, SelectedImage.SrcRect.Width * multiplier, SelectedImage.SrcRect.Height * multiplier);
            }
        }

        public void DrawSelectionRect(Graphics graphics)
        {
            if (SelectedImage != null)
            {
                graphics.DrawRectangle(selectionPen, (int)SelectedImage.Rect.X, (int)SelectedImage.Rect.Y, (int)SelectedImage.Rect.Width, (int)SelectedImage.Rect.Height);
            }
        }

        public void SelectImage(Point pos, int maxImages)
        {
            SelectedImage = null;
            for (int i = 0; i < Images.Count && i < maxImages; i++)
            {
                Image image = Images[i];
                if (pos.X >= image.Rect.X && pos.X <= image.Rect.X + image.Rect.Width && pos.Y >= image.Rect.Y && pos.Y <= image.Rect.Y + image.Rect.Height)
                {
                    if (SelectedImage == null || image.Z > SelectedImage.Z)
                    {
                        SelectedImage = image;
                        RelativeSelectedPoint.X = pos.X - (int)image.Rect.X;
                        RelativeSelectedPoint.Y = pos.Y - (int)image.Rect.Y;
                    }
                }
            }
        }

        public void SelectImage(int index)
        {
            if ((index >= 0) && (index < Images.Count))
            {
                SelectedImage = Images[index];
                RelativeSelectedPoint.X = (int)SelectedImage.Rect.Width / 2;
                RelativeSelectedPoint.Y = (int)SelectedImage.Rect.Height / 2;
            }
        }
    
        public bool PlaceImage(int index)
        {
            if (SelectedImage != null)
            {
                if (index < Images.Count)
                {
                    int selectedIndex = Images.FindIndex(image => image.Name == SelectedImage.Name);

                    if (index == selectedIndex)
                    {
                        return true;
                    }

                    Images[selectedIndex] = Images[index];
                    Images[index] = SelectedImage;
                }
                SelectedImage = null;
            }
            return false;
        }

        public void LoadAllImages(IEnumerable<string> paths)
        {
            foreach (string path in paths)
            {
                if (File.Exists(path))
                {
                    LoadImage(path);
                }
                else if (Directory.Exists(path))
                {
                    ProcessDirectory(path);
                }
            }
        }

        public void LoadImage(string path)
        {
            try
            {
                Images.Add(new Image(path, Images.Count));
            }
            catch (Exception e)
            {
                //
            }
        }

        public void ClearImages()
        {
            Images.Clear();
        }

        private void ProcessDirectory(string targetDirectory, bool recursiveSearch = true)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                LoadImage(fileName);

            if (recursiveSearch)
            {
                string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
                foreach (string subdirectory in subdirectoryEntries)
                    ProcessDirectory(subdirectory, recursiveSearch);
            }
        }
    }
}
