namespace CompactView
{
    partial class ToolsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btClose = new System.Windows.Forms.Button();
            this.btSelect = new System.Windows.Forms.Button();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btCompact = new System.Windows.Forms.Button();
            this.btRepair = new System.Windows.Forms.Button();
            this.btShrink = new System.Windows.Forms.Button();
            this.btUpgrade = new System.Windows.Forms.Button();
            this.btVerify = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btCreate = new System.Windows.Forms.Button();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbUpgradeTo = new System.Windows.Forms.ComboBox();
            this.cbVersion = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btClose
            // 
            this.btClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btClose.Location = new System.Drawing.Point(268, 280);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(92, 25);
            this.btClose.TabIndex = 0;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            // 
            // btSelect
            // 
            this.btSelect.Location = new System.Drawing.Point(544, 23);
            this.btSelect.Name = "btSelect";
            this.btSelect.Size = new System.Drawing.Size(24, 22);
            this.btSelect.TabIndex = 1;
            this.btSelect.Text = "...";
            this.toolTip2.SetToolTip(this.btSelect, "Select database file");
            this.btSelect.UseVisualStyleBackColor = true;
            this.btSelect.Click += new System.EventHandler(this.btSelect_Click);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(100, 63);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(438, 20);
            this.tbPassword.TabIndex = 2;
            this.toolTip2.SetToolTip(this.tbPassword, "Database password. Leave blank if none");
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(20, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "File name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btCompact
            // 
            this.btCompact.Location = new System.Drawing.Point(207, 19);
            this.btCompact.Name = "btCompact";
            this.btCompact.Size = new System.Drawing.Size(92, 25);
            this.btCompact.TabIndex = 4;
            this.btCompact.Text = "Compact";
            this.toolTip1.SetToolTip(this.btCompact, "Reclaims wasted space in the database by creating a new database file from the ex" +
                    "isting file");
            this.btCompact.UseVisualStyleBackColor = true;
            this.btCompact.Click += new System.EventHandler(this.btCompact_Click);
            // 
            // btRepair
            // 
            this.btRepair.Location = new System.Drawing.Point(403, 19);
            this.btRepair.Name = "btRepair";
            this.btRepair.Size = new System.Drawing.Size(92, 25);
            this.btRepair.TabIndex = 5;
            this.btRepair.Text = "Repair";
            this.toolTip1.SetToolTip(this.btRepair, "Repair a corrupted database. First try to recover the corrupted rows, then remove" +
                    " the remaining corrupted rows");
            this.btRepair.UseVisualStyleBackColor = true;
            this.btRepair.Click += new System.EventHandler(this.btRepair_Click);
            // 
            // btShrink
            // 
            this.btShrink.Location = new System.Drawing.Point(305, 19);
            this.btShrink.Name = "btShrink";
            this.btShrink.Size = new System.Drawing.Size(92, 25);
            this.btShrink.TabIndex = 6;
            this.btShrink.Text = "Shrink";
            this.toolTip1.SetToolTip(this.btShrink, "Reclaims wasted space in the database by moving empty pages to the end of the fil" +
                    "e, and then truncating the file");
            this.btShrink.UseVisualStyleBackColor = true;
            this.btShrink.Click += new System.EventHandler(this.btShrink_Click);
            // 
            // btUpgrade
            // 
            this.btUpgrade.Location = new System.Drawing.Point(109, 19);
            this.btUpgrade.Name = "btUpgrade";
            this.btUpgrade.Size = new System.Drawing.Size(92, 25);
            this.btUpgrade.TabIndex = 7;
            this.btUpgrade.Text = "Upgrade";
            this.toolTip1.SetToolTip(this.btUpgrade, "Upgrades a database from version 3.1 to 3.5. After the upgrade, the database will" +
                    " be encrypted if the source database was encrypted");
            this.btUpgrade.UseVisualStyleBackColor = true;
            this.btUpgrade.Click += new System.EventHandler(this.btUpgrade_Click);
            // 
            // btVerify
            // 
            this.btVerify.Location = new System.Drawing.Point(501, 19);
            this.btVerify.Name = "btVerify";
            this.btVerify.Size = new System.Drawing.Size(92, 25);
            this.btVerify.TabIndex = 8;
            this.btVerify.Text = "Verify";
            this.toolTip1.SetToolTip(this.btVerify, "Recalculates the checksums for each page in the database and compares the new che" +
                    "cksums to the expected values");
            this.btVerify.UseVisualStyleBackColor = true;
            this.btVerify.Click += new System.EventHandler(this.btVerify_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.CheckFileExists = false;
            this.openFileDialog1.Filter = "SQL CE databases (*.sdf)|*.sdf|All files|*.*";
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.Location = new System.Drawing.Point(12, 232);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(563, 42);
            this.label3.TabIndex = 15;
            this.label3.Text = "Before every action a copy of the database file will be created by appending 001," +
                " 002, 003 etc to the file name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 30000;
            this.toolTip1.InitialDelay = 200;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 100;
            // 
            // btCreate
            // 
            this.btCreate.Location = new System.Drawing.Point(11, 19);
            this.btCreate.Name = "btCreate";
            this.btCreate.Size = new System.Drawing.Size(92, 25);
            this.btCreate.TabIndex = 0;
            this.btCreate.Text = "Create";
            this.toolTip1.SetToolTip(this.btCreate, "Create a new database file");
            this.btCreate.UseVisualStyleBackColor = true;
            this.btCreate.Click += new System.EventHandler(this.btCreate_Click);
            // 
            // tbFileName
            // 
            this.tbFileName.BackColor = System.Drawing.SystemColors.Window;
            this.tbFileName.Cursor = System.Windows.Forms.Cursors.Default;
            this.tbFileName.Location = new System.Drawing.Point(100, 24);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.ReadOnly = true;
            this.tbFileName.Size = new System.Drawing.Size(438, 20);
            this.tbFileName.TabIndex = 2;
            this.toolTip2.SetToolTip(this.tbFileName, "Database file name. Press the button to the right to select a file");
            this.tbFileName.Enter += new System.EventHandler(this.tbFileName_Enter);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(20, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbUpgradeTo);
            this.groupBox1.Controls.Add(this.cbVersion);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbPassword);
            this.groupBox1.Controls.Add(this.tbFileName);
            this.groupBox1.Controls.Add(this.btSelect);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(579, 140);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Database";
            // 
            // cbUpgradeTo
            // 
            this.cbUpgradeTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUpgradeTo.FormattingEnabled = true;
            this.cbUpgradeTo.Location = new System.Drawing.Point(417, 101);
            this.cbUpgradeTo.Name = "cbUpgradeTo";
            this.cbUpgradeTo.Size = new System.Drawing.Size(121, 21);
            this.cbUpgradeTo.TabIndex = 4;
            this.cbUpgradeTo.SelectedIndexChanged += new System.EventHandler(this.cbUpgradeTo_SelectedIndexChanged);
            // 
            // cbVersion
            // 
            this.cbVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVersion.FormattingEnabled = true;
            this.cbVersion.Location = new System.Drawing.Point(100, 102);
            this.cbVersion.Name = "cbVersion";
            this.cbVersion.Size = new System.Drawing.Size(121, 21);
            this.cbVersion.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(256, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(159, 18);
            this.label5.TabIndex = 3;
            this.label5.Text = "Upgrade to version:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(20, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Version:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btCreate);
            this.groupBox2.Controls.Add(this.btCompact);
            this.groupBox2.Controls.Add(this.btVerify);
            this.groupBox2.Controls.Add(this.btShrink);
            this.groupBox2.Controls.Add(this.btUpgrade);
            this.groupBox2.Controls.Add(this.btRepair);
            this.groupBox2.Location = new System.Drawing.Point(12, 171);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(604, 57);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Action";
            // 
            // toolTip2
            // 
            this.toolTip2.AutoPopDelay = 30000;
            this.toolTip2.InitialDelay = 200;
            this.toolTip2.ReshowDelay = 100;
            // 
            // ToolsForm
            // 
            this.AcceptButton = this.btClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btClose;
            this.ClientSize = new System.Drawing.Size(628, 313);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ToolsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tools";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ToolsForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btSelect;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btCompact;
        private System.Windows.Forms.Button btRepair;
        private System.Windows.Forms.Button btShrink;
        private System.Windows.Forms.Button btUpgrade;
        private System.Windows.Forms.Button btVerify;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.Button btCreate;
        private System.Windows.Forms.ComboBox cbVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbUpgradeTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}