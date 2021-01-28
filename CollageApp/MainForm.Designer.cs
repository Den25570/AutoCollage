using System.Reflection;
using System.Windows.Forms;

namespace CollageApp
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainPanel = new System.Windows.Forms.Panel();
            this.ImagePreviewPanel = new System.Windows.Forms.Panel();
            this.formattingBox = new System.Windows.Forms.GroupBox();
            this.HideCheckBox = new System.Windows.Forms.CheckBox();
            this.ImageApplyButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.WCut = new System.Windows.Forms.NumericUpDown();
            this.XCut = new System.Windows.Forms.NumericUpDown();
            this.YCut = new System.Windows.Forms.NumericUpDown();
            this.HCut = new System.Windows.Forms.NumericUpDown();
            this.stretchRadio = new System.Windows.Forms.RadioButton();
            this.cutCustomRadio = new System.Windows.Forms.RadioButton();
            this.cutMiddleRadio = new System.Windows.Forms.RadioButton();
            this.cutRightBottomRadio = new System.Windows.Forms.RadioButton();
            this.cutTopLeftRadio = new System.Windows.Forms.RadioButton();
            this.imageListBox = new System.Windows.Forms.GroupBox();
            this.ImageListView = new System.Windows.Forms.ListBox();
            this.previewBox = new System.Windows.Forms.GroupBox();
            this.previewPanel = new System.Windows.Forms.Panel();
            this.ThumbnailImageSelectionPanel = new System.Windows.Forms.Panel();
            this.selectFilesButton = new System.Windows.Forms.Button();
            this.blockHeightTextBox = new System.Windows.Forms.NumericUpDown();
            this.blockWidthTextBox = new System.Windows.Forms.NumericUpDown();
            this.templateBox = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.backgroundColorPreview = new System.Windows.Forms.Panel();
            this.ColorButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.bottomMarginEdit = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.leftMarginEdit = new System.Windows.Forms.NumericUpDown();
            this.columnsTextBox = new System.Windows.Forms.NumericUpDown();
            this.rowsTextBox = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.rowLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jpegToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bmpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.шаблонToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выровнятьВсеИзображенияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поЦентруToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сверхуСлеваToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справаСнизуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.растянутьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.изображенияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.экспортироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TemplatePanel = new System.Windows.Forms.Panel();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.ImagePreviewPanel.SuspendLayout();
            this.formattingBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WCut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.XCut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YCut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HCut)).BeginInit();
            this.imageListBox.SuspendLayout();
            this.previewBox.SuspendLayout();
            this.previewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blockHeightTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blockWidthTextBox)).BeginInit();
            this.templateBox.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bottomMarginEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftMarginEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnsTextBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowsTextBox)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.TemplatePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MainPanel.AutoScroll = true;
            this.MainPanel.AutoSize = true;
            this.MainPanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.MainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainPanel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.MainPanel.Location = new System.Drawing.Point(250, 27);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(847, 634);
            this.MainPanel.TabIndex = 0;
            this.MainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // ImagePreviewPanel
            // 
            this.ImagePreviewPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ImagePreviewPanel.AutoSize = true;
            this.ImagePreviewPanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ImagePreviewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ImagePreviewPanel.Controls.Add(this.formattingBox);
            this.ImagePreviewPanel.Controls.Add(this.imageListBox);
            this.ImagePreviewPanel.Controls.Add(this.previewBox);
            this.ImagePreviewPanel.Controls.Add(this.selectFilesButton);
            this.ImagePreviewPanel.Location = new System.Drawing.Point(1098, 27);
            this.ImagePreviewPanel.Name = "ImagePreviewPanel";
            this.ImagePreviewPanel.Size = new System.Drawing.Size(345, 633);
            this.ImagePreviewPanel.TabIndex = 1;
            // 
            // formattingBox
            // 
            this.formattingBox.Controls.Add(this.HideCheckBox);
            this.formattingBox.Controls.Add(this.ImageApplyButton);
            this.formattingBox.Controls.Add(this.label7);
            this.formattingBox.Controls.Add(this.label6);
            this.formattingBox.Controls.Add(this.label5);
            this.formattingBox.Controls.Add(this.label4);
            this.formattingBox.Controls.Add(this.WCut);
            this.formattingBox.Controls.Add(this.XCut);
            this.formattingBox.Controls.Add(this.YCut);
            this.formattingBox.Controls.Add(this.HCut);
            this.formattingBox.Controls.Add(this.stretchRadio);
            this.formattingBox.Controls.Add(this.cutCustomRadio);
            this.formattingBox.Controls.Add(this.cutMiddleRadio);
            this.formattingBox.Controls.Add(this.cutRightBottomRadio);
            this.formattingBox.Controls.Add(this.cutTopLeftRadio);
            this.formattingBox.Location = new System.Drawing.Point(212, 328);
            this.formattingBox.Name = "formattingBox";
            this.formattingBox.Size = new System.Drawing.Size(119, 286);
            this.formattingBox.TabIndex = 13;
            this.formattingBox.TabStop = false;
            this.formattingBox.Text = "Форматирование изображения";
            // 
            // HideCheckBox
            // 
            this.HideCheckBox.AutoSize = true;
            this.HideCheckBox.Location = new System.Drawing.Point(5, 150);
            this.HideCheckBox.Name = "HideCheckBox";
            this.HideCheckBox.Size = new System.Drawing.Size(64, 17);
            this.HideCheckBox.TabIndex = 22;
            this.HideCheckBox.Text = "Скрыть";
            this.HideCheckBox.UseVisualStyleBackColor = true;
            this.HideCheckBox.CheckedChanged += new System.EventHandler(this.HideCheckBox_CheckedChanged);
            // 
            // ImageApplyButton
            // 
            this.ImageApplyButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ImageApplyButton.Location = new System.Drawing.Point(6, 256);
            this.ImageApplyButton.Name = "ImageApplyButton";
            this.ImageApplyButton.Size = new System.Drawing.Size(103, 23);
            this.ImageApplyButton.TabIndex = 21;
            this.ImageApplyButton.Text = "Применить";
            this.ImageApplyButton.UseVisualStyleBackColor = false;
            this.ImageApplyButton.Click += new System.EventHandler(this.ImageApplyButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(54, 214);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Высота";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.Location = new System.Drawing.Point(54, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Ширина";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(3, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Y";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(3, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "X";
            // 
            // WCut
            // 
            this.WCut.Enabled = false;
            this.WCut.Location = new System.Drawing.Point(57, 186);
            this.WCut.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.WCut.Name = "WCut";
            this.WCut.Size = new System.Drawing.Size(52, 20);
            this.WCut.TabIndex = 17;
            this.WCut.ValueChanged += new System.EventHandler(this.WCut_ValueChanged);
            // 
            // XCut
            // 
            this.XCut.Enabled = false;
            this.XCut.Location = new System.Drawing.Point(6, 186);
            this.XCut.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.XCut.Name = "XCut";
            this.XCut.Size = new System.Drawing.Size(42, 20);
            this.XCut.TabIndex = 16;
            this.XCut.ValueChanged += new System.EventHandler(this.XCut_ValueChanged);
            // 
            // YCut
            // 
            this.YCut.Enabled = false;
            this.YCut.Location = new System.Drawing.Point(6, 230);
            this.YCut.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.YCut.Name = "YCut";
            this.YCut.Size = new System.Drawing.Size(42, 20);
            this.YCut.TabIndex = 15;
            this.YCut.ValueChanged += new System.EventHandler(this.YCut_ValueChanged);
            // 
            // HCut
            // 
            this.HCut.Enabled = false;
            this.HCut.Location = new System.Drawing.Point(57, 230);
            this.HCut.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.HCut.Name = "HCut";
            this.HCut.Size = new System.Drawing.Size(52, 20);
            this.HCut.TabIndex = 14;
            this.HCut.ValueChanged += new System.EventHandler(this.HCut_ValueChanged);
            // 
            // stretchRadio
            // 
            this.stretchRadio.AutoSize = true;
            this.stretchRadio.Location = new System.Drawing.Point(5, 99);
            this.stretchRadio.Name = "stretchRadio";
            this.stretchRadio.Size = new System.Drawing.Size(77, 17);
            this.stretchRadio.TabIndex = 4;
            this.stretchRadio.TabStop = true;
            this.stretchRadio.Text = "Растянуть";
            this.stretchRadio.UseVisualStyleBackColor = true;
            // 
            // cutCustomRadio
            // 
            this.cutCustomRadio.AutoSize = true;
            this.cutCustomRadio.Location = new System.Drawing.Point(5, 122);
            this.cutCustomRadio.Name = "cutCustomRadio";
            this.cutCustomRadio.Size = new System.Drawing.Size(93, 17);
            this.cutCustomRadio.TabIndex = 3;
            this.cutCustomRadio.TabStop = true;
            this.cutCustomRadio.Text = "Произвольно";
            this.cutCustomRadio.UseVisualStyleBackColor = true;
            this.cutCustomRadio.CheckedChanged += new System.EventHandler(this.cutCustomRadio_CheckedChanged);
            // 
            // cutMiddleRadio
            // 
            this.cutMiddleRadio.AutoSize = true;
            this.cutMiddleRadio.Location = new System.Drawing.Point(6, 76);
            this.cutMiddleRadio.Name = "cutMiddleRadio";
            this.cutMiddleRadio.Size = new System.Drawing.Size(76, 17);
            this.cutMiddleRadio.TabIndex = 2;
            this.cutMiddleRadio.TabStop = true;
            this.cutMiddleRadio.Text = "По центру";
            this.cutMiddleRadio.UseVisualStyleBackColor = true;
            this.cutMiddleRadio.CheckedChanged += new System.EventHandler(this.cutMiddleRadio_CheckedChanged);
            // 
            // cutRightBottomRadio
            // 
            this.cutRightBottomRadio.AutoSize = true;
            this.cutRightBottomRadio.Location = new System.Drawing.Point(6, 53);
            this.cutRightBottomRadio.Name = "cutRightBottomRadio";
            this.cutRightBottomRadio.Size = new System.Drawing.Size(97, 17);
            this.cutRightBottomRadio.TabIndex = 1;
            this.cutRightBottomRadio.TabStop = true;
            this.cutRightBottomRadio.Text = "Справа/Снизу";
            this.cutRightBottomRadio.UseVisualStyleBackColor = true;
            this.cutRightBottomRadio.CheckedChanged += new System.EventHandler(this.cutRightBottomRadio_CheckedChanged);
            // 
            // cutTopLeftRadio
            // 
            this.cutTopLeftRadio.AutoSize = true;
            this.cutTopLeftRadio.Location = new System.Drawing.Point(6, 31);
            this.cutTopLeftRadio.Name = "cutTopLeftRadio";
            this.cutTopLeftRadio.Size = new System.Drawing.Size(96, 17);
            this.cutTopLeftRadio.TabIndex = 0;
            this.cutTopLeftRadio.TabStop = true;
            this.cutTopLeftRadio.Text = "Сверху/Слева";
            this.cutTopLeftRadio.UseVisualStyleBackColor = true;
            this.cutTopLeftRadio.CheckedChanged += new System.EventHandler(this.cutTopLeftRadio_CheckedChanged);
            // 
            // imageListBox
            // 
            this.imageListBox.AutoSize = true;
            this.imageListBox.Controls.Add(this.ImageListView);
            this.imageListBox.Location = new System.Drawing.Point(6, 331);
            this.imageListBox.Name = "imageListBox";
            this.imageListBox.Size = new System.Drawing.Size(197, 268);
            this.imageListBox.TabIndex = 12;
            this.imageListBox.TabStop = false;
            this.imageListBox.Text = "Список изображений";
            // 
            // ImageListView
            // 
            this.ImageListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImageListView.FormattingEnabled = true;
            this.ImageListView.Location = new System.Drawing.Point(3, 16);
            this.ImageListView.Name = "ImageListView";
            this.ImageListView.Size = new System.Drawing.Size(191, 249);
            this.ImageListView.TabIndex = 3;
            this.ImageListView.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // previewBox
            // 
            this.previewBox.Controls.Add(this.previewPanel);
            this.previewBox.Location = new System.Drawing.Point(6, 11);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(325, 310);
            this.previewBox.TabIndex = 11;
            this.previewBox.TabStop = false;
            this.previewBox.Text = "Предпросмотр";
            // 
            // previewPanel
            // 
            this.previewPanel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.previewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewPanel.Controls.Add(this.ThumbnailImageSelectionPanel);
            this.previewPanel.Location = new System.Drawing.Point(6, 19);
            this.previewPanel.Name = "previewPanel";
            this.previewPanel.Size = new System.Drawing.Size(313, 285);
            this.previewPanel.TabIndex = 8;
            this.previewPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.previewPanel_Paint);
            // 
            // ThumbnailImageSelectionPanel
            // 
            this.ThumbnailImageSelectionPanel.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ThumbnailImageSelectionPanel.Location = new System.Drawing.Point(50, 65);
            this.ThumbnailImageSelectionPanel.Name = "ThumbnailImageSelectionPanel";
            this.ThumbnailImageSelectionPanel.Size = new System.Drawing.Size(200, 100);
            this.ThumbnailImageSelectionPanel.TabIndex = 0;
            this.ThumbnailImageSelectionPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ThumbnailImageSelectionPanel_MouseMove);
            // 
            // selectFilesButton
            // 
            this.selectFilesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.selectFilesButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.selectFilesButton.Location = new System.Drawing.Point(6, 605);
            this.selectFilesButton.Name = "selectFilesButton";
            this.selectFilesButton.Size = new System.Drawing.Size(197, 23);
            this.selectFilesButton.TabIndex = 2;
            this.selectFilesButton.Text = "Выбрать директорию";
            this.selectFilesButton.UseVisualStyleBackColor = false;
            this.selectFilesButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // blockHeightTextBox
            // 
            this.blockHeightTextBox.Location = new System.Drawing.Point(110, 71);
            this.blockHeightTextBox.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.blockHeightTextBox.Name = "blockHeightTextBox";
            this.blockHeightTextBox.Size = new System.Drawing.Size(93, 20);
            this.blockHeightTextBox.TabIndex = 10;
            this.blockHeightTextBox.ValueChanged += new System.EventHandler(this.blockHeightTextBox_ValueChanged);
            // 
            // blockWidthTextBox
            // 
            this.blockWidthTextBox.Location = new System.Drawing.Point(9, 71);
            this.blockWidthTextBox.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.blockWidthTextBox.Name = "blockWidthTextBox";
            this.blockWidthTextBox.Size = new System.Drawing.Size(91, 20);
            this.blockWidthTextBox.TabIndex = 9;
            this.blockWidthTextBox.ValueChanged += new System.EventHandler(this.blockWidthTextBox_ValueChanged);
            // 
            // templateBox
            // 
            this.templateBox.Controls.Add(this.groupBox2);
            this.templateBox.Controls.Add(this.groupBox1);
            this.templateBox.Controls.Add(this.columnsTextBox);
            this.templateBox.Controls.Add(this.rowsTextBox);
            this.templateBox.Controls.Add(this.label3);
            this.templateBox.Controls.Add(this.rowLabel);
            this.templateBox.Controls.Add(this.blockHeightTextBox);
            this.templateBox.Controls.Add(this.label1);
            this.templateBox.Controls.Add(this.label2);
            this.templateBox.Controls.Add(this.blockWidthTextBox);
            this.templateBox.Location = new System.Drawing.Point(9, 12);
            this.templateBox.Name = "templateBox";
            this.templateBox.Size = new System.Drawing.Size(232, 230);
            this.templateBox.TabIndex = 4;
            this.templateBox.TabStop = false;
            this.templateBox.Text = "Шаблон";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.backgroundColorPreview);
            this.groupBox2.Controls.Add(this.ColorButton);
            this.groupBox2.Location = new System.Drawing.Point(9, 170);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(217, 49);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Фон";
            // 
            // backgroundColorPreview
            // 
            this.backgroundColorPreview.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.backgroundColorPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.backgroundColorPreview.Location = new System.Drawing.Point(9, 19);
            this.backgroundColorPreview.Name = "backgroundColorPreview";
            this.backgroundColorPreview.Size = new System.Drawing.Size(91, 22);
            this.backgroundColorPreview.TabIndex = 0;
            this.backgroundColorPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.backgroundColorPreview_Paint);
            // 
            // ColorButton
            // 
            this.ColorButton.Location = new System.Drawing.Point(101, 19);
            this.ColorButton.Name = "ColorButton";
            this.ColorButton.Size = new System.Drawing.Size(110, 22);
            this.ColorButton.TabIndex = 15;
            this.ColorButton.Text = "Выбрать фон";
            this.ColorButton.UseVisualStyleBackColor = true;
            this.ColorButton.Click += new System.EventHandler(this.ColorButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.bottomMarginEdit);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.leftMarginEdit);
            this.groupBox1.Location = new System.Drawing.Point(9, 97);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(217, 67);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Отступы (пиксель)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(107, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Отступ снизу";
            // 
            // bottomMarginEdit
            // 
            this.bottomMarginEdit.Location = new System.Drawing.Point(106, 32);
            this.bottomMarginEdit.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.bottomMarginEdit.Name = "bottomMarginEdit";
            this.bottomMarginEdit.Size = new System.Drawing.Size(93, 20);
            this.bottomMarginEdit.TabIndex = 12;
            this.bottomMarginEdit.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "Отступ слева";
            // 
            // leftMarginEdit
            // 
            this.leftMarginEdit.Location = new System.Drawing.Point(9, 32);
            this.leftMarginEdit.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.leftMarginEdit.Name = "leftMarginEdit";
            this.leftMarginEdit.Size = new System.Drawing.Size(91, 20);
            this.leftMarginEdit.TabIndex = 11;
            this.leftMarginEdit.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // columnsTextBox
            // 
            this.columnsTextBox.Location = new System.Drawing.Point(110, 32);
            this.columnsTextBox.Name = "columnsTextBox";
            this.columnsTextBox.Size = new System.Drawing.Size(93, 20);
            this.columnsTextBox.TabIndex = 10;
            this.columnsTextBox.ValueChanged += new System.EventHandler(this.columnsTextBox_ValueChanged);
            // 
            // rowsTextBox
            // 
            this.rowsTextBox.Location = new System.Drawing.Point(9, 32);
            this.rowsTextBox.Name = "rowsTextBox";
            this.rowsTextBox.Size = new System.Drawing.Size(91, 20);
            this.rowsTextBox.TabIndex = 4;
            this.rowsTextBox.ValueChanged += new System.EventHandler(this.rowsTextBox_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(107, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Число Колонн";
            // 
            // rowLabel
            // 
            this.rowLabel.AutoSize = true;
            this.rowLabel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.rowLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rowLabel.Location = new System.Drawing.Point(6, 16);
            this.rowLabel.Name = "rowLabel";
            this.rowLabel.Size = new System.Drawing.Size(71, 13);
            this.rowLabel.TabIndex = 6;
            this.rowLabel.Text = "Число строк";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Ширина блока";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Высота";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.шаблонToolStripMenuItem,
            this.изображенияToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1442, 24);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьToolStripMenuItem1,
            this.сохранитьКакToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // сохранитьToolStripMenuItem1
            // 
            this.сохранитьToolStripMenuItem1.Enabled = false;
            this.сохранитьToolStripMenuItem1.Name = "сохранитьToolStripMenuItem1";
            this.сохранитьToolStripMenuItem1.Size = new System.Drawing.Size(162, 22);
            this.сохранитьToolStripMenuItem1.Text = "Сохранить";
            // 
            // сохранитьКакToolStripMenuItem
            // 
            this.сохранитьКакToolStripMenuItem.Enabled = false;
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.сохранитьКакToolStripMenuItem.Text = "Сохранить как...";
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jpegToolStripMenuItem,
            this.pngToolStripMenuItem,
            this.bmpToolStripMenuItem});
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.сохранитьToolStripMenuItem.Text = "Экспорт...";
            // 
            // jpegToolStripMenuItem
            // 
            this.jpegToolStripMenuItem.Name = "jpegToolStripMenuItem";
            this.jpegToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.jpegToolStripMenuItem.Text = "JPEG";
            this.jpegToolStripMenuItem.Click += new System.EventHandler(this.jpegToolStripMenuItem_Click);
            // 
            // pngToolStripMenuItem
            // 
            this.pngToolStripMenuItem.Name = "pngToolStripMenuItem";
            this.pngToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.pngToolStripMenuItem.Text = "PNG";
            this.pngToolStripMenuItem.Click += new System.EventHandler(this.pngToolStripMenuItem_Click);
            // 
            // bmpToolStripMenuItem
            // 
            this.bmpToolStripMenuItem.Name = "bmpToolStripMenuItem";
            this.bmpToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.bmpToolStripMenuItem.Text = "BMP";
            this.bmpToolStripMenuItem.Click += new System.EventHandler(this.bmpToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // шаблонToolStripMenuItem
            // 
            this.шаблонToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выровнятьВсеИзображенияToolStripMenuItem});
            this.шаблонToolStripMenuItem.Enabled = false;
            this.шаблонToolStripMenuItem.Name = "шаблонToolStripMenuItem";
            this.шаблонToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.шаблонToolStripMenuItem.Text = "Шаблон";
            // 
            // выровнятьВсеИзображенияToolStripMenuItem
            // 
            this.выровнятьВсеИзображенияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.поЦентруToolStripMenuItem,
            this.сверхуСлеваToolStripMenuItem,
            this.справаСнизуToolStripMenuItem,
            this.растянутьToolStripMenuItem});
            this.выровнятьВсеИзображенияToolStripMenuItem.Name = "выровнятьВсеИзображенияToolStripMenuItem";
            this.выровнятьВсеИзображенияToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.выровнятьВсеИзображенияToolStripMenuItem.Text = "Выровнять все изображения...";
            // 
            // поЦентруToolStripMenuItem
            // 
            this.поЦентруToolStripMenuItem.Name = "поЦентруToolStripMenuItem";
            this.поЦентруToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.поЦентруToolStripMenuItem.Text = "По центру";
            // 
            // сверхуСлеваToolStripMenuItem
            // 
            this.сверхуСлеваToolStripMenuItem.Name = "сверхуСлеваToolStripMenuItem";
            this.сверхуСлеваToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.сверхуСлеваToolStripMenuItem.Text = "Сверху/Слева";
            // 
            // справаСнизуToolStripMenuItem
            // 
            this.справаСнизуToolStripMenuItem.Name = "справаСнизуToolStripMenuItem";
            this.справаСнизуToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.справаСнизуToolStripMenuItem.Text = "Справа/Снизу";
            // 
            // растянутьToolStripMenuItem
            // 
            this.растянутьToolStripMenuItem.Name = "растянутьToolStripMenuItem";
            this.растянутьToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.растянутьToolStripMenuItem.Text = "Растянуть";
            // 
            // изображенияToolStripMenuItem
            // 
            this.изображенияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.экспортироватьToolStripMenuItem});
            this.изображенияToolStripMenuItem.Enabled = false;
            this.изображенияToolStripMenuItem.Name = "изображенияToolStripMenuItem";
            this.изображенияToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.изображенияToolStripMenuItem.Text = "Изображения";
            // 
            // экспортироватьToolStripMenuItem
            // 
            this.экспортироватьToolStripMenuItem.Name = "экспортироватьToolStripMenuItem";
            this.экспортироватьToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.экспортироватьToolStripMenuItem.Text = "Экспортировать все";
            // 
            // TemplatePanel
            // 
            this.TemplatePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TemplatePanel.AutoSize = true;
            this.TemplatePanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.TemplatePanel.Controls.Add(this.templateBox);
            this.TemplatePanel.Location = new System.Drawing.Point(3, 27);
            this.TemplatePanel.Name = "TemplatePanel";
            this.TemplatePanel.Size = new System.Drawing.Size(246, 633);
            this.TemplatePanel.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1442, 663);
            this.Controls.Add(this.TemplatePanel);
            this.Controls.Add(this.ImagePreviewPanel);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.menuStrip);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "АвтоКоллаж";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MainForm_KeyPress);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ImagePreviewPanel.ResumeLayout(false);
            this.ImagePreviewPanel.PerformLayout();
            this.formattingBox.ResumeLayout(false);
            this.formattingBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WCut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.XCut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YCut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HCut)).EndInit();
            this.imageListBox.ResumeLayout(false);
            this.previewBox.ResumeLayout(false);
            this.previewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.blockHeightTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blockWidthTextBox)).EndInit();
            this.templateBox.ResumeLayout(false);
            this.templateBox.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bottomMarginEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leftMarginEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.columnsTextBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowsTextBox)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.TemplatePanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Panel ImagePreviewPanel;
        private System.Windows.Forms.Button selectFilesButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ListBox ImageListView;
        private Label label2;
        private Label label1;
        private Panel previewPanel;
        private GroupBox templateBox;
        private Label label3;
        private Label rowLabel;
        private NumericUpDown columnsTextBox;
        private NumericUpDown rowsTextBox;
        private NumericUpDown blockHeightTextBox;
        private NumericUpDown blockWidthTextBox;
        private GroupBox previewBox;
        private GroupBox imageListBox;
        private GroupBox formattingBox;
        private RadioButton cutCustomRadio;
        private RadioButton cutMiddleRadio;
        private RadioButton cutRightBottomRadio;
        private RadioButton cutTopLeftRadio;
        private RadioButton stretchRadio;
        private MenuStrip menuStrip;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem сохранитьToolStripMenuItem;
        private Label label4;
        private NumericUpDown WCut;
        private NumericUpDown XCut;
        private NumericUpDown YCut;
        private NumericUpDown HCut;
        private Label label6;
        private Label label5;
        private Label label7;
        private Panel ThumbnailImageSelectionPanel;
        private ToolStripMenuItem сохранитьToolStripMenuItem1;
        private ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private ToolStripMenuItem jpegToolStripMenuItem;
        private ToolStripMenuItem pngToolStripMenuItem;
        private ToolStripMenuItem bmpToolStripMenuItem;
        private ToolStripMenuItem выходToolStripMenuItem;
        private ToolStripMenuItem шаблонToolStripMenuItem;
        private ToolStripMenuItem выровнятьВсеИзображенияToolStripMenuItem;
        private ToolStripMenuItem поЦентруToolStripMenuItem;
        private ToolStripMenuItem сверхуСлеваToolStripMenuItem;
        private ToolStripMenuItem справаСнизуToolStripMenuItem;
        private ToolStripMenuItem растянутьToolStripMenuItem;
        private ToolStripMenuItem изображенияToolStripMenuItem;
        private ToolStripMenuItem экспортироватьToolStripMenuItem;
        private Button ImageApplyButton;
        private CheckBox HideCheckBox;
        private Panel TemplatePanel;
        private Label label9;
        private Label label8;
        private NumericUpDown bottomMarginEdit;
        private NumericUpDown leftMarginEdit;
        private GroupBox groupBox1;
        private ColorDialog colorDialog1;
        private Button ColorButton;
        private GroupBox groupBox2;
        private Panel backgroundColorPreview;
    }
}

