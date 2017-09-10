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
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using System.Diagnostics;

/// <summary>
/// Printing type to define how to perform the printing process
/// </summary>
public enum PrintType
{ 
    /// <summary>Print the content directly without showing any dialog</summary>
    DirectPrint, 

    /// <summary>Show print dialog before to do printing</summary>
    ShowPrintDialog,

    /// <summary>Show print dialog before to do printing and calculate total number of pages before show dialog</summary>
    ShowPrintDialogWithTotalPages,

    /// <summary>Show a print preview</summary>
    PrintPreview 
};

public delegate void BeforePagePrintDelegate(int posIniChar, int posEndChar, PrintPageEventArgs e);

public static class RichTextBoxExtension
{
    /// <summary>
    /// Print the content of the RichTextBox
    /// </summary>
    /// <param name="printType">Printing type to define how to perform the printing process</param>
    public static void Print(this RichTextBox richTextBox, PrintType printType)
    {
        new RichTextBoxHelper(richTextBox).Print(printType, null, null);
    }

    /// <summary>
    /// Print the content of the RichTextBox
    /// </summary>
    /// <param name="printType">Printing type to define how to perform the printing process</param>
    /// <param name="margins">Page margins or null value for use default margins</param>
    public static void Print(this RichTextBox richTextBox, PrintType printType, Margins margins)
    {
        new RichTextBoxHelper(richTextBox).Print(printType, margins, null);
    }

    /// <summary>
    /// Print the content of the RichTextBox
    /// </summary>
    /// <param name="printType">Printing type to define how to perform the printing process</param>
    /// <param name="printType">Delegate invoked before print each page</param>
    public static void Print(this RichTextBox richTextBox, PrintType printType, BeforePagePrintDelegate beforePagePrintDelegate)
    {
        new RichTextBoxHelper(richTextBox).Print(printType, null, beforePagePrintDelegate);
    }

    /// <summary>
    /// Print the content of the RichTextBox
    /// </summary>
    /// <param name="printType">Printing type to define how to perform the printing process</param>
    /// <param name="margins">Page margins or null value for use default margins</param>
    /// <param name="printType">Delegate invoked before print each page</param>
    public static void Print(this RichTextBox richTextBox, PrintType printType, Margins margins, BeforePagePrintDelegate beforePagePrintDelegate)
    {
        new RichTextBoxHelper(richTextBox).Print(printType, margins, beforePagePrintDelegate);
    }

    /// <summary>
    /// Sets the font name for the selected text of the RichTextBox
    /// </summary>
    /// <param name="fontName">Name of the font to use</param>
    /// <returns>Returns true on success, false on failure</returns>
    public static bool SelectionFontName(this RichTextBox richTextBox, string fontName)
    {
        return RichTextBoxHelper.SelectionFontName(richTextBox, fontName);
    }

    /// <summary>
    /// Sets the font size for the selected text of the RichTextBox
    /// </summary>
    /// <param name="fontSize">Font size to use</param>
    /// <returns>Returns true on success, false on failure</returns>
    public static bool SelectionFontSize(this RichTextBox richTextBox, int fontSize)
    {
        return RichTextBoxHelper.SelectionFontSize(richTextBox, fontSize);
    }

    /// <summary>
    /// Sets the font style for the selected text of the RichTextBox
    /// </summary>
    /// <param name="fontStyle">Font style to apply to selected text</param>
    /// <returns>Returns true on success, false on failure</returns>
    public static bool SelectionFontStyle(this RichTextBox richTextBox, FontStyle fontStyle)
    {
        return RichTextBoxHelper.SelectionFontStyle(richTextBox, fontStyle);
    }

    /// <summary>
    /// Sets the font color for the selected text of the RichTextBox
    /// </summary>
    /// <param name="color">Color to apply</param>
    /// <returns>Returns true on success, false on failure</returns>
    public static bool SelectionFontColor(this RichTextBox richTextBox, Color color)
    {
        return RichTextBoxHelper.SelectionFontColor(richTextBox, color);
    }

    /// <summary>
    /// Sets the font color for the word in the selected point
    /// </summary>
    /// <param name="color">Color to apply</param>
    /// <returns>Returns true on success, false on failure</returns>
    public static bool WordFontColor(this RichTextBox richTextBox, Color color)
    {
        return RichTextBoxHelper.WordFontColor(richTextBox, color);
    }

