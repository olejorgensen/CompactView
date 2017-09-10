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
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;

namespace CompactView
{
    public partial class ExportForm : Form
    {
        private SqlCeDb db;
        private TreeNode dbNode;

        public ExportForm(SqlCeDb sqlCeDb, TreeNode dbNode)
        {
            InitializeComponent();

            SetCultureTexts();

            db = sqlCeDb;
            this.dbNode = dbNode;
            if (dbNode.ImageIndex == 1) saveFileDialog1.Filter = saveFileDialog1.Filter.Replace("All files|*.*", "CSV files|*.csv|All files|*.*");
            int nItems = FillTreeDb();

            int maxHeight = Screen.PrimaryScreen.WorkingArea.Height - 20;
            int height = nItems * treeDb.ItemHeight + 90;
            if (height < 400) height = 400;
            if (height > maxHeight) height = maxHeight;
            this.Height = height;

            var doubleClickIntercept = new DoubleClickIntercept(treeDb.Handle);  // Disable double click to avoid TreeView double click check bug
        }

        private void SetCultureTexts()
        {
            this.Text = GlobalText.GetValue("Export");
            btnExport.Text = GlobalText.GetValue("Export");
            rbAll.Text = GlobalText.GetValue("All");
            rbSchema.Text = GlobalText.GetValue("OnlySchema");
            rbData.Text = GlobalText.GetValue("OnlyData");
        }

        private int FillTreeDb()
        {
            string tableName = dbNode.ImageIndex == 0 ? null : dbNode.Text;
            treeDb.Nodes.Clear();
            treeDb.Nodes.AddRange(db.GetSchemaNodes(tableName));
            treeDb.ExpandAll();
            return GetTotalNodes(treeDb.Nodes);
        }

        private int GetTotalNodes(TreeNodeCollection nodes)
        {
            int rootNodes = nodes.Count;
            foreach (TreeNode node in nodes) rootNodes += this.GetTotalNodes(node.Nodes);
            return rootNodes ;
        }

        private void RemoveTable(List<string> ddl, string tableName)
        {
            int pos = ddl.FindIndex(cmd => cmd.StartsWith($"CREATE TABLE [{tableName}]"));
            if (pos >= 0) ddl.RemoveAt(pos);

            while ((pos = ddl.FindIndex(cmd => cmd.StartsWith($"ALTER TABLE [{tableName}]"))) >= 0) ddl.RemoveAt(pos);

            string pattern = $@"^CREATE.* INDEX.* ON \[{tableName}\]";
            while ((pos = ddl.FindIndex(cmd => Regex.IsMatch(cmd, pattern))) >= 0) ddl.RemoveAt(pos);

            pattern = $@"^ALTER TABLE.* ADD CONSTRAINT.* FOREIGN KEY.* REFERENCES \[{tableName}\]";
            while ((pos = ddl.FindIndex(cmd => Regex.IsMatch(cmd, pattern, RegexOptions.Singleline))) >= 0) ddl.RemoveAt(pos);
        }

        private void RemoveField(List<string> ddl, string tableName, string fieldName)
        {
            int pos = ddl.FindIndex(cmd => cmd.StartsWith($"CREATE TABLE [{tableName}]"));
            if (pos >= 0)
            {
                ddl[pos] = Regex.Replace(ddl[pos], $@"^\s*\[{fieldName}\].*\r\n", string.Empty, RegexOptions.Multiline);
                ddl[pos] = Regex.Replace(ddl[pos], @",\r\n\)$", "\r\n)");
            }

            string pattern = $@"^ALTER TABLE \[{tableName}\].* PRIMARY KEY (.*\[{fieldName}\].*)";
            while ((pos = ddl.FindIndex(cmd => Regex.IsMatch(cmd, pattern))) >= 0) ddl.RemoveAt(pos);

            pattern = $@"^CREATE.* INDEX.* ON \[{tableName}\] \(.*\[{fieldName}\].*\)";
            while ((pos = ddl.FindIndex(cmd => Regex.IsMatch(cmd, pattern))) >= 0) ddl.RemoveAt(pos);

            pattern = $@"^ALTER TABLE \[{tableName}\] ADD CONSTRAINT.* FOREIGN KEY \(.*\[{fieldName}\].*\)";
            while ((pos = ddl.FindIndex(cmd => Regex.IsMatch(cmd, pattern))) >= 0) ddl.RemoveAt(pos);

            pattern = $@"^ALTER TABLE.* ADD CONSTRAINT.* FOREIGN KEY.* REFERENCES \[{tableName}\] \(.*\[{fieldName}\].*\)";
            while ((pos = ddl.FindIndex(cmd => Regex.IsMatch(cmd, pattern, RegexOptions.Singleline))) >= 0) ddl.RemoveAt(pos);
        }

