using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace DaMEF
{
    /*****************************************************************************************
    * 메시지박스 버튼 텍스트 변경
    * DialogResult Show에서 mark는 메시지창 아이콘 종류
    * 현재 버튼 2개짜리만 처리 / 3개가 필요할 경우 SHOW, HOOKWNDPROC 수정 바람
    * 2016.07.18 LEE KUK HEON   
    /*****************************************************************************************/
    public class MSGBOX
    {
        delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetWindowsHookEx(int hook, HookProc callback, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll")]
        static extern int CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        static extern IntPtr GetDlgItem(IntPtr hDlg, DialogResult nIDDlgItem);
        [DllImport("user32.dll")]
        static extern bool SetDlgItemText(IntPtr hDlg, DialogResult nIDDlgItem, string lpString);
        [DllImport("kernel32.dll")]
        static extern uint GetCurrentThreadId();
        static IntPtr g_hHook;
        static string yes;
        static string cancel;
        static string no;

        // 메시지박스 CUSTOM
        public static DialogResult Show(string text, string caption, string yes, string no, string mark = "")
        {
            MSGBOX.yes = yes;
            MSGBOX.no = no;
            g_hHook = SetWindowsHookEx(5, new HookProc(HookWndProc), IntPtr.Zero, GetCurrentThreadId());


            if (mark == "Error")
            {
                return MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
            else if (mark == "Warning")
            {
                return MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
            else
            {
                return MessageBox.Show(text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            }


        }


        static int HookWndProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            IntPtr hChildWnd;

            if (nCode == 5)
            {
                hChildWnd = wParam;
                if (GetDlgItem(hChildWnd, DialogResult.Yes) != null)
                    SetDlgItemText(hChildWnd, DialogResult.Yes, MSGBOX.yes);
                if (GetDlgItem(hChildWnd, DialogResult.No) != null)
                    SetDlgItemText(hChildWnd, DialogResult.No, MSGBOX.no);
                if (GetDlgItem(hChildWnd, DialogResult.Cancel) != null)
                    SetDlgItemText(hChildWnd, DialogResult.Cancel, MSGBOX.cancel);
                UnhookWindowsHookEx(g_hHook);
            }
            else
                CallNextHookEx(g_hHook, nCode, wParam, lParam);
            return 0;

        }


        //입력박스 CUSTOM
        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

    }
}