    /// <summary>
    /// Sets the background color for the selected text of the RichTextBox
    /// </summary>
    /// <param name="color">Color to apply</param>
    /// <returns>Returns true on success, false on failure</returns>
    public static bool SelectionBackColor(this RichTextBox richTextBox, Color color)
    {
        return RichTextBoxHelper.SelectionBackColor(richTextBox, color);
    }

    public static void SetRedraw(this RichTextBox richTextBox, bool enableRedraw)
    {
        RichTextBoxHelper.SetRedraw(richTextBox, enableRedraw);
    }

    public static int GetFirstVisibleCharIndex(this RichTextBox richTextBox)
    {
        return RichTextBoxHelper.GetFirstVisibleCharIndex(richTextBox);
    }

    public static int GetLastVisibleCharIndex(this RichTextBox richTextBox)
    {
        return RichTextBoxHelper.GetLastVisibleCharIndex(richTextBox);
    }

    public static int GetFirstVisibleLine(this RichTextBox richTextBox)
    {
        return RichTextBoxHelper.GetFirstVisibleLine(richTextBox);
    }

    public static int GetVisibleLines(this RichTextBox richTextBox)
    {
        return RichTextBoxHelper.GetVisibleLines(richTextBox);
    }

    public static void HideSelection(this RichTextBox richTextBox, bool hide)
    {
        RichTextBoxHelper.HideSelection(richTextBox, hide);
    }

    public static void ScrollLines(this RichTextBox richTextBox, int linesToScroll)
    {
        RichTextBoxHelper.ScrollLines(richTextBox, linesToScroll);
    }

    public static int GetHScroll(this RichTextBox richTextBox)
    {
        return RichTextBoxHelper.GetHScroll(richTextBox);
    }

    public static void SetHScroll(this RichTextBox richTextBox, int position)
    {
        RichTextBoxHelper.SetHScroll(richTextBox, position);
    }

    public static int GetEventMask(this RichTextBox richTextBox)
    {
        return RichTextBoxHelper.GetEventMask(richTextBox);
    }

    public static void SetEventMask(this RichTextBox richTextBox, int mask)
    {
        RichTextBoxHelper.SetEventMask(richTextBox, mask);
    }
}

/// <summary>
/// Helper to provide extended methods for the RichTextBox control
/// </summary>
public class RichTextBoxHelper
{
    private RichTextBox richTextBox;
    private BeforePagePrintDelegate beforePagePrintDelegate;
    private int posIniChar;

    /// <summary>
    /// Get the total number of pages needed to print the document
    /// </summary>
    public int TotalPages { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether only calculation will be performed or whether the text will rendered also
    /// </summary>
    private bool MeasureOnly { get; set; }

    public RichTextBoxHelper(RichTextBox richTextBox)
    {
        this.richTextBox = richTextBox;
    }

    public void Print(PrintType printType, Margins margins, BeforePagePrintDelegate beforePagePrintDelegate)
    {
        var doc = new PrintDocument();
        doc.BeginPrint += new PrintEventHandler(this.Document_BeginPrint);
        doc.EndPrint += new PrintEventHandler(this.Document_EndPrint);
        doc.PrintPage += new PrintPageEventHandler(this.Document_PrintPage);
        doc.DefaultPageSettings.Margins = margins == null ? new Margins(20, 20, 20, 20) : margins;
        doc.DocumentName = "CompactView document";
        this.beforePagePrintDelegate = beforePagePrintDelegate;

        try
        {
            switch (printType)
            {
                case PrintType.DirectPrint:
                    MeasureOnly = false;
                    doc.Print();
                    break;
                case PrintType.ShowPrintDialog:
                case PrintType.ShowPrintDialogWithTotalPages:
                    if (printType == PrintType.ShowPrintDialogWithTotalPages)
                    {
                        MeasureOnly = true;
                        PrintController controller = doc.PrintController;
                        doc.PrintController = new PreviewPrintController();
                        doc.Print();   // Only for calculate total pages
                        doc.PrintController = controller;
                    }
                    else
                    {
                        TotalPages = int.MaxValue;
                    }

                    MeasureOnly = false;
                    var printDlg = new PrintDialog();
                    printDlg.Document = doc;
                    printDlg.AllowSomePages = true;
                    printDlg.PrinterSettings.FromPage = 1;
                    printDlg.PrinterSettings.MinimumPage = 1;
                    printDlg.PrinterSettings.ToPage = TotalPages;
                    printDlg.PrinterSettings.MaximumPage = TotalPages;
                    if (printDlg.ShowDialog() == DialogResult.OK) doc.Print();
                    break;
                case PrintType.PrintPreview:
                    MeasureOnly = false;
                    var previewDlg = new PrintPreviewDialog();
                    previewDlg.WindowState = FormWindowState.Maximized;
                    previewDlg.Document = doc;
                    previewDlg.ShowIcon = false;
                    previewDlg.ShowDialog();
                    break;
            }
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex.Message);
        }
    }

