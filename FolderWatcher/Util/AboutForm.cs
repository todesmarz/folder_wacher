using System;
using System.Windows.Forms;

namespace Rui.FolderWatcher.Util
{
    public partial class AboutForm : Form
    {
        #region 定数
        /// <summary>
        /// コピーライトフォーマット
        /// </summary>
        private const String COPY_RIGHT = "Copy right (c) {0}. All rights reserved.";
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AboutForm()
        {
            InitializeComponent();

            softwareNameLabel.Text = Application.ProductName + " " + Application.ProductVersion;
            copyRightLabel.Text = String.Format(COPY_RIGHT, Application.CompanyName);
        }

        /// <summary>
        /// フォームロードイベント
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