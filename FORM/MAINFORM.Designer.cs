
namespace DaMEF
{
    partial class MAINFORM
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.caseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.storageMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.streamMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tESTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pARSERToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iSOBMFFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nMEAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rIFFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderView = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.listView1 = new System.Windows.Forms.ListView();
            this.fileGirdView = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.hexView1 = new DaMEF.UserControl.hexView();
            this.containerTree = new System.Windows.Forms.TreeView();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileGirdView)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.caseToolStripMenuItem,
            this.toolToolStripMenuItem,
            this.helpToolStripMenuItem,
            this.tESTToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(241, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1160, 30);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // caseToolStripMenuItem
            // 
            this.caseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.caseToolStripMenuItem.Name = "caseToolStripMenuItem";
            this.caseToolStripMenuItem.Size = new System.Drawing.Size(44, 26);
            this.caseToolStripMenuItem.Text = "Case";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.openToolStripMenuItem.Text = "New Case";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolToolStripMenuItem
            // 
            this.toolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.storageMapToolStripMenuItem,
            this.streamMapToolStripMenuItem});
            this.toolToolStripMenuItem.Name = "toolToolStripMenuItem";
            this.toolToolStripMenuItem.Size = new System.Drawing.Size(42, 26);
            this.toolToolStripMenuItem.Text = "Tool";
            // 
            // storageMapToolStripMenuItem
            // 
            this.storageMapToolStripMenuItem.Name = "storageMapToolStripMenuItem";
            this.storageMapToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.storageMapToolStripMenuItem.Text = "Storage Map";
            // 
            // streamMapToolStripMenuItem
            // 
            this.streamMapToolStripMenuItem.Name = "streamMapToolStripMenuItem";
            this.streamMapToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.streamMapToolStripMenuItem.Text = "Stream Map";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 26);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tESTToolStripMenuItem
            // 
            this.tESTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pARSERToolStripMenuItem});
            this.tESTToolStripMenuItem.Name = "tESTToolStripMenuItem";
            this.tESTToolStripMenuItem.Size = new System.Drawing.Size(44, 26);
            this.tESTToolStripMenuItem.Text = "TEST";
            // 
            // pARSERToolStripMenuItem
            // 
            this.pARSERToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iSOBMFFToolStripMenuItem,
            this.nMEAToolStripMenuItem,
            this.rIFFToolStripMenuItem});
            this.pARSERToolStripMenuItem.Name = "pARSERToolStripMenuItem";
            this.pARSERToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.pARSERToolStripMenuItem.Text = "PARSER";
            this.pARSERToolStripMenuItem.Click += new System.EventHandler(this.pARSERToolStripMenuItem_Click);
            // 
            // iSOBMFFToolStripMenuItem
            // 
            this.iSOBMFFToolStripMenuItem.Name = "iSOBMFFToolStripMenuItem";
            this.iSOBMFFToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.iSOBMFFToolStripMenuItem.Text = "ISOBMFF";
            this.iSOBMFFToolStripMenuItem.Click += new System.EventHandler(this.iSOBMFFToolStripMenuItem_Click);
            // 
            // nMEAToolStripMenuItem
            // 
            this.nMEAToolStripMenuItem.Name = "nMEAToolStripMenuItem";
            this.nMEAToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.nMEAToolStripMenuItem.Text = "NMEA";
            this.nMEAToolStripMenuItem.Click += new System.EventHandler(this.nMEAToolStripMenuItem_Click);
            // 
            // rIFFToolStripMenuItem
            // 
            this.rIFFToolStripMenuItem.Name = "rIFFToolStripMenuItem";
            this.rIFFToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.rIFFToolStripMenuItem.Text = "RIFF";
            this.rIFFToolStripMenuItem.Click += new System.EventHandler(this.rIFFToolStripMenuItem_Click);
            // 
            // folderView
            // 
            this.folderView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.folderView.Location = new System.Drawing.Point(3, 33);
            this.folderView.Name = "folderView";
            this.folderView.Size = new System.Drawing.Size(235, 374);
            this.folderView.TabIndex = 1;
            this.folderView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.folderView_BeforeExpand);
            this.folderView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.folderView_NodeMouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(571, 340);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // fileGirdView
            // 
            this.fileGirdView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fileGirdView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileGirdView.Location = new System.Drawing.Point(244, 33);
            this.fileGirdView.Name = "fileGirdView";
            this.fileGirdView.RowTemplate.Height = 23;
            this.fileGirdView.Size = new System.Drawing.Size(1154, 374);
            this.fileGirdView.TabIndex = 2;
            this.fileGirdView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.fileView_CellContentClick);
            this.fileGirdView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.fileView_MouseClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.202F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.798F));
            this.tableLayoutPanel1.Controls.Add(this.fileGirdView, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.folderView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.menuStrip, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.containerTree, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.422402F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92.5776F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 351F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1401, 762);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.hexView1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.listView1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(244, 413);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1154, 346);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // hexView1
            // 
            this.hexView1.AutoSize = true;
            this.hexView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexView1.Location = new System.Drawing.Point(580, 3);
            this.hexView1.Name = "hexView1";
            this.hexView1.Size = new System.Drawing.Size(571, 340);
            this.hexView1.TabIndex = 0;
            // 
            // containerTree
            // 
            this.containerTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.containerTree.Location = new System.Drawing.Point(3, 413);
            this.containerTree.Name = "containerTree";
            this.containerTree.Size = new System.Drawing.Size(235, 346);
            this.containerTree.TabIndex = 3;
            this.containerTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.containerTree_NodeMouseClick);
            // 
            // MAINFORM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1401, 762);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MAINFORM";
            this.Text = "DaMEF";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileGirdView)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem caseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TreeView folderView;
        private System.Windows.Forms.ToolStripMenuItem tESTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pARSERToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem iSOBMFFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nMEAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rIFFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem storageMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem streamMapToolStripMenuItem;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.DataGridView fileGirdView;
        private UserControl.hexView hexView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TreeView containerTree;
    }
}

