using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageApp
{
    public static class TilePainter
    {
        private static SolidBrush lightSolidBrush = new SolidBrush(Color.FromArgb(255, 255, 255));
        private static SolidBrush darkSolidBrush = new SolidBrush(Color.FromArgb(238, 238, 238));
        public static void DrawTiles(Graphics graphics, Rectangle rect, int tileSize)
        {
            SolidBrush currBrush;

            for (int i = rect.Y; i < rect.Height; i+= tileSize)
            {
                currBrush = (i / tileSize) % 2 == 0 ? lightSolidBrush : darkSolidBrush;
                for (int j = rect.X; j < rect.Width; j += tileSize)
                {
                    currBrush = currBrush == lightSolidBrush ? darkSolidBrush : lightSolidBrush;
                    graphics.FillRectangle(currBrush, rect.X + j, rect.Y + i, tileSize, tileSize);
                }
            }
        }
    }
}
