using System;
using System.Windows.Forms;

namespace Rui.FolderWatcher.Util
{
    public partial class AboutForm : Form
    {
        #region �萔
        /// <summary>
        /// �R�s�[���C�g�t�H�[�}�b�g
        /// </summary>
        private const String COPY_RIGHT = "Copy right (c) {0}. All rights reserved.";
        #endregion

        #region �R���X�g���N�^
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public AboutForm()
        {
            InitializeComponent();

            softwareNameLabel.Text = Application.ProductName + " " + Application.ProductVersion;
            copyRightLabel.Text = String.Format(COPY_RIGHT, Application.CompanyName);
        }

        /// <summary>
        /// �t�H�[�����[�h�C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = this.Icon.ToBitmap();
        }
        #endregion
    }
}