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
using System.Collections.Generic;
using System.Text;

namespace CompactView
{
    public class ColumnList : List<string>
    {
        public override string ToString()
        {
            var str = new StringBuilder();
            foreach (string column in this)
            {
                if (column.StartsWith("["))
                    str.Append($"{column}, ");
                else
                    str.Append($"[{column}], ");
            }
            if (str.Length >= 2)
                str.Remove(str.Length - 2, 2);
            return str.ToString();
        }
    }
}
