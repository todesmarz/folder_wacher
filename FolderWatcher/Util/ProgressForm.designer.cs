namespace Rui.FolderWatcher.Util
{
    partial class ProgressForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressForm));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.targetLabel = new System.Windows.Forms.Label();
            this.targetFolderLabel = new System.Windows.Forms.Label();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.cancelButton = new System.Windows.Forms.Button();
            this.restTimeLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.currentLabel = new System.Windows.Forms.Label();
            this.tergetTitleLabel = new System.Windows.Forms.Label();
            this.currentTitleLabel = new System.Windows.Forms.Label();
            this.restTimeTitleLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            // 
            // targetLabel
            // 
            resources.ApplyResources(this.targetLabel, "targetLabel");
            this.targetLabel.Name = "targetLabel";
            // 
            // targetFolderLabel
            // 
            resources.ApplyResources(this.targetFolderLabel, "targetFolderLabel");
            this.targetFolderLabel.Name = "targetFolderLabel";
            // 
            // updateTimer
            // 
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // cancelButton
            // 
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // restTimeLabel
            // 
            resources.ApplyResources(this.restTimeLabel, "restTimeLabel");
            this.restTimeLabel.Name = "restTimeLabel";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.targetFolderLabel);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // currentLabel
            // 
            resources.ApplyResources(this.currentLabel, "currentLabel");
            this.currentLabel.Name = "currentLabel";
            // 
            // tergetTitleLabel
            // 
            resources.ApplyResources(this.tergetTitleLabel, "tergetTitleLabel");
            this.tergetTitleLabel.Name = "tergetTitleLabel";
            // 
            // currentTitleLabel
            // 
            resources.ApplyResources(this.currentTitleLabel, "currentTitleLabel");
            this.currentTitleLabel.Name = "currentTitleLabel";
            // 
            // restTimeTitleLabel
            // 
            resources.ApplyResources(this.restTimeTitleLabel, "restTimeTitleLabel");
            this.restTimeTitleLabel.Name = "restTimeTitleLabel";
            // 
            // ProgressForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.restTimeTitleLabel);
            this.Controls.Add(this.currentTitleLabel);
            this.Controls.Add(this.tergetTitleLabel);
            this.Controls.Add(this.currentLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.restTimeLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.targetLabel);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgressForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.ProgressForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        public System.Windows.Forms.Label targetLabel;
        private System.Windows.Forms.Label targetFolderLabel;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label restTimeLabel;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label currentLabel;
        private System.Windows.Forms.Label tergetTitleLabel;
        private System.Windows.Forms.Label currentTitleLabel;
        private System.Windows.Forms.Label restTimeTitleLabel;
    }
}