    private void Document_BeginPrint(object sender, PrintEventArgs e)
    {
        posIniChar = 0;   // Start at the beginning of the text
        TotalPages = 0;   // Initialize pages counter
    }

    private void Document_PrintPage(object sender, PrintPageEventArgs e)
    {
        // Print RichTextBox content starting at the indicated character. Store the last character printed.
        posIniChar = DoPrint(posIniChar, richTextBox.TextLength, e);

        // Check for more pages
        e.HasMorePages = posIniChar < richTextBox.TextLength;

        TotalPages++;
    }

    private void Document_EndPrint(object sender, PrintEventArgs e)
    {
        // Clean up cached data
        PrintDone();
    }
    
    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public Int32 left;
        public Int32 top;
        public Int32 right;
        public Int32 bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct CHARRANGE
    {
        public Int32 cpMin;
        public Int32 cpMax;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct FORMATRANGE
    {
        public IntPtr hdc;
        public IntPtr hdcTarget;
        public RECT rc;
        public RECT rcPage;
        public CHARRANGE chrg;

        public static FORMATRANGE Set(PrintPageEventArgs e, int charIndexIni, int charIndexEnd)
        {
            FORMATRANGE fr;

            // Specify device context of output device
            fr.hdc = e.Graphics.GetHdc();

            // Use the same DC for measuring and rendering
            fr.hdcTarget = fr.hdc;

            // Specify the area inside page margins to render and print
            fr.rc.top = HundredthInchToTwips(e.MarginBounds.Top);
            fr.rc.bottom = HundredthInchToTwips(e.MarginBounds.Bottom);
            fr.rc.left = HundredthInchToTwips(e.MarginBounds.Left);
            fr.rc.right = HundredthInchToTwips(e.MarginBounds.Right);

            // Specify the page area
            fr.rcPage.top = HundredthInchToTwips(e.PageBounds.Top);
            fr.rcPage.bottom = HundredthInchToTwips(e.PageBounds.Bottom);
            fr.rcPage.left = HundredthInchToTwips(e.PageBounds.Left);
            fr.rcPage.right = HundredthInchToTwips(e.PageBounds.Right);

            // Specify characters to print
            fr.chrg.cpMin = charIndexIni;
            fr.chrg.cpMax = charIndexEnd;

            return fr;
        }
    }

    [DllImport("user32.dll")]
    private static extern Int32 SendMessage(IntPtr hWnd, Int32 msg, Int32 wParam, IntPtr lParam);

    [DllImport("user32.dll")]
    private extern static Int32 SendMessage(IntPtr hWnd, Int32 msg, Int32 wParam, ref Point lParam);

    [DllImport("user32.dll", SetLastError = true)]
    static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int GetScrollPos(IntPtr hWnd, Orientation nBar);

    [DllImport("user32.dll")]
    public static extern int SetScrollPos(IntPtr hWnd, Orientation nBar, int nPos, bool bRedraw);
    
    // Windows Messages defines
    private const Int32 WM_USER = 0x400;
    private const Int32 EM_FORMATRANGE = WM_USER + 57;

    /// <summary>
    /// Calculate or render the contents of our RichTextBox for printing
    /// </summary>
    /// <param name="e">The PrintPageEventArgs object from the PrintPage event</param>
    /// <param name="posIniChar">Index of first character to be printed</param>
    /// <param name="posEndChar">Index of last character to be printed</param>
    /// <returns>Index of last character that fitted on the page, plus 1</returns>
    public int DoPrint(int posIniChar, int posEndChar, PrintPageEventArgs e)
    {
        // Fill in the FORMATRANGE struct
        var fr = FORMATRANGE.Set(e, posIniChar, posEndChar);

        // Allocate memory for the FORMATRANGE struct and copy this to the memory
        IntPtr lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fr));
        Marshal.StructureToPtr(fr, lParam, false);

