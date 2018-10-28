namespace TackyImageViewer
{
    partial class fMain
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMain));
            this.tsToolBar = new System.Windows.Forms.ToolStrip();
            this.sstatus = new System.Windows.Forms.StatusStrip();
            this.scMajor = new System.Windows.Forms.SplitContainer();
            this.scMinor = new System.Windows.Forms.SplitContainer();
            this.tvTree = new System.Windows.Forms.TreeView();
            this.lvFiles = new System.Windows.Forms.ListView();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tscbListType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tscbImageDisplayType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tsToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMajor)).BeginInit();
            this.scMajor.Panel1.SuspendLayout();
            this.scMajor.Panel2.SuspendLayout();
            this.scMajor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMinor)).BeginInit();
            this.scMinor.Panel1.SuspendLayout();
            this.scMinor.Panel2.SuspendLayout();
            this.scMinor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // tsToolBar
            // 
            this.tsToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tscbListType,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.tscbImageDisplayType,
            this.toolStripLabel3});
            this.tsToolBar.Location = new System.Drawing.Point(0, 0);
            this.tsToolBar.Name = "tsToolBar";
            this.tsToolBar.Size = new System.Drawing.Size(800, 25);
            this.tsToolBar.TabIndex = 0;
            this.tsToolBar.Text = "toolStrip1";
            // 
            // sstatus
            // 
            this.sstatus.Location = new System.Drawing.Point(0, 428);
            this.sstatus.Name = "sstatus";
            this.sstatus.Size = new System.Drawing.Size(800, 22);
            this.sstatus.TabIndex = 1;
            this.sstatus.Text = "statusStrip1";
            // 
            // scMajor
            // 
            this.scMajor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMajor.Location = new System.Drawing.Point(0, 25);
            this.scMajor.Name = "scMajor";
            // 
            // scMajor.Panel1
            // 
            this.scMajor.Panel1.Controls.Add(this.scMinor);
            // 
            // scMajor.Panel2
            // 
            this.scMajor.Panel2.Controls.Add(this.pbImage);
            this.scMajor.Size = new System.Drawing.Size(800, 403);
            this.scMajor.SplitterDistance = 335;
            this.scMajor.TabIndex = 2;
            // 
            // scMinor
            // 
            this.scMinor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMinor.Location = new System.Drawing.Point(0, 0);
            this.scMinor.Name = "scMinor";
            this.scMinor.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMinor.Panel1
            // 
            this.scMinor.Panel1.Controls.Add(this.tvTree);
            // 
            // scMinor.Panel2
            // 
            this.scMinor.Panel2.Controls.Add(this.lvFiles);
            this.scMinor.Size = new System.Drawing.Size(335, 403);
            this.scMinor.SplitterDistance = 208;
            this.scMinor.TabIndex = 0;
            // 
            // tvTree
            // 
            this.tvTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTree.HideSelection = false;
            this.tvTree.Location = new System.Drawing.Point(0, 0);
            this.tvTree.Name = "tvTree";
            this.tvTree.Size = new System.Drawing.Size(335, 208);
            this.tvTree.TabIndex = 0;
            this.tvTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvTree_BeforeExpand);
            this.tvTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvTree_AfterSelect);
            // 
            // lvFiles
            // 
            this.lvFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFiles.Location = new System.Drawing.Point(0, 0);
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new System.Drawing.Size(335, 191);
            this.lvFiles.TabIndex = 0;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.SelectedIndexChanged += new System.EventHandler(this.lvFiles_SelectedIndexChanged);
            // 
            // pbImage
            // 
            this.pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbImage.Location = new System.Drawing.Point(0, 0);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(461, 403);
            this.pbImage.TabIndex = 0;
            this.pbImage.TabStop = false;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(84, 22);
            this.toolStripLabel1.Text = "Show Icons as:";
            // 
            // tscbListType
            // 
            this.tscbListType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbListType.Items.AddRange(new object[] {
            "Tile",
            "Large Icons",
            "Small icons",
            "List",
            "Details"});
            this.tscbListType.Name = "tscbListType";
            this.tscbListType.Size = new System.Drawing.Size(121, 25);
            this.tscbListType.SelectedIndexChanged += new System.EventHandler(this.tscbListType_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(89, 22);
            this.toolStripLabel2.Text = "Show image as:";
            // 
            // tscbImageDisplayType
            // 
            this.tscbImageDisplayType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbImageDisplayType.Items.AddRange(new object[] {
            "Normal",
            "Stretch Image",
            "Auto Size",
            "Center Image",
            "Zoom"});
            this.tscbImageDisplayType.Name = "tscbImageDisplayType";
            this.tscbImageDisplayType.Size = new System.Drawing.Size(121, 25);
            this.tscbImageDisplayType.SelectedIndexChanged += new System.EventHandler(this.tscbImageDisplayType_SelectedIndexChanged);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(86, 22);
            this.toolStripLabel3.Text = "toolStripLabel3";
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.scMajor);
            this.Controls.Add(this.sstatus);
            this.Controls.Add(this.tsToolBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tacky Image Viewer";
            this.Load += new System.EventHandler(this.fMain_Load);
            this.tsToolBar.ResumeLayout(false);
            this.tsToolBar.PerformLayout();
            this.scMajor.Panel1.ResumeLayout(false);
            this.scMajor.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMajor)).EndInit();
            this.scMajor.ResumeLayout(false);
            this.scMinor.Panel1.ResumeLayout(false);
            this.scMinor.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMinor)).EndInit();
            this.scMinor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsToolBar;
        private System.Windows.Forms.StatusStrip sstatus;
        private System.Windows.Forms.SplitContainer scMajor;
        private System.Windows.Forms.SplitContainer scMinor;
        private System.Windows.Forms.TreeView tvTree;
        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tscbListType;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox tscbImageDisplayType;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
    }
}

