using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class Image
    {
        public RectangleF OriginalRect;
        public RectangleF Rect;
        public RectangleF SrcRect;
        public ImageFormatType imageFormatType;
        public int Z;
        public string Name;

        public Bitmap bitmap;

        public Image(string path, int z)
        {
            try
            {
                bitmap = new Bitmap(path);
            }
            catch (Exception e)
            {
                throw e;
            }

            Name = Path.GetFileNameWithoutExtension(path);
            this.Z = z;
            Rect.Width = bitmap.Width;
            Rect.Height = bitmap.Height;
            Rect.X = 0;
            Rect.Y = 0;
            
            imageFormatType = ImageFormatType.CutTopLeft;
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
