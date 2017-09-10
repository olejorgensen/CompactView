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
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CompactView
{
    public class SqlCeDb : SqlCeBase
    {
        private System.Globalization.CultureInfo culture;
        private Dictionary<string, string> tablesDdl = new Dictionary<string, string>();
        private Dictionary<string, string> primaryKeysDdl = new Dictionary<string, string>();
        private Dictionary<string, string> indexesDdl = new Dictionary<string, string>();
        private Dictionary<string, string> foreignKeysDdl = new Dictionary<string, string>();

        public SqlCeDb() : base()
        {
            culture = System.Globalization.CultureInfo.InvariantCulture;
        }

        new public bool Open(string databaseFile, string password)
        {
            tablesDdl.Clear();
            return base.Open(databaseFile, password);
        }

        new public void Close()
        {
            ResetDdl();
            base.Close();
        }

        public string GetDatabaseDdl()
        {
            var ddl = new StringBuilder();
            string newLine = string.Empty;
            foreach (string tableName in TableNames)
            {
                ddl.Append(newLine + GetTableDdl(tableName, true, false, false, false));
                newLine = Environment.NewLine;
            }
            foreach (string tableName in TableNames)
                ddl.Append(GetTableDdl(tableName, false, true, false, false));
            foreach (string tableName in TableNames)
                ddl.Append(GetTableDdl(tableName, false, false, true, false));
            foreach (string tableName in TableNames)
                ddl.Append(GetTableDdl(tableName, false, false, false, true));
            return ddl.ToString();
        }

        public string GetTableDdl(string tableName, bool table, bool primaryKeys, bool indexes, bool foreignKeys)
        {
            if (!tablesDdl.ContainsKey(tableName))
                LoadTableDdl(tableName);
            var ddl = new StringBuilder();
            if (table)
                ddl.Append(tablesDdl[tableName]);
            if (primaryKeys)
                ddl.Append(primaryKeysDdl[tableName]);
            if (indexes)
                ddl.Append(indexesDdl[tableName]);
            if (foreignKeys)
                ddl.Append(foreignKeysDdl[tableName]);
            return ddl.ToString();
        }

        public object GetTableData(string tableName, string orderColumn, SortOrder sortOrder)
        {
            string sql = string.IsNullOrEmpty(orderColumn) || (sortOrder == SortOrder.None) ? $"SELECT * FROM [{tableName}]" :
                $"SELECT * FROM [{tableName}] ORDER BY [{orderColumn}] {(sortOrder == SortOrder.Ascending ? "ASC" : "DESC")}";
            return ExecuteSql(sql, true);
        }

        public void ResetDdl()
        {
            tablesDdl.Clear();
            primaryKeysDdl.Clear();
            indexesDdl.Clear();
            foreignKeysDdl.Clear();
            ResetTableNames();
        }

        public TreeNode[] GetSchemaNodes(string tableName)
        {
            string sWhere = tableName == null ? string.Empty : $"WHERE c.TABLE_NAME = '{tableName}' ";
            var nodes = new List<TreeNode>();
            DbCommand cmd = Connection.CreateCommand();
            cmd.CommandText = "SELECT c.COLUMN_NAME, c.TABLE_NAME, t.CONSTRAINT_TYPE FROM INFORMATION_SCHEMA.COLUMNS AS c " +
                "LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS u ON u.TABLE_NAME = c.TABLE_NAME AND u.COLUMN_NAME = c.COLUMN_NAME " +
                "LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS t ON t.CONSTRAINT_NAME = u.CONSTRAINT_NAME AND t.TABLE_NAME = c.TABLE_NAME " +
                "ORDER BY c.TABLE_NAME, c.ORDINAL_POSITION";

            cmd.CommandText = "SELECT c.COLUMN_NAME, c.TABLE_NAME, i.PRIMARY_KEY, t.UNIQUE_CONSTRAINT_TABLE_NAME + '@' + u2.COLUMN_NAME " +
                "FROM INFORMATION_SCHEMA.COLUMNS AS c " +
                "LEFT JOIN INFORMATION_SCHEMA.INDEXES AS i ON i.TABLE_NAME = c.TABLE_NAME AND i.COLUMN_NAME = c.COLUMN_NAME " +
                "LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS u ON u.TABLE_NAME = c.TABLE_NAME AND u.COLUMN_NAME = c.COLUMN_NAME " +
                "LEFT JOIN INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS t ON t.CONSTRAINT_TABLE_NAME = c.TABLE_NAME AND t.CONSTRAINT_NAME = u.CONSTRAINT_NAME " +
                "LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS u2 ON u2.TABLE_NAME = t.UNIQUE_CONSTRAINT_TABLE_NAME AND u2.CONSTRAINT_NAME = t.UNIQUE_CONSTRAINT_NAME " +
                sWhere + "ORDER BY c.TABLE_NAME, c.ORDINAL_POSITION";
            DbDataReader dr = cmd.ExecuteReader();

            TreeNode node = null;
            string sTableName = string.Empty;
            string fieldName = string.Empty;
            string referenc = null;
            bool fieldChange = false;
            int index = 1;

            while (dr.Read())
            {
                if (dr.GetString(1) != sTableName)
                    fieldChange = true;
                if (dr.GetString(0) != fieldName)
                    fieldChange = true;

                if (fieldChange)
                {
                    if (!string.IsNullOrEmpty(fieldName))
                        node.Nodes.Add(referenc, fieldName, index, index).Checked = true;

                    if (dr.GetString(1) != sTableName)
                    {
                        node = new TreeNode(dr.GetString(1), 0, 0)
                        {
                            Checked = true
                        };
                        nodes.Add(node);
                    }
                    sTableName = dr.GetString(1);
                    fieldName = dr.GetString(0);
                    referenc = null;
                    fieldChange = false;
                    index = 1;
                }

                if (!dr.IsDBNull(2) && dr.GetBoolean(2))
                    index = 3;
                if (!dr.IsDBNull(2) && !dr.GetBoolean(2) && index == 1)
                    index = 2;
                if (!dr.IsDBNull(3) && index != 3)
                    index = 4;
                if (!dr.IsDBNull(3))
                    referenc = dr.GetString(3);
            }
            if (!string.IsNullOrEmpty(fieldName))
                node.Nodes.Add(fieldName, fieldName, index, index).Checked = true;
            dr.Close();

            return nodes.ToArray<TreeNode>();
        }

        private void LoadTableDdl(string tableName)
        {
            var ddl = new StringBuilder();
            ddl.Append($"CREATE TABLE [{tableName}]{Environment.NewLine}({Environment.NewLine}");
            AddColumnsDdl(ref ddl, tableName);
            ddl.Append($"{Environment.NewLine});{Environment.NewLine}");
            tablesDdl.Add(tableName, ddl.ToString());

            primaryKeysDdl.Add(tableName, GetPrimaryKeysDdl(tableName));
            indexesDdl.Add(tableName, GetIndexesDdl(tableName));
            foreignKeysDdl.Add(tableName, GetForeignKeysDdl(tableName));
        }

        private string GetColumnType(string tableName, string columnName)
        {
            DbCommand cmd = Connection.CreateCommand();
            cmd.CommandText = $"SELECT COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, NUMERIC_PRECISION, NUMERIC_SCALE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='[{tableName}] AND COLUMN_NAME=[{columnName}]";
            DbDataReader dr = cmd.ExecuteReader();

            var s = string.Empty;
            if (dr.Read())
            {
                if (!dr.IsDBNull(2))
                {
                    s = $"Data type: {dr.GetString(1)}\r\nMax length: {dr.GetInt32(2)}";
                }
                else
                {
                    s = $"Data type: {dr.GetString(1)}\r\nPrecision: {dr.GetValue(3)}\r\nScale: {dr.GetValue(4)}";
                }
            }
            dr.Close();

            return s;
        }

        private Column GetColumn(ref DbDataReader dr)
        {
            var col = new Column
            {
                ColumnName = dr.GetString(0),
                ColumnHasDefault = (dr.IsDBNull(1) ? false : dr.GetBoolean(1)),
                ColumnDefault = (dr.IsDBNull(2) ? string.Empty : dr.GetString(2).Trim()),
                RowGuidCol = (dr.IsDBNull(3) ? false : dr.GetInt32(3) == 378 || dr.GetInt32(3) == 282),
                IsNullable = dr.GetString(4).ToLower() == "yes",
                DataType = dr.GetString(5),
                CharacterMaxLength = (dr.IsDBNull(6) ? 0 : dr.GetInt32(6)),
                NumericPrecision = (dr.IsDBNull(7) ? 0 : Convert.ToInt32(dr[7], culture)),
                NumericScale = (dr.IsDBNull(8) ? 0 : Convert.ToInt32(dr[8], culture)),
                AutoIncrementNext = (dr.IsDBNull(9) ? 0 : Convert.ToInt64(dr[9], culture)),
                AutoIncrementSeed = (dr.IsDBNull(10) ? 0 : Convert.ToInt64(dr[10], culture)),
                AutoIncrementBy = (dr.IsDBNull(11) ? 0 : Convert.ToInt64(dr[11], culture))
            };
            return col;
        }

        private Index GetIndex(ref DbDataReader dr)
        {
            var idx = new Index
            {
                IndexName = dr.GetString(0),
                Unique = dr.GetBoolean(2),
                Clustered = dr.GetBoolean(3),
                OrdinalPosition = dr.GetInt32(4),
                ColumnName = dr.GetString(5),
                SortOrder = (dr.GetInt16(6) == 1 ? SortOrderEnum.ASC : SortOrderEnum.DESC)
            };
            return idx;
        }

        private Constraint GetConstraint(ref DbDataReader dr)
        {
            var cst = new Constraint
            {
                ConstraintTableName = dr.GetString(0),
                ConstraintName = dr.GetString(1),
                UniqueConstraintTableName = dr.GetString(2),
                UniqueConstraintName = dr.GetString(3),
                DeleteRule = dr.GetString(4),
                UpdateRule = dr.GetString(5),

                Columns = new ColumnList()
            };
            DbCommand cmd = Connection.CreateCommand();
            cmd.CommandText = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME = '{cst.ConstraintTableName}' AND CONSTRAINT_NAME = '{cst.ConstraintName}' ORDER BY ORDINAL_POSITION ASC";
            DbDataReader drColumns = cmd.ExecuteReader();
            while (drColumns.Read())
                cst.Columns.Add(drColumns.GetString(0));
            drColumns.Close();

            cst.UniqueColumns = new ColumnList();
            cmd.CommandText = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME = '{cst.UniqueConstraintTableName}' AND CONSTRAINT_NAME = '{cst.UniqueConstraintName}' ORDER BY ORDINAL_POSITION ASC";
            DbDataReader drUniqueColumns = cmd.ExecuteReader();
            while (drUniqueColumns.Read())
                cst.UniqueColumns.Add(drUniqueColumns.GetString(0));
            drUniqueColumns.Close();

            return cst;
        }

        private void AddColumnLine(ref StringBuilder ddl, Column col)
        {
            switch (col.DataType)
            {
                case "nvarchar":
                    ddl.AppendFormat(culture,
                        "   [{0}] {1}({2}){3}{4}",
                        col.ColumnName,
                        "NVARCHAR",
                        col.CharacterMaxLength,
                        (col.IsNullable ? string.Empty : " NOT NULL"),
                        (col.ColumnHasDefault ? $" DEFAULT {col.ColumnDefault}" : string.Empty)
                        );
                    break;
                case "nchar":
                    ddl.AppendFormat(culture,
                        "   [{0}] {1}({2}){3}{4}",
                        col.ColumnName,
                        "NCHAR",
                        col.CharacterMaxLength,
                        (col.IsNullable ? string.Empty : " NOT NULL"),
                        (col.ColumnHasDefault ? $" DEFAULT {col.ColumnDefault}" : string.Empty)
                        );
                    break;
                case "numeric":
                    ddl.AppendFormat(culture,
                        "   [{0}] {1}({2},{3}){4}{5}",
                        col.ColumnName,
                        "DECIMAL",
                        col.NumericPrecision,
                        col.NumericScale,
                        (col.IsNullable ? string.Empty : " NOT NULL"),
                        (col.ColumnHasDefault ? $" DEFAULT {col.ColumnDefault}" : string.Empty)
                        );
                    break;
                case "binary":
                    ddl.AppendFormat
                    (
                        culture,
                        "   [{0}] {1}({2}){3}{4}",
                        col.ColumnName,
                        "BINARY",
                        col.CharacterMaxLength,
                        (col.IsNullable ? string.Empty : " NOT NULL"),
                        (col.ColumnHasDefault ? $" DEFAULT {col.ColumnDefault}" : string.Empty)
                    );
                    break;
                case "varbinary":
                    ddl.AppendFormat
                    (
                        culture,
                        "   [{0}] {1}({2}){3}{4}",
                        col.ColumnName,
                        "VARBINARY",
                        col.CharacterMaxLength,
                        (col.IsNullable ? string.Empty : " NOT NULL"),
                        (col.ColumnHasDefault ? $" DEFAULT {col.ColumnDefault}" : string.Empty)
                    );
                    break;
                default:
                    string rowGuidCol = string.Empty;
                    if (col.RowGuidCol)
                    {
                        rowGuidCol = " ROWGUIDCOL";
                    }
                    ddl.AppendFormat
                    (
                        culture,
                        "   [{0}] {1}{2}{3}{4}{5}",
                        col.ColumnName,
                        col.DataType.ToUpper(),
                        (col.IsNullable ? string.Empty : " NOT NULL"),
                        (col.ColumnHasDefault ? $" DEFAULT {col.ColumnDefault}" : string.Empty),
                        rowGuidCol,
                        (col.AutoIncrementBy > 0 ? string.Format(culture, " IDENTITY ({0},{1})", col.AutoIncrementSeed, col.AutoIncrementBy) : string.Empty)
                    );
                    break;
            }
        }

        private void AddColumnsDdl(ref StringBuilder ddl, string tableName)
        {
            DbCommand cmd = Connection.CreateCommand();
            cmd.CommandText = $"SELECT COLUMN_NAME, COLUMN_HASDEFAULT, COLUMN_DEFAULT, COLUMN_FLAGS, IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, NUMERIC_PRECISION, NUMERIC_SCALE, AUTOINC_NEXT, AUTOINC_SEED, AUTOINC_INCREMENT FROM INFORMATION_SCHEMA.COLUMNS WHERE SUBSTRING(COLUMN_NAME, 1,5) <> '__sys' AND TABLE_NAME = '{tableName}' ORDER BY ORDINAL_POSITION ASC";
            DbDataReader dr = cmd.ExecuteReader();
            bool first = true;
            while (dr.Read())
            {
                if (first)
                    first = false;
                else
                    ddl.Append($",{Environment.NewLine}");
                AddColumnLine(ref ddl, GetColumn(ref dr));
            }
            dr.Close();
        }

        public string GetAutoincNext(string tableName)
        {
            DbCommand cmd = Connection.CreateCommand();
            cmd.CommandText = $"SELECT AUTOINC_NEXT FROM INFORMATION_SCHEMA.COLUMNS WHERE SUBSTRING(COLUMN_NAME, 1,5) <> '__sys' AND AUTOINC_NEXT IS NOT NULL AND TABLE_NAME = '{tableName}'";
            DbDataReader dr = cmd.ExecuteReader();
            string identityNext = null;
            if (dr.Read() && !dr.IsDBNull(0))
                identityNext = dr[0].ToString();
            dr.Close();
            return identityNext;
        }

        public List<string> GetColumns(string tableName)
        {
            var list = new List<string>();

            DbCommand cmd = Connection.CreateCommand();
            cmd.CommandText = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE SUBSTRING(COLUMN_NAME, 1,5) <> '__sys' AND TABLE_NAME = '{tableName}' ORDER BY ORDINAL_POSITION ASC";
            DbDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                list.Add(dr.GetString(0));
            dr.Close();

            return list;
        }

        private string GetPrimaryKeysDdl(string tableName)
        {
            DbCommand cmd = Connection.CreateCommand();
            cmd.CommandText = $"SELECT u.COLUMN_NAME, c.CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS c INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS u ON c.CONSTRAINT_NAME = u.CONSTRAINT_NAME AND u.TABLE_NAME = c.TABLE_NAME WHERE c.CONSTRAINT_TYPE = 'PRIMARY KEY' AND c.TABLE_NAME = '{tableName}' ORDER BY c.CONSTRAINT_NAME, u.ORDINAL_POSITION";
            DbDataReader dr = cmd.ExecuteReader();
            var ddl = new StringBuilder();
            bool first = true;
            while (dr.Read())
            {
                if (first)
                    ddl.AppendFormat("{0}ALTER TABLE [{1}] ADD CONSTRAINT [{2}] PRIMARY KEY (", Environment.NewLine, tableName, dr.GetString(1));
                else
                    ddl.Append(", ");
                ddl.Append($"[{dr.GetString(0)}]");
                first = false;
            }
            if (!first)
                ddl.Append($");{Environment.NewLine}");
            dr.Close();
            return ddl.ToString();
        }

        private string GetIndexesDdl(string tableName)
        {
            DbCommand cmd = Connection.CreateCommand();
            cmd.CommandText = $"SELECT INDEX_NAME, PRIMARY_KEY, [UNIQUE], [CLUSTERED], ORDINAL_POSITION, COLUMN_NAME, [COLLATION] AS SORT_ORDER FROM INFORMATION_SCHEMA.INDEXES WHERE (PRIMARY_KEY = 0) AND (TABLE_NAME = '{tableName}') AND (SUBSTRING(COLUMN_NAME, 1,5) <> '__sys') ORDER BY INDEX_NAME, ORDINAL_POSITION";
            DbDataReader dr = cmd.ExecuteReader();
            var ddl = new StringBuilder();
            string prevName = string.Empty;
            while (dr.Read())
            {
                Index idx = GetIndex(ref dr);
                if (idx.IndexName == prevName)
                {
                    ddl.Append(", ");
                }
                else
                {
                    if (!string.IsNullOrEmpty(prevName))
                        ddl.Append($");{Environment.NewLine}");
                    ddl.Append($"{Environment.NewLine}CREATE ");
                    if (idx.Unique)
                        ddl.Append("UNIQUE ");
                    if (idx.Clustered)
                        ddl.Append("CLUSTERED ");
                    ddl.Append($"INDEX [{idx.IndexName}] ON [{tableName}] (");
                }
                ddl.Append($"[{idx.ColumnName}] {idx.SortOrder.ToString()}");
                prevName = idx.IndexName;
            }
            if (!string.IsNullOrEmpty(prevName))
                ddl.Append($");{Environment.NewLine}");
            dr.Close();
            return ddl.ToString();
        }

        private string GetForeignKeysDdl(string tableName)
        {
            DbCommand cmd = Connection.CreateCommand();
            cmd.CommandText = $"SELECT DISTINCT CONSTRAINT_TABLE_NAME, CONSTRAINT_NAME, UNIQUE_CONSTRAINT_TABLE_NAME, UNIQUE_CONSTRAINT_NAME, DELETE_RULE, UPDATE_RULE FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS WHERE CONSTRAINT_TABLE_NAME='{tableName}'";
            DbDataReader dr = cmd.ExecuteReader();
            var ddl = new StringBuilder();
            while (dr.Read())
            {
                Constraint cst = GetConstraint(ref dr);
                ddl.Append($"{Environment.NewLine}ALTER TABLE [{cst.ConstraintTableName}] ADD CONSTRAINT [{cst.ConstraintName}] FOREIGN KEY ({cst.Columns.ToString()}){Environment.NewLine}   REFERENCES [{cst.UniqueConstraintTableName}] ({cst.UniqueColumns.ToString()}) ON DELETE {cst.DeleteRule} ON UPDATE {cst.UpdateRule};{Environment.NewLine}");
            }
            dr.Close();
            return ddl.ToString();
        }
    }

    enum SortOrderEnum
    {
        ASC, DESC
    }

    struct Index
    {
        public string IndexName { get; set; }
        public bool Unique { get; set; }
        public bool Clustered { get; set; }
        public int OrdinalPosition { get; set; }
        public string ColumnName { get; set; }
        public SortOrderEnum SortOrder { get; set; }
    }
}
