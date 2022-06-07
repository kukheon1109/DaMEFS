using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DaMEF.UserControl;


namespace DaMEF
{
    public partial class MAINFORM : Form
    {
        public MAINFORM()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MsgBox.Show("Developed by Kukheon", "DaMEF", MsgBox.Buttons.OK);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Select Evidence Path."})
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    RunData._workPath.evidencePath = fbd.SelectedPath;
                    if (!setFolderList())
                    {
                        MsgBox.Show("wrong path, retry!", "DaMEF", MsgBox.Buttons.OK);
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        // 파일 경로 설정후 treeView 셋팅하기
        private bool setFolderList()
        {
            DriveInfo driveInfo = new DriveInfo(RunData._workPath.evidencePath); // 드라이브 경로 선택
            int driveImage = WhatIsdriveImage(driveInfo); // Drive Image 셋팅
            TreeNode node = new TreeNode(RunData._workPath.evidencePath.Substring(0,1), driveImage, driveImage);
            node.Tag = RunData._workPath.evidencePath;

            if (driveInfo.IsReady == true)
            {
                node.Nodes.Add("...");
            }

            folderView.Nodes.Add(node);

            return true;
        }

        private int WhatIsdriveImage(DriveInfo di)
        {
            int rtrValue = 0;
            switch (di.DriveType)    //set the drive's icon
            {
                case DriveType.CDRom:
                    rtrValue = 3;
                    break;
                case DriveType.Network:
                    rtrValue = 6;
                    break;
                case DriveType.NoRootDirectory:
                    rtrValue = 8;
                    break;
                case DriveType.Unknown:
                    rtrValue = 8;
                    break;
                default:
                    rtrValue = 2;
                    break;
            }

            return rtrValue;
        }

        private void folderView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                if (e.Node.Nodes[0].Text == "..." && e.Node.Nodes[0].Tag == null)
                {
                    e.Node.Nodes.Clear();

                    //get the list of sub direcotires
                    string[] dirs = Directory.GetDirectories(e.Node.Tag.ToString());

                    foreach (string dir in dirs)
                    {
                        DirectoryInfo di = new DirectoryInfo(dir);
                        TreeNode node = new TreeNode(di.Name, 0, 1);

                        try
                        {
                            //keep the directory's full path in the tag for use later
                            node.Tag = @dir;

                            //if the directory has sub directories add the place holder
                            if (di.GetDirectories().Count() > 0)
                                node.Nodes.Add(null, "...", 0, 0);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            //display a locked folder icon
                            node.ImageIndex = 12;
                            node.SelectedImageIndex = 12;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "DirectoryLister",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            e.Node.Nodes.Add(node);
                        }
                    }
                }
            }
        }

