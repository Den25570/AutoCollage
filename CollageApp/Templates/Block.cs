using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageApp.Templates
{
    public class Block
    {
        public Rectangle rectangle;
        public BlockUnion blockUnion;
        public int X {get {return rectangle.X;} }
        public int Y { get { return rectangle.Y; } }
        public int Width { get { return rectangle.Width; } }
        public int Height { get { return rectangle.Height; } }

    }
}
