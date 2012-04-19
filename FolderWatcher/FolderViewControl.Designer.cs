namespace Rui.FolderWatcher
{
    partial class FolderViewControl
    {
        /// <summary> 
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナで生成されたコード

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderViewControl));
            this.detailPanel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.fileTypeLabel = new System.Windows.Forms.Label();
            this.LastWriteDateTitleLabel = new System.Windows.Forms.Label();
            this.lastWriteDateLabel = new System.Windows.Forms.Label();
            this.sizeTitleLabel = new System.Windows.Forms.Label();
            this.sizeLabel = new System.Windows.Forms.Label();
            this.createTitleLabel = new System.Windows.Forms.Label();
            this.createLabel = new System.Windows.Forms.Label();
            this.fileIconpictureBox = new System.Windows.Forms.PictureBox();
            this.largeImageList = new System.Windows.Forms.ImageList(this.components);
            this.smallImageList = new System.Windows.Forms.ImageList(this.components);
            this.folderListControl = new Rui.FolderWatcher.FolderListControl();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.detailPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileIconpictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // detailPanel
            // 
            this.detailPanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.detailPanel.Controls.Add(this.flowLayoutPanel1);
            this.detailPanel.Controls.Add(this.fileIconpictureBox);
            resources.ApplyResources(this.detailPanel, "detailPanel");
            this.detailPanel.Name = "detailPanel";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.fileNameLabel);
            this.flowLayoutPanel1.Controls.Add(this.fileTypeLabel);
            this.flowLayoutPanel1.Controls.Add(this.LastWriteDateTitleLabel);
            this.flowLayoutPanel1.Controls.Add(this.lastWriteDateLabel);
            this.flowLayoutPanel1.Controls.Add(this.sizeTitleLabel);
            this.flowLayoutPanel1.Controls.Add(this.sizeLabel);
            this.flowLayoutPanel1.Controls.Add(this.createTitleLabel);
            this.flowLayoutPanel1.Controls.Add(this.createLabel);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // fileNameLabel
            // 
            resources.ApplyResources(this.fileNameLabel, "fileNameLabel");
            this.fileNameLabel.Name = "fileNameLabel";
            // 
            // fileTypeLabel
            // 
            resources.ApplyResources(this.fileTypeLabel, "fileTypeLabel");
            this.fileTypeLabel.Name = "fileTypeLabel";
            // 
            // LastWriteDateTitleLabel
            // 
            resources.ApplyResources(this.LastWriteDateTitleLabel, "LastWriteDateTitleLabel");
            this.LastWriteDateTitleLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.LastWriteDateTitleLabel.Name = "LastWriteDateTitleLabel";
            // 
            // lastWriteDateLabel
            // 
            resources.ApplyResources(this.lastWriteDateLabel, "lastWriteDateLabel");
            this.lastWriteDateLabel.Name = "lastWriteDateLabel";
            // 
            // sizeTitleLabel
            // 
            resources.ApplyResources(this.sizeTitleLabel, "sizeTitleLabel");
            this.sizeTitleLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.sizeTitleLabel.Name = "sizeTitleLabel";
            // 
            // sizeLabel
            // 
            resources.ApplyResources(this.sizeLabel, "sizeLabel");
            this.sizeLabel.Name = "sizeLabel";
            // 
            // createTitleLabel
            // 
            resources.ApplyResources(this.createTitleLabel, "createTitleLabel");
            this.createTitleLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.createTitleLabel.Name = "createTitleLabel";
            // 
            // createLabel
            // 
            resources.ApplyResources(this.createLabel, "createLabel");
            this.createLabel.Name = "createLabel";
            // 
            // fileIconpictureBox
            // 
            resources.ApplyResources(this.fileIconpictureBox, "fileIconpictureBox");
            this.fileIconpictureBox.Name = "fileIconpictureBox";
            this.fileIconpictureBox.TabStop = false;
            // 
            // largeImageList
            // 
            this.largeImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.largeImageList, "largeImageList");
            this.largeImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // smallImageList
            // 
            this.smallImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            resources.ApplyResources(this.smallImageList, "smallImageList");
            this.smallImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // folderListControl
            // 
            this.folderListControl.DisplayNavigatinPain = true;
            resources.ApplyResources(this.folderListControl, "folderListControl");
            this.folderListControl.FolderListViewMode = System.Windows.Forms.View.LargeIcon;
            this.folderListControl.Name = "folderListControl";
            this.folderListControl.Search = null;
            this.folderListControl.SelectedItem += new Rui.FolderWatcher.FolderListControl.SelectedItemEventHandler(this.folderListControl1_SelectedItem);
            // 
            // toolTip
            // 
            this.toolTip.IsBalloon = true;
            // 
            // FolderViewControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.folderListControl);
            this.Controls.Add(this.detailPanel);
            this.Name = "FolderViewControl";
            this.detailPanel.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileIconpictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel detailPanel;
        private System.Windows.Forms.ImageList largeImageList;
        private System.Windows.Forms.ImageList smallImageList;
        private FolderListControl folderListControl;
        private System.Windows.Forms.PictureBox fileIconpictureBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.Label fileTypeLabel;
        private System.Windows.Forms.Label LastWriteDateTitleLabel;
        private System.Windows.Forms.Label lastWriteDateLabel;
        private System.Windows.Forms.Label sizeTitleLabel;
        private System.Windows.Forms.Label sizeLabel;
        private System.Windows.Forms.Label createTitleLabel;
        private System.Windows.Forms.Label createLabel;
        private System.Windows.Forms.ToolTip toolTip;

    }
}
