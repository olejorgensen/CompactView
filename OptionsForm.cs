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
using System.Drawing;
using System.Windows.Forms;

namespace CompactView
{
    public partial class OptionsForm : Form
    {
        private DataGridView dataGrid = null;
        private Color backColor1;
        private Color backColor2;
        private Color foreColor1;
        private Color foreColor2;
        private Settings settings;

        public OptionsForm(DataGridView dataGridView, Settings settings)
        {
            InitializeComponent();

            SetCultureTexts();
            this.settings = settings;
            ColorComboBox.LinkTo(cbBackground1);
            ColorComboBox.LinkTo(cbBackground2);
            ColorComboBox.LinkTo(cbFont1);
            ColorComboBox.LinkTo(cbFont2);
            ColorComboBox.SetectColor(cbBackground1, Color.FromArgb(settings.BackColor1));
            ColorComboBox.SetectColor(cbBackground2, Color.FromArgb(settings.BackColor2));
            ColorComboBox.SetectColor(cbFont1, Color.FromArgb(settings.TextColor1));
            ColorComboBox.SetectColor(cbFont2, Color.FromArgb(settings.TextColor2));
            backColor1 = label1.BackColor = label3.BackColor = dataGridView.DefaultCellStyle.BackColor;
            backColor2 = label2.BackColor = label4.BackColor = dataGridView.AlternatingRowsDefaultCellStyle.BackColor;
            foreColor1 = label1.ForeColor = label3.ForeColor = dataGridView.DefaultCellStyle.ForeColor;
            foreColor2 = label2.ForeColor = label4.ForeColor = dataGridView.AlternatingRowsDefaultCellStyle.ForeColor;
            cbColorSet.SelectedIndex = settings.ColorSet;
            dataGrid = dataGridView;
        }

        private void SetCultureTexts()
        {
            this.Text = GlobalText.GetValue("Options");
            groupBox1.Text = GlobalText.GetValue("Colors");
            label6.Text = GlobalText.GetValue("FontColor");
            label5.Text = GlobalText.GetValue("BackgroundColor");
            label1.Text = $"{GlobalText.GetValue("Line")} 1";
            label2.Text = $"{GlobalText.GetValue("Line")} 2";
            label3.Text = $"{GlobalText.GetValue("Line")} 3";
            label4.Text = $"{GlobalText.GetValue("Line")} 4";
            label7.Text = $"{GlobalText.GetValue("ColorSet")}:";
            cbColorSet.Items[cbColorSet.Items.Count - 1] = GlobalText.GetValue("UserDefined");
            groupBox2.Text = GlobalText.GetValue("UserDefined");
            btnOk.Text = GlobalText.GetValue("Ok");
            btnCancel.Text = GlobalText.GetValue("Cancel");
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGrid == null) return;
            label1.BackColor = label3.BackColor = dataGrid.DefaultCellStyle.BackColor = cbBackground1.ForeColor;
            label2.BackColor = label4.BackColor = dataGrid.AlternatingRowsDefaultCellStyle.BackColor = cbBackground2.ForeColor;
            label1.ForeColor = label3.ForeColor = dataGrid.DefaultCellStyle.ForeColor = cbFont1.ForeColor;
            label2.ForeColor = label4.ForeColor = dataGrid.AlternatingRowsDefaultCellStyle.ForeColor = cbFont2.ForeColor;
        }

