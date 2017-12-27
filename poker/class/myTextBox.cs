using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace poker
{
    public class TextBoxLabel : System.Windows.Forms.TextBox
    {
        [DllImport("user32", EntryPoint = "HideCaret")]
        private static extern bool HideCaret(IntPtr hWnd);

        [DllImport("user32", EntryPoint = "ShowCaret")]
        private static extern bool ShowCaret(IntPtr hWnd);

        public TextBoxLabel() : base()
        {

            this.TabStop = false;
            this.SetStyle(ControlStyles.Selectable, false);
            this.Cursor = Cursors.Default;
            this.ReadOnly = true;
            this.ShortcutsEnabled = false;
            this.HideSelection = true;
            this.GotFocus += new EventHandler(TextBoxLabel_GotFocus);
            this.MouseMove += new MouseEventHandler(TextBoxLabel_MouseMove);
        }

        private void TextBoxLabel_GotFocus(Object sender, System.EventArgs e)
        {
            if (ShowCaret(((TextBox)sender).Handle))
            {
                HideCaret(((TextBox)sender).Handle);
            }
        }

        private void TextBoxLabel_MouseMove(Object sender, MouseEventArgs e)
        {
            if (((TextBox)sender).SelectedText.Length > 0)
            {
                ((TextBox)sender).SelectionLength = 0;
            }
        }
    }
}
