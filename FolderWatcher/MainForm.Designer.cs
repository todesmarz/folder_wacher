namespace Rui.FolderWatcher
{
    partial class MainForm
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

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolMenuStrip = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileOpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.FileSaveAsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.CloseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditCopyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.EditSearchMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.EditSelectAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditRefreshMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VisibleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VisibleNavigationPainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VisibleDetailPainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.VisibleIconMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.VisibleDetailMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.VisibleUpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.VisibleColDetailMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolOptionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpVersionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.OpenSubDirMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExplorerOpenMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.CopyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.PropertyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderViewControl = new Rui.FolderWatcher.FolderViewControl();
            this.toolMenuStrip.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolMenuStrip
            // 
            this.toolMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.EditMenuItem,
            this.VisibleMenuItem,
            this.ToolMenuItem,
            this.HelpMenuItem});
            resources.ApplyResources(this.toolMenuStrip, "toolMenuStrip");
            this.toolMenuStrip.Name = "toolMenuStrip";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileOpenMenuItem,
            this.toolStripSeparator5,
            this.FileSaveAsMenuItem,
            this.toolStripSeparator,
            this.CloseMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            resources.ApplyResources(this.FileMenuItem, "FileMenuItem");
            // 
            // FileOpenMenuItem
            // 
            resources.ApplyResources(this.FileOpenMenuItem, "FileOpenMenuItem");
            this.FileOpenMenuItem.Name = "FileOpenMenuItem";
            this.FileOpenMenuItem.Click += new System.EventHandler(this.FileOpenMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // FileSaveAsMenuItem
            // 
            resources.ApplyResources(this.FileSaveAsMenuItem, "FileSaveAsMenuItem");
            this.FileSaveAsMenuItem.Name = "FileSaveAsMenuItem";
            this.FileSaveAsMenuItem.Click += new System.EventHandler(this.FileSaveAsMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            resources.ApplyResources(this.toolStripSeparator, "toolStripSeparator");
            // 
            // CloseMenuItem
            // 
            this.CloseMenuItem.Name = "CloseMenuItem";
            resources.ApplyResources(this.CloseMenuItem, "CloseMenuItem");
            this.CloseMenuItem.Click += new System.EventHandler(this.CloseMenuItem_Click);
            // 
            // EditMenuItem
            // 
            this.EditMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditCopyMenuItem,
            this.toolStripSeparator4,
            this.EditSearchMenuItem,
            this.toolStripSeparator1,
            this.EditSelectAllMenuItem,
            this.EditRefreshMenuItem});
            resources.ApplyResources(this.EditMenuItem, "EditMenuItem");
            this.EditMenuItem.Name = "EditMenuItem";
            this.EditMenuItem.DropDownOpening += new System.EventHandler(this.EditMenuItem_DropDownOpening);
            // 
            // EditCopyMenuItem
            // 
            resources.ApplyResources(this.EditCopyMenuItem, "EditCopyMenuItem");
            this.EditCopyMenuItem.Name = "EditCopyMenuItem";
            this.EditCopyMenuItem.Click += new System.EventHandler(this.CopyMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // EditSearchMenuItem
            // 
            resources.ApplyResources(this.EditSearchMenuItem, "EditSearchMenuItem");
            this.EditSearchMenuItem.Name = "EditSearchMenuItem";
            this.EditSearchMenuItem.Click += new System.EventHandler(this.EditSearchMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // EditSelectAllMenuItem
            // 
            resources.ApplyResources(this.EditSelectAllMenuItem, "EditSelectAllMenuItem");
            this.EditSelectAllMenuItem.Name = "EditSelectAllMenuItem";
            this.EditSelectAllMenuItem.Click += new System.EventHandler(this.EditSelectAllMenuItem_Click);
            // 
            // EditRefreshMenuItem
            // 
            resources.ApplyResources(this.EditRefreshMenuItem, "EditRefreshMenuItem");
            this.EditRefreshMenuItem.Name = "EditRefreshMenuItem";
            this.EditRefreshMenuItem.Click += new System.EventHandler(this.EditRefreshMenuItem_Click);
            // 
            // VisibleMenuItem
            // 
            this.VisibleMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.VisibleNavigationPainMenuItem,
            this.VisibleDetailPainMenuItem,
            this.toolStripSeparator6,
            this.VisibleIconMenuItem,
            this.VisibleDetailMenuItem,
            this.toolStripSeparator3,
            this.VisibleUpMenuItem,
            this.toolStripSeparator8,
            this.VisibleColDetailMenuItem});
            this.VisibleMenuItem.Name = "VisibleMenuItem";
            resources.ApplyResources(this.VisibleMenuItem, "VisibleMenuItem");
            // 
            // VisibleNavigationPainMenuItem
            // 
            this.VisibleNavigationPainMenuItem.Checked = true;
            this.VisibleNavigationPainMenuItem.CheckOnClick = true;
            this.VisibleNavigationPainMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.VisibleNavigationPainMenuItem.Name = "VisibleNavigationPainMenuItem";
            resources.ApplyResources(this.VisibleNavigationPainMenuItem, "VisibleNavigationPainMenuItem");
            this.VisibleNavigationPainMenuItem.CheckedChanged += new System.EventHandler(this.VisibleNavigationPainMenuItem_CheckedChanged);
            // 
            // VisibleDetailPainMenuItem
            // 
            this.VisibleDetailPainMenuItem.Checked = true;
            this.VisibleDetailPainMenuItem.CheckOnClick = true;
            this.VisibleDetailPainMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.VisibleDetailPainMenuItem.Name = "VisibleDetailPainMenuItem";
            resources.ApplyResources(this.VisibleDetailPainMenuItem, "VisibleDetailPainMenuItem");
            this.VisibleDetailPainMenuItem.CheckStateChanged += new System.EventHandler(this.VisibleDetailPainMenuItem_CheckStateChanged);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // VisibleIconMenuItem
            // 
            this.VisibleIconMenuItem.Name = "VisibleIconMenuItem";
            resources.ApplyResources(this.VisibleIconMenuItem, "VisibleIconMenuItem");
            this.VisibleIconMenuItem.Click += new System.EventHandler(this.VisibleLargeIconMenuItem_Click);
            // 
            // VisibleDetailMenuItem
            // 
            this.VisibleDetailMenuItem.Checked = true;
            this.VisibleDetailMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.VisibleDetailMenuItem.Name = "VisibleDetailMenuItem";
            resources.ApplyResources(this.VisibleDetailMenuItem, "VisibleDetailMenuItem");
            this.VisibleDetailMenuItem.Click += new System.EventHandler(this.VisibleDetailMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // VisibleUpMenuItem
            // 
            resources.ApplyResources(this.VisibleUpMenuItem, "VisibleUpMenuItem");
            this.VisibleUpMenuItem.Name = "VisibleUpMenuItem";
            this.VisibleUpMenuItem.Click += new System.EventHandler(this.VisibleUpMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            resources.ApplyResources(this.toolStripSeparator8, "toolStripSeparator8");
            // 
            // VisibleColDetailMenuItem
            // 
            this.VisibleColDetailMenuItem.Name = "VisibleColDetailMenuItem";
            resources.ApplyResources(this.VisibleColDetailMenuItem, "VisibleColDetailMenuItem");
            this.VisibleColDetailMenuItem.Click += new System.EventHandler(this.VisibleColDetailMenuItem_Click);
            // 
            // ToolMenuItem
            // 
            this.ToolMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolOptionMenuItem});
            this.ToolMenuItem.Name = "ToolMenuItem";
            resources.ApplyResources(this.ToolMenuItem, "ToolMenuItem");
            // 
            // ToolOptionMenuItem
            // 
            this.ToolOptionMenuItem.Name = "ToolOptionMenuItem";
            resources.ApplyResources(this.ToolOptionMenuItem, "ToolOptionMenuItem");
            this.ToolOptionMenuItem.Click += new System.EventHandler(this.ToolOptionMenuItem_Click);
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpVersionMenuItem});
            this.HelpMenuItem.Name = "HelpMenuItem";
            resources.ApplyResources(this.HelpMenuItem, "HelpMenuItem");
            // 
            // HelpVersionMenuItem
            // 
            this.HelpVersionMenuItem.Name = "HelpVersionMenuItem";
            resources.ApplyResources(this.HelpVersionMenuItem, "HelpVersionMenuItem");
            this.HelpVersionMenuItem.Click += new System.EventHandler(this.HelpVersionMenuItem_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenSubDirMenuItem,
            this.ExplorerOpenMenuItem,
            this.toolStripSeparator2,
            this.CopyMenuItem,
            this.toolStripSeparator7,
            this.PropertyMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // OpenSubDirMenuItem
            // 
            this.OpenSubDirMenuItem.Name = "OpenSubDirMenuItem";
            resources.ApplyResources(this.OpenSubDirMenuItem, "OpenSubDirMenuItem");
            this.OpenSubDirMenuItem.Click += new System.EventHandler(this.OpenSubDirMenuItem_Click);
            // 
            // ExplorerOpenMenuItem
            // 
            this.ExplorerOpenMenuItem.Name = "ExplorerOpenMenuItem";
            resources.ApplyResources(this.ExplorerOpenMenuItem, "ExplorerOpenMenuItem");
            this.ExplorerOpenMenuItem.Click += new System.EventHandler(this.ExplorerOpenMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // CopyMenuItem
            // 
            this.CopyMenuItem.Name = "CopyMenuItem";
            resources.ApplyResources(this.CopyMenuItem, "CopyMenuItem");
            this.CopyMenuItem.Click += new System.EventHandler(this.CopyMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            // 
            // PropertyMenuItem
            // 
            this.PropertyMenuItem.Name = "PropertyMenuItem";
            resources.ApplyResources(this.PropertyMenuItem, "PropertyMenuItem");
            this.PropertyMenuItem.Click += new System.EventHandler(this.PropertyMenuItem_Click);
            // 
            // folderViewControl
            // 
            this.folderViewControl.AllowDrop = true;
            this.folderViewControl.ContextMenuStrip = this.contextMenuStrip;
            this.folderViewControl.DisplayDetailPain = true;
            this.folderViewControl.DisplayNavigatinPain = true;
            resources.ApplyResources(this.folderViewControl, "folderViewControl");
            this.folderViewControl.FolderListViewMode = System.Windows.Forms.View.LargeIcon;
            this.folderViewControl.Name = "folderViewControl";
            this.folderViewControl.OpenFolderEvent += new Rui.FolderWatcher.FolderListControl.SelectedItemEventHandler(this.folderViewControl1_OpenFolderEvent);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.folderViewControl);
            this.Controls.Add(this.toolMenuStrip);
            this.MainMenuStrip = this.toolMenuStrip;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.FolderWatcher_Load);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView_MouseDoubleClick);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FolderWatcherForm_FormClosing);
            this.Resize += new System.EventHandler(this.FolderWatcherForm_Resize);
            this.toolMenuStrip.ResumeLayout(false);
            this.toolMenuStrip.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip toolMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileOpenMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem CloseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditCopyMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem EditSelectAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolOptionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpVersionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditSearchMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem EditRefreshMenuItem;
        private System.Windows.Forms.ToolStripMenuItem VisibleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem VisibleIconMenuItem;
        private System.Windows.Forms.ToolStripMenuItem VisibleDetailMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem FileSaveAsMenuItem;
        private FolderViewControl folderViewControl;
        private System.Windows.Forms.ToolStripMenuItem VisibleNavigationPainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem VisibleDetailPainMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem VisibleColDetailMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem OpenSubDirMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExplorerOpenMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem CopyMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem PropertyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem VisibleUpMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
    }
}

