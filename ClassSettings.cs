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
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections.Specialized;
using System.IO;

namespace CompactView
{
    public class Settings
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Maximized { get; set; }
        public int TextColor1 { get; set; }
        public int TextColor2 { get; set; }
        public int BackColor1 { get; set; }
        public int BackColor2 { get; set; }
        public int ColorSet { get; set; }
        public StringCollection RecentFiles { get; set; }
        public int MaxRecentFiles { get; set; }

        public Settings()
        {
            MaxRecentFiles = 10;
            RecentFiles = new StringCollection();
        }

        public bool AddToRecentFiles(string fileName)
        {
            int i = RecentFiles.IndexOf(fileName);
            if (i >= 0) RecentFiles.RemoveAt(i);

            while (RecentFiles.Count >= MaxRecentFiles) RecentFiles.RemoveAt(MaxRecentFiles - 1);
            RecentFiles.Insert(0, fileName);
            return true;
        }

        public bool RemoveFromRecentFiles(string fileName)
        {
            int i = RecentFiles.IndexOf(fileName);
            bool ok = i >= 0;
            if (ok) RecentFiles.RemoveAt(i);
            return ok;
        }

        private string FileName
        {
            get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Application.ProductName + @"\Settings.xml"); }
        }

        public void Load()
        {
            var table = new DataTable("Settings");

            try
            {
                if (!Directory.Exists(FileName)) Directory.CreateDirectory(Path.GetDirectoryName(FileName));
                table.ReadXml(FileName);
            }
            catch
            {
            }

            if (table.Rows.Count <= 0) return;
            DataRow row = table.Rows[0];

            if (table.Columns.Contains("X")) X = row.Field<int>("X");
            if (table.Columns.Contains("Y")) Y = row.Field<int>("Y");
            if (table.Columns.Contains("Width")) Width = row.Field<int>("Width");
            if (table.Columns.Contains("Height")) Height = row.Field<int>("Height");
            if (table.Columns.Contains("Maximized")) Maximized = row.Field<bool>("Maximized");
            if (table.Columns.Contains("TextColor1")) TextColor1 = row.Field<int>("TextColor1");
            if (table.Columns.Contains("TextColor2")) TextColor2 = row.Field<int>("TextColor2");
            if (table.Columns.Contains("BackColor1")) BackColor1 = row.Field<int>("BackColor1");
            if (table.Columns.Contains("BackColor2")) BackColor2 = row.Field<int>("BackColor2");
            if (table.Columns.Contains("ColorSet")) ColorSet = row.Field<int>("ColorSet");

            RecentFiles.Clear();
            for (int i = 1; i <= MaxRecentFiles; i++)
            {
                if (table.Columns.Contains($"RecentFiles{i}")) RecentFiles.Add(row.Field<string>($"RecentFiles{i}"));
            }
        }

        public void Save()
        {
            var table = new DataTable("Settings");
            table.Columns.Add("X", typeof(int));
            table.Columns.Add("Y", typeof(int));
            table.Columns.Add("Width", typeof(int));
            table.Columns.Add("Height", typeof(int));
            table.Columns.Add("Maximized", typeof(bool));
            table.Columns.Add("TextColor1", typeof(int));
            table.Columns.Add("TextColor2", typeof(int));
            table.Columns.Add("BackColor1", typeof(int));
            table.Columns.Add("BackColor2", typeof(int));
            table.Columns.Add("ColorSet", typeof(int));
            for (int i = 1; i <= RecentFiles.Count; i++) table.Columns.Add($"RecentFiles{i}", typeof(string));

            DataRow row = table.NewRow();
            row[0] = X;
            row[1] = Y;
            row[2] = Width;
            row[3] = Height;
            row[4] = Maximized;
            row[5] = TextColor1;
            row[6] = TextColor2;
            row[7] = BackColor1;
            row[8] = BackColor2;
            row[9] = ColorSet;
            for (int i = 0; i < RecentFiles.Count; i++) row[i + 10] = RecentFiles[i];
            table.Rows.Add(row);

            try
            {
                var xw = new XmlTextWriter(FileName, Encoding.UTF8) { Formatting = Formatting.Indented };
                table.WriteXml(xw, XmlWriteMode.WriteSchema);
                xw.Close();
            }
            catch
            {
            }
        }
    }
}
