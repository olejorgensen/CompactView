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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.Common;

namespace CompactView
{
    public partial class ImportForm : Form
    {
        private SqlCeDb db;
        private bool abort = false;
        private List<string> ddl;

        public ImportForm(SqlCeDb sqlCeDb, string fileName)
        {
            InitializeComponent();

            SetCultureTexts();
            statusStrip1_SizeChanged(null, null);

            db = sqlCeDb;
            FillTreeDb(fileName);
        }

        private void SetCultureTexts()
        {
            this.Text = GlobalText.GetValue("Import");
            btnImport.Text = GlobalText.GetValue("Import");
            cbSchema.Text = GlobalText.GetValue("Schema");
            cbData.Text = GlobalText.GetValue("Data");
        }

        private void FillTreeDb(string fileName)
        {
            bool schema = false;
            bool data = false;
            bool ok = false;
            string cmdError = null;
            treeDb.Nodes.Clear();
            treeDb.BeginUpdate();
            var reader = new StreamReader(fileName);
            ddl = new List<string>();
            // Regular expression to search texts finished with semicolons that is not between single quotes
            for (Match m = Regex.Match(reader.ReadToEnd(), @"(?:[^;']|'[^']*')+;\s*", RegexOptions.Compiled); m.Success; m = m.NextMatch()) ddl.Add(m.Value.TrimEnd('\r', '\n'));

            RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase;
            var regexCreateTable = new Regex(@"(?<=^CREATE\s+TABLE\s+\[)[^\]]*(?=\]\s+\([^\)]*\))", options);
            var regexInsert = new Regex(@"(?<=^INSERT\s+INTO\s+\[)[^\]]*(?=\]\s+\([^\)]*\)\s+VALUES\s+\([^\)]*\))", options);
            var regexAlterTable = new Regex(@"^ALTER\s+TABLE\s+\[", options);
            var regexCreateIndex = new Regex(@"^CREATE\s+.*INDEX\s+", options);
            var regexSetIdentity = new Regex(@"^SET\s+IDENTITY_INSERT\s+\[", options);

            for (int i = 0; i < ddl.Count; i++)
            {
                ok = false;
                Match match1 = regexCreateTable.Match(ddl[i]);
                if (match1.Success)
                {
                    treeDb.Nodes.Add(match1.Value, match1.Value, 0, 0).Checked = true;
                    schema = true;
                    ok = true;
                }
                Match match2 = regexInsert.Match(ddl[i]);
                if (match2.Success)
                {
                    if (!treeDb.Nodes.ContainsKey(match2.Value)) treeDb.Nodes.Add(match2.Value, match2.Value, 0, 0).Checked = true;
                    data = true;
                    ok = true;
                }
                if (!ok)
                {
                    if (regexAlterTable.IsMatch(ddl[i]) | regexCreateIndex.IsMatch(ddl[i]) | regexSetIdentity.IsMatch(ddl[i])) ok = true;
                    if (!ok)
                    {
                        cmdError = ddl[i].Length <= 500 ? ddl[i] : ddl[i].Remove(500) + "...";
                        break;
                    }
                }
            }

            treeDb.EndUpdate();
            treeDb.ExpandAll();
            cbSchema.Enabled = cbSchema.Checked = schema;
            cbData.Enabled = cbData.Checked = data;

            if (!ok)
            {
                GlobalText.ShowError("ImportFormatError", cmdError);
                abort = true;
            }
        }

