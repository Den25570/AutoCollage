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
        public RectangleF OriginalRect;
        public RectangleF Rect;
        public RectangleF SrcRect;
        public ImageFormatType imageFormatType;
        public int Z;
        public string Name;

        public bool isSelected = false;

        public Bitmap bitmap;

        public ImagePanel ImagePanel = new ImagePanel();

        private Pen selectionPen = new Pen(Color.Blue, 4);

        public ImageInfo(string path, int z)
        {
            try
            {
                bitmap = new Bitmap(path);

                ImagePanel.Visible = true;
                ImagePanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
                ImagePanel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
                ImagePanel.Name = "ImagePanel_" + z;
                ImagePanel.Paint += panel_Paint;
            }
            catch (Exception e)
            {
                throw e;
            }

            Name = Path.GetFileNameWithoutExtension(path);
            Z = z;
            
            imageFormatType = ImageFormatType.CutTopLeft;
        }
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, (sender as Panel).ClientRectangle, SrcRect, GraphicsUnit.Pixel);
            if (isSelected)
                e.Graphics.DrawRectangle(selectionPen, (sender as Panel).ClientRectangle);
        }

        public void CalculateSrcRect()
        {
            float scale = Math.Min(bitmap.Width / OriginalRect.Width, bitmap.Height / OriginalRect.Height);
            switch (imageFormatType)
            {
                case ImageFormatType.CutTopLeft:                  
                    SrcRect = new RectangleF(0, 0, OriginalRect.Width * scale, OriginalRect.Height * scale);
                    break;
                case ImageFormatType.CutBotRight:
                    SrcRect = new RectangleF(bitmap.Width - OriginalRect.Width * scale, bitmap.Height - OriginalRect.Height * scale, OriginalRect.Width * scale, OriginalRect.Height * scale);
                    break;
                case ImageFormatType.CutMiddle:
                    SrcRect = new RectangleF(bitmap.Width / 2 - (OriginalRect.Width * scale) / 2, bitmap.Height / 2 - (OriginalRect.Height * scale) / 2, OriginalRect.Width * scale, OriginalRect.Height * scale);
                    break;
                case ImageFormatType.CustomCut:

                    break;
                case ImageFormatType.Stretch:
                    SrcRect = new RectangleF(0, 0, bitmap.Width, bitmap.Height);
                    break;
            }
        }
    }
}
