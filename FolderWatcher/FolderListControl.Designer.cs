namespace Rui.FolderWatcher
{
    partial class FolderListControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderListControl));
            this.navigationTreeView = new System.Windows.Forms.TreeView();
            this.fileListView = new System.Windows.Forms.ListView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.SuspendLayout();
            // 
            // navigationTreeView
            // 
            resources.ApplyResources(this.navigationTreeView, "navigationTreeView");
            this.navigationTreeView.HideSelection = false;
            this.navigationTreeView.Name = "navigationTreeView";
            this.navigationTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.folderTreeView_AfterSelect);
            // 
            // fileListView
            // 
            resources.ApplyResources(this.fileListView, "fileListView");
            this.fileListView.FullRowSelect = true;
            this.fileListView.Name = "fileListView";
            this.fileListView.UseCompatibleStateImageBehavior = false;
            this.fileListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.fileListView_MouseDoubleClick);
            this.fileListView.SelectedIndexChanged += new System.EventHandler(this.fileListView_SelectedIndexChanged);
            this.fileListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.fileListView_ColumnClick);
            this.fileListView.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.fileListView_PreviewKeyDown);
            // 
            // splitter1
            // 
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // FolderListControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fileListView);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.navigationTreeView);
            this.Name = "FolderListControl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView navigationTreeView;
        private System.Windows.Forms.ListView fileListView;
        private System.Windows.Forms.Splitter splitter1;
    }
}
