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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CompactView
{
    public static class ColorComboBox
    {
        private static List<Brush> brushes = null;
        private static List<string> colorNames = null;
        private static List<int> argbColors = null;
        private static Bitmap buffer = null;  // Use buffer to avoid flickering
        private static Pen grayDotPen = new Pen(Color.LightGray);
        private static Pen blackDotPen = new Pen(Color.Black);

        /// <summary>
        /// Link the specified ComboBox with the ColorComboBox behavior
        /// </summary>
        /// <param name="comboBox">ComboBox to be used as ColorComboBox</param>
        public static void LinkTo(ComboBox comboBox)
        {
            if (brushes == null)
                Init();

            int width = buffer == null ? 0 : buffer.Width;
            int height = buffer == null ? 0 : buffer.Height;
            if (comboBox.Width > width || comboBox.Height > height)
            {
                if (comboBox.Width > width)
                    width = comboBox.Width;
                if (comboBox.Height > height)
                    height = comboBox.Height;
                if (buffer != null)
                    buffer.Dispose();
                buffer = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            }

            comboBox.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Sorted = false;
            comboBox.Items.AddRange(colorNames.ToArray());
            comboBox.ForeColor = Color.Transparent;  // ForeColor save the color selected. To start any color is not selected
            comboBox.DrawItem += ComboBox_DrawItem;
            comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            comboBox.DropDownClosed += ComboBox_DropDownClosed;
        }

        /// <summary>
        /// Select the desired color on the ComboBox
        /// </summary>
        /// <param name="comboBox">ComboBox which you want to make the selection</param>
        /// <param name="colorName">Name of the color to select</param>
        public static void SetectColor(ComboBox comboBox, string colorName)
        {
            int i = colorNames.IndexOf(colorName);
            comboBox.SelectedIndex = i;
        }

        /// <summary>
        /// Select the desired color on the ComboBox
        /// </summary>
        /// <param name="comboBox">ComboBox which you want to make the selection</param>
        /// <param name="color">Color to select</param>
        public static void SetectColor(ComboBox comboBox, Color color)
        {
            int i = colorNames.IndexOf(color.Name);
            if (i < 0)
                i = argbColors.IndexOf(color.ToArgb());
            comboBox.ForeColor = i < 0 ? color : Color.FromName(colorNames[i]);
            comboBox.SelectedIndex = i;
            if (i < 0)
                comboBox.ForeColor = color;  // if set SelectedIndex < 0 the calling to event SelectedIndexChanged will set the color as transparent
        }

        private static void Init()
        {
            grayDotPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            blackDotPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            var colors = Enum.GetValues(typeof(KnownColor))
                .Cast<KnownColor>()
                .Where(k => k > KnownColor.Transparent && k < KnownColor.ButtonFace) //Exclude transparent and system colors
                .Select(k => Color.FromKnownColor(k))
                .OrderBy(c => c.GetHue())
                .ThenBy(c => c.GetSaturation())
                .ThenBy(c => c.GetBrightness());

            brushes = new List<Brush>();
            colorNames = new List<string>();
            argbColors = new List<int>();
            foreach (Color color in colors)
            {
                colorNames.Add(color.Name);
                brushes.Add(new SolidBrush(color));
                argbColors.Add(color.ToArgb());
            }
        }

        private static void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            var graphics = Graphics.FromImage(buffer);  // Drawing through the buffer to avoid flickering
            graphics.FillRectangle(Brushes.White, 0, 0, e.Bounds.Width, e.Bounds.Height);
            if (e.Index < 0)
                return;

            bool selected = colorNames[e.Index] == (sender as ComboBox).ForeColor.Name;
            if (((e.State & DrawItemState.Selected) != 0 || selected) && e.Bounds.Top != 3)
            {
                var rectBounds = new Rectangle(0, 0, e.Bounds.Width - 1, e.Bounds.Height - 1);
                Pen pen = selected ? blackDotPen : grayDotPen;
                graphics.DrawRectangle(pen, rectBounds);
            }

            var rectColor = new Rectangle(1, 1, 30, e.Bounds.Height - 3);
            graphics.FillRectangle(brushes[e.Index], rectColor);
            graphics.DrawRectangle(Pens.Black, rectColor);
            graphics.DrawString(colorNames[e.Index], e.Font, Brushes.Black, 34, 1);
            e.Graphics.DrawImageUnscaledAndClipped(buffer, e.Bounds);
        }

        private static void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (!comboBox.DroppedDown)
                ComboBox_DropDownClosed(sender, e);
        }

        private static void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            comboBox.ForeColor = comboBox.SelectedIndex >= 0 ? Color.FromName(colorNames[comboBox.SelectedIndex]) : Color.Transparent;
        }
    }
}
