using CollageApp.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CollageApp.State
{
    [Serializable]
    public class Settings
    {
        public int BlockWidth;
        public int BlockHeight;
        public int LeftMargin;
        public int BottomMargin;

        public Color BackgroundColor { 
            get {
                return Color.FromArgb(BackgroundColorARGB);
            } }
        public int BackgroundColorARGB;

        public Settings() { }
        public Settings(LinearTemplate template, Color bgColor)
        {
            BlockWidth = template.BlockWidth;
            BlockHeight = template.BlockHeight;
            LeftMargin = template.LeftMargin;
            BottomMargin = template.BottomMargin;
            BackgroundColorARGB = bgColor.ToArgb();
        }
    }
}
