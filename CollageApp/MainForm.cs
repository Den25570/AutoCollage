using CollageApp.State;
using CollageApp.Templates;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CollageApp.Image;

namespace CollageApp
{
    public partial class MainForm : Form
    {
        //Unmanaged code for panel dragging
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        //Utils
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private AppState appState = new AppState();

        //Drawing stuff
        private ImageProcessor imageProcessor = new ImageProcessor(log);
        private LinearTemplate template;
        private GraphicsState buffer;
        private Pen borderPen;
        private bool blockControlUpdating;
        private float thumbnailMultiplier;

        //
        private List<Panel> panels = new List<Panel>();

        //Pens/Brushes/Colors
        Color lightGreenSemiTransparent = Color.FromArgb(120, Color.LightGreen.R, Color.LightGreen.G, Color.LightGreen.B);


        public MainForm()
        {
            InitializeComponent();

            //ToDo: make selectable
            template = new LinearTemplate();

            SetInitialValues();

            log.Info("Program startup.");
        }

        private void SetInitialValues()
        {
            //Default image/template settings           
            blockHeightTextBox.Text = "100";
            blockWidthTextBox.Text = "100";
            rowsTextBox.Text = "3";
            columnsTextBox.Text = "3";
            borderPen = new Pen(Color.Blue, 4);

            //Controls
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, this.MainPanel, new object[] { true });
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, this.ThumbnailImageSelectionPanel, new object[] { true });
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, this.previewPanel, new object[] { true });
            ThumbnailImageSelectionPanel.BackColor = lightGreenSemiTransparent;

            //PrepearingState
            // appState.AddDelegate(DelegateEnum.LoadImages, LoadImages);
            // appState.AddDelegate(DelegateEnum.LoadImages, LoadImages);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            TilePainter.DrawTiles(e.Graphics, MainPanel.ClientRectangle, 20); 
        }

        private void ImageSelected()
        {
            switch (imageProcessor.SelectedImage.imageFormatType)
            {
                case ImageFormatType.CutTopLeft:
                    cutTopLeftRadio.Checked = true;
                    break;
                case ImageFormatType.CutBotRight:
                    cutRightBottomRadio.Checked = true;
                    break;
                case ImageFormatType.CutMiddle:
                    cutMiddleRadio.Checked = true;
                    break;
                case ImageFormatType.CustomCut:
                    cutCustomRadio.Checked = true;
                    break;
                case ImageFormatType.Stretch:
                    stretchRadio.Checked = true;
                    break;
            }
            UpdateCutValues();
        }

        private void UpdateCutValues()
        {
            XCut.Value = (decimal)Math.Ceiling(imageProcessor.SelectedImage.SrcRect.X);
            YCut.Value = (decimal)Math.Ceiling(imageProcessor.SelectedImage.SrcRect.Y);
            WCut.Value = (decimal)Math.Ceiling(imageProcessor.SelectedImage.SrcRect.Width);
            HCut.Value = (decimal)Math.Ceiling(imageProcessor.SelectedImage.SrcRect.Height);

            if (imageProcessor.SelectedImage.imageFormatType == ImageFormatType.CustomCut)
                XCut.Enabled = YCut.Enabled = WCut.Enabled = HCut.Enabled = true;
            else
                XCut.Enabled = YCut.Enabled = WCut.Enabled = HCut.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter ="Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF|" +"All files (*.*)|*.*";
            openFileDialog1.Multiselect = true;
            openFileDialog1.Title = "Select Photos";

            DialogResult dr = this.openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                LoadImages(openFileDialog1.FileNames);
                updateField(true);
            }
        }

        private void LoadImages(string[] filenames)
        {
            imageProcessor.ClearImages();
            imageProcessor.LoadAllImages(openFileDialog1.FileNames);

            ImageListView.Items.Clear();
            foreach (ImageInfo image in imageProcessor.Images)
            {
                ImageListView.Items.Add(image.Name);

                image.ImagePanel.MouseClick += panel_MouseClick;
                image.ImagePanel.MouseMove += panel_MouseMove;

                MainPanel.Controls.Add(image.ImagePanel);
            }

            imageProcessor.SelectedImage = null;
            ThumbnailImageSelectionPanel.Enabled = ThumbnailImageSelectionPanel.Visible = false;
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SelectImage(sender);

                ReleaseCapture();
                SendMessage((sender as Control).Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);

                imageProcessor.PlaceImage(template.GetBlockIndex(new Point(e.Location.X + (sender as Control).Location.X, e.Location.Y + (sender as Control).Location.Y)));
                template.RearrangeImagesAccordingToTemplate(imageProcessor.Images, MainPanel.ClientRectangle);

                updateField(true);
            }
        }

        private void UnSelectImage()
        {
            ImageInfo prevImage = imageProcessor.SelectedImage;

            imageProcessor.UnSelectImage();

            prevImage?.ImagePanel.Invalidate();
            ThumbnailImageSelectionPanel.Enabled = ThumbnailImageSelectionPanel.Visible = false;
            ThumbnailImageSelectionPanel.Invalidate();
        }

        private void panel_MouseClick(object sender, MouseEventArgs e)
        {
            SelectImage(sender);
        }

        private void SelectImage(object sender)
        {
            ImageInfo prevImage = imageProcessor.SelectedImage;

            imageProcessor.SelectImage(sender as ImagePanel);

            prevImage?.ImagePanel.Invalidate();

            if (imageProcessor.SelectedImage != null)
            {
                ImageSelected();
                (sender as Control).Invalidate();
                previewPanel.Invalidate();
            }
        }

        private void UnloadImages()
        {
            imageProcessor.ClearImages();
            ImageListView.Items.Clear();
            panels.Clear();
            imageProcessor.SelectedImage = null;
            ThumbnailImageSelectionPanel.Enabled = ThumbnailImageSelectionPanel.Visible = false;
        }

        private void updateField(bool updateAll)
        {
            imageProcessor.Width = template.Columns * template.BlockWidth;
            imageProcessor.Height = template.Rows * template.BlockHeight;
            template.fieldRect = MainPanel.ClientRectangle;
            imageProcessor.fieldRectangle = MainPanel.ClientRectangle;

            template.RearrangeImagesAccordingToTemplate(imageProcessor.Images, MainPanel.ClientRectangle);

            imageProcessor.Multiplier = template.Multiplier;

            if (imageProcessor.SelectedImage != null)
            {
                ThumbnailImageSelectionPanel.Enabled = ThumbnailImageSelectionPanel.Visible = true;
                thumbnailMultiplier = Math.Min(previewPanel.ClientRectangle.Height / (float)imageProcessor.SelectedImage.bitmap.Height, previewPanel.ClientRectangle.Width / (float)imageProcessor.SelectedImage.bitmap.Width);
                ThumbnailImageSelectionPanel.Location = new Point((int)(imageProcessor.SelectedImage.SrcRect.X * thumbnailMultiplier + previewPanel.ClientRectangle.Width / 2 - imageProcessor.SelectedImage.bitmap.Width * thumbnailMultiplier / 2),
                        (int)(imageProcessor.SelectedImage.SrcRect.Y * thumbnailMultiplier + previewPanel.ClientRectangle.Height / 2 - imageProcessor.SelectedImage.bitmap.Height * thumbnailMultiplier / 2));
                ThumbnailImageSelectionPanel.Size = new Size((int)(imageProcessor.SelectedImage.SrcRect.Width * thumbnailMultiplier),
                        (int)(imageProcessor.SelectedImage.SrcRect.Height * thumbnailMultiplier));

                UpdateCutValues();

                
                
            }
            if (updateAll)
                foreach (var image in imageProcessor.Images)
                    image.ImagePanel.Invalidate();
            else
                imageProcessor.SelectedImage?.ImagePanel.Invalidate();
            previewPanel.Invalidate();
        }

        private void rowsTextBox_ValueChanged(object sender, EventArgs e)
        {
            template.Rows = (int)(rowsTextBox.Value);
            updateField(true);           
        }

        private void columnsTextBox_ValueChanged(object sender, EventArgs e)
        {
            template.Columns = (int)(columnsTextBox.Value);
            updateField(true);
        }

        private void blockHeightTextBox_ValueChanged(object sender, EventArgs e)
        {
            template.BlockHeight = (int)(blockHeightTextBox.Value);
            updateField(true);
        }

        private void blockWidthTextBox_ValueChanged(object sender, EventArgs e)
        {
            template.BlockWidth = (int)(blockWidthTextBox.Value);
            updateField(true);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            imageProcessor.SelectImage(ImageListView.SelectedIndex);

            if (imageProcessor.SelectedImage != null)
            {
                ImageSelected();
                previewPanel.Invalidate();
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            relocateControls();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            relocateControls();
        }

        private int margin = 10;

        private void relocateControls()
        {
            ImagePreviewPanel.Height = ClientRectangle.Height;
            MainPanel.SetBounds(margin, margin * 3, ClientRectangle.Width - ImagePreviewPanel.Width - margin * 3, ClientRectangle.Height - margin * 4);
            imageListBox.SetBounds(templateBox.Location.X, templateBox.Location.Y + templateBox.Height + margin / 2, templateBox.Width, selectFilesButton.Location.Y - (templateBox.Location.Y + templateBox.Height + margin));
            ImageListView.Height = imageListBox.Height - margin * 2;

            if (imageProcessor.SelectedImage == null)
                ThumbnailImageSelectionPanel.Enabled = ThumbnailImageSelectionPanel.Visible = false;

            updateField(true);
        }

        private void previewPanel_Paint(object sender, PaintEventArgs e)
        {
            if (imageProcessor.SelectedImage != null)
            {
                TilePainter.DrawTiles(e.Graphics, previewPanel.ClientRectangle, 20);
                imageProcessor.RenderPreview(e.Graphics, previewPanel.ClientRectangle);
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (imageProcessor.SelectedImage != null && !blockControlUpdating)
            {
                imageProcessor.SelectedImage.imageFormatType = ImageFormatType.Stretch;
                updateField(false);
            }
        }

        private void cutTopLeftRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (imageProcessor.SelectedImage != null && !blockControlUpdating)
            {
                imageProcessor.SelectedImage.imageFormatType = ImageFormatType.CutTopLeft;
                updateField(false);
            }
        }

        private void cutRightBottomRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (imageProcessor.SelectedImage != null && !blockControlUpdating)
            {
                imageProcessor.SelectedImage.imageFormatType = ImageFormatType.CutBotRight;
                updateField(false);
            }
        }

        private void cutMiddleRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (imageProcessor.SelectedImage != null && !blockControlUpdating)
            {
                imageProcessor.SelectedImage.imageFormatType = ImageFormatType.CutMiddle;
                updateField(false);
            }
        }

        private void cutCustomRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (imageProcessor.SelectedImage != null && !blockControlUpdating)
            {
                imageProcessor.SelectedImage.imageFormatType = ImageFormatType.CustomCut;
                updateField(false);
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap Collage = imageProcessor.GetFullCollage(template.TotalCells);

            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string filename = dialog.FileName;
                if (!dialog.FileName.Contains(".jpeg"))
                    filename += ".jpeg";
                Collage.Save(filename, ImageFormat.Jpeg);
            }
        }

        private void XCut_ValueChanged(object sender, EventArgs e)
        {
            if (imageProcessor.SelectedImage != null && imageProcessor.SelectedImage.imageFormatType == ImageFormatType.CustomCut && !blockControlUpdating)
            {
                imageProcessor.SelectedImage.SrcRect.X = (int)XCut.Value;
                updateField(false);
            }
        }

        private void WCut_ValueChanged(object sender, EventArgs e)
        {
            if (imageProcessor.SelectedImage != null && imageProcessor.SelectedImage.imageFormatType == ImageFormatType.CustomCut && !blockControlUpdating)
            {
                imageProcessor.SelectedImage.SrcRect.Width = (int)WCut.Value;
                updateField(false);
            }
        }

        private void YCut_ValueChanged(object sender, EventArgs e)
        {
            if (imageProcessor.SelectedImage != null && imageProcessor.SelectedImage.imageFormatType == ImageFormatType.CustomCut && !blockControlUpdating)
            {
                imageProcessor.SelectedImage.SrcRect.Y = (int)YCut.Value;
                updateField(false);
            }
        }

        private void HCut_ValueChanged(object sender, EventArgs e)
        {
            if (imageProcessor.SelectedImage != null && imageProcessor.SelectedImage.imageFormatType == ImageFormatType.CustomCut && !blockControlUpdating)
            {
                imageProcessor.SelectedImage.SrcRect.Height = (int)HCut.Value;
                updateField(false);
            }
        }

        private void ThumbnailImageSelectionPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (imageProcessor.SelectedImage != null && imageProcessor.SelectedImage.imageFormatType == ImageFormatType.CustomCut)
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage((sender as Control).Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);

                    UpdateThumbnailCutPos();
                }
            }           
        }

        private void UpdateThumbnailCutPos()
        {
            ThumbnailImageSelectionPanel.Location = new Point(
                       Math.Min(Math.Max(imageProcessor.SelectedImageRect.X, ThumbnailImageSelectionPanel.Location.X), imageProcessor.SelectedImageRect.X + imageProcessor.SelectedImageRect.Width - ThumbnailImageSelectionPanel.Size.Width),
                       Math.Min(Math.Max(imageProcessor.SelectedImageRect.Y, ThumbnailImageSelectionPanel.Location.Y), imageProcessor.SelectedImageRect.Y + imageProcessor.SelectedImageRect.Height - ThumbnailImageSelectionPanel.Size.Height));
            
            imageProcessor.SelectedImage.SrcRect.Location = new Point(
                Math.Max((int)((ThumbnailImageSelectionPanel.Location.X - previewPanel.ClientRectangle.Width / 2 + imageProcessor.SelectedImage.bitmap.Width * thumbnailMultiplier / 2) / thumbnailMultiplier), 0),
                Math.Max((int)((ThumbnailImageSelectionPanel.Location.Y - previewPanel.ClientRectangle.Height / 2 + imageProcessor.SelectedImage.bitmap.Height * thumbnailMultiplier / 2) /  thumbnailMultiplier), 0));

            updateField(false);
        }
    }
}
