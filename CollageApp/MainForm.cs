using CollageApp.Templates;
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

namespace CollageApp
{
    public partial class MainForm : Form
    {
        private ImageProcessor imageProcessor;
        private LinearTemplate template;
        private bool saved = false;
        private GraphicsState buffer;
        private Pen borderPen;
        private bool notDragging = true;

        public MainForm()
        {
            InitializeComponent();

            imageProcessor = new ImageProcessor();
            template = new LinearTemplate();

            SetInitialValues();
        }

        private void SetInitialValues()
        {
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, this.MainPanel, new object[] { true });
            blockHeightTextBox.Text = "100";
            blockWidthTextBox.Text = "100";
            rowsTextBox.Text = "3";
            columnsTextBox.Text = "3";
            borderPen = new Pen(Color.Blue, 4);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            TilePainter.DrawTiles(e.Graphics, new Rectangle(0,0,100,100), 20);

           /* if (imageProcessor.SelectedImage != null && !notDragging && !saved)
            {
                imageProcessor.RenderImages(e.Graphics, template.TotalCells);             
                saved = true;
            }
            else if (imageProcessor.SelectedImage != null && !notDragging && saved)
            {
                imageProcessor.DrawSavedCollage(e.Graphics);
            }
            else
            {
                imageProcessor.RenderImages(e.Graphics, template.TotalCells);
                saved = false;
            }

            template.DrawTemplate(e.Graphics);
            imageProcessor.DrawSelectedImage(e.Graphics);
            imageProcessor.DrawSelectionRect(e.Graphics);      */ 
        }

