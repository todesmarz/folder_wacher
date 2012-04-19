using System;
using System.Windows.Forms;
using Rui.FolderWatcher.Util;
using Rui.FolderWatcher.Data;

namespace Rui.FolderWatcher
{
    public partial class SearchForm : Form
    {
        #region プロパティ
        /// <summary>
        /// 入力された検索文字を取得、設定します。
        /// </summary>
        public String SearchWord
        {
            get
            {
                return termsTextBox.Text;
            }

            set
            {
                termsTextBox.Text = value;
            }
        }

        /// <summary>
        /// 大文字、小文字を区別するかを取得、設定します。
        /// </summary>
        public bool IgnoreCase
        {
            get
            {
                return ignoreCaseCheckBox.Checked;
            }

            set
            {
                ignoreCaseCheckBox.Checked = value;
            }
        }

        /// <summary>
        /// 曖昧検索を行うかを取得、設定します。
        /// </summary>
        public bool Vague
        {
            get
            {
                return vagueCheckBox.Checked;
            }

            set
            {
                vagueCheckBox.Checked = value;
            }
        }

        /// <summary>
        /// 検索範囲を取得、設定します。
        /// </summary>
        public FileSearchData.TargetKind Target
        {
            get
            {
                return (FileSearchData.TargetKind)targetComboBox.SelectedIndex;
            }

            set
            {
                targetComboBox.SelectedIndex = (int) value;
            }
        }

        /// <summary>
        /// 設定した検索データを取得します。
        /// </summary>
        public FileSearchData SearchData
        {
            get
            {
                return new FileSearchData(SearchWord, IgnoreCase, Vague, Target);
            }
        }
        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="searchData"></param>
        public SearchForm(FileSearchData searchData)
        {
            InitializeComponent();

            InitSetting(searchData);
        }

        /// <summary>
        /// 初期状態に戻します。
        /// </summary>
        /// <param name="searchData">検索設定データ</param>
        public void InitSetting(FileSearchData searchData)
        {
            if (searchData == null)
            {
                termsTextBox.Text = string.Empty;
                ignoreCaseCheckBox.Checked = false;
                vagueCheckBox.Checked = false;
                targetComboBox.SelectedIndex = (int)FileSearchData.TargetKind.All;
            }
            else
            {
                termsTextBox.Text = searchData.SearchWord;
                ignoreCaseCheckBox.Checked = searchData.IgnoreCase;
                vagueCheckBox.Checked = searchData.Vague;
                targetComboBox.SelectedIndex = (int)searchData.Target;
            }
        }
        #endregion

        #region キー操作イベント
        /// <summary>
        /// キーを押したときのイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void termsTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
            {
                searchButton.PerformClick();
            }
        }
        #endregion
    }
}
