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
using System.Text;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.IO;
using System.Xml;


namespace CompactView
{
    public static class GlobalText
	{
        private static Dictionary<string, string> list = null;
        private static string _cultureCode = null;

        /// <summary>
        /// Get or set the culture code in the format LanguageCode-RegionalCode 
        /// </summary>
        public static string CultureCode
        {
            get
            { 
                return _cultureCode;
            }
            set
            {
                if (list == null)
                {
                    list = new Dictionary<string, string>();
                    FillEnglish();
                }
                if (value == _cultureCode) return;
                _cultureCode = value;
                if (string.IsNullOrEmpty(value)) return;
                string fileName = XmlName(_cultureCode);
                if (File.Exists(fileName))
                {
                    DataTable table = new DataTable("Dictionary");
                    table.ReadXml(fileName);
                    foreach (DataRow row in table.Rows) if (list.ContainsKey(row[0].ToString())) list[row[0].ToString()] = row[1].ToString();
                }
            }
        }

        private static string XmlName(string cultureCode)
        {
            string s = Application.ExecutablePath;
            int i = s.LastIndexOf('.');
            if (i >= 0) s = s.Remove(i);
            return s + "_" + cultureCode + ".xml";
        }

        private static void FillEnglish()
        {
            list.Clear();
            list.Add("TranslationInfo", "English translation by Iván Costales Suárez (CompactView)");
            list.Add("OpenDatabase", "Open database");
            list.Add("ReadOnly", "Read only");
            list.Add("AllowEditing", "Allow editing");
            list.Add("Query", "Query");
            list.Add("Execute", "Execute");
            list.Add("Clear", "Clear");
            list.Add("Tools", "Tools");
            list.Add("About", "About");
            list.Add("Database", "Database");
            list.Add("Data", "Data");
            list.Add("SqlSchema", "SQL Schema");
            list.Add("FileName", "File name");
            list.Add("Password", "Password");
            list.Add("FileNameTip", "Database file name. Press the button to the right to select a file");
            list.Add("PasswordTip", "Database password. Leave blank if none");
            list.Add("SelectTip", "Select database file");
            list.Add("UpgradeToVersion", "Upgrade to version");
            list.Add("Action", "Action");
            list.Add("Create", "Create");
            list.Add("Compact", "Compact");
            list.Add("Repair", "Repair");
            list.Add("Shrink", "Shrink");
            list.Add("Upgrade", "Upgrade");
            list.Add("Verify", "Verify");
            list.Add("CreateTip", "Create a new database file");
            list.Add("CompactTip", "Reclaims wasted space in the database by creating a new database file from the existing file");
            list.Add("RepairTip", "Repair a corrupted database. First try to recover the corrupted rows, then remove the remaining corrupted rows");
            list.Add("ShrinkTip", "Reclaims wasted space in the database by moving empty pages to the end of the file, and then truncating the file");
            list.Add("UpgradeTip", "Upgrades a database from version 3.1 to 3.5. After the upgrade, the database will be encrypted if the source database was encrypted");
            list.Add("VerifyTip", "Recalculates the checksums for each page in the database and compares the new checksums to the expected values");
            list.Add("ToolsNote", "Before every action a copy of the database file will be created by appending 001, 002, 003 etc to the file name");
            list.Add("Close", "Close");
            list.Add("Version", "Version");
            list.Add("AboutDescription", "SQL Server Compact Edition Database viewer and editor");
            list.Add("PasswordNote", "The database is encrypted, please provide the password to continue");
            list.Add("Ok", "OK");
            list.Add("Cancel", "Cancel");
            list.Add("FileNotFound", "File not found");
            list.Add("UnableToOpen", "Unable to open the database");
            list.Add("Error", "Error");
            list.Add("ChangingDataError", "Error changing the data");
            list.Add("Rows", "rows");
            list.Add("Milliseconds", "milliseconds");
            list.Add("QueryNote", "By clicking on any field bracketed in the SQL Schema, it is added to the query");
            list.Add("NoDatabaseSelect", "No database selected!");
            list.Add("DatabaseMissing", "Database file is missing!");
            list.Add("BackupError", "Error creating backup");
            list.Add("CreateError", "Error creating database");
            list.Add("CompactError", "Error compacting database");
            list.Add("RepairError", "Error repairing database");
            list.Add("ShrinkError", "Error shrinking database");
            list.Add("UpgradeError", "Error upgrading database");
            list.Add("VerifyError", "Error verifying database");
            list.Add("Information", "Information");
            list.Add("CreateDone", "Database created successfully");
            list.Add("CompactDone", "Database compacted successfully");
            list.Add("RepairDone", "Repairing completed successfully");
            list.Add("ShrinkDone", "Shrinking completed successfully");
            list.Add("UpgradeDone", "Upgrading completed successfully");
            list.Add("VerifyDone", "Verifying completed successfully");
            list.Add("VerifyFault", "Database is corrupted. In case of encrypted databases this may also indicate wrong password");
            list.Add("DatabaseInfoError", "Error getting database information");
            list.Add("Options", "Options");
            list.Add("Colors", "Colors");
            list.Add("FontColor", "Font Color");
            list.Add("BackgroundColor", "Background Color");
            list.Add("Line", "Line");
            list.Add("ColorSet", "Color Set");
            list.Add("UserDefined", "User defined");
            list.Add("Cut", "Cut");
            list.Add("Copy", "Copy");
            list.Add("Paste", "Paste");
            list.Add("LoadFromFile", "Load from file");
            list.Add("SaveToFile", "Save to file");
            list.Add("Querys", "Querys");
            list.Add("Print", "Print");
            list.Add("File", "File");
            list.Add("RecentFiles", "RecentFiles");
            list.Add("Import", "Import");
            list.Add("Export", "Export");
            list.Add("Exit", "Exit");
            list.Add("Edit", "Edit");
            list.Add("Delete", "Delete");
            list.Add("ShowEditor", "ShowEditor");
            list.Add("DatabaseTools", "DatabaseTools");
            list.Add("Help", "Help");
            list.Add("LoadSqlQuery", "Load SQL query");
            list.Add("SaveSqlQuery", "Save SQL query");
            list.Add("SaveSqlSchema", "Save SQL schema");
            list.Add("CloseDatabase", "Close database");
            list.Add("All", "All");
            list.Add("OnlySchema", "Only schema");
            list.Add("OnlyData", "Only data");
            list.Add("UsedAsAForeignKey", "is used as a foreign key by");
            list.Add("Warning", "Warning");
            list.Add("Schema", "Schema");
            list.Add("ImportFormatError", "File format to be imported is not correct");
            list.Add("ErrorImportingTablesSchema", "Error importing sql schema. Some tables to be imported already exist in the current database");
            list.Add("ErrorImportingTablesData", "Error importing sql data. Missing some table in which data should be imported");
            list.Add("ImportOk", "Import process completed successfully");
            list.Add("ImportError", "Error when running the import process");
            list.Add("Confirm", "Confirm");
            list.Add("SelectedTextQuery", "Do you want to execute the selected text only?");
            list.Add("Preview", "Print preview");
            list.Add("Property", "Property");
            list.Add("Value", "Value");
            list.Add("FilePath", "File Path");
            list.Add("FileSize", "File Size");
            list.Add("LocaleIdentifier", "Locale Identifier");
            list.Add("EncryptionMode", "Encryption Mode");
            list.Add("CaseSensitive", "Case Sensitive");
            list.Add("True", "Yes");
            list.Add("False", "No");
            list.Add("Tables", "Tables");
            list.Add("Indexes", "Indexes");
            list.Add("Keys", "Keys");
            list.Add("TableConstraints", "Table Constraints");
            list.Add("ForeignConstraints", "Foreign Constraints");

            string fileName = XmlName("en-EN");
            if (!File.Exists(fileName))
            {
                DataTable table = new DataTable("Dictionary");
                table.Columns.Add("Key", typeof(string));
                table.Columns.Add("Value", typeof(string));
                foreach (KeyValuePair<string, string> row in list) table.Rows.Add(row.Key, row.Value);
                XmlTextWriter xw = new XmlTextWriter(fileName, Encoding.UTF8) { Formatting = Formatting.Indented };
                table.WriteXml(xw, XmlWriteMode.WriteSchema);
                xw.Close();
            }
        }

        public static string GetValue(string key)
        {
            if (string.IsNullOrEmpty(CultureCode)) CultureCode = System.Globalization.CultureInfo.CurrentCulture.Name;
            string value;
            return list.TryGetValue(key, out value) ? value : "";
        }

        public static void ShowError(string key)
        {
            ShowError(key, null);
        }

        public static void ShowError(string key, string additionalMsg)
        {
            string errorText = string.IsNullOrEmpty(additionalMsg) ? GlobalText.GetValue(key) : GlobalText.GetValue(key) + ":\r\n\r\n" + additionalMsg;
            MessageBox.Show(errorText, GlobalText.GetValue("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowInfo(string key)
        {
            MessageBox.Show(GlobalText.GetValue(key), GlobalText.GetValue("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowWarning(string warningMsg)
        {
            MessageBox.Show(warningMsg, GlobalText.GetValue("Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
