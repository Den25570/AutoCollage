using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CollageApp.Image
{
    public class ImagePanel : Panel
    {
        public ImageInfo AssociatedImage;

        public ImagePanel(ImageInfo associatedImage)
        {
            DoubleBuffered = true;
            AssociatedImage = associatedImage;
        }
    }
}