        private void MainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            imageProcessor.SelectImage(e.Location, template.TotalCells);
            notDragging = false;
        }

        private void MainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (imageProcessor.SelectedImage != null && !notDragging)
            {
                imageProcessor.SelectedImage.Rect.X = e.X - imageProcessor.RelativeSelectedPoint.X;
                imageProcessor.SelectedImage.Rect.Y = e.Y - imageProcessor.RelativeSelectedPoint.Y;
                MainPanel.Invalidate();
            }
        }

        private void MainPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (imageProcessor.SelectedImage != null && !notDragging)
            {
                Image tmpImage = imageProcessor.SelectedImage;
                if (imageProcessor.PlaceImage(template.GetBlockIndex(e.Location)))
                {
                    imageProcessor.SelectedImage = tmpImage;
                    ImageSelected();
                    notDragging = true;
                }
                updateField();
            }
        }

        private void ImageSelected()
        {
            XCut.Enabled = false;
            YCut.Enabled = false;
            WCut.Enabled = false;
            HCut.Enabled = false;
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
                    XCut.Enabled = true;
                    YCut.Enabled = true;
                    WCut.Enabled = true;
                    HCut.Enabled = true;
                    break;
                case ImageFormatType.Stretch:
                    stretchRadio.Checked = true;
                    break;
            }
            UpdateCutValues();
        }

        private void UpdateCutValues()
        {
            XCut.Value = (decimal)imageProcessor.SelectedImage.SrcRect.X;
            YCut.Value = (decimal)imageProcessor.SelectedImage.SrcRect.Y;
            WCut.Value = (decimal)imageProcessor.SelectedImage.SrcRect.Width;
            HCut.Value = (decimal)imageProcessor.SelectedImage.SrcRect.Height;

            if (imageProcessor.SelectedImage.imageFormatType == ImageFormatType.CustomCut)
            {
                XCut.Enabled = true;
                YCut.Enabled = true;
                WCut.Enabled = true;
                HCut.Enabled = true;
            }
            else
            {
                XCut.Enabled = false;
                YCut.Enabled = false;
                WCut.Enabled = false;
                HCut.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter ="Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF|" +"All files (*.*)|*.*";

            openFileDialog1.Multiselect = true;
            openFileDialog1.Title = "Select Photos";

            DialogResult dr = this.openFileDialog1.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                imageProcessor.ClearImages();
                imageProcessor.LoadAllImages(openFileDialog1.FileNames);

                ImageListView.Items.Clear();
                foreach (Image image in imageProcessor.Images)
                {
                    ImageListView.Items.Add(image.Name);
                }

                imageProcessor.SelectedImage = null;
                updateField();
            }
        }

        private void updateField()
        {
            imageProcessor.Width = template.Columns * template.BlockWidth;
            imageProcessor.Height = template.Rows * template.BlockHeight;

            template.RearrangeImagesAccordingToTemplate(imageProcessor.Images, new Rectangle(0, 0, MainPanel.Width, MainPanel.Height));

            imageProcessor.Multiplier = template.Multiplier;

            if (imageProcessor.SelectedImage != null)
            {
                UpdateCutValues();
            }

            MainPanel.Invalidate();
            previewPanel.Invalidate();
        }

        private void rowsTextBox_ValueChanged(object sender, EventArgs e)
        {
            template.Rows = (int)(rowsTextBox.Value);
            updateField();           
        }

        private void columnsTextBox_ValueChanged(object sender, EventArgs e)
        {
            template.Columns = (int)(columnsTextBox.Value);
            updateField();
        }

        private void blockHeightTextBox_ValueChanged(object sender, EventArgs e)
        {
            template.BlockHeight = (int)(blockHeightTextBox.Value);
            updateField();
        }

        private void blockWidthTextBox_ValueChanged(object sender, EventArgs e)
        {
            template.BlockWidth = (int)(blockWidthTextBox.Value);
            updateField();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            imageProcessor.SelectImage(ImageListView.SelectedIndex);

            if (imageProcessor.SelectedImage != null)
            {
                ImageSelected();
                notDragging = true;
                MainPanel.Invalidate();
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

        //    selectFilesButton.SetBounds(ImagePreviewPanel.Location.X + margin /2, selectFilesButton.Location.Y, ImagePreviewPanel.Width - margin, selectFilesButton.Height);

            MainPanel.SetBounds(margin, margin * 3, ClientRectangle.Width - ImagePreviewPanel.Width - margin * 3, ClientRectangle.Height - margin * 4);

            imageListBox.SetBounds(templateBox.Location.X, templateBox.Location.Y + templateBox.Height + margin / 2, templateBox.Width, selectFilesButton.Location.Y - (templateBox.Location.Y + templateBox.Height + margin));
            ImageListView.Height = imageListBox.Height - margin * 2;

            updateField();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {}

        private void templateBox_Enter(object sender, EventArgs e)
        {

        }

        private void previewPanel_Paint(object sender, PaintEventArgs e)
        {
            if (imageProcessor.SelectedImage != null)
            {
                imageProcessor.RenderPreview(e.Graphics, previewPanel.ClientRectangle);
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (imageProcessor.SelectedImage != null)
            {
                imageProcessor.SelectedImage.imageFormatType = ImageFormatType.Stretch;
                updateField();
            }
        }

        private void cutTopLeftRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (imageProcessor.SelectedImage != null)
            {
                imageProcessor.SelectedImage.imageFormatType = ImageFormatType.CutTopLeft;
                updateField();
            }
        }

        private void cutRightBottomRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (imageProcessor.SelectedImage != null)
            {
                imageProcessor.SelectedImage.imageFormatType = ImageFormatType.CutBotRight;
                updateField();
            }
        }

        private void cutMiddleRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (imageProcessor.SelectedImage != null)
            {
                imageProcessor.SelectedImage.imageFormatType = ImageFormatType.CutMiddle;
                updateField();
            }
        }

        private void cutCustomRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (imageProcessor.SelectedImage != null)
            {
                imageProcessor.SelectedImage.imageFormatType = ImageFormatType.CustomCut;
                updateField();
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

        ////////////////////////////////////////////////////////////////////////////////
        private void previewPanel_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void previewPanel_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void previewPanel_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void XCut_ValueChanged(object sender, EventArgs e)
        {
            if (imageProcessor.SelectedImage != null && imageProcessor.SelectedImage.imageFormatType == ImageFormatType.CustomCut)
            {
                imageProcessor.SelectedImage.SrcRect.X = (int)XCut.Value;
                updateField();
            }
        }

        private void WCut_ValueChanged(object sender, EventArgs e)
        {
            if (imageProcessor.SelectedImage != null && imageProcessor.SelectedImage.imageFormatType == ImageFormatType.CustomCut)
            {
                imageProcessor.SelectedImage.SrcRect.Width = (int)WCut.Value;
                updateField();
            }
        }

        private void YCut_ValueChanged(object sender, EventArgs e)
        {
            if (imageProcessor.SelectedImage != null && imageProcessor.SelectedImage.imageFormatType == ImageFormatType.CustomCut)
            {
                imageProcessor.SelectedImage.SrcRect.Y = (int)YCut.Value;
                updateField();
            }
        }

        private void HCut_ValueChanged(object sender, EventArgs e)
        {
            if (imageProcessor.SelectedImage != null && imageProcessor.SelectedImage.imageFormatType == ImageFormatType.CustomCut)
            {
                imageProcessor.SelectedImage.SrcRect.Height = (int)HCut.Value;
                updateField();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
