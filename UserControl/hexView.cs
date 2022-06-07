using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DaMEF.UserControl
{
    public partial class hexView : System.Windows.Forms.UserControl
    {
        private System.ComponentModel.Design.ByteViewer byteViewer;
        public hexView()
        {
            InitializeComponent();

            byteViewer = new ByteViewer();
            
            byteViewer.Dock = DockStyle.Fill;
            byteViewer.SetBytes(new byte[] { });
            this.Controls.Add(byteViewer);
        }

        public void setHexViewer(string filePath)
        {
            byteViewer.SetFile(filePath);
        }
        private void clearBytes(object sender, EventArgs e)
        {
            byteViewer.SetBytes(new byte[] { });
        }

        public void setHexByte(byte[] data)
        {
            byteViewer.SetBytes(data);
        }

    }
}
