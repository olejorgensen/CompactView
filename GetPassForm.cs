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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CompactView
{
    public partial class GetPassForm : Form
    {
        public GetPassForm()
        {
            InitializeComponent();
            SetCultureTexts();

            this.ActiveControl = edPass;
            btOK.Focus();
        }

        private void SetCultureTexts()
        {
            this.Text = GlobalText.GetValue("Password");
            label1.Text = GlobalText.GetValue("PasswordNote");
            btOK.Text = GlobalText.GetValue("Ok");
            btCancel.Text = GlobalText.GetValue("Cancel");
        }
    }
}
