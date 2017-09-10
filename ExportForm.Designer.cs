namespace CompactView
{
    partial class ExportForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportForm));
            this.treeDb = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnExport = new System.Windows.Forms.Button();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbSchema = new System.Windows.Forms.RadioButton();
            this.rbData = new System.Windows.Forms.RadioButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // treeDb
            // 
            this.treeDb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeDb.CheckBoxes = true;
            this.treeDb.ImageIndex = 0;
            this.treeDb.ImageList = this.imageList1;
            this.treeDb.Location = new System.Drawing.Point(12, 12);
            this.treeDb.Name = "treeDb";
            this.treeDb.SelectedImageIndex = 0;
            this.treeDb.ShowPlusMinus = false;
            this.treeDb.ShowRootLines = false;
            this.treeDb.Size = new System.Drawing.Size(397, 351);
            this.treeDb.TabIndex = 0;
            this.treeDb.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeDb_BeforeCheck);
            this.treeDb.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeDb_AfterCheck);
            this.treeDb.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeDb_BeforeCollapse);
            this.treeDb.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeDb_BeforeSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "table-small.png");
            this.imageList1.Images.SetKeyName(1, "square-gray.png");
            this.imageList1.Images.SetKeyName(2, "index.png");
            this.imageList1.Images.SetKeyName(3, "key.png");
            this.imageList1.Images.SetKeyName(4, "lock.png");
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(326, 371);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(83, 23);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // rbAll
            // 
            this.rbAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Location = new System.Drawing.Point(12, 374);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(36, 17);
            this.rbAll.TabIndex = 2;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "All";
            this.rbAll.UseVisualStyleBackColor = true;
            // 
            // rbSchema
            // 
            this.rbSchema.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbSchema.AutoSize = true;
            this.rbSchema.Location = new System.Drawing.Point(75, 374);
            this.rbSchema.Name = "rbSchema";
            this.rbSchema.Size = new System.Drawing.Size(86, 17);
            this.rbSchema.TabIndex = 2;
            this.rbSchema.Text = "Only schema";
            this.rbSchema.UseVisualStyleBackColor = true;
            // 
            // rbData
            // 
            this.rbData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbData.AutoSize = true;
            this.rbData.Location = new System.Drawing.Point(188, 374);
            this.rbData.Name = "rbData";
            this.rbData.Size = new System.Drawing.Size(70, 17);
            this.rbData.TabIndex = 2;
            this.rbData.Text = "Only data";
            this.rbData.UseVisualStyleBackColor = true;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "SQL text files (*.sql)|*.sql|Text files|*.txt|All files|*.*";
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(422, 401);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.rbData);
            this.Controls.Add(this.treeDb);
            this.Controls.Add(this.rbAll);
            this.Controls.Add(this.rbSchema);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(393, 252);
            this.Name = "ExportForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Export";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeDb;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.RadioButton rbSchema;
        private System.Windows.Forms.RadioButton rbData;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}