        private string RemoveUncheck(List<string> ddl, TreeView treeView)
        {
            foreach (TreeNode node in treeView.Nodes)
            {
                if (node.Checked)
                {
                    foreach (TreeNode n in node.Nodes) if (!n.Checked) RemoveField(ddl, node.Text, n.Text);
                }
                else
                {
                    RemoveTable(ddl, node.Text);
                }
            }

            var sb = new StringBuilder();
            foreach (string s in ddl) sb.AppendLine($"{s};{Environment.NewLine}");
            return sb.ToString();
        }

        private string GetExportTablesSchema(SqlCeDb db, TreeView treeView)
        {
            var ddl = new List<string>(Regex.Split(db.GetDatabaseDdl(), @";\s*").Where(s => !string.IsNullOrWhiteSpace(s)));
            for (int i = ddl.Count - 1; i >= 0; i--) if (!ddl[i].StartsWith("CREATE TABLE ")) ddl.RemoveAt(i);
            return RemoveUncheck(ddl, treeView);
        }

        private string GetExportConstraintsSchema(SqlCeDb db, TreeView treeView)
        {
            var ddl = new List<string>(Regex.Split(db.GetDatabaseDdl(), @";\s*").Where(s => !string.IsNullOrWhiteSpace(s)));
            for (int i = ddl.Count - 1; i >= 0; i--) if (ddl[i].StartsWith("CREATE TABLE ")) ddl.RemoveAt(i);
            return RemoveUncheck(ddl, treeView);
        }

        private string ChangeIdentity(string tablesDdl)
        {
            var ddl = new List<string>(Regex.Split(tablesDdl, @";\s*").Where(s => !string.IsNullOrWhiteSpace(s)));
            for (int i = 0; i < ddl.Count; i++)
            {
                string identity = null;
                Match match = Regex.Match(ddl[i], @"(?<=^CREATE TABLE \[)[^\]]*(?=\])", RegexOptions.Singleline);
                if (match.Success) identity = db.GetAutoincNext(match.Value);
                if (identity != null) ddl[i] = Regex.Replace(ddl[i], @"(?<=\r\n\ *\[.*\].* IDENTITY \()\d+(?=,)", identity, RegexOptions.Singleline);
            }

            var sb = new StringBuilder();
            foreach (string s in ddl) sb.AppendLine($"{s};{Environment.NewLine}");
            return sb.ToString();
        }

        private string GetExportData(SqlCeDb db, TreeView treeView)
        {
            var ddl = new StringBuilder();

            foreach (TreeNode node in treeView.Nodes)
            {
                var sb = new StringBuilder();
                int count = 0;
                if (node.Checked) foreach (TreeNode n in node.Nodes)
                {
                    if (n.Checked)
                    {
                        sb.Append($"[{n.Text}], ");
                        count++;
                    }
                }
                if (count == 0) continue;

                string schema = db.GetTableDdl(node.Text, true, false, false, false);
                string identity = db.GetAutoincNext(node.Text);
                if (identity != null) ddl.AppendLine($"SET IDENTITY_INSERT [{node.Text}] ON;{Environment.NewLine}");

                string fields = sb.ToString().TrimEnd(',', ' ');
                string sqlSelect = $"SELECT {fields} FROM [{node.Text}]";
                var dr = (IDataReader)db.ExecuteSql(sqlSelect, false);
                object[] values = new object[count];
                while (dr.Read())
                {
                    ddl.Append($"INSERT INTO [{node.Text}] ({fields}) VALUES (");
                    dr.GetValues(values);
                    for (int i = 0; i < count; i++)
                    {
                        string s;
                        if (dr.IsDBNull(i))
                        {
                            s = "NULL";
                        }
                        else
                        {
                            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
                            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                            Type t = values[i].GetType();
                            bool numeric = t == typeof(Byte) || t == typeof(Int16) || t == typeof(Int32) ||
                                t == typeof(Int64) || t == typeof(Decimal) || t == typeof(Double) || t == typeof(Single);
                            if (t == typeof(Byte[]))
                            {
                                s = $"0x{BitConverter.ToString(values[i] as Byte[]).Replace("-", string.Empty)}";
                            }
                            else if (t == typeof(DateTime))
                            {
                                s = $"'{((DateTime)values[i]).ToString("yyyy.MM.dd HH:mm:ss.fff")}'";
                            }
                            else
                            {
                                s = numeric ? values[i].ToString() : $"'{values[i].ToString().Replace("'", "''")}'";
                            }
                            Thread.CurrentThread.CurrentCulture = ci;
                        }
                        ddl.Append(s);
                        if (i < count - 1) ddl.Append(", ");
                    }
                    ddl.AppendLine($");{Environment.NewLine}");
                }
                dr.Close();

                if (identity != null) ddl.AppendLine($"SET IDENTITY_INSERT [{node.Text}] OFF;{Environment.NewLine}");
            }
            return ddl.ToString();
        }

