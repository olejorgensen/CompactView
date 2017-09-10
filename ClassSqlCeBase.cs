/**************************************************************************
Copyright (C) 2011-2015 Iván Costales Suárez

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
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CompactView
{
    public class Version
    {
        public string SqlceVersion { get; private set; }
        public string AssemblyVersion { get; private set; }
        public UInt32 SdfCodeVersion { get; private set; }

        public Version(string sqlceVersion, string assemblyVersion, UInt32 sdfCodeVersion)
        {
            SqlceVersion = sqlceVersion;
            AssemblyVersion = assemblyVersion;
            SdfCodeVersion = sdfCodeVersion;
        }

        public override string ToString()
        {
            return SqlceVersion;
        }
    }

    public class SqlCeBase : IDisposable
    {
        private static Version[] _availabeVersions =
        {   // List of available versions starting with the oldest
            // new Version("2.0", "2.0.0.0", 0x73616261),
            new Version("3.1", "9.0.242.0", 0x002dd714),
            new Version("3.5", "3.5.0.0", 0x00357b9d),
            new Version("4.0", "4.0.0.0", 0x003d0900)
        };

        /// <summary>
        /// Initialize a new instance of SqlCeDb
        /// </summary>
        public SqlCeBase()
        {
            LastError = string.Empty;
            regexSemicolon.Match("");
        }

        public static Version[] AvailableVersions { get { return _availabeVersions; } }
        public DbConnection Connection { get; private set; }
        public string FileName { get; private set; }
        public string Password { get; private set; }
        public string LastError { get; private set; }
        public Version Version { get; private set; }
        public bool BadPassword { get; private set; }
        public int QueryCount { get; private set; }

        private Assembly assembly;
        // Regular expression to search texts finished with semicolons that is not between single quotes
        private Regex regexSemicolon = new Regex("(?:[^;']|'[^']*')+", RegexOptions.Compiled | RegexOptions.Multiline);

        public void Dispose()
        {
            Close();
        }

        /// <summary>
        /// Open the specified database file
        /// </summary>
        /// <param name="databaseFile">Database file name to open</param>
        public bool Open(string databaseFile)
        {
            return Open(databaseFile, null);
        }

        /// <summary>
        /// Open the specified database file
        /// </summary>
        /// <param name="databaseFile">Database file name to open</param>
        /// <param name="password">Password of database file</param>
        public bool Open(string databaseFile, string password)
        {
            Close();
            FileName = Path.GetFullPath(databaseFile);
            Password = password;

            bool ok = false;
            Version version = GetSdfVersion(FileName);
            if (version == null)
            {
                foreach (Version ver in AvailableVersions.Reverse<Version>())
                {
                    ok = OpenConnection(ver, databaseFile, password);
                    if (ok || BadPassword)
                        break;
                }
            }
            else
            {
                ok = OpenConnection(version, databaseFile, password);
                if (!ok && (!BadPassword || password != null))
                    GlobalText.ShowError("UnableToOpen", LastError);
            }
            return ok;
        }

        public void Close()
        {
            if (Connection != null && Connection.State != ConnectionState.Closed)
                Connection.Close();
            Connection = null;
            Version = null;
            LastError = string.Empty;
            FileName = string.Empty;
            Password = string.Empty;
            BadPassword = false;
            _tableNames = null;
            _databaseInfo = null;
        }

        public bool IsOpen => Connection?.State == ConnectionState.Open;

        private DataTable _databaseInfo;

        public DataTable DatabaseInfo
        {
            get
            {
                if (_databaseInfo != null)
                    return _databaseInfo;
                if (Connection == null)
                    return null;
                _databaseInfo = new DataTable();
                _databaseInfo.Columns.Add("Property");
                _databaseInfo.Columns.Add("Value");
                _databaseInfo.Rows.Add("Version", Version);
                _databaseInfo.Rows.Add("File Path", FileName);
                _databaseInfo.Rows.Add("File Size", GetFileSize(new FileInfo(FileName).Length));

                try
                {
                    var dbInfo = (List<KeyValuePair<string, string>>)Connection.GetType().InvokeMember("GetDatabaseInfo", BindingFlags.InvokeMethod, null, Connection, null);
                    foreach (KeyValuePair<string, string> key in dbInfo)
                        _databaseInfo.Rows.Add(System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(key.Key), key.Value);
                }
                catch
                {
                }

                try
                {
                    DbCommand cmd = Connection.CreateCommand();

                    cmd.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES";
                    _databaseInfo.Rows.Add("Tables", cmd.ExecuteScalar().ToString());

                    cmd.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.INDEXES";
                    _databaseInfo.Rows.Add("Indexes", cmd.ExecuteScalar().ToString());

                    cmd.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE";
                    _databaseInfo.Rows.Add("Keys", cmd.ExecuteScalar().ToString());

                    cmd.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS";
                    _databaseInfo.Rows.Add("Table Constraints", cmd.ExecuteScalar().ToString());

                    cmd.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS";
                    _databaseInfo.Rows.Add("Foreign Constraints", cmd.ExecuteScalar().ToString());

                    return _databaseInfo;
                }
                catch
                {
                    return null;
                }
            }
        }

        private string GetFileSize(long bytes)
        {
            string[] size = { "Bytes", "KB", "MB", "GB", "TB" };
            int log = (int)Math.Log(0.1 + bytes, 1024);
            if (log > 4)
                log = 4;
            double n = bytes / Math.Pow(1024, log);
            return $"{n:N0} {size[log]}";
        }

        private List<string> _tableNames = null;

        public List<string> TableNames
        {
            get
            {
                if (_tableNames != null)
                    return _tableNames;
                _tableNames = new List<string>();
                DbDataReader dr = ExecuteReader("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = N'TABLE'");
                while (dr.Read())
                    _tableNames.Add(dr.GetString(0));
                dr.Close();
                return _tableNames;
            }
        }

        protected void ResetTableNames()
        {
            _tableNames?.Clear();
            _tableNames = null;
        }

        private enum ResultSetOptions : int { None, Updatable, Scrollable, Sensitive, Insensitive };

        public object ExecuteSql(string sql, bool updatable)
        {
            if (Connection == null)
                return null;

            if (Connection.State == ConnectionState.Closed)
                Connection.Open();
 
            LastError = string.Empty;

            object command = assembly.CreateInstance("System.Data.SqlServerCe.SqlCeCommand", false, BindingFlags.CreateInstance, null, new object[] { null, Connection }, null, null);
            var enumType = assembly.GetType("System.Data.SqlServerCe.ResultSetOptions");
            ResultSetOptions options = updatable ? ResultSetOptions.Scrollable | ResultSetOptions.Updatable : ResultSetOptions.Scrollable;

            object result = null;
            QueryCount = 0;

            for (Match m = regexSemicolon.Match(sql); m.Success; m = m.NextMatch())
                if (!string.IsNullOrWhiteSpace(m.Value))
                {
                    QueryCount++;
                    try
                    {
                        command.GetType().InvokeMember("CommandText", BindingFlags.SetProperty, null, command, new object[] { m.Value.Trim() });
                        object resultset = command.GetType().GetMethod("ExecuteResultSet", new System.Type[] { enumType }, null).Invoke(command, new object[] { options });
                        bool scrollable = (bool)resultset.GetType().InvokeMember("Scrollable", BindingFlags.GetProperty, null, resultset, null);
                        if (scrollable)
                            result = resultset;
                    }
                    catch (Exception e)
                    {
                        LastError = $"{GlobalText.GetValue("Query")} {QueryCount}: {(e.InnerException == null ? e.Message : e.InnerException.Message)}";
                        return null;
                    }
                }

            return result;
        }

        public bool Compact(string databaseFile)
        {
            return Compact(databaseFile, null, null);
        }

        public bool Compact(string databaseFile, string password, string newPassword)
        {
            string connectionStr = password == newPassword ? null : $"Data Source=; Password={(newPassword ?? string.Empty)}";
            return Tool(databaseFile, password, "Compact", new object[] { connectionStr });
        }

        private enum RepairOption : int { DeleteCorruptedRows, RecoverCorruptedRows, RecoverAllPossibleRows, RecoverAllOrFail }

        public bool Repair(string databaseFile)
        {
            return Repair(databaseFile, null, null);
        }

        public bool Repair(string databaseFile, string password, string newPassword)
        {
            string connectionStr = password == newPassword ? null : $"Data Source={databaseFile}; Password={(newPassword ?? string.Empty)}";
            bool ok = DoRepair(databaseFile, password, new object[] { connectionStr, RepairOption.RecoverAllPossibleRows });
            if (ok)
                ok = DoRepair(databaseFile, newPassword, new object[] { connectionStr, RepairOption.DeleteCorruptedRows });
            return ok;
        }

        private bool DoRepair(string databaseFile, string password, object[] parameters)
        {
            Close();
            if (!Open(databaseFile, password))
                return false;
            Close();

            string connectionStr = GetConnectionString(databaseFile, password);

            try
            {
                object engine = assembly.CreateInstance("System.Data.SqlServerCe.SqlCeEngine", false, BindingFlags.CreateInstance, null, new object[] { connectionStr }, null, null);
                var enumType = assembly.GetType("System.Data.SqlServerCe.RepairOption");
                engine.GetType().GetMethod("Repair", new System.Type[] { typeof(string), enumType }, null).Invoke(engine, parameters);
                engine.GetType().InvokeMember("Dispose", BindingFlags.InvokeMethod, null, engine, null);
                return true;
            }
            catch (Exception e)
            {
                LastError = (e.InnerException == null) ? e.Message : e.InnerException.Message;
                return false;
            }
        }

        public bool Shrink(string databaseFile)
        {
            return Shrink(databaseFile, null, null);
        }

        public bool Shrink(string databaseFile, string password, string newPassword)
        {
            string connectionStr = password == newPassword ? null : $"Data Source=; Password={(newPassword ?? string.Empty)}";
            return Tool(databaseFile, password, "Shrink", null);
        }

        public bool Upgrade(string databaseFile, Version toVersion)
        {
            return Upgrade(databaseFile, null, null, toVersion);
        }

        public bool Upgrade(string databaseFile, string password, string newPassword, Version toVersion)
        {
            Close();
            OpenConnection(toVersion, databaseFile, password);
            Close();

            string newConnectionStr = password == newPassword ? null : $"Data Source=; Password={(newPassword ?? string.Empty)}";
            string connectionStr = GetConnectionString(databaseFile, password);

            try
            {
                object engine = assembly.CreateInstance("System.Data.SqlServerCe.SqlCeEngine", false, BindingFlags.CreateInstance, null, new object[] { connectionStr }, null, null);
                engine.GetType().InvokeMember("Upgrade", BindingFlags.InvokeMethod, null, engine, new object[] { newConnectionStr });
                return true;
            }
            catch (Exception e)
            {
                LastError = (e.InnerException == null) ? e.Message : e.InnerException.Message;
                return false;
            }
        }

        public bool Verify(string databaseFile)
        {
            return Verify(databaseFile, null);
        }

        public bool Verify(string databaseFile, string password)
        {
            Close();
            if (!Open(databaseFile, password))
                return false;
            Close();

            string connectionStr = GetConnectionString(databaseFile, password);

            try
            {
                LastError = string.Empty;
                object engine = assembly.CreateInstance("System.Data.SqlServerCe.SqlCeEngine", false, BindingFlags.CreateInstance, null, new object[] { connectionStr }, null, null);
                return (bool)engine.GetType().InvokeMember("Verify", BindingFlags.InvokeMethod, null, engine, null);
            }
            catch (Exception e)
            {
                LastError = (e.InnerException == null) ? e.Message : e.InnerException.Message;
                return false;
            }
        }

        public bool CreateDatabase(string databaseFile, Version version)
        {
            return CreateDatabase(databaseFile, null, int.MinValue, version);
        }

        public bool CreateDatabase(string databaseFile, string password, Version version)
        {
            return CreateDatabase(databaseFile, password, int.MinValue, version);
        }

        public bool CreateDatabase(string databaseFile, string password, int lcid, Version version)
        {
            try
            {
                OpenConnection(version, databaseFile, password);
            }
            catch
            {
            }
            Close();

            string connectionStr = $"Data Source={databaseFile}";
            if (!string.IsNullOrEmpty(password))
                connectionStr += $"; Password={password}; Encrypt=TRUE";
            if (lcid != int.MinValue)
                connectionStr += $"; LCID={lcid}";
            try
            {
                object engine = assembly.CreateInstance("System.Data.SqlServerCe.SqlCeEngine", false, BindingFlags.CreateInstance, null, new object[] { connectionStr }, null, null);
                engine.GetType().InvokeMember("CreateDatabase", BindingFlags.InvokeMethod, null, engine, null);
                return true;
            }
            catch (Exception e)
            {
                LastError = (e.InnerException == null) ? e.Message : e.InnerException.Message;
                return false;
            }
        }

        private DbDataReader ExecuteReader(string sql)
        {
            DbCommand cmd = Connection.CreateCommand();
            cmd.CommandText = sql;
            return cmd.ExecuteReader();
        }

        public string GetColumnDataType(string table, string column)
        {
            DbCommand cmd = Connection.CreateCommand();
            cmd.CommandText = $"SELECT DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='{table}' AND COLUMN_NAME='{column}'";
            DbDataReader dr = cmd.ExecuteReader();
            string s = dr.Read() ? dr.GetString(0) : string.Empty;
            dr.Close();
            return s;
        }

        private bool Tool(string databaseFile, string password, string tool, object[] parameters)
        {
            Close();
            if (!Open(databaseFile, password))
                return false;
            Close();

            string connectionStr = GetConnectionString(databaseFile, password);

            try
            {
                object engine = assembly.CreateInstance("System.Data.SqlServerCe.SqlCeEngine", false, BindingFlags.CreateInstance, null, new object[] { connectionStr }, null, null);
                engine.GetType().InvokeMember(tool, BindingFlags.InvokeMethod, null, engine, parameters);
                return true;
            }
            catch (Exception e)
            {
                LastError = (e.InnerException == null) ? e.Message : e.InnerException.Message;
                return false;
            }
        }

        private bool OpenConnection(Version version, string databaseFile, string password)
        {
            Connection = null;
            Version = null;
            string[] vers = version.SqlceVersion.Split('.');

            string connectionStr = GetConnectionString(databaseFile, password);

            try
            {
                assembly = Assembly.Load($"System.Data.SqlServerCe, Version={version.AssemblyVersion}, Culture=neutral, PublicKeyToken=89845dcd8080cc91");
            }
            catch (Exception e)
            {
                try
                {
                    string desktop = (version.SqlceVersion == "3.1") ? string.Empty : @"\Desktop";
                    string assemblyPath = $@"Microsoft SQL Server Compact Edition\v{vers[0]}.{vers[1]}{desktop}\System.Data.SqlServerCe.dll";
                    string progFilePath = Environment.GetFolderPath(IntPtr.Size == 4 ? Environment.SpecialFolder.ProgramFilesX86 : Environment.SpecialFolder.ProgramFiles);
                    assembly = Assembly.LoadFile(Path.Combine(progFilePath, assemblyPath));
                }
                catch
                {
                    LastError = e.Message;
                    return false;
                }
            }
            if (assembly == null)
                return false;

            try
            {
                Connection = (DbConnection)assembly.CreateInstance("System.Data.SqlServerCe.SqlCeConnection", false, BindingFlags.CreateInstance, null, new object[] { connectionStr }, null, null);
                if (Connection == null)
                    return false;
                Connection.Open();
                Version = version;
                return true;
            }
            catch (Exception e)
            {
                int nativeError = int.MinValue;
                try
                {
                    var ex = e.GetBaseException();
                    nativeError = (int)ex.GetType().InvokeMember("NativeError", BindingFlags.GetProperty, null, ex, null);
                }
                catch
                {
                }
                switch (nativeError)
                {
                    case 25138:
                        break;  // Old database version that needs to be upgraded
                    case 25028:
                        BadPassword = true;
                        break;  // Bad password
                    case 28609:
                        break;  // Incorrect version of database, more recent than expected
                    default:
                        throw (e);
                }
                LastError = e.Message;
                return false;
            }
        }

        private string GetConnectionString(string databaseFile, string password)
        {
            string connectionStr = $"Data Source=\"{databaseFile}\"; Max Database Size=4091";
            // Maximum 4 Gb (with value 4096 the program is not able to open databases above 2045 Mb)
            if (!string.IsNullOrEmpty(password))
                connectionStr += $"; Password=\"{password}\"";

            bool readOnly = false;
            try
            {
                var fileInfo = new FileInfo(databaseFile);
                readOnly = (fileInfo.Attributes & FileAttributes.ReadOnly) != 0;
            }
            catch
            {
            }
            if (readOnly)
                connectionStr += $"; Mode=Read Only; Temp Path=\"{Path.GetTempPath()}\"";

            return connectionStr;
        }

        private Version GetSdfVersion(string filePath)
        {
            UInt32 sdfCodeVersion;
            using (var reader = new BinaryReader(File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                reader.BaseStream.Seek(16, SeekOrigin.Begin);
                sdfCodeVersion = reader.ReadUInt32();
                reader.Close();
            }
            return AvailableVersions.FirstOrDefault(version => version.SdfCodeVersion == sdfCodeVersion);
        }
    }
}
