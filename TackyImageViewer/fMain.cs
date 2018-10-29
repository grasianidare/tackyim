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
using System.Xml;

namespace TackyImageViewer
{
    public partial class fMain : Form
    {
        private static string InitialConfigFile = @"<AppConfig>
	<!-- IconShowMode: possible values:
	Tile
	LargeIcons
	SmallIcons
	List
	Details
	-->
    <IconShowMode>List</IconShowMode>
	<!-- ImageShowMode: possible values:
		Normal
		StretchImage
		AutoSize
		CenterImage
		Zoom	
	-->
    <ImageShowMode>CenterImage</ImageShowMode>
	<!-- VerticalSplitter:
	stores size, in pixels
	-->
    <VerticalSplitter>300</VerticalSplitter>
	<!-- HorizontalSplitter:
	you gessed it. pixel size of the horizontal splitter
	-->
    <HorizontalSplitter>200</HorizontalSplitter>
</AppConfig>
";
        public bool IsFullScreen = false;
        public string configFile;
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
            ".TIF",
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

            configFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                        Application.ProductName + ".xml");
            if (!File.Exists(configFile))
            {
                using (StreamWriter sw = File.CreateText(configFile))
                {
                    sw.WriteLine(InitialConfigFile);
                }
            }

            lvFiles.Columns.Add("File name");
            lvFiles.Columns.Add("Size");
            lvFiles.Columns.Add("Last Access");
            lvFiles.Columns.Add("Last Modified");

            string[] args = Environment.GetCommandLineArgs();
            string hi = string.Empty;
            if (args.Length > 1)
            {
                if (File.Exists(args[1]))
                    LoadFile(args[1]);
                //for (int i = 0; i < args.Length; i++)
                //  hi += args[i] + " ";
            }
            //MessageBox.Show(hi);

            #region load preferences...
            string pref = ReadConfig("IconShowMode");
            if (pref.Equals(IconShowMode.Tile.ToString()))
            {
                lvFiles.View = View.Tile;
                tscbListType.SelectedIndex = (int)IconShowMode.Tile;
            }
            else if (pref.Equals(IconShowMode.LargeIcons.ToString()))
            {
                lvFiles.View = View.LargeIcon;
                tscbListType.SelectedIndex = (int)IconShowMode.LargeIcons;
            }
            else if (pref.Equals(IconShowMode.SmallIcons.ToString()))
            {
                lvFiles.View = View.SmallIcon;
                tscbListType.SelectedIndex = (int)IconShowMode.SmallIcons;
            }
            else if (pref.Equals(IconShowMode.Details.ToString()))
            {
                lvFiles.View = View.Details;
                tscbListType.SelectedIndex = (int)IconShowMode.Details;
            }
            else //List is the default
            {
                lvFiles.View = View.List;
                tscbListType.SelectedIndex = (int)IconShowMode.List;
            }

            pref = ReadConfig("ImageShowMode");
            if (pref.Equals(ImageShowMode.AutoSize.ToString()))
            {
                pbImage.SizeMode = PictureBoxSizeMode.AutoSize;
                tscbImageDisplayType.SelectedIndex = (int)ImageShowMode.AutoSize;
            }
            else if (pref.Equals(ImageShowMode.Normal.ToString()))
            {
                pbImage.SizeMode = PictureBoxSizeMode.Normal;
                tscbImageDisplayType.SelectedIndex = (int)ImageShowMode.Normal;
            }
            else if (pref.Equals(ImageShowMode.StretchImage.ToString()))
            {
                pbImage.SizeMode = PictureBoxSizeMode.StretchImage;
                tscbImageDisplayType.SelectedIndex = (int)ImageShowMode.StretchImage;
            }
            else if (pref.Equals(ImageShowMode.Zoom.ToString()))
            {
                pbImage.SizeMode = PictureBoxSizeMode.Zoom;
                tscbImageDisplayType.SelectedIndex = (int)ImageShowMode.Zoom;
            }
            else //center image is the default
            {
                pbImage.SizeMode = PictureBoxSizeMode.CenterImage;
                tscbImageDisplayType.SelectedIndex = (int)ImageShowMode.CenterImage;
            }

            pref = ReadConfig("VerticalSplitter");
            int myTest;
            if (int.TryParse(pref, out myTest))
            {
                scMajor.SplitterDistance = myTest;
            }
            pref = ReadConfig("HorizontalSplitter");
            if (int.TryParse(pref, out myTest))
            {
                scMinor.SplitterDistance = myTest;
            }
            #endregion
        }
        private string ReadConfig(string name)
        {

                XmlDocument doc = new XmlDocument();
                doc.Load(configFile);
                foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                {
                    if (node.Name.Equals(name))
                        return node.InnerText;
                }
                return string.Empty;
        }

        private void WriteConfig(string name, string value)
        {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(configFile);
                    foreach (XmlNode node in doc.DocumentElement.ChildNodes)
                    {
                        if (node.Name.Equals(name))
                        {
                            node.InnerText = value;
                            break;
                        }
                    }
                    doc.Save(configFile);
                }
                catch (Exception myException)
                {
                    if (myException.GetType() == typeof(System.Xml.XmlException))
                    {
                        MessageBox.Show("bad written xml, check tag names.", "XML Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                        MessageBox.Show("Another kind of error!" + myException.ToString(), "IDontKnow error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

        }

        private void LoadFile(string fileName)
        {
            /*
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
            Image img = Image.FromFile(fileName);
            pbImage.Image = img;
            //}
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
                            lvi.SubItems.Add(file.Length.ToString()); //size, in bytes
                            lvi.SubItems.Add(file.LastWriteTime.ToString("dd/MM/yyyy"));
                            lvi.SubItems.Add(file.LastWriteTime.ToString("dd/MM/yyyy"));
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
            {
                LoadFile(lvFiles.SelectedItems[0].Tag.ToString());
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

            WriteConfig("IconShowMode", Enum.ToObject(typeof(IconShowMode), tscbListType.SelectedIndex).ToString());
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
            WriteConfig("ImageShowMode", Enum.ToObject(typeof(ImageShowMode), tscbImageDisplayType.SelectedIndex).ToString());
        }

        private void scMajor_SplitterMoved(object sender, SplitterEventArgs e)
        {
            WriteConfig("VerticalSplitter", scMajor.SplitterDistance.ToString());
        }

        private void scMinor_SplitterMoved(object sender, SplitterEventArgs e)
        {
            WriteConfig("HorizontalSplitter", scMinor.SplitterDistance.ToString());
        }

        private void fMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                if (IsFullScreen)
                { //turns off fullscreen
                    this.TopMost = false;

                    this.FormBorderStyle = FormBorderStyle.Sizable;

                    this.WindowState = FormWindowState.Normal;
                }
                else
                {
                    this.TopMost = true;

                    this.FormBorderStyle = FormBorderStyle.None;

                    this.WindowState = FormWindowState.Maximized;
                }
                IsFullScreen = !IsFullScreen;
            }
        }
    }
}
