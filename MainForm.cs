/**************************************************************************
Copyright (C) 2011-2017 Iván Costales Suárez

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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace CompactView
{
    public partial class MainForm : Form
    {
        private const string formText = "CompactView";
        private SqlParser sqlParserQuery = new SqlParser();
        private SqlParser sqlParserDdl = new SqlParser();
        private SqlCeDb db = new SqlCeDb();
        private Settings settings = new Settings();

        // It is used to find the words "create", "drop" or "alter" that are not quoted
        //private Regex regexCreateAlterDrop = new Regex(@"(?<=^(([^']*'[^']*'[^']*)*|[^']*))\b(create|alter|drop)\b", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);
        private Regex regexDropQuotesAndBrackets = new Regex(@"'[^']*'|\[[^\]]*\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private Regex regexCreateAlterDrop = new Regex("create|alter|drop", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private Regex regexInsertUpdateDelete = new Regex("insert|update|delete", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public MainForm()
        {
            InitializeComponent();

            Text = formText;

            splitterHorizontal.Panel1Collapsed = true;
            sqlParserQuery.RichTextBox = rtbQuery;
            sqlParserDdl.RichTextBox = rtbDdl;
            LoadSettings();

            // Set culture code to the default current culture
            GlobalText.CultureCode = Thread.CurrentThread.CurrentUICulture.Name;
            SetCultureTexts();
            UpdateStatus();
        }

        private void LoadSettings()
        {
            settings.Load();
            Left = settings.X >= 0 ? settings.X : 0;
            Top = settings.Y >= 0 ? settings.Y : 0;
            Width = settings.Width >= 580 ? settings.Width : 580;
            Height = settings.Height >= 380 ? settings.Height : 380;
            WindowState = settings.Maximized ? FormWindowState.Maximized : FormWindowState.Normal;
            if (settings.TextColor1 == 0 && settings.TextColor2 == 0 && settings.BackColor1 == 0 && settings.BackColor2 == 0)
            {
                settings.TextColor1 = Color.Black.ToArgb();
                settings.TextColor2 = Color.Black.ToArgb();
                settings.BackColor1 = Color.LightYellow.ToArgb();
                settings.BackColor2 = Color.LemonChiffon.ToArgb();
            }
            OptionsForm.SelectColors(dataGrid, settings, settings.ColorSet);
            UpdateRecentFilesMenu();
        }

        private void SaveSettings()
        {
            if (WindowState != FormWindowState.Maximized)
            {
                settings.X = Left >= 0 ? Left : 0;
                settings.Y = Top >= 0 ? Top : 0;
                settings.Width = Width >= 580 ? Width : 580;
                settings.Height = Height >= 380 ? Height : 380;
            }
            settings.Maximized = WindowState == FormWindowState.Maximized;
            settings.Save();
        }

        private void UpdateRecentFilesMenu()
        {
            EventHandler e = new EventHandler(RecentFiles_Click);
            recentFilesMenuItem.DropDownItems.Clear();
            foreach (string fileName in settings.RecentFiles)
                recentFilesMenuItem.DropDownItems.Add(fileName, images.Images[0], e);
        }

        private void RecentFiles_Click(object sender, EventArgs e)
        {
            string fileName = ((ToolStripMenuItem)sender).Text;
            LoadDatabase(fileName);
        }

        private void SetCultureTexts()
        {
            btnOpen.Text = GlobalText.GetValue("OpenDatabase");
            cbReadOnly.Items[0] = GlobalText.GetValue("ReadOnly");
            cbReadOnly.Items[1] = GlobalText.GetValue("AllowEditing");
            btnQuery.Text = GlobalText.GetValue("Query");
            btnExecute.Text = GlobalText.GetValue("Execute");
            btnClear.Text = GlobalText.GetValue("Clear");
            btnCut.Text = GlobalText.GetValue("Cut");
            btnCopy.Text = GlobalText.GetValue("Copy");
            btnPaste.Text = GlobalText.GetValue("Paste");
            tabControl1.TabPages[0].Text = GlobalText.GetValue("Data");
            tabControl1.TabPages[1].Text = GlobalText.GetValue("SqlSchema");
            lbResult.Text = GlobalText.GetValue("QueryNote");
            cutToolStripMenuItem.Text = GlobalText.GetValue("Cut");
            copyToolStripMenuItem.Text = GlobalText.GetValue("Copy");
            pasteToolStripMenuItem.Text = GlobalText.GetValue("Paste");
            loadFromFileToolStripMenuItem.Text = $"{GlobalText.GetValue("LoadFromFile")}...";
            saveToFileToolStripMenuItem.Text = $"{GlobalText.GetValue("SaveToFile")}...";
            printToolStripMenuItem.Text = $"{GlobalText.GetValue("Print")}...";
            fileMenuItem.Text = GlobalText.GetValue("File");
            openDatabaseMenuItem.Text = $"{GlobalText.GetValue("OpenDatabase")}...";
            recentFilesMenuItem.Text = GlobalText.GetValue("RecentFiles");
            allowEditingMenuItem.Text = GlobalText.GetValue("AllowEditing");
            importMenuItem.Text = $"{GlobalText.GetValue("Import")}...";
            exportMenuItem.Text = $"{GlobalText.GetValue("Export")}...";
            exitMenuItem.Text = GlobalText.GetValue("Exit");
            editMenuItem.Text = GlobalText.GetValue("Edit");
            cutMenuItem.Text = GlobalText.GetValue("Cut");
            copyMenuItem.Text = GlobalText.GetValue("Copy");
            pasteMenuItem.Text = GlobalText.GetValue("Paste");
            deleteMenuItem.Text = GlobalText.GetValue("Delete");
            queryMenuItem.Text = GlobalText.GetValue("Query");
            showEditorMenuItem.Text = GlobalText.GetValue("ShowEditor");
            executeMenuItem.Text = GlobalText.GetValue("Execute");
            clearMenuItem.Text = GlobalText.GetValue("Clear");
            toolsMenuItem.Text = GlobalText.GetValue("Tools");
            databaseToolsMenuItem.Text = $"{GlobalText.GetValue("DatabaseTools")}...";
            optionsMenuItem.Text = $"{GlobalText.GetValue("Options")}...";
            helpMenuItem.Text = GlobalText.GetValue("Help");
            aboutCompactViewMenuItem.Text = $"{GlobalText.GetValue("About")} CompactView";
            loadSqlMenuItem.Text = $"{GlobalText.GetValue("LoadSqlQuery")}...";
            saveSqlMenuItem.Text = $"{GlobalText.GetValue("SaveSqlQuery")}...";
            saveSchemaMenuItem.Text = $"{GlobalText.GetValue("SaveSqlSchema")}...";
            closeDatabaseMenuItem.Text = GlobalText.GetValue("CloseDatabase");
            printMenuItem.Text = $"{GlobalText.GetValue("Print")}...";
            previewMenuItem.Text = GlobalText.GetValue("Preview");
        }

        private DataTable DatabaseInfoLocale(DataTable table)
        {
            if (table.Columns.Count != 2)
                return table;

            table.Columns[0].ColumnName = GlobalText.GetValue("Property");
            table.Columns[1].ColumnName = GlobalText.GetValue("Value");

            foreach (DataRow row in table.Rows)
            {
                string key = row[0].ToString().Replace(" ", string.Empty);
                string s = GlobalText.GetValue(key);
                if (!string.IsNullOrEmpty(s))
                    row[0] = s;
                if (key == "CaseSensitive")
                {
                    s = GlobalText.GetValue(row[1].ToString());
                    if (!string.IsNullOrEmpty(s))
                        row[1] = s;
                }
            }
            return table;
        }

        private void UpdateStatus()
        {
            btnQuery.Enabled = showEditorMenuItem.Enabled = cutMenuItem.Enabled = copyMenuItem.Enabled = pasteMenuItem.Enabled = db.IsOpen;
            btnExecute.Enabled = btnClear.Enabled = executeMenuItem.Enabled = clearMenuItem.Enabled = btnQuery.Enabled && btnQuery.Checked;
            closeDatabaseMenuItem.Enabled = exportMenuItem.Enabled = importMenuItem.Enabled = db.IsOpen;

            bool queryFocused = btnQuery.Enabled && btnQuery.Checked && rtbQuery.Focused;
            btnCut.Enabled = cutMenuItem.Enabled = deleteMenuItem.Enabled = queryFocused && rtbQuery.SelectionLength > 0;
            btnCopy.Enabled = copyMenuItem.Enabled = db.IsOpen && (queryFocused || tabControl1.SelectedIndex == 1);
            btnPaste.Enabled = pasteMenuItem.Enabled = queryFocused && rtbQuery.CanPaste(DataFormats.GetFormat(DataFormats.Text));

            loadSqlMenuItem.Enabled = saveSqlMenuItem.Enabled = btnQuery.Enabled && btnQuery.Checked;
            saveSchemaMenuItem.Enabled = db.IsOpen;

            printMenuItem.Enabled = previewMenuItem.Enabled = btnCopy.Enabled;
        }

        /// <summary>
        /// Load database without password
        /// </summary>
        /// <param name="fileName">Database file name</param>
        private void LoadDatabase(string fileName)
        {
            LoadDatabase(fileName, null);
        }

        /// <summary>
        /// Load database with password
        /// </summary>
        /// <param name="fileName">Database file name</param>
        /// <param name="password">Database password</param>
        private void LoadDatabase(string fileName, string password)
        {
            Reset();

            try
            {
                if (!File.Exists(fileName))
                    throw new Exception($"{GlobalText.GetValue("FileNotFound")}: '{fileName}'");

                Cursor = Cursors.WaitCursor;

                if (db.Open(fileName, password))
                {
                    Text = string.Concat(formText, " - ", db.FileName);

                    // Fill tree with database name and table names
                    treeDb.BeginUpdate();
                    treeDb.Nodes.Clear();
                    TreeNode main = treeDb.Nodes.Add("Database", Path.GetFileNameWithoutExtension(fileName), 0, 0);
                    foreach (string tableName in db.TableNames)
                        main.Nodes.Add(tableName, tableName, 1, 1);
                    main.Expand();
                    treeDb.EndUpdate();
                    treeDb.SelectedNode = treeDb.Nodes[0];
                    settings.AddToRecentFiles(fileName);
                    UpdateRecentFilesMenu();
                }
                else
                {
                    bool badPassword = db.BadPassword;
                    Reset();
                    if (badPassword)
                    {
                        var form = new GetPassForm();
                        if (form.ShowDialog() == DialogResult.OK)
                            LoadDatabase(fileName, form.edPass.Text.Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                GlobalText.ShowError("UnableToOpen", ex.Message);
                btnQuery.Enabled = btnExecute.Enabled = btnClear.Enabled = false;
                settings.RemoveFromRecentFiles(fileName);
                UpdateRecentFilesMenu();
            }
            Cursor = Cursors.Default;

            UpdateStatus();
        }


        /// <summary>
        /// Reset components and close database connection
        /// </summary>
        private void Reset()
        {
            dataGrid.DataSource = null;
            db.Close();

            treeDb.Nodes.Clear();
            rtbDdl.Clear();
            rtbQuery.Clear();

            btnQuery.Checked = btnQuery.Enabled = btnExecute.Enabled = btnClear.Enabled = importMenuItem.Enabled =
                exportMenuItem.Enabled = saveSchemaMenuItem.Enabled = saveSqlMenuItem.Enabled = closeDatabaseMenuItem.Enabled =
                loadSqlMenuItem.Enabled = printMenuItem.Enabled = previewMenuItem.Enabled = false;
            splitterHorizontal.Panel1Collapsed = splitterHorizontal.IsSplitterFixed = true;
            Text = formText;
            tabControl1.SelectedIndex = 0;
        }

        /// <summary>
        /// Update the TreeDb to show all tables of the database
        /// </summary>
        private void UpdateTreeDb()
        {
            string selected = treeDb.SelectedNode == null ? string.Empty : treeDb.SelectedNode.Text;
            if (treeDb.SelectedNode == treeDb.Nodes[0])
                selected = string.Empty;

            treeDb.BeginUpdate();
            TreeNode main = treeDb.Nodes[0];
            main.Nodes.Clear();
            foreach (string tableName in db.TableNames)
                main.Nodes.Add(tableName, tableName, 1, 1);
            main.Expand();
            treeDb.EndUpdate();

            treeDb.SelectedNode = null;
            if (!string.IsNullOrEmpty(selected))
            {
                int i = main.Nodes.IndexOfKey(selected);
                if (i >= 0)
                    treeDb.SelectedNode = main.Nodes[i];
            }
            else
            {
                treeDb.SelectedNode = treeDb.Nodes[0];
            }
        }

        private Bitmap CreateBmp(string text, IntPtr handle, Font font, Color foreColor)
        {
            var g = Graphics.FromHwnd(handle);
            var size = g.MeasureString(text, font).ToSize();
            var bmp = new Bitmap(size.Width, size.Height, g);

            g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.DrawString(text, font, new SolidBrush(foreColor), 0, 0);
            g.Flush();

            return (bmp);
        }

        private Image CreatePng(string text, IntPtr handle, Font font, Color foreColor)
        {
            var g = Graphics.FromHwnd(handle);
            var size = g.MeasureString(text, font).ToSize();
            var bmp = new Bitmap(size.Width, size.Height, g);

            g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.DrawString(text, font, new SolidBrush(foreColor), 0, 0);
            g.Flush();

            var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);
            bmp.Dispose();
            return Image.FromStream(ms);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                LoadDatabase(openFileDialog1.FileName);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            splitterHorizontal.Panel1Collapsed = splitterHorizontal.IsSplitterFixed = !btnQuery.Checked;
            showEditorMenuItem.Checked = btnQuery.Checked;
            rtbQuery.Select();
            UpdateStatus();
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void treeDb_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!db.IsOpen)
                return;

            dataGrid.SuspendLayout();
            if (e.Node.ImageIndex == 0)
            {   // Database node
                dataGrid.ReadOnly = true;
                dataGrid.DataSource = DatabaseInfoLocale(db.DatabaseInfo);
                rtbDdl.Text = db.GetDatabaseDdl();
                sqlParserDdl.Update();
            }
            else
            {   // Table node
                dataGrid.ReadOnly = cbReadOnly.SelectedIndex == 0;
                dataGrid.Columns.Clear();
                if (tabControl1.SelectedIndex == 0)
                {
                    dataGrid.DataSource = db.GetTableData(e.Node.Text, null, SortOrder.None);
                    rtbDdl.Text = db.GetTableDdl(e.Node.Name, true, true, true, true);
                }
                else
                {
                    rtbDdl.Text = db.GetTableDdl(e.Node.Name, true, true, true, true);
                    dataGrid.DataSource = db.GetTableData(e.Node.Text, null, SortOrder.None);
                }
            }
            dataGrid.AllowUserToAddRows = dataGrid.AllowUserToDeleteRows = !dataGrid.ReadOnly;
            dataGrid.ResumeLayout();
        }

        private void dataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Error when show/edit field
            GlobalText.ShowError("ChangingDataError", e.Exception.Message);
            e.Cancel = true;
        }

        private void dataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Double click to switch row height
            if (dataGrid.CurrentRow.Height == dataGrid.RowTemplate.Height)
                dataGrid.AutoResizeRow(dataGrid.CurrentCell.RowIndex, DataGridViewAutoSizeRowMode.AllCells);
            else
                dataGrid.CurrentRow.Height = dataGrid.RowTemplate.Height;
        }

        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            // On F5 key press, execute the query
            if (e.KeyData == Keys.F5 && rtbQuery.Focused)
                btnExecute.PerformClick();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (rtbQuery.Text.Trim().Length == 0)
                return;
            bool partial = rtbQuery.SelectedText.Trim().Length > 0;
            if (partial)
            {
                DialogResult result = MessageBox.Show(GlobalText.GetValue("SelectedTextQuery"), GlobalText.GetValue("Confirm"),
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Cancel)
                    return;
                partial = result == DialogResult.Yes;
            }
            string sql = partial ? rtbQuery.SelectedText.Trim() : rtbQuery.Text.Trim();

            dataGrid.DataSource = null;

            // Initial time
            DateTime initTime = DateTime.Now;

            object resultSet = db.ExecuteSql(sql, false);

            long ms = (long)(DateTime.Now - initTime).TotalMilliseconds;

            dataGrid.DataSource = resultSet;

            if (resultSet != null || string.IsNullOrEmpty(db.LastError))
            {
                lbResult.ForeColor = Color.Black;
                lbResult.Text = $"{db.QueryCount} {GlobalText.GetValue("Querys")}, {dataGrid.RowCount} {GlobalText.GetValue("Rows")}, {ms} {GlobalText.GetValue("Milliseconds")}";
                if (resultSet == null && regexCreateAlterDrop.IsMatch(regexDropQuotesAndBrackets.Replace(rtbQuery.Text, string.Empty)))
                {
                    db.ResetDdl();  // Update DDL
                    UpdateTreeDb();
                }
                else
                {
                    tabControl1.SelectedIndex = 0;
                    if (resultSet == null && regexInsertUpdateDelete.IsMatch(regexDropQuotesAndBrackets.Replace(rtbQuery.Text, string.Empty)))
                    {
                        TreeNode node = treeDb.SelectedNode;  // Update data view
                        treeDb.SelectedNode = null;
                        treeDb.SelectedNode = node;
                    }
                }
            }
            else
            {
                lbResult.ForeColor = Color.Red;
                lbResult.Text = db.LastError;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rtbQuery.Clear();
            lbResult.Text = string.Empty;
        }

        private void cbReadOnly_SelectedIndexChanged(object sender, EventArgs e)
        {
            TreeNode node = treeDb.SelectedNode;
            if (node != null && node.ImageIndex == 1)
                treeDb_AfterSelect(treeDb, new TreeViewEventArgs(node));
            allowEditingMenuItem.Checked = cbReadOnly.SelectedIndex == 1;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
                dataGrid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            UpdateStatus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Runs only once when the application just finish of display the form
            timer1.Stop();

            // Initialize regex
            regexDropQuotesAndBrackets.Match(string.Empty);
            regexCreateAlterDrop.Match(string.Empty);
            regexInsertUpdateDelete.Match(string.Empty);
        }

        private void rtbDdl_Click(object sender, EventArgs e)
        {
            // If the query is enabled, clicking on the fields are added to the query
            if (!btnQuery.Checked)
                return;

            char c = ' ';

            int start = rtbDdl.SelectionStart;
            while (start >= 0 && (c = rtbDdl.Text[start]) != '[')
                if (c == ']' || c == (char)10)
                    break;
                else
                    start--;
            if (c != '[')
                return;

            int stop = start;
            while (stop < rtbDdl.Text.Length && (c = rtbDdl.Text[stop]) != ']')
                if (c == (char)10)
                    break;
                else
                    stop++;
            if (c != ']')
                return;

            rtbDdl.SelectionStart = start;
            rtbDdl.SelectionLength = stop - start + 1;

            rtbQuery.SelectedText = rtbDdl.SelectedText;
            rtbQuery.Focus();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();

            // Open the database file if specified on the command line
            string[] cmdLine = Environment.GetCommandLineArgs();
            if (cmdLine.Length > 1 && !string.IsNullOrEmpty(cmdLine[1]))
                LoadDatabase(Path.GetFullPath(cmdLine[1]));
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            bool onQuery = (sender as ContextMenuStrip).SourceControl == rtbQuery;
            cutToolStripMenuItem.Visible = pasteToolStripMenuItem.Visible = loadFromFileToolStripMenuItem.Visible = onQuery;
            cutToolStripMenuItem.Enabled = rtbQuery.SelectionLength > 0;
            pasteToolStripMenuItem.Enabled = rtbQuery.CanPaste(DataFormats.GetFormat(DataFormats.Text));
        }

        private void loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() != DialogResult.OK)
                return;
            rtbQuery.LoadFile(openFileDialog2.FileName, RichTextBoxStreamType.PlainText);
        }

        private void saveToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            bool onQuery = ((sender as ToolStripMenuItem).Owner as ContextMenuStrip).SourceControl == rtbQuery;
            RichTextBox rtb = onQuery ? rtbQuery : rtbDdl;
            bool all = rtb.SelectionLength == 0;
            var writer = new StreamWriter(saveFileDialog1.FileName, false, Encoding.UTF8);
            writer.Write(all ? rtb.Text : rtb.SelectedText);
            writer.Close();
        }

        private void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0)
            {   // Cell is row header, therefore should be select the entire row
                dataGrid.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
                bool readOnly = dataGrid.ReadOnly;
                dataGrid.ReadOnly = true;
                dataGrid.ReadOnly = readOnly;
            }
            else
            {   // Normal cell
                dataGrid.EditMode = DataGridViewEditMode.EditOnEnter;
            }
        }

        private void aboutCompactViewMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void databaseToolsMenuItem_Click(object sender, EventArgs e)
        {
            string sFilename = db.FileName;
            string sPassword = db.Password;
            Reset();
            UpdateStatus();

            new ToolsForm(sFilename, sPassword).ShowDialog();
        }

        private void optionsMenuItem_Click(object sender, EventArgs e)
        {
            if (new OptionsForm(dataGrid, settings).ShowDialog() != DialogResult.OK)
                return;
        }

        private void exportMenuItem_Click(object sender, EventArgs e)
        {
            new ExportForm(db, treeDb.SelectedNode).ShowDialog();
        }

        private void allowEditingMenuItem_Click(object sender, EventArgs e)
        {
            allowEditingMenuItem.Checked = !allowEditingMenuItem.Checked;
            cbReadOnly.SelectedIndex = allowEditingMenuItem.Checked ? 1 : 0;
        }

        private void showEditorMenuItem_Click(object sender, EventArgs e)
        {
            btnQuery.Checked = !btnQuery.Checked;
            showEditorMenuItem.Checked = btnQuery.Checked;
            btnQuery_Click(null, null);
        }

        private void rtbQuery_Enter_Leave_SelectionChanged(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            rtbQuery.Cut();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            bool queryFocused = btnQuery.Enabled && btnQuery.Checked && rtbQuery.Focused;
            RichTextBox rtb = queryFocused ? rtbQuery : rtbDdl;
            bool all = rtb.SelectionLength == 0;
            if (all)
                rtb.SelectAll();
            rtb.Copy();
            if (all)
                rtb.DeselectAll();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            rtbQuery.Paste(DataFormats.GetFormat(DataFormats.Text));
        }

        private void deleteMenuItem_Click(object sender, EventArgs e)
        {
            rtbQuery.SelectedText = string.Empty;
        }

        private bool queryPrint = false;

        private void previewMenuItem_Click(object sender, EventArgs e)
        {
            queryPrint = btnQuery.Enabled && btnQuery.Checked && rtbQuery.Focused;
            RichTextBox rtb = queryPrint ? rtbQuery : rtbDdl;
            rtb.Print(PrintType.PrintPreview, BeforePagePrint);
        }

        private void printMenuItem_Click(object sender, EventArgs e)
        {
            queryPrint = btnQuery.Enabled && btnQuery.Checked && rtbQuery.Focused;
            RichTextBox rtb = queryPrint ? rtbQuery : rtbDdl;
            rtb.Print(PrintType.ShowPrintDialog, BeforePagePrint);
        }

        private void BeforePagePrint(int posIniChar, int posEndChar, PrintPageEventArgs e)
        {
            SqlParser parser = queryPrint ? sqlParserQuery : sqlParserDdl;
            parser.ParseRichTextBox(posIniChar, posEndChar);
        }

        private void loadSqlMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() != DialogResult.OK)
                return;
            rtbQuery.Text = File.ReadAllText(openFileDialog2.FileName);
        }

        private void saveSqlMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            bool all = rtbQuery.SelectionLength == 0;
            TextWriter writer = new StreamWriter(saveFileDialog1.FileName, false, Encoding.UTF8);
            writer.Write(all ? rtbQuery.Text : rtbQuery.SelectedText);
            writer.Close();
        }

        private void saveSchemaMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            bool all = rtbDdl.SelectionLength == 0;
            TextWriter writer = new StreamWriter(saveFileDialog1.FileName, false, Encoding.UTF8);
            writer.Write(all ? rtbDdl.Text : rtbDdl.SelectedText);
            writer.Close();
        }

        private void closeDatabaseMenuItem_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void importMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                if (new ImportForm(db, openFileDialog2.FileName).ShowDialog() == DialogResult.OK)
                {
                    db.ResetDdl();  // Update DDL
                    UpdateTreeDb();
                }
            }
        }

        private int GetOleHeaderPos(byte[] bytes)
        {
            const string BITMAP_ID_BLOCK = "BM";
            const string JPG_ID_BLOCK = "\u00FF\u00D8\u00FF";
            const string PNG_ID_BLOCK = "\u0089PNG\r\n\u001a\n";
            const string GIF_ID_BLOCK = "GIF8";
            const string TIFF_ID_BLOCK = "II*\u0000";

            int length = bytes.Length;
            if (length > 300)
                length = 300;
            string s = Encoding.UTF7.GetString(bytes, 0, length);
            if (s.Length > 300)
                s = s.Remove(300);

            int i = s.IndexOf(BITMAP_ID_BLOCK);
            if (i >= 0)
                return i;
            i = s.IndexOf(JPG_ID_BLOCK);
            if (i >= 0)
                return i;
            i = s.IndexOf(PNG_ID_BLOCK);
            if (i >= 0)
                return i;
            i = s.IndexOf(GIF_ID_BLOCK);
            if (i >= 0)
                return i;
            i = s.IndexOf(TIFF_ID_BLOCK);
            if (i >= 0)
                return i;
            return 0;
        }

        private Image GetImage(byte[] bytes)
        {
            try
            {
                int i = GetOleHeaderPos(bytes);
                Image image;
                using (var ms = new MemoryStream(bytes, i, bytes.Length - i))
                    image = Image.FromStream(ms);
                return image;
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        private void dataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGrid.Columns[e.ColumnIndex].ValueType != typeof(byte[]))
                return;
            object val = e.Value;
            if (val != null && val.GetType() == typeof(byte[]) && ((byte[])val).Length > 0)
            {
                if (GetImage((byte[])val) != null)
                    return;
                int length = ((byte[])val).Length;
                string s = string.Empty;
                if (length > 100)
                {
                    length = 100;
                    s = "...";
                }
                e.Value = CreatePng(BitConverter.ToString((byte[])val, 0, length) + s, dataGrid.Handle, e.CellStyle.Font, e.CellStyle.ForeColor);
            }
            else
            {
                e.Value = CreatePng(" ", dataGrid.Handle, e.CellStyle.Font, e.CellStyle.ForeColor);
            }
            e.FormattingApplied = true;
        }

        private void dataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dataGrid[e.ColumnIndex, e.RowIndex];
            if (cell.ValueType == typeof(string) && cell.Value == DBNull.Value)
                dataGrid[e.ColumnIndex, e.RowIndex].Value = string.Empty;
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedCells.Count != 1)
                return;
            object val = dataGrid.SelectedCells[0].Value;
            Type t = dataGrid.SelectedCells[0].ValueType;
            if (t == typeof(byte[]))
            {
                Image image = GetImage((byte[])val);
                if (image == null)
                    Clipboard.SetText($"0x{BitConverter.ToString((byte[])val).Replace("-", string.Empty)}");
                else
                    Clipboard.SetImage(image);
            }
            else
            {
                Clipboard.SetText(val.ToString());
            }
        }

        private void dataGrid_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit = dataGrid.HitTest(e.X, e.Y);
            if (hit.Type == DataGridViewHitTestType.Cell)
            {
                dataGrid.ContextMenuStrip = contextMenuStrip2;
                dataGrid.CurrentCell = dataGrid[hit.ColumnIndex, hit.RowIndex];
                foreach (DataGridViewCell cell in dataGrid.SelectedCells)
                    if (cell != dataGrid.CurrentCell)
                        cell.Selected = false;
            }
            else
            {
                dataGrid.ContextMenuStrip = null;
            }
        }

        private void dataGrid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!db.IsOpen || treeDb.SelectedNode == null || treeDb.SelectedNode.ImageIndex == 0)
                return;

            string tableName = treeDb.SelectedNode.Text;
            string columnName = dataGrid.Columns[e.ColumnIndex].HeaderCell.Value.ToString();
            SortOrder order = dataGrid.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection;
            string dbtype = db.GetColumnDataType(tableName, columnName);
            if (dbtype == "ntext" || dbtype == "image")
                return;

            dataGrid.SuspendLayout();
            dataGrid.Columns.Clear();
            switch (order)
            {
                case SortOrder.None:
                    order = SortOrder.Ascending;
                    break;
                case SortOrder.Ascending:
                    order = SortOrder.Descending;
                    break;
                case SortOrder.Descending:
                    order = SortOrder.Ascending;
                    break;
            }
            dataGrid.DataSource = db.GetTableData(tableName, columnName, order);
            dataGrid.Columns[columnName].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGrid.Columns[columnName].HeaderCell.SortGlyphDirection = order;
            if (e.Button == MouseButtons.Right)
                dataGrid.Columns[columnName].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGrid.ResumeLayout();
        }

        private void dataGrid_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.FillWeight = 1;
        }
    }
}
