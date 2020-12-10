using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollageApp
{
    public interface ITemplate
    {
        void RearrangeImagesAccordingToTemplate(IEnumerable<Image> images, Rectangle rect);
        void DrawTemplate(Graphics graphics);
        int GetBlockIndex(Point pos);

        int TotalCells { get; }
    }
}
