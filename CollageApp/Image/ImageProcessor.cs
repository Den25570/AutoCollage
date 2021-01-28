using CollageApp.Image;
using CollageApp.Templates;
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
        public float Multiplier;

        public ImageInfo SelectedImage;
        public int SelectedIndex { get; private set; }
        public Rectangle SelectedImageRect { get; private set; }

        private ILog log;
        private LinearTemplate template;

        //color
        public Brush BackgroundBrush = new SolidBrush(Color.Transparent);

        public ImageProcessor(ILog log, LinearTemplate template)
        {
            Images = new List<ImageInfo>();
            this.log = log;
            this.template = template;
        }   

        public void DrawTemplateBackground(Graphics graphics)
        {
            float width = template.Columns * (template.BlockWidth + template.LeftMargin) * template.Multiplier - template.LeftMargin * template.Multiplier;
            float height = template.Rows * (template.BlockHeight + template.BottomMargin) * template.Multiplier - template.BottomMargin * template.Multiplier;
            graphics.FillRectangle(BackgroundBrush, 0, 0, width, height);
        }

        public Bitmap GetFullCollage(int maxImages)
        {
            int width = template.Columns * (template.BlockWidth + template.LeftMargin) - template.LeftMargin;
            int height = template.Rows * (template.BlockHeight + template.BottomMargin) - template.BottomMargin;

            Collage = new Bitmap(width, height);
            Graphics collageGraphics = Graphics.FromImage(Collage);
            collageGraphics.FillRectangle(BackgroundBrush, 0, 0, Collage.Width, Collage.Height);

            for (int i = 0; i < Images.Count && i < maxImages; i++)
            {
                ImageInfo image = Images[i];

                RectangleF destRectangle = new RectangleF(image.OriginalRect.X + image.OriginalRectangleShift.X, image.OriginalRect.Y + image.OriginalRectangleShift.Y, image.OriginalRect.Width, image.OriginalRect.Height);

                if (!image.IsHidden && image.Visible)
                    collageGraphics.DrawImage(image.bitmap, destRectangle, image.SrcRect, GraphicsUnit.Pixel);
                else maxImages++;
            }

            return Collage;
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
            if (SelectedImage != null) {
                SelectedImage.isSelected = false;
            }
            SelectedImage = panel.AssociatedImage;
            SelectedIndex = SelectedImage.Index;

            SelectedImage.isSelected = true;
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
                    int selectedIndex = SelectedImage.Index;

                    if (index == selectedIndex)
                        return true;
                    else
                        SwapImages(selectedIndex, index);
                }
            }
            return false;
        }

        public void SwapImages(int index1, int index2)
        {
            Images[index1].Index = index2;
            Images[index2].Index = index1;
            ImageInfo temp = Images[index1];
            Images[index1] = Images[index2];
            Images[index2] = temp;
        }

        public void ChangeImageProperties(ImageInfo image, RectangleF srcRect, ImageFormatType imageFormatType, Boolean isHidden)
        {
            image.SrcRect = srcRect;
            image.imageFormatType = imageFormatType;
            image.IsHidden = isHidden;

            if (!isHidden)
            {
                image.ImagePanel.Show();
            }
            else
            {
                image.ImagePanel.Hide();
            }
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