        private void RemoveTable(string tableName)
        {
            string pattern = @"^CREATE\s+TABLE\s+\[" + tableName + @"\]";
            int pos = ddl.FindIndex(cmd => Regex.IsMatch(cmd, pattern, RegexOptions.Singleline));
            if (pos >= 0) ddl.RemoveAt(pos);

            pattern = @"^ALTER\s+TABLE\s+\[" + tableName + @"\]";
            while ((pos = ddl.FindIndex(cmd => Regex.IsMatch(cmd, pattern, RegexOptions.Singleline))) >= 0) ddl.RemoveAt(pos);

            pattern = @"^CREATE.*\s+INDEX.*\s+ON\s+\[" + tableName + @"\]";
            while ((pos = ddl.FindIndex(cmd => Regex.IsMatch(cmd, pattern, RegexOptions.Singleline))) >= 0) ddl.RemoveAt(pos);

            pattern = @"^ALTER\s+TABLE.*\s+ADD\s+CONSTRAINT.*\s+FOREIGN\s+KEY.*\s+REFERENCES\s+\[" + tableName + @"\]";
            while ((pos = ddl.FindIndex(cmd => Regex.IsMatch(cmd, pattern, RegexOptions.Singleline))) >= 0) ddl.RemoveAt(pos);

            pattern = @"^INSERT\s+INTO\s+\[" + tableName + @"\]";
            while ((pos = ddl.FindIndex(cmd => Regex.IsMatch(cmd, pattern, RegexOptions.Singleline))) >= 0) ddl.RemoveAt(pos);

            pattern = @"^SET\s+IDENTITY_INSERT\s+\[" + tableName + @"\]";
            while ((pos = ddl.FindIndex(cmd => Regex.IsMatch(cmd, pattern, RegexOptions.Singleline))) >= 0) ddl.RemoveAt(pos);
        }

        private void FilterDdl()
        {
            foreach (TreeNode node in treeDb.Nodes) if (!node.Checked) RemoveTable(node.Text);

            string patternInsert = @"^INSERT\s+INTO\s+\[.*\]";
            string patternIdentity = @"^SET\s+IDENTITY_INSERT\s+\[.*\]";
            for (int i = ddl.Count - 1; i >= 0; i--)
            {
                bool insert = Regex.IsMatch(ddl[i], patternInsert, RegexOptions.Singleline);
                bool identity = Regex.IsMatch(ddl[i], patternIdentity, RegexOptions.Singleline);
                if ((!cbSchema.Checked && !insert && !identity) || (!cbData.Checked && insert) || (!cbData.Checked && identity) ) ddl.RemoveAt(i);
            }
        }

        protected override void SetVisibleCore(bool value)
        {
            // Prevents the form is displayed if abort is true
            base.SetVisibleCore(abort ? false : value);
            if (abort) DialogResult = DialogResult.Abort;
        }

        private void treeDb_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;  // Disable select to allow hide selection
        }

        private void treeDb_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (treeDb.HitTest(e.Location).Location == TreeViewHitTestLocations.Label) e.Node.Checked = !e.Node.Checked;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            bool anyTableExists = false;
            bool allTablesExists = true;
            foreach (TreeNode node in treeDb.Nodes) if (node.Checked)
            {
                bool exists = db.TableNames.Contains(node.Text, StringComparer.InvariantCultureIgnoreCase);
                if (exists) anyTableExists = true; else allTablesExists = false;
            }

            if (cbSchema.Checked && anyTableExists)
            {
                GlobalText.ShowError("ErrorImportingTablesSchema");
                DialogResult = DialogResult.Abort;
                return;
            }

            if (cbData.Checked && !cbSchema.Checked && !allTablesExists)
            {
                GlobalText.ShowError("ErrorImportingTablesData");
                DialogResult = DialogResult.Abort;
                return;
            }

            FilterDdl();

            toolStripProgressBar1.Value = 0;
            toolStripProgressBar1.Maximum = ddl.Count;
            DbCommand cmd = db.Connection.CreateCommand();
            cmd.CommandType = CommandType.Text;

            try
            {
                foreach (string sql in ddl)
                {
                    toolStripProgressBar1.Increment(1);
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                GlobalText.ShowInfo("ImportOk");
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                string sql = cmd.CommandText.Length <= 500 ? cmd.CommandText : cmd.CommandText.Remove(500) + "...";
                GlobalText.ShowError("ImportError", ex.Message + Environment.NewLine + Environment.NewLine + sql);
                DialogResult = DialogResult.Abort;
            }
        }

        private void statusStrip1_SizeChanged(object sender, EventArgs e)
        {
            toolStripProgressBar1.Width = statusStrip1.Width - 20;
        }
    }
}