        private string GetCsvExportData(SqlCeDb db, TreeView treeView, bool titles, bool data)
        {
            int ncount = 0;
            foreach (TreeNode node in treeView.Nodes) if (node.Checked) ncount++;
            if (ncount != 1) return null;

            var csv = new StringBuilder();

            foreach (TreeNode node in treeView.Nodes)
            {
                var sb = new StringBuilder();
                var sbTitles = new StringBuilder();
                int count = 0;
                if (node.Checked) foreach (TreeNode n in node.Nodes)
                {
                    if (n.Checked)
                    {
                        sbTitles.Append($"\"{n.Text}\",");
                        sb.Append($"[{n.Text}], ");
                        count++;
                    }
                }
                if (count == 0) continue;
                if (titles) csv.AppendLine(sbTitles.ToString().TrimEnd(','));
                if (!data) continue;

                string fields = sb.ToString().TrimEnd(',', ' ');
                string sqlSelect = $"SELECT {fields} FROM [{node.Text}]";
                var dr = (IDataReader)db.ExecuteSql(sqlSelect, false);
                object[] values = new object[count];
                while (dr.Read())
                {
                    dr.GetValues(values);
                    for (int i = 0; i < count; i++)
                    {
                        string s;
                        if (dr.IsDBNull(i))
                        {
                            s = string.Empty;
                        }
                        else
                        {
                            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
                            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
                            Type t = values[i].GetType();
                            bool numeric = t == typeof(Byte) || t == typeof(Int16) || t == typeof(Int32) ||
                                t == typeof(Int64) || t == typeof(Decimal) || t == typeof(Double) || t == typeof(Single);
                            if (t == typeof(Byte[]))
                            {
                                s = $"0x{BitConverter.ToString(values[i] as Byte[]).Replace("-", string.Empty)}";
                            }
                            else if (t == typeof(DateTime))
                            {
                                s = ((DateTime)values[i]).ToString("yyyy.MM.dd HH:mm:ss.fff");
                            }
                            else
                            {
                                s = numeric ? values[i].ToString() : values[i].ToString().Replace("\"", "''");
                            }
                            Thread.CurrentThread.CurrentCulture = ci;
                        }
                        csv.Append($"\"{s}\"");
                        if (i < count - 1) csv.Append(",");
                    }
                    csv.AppendLine();
                }
                dr.Close();
            }
            return csv.ToString();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(saveFileDialog1.FileName))
            {
                saveFileDialog1.InitialDirectory = Path.GetDirectoryName(db.FileName);
                string s = dbNode.ImageIndex == 0 ? string.Empty : $"_{dbNode.Text}";
                saveFileDialog1.FileName = $"{Path.GetFileNameWithoutExtension(db.FileName)}{s}_export.sql";
            }
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            var writer = new StreamWriter(saveFileDialog1.FileName, false, Encoding.UTF8);
            if (dbNode.ImageIndex == 1 && saveFileDialog1.FilterIndex == 3)  // CSV
            {
                writer.Write(GetCsvExportData(db, treeDb, rbAll.Checked || rbSchema.Checked, rbAll.Checked || rbData.Checked));
            }
            else
            {
                if (rbAll.Checked || rbSchema.Checked)
                {
                    string ddl = GetExportTablesSchema(db, treeDb);
                    if (rbAll.Checked) writer.Write(ChangeIdentity(ddl)); else writer.Write(ddl);
                }
                if (rbAll.Checked || rbData.Checked) writer.Write(GetExportData(db, treeDb));
                if (rbAll.Checked || rbSchema.Checked) writer.Write(GetExportConstraintsSchema(db, treeDb));
            }
            writer.Close();
            this.Close();
        }

        private void treeDb_AfterCheck(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode node in e.Node.Nodes) node.Checked = e.Node.Checked;

            if (e.Node.Checked && e.Node.Parent != null)
            {
                treeDb.AfterCheck -= treeDb_AfterCheck;
                e.Node.Parent.Checked = true;
                treeDb.AfterCheck += treeDb_AfterCheck;
            }
        }

        private void treeDb_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;  // Disable collapse
        }

        private void treeDb_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;  // Disable select to allow hide selection
        }

        private void treeDb_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Parent == null || !e.Node.Checked) return;
            TreeNode[] nodes = treeDb.Nodes.Find($"{e.Node.Parent.Text}@{e.Node.Text}", true);
            if (nodes.Length <= 0 || nodes[0].Parent == null) return;

            bool used = false;
            for (int i = 0; i < nodes.Length; i++) if (nodes[i].Checked) used = true;
            if (!used) return;

            string txt = $"[{e.Node.Parent.Text}]-[{e.Node.Text}] {GlobalText.GetValue("UsedAsAForeignKey")}{Environment.NewLine}[{nodes[0].Parent.Text}]-[{nodes[0].Text}].";
            GlobalText.ShowWarning(txt);
        }
    }

    class DoubleClickIntercept : System.Windows.Forms.NativeWindow
    {
        public DoubleClickIntercept(IntPtr hWnd)
        {
            this.AssignHandle(hWnd);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_LBUTTONDBLCLK = 0x203;

            if (m.Msg == WM_LBUTTONDBLCLK) m.Result = IntPtr.Zero; else base.WndProc(ref m);
        }
    }
}