        // Zero wParam means measure, non-zero wParam means render
        Int32 wParam = (MeasureOnly ? 0 : 1);

        // Measure to get end char index needed for call delegate before print
        if (beforePagePrintDelegate != null && !MeasureOnly)
        {
            int nextIndex = SendMessage(richTextBox.Handle, EM_FORMATRANGE, 0, lParam);
            beforePagePrintDelegate(posIniChar, nextIndex - 1, e);
        }

        // Send the Win32 message for printing
        int res = SendMessage(richTextBox.Handle, EM_FORMATRANGE, wParam, lParam);

        // Free allocated memory
        Marshal.FreeCoTaskMem(lParam);

        // Release the device context handle obtained by a previous call
        e.Graphics.ReleaseHdc(fr.hdc);

        return res;
    }

    /// <summary>
    /// Convert between 1/100 inch (unit used by the .NET framework) and twips (1/1440 inch, used by Win32 API calls)
    /// </summary>
    /// <param name="n">Value in 1/100 inch</param>
    /// <returns>Value in twips (1/1440 inch)</returns>
    private static Int32 HundredthInchToTwips(int n)
    {
        return (Int32)(n * 14.4);
    }

    /// <summary>
    /// Free cached data after printing
    /// </summary>
    public void PrintDone()
    {
        var lParam = new IntPtr(0);
        SendMessage(richTextBox.Handle, EM_FORMATRANGE, 0, lParam);
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
    private struct CHARFORMAT2
    {
        public int cbSize;
        public UInt32 dwMask;
        public UInt32 dwEffects;
        public Int32 yHeight;
        public Int32 yOffset;
        public Int32 crTextColor;
        public byte bCharSet;
        public byte bPitchAndFamily;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szFaceName;
        public short wWeight;
        public short sSpacing;
        public int crBackColor;
        public int lcid;
        public int dwReserved;
        public short sStyle;
        public short wKerning;
        public byte bUnderlineType;
        public byte bAnimation;
        public byte bRevAuthor;
        public byte bReserved1;
    }

    // Windows Messages defines
    private const Int32 EM_SETCHARFORMAT = WM_USER + 68;

    // Constants for EM_SETCHARFORMAT/EM_GETCHARFORMAT
    private const Int32 SCF_SELECTION = 0x0001;
    private const Int32 SCF_WORD = 0x0002;

    // Constants for CHARFORMAT2 member dwMask
    private const UInt32 CFM_BOLD = 0x00000001;
    private const UInt32 CFM_ITALIC = 0x00000002;
    private const UInt32 CFM_UNDERLINE = 0x00000004;
    private const UInt32 CFM_STRIKEOUT = 0x00000008;
    private const UInt32 CFM_FACE = 0x20000000;
    private const UInt32 CFM_SIZE = 0x80000000;
    private const UInt32 CFM_COLOR = 0x40000000;
    private const UInt32 CFM_BACKCOLOR = 0x4000000;

    // Constants for CHARFORMAT2 member dwEffects
    private const UInt32 CFE_BOLD = 0x00000001;
    private const UInt32 CFE_ITALIC = 0x00000002;
    private const UInt32 CFE_UNDERLINE = 0x00000004;
    private const UInt32 CFE_STRIKEOUT = 0x00000008;

    /// <summary>
    /// Sets the font name for the selected text of the RichTextBox
    /// </summary>
    /// <param name="richTextBox">RichTextBox control</param>
    /// <param name="fontName">Name of the font to use</param>
    /// <returns>Returns true on success, false on failure</returns>
    public static bool SelectionFontName(RichTextBox richTextBox, string fontName)
    {
        var cf = new CHARFORMAT2();
        cf.cbSize = Marshal.SizeOf(cf);
        cf.dwMask = CFM_FACE;
        cf.szFaceName = fontName.Length > 32 ? fontName.Remove(32) : fontName;

        IntPtr lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf));
        Marshal.StructureToPtr(cf, lParam, false);

        int res = SendMessage(richTextBox.Handle, EM_SETCHARFORMAT, SCF_SELECTION, lParam);
        return (res == 0);
    }

    /// <summary>
    /// Sets the font size for the selected text of the RichTextBox
    /// </summary>
    /// <param name="richTextBox">RichTextBox control</param>
    /// <param name="fontSize">Font size to use</param>
    /// <returns>Returns true on success, false on failure</returns>
    public static bool SelectionFontSize(RichTextBox richTextBox, int fontSize)
    {
        var cf = new CHARFORMAT2();
        cf.cbSize = Marshal.SizeOf(cf);
        cf.dwMask = CFM_SIZE;
        cf.yHeight = fontSize * 20;   // yHeight is in 1/20 pt

        IntPtr lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf));
        Marshal.StructureToPtr(cf, lParam, false);

        int res = SendMessage(richTextBox.Handle, EM_SETCHARFORMAT, SCF_SELECTION, lParam);
        return (res == 0);
    }

    /// <summary>
    /// Sets the font style for the selected text of the RichTextBox
    /// </summary>
    /// <param name="richTextBox">RichTextBox control</param>
    /// <param name="fontStyle">Font style to apply to selected text</param>
    /// <returns>Returns true on success, false on failure</returns>
    public static bool SelectionFontStyle(RichTextBox richTextBox, FontStyle fontStyle)
    {
        uint mask = CFM_BOLD | CFM_ITALIC | CFM_UNDERLINE | CFM_STRIKEOUT;

        uint effect = 0;
        if ((fontStyle & FontStyle.Bold) != 0) effect |= CFE_BOLD;
        if ((fontStyle & FontStyle.Italic) != 0) effect |= CFE_ITALIC;
        if ((fontStyle & FontStyle.Underline) != 0) effect |= CFE_UNDERLINE;
        if ((fontStyle & FontStyle.Strikeout) != 0) effect |= CFE_STRIKEOUT;

        return SetSelectionStyle(richTextBox, Color.Empty, Color.Empty, mask, effect);
    }

    /// <summary>
    /// Sets the font color for the selected text of the RichTextBox
    /// </summary>
    /// <param name="richTextBox">RichTextBox control</param>
    /// <param name="color">Color to apply</param>
    /// <returns>Returns true on success, false on failure</returns>
    public static bool SelectionFontColor(RichTextBox richTextBox, Color color)
    {
        return SetSelectionStyle(richTextBox, color, Color.Empty, CFM_COLOR, 0);
    }

    /// <summary>
    /// Sets the font color for the word in the selected point of the RichTextBox
    /// </summary>
    /// <param name="richTextBox">RichTextBox control</param>
    /// <param name="color">Color to apply</param>
    /// <returns>Returns true on success, false on failure</returns>
    public static bool WordFontColor(RichTextBox richTextBox, Color color)
    {
        var cf = new CHARFORMAT2();
        cf.cbSize = Marshal.SizeOf(cf);
        cf.crTextColor = ColorTranslator.ToWin32(color);
        cf.crBackColor = ColorTranslator.ToWin32(Color.Empty);
        cf.dwMask = CFM_COLOR;
        cf.dwEffects = 0;

        IntPtr lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf));
        Marshal.StructureToPtr(cf, lParam, false);

        int res = SendMessage(richTextBox.Handle, EM_SETCHARFORMAT, SCF_SELECTION | SCF_WORD, lParam);
        return (res == 0);
    }

    /// <summary>
    /// Sets the background color for the selected text of the RichTextBox
    /// </summary>
    /// <param name="richTextBox">RichTextBox control</param>
    /// <param name="color">Color to apply</param>
    /// <returns>Returns true on success, false on failure</returns>
    public static bool SelectionBackColor(RichTextBox richTextBox, Color color)
    {
        return SetSelectionStyle(richTextBox, Color.Empty, color, CFM_BACKCOLOR, 0);
    }

    /// <summary>
    /// Sets the font style for the selected text of the RichTextBox
    /// </summary>
    /// <param name="richTextBox">RichTextBox control</param>
    /// <param name="mask">Mask to determine which styles will be modified</param>
    /// <param name="effect">New values for the styles</param>
    /// <returns>Returns true on success, false on failure</returns>
    private static bool SetSelectionStyle(RichTextBox richTextBox, Color fontColor, Color backColor, UInt32 mask, UInt32 effect)
    {
        var cf = new CHARFORMAT2();
        cf.cbSize = Marshal.SizeOf(cf);
        cf.crTextColor = ColorTranslator.ToWin32(fontColor);
        cf.crBackColor = ColorTranslator.ToWin32(backColor);
        cf.dwMask = mask;
        cf.dwEffects = effect;

        IntPtr lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf));
        Marshal.StructureToPtr(cf, lParam, false);

        int res = SendMessage(richTextBox.Handle, EM_SETCHARFORMAT, SCF_SELECTION, lParam);
        return (res == 0);
    }

    public static void SetRedraw(RichTextBox richTextBox, bool enableRedraw)
    {
        const int WM_SETREDRAW = 0x000B;
        SendMessage(richTextBox.Handle, WM_SETREDRAW, enableRedraw ? 1 : 0, IntPtr.Zero);
    }

    public static int GetFirstVisibleCharIndex(RichTextBox richTextBox)
    {
        const int EM_CHARFROMPOS = 0x00D7;
        var point = new Point(1, 1);
        return SendMessage(richTextBox.Handle, EM_CHARFROMPOS, 0, ref point);
    }

    public static int GetLastVisibleCharIndex(RichTextBox richTextBox)
    {
        const int EM_CHARFROMPOS = 0x00D7;
        var point = new Point(richTextBox.ClientRectangle.Right - 1, richTextBox.ClientRectangle.Bottom - 1);
        return SendMessage(richTextBox.Handle, EM_CHARFROMPOS, 0, ref point);
    }

    public static int GetFirstVisibleLine(RichTextBox richTextBox)
    {
        const int EM_GETFIRSTVISIBLELINE = 0x00CE;
        return SendMessage(richTextBox.Handle, EM_GETFIRSTVISIBLELINE, 0, IntPtr.Zero);
    }

    public static int GetVisibleLines(RichTextBox richTextBox)
    {
        Size size = TextRenderer.MeasureText("H", richTextBox.Font);
        return (int)Math.Ceiling((double)richTextBox.ClientRectangle.Height / size.Height);
    }

    public static void HideSelection(RichTextBox richTextBox, bool hide)
    {
        const int WM_USER = 0x400;
        const int EM_HIDESELECTION = WM_USER + 63;
        SendMessage(richTextBox.Handle, EM_HIDESELECTION, hide ? 1 : 0, IntPtr.Zero);
    }

    public static void ScrollLines(RichTextBox richTextBox, int linesToScroll)
    {
        const int EM_LINESCROLL = 0x00B6;
        SendMessage(richTextBox.Handle, EM_LINESCROLL, 0, (IntPtr)linesToScroll);
    }

    public static int GetHScroll(RichTextBox richTextBox)
    {
        return GetScrollPos(richTextBox.Handle, Orientation.Horizontal);
    }

    public static void SetHScroll(RichTextBox richTextBox, int position)
    {
        const int SB_THUMBPOSITION = 4;
        const int GWL_STYLE = (-16);
        const UInt32 WS_HSCROLL = 0x100000;
        const int WM_HSCROLL = 0x0114;

        int style = GetWindowLong(richTextBox.Handle, GWL_STYLE);
        bool hScroll = (style & WS_HSCROLL) != 0;

        if (hScroll)
        {
            SetScrollPos(richTextBox.Handle, Orientation.Horizontal, position, true);
            SendMessage(richTextBox.Handle, WM_HSCROLL, SB_THUMBPOSITION + 0x10000 * position, IntPtr.Zero);
        }
    }

    public static int GetEventMask(RichTextBox richTextBox)
    {
        const int EM_GETEVENTMASK = WM_USER + 59;
        return SendMessage(richTextBox.Handle, EM_GETEVENTMASK, 0, IntPtr.Zero);
    }

    public static void SetEventMask(RichTextBox richTextBox, int mask)
    {
        const int EM_SETEVENTMASK = WM_USER + 69;
        SendMessage(richTextBox.Handle, EM_SETEVENTMASK, 0, (IntPtr)mask);
    }
}
