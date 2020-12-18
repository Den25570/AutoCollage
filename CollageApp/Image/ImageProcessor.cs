using CollageApp.Image;
using log4net;
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
        public bool inCenter;
        public Rectangle fieldRectangle;

        public List<ImageInfo> Images;
        public Bitmap Collage;
        public int Width;
        public int Height;
        public float Multiplier;

        public ImageInfo SelectedImage;
        public Rectangle SelectedImageRect { get; private set; }

        private ILog log;

        public ImageProcessor(ILog log)
        {
            Images = new List<ImageInfo>();
            this.log = log;
        }   

        public Bitmap GetFullCollage(int maxImages)
        {
            Collage = new Bitmap(Width, Height);
            Graphics collageGraphics = Graphics.FromImage(Collage);

            for (int i = 0; i < Images.Count && i < maxImages; i++)
            {
                ImageInfo image = Images[i];
                collageGraphics.DrawImage(image.bitmap, image.OriginalRect, image.SrcRect, GraphicsUnit.Pixel);
            }

            return Collage;
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
                CalculateImageRect(originalRect);
                graphics.DrawImage(SelectedImage.bitmap, SelectedImageRect);
            }
        }

        public void SelectImage(ImagePanel panel)
        {
            Images.ForEach(image => image.isSelected = false); 
            
            SelectedImage = Images.Find(image => image.ImagePanel == panel);
            SelectedImage.isSelected = true;
        }

        public void SelectImage(int index)
        {
            Images.ForEach(image => image.isSelected = false);

            if ((index >= 0) && (index < Images.Count))
            {
                SelectedImage = Images[index];
                SelectedImage.isSelected = true;
            }
        }

        public void UnSelectImage()
        {
            Images.ForEach(image => image.isSelected = false);
            SelectedImage = null;
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
                ImageInfo imageInfo = new ImageInfo(path, Images.Count);
                Images.Add(imageInfo);
            }
            catch (Exception e)
            {
                log.Error($"ImageInfo: {path} cannot be loaded.");
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

        private void CalculateImageRect(Rectangle originalRect)
        {
            float multiplier = Math.Min(originalRect.Height / (float)SelectedImage.bitmap.Height, originalRect.Width / (float)SelectedImage.bitmap.Width);
            SelectedImageRect = new Rectangle((int)(originalRect.Width / 2 - SelectedImage.bitmap.Width * multiplier / 2),
                (int)(originalRect.Height / 2 - SelectedImage.bitmap.Height * multiplier / 2),
                (int)(SelectedImage.bitmap.Width * multiplier),
                (int)(SelectedImage.bitmap.Height * multiplier));
        }
    }
}
