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
using System.Diagnostics;

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
        private Dictionary<RadioButton, ImageFormatType> radioButtonAssociations = new Dictionary<RadioButton, ImageFormatType>();

        //Drawing stuff       
        public ImageInfo SelectedImage { get { return imageProcessor.SelectedImage; } }
        private bool imageStateChanged = false;
        public LinearTemplate template = new LinearTemplate();
        public ImageProcessor imageProcessor;
        private GraphicsState buffer;
        private bool blockControlUpdating;
        private float thumbnailMultiplier;

        //Pens/Brushes/Colors
        private Pen borderPen = new Pen(Color.Blue, 4);
        private Color lightGreenSemiTransparent = Color.FromArgb(120, Color.LightGreen.R, Color.LightGreen.G, Color.LightGreen.B);
        private Color backgroundColor = Color.Transparent;


        public MainForm()
        {
            blockControlUpdating = true;

            InitializeComponent();
            SetInitialValues();

            log.Info("Program startup.");

            blockControlUpdating = false;
        }

        private void SetInitialValues()
        {
            imageProcessor = new ImageProcessor(log, template);

            Settings setting = Serializer.LoadSettings();

            if (setting != null)
            {
                template.BlockWidth = setting.BlockWidth;
                template.BlockHeight = setting.BlockHeight;
                template.LeftMargin = setting.LeftMargin;
                template.BottomMargin = setting.BottomMargin;
                backgroundColor = setting.BackgroundColor;
                
            }

            blockHeightTextBox.Value = template.BlockHeight;
            blockWidthTextBox.Value = template.BlockWidth;
            leftMarginEdit.Value = template.LeftMargin;
            bottomMarginEdit.Value = template.BottomMargin;
            rowsTextBox.Value = 3;
            columnsTextBox.Value = 3;


            //Controls
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, this.MainPanel, new object[] { true });
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, this.ThumbnailImageSelectionPanel, new object[] { true });
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, this.previewPanel, new object[] { true });
            ThumbnailImageSelectionPanel.BackColor = lightGreenSemiTransparent;
            backgroundColorPreview.BackColor = backgroundColor;
            imageProcessor.BackgroundBrush = new SolidBrush(backgroundColor);

            //
            radioButtonAssociations.Add(cutTopLeftRadio, ImageFormatType.CutTopLeft);
            radioButtonAssociations.Add(cutRightBottomRadio, ImageFormatType.CutBotRight);
            radioButtonAssociations.Add(cutMiddleRadio, ImageFormatType.CutMiddle);
            radioButtonAssociations.Add(stretchRadio, ImageFormatType.Stretch);
            radioButtonAssociations.Add(cutCustomRadio, ImageFormatType.CustomCut);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            BackgroundPainter.DrawBackground(e.Graphics, MainPanel.ClientRectangle, Color.Transparent);
            imageProcessor.DrawTemplateBackground(e.Graphics);
        }

        private void ImageSelected()
        {
            imageProcessor.SelectedImage.ImagePanel.BringToFront();

            blockControlUpdating = true;
            HideCheckBox.Checked = SelectedImage.IsHidden;
            switch (SelectedImage.imageFormatType)
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
            blockControlUpdating = false;
        }

        private void UpdateCutValues()
        {
            XCut.Value = (decimal)Math.Ceiling(imageProcessor.SelectedImage.SrcRect.X);
            YCut.Value = (decimal)Math.Ceiling(imageProcessor.SelectedImage.SrcRect.Y);
            WCut.Value = (decimal)Math.Ceiling(imageProcessor.SelectedImage.SrcRect.Width);
            HCut.Value = (decimal)Math.Ceiling(imageProcessor.SelectedImage.SrcRect.Height);      
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
                
                template.RearrangeImagesAccordingToTemplate(imageProcessor.Images, MainPanel.ClientRectangle);
                UpdateField(true);
            }
        }

        public void LoadImages(string[] filenames)
        {
            UnloadImages();
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

            appState.AddAction(DelegateEnum.LoadImages, DelegateEnum.UnloadImages, new object[]{ this, filenames }, new object[] { this });

            MainPanel.Invalidate();
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //
                SelectImage(sender);

                //
                ReleaseCapture();
                SendMessage((sender as Control).Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);

                //
                int selectedIndex = imageProcessor.Images.FindIndex(image => image.Name == SelectedImage.Name);
                int placedIndex = template.GetBlockIndex(new Point(e.Location.X + (sender as Control).Location.X, e.Location.Y + (sender as Control).Location.Y));
                SwapImageNames(selectedIndex, placedIndex);
                imageProcessor.PlaceImage(placedIndex);
                template.RearrangeImagesAccordingToTemplate(imageProcessor.Images, MainPanel.ClientRectangle);
              //  UpdateField(true);
            }
        }

        private void SwapImageNames(int selectedIndex, int placedIndex)
        {
            blockControlUpdating = true;
            var tmp = ImageListView.Items[selectedIndex];
            ImageListView.Items[selectedIndex] = ImageListView.Items[placedIndex];
            ImageListView.Items[placedIndex] = tmp;
            ImageListView.SelectedIndex = placedIndex;
            blockControlUpdating = false;
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
            if (SelectedImage != null)
            {
                if (imageStateChanged)
                {
                    string message = "Параметры изображения не сохранены. Применить изменения?";
                    string caption = "Параметры изображения не сохранены";
                    MessageBoxButtons buttons = MessageBoxButtons.YesNoCancel;
                    DialogResult result = MessageBox.Show(message, caption, buttons);
                    if (result == DialogResult.Yes)
                    {
                        ApplyChangesToImage();
                    }
                    else if (result == DialogResult.No)
                    {
                        imageStateChanged = false;
                        ImageApplyButton.Enabled = false;
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }

            ImageInfo prevImage = SelectedImage;          

            if (sender is ImagePanel)
            {
                imageProcessor.SelectImage(sender as ImagePanel);
                blockControlUpdating = true;
                ImageListView.SelectedIndex = imageProcessor.SelectedIndex;
                blockControlUpdating = false;
            }
            else
            {               
                imageProcessor.SelectImage(imageProcessor.Images[(sender as ListBox).SelectedIndex].ImagePanel);
            }          

            prevImage?.ImagePanel.Invalidate();

            if (SelectedImage != null)
            {
                ImageSelected();
                (sender as Control).Invalidate();
                previewPanel.Invalidate();
            }
            updateSelectionRect();
            //  UpdateField(false);
        }

        public void UnloadImages()
        {
            if (imageProcessor.Images != null && imageProcessor.Images.Count != 0)
            {
                foreach(var image in imageProcessor.Images)
                {
                    MainPanel.Controls.Remove(image.ImagePanel);
                }

                imageProcessor.ClearImages();
            }
            
            ImageListView.Items.Clear();

            imageProcessor.SelectedImage = null;
            ThumbnailImageSelectionPanel.Enabled = ThumbnailImageSelectionPanel.Visible = false;
        }

        private void UpdateField(bool updateAll)
        {
            template.fieldRect = MainPanel.ClientRectangle;
            imageProcessor.fieldRectangle = MainPanel.ClientRectangle;
            imageProcessor.Multiplier = template.Multiplier;

            if (imageProcessor.SelectedImage != null)
            {
                updateSelectionRect();
                SelectedImage.ImagePanel.Invalidate();
            }
            if (updateAll)
                foreach (var image in imageProcessor.Images)
                    image.ImagePanel.Invalidate();


            MainPanel.Invalidate();
            previewPanel.Invalidate();
        }

        public void updateSelectionRect()
        {
            ImageFormatType imageFormatType = radioButtonAssociations[formattingBox.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked)];

            RectangleF srcRect = imageFormatType != ImageFormatType.CustomCut ? SelectedImage.CalculateSrcRect(imageFormatType) : new RectangleF((int)XCut.Value, (int)YCut.Value, (int)WCut.Value, (int)HCut.Value);

            try
            {
                blockControlUpdating = true;
                XCut.Value = (decimal)Math.Ceiling(srcRect.X);
                YCut.Value = (decimal)Math.Ceiling(srcRect.Y);
                WCut.Value = (decimal)Math.Ceiling(srcRect.Width);
                HCut.Value = (decimal)Math.Ceiling(srcRect.Height);
            }
            catch {
                
            }


            ThumbnailImageSelectionPanel.Enabled = ThumbnailImageSelectionPanel.Visible = true;
            thumbnailMultiplier = Math.Min(previewPanel.ClientRectangle.Height / (float)imageProcessor.SelectedImage.bitmap.Height, previewPanel.ClientRectangle.Width / (float)imageProcessor.SelectedImage.bitmap.Width);

            ThumbnailImageSelectionPanel.Location = new Point((int)(srcRect.X * thumbnailMultiplier + previewPanel.ClientRectangle.Width / 2 - imageProcessor.SelectedImage.bitmap.Width * thumbnailMultiplier / 2),
                    (int)(srcRect.Y * thumbnailMultiplier + previewPanel.ClientRectangle.Height / 2 - imageProcessor.SelectedImage.bitmap.Height * thumbnailMultiplier / 2));
            ThumbnailImageSelectionPanel.Size = new Size((int)(srcRect.Width * thumbnailMultiplier),
                    (int)(srcRect.Height * thumbnailMultiplier));

            if (imageFormatType == ImageFormatType.CustomCut)
                XCut.Enabled = YCut.Enabled = WCut.Enabled = HCut.Enabled = true;
            else
                XCut.Enabled = YCut.Enabled = WCut.Enabled = HCut.Enabled = false;

            blockControlUpdating = false;
            imageProcessor.SelectedImage?.ImagePanel.Invalidate();
            previewPanel.Invalidate();
        }

        private void rowsTextBox_ValueChanged(object sender, EventArgs e)
        {
            if (!blockControlUpdating)
            {
                appState.AddAction(DelegateEnum.ChangeFieldProperties, DelegateEnum.ChangeFieldProperties,
                    new object[] { this, new object[] { (int)(rowsTextBox.Value), -1, -1, -1, -1, -1 } }, new object[] { this, new object[] { template.Rows, -1, -1, -1, -1, -1 } });
                template.Rows = (int)(rowsTextBox.Value);

                template.RearrangeImagesAccordingToTemplate(imageProcessor.Images, MainPanel.ClientRectangle);
                UpdateField(true);
            }        
        }

        private void columnsTextBox_ValueChanged(object sender, EventArgs e)
        {
            if (!blockControlUpdating)
            {
                appState.AddAction(DelegateEnum.ChangeFieldProperties, DelegateEnum.ChangeFieldProperties,
                    new object[] { this, new object[] { -1, (int)(columnsTextBox.Value), -1, -1, -1, -1 } }, new object[] { this, new object[] { -1, template.Columns, -1, -1, -1, -1 } });
                template.Columns = (int)(columnsTextBox.Value);

                template.RearrangeImagesAccordingToTemplate(imageProcessor.Images, MainPanel.ClientRectangle);
                UpdateField(true);
            }
        }

        private void blockHeightTextBox_ValueChanged(object sender, EventArgs e)
        {
            if (!blockControlUpdating)
            {
                appState.AddAction(DelegateEnum.ChangeFieldProperties, DelegateEnum.ChangeFieldProperties,
                    new object[] { this, new object[] { -1, -1, -1, (int)(blockHeightTextBox.Value), -1,-1 } }, new object[] { this, new object[] { -1, -1, -1, template.BlockHeight, -1, -1 } });
                template.BlockHeight = (int)(blockHeightTextBox.Value);

                template.RearrangeImagesAccordingToTemplate(imageProcessor.Images, MainPanel.ClientRectangle);
                UpdateField(true);
            }
        }

        private void blockWidthTextBox_ValueChanged(object sender, EventArgs e)
        {
            if (!blockControlUpdating)
            {
                appState.AddAction(DelegateEnum.ChangeFieldProperties, DelegateEnum.ChangeFieldProperties,
                    new object[] { this, new object[] { -1, -1, (int)(blockWidthTextBox.Value), -1, -1, -1 } }, new object[] { this, new object[] { -1, -1, template.BlockWidth, -1, -1, -1 } });
                template.BlockWidth = (int)(blockWidthTextBox.Value);

                template.RearrangeImagesAccordingToTemplate(imageProcessor.Images, MainPanel.ClientRectangle);
                UpdateField(true);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!blockControlUpdating)
            {
                SelectImage(sender);
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

        private int margin = 2;

        private void relocateControls()
        {
            ImagePreviewPanel.Height = ClientRectangle.Height - (menuStrip.Size.Height + margin * 2) ;

            MainPanel.Width += ImagePreviewPanel.Left - MainPanel.Right;
            //MainPanel.SetBounds(TemplatePanel.Right + margin, menuStrip.Size.Height + margin, ClientRectangle.Width - ImagePreviewPanel.Width - margin * 3, ClientRectangle.Height - menuStrip.Size.Height - margin *2);

            //  imageListBox.SetBounds(templateBox.Location.X, templateBox.Location.Y + templateBox.Height + margin / 2, templateBox.Width, selectFilesButton.Location.Y - (templateBox.Location.Y + templateBox.Height) - 10);
            //  ImageListView.Height = imageListBox.Height - margin * 10;
            imageListBox.Height = selectFilesButton.Location.Y - (imageListBox.Location.Y) - 10;

            if (imageProcessor.SelectedImage == null)
                ThumbnailImageSelectionPanel.Enabled = ThumbnailImageSelectionPanel.Visible = false;

            UpdateField(true);
        }

        private void previewPanel_Paint(object sender, PaintEventArgs e)
        {
            if (imageProcessor.SelectedImage != null)
            {
                BackgroundPainter.DrawBackground(e.Graphics, MainPanel.ClientRectangle, Color.Transparent);
                imageProcessor.RenderPreview(e.Graphics, previewPanel.ClientRectangle);
            }

        }

        private void ThumbnailImageSelectionPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (imageProcessor.SelectedImage != null && cutCustomRadio.Checked)
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

            XCut.Value = Math.Max((int)((ThumbnailImageSelectionPanel.Location.X - previewPanel.ClientRectangle.Width / 2 + imageProcessor.SelectedImage.bitmap.Width * thumbnailMultiplier / 2) / thumbnailMultiplier), 0);
            YCut.Value = Math.Max((int)((ThumbnailImageSelectionPanel.Location.Y - previewPanel.ClientRectangle.Height / 2 + imageProcessor.SelectedImage.bitmap.Height * thumbnailMultiplier / 2) /  thumbnailMultiplier), 0);
        }

        private void CloseApp()
        {
            Close();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseApp();
        }

        private void ImageApplyButton_Click(object sender, EventArgs e)
        {
            if (SelectedImage != null)
            {
                ApplyChangesToImage();
            }
        }

        private void ApplyChangesToImage()
        {
            ImageFormatType imageFormatType = radioButtonAssociations[formattingBox.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked)];
            RectangleF rect = new RectangleF((int)XCut.Value, (int)YCut.Value, (int)WCut.Value, (int)HCut.Value);
            bool isHidden = HideCheckBox.Checked;

            appState.AddAction(DelegateEnum.ChangeImageProperties, DelegateEnum.ChangeImageProperties,
                new object[] { this, SelectedImage, rect, imageFormatType, isHidden },
                new object[] { this, SelectedImage, SelectedImage.SrcRect, SelectedImage.imageFormatType, SelectedImage.IsHidden });

            imageProcessor.ChangeImageProperties(SelectedImage, rect, imageFormatType, isHidden);
            imageStateChanged = false;
            ImageApplyButton.Enabled = false;

            UpdateField(false);
        }

        private void cutTopLeftRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (!blockControlUpdating)
            {
                ImageDataUpdated();
                updateSelectionRect();
            }
        }

        private void cutRightBottomRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (!blockControlUpdating)
            {
                ImageDataUpdated();
                updateSelectionRect();
            }
        }

        private void cutMiddleRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (!blockControlUpdating)
            {
                ImageDataUpdated();
                updateSelectionRect();
            }
        }

        private void cutCustomRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (!blockControlUpdating)
            {
                ImageDataUpdated();
                updateSelectionRect();
            }
        }

        private void XCut_ValueChanged(object sender, EventArgs e)
        {
            if (!blockControlUpdating)
            {
                ImageDataUpdated();
                updateSelectionRect();
            }
        }

        private void WCut_ValueChanged(object sender, EventArgs e)
        {
            if (!blockControlUpdating)
            {
                ImageDataUpdated();
                updateSelectionRect();
            }
        }

        private void YCut_ValueChanged(object sender, EventArgs e)
        {
            if (!blockControlUpdating)
            {
                ImageDataUpdated();
                updateSelectionRect();
            }
        }

        private void HCut_ValueChanged(object sender, EventArgs e)
        {
            if (!blockControlUpdating)
            {
                ImageDataUpdated();
                updateSelectionRect();
            }
                
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void UpdateTemplate()
        {
            blockControlUpdating = true;

            blockWidthTextBox.Value = template.BlockWidth;
            blockHeightTextBox.Value = template.BlockHeight;

            rowsTextBox.Value = template.Rows;
            columnsTextBox.Value = template.Columns;

            leftMarginEdit.Value = template.LeftMargin;
            bottomMarginEdit.Value = template.BottomMargin;

            blockControlUpdating = false;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Z && e.Modifiers == Keys.Control) {
                appState.UndoAction();
                UpdateTemplate();
                UpdateField(true);
            }
            else if (e.KeyCode == Keys.Y && e.Modifiers == Keys.Control)
            {
                appState.LoadAction();
                UpdateTemplate();
                UpdateField(true);
            }
        }

        private void HideCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!blockControlUpdating)
            {
                ImageDataUpdated();
            }
        }

        private void ImageDataUpdated()
        {
            imageStateChanged = true;
            ImageApplyButton.Enabled = true;
        }

        private void jpegToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap collage = imageProcessor.GetFullCollage(template.TotalCells);
            SaveCollage(collage, ImageFormat.Jpeg);
        }

        private void pngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap collage = imageProcessor.GetFullCollage(template.TotalCells);
            SaveCollage(collage, ImageFormat.Png);
        }

        private void bmpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap collage = imageProcessor.GetFullCollage(template.TotalCells);
            SaveCollage(collage, ImageFormat.Bmp);
        }

        private static void SaveCollage(Bitmap Collage, ImageFormat imageFormat)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                string fileExtension = imageFormat.ToString().ToLower();
                dialog.Filter = $"Images (*.{fileExtension};)|*.{fileExtension};|" + "All files (*.*)|*.*";
                dialog.Title = "Select path";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string filename = dialog.FileName;
                    if (!dialog.FileName.Contains(fileExtension))
                        filename += fileExtension;
                    Collage.Save(filename, imageFormat);

                    Process photoViewer = new Process();
                    photoViewer.StartInfo.FileName = filename;
                    photoViewer.Start();
                }
            }
            
            
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (!blockControlUpdating)
            {
                appState.AddAction(DelegateEnum.ChangeFieldProperties, DelegateEnum.ChangeFieldProperties,
                    new object[] { this, new object[] { -1, -1, -1, -1, (int)(leftMarginEdit.Value), -1  } }, new object[] { this, new object[] { -1, -1, -1, -1, template.LeftMargin, -1 } });
                template.LeftMargin = (int)(leftMarginEdit.Value);
                UpdateField(true);
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (!blockControlUpdating)
            {
                appState.AddAction(DelegateEnum.ChangeFieldProperties, DelegateEnum.ChangeFieldProperties,
                    new object[] { this, new object[] { -1, -1, -1, -1, -1, (int)(bottomMarginEdit.Value) } }, new object[] { this, new object[] { -1, -1, -1, -1, -1, template.BottomMargin } });
                template.BottomMargin = (int)(bottomMarginEdit.Value);
                UpdateField(true);
            }
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    backgroundColor = colorDialog.Color;                   
                    imageProcessor.BackgroundBrush = new SolidBrush(colorDialog.Color);
                    backgroundColorPreview.Invalidate();
                    MainPanel.Invalidate();
                }
            }
        }

        private void backgroundColorPreview_Paint(object sender, PaintEventArgs e)
        {
            BackgroundPainter.DrawBackground(e.Graphics, MainPanel.ClientRectangle, backgroundColor);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Serializer.SaveSettings(new Settings(template, backgroundColor));
        }
    }
}
