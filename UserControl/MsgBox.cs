using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DaMEF.UserControl
{
    public class MsgBox : Form
    {
        public static MsgBox _msgBox;
        private Panel _plHeader = new Panel();
        private Panel _plCenter = new Panel();
        private Panel _plFooter = new Panel();
        private FlowLayoutPanel _flpButtons = new FlowLayoutPanel();
        private List<ChromeButton> _buttonCollection = new List<ChromeButton>();
        private static DialogResult _buttonResult = new DialogResult();
        private Label _lblTitle;
        private Label _lblMessage;
        private PictureBox picClose = new System.Windows.Forms.PictureBox();

        protected Point mousePoint;

        private MsgBox()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.BackgroundImage = Properties.Resources.back21;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Width = 350;

            picClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            picClose.BackgroundImage = Properties.Resources.btn_close;
            picClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            picClose.Name = "picClose";
            picClose.Dock = DockStyle.Right;
            picClose.Size = new System.Drawing.Size(19, 19);
            picClose.Click += new System.EventHandler(this.picClose_Click);

            _lblTitle = new Label();
            _lblTitle.ForeColor = Color.White;
            _lblTitle.Font = new System.Drawing.Font("맑은 고딕", 9);
            _lblTitle.Dock = DockStyle.Fill;
            _lblTitle.BackColor = Color.Transparent;
            _lblTitle.Height = 25;
            _lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            _lblTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this._lblTitle_MouseDown);
            _lblTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this._lblTitle_MouseMove);

            _lblMessage = new Label();
            _lblMessage.ForeColor = Color.Black;
            _lblMessage.Font = new System.Drawing.Font("맑은 고딕", 9);
            _lblMessage.Dock = DockStyle.Fill;
            _lblMessage.BackColor = Color.Transparent;
            _lblMessage.Height = 50;
            _lblMessage.Text = "";
            _lblMessage.TextAlign = ContentAlignment.MiddleCenter;

            _flpButtons.FlowDirection = FlowDirection.RightToLeft;
            _flpButtons.Dock = DockStyle.Fill;

            _plHeader.Dock = DockStyle.Top;
            _plHeader.Padding = new Padding(3);
            _plHeader.BackColor = Color.Transparent;
            //_plHeader.Controls.Add(_lblMessage);
            _plHeader.Height = 25;
            _plHeader.Controls.Add(_lblTitle);
            _plHeader.Controls.Add(picClose);

            _plFooter.Dock = DockStyle.Bottom;
            //_plFooter.Padding = new Padding(5);
            _plFooter.BackColor = Color.Transparent;
            _plFooter.Height = 40;
            _plFooter.Controls.Add(_flpButtons);

            _plCenter.Dock = DockStyle.Fill;
            _plCenter.Padding = new Padding(2);
            _plCenter.BackColor = Color.White;
            //_plCenter.Height = 60;
            _plCenter.Controls.Add(_lblMessage);
            _plCenter.Controls.Add(_plFooter);


            this.Controls.Add(_plCenter);
            //this.Controls.Add(_plFooter);
            this.Controls.Add(_plHeader);

            _buttonResult = DialogResult.None;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            _msgBox.Close();
        }

        public static void Show(string message)
        {
            _msgBox = new MsgBox();
            _msgBox._lblMessage.Text = message;
            _msgBox.Size = MsgBox.MessageSize(message, false);
            _msgBox.ShowDialog();
        }

        public static void Show(string message, string title)
        {
            _msgBox = new MsgBox();
            _msgBox._lblMessage.Text = message;
            _msgBox._lblTitle.Text = title;
            _msgBox.Size = MsgBox.MessageSize(message, false);
            _msgBox._plFooter.Size = new Size(_msgBox.Width, 0);
            _msgBox.ShowDialog();
        }

        public static DialogResult Show(string message, string title, Buttons buttons)
        {
            _msgBox = new MsgBox();
            _msgBox._lblMessage.Text = message;
            _msgBox._lblTitle.Text = title;
            _msgBox.Owner = _msgBox.ParentForm;

            MsgBox.InitButtons(buttons);
            //MsgBox.InitIcon(icon);

            _msgBox.Size = MsgBox.MessageSize(message, true);
            _msgBox.ShowDialog();

            //MessageBeep(0);
            return _buttonResult;

        }

        public static DialogResult Show(string message, string title, string[] prog)
        {
            _msgBox = new MsgBox();
            _msgBox._lblMessage.Text = message;
            _msgBox._lblTitle.Text = title;
            _msgBox.Owner = _msgBox.ParentForm;

            MsgBox.InitButtons(prog);
            //MsgBox.InitIcon(icon);

            _msgBox.Size = MsgBox.MessageSize(message, prog);
            _msgBox.ShowDialog();

            //MessageBeep(0);
            return _buttonResult;

        }



        public void _lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            mousePoint = new Point(e.X, e.Y);
        }

        public void _lblTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                Location = new Point(this.Left - (mousePoint.X - e.X),
                    this.Top - (mousePoint.Y - e.Y));
            }
        }


        private static void InitButtons(Buttons buttons)
        {
            switch (buttons)
            {
                case MsgBox.Buttons.AbortRetryIgnore:
                    _msgBox.InitAbortRetryIgnoreButtons();
                    break;

                case MsgBox.Buttons.OK:
                    _msgBox.InitOKButton();
                    break;

                case MsgBox.Buttons.OKCancel:
                    _msgBox.InitOKCancelButtons();
                    break;

                case MsgBox.Buttons.RetryCancel:
                    _msgBox.InitRetryCancelButtons();
                    break;

                case MsgBox.Buttons.YesNo:
                    _msgBox.InitYesNoButtons();
                    break;

                case MsgBox.Buttons.YesNoCancel:
                    _msgBox.InitYesNoCancelButtons();
                    break;
                case MsgBox.Buttons.Certify:
                    _msgBox.InitCertifyButtons();
                    break;
            }

            foreach (ChromeButton btn in _msgBox._buttonCollection)
            {
                if (btn.Text == "인증요청")
                {
                    btn.Height = 28;
                    btn.Width = 70;
                }
                else
                {
                    btn.Height = 28;
                    btn.Width = 50;
                }

                _msgBox._flpButtons.Controls.Add(btn);
            }
        }

        private static void InitButtons(string[] progs)
        {
            _msgBox.InitProgSelButtons(progs);

            foreach (ChromeButton btn in _msgBox._buttonCollection)
            {
                btn.Height = 28;
                btn.Width = 100;

                _msgBox._flpButtons.Controls.Add(btn);
            }

        }

        private void InitAbortRetryIgnoreButtons()
        {
            ChromeButton btnAbort = new ChromeButton();
            btnAbort.Text = "Abort";
            btnAbort.Click += ButtonClick;

            ChromeButton btnRetry = new ChromeButton();
            btnRetry.Text = "Retry";
            btnRetry.Click += ButtonClick;

            ChromeButton btnIgnore = new ChromeButton();
            btnIgnore.Text = "Ignore";
            btnIgnore.Click += ButtonClick;

            this._buttonCollection.Add(btnAbort);
            this._buttonCollection.Add(btnRetry);
            this._buttonCollection.Add(btnIgnore);
        }

        private void InitOKButton()
        {
            ChromeButton btnOK = new ChromeButton();
            btnOK.Text = "확인";
            btnOK.Click += new System.EventHandler(ButtonClick);

            this._buttonCollection.Add(btnOK);
        }

        private void InitOKCancelButtons()
        {
            ChromeButton btnOK = new ChromeButton();
            btnOK.Text = "확인";
            btnOK.Click += ButtonClick;

            ChromeButton btnCancel = new ChromeButton();
            btnCancel.Text = "취소";
            btnCancel.Click += ButtonClick;


            this._buttonCollection.Add(btnOK);
            this._buttonCollection.Add(btnCancel);
        }

        private void InitRetryCancelButtons()
        {
            ChromeButton btnRetry = new ChromeButton();
            btnRetry.Text = "확인";
            btnRetry.Click += ButtonClick;

            ChromeButton btnCancel = new ChromeButton();
            btnCancel.Text = "취소";
            btnCancel.Click += ButtonClick;

            this._buttonCollection.Add(btnCancel);
            this._buttonCollection.Add(btnRetry);
        }

        private void InitYesNoButtons()
        {
            ChromeButton btnYes = new ChromeButton();
            btnYes.Text = "예";
            btnYes.Click += new System.EventHandler(ButtonClick);

            ChromeButton btnNo = new ChromeButton();
            btnNo.Text = "아니오";
            btnNo.Click += new System.EventHandler(ButtonClick);

            this._buttonCollection.Add(btnNo);
            this._buttonCollection.Add(btnYes);
        }

        private void InitYesNoCancelButtons()
        {
            ChromeButton btnYes = new ChromeButton();
            btnYes.Text = "예";
            btnYes.Click += ButtonClick;

            ChromeButton btnNo = new ChromeButton();
            btnNo.Text = "아니오";
            btnNo.Click += ButtonClick;

            ChromeButton btnCancel = new ChromeButton();
            btnCancel.Text = "취소";
            btnCancel.Click += ButtonClick;

            this._buttonCollection.Add(btnCancel);
            this._buttonCollection.Add(btnNo);
            this._buttonCollection.Add(btnYes);
        }

        private void InitCertifyButtons()
        {
            ChromeButton btnOK = new ChromeButton();
            btnOK.Text = "OK";
            btnOK.Click += ButtonClick;

            ChromeButton btnCertify = new ChromeButton();
            btnCertify.Text = "인증요청";
            btnCertify.Click += ButtonClick;

            this._buttonCollection.Add(btnOK);
            this._buttonCollection.Add(btnCertify);
        }

        private void InitProgSelButtons(string[] Programs)
        {
            foreach (string program in Programs)
            {
                ChromeButton btnProg = new ChromeButton();
                btnProg.Text = program;
                btnProg.Click += ButtonClick;

                this._buttonCollection.Add(btnProg);
            }
        }


        private static void ButtonClick(object sender, EventArgs e)
        {
            ChromeButton btn = (ChromeButton)sender;

            switch (btn.Text)
            {
                case "Abort":
                    _buttonResult = DialogResult.Abort;
                    break;

                case "Retry":
                    _buttonResult = DialogResult.Retry;
                    break;

                case "Ignore":
                    _buttonResult = DialogResult.Ignore;
                    break;

                case "확인":
                    _buttonResult = DialogResult.OK;
                    break;

                case "취소":
                    _buttonResult = DialogResult.Cancel;
                    break;

                case "예":
                    _buttonResult = DialogResult.Yes;
                    break;

                case "아니오":
                    _buttonResult = DialogResult.No;
                    break;

                case "인증요청":
                    _buttonResult = DialogResult.Retry;
                    break;

                case "OK":
                    _buttonResult = DialogResult.OK;
                    break;

                case "SMART A(구)":
                    _buttonResult = DialogResult.Yes;
                    break;

                case "SMART A(신)":
                    _buttonResult = DialogResult.No;
                    break;

                case "세무사랑2":
                    _buttonResult = DialogResult.Retry;
                    break;
            }

            //_msgBox.Dispose();
            _msgBox.Close();
        }

        private static Size MessageSize(string message, bool IsButton)
        {
            Graphics g = _msgBox.CreateGraphics();
            int width = 200;
            int height = 130;

            if (IsButton == false)
            {
                height = height - _msgBox._plFooter.Height;
                _msgBox._plFooter.Size = new Size(_msgBox.Width, 0);
            }

            SizeF size = g.MeasureString(message, new System.Drawing.Font("맑은 고딕", 9));

            if (message.Length < 150)
            {
                //if ((int)size.Width < 150)
                //{
                width = (int)size.Width + 50;
                height = (int)size.Height > 60 ? (int)size.Height + 70 : 130;
                //}
            }
            else
            {
                string[] groups = (from Match m in Regex.Matches(message, ".{1,180}") select m.Value).ToArray();
                int lines = groups.Length + 1;
                width = 700;
                height += (int)(size.Height + 10) * lines;
            }
            return new Size(width, height);
        }

        private static Size MessageSize(string message)
        {
            Graphics g = _msgBox.CreateGraphics();
            int width = 200;
            int height = 130;

            SizeF size = g.MeasureString(message, new System.Drawing.Font("맑은 고딕", 9));

            if (message.Length < 150)
            {
                if ((int)size.Width < 150)
                {
                    width = (int)size.Width + 50;
                }
            }
            else
            {
                string[] groups = (from Match m in Regex.Matches(message, ".{1,180}") select m.Value).ToArray();
                int lines = groups.Length + 1;
                width = 700;
                height += (int)(size.Height + 10) * lines;
            }
            return new Size(width, height);
        }

        private static Size MessageSize(string message, string[] prog)
        {
            Graphics g = _msgBox.CreateGraphics();
            int width = 200;
            int height = 130;

            SizeF size = g.MeasureString(message, new System.Drawing.Font("맑은 고딕", 9));

            if (message.Length < 150)
            {
                width = prog.Length * 100 > (int)size.Width ? prog.Length * 100 + 50 : (int)size.Width + 50;
            }
            else
            {
                string[] groups = (from Match m in Regex.Matches(message, ".{1,180}") select m.Value).ToArray();
                int lines = groups.Length + 1;
                width = 700;
                height += (int)(size.Height + 10) * lines;
            }
            return new Size(width, height);
        }


        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MsgBox));
            this.SuspendLayout();
            // 
            // MsgBox
            // 
            this.ClientSize = new System.Drawing.Size(284, 174);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MsgBox";
            this.Load += new System.EventHandler(this.MsgBox_Load);
            this.ResumeLayout(false);

        }

        public enum Buttons
        {
            AbortRetryIgnore = 1,
            OK = 2,
            OKCancel = 3,
            RetryCancel = 4,
            YesNo = 5,
            YesNoCancel = 6,
            Certify = 7,
            ProgSel = 8
        }

        private void MsgBox_Load(object sender, EventArgs e)
        {

        }
    }
}
