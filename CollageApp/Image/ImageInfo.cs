using CollageApp.Image;
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
    public enum ImageFormatType
    {
        CutTopLeft,
        CutBotRight,
        CutMiddle,
        CustomCut,
        Stretch
    }

    public class ImageInfo
    {
        //
        public RectangleF OriginalRect;
        public Point OriginalRectangleShift;
        public RectangleF Rect;
        public string Name;
        public Bitmap bitmap;
        public ImagePanel ImagePanel = null;
        private Pen selectionPen = new Pen(Color.Blue, 4);

        //
        public RectangleF SrcRect;
        public ImageFormatType imageFormatType = ImageFormatType.CutMiddle;
        public Boolean Visible = true;
        public Boolean IsHidden = false;

        //
        public bool isSelected = false;
        public int Index;

        public ImageInfo(string path, int index)
        {
            try
            {
                ImagePanel = new ImagePanel(this);
                bitmap = new Bitmap(path);
                Index = index;

                ImagePanel.Visible = true;
                ImagePanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
                ImagePanel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
                ImagePanel.Name = "ImagePanel_" + index;
                ImagePanel.Paint += panel_Paint;
            }
            catch (Exception e)
            {
                throw e;
            }

            Name = Path.GetFileName(path);
        }
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            ImagePanel.Visible = Visible && !IsHidden;
            if (Visible && !IsHidden)
            {
                e.Graphics.DrawImage(bitmap, (sender as Panel).ClientRectangle, SrcRect, GraphicsUnit.Pixel);
                if (isSelected)
                    e.Graphics.DrawRectangle(selectionPen, (sender as Panel).ClientRectangle);
            }           
        }

        public void CalculateSrcRect()
        {
            SrcRect = CalculateSrcRect(imageFormatType);
        }

        public RectangleF CalculateSrcRect(ImageFormatType imageFormatType)
        {
            RectangleF srcRect = new RectangleF();
            float scale = Math.Min(bitmap.Width / OriginalRect.Width, bitmap.Height / OriginalRect.Height);
            switch (imageFormatType)
            {
                case ImageFormatType.CutTopLeft:
                    srcRect = new RectangleF(0, 0, OriginalRect.Width * scale, OriginalRect.Height * scale);
                    break;
                case ImageFormatType.CutBotRight:
                    srcRect = new RectangleF(bitmap.Width - OriginalRect.Width * scale, bitmap.Height - OriginalRect.Height * scale, OriginalRect.Width * scale, OriginalRect.Height * scale);
                    break;
                case ImageFormatType.CutMiddle:
                    srcRect = new RectangleF(bitmap.Width / 2 - (OriginalRect.Width * scale) / 2, bitmap.Height / 2 - (OriginalRect.Height * scale) / 2, OriginalRect.Width * scale, OriginalRect.Height * scale);
                    break;
                case ImageFormatType.CustomCut:
                    break;
                case ImageFormatType.Stretch:
                    srcRect = new RectangleF(0, 0, bitmap.Width, bitmap.Height);
                    break;
            }
            return srcRect;
        }

    }
}
