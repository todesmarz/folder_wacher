using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Rui.FolderWatcher.Data;

namespace Rui.FolderWatcher
{
    public partial class ColumnDetailForm : Form
    {
        #region メンバー変数
        /// <summary>
        /// 選択アイテムリスト
        /// </summary>
        public List<KeyValuePair<FolderWatcherSetting.FileItemKind, string>> itemList;
        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ColumnDetailForm()
        {
            InitializeComponent();

            itemList = new List<KeyValuePair<FolderWatcherSetting.FileItemKind, string>>();
        }

        /// <summary>
        /// フォームロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColumnDetailForm_Load(object sender, EventArgs e)
        {
            FolderWatcherSetting setting = FolderWatcherSetting.Instance;

            // 設定されている項目を取得
            foreach (FolderWatcherSetting.FileItemKind fileItem in setting.FileItemList)
            {
                itemList.Add(new KeyValuePair<FolderWatcherSetting.FileItemKind, string>(fileItem, FolderWatcherSetting.GetString(fileItem)));
            }

            // そのほかの項目を取得
            foreach (FolderWatcherSetting.FileItemKind fileItem in Enum.GetValues(typeof(FolderWatcherSetting.FileItemKind)))
            {
                if (Array.IndexOf(setting.FileItemList, fileItem) >= 0)
                {
                    continue;
                }

                itemList.Add(new KeyValuePair<FolderWatcherSetting.FileItemKind, string>(fileItem, FolderWatcherSetting.GetString(fileItem)));
            }

            // リストに登録
            foreach (KeyValuePair<FolderWatcherSetting.FileItemKind, string> item in itemList)
            {
                bool isCheck = false;
                if (Array.IndexOf(setting.FileItemList, item.Key) >= 0)
                {
                    isCheck = true;
                }

                checkedListBox.Items.Add(item.Value, isCheck);
            }

            checkedListBox.SelectedIndex = 0;
        }
        #endregion

        #region ボタンイベント
        /// <summary>
        /// 上へボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upButton_Click(object sender, EventArgs e)
        {
            int selectIndex = checkedListBox.SelectedIndex;
            if (selectIndex <= 1)
            {
                return;
            }

            //移動元のデータを取得
            object selectObject = checkedListBox.SelectedItem;

            bool isChecked = checkedListBox.CheckedIndices.Contains(selectIndex);

            //リストから移動元のデータを削除
            checkedListBox.Items.RemoveAt(selectIndex);

            //元の1つ前のインデックス番号に挿入する
            checkedListBox.Items.Insert(selectIndex - 1, selectObject);

            // チェック状態を反映
            checkedListBox.SetItemChecked(selectIndex - 1, isChecked);

            //移動後のインデックスを選択する
            checkedListBox.SelectedIndex = selectIndex - 1;
        }

        /// <summary>
        /// 下へボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void downButton_Click(object sender, EventArgs e)
        {
            int selectIndex = checkedListBox.SelectedIndex;
            if (selectIndex >= checkedListBox.Items.Count - 1)
            {
                return;
            }

            //移動元のデータを取得
            object selectObject = checkedListBox.SelectedItem;

            bool isChecked = checkedListBox.CheckedIndices.Contains(selectIndex);

            //リストから移動元のデータを削除
            checkedListBox.Items.RemoveAt(selectIndex);

            //元の1つ前のインデックス番号に挿入する
            checkedListBox.Items.Insert(selectIndex + 1, selectObject);

            // チェック状態を反映
            checkedListBox.SetItemChecked(selectIndex + 1, isChecked);

            //移動後のインデックスを選択する
            checkedListBox.SelectedIndex = selectIndex + 1;
        }

        /// <summary>
        /// 表示ボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void visibleButton_Click(object sender, EventArgs e)
        {
            //選択アイテムインデックスを取得
            int selectIndex = checkedListBox.SelectedIndex;
            if (selectIndex < 0)
            {
                return;
            }

            checkedListBox.SetItemChecked(selectIndex, true);

            bool isChecked = checkedListBox.CheckedIndices.Contains(selectIndex);
            visibleButton.Enabled = !isChecked;
            hideButton.Enabled = isChecked;
        }

        /// <summary>
        /// 非表示ボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hideButton_Click(object sender, EventArgs e)
        {
            //選択アイテムインデックスを取得
            int selectIndex = checkedListBox.SelectedIndex;
            if (selectIndex < 0)
            {
                return;
            }

            checkedListBox.SetItemChecked(selectIndex, false);

            bool isChecked = checkedListBox.CheckedIndices.Contains(selectIndex);
            visibleButton.Enabled = !isChecked;
            hideButton.Enabled = isChecked;
        }

        /// <summary>
        /// OKボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            CheckedListBox.CheckedItemCollection checkItemList = checkedListBox.CheckedItems;

            List<FolderWatcherSetting.FileItemKind> fileItemList = new List<FolderWatcherSetting.FileItemKind>();

            foreach (object item in checkItemList)
            {
                foreach (KeyValuePair<FolderWatcherSetting.FileItemKind, string> fileItem in itemList)
                {
                    // チェックされたアイテムを設定
                    if (fileItem.Value.Equals(item) == true)
                    {
                        fileItemList.Add(fileItem.Key);
                    }
                }
            }

            // 設定に設定
            FolderWatcherSetting setting = FolderWatcherSetting.Instance;
            setting.FileItemList = fileItemList.ToArray();
        }
        #endregion

        /// <summary>
        /// 選択アイテム変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //選択アイテムインデックスを取得
            int selectIndex = checkedListBox.SelectedIndex;
            if (selectIndex < 0)
            {
                return;
            }

            bool isChecked = checkedListBox.CheckedIndices.Contains(selectIndex);
            visibleButton.Enabled = !isChecked;
            hideButton.Enabled = isChecked;

            upButton.Enabled = (selectIndex > 1);
            downButton.Enabled = (selectIndex < checkedListBox.Items.Count - 1 && selectIndex != 0);
        }
    }
}
