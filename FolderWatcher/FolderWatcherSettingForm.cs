using System;
using System.Windows.Forms;
using Rui.FolderWatcher.Data;

namespace Rui.FolderWatcher
{
    public partial class FolderWatcherSettingForm : Form
    {
        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FolderWatcherSettingForm()
        {
            InitializeComponent();

            sizeUnitComboBox.DataSource = Enum.GetValues(typeof(FolderWatcherSetting.SizeUnitKind));

            FolderWatcherSetting setting = FolderWatcherSetting.Instance;
            sizeUnitComboBox.SelectedItem = setting.SizeUnit;

            hashKindComboBox.SelectedIndex = (int) setting.Hash;

            saveSettingCheckBox.Checked = setting.SaveSettingFile;

            relativePathCheckBox.Checked = setting.RelativePath;

            opacityTrackBar.Value = setting.Opacity;
        }
        #endregion

        #region ボタンイベント
        /// <summary>
        /// OKボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            Save();
            this.Close();
        }

        /// <summary>
        /// Cancelボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            FolderWatcherSetting setting = FolderWatcherSetting.Instance;
            setting.SizeUnit = (FolderWatcherSetting.SizeUnitKind) sizeUnitComboBox.SelectedItem;
            setting.Hash = (FolderWatcherSetting.HashKind)hashKindComboBox.SelectedIndex;
            setting.SaveSettingFile = saveSettingCheckBox.Checked;

            setting.RelativePath = relativePathCheckBox.Checked;

            setting.Opacity = opacityTrackBar.Value;

            DirectoryAnalyzeData.HashType = setting.Hash;
            DirectoryAnalyzeData.RelativePath = setting.RelativePath;

            setting.Save();
        }
        #endregion

        #region 設定
        /// <summary>
        /// 透過度決定スクロールイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void opacityTrackBar_Scroll(object sender, EventArgs e)
        {
            this.Opacity = 1 - opacityTrackBar.Value * 0.05;
        }
        #endregion
    }
}