/**************************************************************************
Copyright (C) 2011-2014 Iván Costales Suárez

This file is part of CompactView.

CompactView is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

CompactView is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with CompactView.  If not, see <http://www.gnu.org/licenses/>.

CompactView web site <http://sourceforge.net/p/compactview/>.
**************************************************************************/
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace CompactView
{
    public partial class ToolsForm : Form
    {

        private SqlCeDb db = new SqlCeDb();
        private string openPassword = null;

        public ToolsForm(string fileName, string password)
        {
            InitializeComponent();

            SetCultureTexts();
            foreach (Version version in SqlCeDb.AvailableVersions) cbVersion.Items.Add(version);
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName)) LoadDatabase(fileName, password);
            SetActiveButtons();
        }

        private void SetActiveButtons()
        {
            btCompact.Enabled = btRepair.Enabled = btShrink.Enabled = btVerify.Enabled = db.IsOpen;
            cbUpgradeTo.Enabled = btCompact.Enabled && cbUpgradeTo.Items.Count > 0;
            btUpgrade.Enabled = cbUpgradeTo.Enabled && cbUpgradeTo.SelectedIndex >= 0;
            btCreate.Enabled = cbVersion.Enabled = !db.IsOpen && !string.IsNullOrEmpty(tbFileName.Text) && !File.Exists(tbFileName.Text);
            if (btCreate.Enabled) cbVersion.SelectedIndex = SqlCeDb.AvailableVersions.Length - 1;
        }

        private void SetCultureTexts()
        {
            this.Text = GlobalText.GetValue("Tools");
            groupBox1.Text = GlobalText.GetValue("Database");
            label1.Text = GlobalText.GetValue("FileName") + ":";
            label2.Text = GlobalText.GetValue("Password") + ":";
            label4.Text = GlobalText.GetValue("Version") + ":";
            toolTip2.SetToolTip(tbFileName, GlobalText.GetValue("FileNameTip"));
            toolTip2.SetToolTip(tbPassword, GlobalText.GetValue("PasswordTip"));
            toolTip2.SetToolTip(btSelect, GlobalText.GetValue("SelectTip"));
            label5.Text = GlobalText.GetValue("UpgradeToVersion") + ":";
            groupBox2.Text = GlobalText.GetValue("Action");
            btCreate.Text = GlobalText.GetValue("Create");
            btCompact.Text = GlobalText.GetValue("Compact");
            btRepair.Text = GlobalText.GetValue("Repair");
            btShrink.Text = GlobalText.GetValue("Shrink");
            btUpgrade.Text = GlobalText.GetValue("Upgrade");
            btVerify.Text = GlobalText.GetValue("Verify");
            toolTip1.SetToolTip(btCreate, GlobalText.GetValue("CreateTip"));
            toolTip1.SetToolTip(btCompact, GlobalText.GetValue("CompactTip"));
            toolTip1.SetToolTip(btRepair, GlobalText.GetValue("RepairTip"));
            toolTip1.SetToolTip(btShrink, GlobalText.GetValue("ShrinkTip"));
            toolTip1.SetToolTip(btUpgrade, GlobalText.GetValue("UpgradeTip"));
            toolTip1.SetToolTip(btVerify, GlobalText.GetValue("VerifyTip"));
            label3.Text = GlobalText.GetValue("ToolsNote");
            btClose.Text = GlobalText.GetValue("Close");
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            string fileName = openFileDialog1.FileName;
            tbFileName.Text = fileName;

            db.Close();
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName)) LoadDatabase(fileName, null);
            SetActiveButtons();
        }

        private void LoadDatabase(string fileName, string password)
        {
            tbFileName.Text = fileName;
            tbPassword.Text = password == null ? "" : password;
            if (db.Open(fileName, password))
            {
                cbVersion.Text = db.Version.SqlceVersion;
                openPassword = password;
                cbUpgradeTo.Items.Clear();
                bool ok = false;
                foreach (Version version in SqlCeDb.AvailableVersions) if (ok) cbUpgradeTo.Items.Add(version); else if (version == db.Version) ok = true;
                if (cbUpgradeTo.Items.Count == 1) cbUpgradeTo.SelectedIndex = 0;
            }
            else
            {
                if (db.BadPassword)
                {
                    db.Close();
                    GetPassForm form = new GetPassForm();
                    if (form.ShowDialog() == DialogResult.OK) LoadDatabase(fileName, form.edPass.Text.Trim());
                }
            }
        }

        private bool CreateBackup()
        {
            if (string.IsNullOrEmpty(db.FileName))
            {
                GlobalText.ShowError("NoDatabaseSelect");
                return false;
            }

            if (!File.Exists(db.FileName))
            {
                GlobalText.ShowError("DatabaseMissing");
                return false;
            }

            string backupFile = "";
            int i = 1;

            try
            {
                while (File.Exists(backupFile = Path.ChangeExtension(db.FileName, "." + i.ToString("0000") + Path.GetExtension(db.FileName)))) i++;
                File.Copy(db.FileName, backupFile);
                return true;
            }
            catch (Exception ex)
            {
                GlobalText.ShowError("BackupError", ex.Message);
                return false;
            }
        }

        private void btCreate_Click(object sender, EventArgs e)
        {
            bool ok = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                ok = db.CreateDatabase(tbFileName.Text, tbPassword.Text.Trim(), (Version)cbVersion.SelectedItem);
                if (ok) GlobalText.ShowInfo("CreateDone"); else GlobalText.ShowError("CreateError", db.LastError);
            }
            catch (Exception ex)
            {
                GlobalText.ShowError("CreateError", ex.Message);
            }
            this.Cursor = Cursors.Default;

            if (ok) LoadDatabase(tbFileName.Text, tbPassword.Text.Trim());
            SetActiveButtons();
        }

        private void btCompact_Click(object sender, EventArgs e)
        {
            if (!CreateBackup()) return;

            bool ok = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                ok = db.Compact(tbFileName.Text, openPassword, tbPassword.Text.Trim());
                if (ok) GlobalText.ShowInfo("CompactDone"); else GlobalText.ShowError("CompactError", db.LastError);
            }
            catch (Exception ex)
            {
                GlobalText.ShowError("CompactError", ex.Message);
            }
            this.Cursor = Cursors.Default;

            if (ok) LoadDatabase(tbFileName.Text, tbPassword.Text.Trim());
            SetActiveButtons();
        }

        private void btRepair_Click(object sender, EventArgs e)
        {
            if (!CreateBackup()) return;

            bool ok = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                ok = db.Repair(tbFileName.Text, openPassword, tbPassword.Text.Trim());
                if (ok) GlobalText.ShowInfo("RepairDone"); else GlobalText.ShowError("RepairError", db.LastError);
            }
            catch (Exception ex)
            {
                GlobalText.ShowError("RepairError", ex.Message);
            }
            this.Cursor = Cursors.Default;

            if (ok) LoadDatabase(tbFileName.Text, tbPassword.Text.Trim());
            SetActiveButtons();
        }

        private void btShrink_Click(object sender, EventArgs e)
        {
            if (!CreateBackup()) return;

            bool ok = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                ok = db.Shrink(tbFileName.Text, openPassword, tbPassword.Text.Trim());
                if (ok) GlobalText.ShowInfo("ShrinkDone"); else GlobalText.ShowError("ShrinkError", db.LastError);
            }
            catch (Exception ex)
            {
                GlobalText.ShowError("ShrinkError", ex.Message);
            }
            this.Cursor = Cursors.Default;

            if (ok) LoadDatabase(tbFileName.Text, tbPassword.Text.Trim());
            SetActiveButtons();
        }

        private void btUpgrade_Click(object sender, EventArgs e)
        {

            if (!CreateBackup()) return;

            bool ok = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                ok = db.Upgrade(tbFileName.Text, openPassword, tbPassword.Text.Trim(), (Version)cbUpgradeTo.SelectedItem);
                if (ok) GlobalText.ShowInfo("UpgradeDone"); else GlobalText.ShowError("UpgradeError", db.LastError);
            }
            catch (Exception ex)
            {
                GlobalText.ShowError("UpgradeError", ex.Message);
            }
            this.Cursor = Cursors.Default;

            if (ok) LoadDatabase(tbFileName.Text, tbPassword.Text.Trim());
            SetActiveButtons();
        }

        private void btVerify_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (db.Verify(tbFileName.Text, tbPassword.Text.Trim())) GlobalText.ShowInfo("VerifyDone");
                else if (db.LastError == "") GlobalText.ShowError("VerifyFault"); else GlobalText.ShowError("VerifyError", db.LastError);
            }
            catch (Exception ex)
            {
                GlobalText.ShowError("VerifyError", ex.Message);
            }
            db.Close();
            this.Cursor = Cursors.Default;

            LoadDatabase(tbFileName.Text, tbPassword.Text.Trim());
        }

        private void tbFileName_Enter(object sender, EventArgs e)
        {
            // File name text box is read only, the file is selected by pressing the button at the right
            btSelect.Focus();
        }

        private void cbUpgradeTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            btUpgrade.Enabled = cbUpgradeTo.SelectedIndex >= 0;
        }

        private void ToolsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            db.Close();
        }
    }
}
