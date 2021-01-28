using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageApp.Templates
{
    public class LinearTemplate : ITemplate
    {
        public Rectangle fieldRect;
        public bool inCenter = false;

        public int Rows = 3;
        public int Columns = 3;

        public int BlockWidth = 100;
        public int BlockHeight = 100;
        public int LeftMargin = 0;
        public int BottomMargin = 0;
        public int TotalCells { get { return Rows* Columns; } }
        public float Multiplier;

        private float scaledBlockWidth;
        private float scaledBlockHeight;

        private Pen templatePen = new Pen(Color.Red);

        /* Block region */

        private List<Block> blocks;

        public void InitBlocks()
        {

        }

        /*------------------------------*/

        public void ChangeTemplateProperties(object[] objects)
        {
            Rows = (int)objects[0] != -1 ? (int)objects[0] : Rows;
            Columns = (int)objects[1] != -1 ? (int)objects[1] : Columns;

            BlockWidth = (int)objects[2] != -1 ? (int)objects[2] : BlockWidth;
            BlockHeight = (int)objects[3] != -1 ? (int)objects[3] : BlockHeight;

            LeftMargin = (int)objects[4] != -1 ? (int)objects[4] : LeftMargin;
            BottomMargin = (int)objects[5] != -1 ? (int)objects[5] : BottomMargin;
        }

        public void RearrangeImagesAccordingToTemplate(IEnumerable<ImageInfo> images, Rectangle rect)
        {
            //Preparation
            int totalWidth = Columns* (BlockWidth + LeftMargin);
            int totalHeight = Rows * (BlockHeight + BottomMargin);

            //Calculations
            scaledBlockWidth = BlockWidth;
            scaledBlockHeight = BlockHeight;

            if (totalWidth > rect.Width)
            {
                scaledBlockWidth = rect.Width / Columns;
                scaledBlockWidth -= LeftMargin * scaledBlockWidth / BlockWidth;
            }
            if (totalHeight > rect.Height)
            {
                scaledBlockHeight = rect.Height / Rows;
                scaledBlockHeight -= BottomMargin * scaledBlockHeight / BlockHeight;
            }

            //Final setting
            Multiplier = Math.Min(scaledBlockWidth / BlockWidth, scaledBlockHeight / BlockHeight);
            scaledBlockWidth = BlockWidth * Multiplier;
            scaledBlockHeight = BlockHeight * Multiplier;

            int i = 0, j = 0;
            foreach(ImageInfo image in images)
            {
                if (!image.IsHidden)
                {
                    if (i < Rows)
                    {
                        image.Rect = new RectangleF(j * scaledBlockWidth + j*LeftMargin * Multiplier, i * scaledBlockHeight + i *BottomMargin * Multiplier, scaledBlockWidth, scaledBlockHeight);
                        image.OriginalRect = new RectangleF(j * BlockWidth, i * BlockHeight, BlockWidth, BlockHeight);
                        image.OriginalRectangleShift = new Point(j * LeftMargin, i* BottomMargin);
                        image.ImagePanel.Location = new Point((int)image.Rect.X, (int)image.Rect.Y);
                        image.ImagePanel.Size = new Size((int)image.Rect.Width, (int)image.Rect.Height);

                        image.CalculateSrcRect();

                        image.Visible = true;
                        image.ImagePanel.Visible = true;

                        j = j < Columns - 1 ? j + 1 : 0;
                        i = j > 0 ? i : i + 1;
                    }
                    else
                    {
                        image.Visible = false;
                        image.ImagePanel.Visible = false;
                    }
                    image.ImagePanel.Invalidate();
                }
            }
        }

        public void DrawTemplate(Graphics graphics)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    float x = j * scaledBlockWidth;
                    float y = i * scaledBlockHeight;
                    if (inCenter)
                    {
                        x += fieldRect.Width / 2 - Columns * scaledBlockWidth / 2;
                        y += fieldRect.Width / 2 - Columns * scaledBlockWidth / 2;
                    }
                    graphics.DrawRectangle(templatePen, x, y, scaledBlockWidth, scaledBlockHeight);
                }
            }
        }

        public int GetBlockIndex(Point pos)
        {
            int x = (int)(pos.X / ((BlockWidth + LeftMargin) * Multiplier));
            int y = (int)(pos.Y / ((BlockHeight + BottomMargin) * Multiplier));
            return y * Columns + x;
        }
    }
}
