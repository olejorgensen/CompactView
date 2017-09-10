namespace CompactView
{
    partial class MainForm
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.cbReadOnly = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCut = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnPaste = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnQuery = new System.Windows.Forms.ToolStripButton();
            this.btnExecute = new System.Windows.Forms.ToolStripButton();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.splitterHorizontal = new System.Windows.Forms.SplitContainer();
            this.rtbQuery = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbResult = new System.Windows.Forms.Label();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.treeDb = new System.Windows.Forms.TreeView();
            this.images = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rtbDdl = new System.Windows.Forms.RichTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDatabaseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeDatabaseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentFilesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.loadSqlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSqlMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSchemaMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.allowEditingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.importMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.previewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.queryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showEditorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.executeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseToolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutCompactViewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitterHorizontal)).BeginInit();
            this.splitterHorizontal.Panel1.SuspendLayout();
            this.splitterHorizontal.Panel2.SuspendLayout();
            this.splitterHorizontal.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolBar
            // 
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpen,
            this.cbReadOnly,
            this.toolStripSeparator1,
            this.btnCut,
            this.btnCopy,
            this.btnPaste,
            this.toolStripSeparator5,
            this.btnQuery,
            this.btnExecute,
            this.btnClear});
            this.toolBar.Location = new System.Drawing.Point(0, 24);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(748, 25);
            this.toolBar.TabIndex = 0;
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.White;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.btnOpen.Size = new System.Drawing.Size(23, 22);
            this.btnOpen.Text = "Open";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // cbReadOnly
            // 
            this.cbReadOnly.BackColor = System.Drawing.Color.LightYellow;
            this.cbReadOnly.Items.AddRange(new object[] {
            "Read only",
            "Allow editing"});
            this.cbReadOnly.Name = "cbReadOnly";
            this.cbReadOnly.Size = new System.Drawing.Size(145, 25);
            this.cbReadOnly.Text = "Read only";
            this.cbReadOnly.SelectedIndexChanged += new System.EventHandler(this.cbReadOnly_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCut
            // 
            this.btnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCut.Enabled = false;
            this.btnCut.Image = ((System.Drawing.Image)(resources.GetObject("btnCut.Image")));
            this.btnCut.ImageTransparentColor = System.Drawing.Color.White;
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(23, 22);
            this.btnCut.Tag = "";
            this.btnCut.Text = "Cut";
            this.btnCut.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Enabled = false;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.White;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(23, 22);
            this.btnCopy.Text = "Copy";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPaste.Enabled = false;
            this.btnPaste.Image = ((System.Drawing.Image)(resources.GetObject("btnPaste.Image")));
            this.btnPaste.ImageTransparentColor = System.Drawing.Color.White;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(23, 22);
            this.btnPaste.Text = "Paste";
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnQuery
            // 
            this.btnQuery.CheckOnClick = true;
            this.btnQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnQuery.Enabled = false;
            this.btnQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnQuery.Image")));
            this.btnQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(23, 22);
            this.btnQuery.Text = "Query";
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExecute.Enabled = false;
            this.btnExecute.Image = ((System.Drawing.Image)(resources.GetObject("btnExecute.Image")));
            this.btnExecute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(23, 22);
            this.btnExecute.Text = "Execute";
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnClear
            // 
            this.btnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClear.Enabled = false;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(23, 22);
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // splitterHorizontal
            // 
            this.splitterHorizontal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitterHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitterHorizontal.Location = new System.Drawing.Point(0, 49);
            this.splitterHorizontal.Name = "splitterHorizontal";
            this.splitterHorizontal.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitterHorizontal.Panel1
            // 
            this.splitterHorizontal.Panel1.Controls.Add(this.rtbQuery);
            this.splitterHorizontal.Panel1.Controls.Add(this.panel1);
            this.splitterHorizontal.Panel1MinSize = 200;
            // 
            // splitterHorizontal.Panel2
            // 
            this.splitterHorizontal.Panel2.Controls.Add(this.splitContainer2);
            this.splitterHorizontal.Size = new System.Drawing.Size(748, 363);
            this.splitterHorizontal.SplitterDistance = 200;
            this.splitterHorizontal.TabIndex = 1;
            // 
            // rtbQuery
            // 
            this.rtbQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbQuery.ContextMenuStrip = this.contextMenuStrip1;
            this.rtbQuery.DetectUrls = false;
            this.rtbQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbQuery.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.rtbQuery.HideSelection = false;
            this.rtbQuery.Location = new System.Drawing.Point(0, 0);
            this.rtbQuery.Name = "rtbQuery";
            this.rtbQuery.Size = new System.Drawing.Size(746, 177);
            this.rtbQuery.TabIndex = 6;
            this.rtbQuery.Text = "";
            this.rtbQuery.WordWrap = false;
            this.rtbQuery.SelectionChanged += new System.EventHandler(this.rtbQuery_Enter_Leave_SelectionChanged);
            this.rtbQuery.Enter += new System.EventHandler(this.rtbQuery_Enter_Leave_SelectionChanged);
            this.rtbQuery.Leave += new System.EventHandler(this.rtbQuery_Enter_Leave_SelectionChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripMenuItem1,
            this.loadFromFileToolStripMenuItem,
            this.saveToFileToolStripMenuItem,
            this.toolStripMenuItem2,
            this.printToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(158, 148);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
            this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(154, 6);
            // 
            // loadFromFileToolStripMenuItem
            // 
            this.loadFromFileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("loadFromFileToolStripMenuItem.Image")));
            this.loadFromFileToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.loadFromFileToolStripMenuItem.Name = "loadFromFileToolStripMenuItem";
            this.loadFromFileToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.loadFromFileToolStripMenuItem.Text = "Load from file...";
            this.loadFromFileToolStripMenuItem.Click += new System.EventHandler(this.loadFromFileToolStripMenuItem_Click);
            // 
            // saveToFileToolStripMenuItem
            // 
            this.saveToFileToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToFileToolStripMenuItem.Image")));
            this.saveToFileToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.saveToFileToolStripMenuItem.Name = "saveToFileToolStripMenuItem";
            this.saveToFileToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.saveToFileToolStripMenuItem.Text = "Save to file...";
            this.saveToFileToolStripMenuItem.Click += new System.EventHandler(this.saveToFileToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(154, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripMenuItem.Image")));
            this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.printToolStripMenuItem.Text = "Print...";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.lbResult);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 177);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(746, 21);
            this.panel1.TabIndex = 5;
            // 
            // lbResult
            // 
            this.lbResult.AutoEllipsis = true;
            this.lbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbResult.Location = new System.Drawing.Point(0, 0);
            this.lbResult.Name = "lbResult";
            this.lbResult.Size = new System.Drawing.Size(742, 17);
            this.lbResult.TabIndex = 2;
            this.lbResult.Text = "By clicking on any field bracketed in the SQL Schema, it is added to the query";
            this.lbResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.treeDb);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(746, 157);
            this.splitContainer2.SplitterDistance = 172;
            this.splitContainer2.TabIndex = 0;
            // 
            // treeDb
            // 
            this.treeDb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeDb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeDb.HideSelection = false;
            this.treeDb.ImageIndex = 0;
            this.treeDb.ImageList = this.images;
            this.treeDb.Location = new System.Drawing.Point(0, 0);
            this.treeDb.Name = "treeDb";
            this.treeDb.SelectedImageIndex = 0;
            this.treeDb.ShowNodeToolTips = true;
            this.treeDb.ShowRootLines = false;
            this.treeDb.Size = new System.Drawing.Size(172, 157);
            this.treeDb.TabIndex = 1;
            this.treeDb.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDb_AfterSelect);
            // 
            // images
            // 
            this.images.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("images.ImageStream")));
            this.images.TransparentColor = System.Drawing.Color.Transparent;
            this.images.Images.SetKeyName(0, "database.png");
            this.images.Images.SetKeyName(1, "table.png");
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(570, 157);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGrid);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(562, 131);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGrid
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LemonChiffon;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightYellow;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGrid.Location = new System.Drawing.Point(3, 3);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.RowHeadersWidth = 24;
            this.dataGrid.Size = new System.Drawing.Size(556, 125);
            this.dataGrid.TabIndex = 1;
            this.dataGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellClick);
            this.dataGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellDoubleClick);
            this.dataGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGrid_CellFormatting);
            this.dataGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_CellValueChanged);
            this.dataGrid.ColumnAdded += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dataGrid_ColumnAdded);
            this.dataGrid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGrid_ColumnHeaderMouseClick);
            this.dataGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGrid_DataError);
            this.dataGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGrid_MouseDown);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rtbDdl);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(562, 131);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "SQL Schema";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rtbDdl
            // 
            this.rtbDdl.BackColor = System.Drawing.SystemColors.Window;
            this.rtbDdl.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbDdl.ContextMenuStrip = this.contextMenuStrip1;
            this.rtbDdl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDdl.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbDdl.HideSelection = false;
            this.rtbDdl.Location = new System.Drawing.Point(3, 3);
            this.rtbDdl.Name = "rtbDdl";
            this.rtbDdl.ReadOnly = true;
            this.rtbDdl.Size = new System.Drawing.Size(556, 125);
            this.rtbDdl.TabIndex = 2;
            this.rtbDdl.Text = "";
            this.rtbDdl.WordWrap = false;
            this.rtbDdl.Click += new System.EventHandler(this.rtbDdl_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "SQL CE databases (*.sdf)|*.sdf|All files|*.*";
            this.openFileDialog1.Title = "Select SQL CE database";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.Filter = "SQL text files (*.sql)|*.sql|Text files|*.txt|All files|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "SQL text files (*.sql)|*.sql|Text files|*.txt|All files|*.*";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.editMenuItem,
            this.queryMenuItem,
            this.toolsMenuItem,
            this.helpMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(748, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDatabaseMenuItem,
            this.closeDatabaseMenuItem,
            this.recentFilesMenuItem,
            this.toolStripSeparator4,
            this.loadSqlMenuItem,
            this.saveSqlMenuItem,
            this.saveSchemaMenuItem,
            this.toolStripSeparator8,
            this.allowEditingMenuItem,
            this.toolStripMenuItem3,
            this.importMenuItem,
            this.exportMenuItem,
            this.toolStripSeparator3,
            this.previewMenuItem,
            this.printMenuItem,
            this.toolStripSeparator6,
            this.exitMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileMenuItem.Text = "File";
            // 
            // openDatabaseMenuItem
            // 
            this.openDatabaseMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openDatabaseMenuItem.Image")));
            this.openDatabaseMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.openDatabaseMenuItem.Name = "openDatabaseMenuItem";
            this.openDatabaseMenuItem.Size = new System.Drawing.Size(175, 22);
            this.openDatabaseMenuItem.Text = "Open database...";
            this.openDatabaseMenuItem.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // closeDatabaseMenuItem
            // 
            this.closeDatabaseMenuItem.Name = "closeDatabaseMenuItem";
            this.closeDatabaseMenuItem.Size = new System.Drawing.Size(175, 22);
            this.closeDatabaseMenuItem.Text = "Close database";
            this.closeDatabaseMenuItem.Click += new System.EventHandler(this.closeDatabaseMenuItem_Click);
            // 
            // recentFilesMenuItem
            // 
            this.recentFilesMenuItem.Name = "recentFilesMenuItem";
            this.recentFilesMenuItem.Size = new System.Drawing.Size(175, 22);
            this.recentFilesMenuItem.Text = "Recent files";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(172, 6);
            // 
            // loadSqlMenuItem
            // 
            this.loadSqlMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("loadSqlMenuItem.Image")));
            this.loadSqlMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.loadSqlMenuItem.Name = "loadSqlMenuItem";
            this.loadSqlMenuItem.Size = new System.Drawing.Size(175, 22);
            this.loadSqlMenuItem.Text = "Load SQL query...";
            this.loadSqlMenuItem.Click += new System.EventHandler(this.loadSqlMenuItem_Click);
            // 
            // saveSqlMenuItem
            // 
            this.saveSqlMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveSqlMenuItem.Image")));
            this.saveSqlMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.saveSqlMenuItem.Name = "saveSqlMenuItem";
            this.saveSqlMenuItem.Size = new System.Drawing.Size(175, 22);
            this.saveSqlMenuItem.Text = "Save SQL query...";
            this.saveSqlMenuItem.Click += new System.EventHandler(this.saveSqlMenuItem_Click);
            // 
            // saveSchemaMenuItem
            // 
            this.saveSchemaMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveSchemaMenuItem.Image")));
            this.saveSchemaMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.saveSchemaMenuItem.Name = "saveSchemaMenuItem";
            this.saveSchemaMenuItem.Size = new System.Drawing.Size(175, 22);
            this.saveSchemaMenuItem.Text = "Save SQL schema...";
            this.saveSchemaMenuItem.Click += new System.EventHandler(this.saveSchemaMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(172, 6);
            // 
            // allowEditingMenuItem
            // 
            this.allowEditingMenuItem.Name = "allowEditingMenuItem";
            this.allowEditingMenuItem.Size = new System.Drawing.Size(175, 22);
            this.allowEditingMenuItem.Text = "Allow editing";
            this.allowEditingMenuItem.Click += new System.EventHandler(this.allowEditingMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(172, 6);
            // 
            // importMenuItem
            // 
            this.importMenuItem.Name = "importMenuItem";
            this.importMenuItem.Size = new System.Drawing.Size(175, 22);
            this.importMenuItem.Text = "Import...";
            this.importMenuItem.Click += new System.EventHandler(this.importMenuItem_Click);
            // 
            // exportMenuItem
            // 
            this.exportMenuItem.Name = "exportMenuItem";
            this.exportMenuItem.Size = new System.Drawing.Size(175, 22);
            this.exportMenuItem.Text = "Export...";
            this.exportMenuItem.Click += new System.EventHandler(this.exportMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(172, 6);
            // 
            // previewMenuItem
            // 
            this.previewMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("previewMenuItem.Image")));
            this.previewMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.previewMenuItem.Name = "previewMenuItem";
            this.previewMenuItem.Size = new System.Drawing.Size(175, 22);
            this.previewMenuItem.Text = "Print preview";
            this.previewMenuItem.Click += new System.EventHandler(this.previewMenuItem_Click);
            // 
            // printMenuItem
            // 
            this.printMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printMenuItem.Image")));
            this.printMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.printMenuItem.Name = "printMenuItem";
            this.printMenuItem.Size = new System.Drawing.Size(175, 22);
            this.printMenuItem.Text = "Print...";
            this.printMenuItem.Click += new System.EventHandler(this.printMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(172, 6);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(175, 22);
            this.exitMenuItem.Text = "Exit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // editMenuItem
            // 
            this.editMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutMenuItem,
            this.copyMenuItem,
            this.pasteMenuItem,
            this.deleteMenuItem});
            this.editMenuItem.Name = "editMenuItem";
            this.editMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editMenuItem.Text = "Edit";
            // 
            // cutMenuItem
            // 
            this.cutMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutMenuItem.Image")));
            this.cutMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.cutMenuItem.Name = "cutMenuItem";
            this.cutMenuItem.Size = new System.Drawing.Size(107, 22);
            this.cutMenuItem.Text = "Cut";
            this.cutMenuItem.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // copyMenuItem
            // 
            this.copyMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyMenuItem.Image")));
            this.copyMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.copyMenuItem.Name = "copyMenuItem";
            this.copyMenuItem.Size = new System.Drawing.Size(107, 22);
            this.copyMenuItem.Text = "Copy";
            this.copyMenuItem.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // pasteMenuItem
            // 
            this.pasteMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteMenuItem.Image")));
            this.pasteMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.pasteMenuItem.Name = "pasteMenuItem";
            this.pasteMenuItem.Size = new System.Drawing.Size(107, 22);
            this.pasteMenuItem.Text = "Paste";
            this.pasteMenuItem.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // deleteMenuItem
            // 
            this.deleteMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteMenuItem.Image")));
            this.deleteMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.deleteMenuItem.Name = "deleteMenuItem";
            this.deleteMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteMenuItem.Text = "Delete";
            this.deleteMenuItem.Click += new System.EventHandler(this.deleteMenuItem_Click);
            // 
            // queryMenuItem
            // 
            this.queryMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showEditorMenuItem,
            this.executeMenuItem,
            this.clearMenuItem});
            this.queryMenuItem.Name = "queryMenuItem";
            this.queryMenuItem.Size = new System.Drawing.Size(51, 20);
            this.queryMenuItem.Text = "Query";
            // 
            // showEditorMenuItem
            // 
            this.showEditorMenuItem.Enabled = false;
            this.showEditorMenuItem.Name = "showEditorMenuItem";
            this.showEditorMenuItem.Size = new System.Drawing.Size(137, 22);
            this.showEditorMenuItem.Text = "Show editor";
            this.showEditorMenuItem.Click += new System.EventHandler(this.showEditorMenuItem_Click);
            // 
            // executeMenuItem
            // 
            this.executeMenuItem.Enabled = false;
            this.executeMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("executeMenuItem.Image")));
            this.executeMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.executeMenuItem.Name = "executeMenuItem";
            this.executeMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.executeMenuItem.Size = new System.Drawing.Size(137, 22);
            this.executeMenuItem.Text = "Execute";
            this.executeMenuItem.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // clearMenuItem
            // 
            this.clearMenuItem.Enabled = false;
            this.clearMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("clearMenuItem.Image")));
            this.clearMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.clearMenuItem.Name = "clearMenuItem";
            this.clearMenuItem.Size = new System.Drawing.Size(137, 22);
            this.clearMenuItem.Text = "Clear";
            this.clearMenuItem.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // toolsMenuItem
            // 
            this.toolsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.databaseToolsMenuItem,
            this.toolStripSeparator7,
            this.optionsMenuItem});
            this.toolsMenuItem.Name = "toolsMenuItem";
            this.toolsMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsMenuItem.Text = "Tools";
            // 
            // databaseToolsMenuItem
            // 
            this.databaseToolsMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("databaseToolsMenuItem.Image")));
            this.databaseToolsMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.databaseToolsMenuItem.Name = "databaseToolsMenuItem";
            this.databaseToolsMenuItem.Size = new System.Drawing.Size(160, 22);
            this.databaseToolsMenuItem.Text = "Database tools...";
            this.databaseToolsMenuItem.Click += new System.EventHandler(this.databaseToolsMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(157, 6);
            // 
            // optionsMenuItem
            // 
            this.optionsMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("optionsMenuItem.Image")));
            this.optionsMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.optionsMenuItem.Name = "optionsMenuItem";
            this.optionsMenuItem.Size = new System.Drawing.Size(160, 22);
            this.optionsMenuItem.Text = "Options...";
            this.optionsMenuItem.Click += new System.EventHandler(this.optionsMenuItem_Click);
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutCompactViewMenuItem});
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpMenuItem.Text = "Help";
            // 
            // aboutCompactViewMenuItem
            // 
            this.aboutCompactViewMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("aboutCompactViewMenuItem.Image")));
            this.aboutCompactViewMenuItem.ImageTransparentColor = System.Drawing.Color.White;
            this.aboutCompactViewMenuItem.Name = "aboutCompactViewMenuItem";
            this.aboutCompactViewMenuItem.Size = new System.Drawing.Size(184, 22);
            this.aboutCompactViewMenuItem.Text = "About CompactView";
            this.aboutCompactViewMenuItem.Click += new System.EventHandler(this.aboutCompactViewMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(103, 26);
            // 
            // copyToolStripMenuItem1
            // 
            this.copyToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem1.Image")));
            this.copyToolStripMenuItem1.Name = "copyToolStripMenuItem1";
            this.copyToolStripMenuItem1.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem1.Text = "Copy";
            this.copyToolStripMenuItem1.Click += new System.EventHandler(this.copyToolStripMenuItem1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 412);
            this.Controls.Add(this.splitterHorizontal);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "CompactView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainForm_KeyDown);
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            this.splitterHorizontal.Panel1.ResumeLayout(false);
            this.splitterHorizontal.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitterHorizontal)).EndInit();
            this.splitterHorizontal.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.ToolStripButton btnQuery;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.SplitContainer splitterHorizontal;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView treeDb;
        private System.Windows.Forms.ImageList images;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RichTextBox rtbDdl;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox rtbQuery;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbResult;
        private System.Windows.Forms.ToolStripButton btnExecute;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripComboBox cbReadOnly;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem loadFromFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToFileToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnCut;
        private System.Windows.Forms.ToolStripButton btnCopy;
        private System.Windows.Forms.ToolStripButton btnPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDatabaseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recentFilesMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem allowEditingMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem importMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem queryMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showEditorMenuItem;
        private System.Windows.Forms.ToolStripMenuItem executeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseToolsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem optionsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutCompactViewMenuItem;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem previewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadSqlMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSqlMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSchemaMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeDatabaseMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem1;
	}
}

