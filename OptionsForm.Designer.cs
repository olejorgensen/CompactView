namespace CompactView
{
    partial class OptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.cbBackground1 = new System.Windows.Forms.ComboBox();
            this.cbFont1 = new System.Windows.Forms.ComboBox();
            this.cbBackground2 = new System.Windows.Forms.ComboBox();
            this.cbFont2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnFont2 = new System.Windows.Forms.Button();
            this.btnBack2 = new System.Windows.Forms.Button();
            this.btnFont1 = new System.Windows.Forms.Button();
            this.btnBack1 = new System.Windows.Forms.Button();
            this.cbColorSet = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbBackground1
            // 
            this.cbBackground1.FormattingEnabled = true;
            this.cbBackground1.Location = new System.Drawing.Point(251, 42);
            this.cbBackground1.Name = "cbBackground1";
            this.cbBackground1.Size = new System.Drawing.Size(180, 21);
            this.cbBackground1.TabIndex = 0;
            this.cbBackground1.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // cbFont1
            // 
            this.cbFont1.FormattingEnabled = true;
            this.cbFont1.Location = new System.Drawing.Point(16, 42);
            this.cbFont1.Name = "cbFont1";
            this.cbFont1.Size = new System.Drawing.Size(180, 21);
            this.cbFont1.TabIndex = 0;
            this.cbFont1.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // cbBackground2
            // 
            this.cbBackground2.FormattingEnabled = true;
            this.cbBackground2.Location = new System.Drawing.Point(251, 82);
            this.cbBackground2.Name = "cbBackground2";
            this.cbBackground2.Size = new System.Drawing.Size(180, 21);
            this.cbBackground2.TabIndex = 0;
            this.cbBackground2.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // cbFont2
            // 
            this.cbFont2.FormattingEnabled = true;
            this.cbFont2.Location = new System.Drawing.Point(16, 82);
            this.cbFont2.Name = "cbFont2";
            this.cbFont2.Size = new System.Drawing.Size(180, 21);
            this.cbFont2.TabIndex = 0;
            this.cbFont2.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(14, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Line 1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(14, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Line 2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(14, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 25);
            this.label3.TabIndex = 1;
            this.label3.Text = "Line 3";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(14, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 25);
            this.label4.TabIndex = 1;
            this.label4.Text = "Line 4";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(251, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(180, 21);
            this.label5.TabIndex = 1;
            this.label5.Text = "Background Color";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(180, 21);
            this.label6.TabIndex = 1;
            this.label6.Text = "Font Color";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(239, 229);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(92, 25);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(360, 229);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 25);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.cbColorSet);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(669, 203);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Colors";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbFont1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.btnFont2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btnBack2);
            this.groupBox2.Controls.Add(this.cbFont2);
            this.groupBox2.Controls.Add(this.btnFont1);
            this.groupBox2.Controls.Add(this.cbBackground2);
            this.groupBox2.Controls.Add(this.btnBack1);
            this.groupBox2.Controls.Add(this.cbBackground1);
            this.groupBox2.Location = new System.Drawing.Point(173, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(479, 120);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "User defined";
            // 
            // btnFont2
            // 
            this.btnFont2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFont2.BackgroundImage")));
            this.btnFont2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnFont2.Location = new System.Drawing.Point(202, 79);
            this.btnFont2.Name = "btnFont2";
            this.btnFont2.Size = new System.Drawing.Size(25, 25);
            this.btnFont2.TabIndex = 2;
            this.btnFont2.UseVisualStyleBackColor = true;
            this.btnFont2.Click += new System.EventHandler(this.btnFont2_Click);
            // 
            // btnBack2
            // 
            this.btnBack2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBack2.BackgroundImage")));
            this.btnBack2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBack2.Location = new System.Drawing.Point(437, 79);
            this.btnBack2.Name = "btnBack2";
            this.btnBack2.Size = new System.Drawing.Size(25, 25);
            this.btnBack2.TabIndex = 2;
            this.btnBack2.UseVisualStyleBackColor = true;
            this.btnBack2.Click += new System.EventHandler(this.btnBack2_Click);
            // 
            // btnFont1
            // 
            this.btnFont1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFont1.BackgroundImage")));
            this.btnFont1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnFont1.Location = new System.Drawing.Point(202, 40);
            this.btnFont1.Name = "btnFont1";
            this.btnFont1.Size = new System.Drawing.Size(25, 25);
            this.btnFont1.TabIndex = 2;
            this.btnFont1.UseVisualStyleBackColor = true;
            this.btnFont1.Click += new System.EventHandler(this.btnFont1_Click);
            // 
            // btnBack1
            // 
            this.btnBack1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBack1.BackgroundImage")));
            this.btnBack1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBack1.Location = new System.Drawing.Point(437, 39);
            this.btnBack1.Name = "btnBack1";
            this.btnBack1.Size = new System.Drawing.Size(25, 25);
            this.btnBack1.TabIndex = 2;
            this.btnBack1.UseVisualStyleBackColor = true;
            this.btnBack1.Click += new System.EventHandler(this.btnBack1_Click);
            // 
            // cbColorSet
            // 
            this.cbColorSet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColorSet.FormattingEnabled = true;
            this.cbColorSet.Items.AddRange(new object[] {
            "LightYellow/LemonChiffon",
            "MintCream/PaleGreen",
            "White/NavajoWhite",
            "Honeydew/PaleTurquoise",
            "Azure/DarkCyan",
            "User defined"});
            this.cbColorSet.Location = new System.Drawing.Point(173, 23);
            this.cbColorSet.Name = "cbColorSet";
            this.cbColorSet.Size = new System.Drawing.Size(180, 21);
            this.cbColorSet.TabIndex = 5;
            this.cbColorSet.SelectedIndexChanged += new System.EventHandler(this.cbColorSet_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(14, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 21);
            this.label7.TabIndex = 1;
            this.label7.Text = "Color Set:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.FullOpen = true;
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(693, 265);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsForm_FormClosing);
            this.Load += new System.EventHandler(this.OptionsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbBackground1;
        private System.Windows.Forms.ComboBox cbFont1;
        private System.Windows.Forms.ComboBox cbBackground2;
        private System.Windows.Forms.ComboBox cbFont2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBack1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btnFont2;
        private System.Windows.Forms.Button btnBack2;
        private System.Windows.Forms.Button btnFont1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cbColorSet;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}