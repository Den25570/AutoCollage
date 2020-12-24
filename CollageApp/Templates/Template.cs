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
        void RearrangeImagesAccordingToTemplate(IEnumerable<ImageInfo> images, Rectangle rect);
        void DrawTemplate(Graphics graphics);
        int GetBlockIndex(Point pos);

        void ChangeTemplateProperties(object[] properties);

        int TotalCells { get; }
    }
}
