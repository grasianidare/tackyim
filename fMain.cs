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

namespace TackyImageViewer
{
    public partial class fMain : Form
    {
        public const string EMPTYDOTS = "...";
        public static readonly string[] FILEXT =
            {
            ".BMP",
            ".EMF",
            ".EXIF",
            ".GIF",
            ".ICON",
            ".JPEG",
            ".JPG",
            ".PNG",
            ".TIFF",
            ".WMF",
            ".SVG"
        };

        private enum IconShowMode
        {
            Tile = 0,
            LargeIcons  =1,
            SmallIcons = 2,
            List = 3,
            Details = 4
        }

        private enum ImageShowMode
        {
            Normal = 0,
            StretchImage = 1,
            AutoSize = 2,
            CenterImage = 3,
            Zoom = 4
        }


        private void PopulateTV()
        {
            //THANK YOU GOOGLE!
            //http://codehill.com/2013/06/list-drives-and-folders-in-a-treeview-using-c/

            //get a list of the drives
            string[] drives = Environment.GetLogicalDrives();

            foreach (string drive in drives)
            {
                DriveInfo di = new DriveInfo(drive);
                int driveImage;

                switch (di.DriveType)    //set the drive's icon
                {
                    case DriveType.CDRom:
                        driveImage = 1;
                        break;
                    case DriveType.Network:
                        driveImage = 2;
                        break;
                    case DriveType.NoRootDirectory:
                        driveImage = 2;
                        break;
                    case DriveType.Unknown:
                        driveImage = 4;
                        break;
                    case DriveType.Fixed:
                        driveImage = 0;
                        break;
                    default:
                        driveImage = 0;
                        break;
                }

                TreeNode node = new TreeNode(drive.Substring(0, 1), driveImage, driveImage);
                node.Tag = drive;

                if (di.IsReady == true)
                    node.Nodes.Add(EMPTYDOTS);

                tvTree.Nodes.Add(node);
            }
        }

        public fMain()
        {
            InitializeComponent();
            lvFiles.View = View.List;
            tscbListType.SelectedIndex = (int)IconShowMode.List;

            pbImage.SizeMode = PictureBoxSizeMode.Normal;
            tscbImageDisplayType.SelectedIndex = (int)ImageShowMode.Normal;
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            PopulateTV();
        }

        private void tvTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                if (e.Node.Nodes[0].Text.Equals(EMPTYDOTS) && e.Node.Nodes[0].Tag == null)
                {
                    e.Node.Nodes.Clear();

                    //get the list of sub direcotires
                    string[] dirs = Directory.GetDirectories(e.Node.Tag.ToString());

                    foreach (string dir in dirs)
                    {
                        DirectoryInfo di = new DirectoryInfo(dir);
                        TreeNode node = new TreeNode(di.Name, 3, 3);// 0, 1);

                        try
                        {
                            //keep the directory's full path in the tag for use later
                            node.Tag = dir;

                            //if the directory has sub directories add the place holder
                            if (di.GetDirectories().Count() > 0)
                                node.Nodes.Add(null, "...", 0, 0);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            //display a locked folder icon
                            node.ImageIndex = 5;// 12;
                            node.SelectedImageIndex = 5;// 12;
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

        private void tvTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //http://www.java2s.com/Code/CSharp/GUI-Windows-Form/UseListViewtodisplayFileinfonamesizeanddate.htm
            if ((tvTree.SelectedNode != null) && (!tvTree.SelectedNode.Text.Equals(EMPTYDOTS)))
            {
                ListViewItem lvi;
                lvFiles.Items.Clear();
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(tvTree.SelectedNode.Tag.ToString());
                FileInfo[] files = dir.GetFiles();

                try
                {
                    lvFiles.BeginUpdate();

                    foreach (System.IO.FileInfo file in files)
                    {
                        if (Array.IndexOf(FILEXT, file.Extension.ToUpper()) >= 0)
                        { //so adiciona arquvios com extensao suportada :)
                            lvi = new ListViewItem();
                            lvi.Text = file.Name;
                            //lvi.ToolTipText = file.Extension;
                            lvi.ImageIndex = 0;
                            lvi.Tag = System.IO.Path.Combine(tvTree.SelectedNode.Tag.ToString(), file.Name);
                            lvFiles.Items.Add(lvi);
                        }
                    }
                }
                finally
                {
                    lvFiles.EndUpdate();
                }
            }
        }

        private void lvFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvFiles.SelectedItems.Count > 0)
            {/*
                if (System.IO.Path.GetExtension(lvFiles.SelectedItems[0].Tag.ToString()).ToUpper().Equals(".SVG"))
                {
                    //PictureBoxSizeMode howWas = pbImage.SizeMode;
                    //pbImage.SizeMode = PictureBoxSizeMode.Normal;
                    SvgDocument img = SvgDocument.Open(lvFiles.SelectedItems[0].Tag.ToString());
                    img.FillRule = SvgFillRule.NonZero;
                    pbImage.Image = img.Draw();
                    //pbImage.SizeMode = howWas;
                }
                else
                { */
                    Image img = Image.FromFile(lvFiles.SelectedItems[0].Tag.ToString());
                    pbImage.Image = img;
                //}
            }
        }

        private void tscbListType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tscbListType.SelectedIndex)
            {
                case (int)IconShowMode.Tile:
                    lvFiles.View = View.Tile;
                    break;
                case (int)IconShowMode.LargeIcons:
                    lvFiles.View = View.LargeIcon;
                    break;
                case (int)IconShowMode.SmallIcons:
                    lvFiles.View = View.SmallIcon;
                    break;
                case (int)IconShowMode.List:
                    lvFiles.View = View.List;
                    break;
                case (int)IconShowMode.Details:
                    lvFiles.View = View.Details;
                    break;
            }
        }

        private void tscbImageDisplayType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tscbImageDisplayType.SelectedIndex)
            {
                case (int)ImageShowMode.AutoSize:
                    pbImage.SizeMode = PictureBoxSizeMode.AutoSize;
                    break;
                case (int)ImageShowMode.CenterImage:
                    pbImage.SizeMode = PictureBoxSizeMode.CenterImage;
                    break;
                case (int)ImageShowMode.Normal:
                    pbImage.SizeMode = PictureBoxSizeMode.Normal;
                    break;
                case (int)ImageShowMode.StretchImage:
                    pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                case (int)ImageShowMode.Zoom:
                    pbImage.SizeMode = PictureBoxSizeMode.Zoom;
                    break;
            }
        }
    }
}