        // 노드 경로
        private void folderView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            string nodePath = e.Node.Tag.ToString();
            if (nodePath != RunData._workPath.evidencePath)
            {
                DirectoryInfo di = new DirectoryInfo(nodePath);
                int fileCnt = di.GetFiles("*.*", System.IO.SearchOption.AllDirectories).Length;
                int rowIDcnt = 1;
                if (fileCnt > 0)
                {
                    string[] files = Directory.GetFiles(nodePath);
                    RunData._gridData.Create_FILE_DT(nodePath);

                    if (GlobalValue.MasterDS.Tables[nodePath].Rows.Count == 0)
                    {
                        foreach (var fileItem in files)
                        {
                            FileInfo fi = new FileInfo(fileItem);
                            DataRow tmpRow = GlobalValue.MasterDS.Tables[nodePath].NewRow();
                            tmpRow["ROW_ID"] = rowIDcnt;
                            tmpRow["FILE_NAME"] = fi.Name.ToString();
                            tmpRow["FILE_PATH"] = fileItem.ToString();
                            tmpRow["FILE_SIZE"] = fi.Length;
                            tmpRow["FILE_EXT"] = fi.Extension.ToString();
                            tmpRow["FILE_C_TIME"] = fi.CreationTime;
                            tmpRow["FILE_A_TIME"] = fi.LastAccessTime;
                            GlobalValue.MasterDS.Tables[nodePath].Rows.Add(tmpRow);
                            rowIDcnt++;
                        }
                    }
                    else
                    {
                        fileGirdView.DataSource = GlobalValue.MasterDS.Tables[nodePath];
                    }
                    
                }

                if (rowIDcnt>1)
                {
                    fileGirdView.DataSource = GlobalValue.MasterDS.Tables[nodePath];
                    
                }
            }
           
        }

        // 파일 목록 그리드에 셋팅
        private bool fileGridSet(string[] files)
        {
            return true;
        }

        // HEX VIEWER 설정
        private void fileView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            DataGridViewRow dgvr = fileGirdView.CurrentRow;
            DataRow row = (dgvr.DataBoundItem as DataRowView).Row;
            GlobalValue.selectedFile = row["FILE_PATH"].ToString();
            hexView1.setHexViewer(GlobalValue.selectedFile);
            
        }

        private void pARSERToolStripMenuItem_Click(object sender, EventArgs e)
        {


            
        }

        // 메뉴 보이게 만들기
        private void fileView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                
            }
            else
            {
                ContextMenuStrip gridView_menu = new System.Windows.Forms.ContextMenuStrip();
                int position_xy_mouse_row = fileGirdView.HitTest(e.X, e.Y).RowIndex;

                if (position_xy_mouse_row >= 0)
                {
                    gridView_menu.Items.Add("Analysis").Name = "Analysis";
                    gridView_menu.Items.Add("Extract Audio").Name = "Extract Audio data";
                    gridView_menu.Items.Add("Extract Video").Name = "Extract Video data";
                }

                gridView_menu.Show(fileGirdView, new Point(e.X, e.Y));

                gridView_menu.ItemClicked += new ToolStripItemClickedEventHandler(view_menu_ItemClicked);
            }
        }

        //
        private void view_menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            DataGridViewRow dgvr = fileGirdView.CurrentRow;
            DataRow row = (dgvr.DataBoundItem as DataRowView).Row;
            switch (e.ClickedItem.Name.ToString())
            {
                case "Analysis":
                    GlobalValue.MA.analysis_module_classifire(row);
                    break;
            }
        }

        private void iSOBMFFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = @"J:\MP4\doyun.mp4";
            ISOBMFF parser = new ISOBMFF();
            parser.SetFile(filePath);
            parser.Parse();
            TreeNode tmp = parser.GetTree();
            containerTree.Nodes.Add(tmp);
            containerTree.ExpandAll();
        }

        private void nMEAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filePath = @"J:\NMEA\TS_N0016.NMEA";
            NMEA parser = new NMEA();
            parser.SetFile(filePath);
            parser.Parse();
        }

        private void rIFFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //for TEST
            string filePath = @"J:\AVI\Rec_20160721_151553_S.avi";
            RunData._targetFile = filePath;
            RIFF parser = new RIFF();
            parser.SetFile(filePath);
            parser.Parse();
            TreeNode tmp = parser.GetTree();
            containerTree.Nodes.Add(tmp);
            containerTree.ExpandAll();

        }

        // 노드가 선택되었을 때 트리, 메타데이터 출력하기
        private void containerTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                var data = containerTree.SelectedNode;
                if (data != null)
                {
                    string selectedChunk = containerTree.SelectedNode.Name.ToString();
                    long start = Convert.ToInt32(selectedChunk.Split('/')[1]); ;
                    long end = Convert.ToInt32(selectedChunk.Split('/')[2]); ;

                    byte[] hexData = new byte[(end - start)];
                    using (FileStream m_stream = new FileStream(RunData._targetFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        using (BinaryReader m_br = new BinaryReader(m_stream, new ASCIIEncoding()))
                        {
                            m_br.Read(hexData, (int)start, (int)(end - start));
                        }
                    }

                    if (hexData.Length > 0)
                    {
                        hexView1.setHexByte(hexData);
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.Show(ex.Message, "DaMEF", MsgBox.Buttons.OK);
                throw;
            }
            

        }
    }
}
