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
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CompactView
{
    public class SqlParser
    {
        private string[] keywords = { "action", "add", "all", "alter", "and", "any", "as", "asc", "authorization", "avg", "backup", "begin", "between", "break", "browse", "bulk", "by", "cascade", 
                                  "case", "check", "checkpoint", "close", "clustered", "coalesce", "collate", "column", "commit", "compute", "constraint", "contains", "containstable",
                                  "continue", "convert", "count", "create", "cross", "current", "current_date", "current_time", "current_timestamp", "current_user", "cursor",
                                  "database", "databasepassword", "dateadd", "datediff", "datename", "datepart", "dbcc", "deallocate", "declare", "default", "delete", "deny", "desc",
                                  "disk", "distinct", "distributed", "double", "drop", "dump", "else", "encryption", "end", "errlvl", "escape", "except", "exec", "execute", "exists",
                                  "exit", "expression", "fetch", "file", "fillfactor", "for", "foreign", "freetext", "freetexttable", "from", "full", "function", "goto", "grant",
                                  "group", "having", "holdlock", "identity", "identity_insert", "identitycol", "if", "in", "index", "inner", "insert", "intersect", "into", "is",
                                  "join", "key", "kill", "left", "like", "lineno", "load", "max", "min", "national", "no", "nocheck", "nonclustered", "not", "null", "nullif", "of", "off",
                                  "offsets", "on", "open", "opendatasource", "openquery", "openrowset", "openxml", "option", "or", "order", "outer", "over", "percent", "plan",
                                  "precision", "primary", "print", "proc", "procedure", "public", "raiserror", "read", "readtext", "reconfigure", "references", "replication",
                                  "restore", "restrict", "return", "revoke", "right", "rollback", "rowcount", "rowguidcol", "rule", "save", "schema", "select", "session_user", "set",
                                  "setuser", "shutdown", "some", "statistics", "sum", "system_user", "table", "textsize", "then", "to", "top", "tran", "transaction", "trigger",
                                  "truncate", "tsequal", "union", "unique", "update", "updatetext", "use", "user", "values", "varying", "view", "waitfor", "when", "where", "while",
                                  "with", "writetext" };
        private string[] types = { "bigint", "int", "integer", "smallint", "tinyint", "bit", "numeric", "decimal", "dec", "money", "float", "real", "datetime", "national character",
                                  "nchar", "national character varying", "nvarchar", "ntext", "binary", "varbinary", "image", "uniqueidentifier", "timestamp", "rowversion" };
        private Regex regexNumbers;
        private Regex regexKeywords;
        private Regex regexStrings;
        private Regex regexKeysQuoted;
        private Regex regexKeysBrackets;
        private Regex regexPar;
        private Regex regexInsertColortbl;
        private Regex regexTypes;
        private RichTextBox richTextBoxAux = new RichTextBox();
        private RichTextBox _RichTextBox;
        private int selectionStart;
        private int selectionLength;

        public SqlParser()
        {
            var pattern = new StringBuilder(@"(?<A>\b(");
            foreach (string key in keywords) pattern.Append(key + "|");
            pattern.Length--;
            pattern.Append(@")\b)");
            regexKeywords = new Regex(pattern.ToString(), RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);

            pattern.Clear();
            pattern.Append(@"(?<A>\b(");
            foreach (string typ in types) pattern.Append(typ + "|");
            pattern.Length--;
            pattern.Append(@")\b)");
            regexTypes = new Regex(pattern.ToString(), RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.IgnoreCase);

            regexInsertColortbl = new Regex(@";}}");
            regexKeysQuoted = new Regex(@"(?<A>(?<!\\)')(?<B>(\\'|[^'\\])*)(?<C>\\cf[134] )(?<D>[^'\\]*)(?<E>\\cf0 )(?<F>(\\'|[^'])*')", RegexOptions.Compiled | RegexOptions.Multiline);
            regexKeysBrackets = new Regex(@"(?<A>\[)(?<B>(\\'|[^\\])*)(?<C>\\cf[134] )(?<D>[^\\]*)(?<E>\\cf0 )(?<F>[^\[]*\])", RegexOptions.Compiled | RegexOptions.Multiline);
            regexNumbers = new Regex(@"(?<A>\b\d+\b)", RegexOptions.Compiled | RegexOptions.Multiline);
            regexStrings = new Regex(@"(?<A>(?<!\\)'(\\'|[^'])*(?<!\\)')", RegexOptions.Compiled | RegexOptions.Multiline);
            regexPar = new Regex(@"\\par\b", RegexOptions.Compiled | RegexOptions.Multiline | RegexOptions.RightToLeft);
            string s = Parse(""); // Initialize Regex;
        }

        public string Parse(string text)
        {
            if (_RichTextBox != null && _RichTextBox.Font != richTextBoxAux.Font) richTextBoxAux.Font = _RichTextBox.Font;
            richTextBoxAux.Clear();
            richTextBoxAux.Text = text;
            richTextBoxAux.SelectAll();
            string rtf = richTextBoxAux.SelectedRtf.Replace(@"\rtf1", @"\rtf1{\colortbl ;\red255\green0\blue0;\red0\green128\blue0;\red0\green0\blue255;\red0\green128\blue128;}");

            rtf = regexNumbers.Replace(rtf, @"\cf1 ${A}\cf0 ");
            rtf = regexKeywords.Replace(rtf, @"\cf3 ${A}\cf0 ");
            rtf = regexTypes.Replace(rtf, @"\cf4 ${A}\cf0 ");
            rtf = regexStrings.Replace(rtf, @"\cf2 ${A}\cf0 ");
            while (regexKeysQuoted.IsMatch(rtf)) rtf = regexKeysQuoted.Replace(rtf, "${A}${B}${D}${F}");
            while (regexKeysBrackets.IsMatch(rtf)) rtf = regexKeysBrackets.Replace(rtf, "${A}${B}${D}${F}");

            return rtf;
        }

        public RichTextBox RichTextBox
        {
            get
            {
                return _RichTextBox;
            }

            set
            {
                if (value == _RichTextBox) return;
                if (_RichTextBox != null)
                {
                    _RichTextBox.VScroll -= RichTextBox_Event;
                    _RichTextBox.TextChanged -= RichTextBox_Event;
                    _RichTextBox.SizeChanged -= RichTextBox_Event;
                }
                _RichTextBox = value;
                _RichTextBox.VScroll += RichTextBox_Event;
                _RichTextBox.TextChanged += RichTextBox_Event;
                _RichTextBox.SizeChanged += RichTextBox_Event;
            }
        }

        public void Update()
        {
            RichTextBox_Event(null, null);
        }

        private bool exit = false;

        public void ParseRichTextBox(int posIniChar, int posEndChar)
        {
            if (exit) return;
            exit = true;

            int firstVisibleLineBefore = _RichTextBox.GetFirstVisibleLine();
            int hScroll = _RichTextBox.GetHScroll();
            selectionLength = _RichTextBox.SelectionLength;
            selectionStart = _RichTextBox.SelectionStart;

            _RichTextBox.SetRedraw(false);
            _RichTextBox.Select(posIniChar, posEndChar - posIniChar + 1);
            _RichTextBox.SelectedRtf = Parse(_RichTextBox.SelectedText);

            _RichTextBox.Select(selectionStart, selectionLength);
            _RichTextBox.SetHScroll(hScroll);
            int firstVisibleLineAfter = _RichTextBox.GetFirstVisibleLine();
            _RichTextBox.ScrollLines(firstVisibleLineBefore - firstVisibleLineAfter);

            _RichTextBox.SetRedraw(true);
            _RichTextBox.Refresh();

            exit = false;
        }

        private void RichTextBox_Event(object sender, EventArgs e)
        {
            int posIni = _RichTextBox.GetFirstVisibleCharIndex();
            int posEnd = _RichTextBox.GetLastVisibleCharIndex();
            ParseRichTextBox(posIni, posEnd);
        }
    }
}
