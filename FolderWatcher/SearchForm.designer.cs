namespace Rui.FolderWatcher
{
    partial class SearchForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchForm));
            this.termsLabel = new System.Windows.Forms.Label();
            this.termsTextBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.detailGroupBox = new System.Windows.Forms.GroupBox();
            this.targetComboBox = new System.Windows.Forms.ComboBox();
            this.targetLabel = new System.Windows.Forms.Label();
            this.vagueCheckBox = new System.Windows.Forms.CheckBox();
            this.ignoreCaseCheckBox = new System.Windows.Forms.CheckBox();
            this.detailGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // termsLabel
            // 
            resources.ApplyResources(this.termsLabel, "termsLabel");
            this.termsLabel.Name = "termsLabel";
            // 
            // termsTextBox
            // 
            resources.ApplyResources(this.termsTextBox, "termsTextBox");
            this.termsTextBox.Name = "termsTextBox";
            this.termsTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.termsTextBox_KeyPress);
            // 
            // searchButton
            // 
            this.searchButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.searchButton, "searchButton");
            this.searchButton.Name = "searchButton";
            this.searchButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.cancelButton, "cancelButton");
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // descriptionLabel
            // 
            resources.ApplyResources(this.descriptionLabel, "descriptionLabel");
            this.descriptionLabel.Name = "descriptionLabel";
            // 
            // detailGroupBox
            // 
            this.detailGroupBox.Controls.Add(this.targetComboBox);
            this.detailGroupBox.Controls.Add(this.targetLabel);
            this.detailGroupBox.Controls.Add(this.vagueCheckBox);
            this.detailGroupBox.Controls.Add(this.ignoreCaseCheckBox);
            resources.ApplyResources(this.detailGroupBox, "detailGroupBox");
            this.detailGroupBox.Name = "detailGroupBox";
            this.detailGroupBox.TabStop = false;
            // 
            // targetComboBox
            // 
            this.targetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.targetComboBox.FormattingEnabled = true;
            this.targetComboBox.Items.AddRange(new object[] {
            resources.GetString("targetComboBox.Items"),
            resources.GetString("targetComboBox.Items1"),
            resources.GetString("targetComboBox.Items2")});
            resources.ApplyResources(this.targetComboBox, "targetComboBox");
            this.targetComboBox.Name = "targetComboBox";
            // 
            // targetLabel
            // 
            resources.ApplyResources(this.targetLabel, "targetLabel");
            this.targetLabel.Name = "targetLabel";
            // 
            // vagueCheckBox
            // 
            resources.ApplyResources(this.vagueCheckBox, "vagueCheckBox");
            this.vagueCheckBox.Name = "vagueCheckBox";
            this.vagueCheckBox.UseVisualStyleBackColor = true;
            // 
            // ignoreCaseCheckBox
            // 
            resources.ApplyResources(this.ignoreCaseCheckBox, "ignoreCaseCheckBox");
            this.ignoreCaseCheckBox.Name = "ignoreCaseCheckBox";
            this.ignoreCaseCheckBox.UseVisualStyleBackColor = true;
            // 
            // SearchForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.detailGroupBox);
            this.Controls.Add(this.descriptionLabel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.termsTextBox);
            this.Controls.Add(this.termsLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SearchForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.detailGroupBox.ResumeLayout(false);
            this.detailGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label termsLabel;
        private System.Windows.Forms.TextBox termsTextBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.GroupBox detailGroupBox;
        private System.Windows.Forms.CheckBox ignoreCaseCheckBox;
        private System.Windows.Forms.CheckBox vagueCheckBox;
        private System.Windows.Forms.ComboBox targetComboBox;
        private System.Windows.Forms.Label targetLabel;
    }
}