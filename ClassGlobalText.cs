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
                    FillEnglish();
                }
                if (value == _cultureCode)
                    return;
                _cultureCode = value;
                if (string.IsNullOrEmpty(value))
                    return;
                string fileName = XmlName(_cultureCode);
                if (File.Exists(fileName))
                {
                    var table = new DataTable("Dictionary");
                    table.ReadXml(fileName);
                    foreach (DataRow row in table.Rows)
                        if (list.ContainsKey(row[0].ToString()))
                            list[row[0].ToString()] = row[1].ToString();
                }
            }
        }

        private static string XmlName(string cultureCode)
        {
            string s = Application.ExecutablePath;
            int i = s.LastIndexOf('.');
            if (i >= 0)
                s = s.Remove(i);
            return s + "_" + cultureCode + ".xml";
        }

        private static void FillEnglish()
        {
            list = new Dictionary<string, string>
            {
                { "TranslationInfo", "English translation by Iván Costales Suárez (CompactView)" },
                { "OpenDatabase", "Open database" },
                { "ReadOnly", "Read only" },
                { "AllowEditing", "Allow editing" },
                { "Query", "Query" },
                { "Execute", "Execute" },
                { "Clear", "Clear" },
                { "Tools", "Tools" },
                { "About", "About" },
                { "Database", "Database" },
                { "Data", "Data" },
                { "SqlSchema", "SQL Schema" },
                { "FileName", "File name" },
                { "Password", "Password" },
                { "FileNameTip", "Database file name. Press the button to the right to select a file" },
                { "PasswordTip", "Database password. Leave blank if none" },
                { "SelectTip", "Select database file" },
                { "UpgradeToVersion", "Upgrade to version" },
                { "Action", "Action" },
                { "Create", "Create" },
                { "Compact", "Compact" },
                { "Repair", "Repair" },
                { "Shrink", "Shrink" },
                { "Upgrade", "Upgrade" },
                { "Verify", "Verify" },
                { "CreateTip", "Create a new database file" },
                { "CompactTip", "Reclaims wasted space in the database by creating a new database file from the existing file" },
                { "RepairTip", "Repair a corrupted database. First try to recover the corrupted rows, then remove the remaining corrupted rows" },
                { "ShrinkTip", "Reclaims wasted space in the database by moving empty pages to the end of the file, and then truncating the file" },
                { "UpgradeTip", "Upgrades a database from version 3.1 to 3.5. After the upgrade, the database will be encrypted if the source database was encrypted" },
                { "VerifyTip", "Recalculates the checksums for each page in the database and compares the new checksums to the expected values" },
                { "ToolsNote", "Before every action a copy of the database file will be created by appending 001, 002, 003 etc to the file name" },
                { "Close", "Close" },
                { "Version", "Version" },
                { "AboutDescription", "SQL Server Compact Edition Database viewer and editor" },
                { "PasswordNote", "The database is encrypted, please provide the password to continue" },
                { "Ok", "OK" },
                { "Cancel", "Cancel" },
                { "FileNotFound", "File not found" },
                { "UnableToOpen", "Unable to open the database" },
                { "Error", "Error" },
                { "ChangingDataError", "Error changing the data" },
                { "Rows", "rows" },
                { "Milliseconds", "milliseconds" },
                { "QueryNote", "By clicking on any field bracketed in the SQL Schema, it is added to the query" },
                { "NoDatabaseSelect", "No database selected!" },
                { "DatabaseMissing", "Database file is missing!" },
                { "BackupError", "Error creating backup" },
                { "CreateError", "Error creating database" },
                { "CompactError", "Error compacting database" },
                { "RepairError", "Error repairing database" },
                { "ShrinkError", "Error shrinking database" },
                { "UpgradeError", "Error upgrading database" },
                { "VerifyError", "Error verifying database" },
                { "Information", "Information" },
                { "CreateDone", "Database created successfully" },
                { "CompactDone", "Database compacted successfully" },
                { "RepairDone", "Repairing completed successfully" },
                { "ShrinkDone", "Shrinking completed successfully" },
                { "UpgradeDone", "Upgrading completed successfully" },
                { "VerifyDone", "Verifying completed successfully" },
                { "VerifyFault", "Database is corrupted. In case of encrypted databases this may also indicate wrong password" },
                { "DatabaseInfoError", "Error getting database information" },
                { "Options", "Options" },
                { "Colors", "Colors" },
                { "FontColor", "Font Color" },
                { "BackgroundColor", "Background Color" },
                { "Line", "Line" },
                { "ColorSet", "Color Set" },
                { "UserDefined", "User defined" },
                { "Cut", "Cut" },
                { "Copy", "Copy" },
                { "Paste", "Paste" },
                { "LoadFromFile", "Load from file" },
                { "SaveToFile", "Save to file" },
                { "Querys", "Querys" },
                { "Print", "Print" },
                { "File", "File" },
                { "RecentFiles", "RecentFiles" },
                { "Import", "Import" },
                { "Export", "Export" },
                { "Exit", "Exit" },
                { "Edit", "Edit" },
                { "Delete", "Delete" },
                { "ShowEditor", "ShowEditor" },
                { "DatabaseTools", "DatabaseTools" },
                { "Help", "Help" },
                { "LoadSqlQuery", "Load SQL query" },
                { "SaveSqlQuery", "Save SQL query" },
                { "SaveSqlSchema", "Save SQL schema" },
                { "CloseDatabase", "Close database" },
                { "All", "All" },
                { "OnlySchema", "Only schema" },
                { "OnlyData", "Only data" },
                { "UsedAsAForeignKey", "is used as a foreign key by" },
                { "Warning", "Warning" },
                { "Schema", "Schema" },
                { "ImportFormatError", "File format to be imported is not correct" },
                { "ErrorImportingTablesSchema", "Error importing sql schema. Some tables to be imported already exist in the current database" },
                { "ErrorImportingTablesData", "Error importing sql data. Missing some table in which data should be imported" },
                { "ImportOk", "Import process completed successfully" },
                { "ImportError", "Error when running the import process" },
                { "Confirm", "Confirm" },
                { "SelectedTextQuery", "Do you want to execute the selected text only?" },
                { "Preview", "Print preview" },
                { "Property", "Property" },
                { "Value", "Value" },
                { "FilePath", "File Path" },
                { "FileSize", "File Size" },
                { "LocaleIdentifier", "Locale Identifier" },
                { "EncryptionMode", "Encryption Mode" },
                { "CaseSensitive", "Case Sensitive" },
                { "True", "Yes" },
                { "False", "No" },
                { "Tables", "Tables" },
                { "Indexes", "Indexes" },
                { "Keys", "Keys" },
                { "TableConstraints", "Table Constraints" },
                { "ForeignConstraints", "Foreign Constraints" }
            };

            string fileName = XmlName("en-EN");
            if (!File.Exists(fileName))
            {
                var table = new DataTable("Dictionary");
                table.Columns.Add("Key", typeof(string));
                table.Columns.Add("Value", typeof(string));
                foreach (KeyValuePair<string, string> row in list)
                    table.Rows.Add(row.Key, row.Value);
                var xw = new XmlTextWriter(fileName, Encoding.UTF8) { Formatting = Formatting.Indented };
                table.WriteXml(xw, XmlWriteMode.WriteSchema);
                xw.Close();
            }
        }

        public static string GetValue(string key)
        {
            if (string.IsNullOrEmpty(CultureCode))
                CultureCode = System.Globalization.CultureInfo.CurrentCulture.Name;
            return list.TryGetValue(key, out string value) ? value : "";
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
