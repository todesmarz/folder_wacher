namespace Rui.FolderWatcher
{
    partial class FolderWatcherSettingForm
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
            System.Windows.Forms.Label sizeUnitTitleLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FolderWatcherSettingForm));
            this.okButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.saveSettingCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.opacityTrackBar = new System.Windows.Forms.TrackBar();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.hashKindComboBox = new System.Windows.Forms.ComboBox();
            this.hashTypeTitleLabel = new System.Windows.Forms.Label();
            this.sizeUnitComboBox = new System.Windows.Forms.ComboBox();
            this.relativePathCheckBox = new System.Windows.Forms.CheckBox();
            sizeUnitTitleLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.opacityTrackBar)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // sizeUnitTitleLabel
            // 
            resources.ApplyResources(sizeUnitTitleLabel, "sizeUnitTitleLabel");
            sizeUnitTitleLabel.Name = "sizeUnitTitleLabel";
            // 
            // okButton
            // 
            resources.ApplyResources(this.okButton, "okButton");
            this.okButton.Name = "okButton";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // closeButton
            // 
            resources.ApplyResources(this.closeButton, "closeButton");
            this.closeButton.Name = "closeButton";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // saveSettingCheckBox
            // 
            resources.ApplyResources(this.saveSettingCheckBox, "saveSettingCheckBox");
            this.saveSettingCheckBox.Name = "saveSettingCheckBox";
            this.saveSettingCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // opacityTrackBar
            // 
            resources.ApplyResources(this.opacityTrackBar, "opacityTrackBar");
            this.opacityTrackBar.LargeChange = 1;
            this.opacityTrackBar.Maximum = 16;
            this.opacityTrackBar.Name = "opacityTrackBar";
            this.opacityTrackBar.Scroll += new System.EventHandler(this.opacityTrackBar_Scroll);
            // 
            // tabControl
            // 
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.saveSettingCheckBox);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.opacityTrackBar);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.relativePathCheckBox);
            this.tabPage3.Controls.Add(this.hashKindComboBox);
            this.tabPage3.Controls.Add(this.hashTypeTitleLabel);
            this.tabPage3.Controls.Add(this.sizeUnitComboBox);
            this.tabPage3.Controls.Add(sizeUnitTitleLabel);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            // 
            // hashKindComboBox
            // 
            this.hashKindComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.hashKindComboBox.FormattingEnabled = true;
            this.hashKindComboBox.Items.AddRange(new object[] {
            resources.GetString("hashKindComboBox.Items"),
            resources.GetString("hashKindComboBox.Items1"),
            resources.GetString("hashKindComboBox.Items2")});
            resources.ApplyResources(this.hashKindComboBox, "hashKindComboBox");
            this.hashKindComboBox.Name = "hashKindComboBox";
            // 
            // hashTypeTitleLabel
            // 
            resources.ApplyResources(this.hashTypeTitleLabel, "hashTypeTitleLabel");
            this.hashTypeTitleLabel.Name = "hashTypeTitleLabel";
            // 
            // sizeUnitComboBox
            // 
            this.sizeUnitComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sizeUnitComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.sizeUnitComboBox, "sizeUnitComboBox");
            this.sizeUnitComboBox.Name = "sizeUnitComboBox";
            // 
            // relativePathCheckBox
            // 
            resources.ApplyResources(this.relativePathCheckBox, "relativePathCheckBox");
            this.relativePathCheckBox.Name = "relativePathCheckBox";
            this.relativePathCheckBox.UseVisualStyleBackColor = true;
            // 
            // FolderWatcherSettingForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FolderWatcherSettingForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            ((System.ComponentModel.ISupportInitialize)(this.opacityTrackBar)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.CheckBox saveSettingCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar opacityTrackBar;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox sizeUnitComboBox;
        private System.Windows.Forms.Label hashTypeTitleLabel;
        private System.Windows.Forms.ComboBox hashKindComboBox;
        private System.Windows.Forms.CheckBox relativePathCheckBox;
    }
}