        private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                settings.ColorSet = cbColorSet.SelectedIndex;
                settings.TextColor1 = cbFont1.ForeColor.ToArgb();
                settings.TextColor2 = cbFont2.ForeColor.ToArgb();
                settings.BackColor1 = cbBackground1.ForeColor.ToArgb();
                settings.BackColor2 = cbBackground2.ForeColor.ToArgb();
            }
            else
            {
                dataGrid.DefaultCellStyle.BackColor = backColor1;
                dataGrid.AlternatingRowsDefaultCellStyle.BackColor = backColor2;
                dataGrid.DefaultCellStyle.ForeColor = foreColor1;
                dataGrid.AlternatingRowsDefaultCellStyle.ForeColor = foreColor2;
            }
        }

        private static int ColorToWinColor(Color color)
        {
            return color.R + (color.G << 8) + (color.B << 16);
        }

        private void ShowColorDlg(ComboBox comboBox)
        {
            colorDialog1.Color = comboBox.ForeColor;
            int[] custom = new int[10];
            for (int i = 0; i < 10; i++) custom[i] = ColorToWinColor(Color.White);
            custom[0] = ColorToWinColor(cbFont1.ForeColor);
            custom[8] = ColorToWinColor(cbFont2.ForeColor);
            custom[1] = ColorToWinColor(cbBackground1.ForeColor);
            custom[9] = ColorToWinColor(cbBackground2.ForeColor);
            colorDialog1.CustomColors = custom;
            if (colorDialog1.ShowDialog() != DialogResult.OK) return;
            ColorComboBox.SetectColor(comboBox, colorDialog1.Color);
            comboBox_SelectedIndexChanged(comboBox, null);
        }

        private void btnFont1_Click(object sender, EventArgs e)
        {
            ShowColorDlg(cbFont1);
        }

        private void btnFont2_Click(object sender, EventArgs e)
        {
            ShowColorDlg(cbFont2);
        }

        private void btnBack1_Click(object sender, EventArgs e)
        {
            ShowColorDlg(cbBackground1);
        }

        private void btnBack2_Click(object sender, EventArgs e)
        {
            ShowColorDlg(cbBackground2);
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            // Move up the form to allow combo boxes dropped down
            int mainTop = Application.OpenForms[0].Top + 90;
            if (Top > mainTop) Top = mainTop;
        }

        public static void SelectColors(DataGridView dataGrid, Settings settings, int index)
        {
            Color cFont1 = Color.Black;
            Color cFont2 = Color.Black;
            Color cBack1 = Color.LightYellow;
            Color cBack2 = Color.LemonChiffon;

            switch (index)
            {
                case 0: 
                    break;
                case 1:
                    cFont1 = Color.Black;
                    cFont2 = Color.Black;
                    cBack1 = Color.MintCream;
                    cBack2 = Color.PaleGreen;
                    break;
                case 2:
                    cFont1 = Color.Maroon;
                    cFont2 = Color.Black;
                    cBack1 = Color.White;
                    cBack2 = Color.NavajoWhite;
                    break;
                case 3:
                    cFont1 = Color.Navy;
                    cFont2 = Color.Black;
                    cBack1 = Color.Honeydew;
                    cBack2 = Color.PaleTurquoise;
                    break;
                case 4:
                    cFont1 = Color.Black;
                    cFont2 = Color.White;
                    cBack1 = Color.Azure;
                    cBack2 = Color.DarkCyan;
                    break;
                case 5:
                    cFont1 = Color.FromArgb(settings.TextColor1);
                    cFont2 = Color.FromArgb(settings.TextColor2);
                    cBack1 = Color.FromArgb(settings.BackColor1);
                    cBack2 = Color.FromArgb(settings.BackColor2);
                    break;
            }

            dataGrid.DefaultCellStyle.ForeColor = cFont1;
            dataGrid.AlternatingRowsDefaultCellStyle.ForeColor = cFont2;
            dataGrid.DefaultCellStyle.BackColor = cBack1;
            dataGrid.AlternatingRowsDefaultCellStyle.BackColor = cBack2;
        }

        private void cbColorSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox2.Enabled = cbColorSet.SelectedIndex == 5;
            if (dataGrid == null) return;
            SelectColors(dataGrid, settings, cbColorSet.SelectedIndex);
            label1.BackColor = label3.BackColor = dataGrid.DefaultCellStyle.BackColor;
            label2.BackColor = label4.BackColor = dataGrid.AlternatingRowsDefaultCellStyle.BackColor;
            label1.ForeColor = label3.ForeColor = dataGrid.DefaultCellStyle.ForeColor;
            label2.ForeColor = label4.ForeColor = dataGrid.AlternatingRowsDefaultCellStyle.ForeColor;
        }
    }
}
