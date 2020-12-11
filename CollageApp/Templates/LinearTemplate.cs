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
        public int TotalCells { get { return Rows* Columns; } }
        public float Multiplier;

        private float blockWidth;
        private float blockHeight;

        private Pen templatePen = new Pen(Color.White);
        private List<Block> blocks;

        public void InitBlocks()
        {

        }

        public void RearrangeImagesAccordingToTemplate(IEnumerable<Image> images, Rectangle rect)
        {
            blockWidth = Columns * BlockWidth >= rect.Width ? rect.Width / Columns : BlockWidth;
            blockHeight = Rows * BlockHeight >= rect.Height ? rect.Height / Rows : BlockHeight;

            Multiplier = Math.Min(blockWidth / BlockWidth, blockHeight / BlockHeight);

            blockWidth = BlockWidth * Multiplier;
            blockHeight = BlockHeight * Multiplier;

            int i = 0, j = 0;
            foreach(Image image in images)
            {
                image.Rect.X = j * blockWidth;
                image.Rect.Y = i * blockHeight;
                image.Rect.Width = blockWidth;
                image.Rect.Height = blockHeight;

                image.OriginalRect.X = j * BlockWidth;
                image.OriginalRect.Y = i * BlockHeight;
                image.OriginalRect.Width = BlockWidth;
                image.OriginalRect.Height = BlockHeight;

                image.CalculateSrcRect();

                j = j < Columns - 1 ? j + 1 : 0;
                i = j > 0 ? i : i + 1;

                if (i >= Rows)
                {
                    break;
                }
            }
        }

        public void DrawTemplate(Graphics graphics)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    float x = j * blockWidth;
                    float y = i * blockHeight;
                    if (inCenter)
                    {
                        x += fieldRect.Width / 2 - Columns * blockWidth / 2;
                        y += fieldRect.Width / 2 - Columns * blockWidth / 2;
                    }
                    graphics.DrawRectangle(templatePen, x, y, blockWidth, blockHeight);
                }
            }
        }

        public int GetBlockIndex(Point pos)
        {
            int x = (int)(pos.X / (BlockWidth * Multiplier));
            int y = (int)(pos.Y / (BlockHeight * Multiplier));
            return y * Columns + x;
        }
    }
}
