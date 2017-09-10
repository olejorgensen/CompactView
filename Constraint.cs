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

namespace CompactView
{
    public class Constraint
    {
        public string ConstraintTableName { get; set; }
        public string ConstraintName { get; set; }
        public string ColumnName { get; set; }
        public ColumnList Columns { get; set; }
        public string UniqueConstraintTableName { get; set; }
        public string UniqueConstraintName { get; set; }
        public string UniqueColumnName { get; set; }
        public ColumnList UniqueColumns { get; set; }
        public string DeleteRule { get; set; }
        public string UpdateRule { get; set; }
    }
}
