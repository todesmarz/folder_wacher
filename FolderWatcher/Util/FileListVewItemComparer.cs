using System;
using System.Collections;
using System.Windows.Forms;
using Rui.FolderWatcher.Data;

namespace Rui.FolderWatcher.Util
{
    class FileListVewItemComparer : IComparer
    {
        #region メンバー変数
        /// <summary>
        /// ソート対象列番号
        /// </summary>
        private int colunIndex;

        /// <summary>
        /// 昇順でソートする場合は true
        /// </summary>
        private bool ascending = false;
        #endregion

        #region プロパティ
        /// <summary>
        /// ソート対象の列番号を取得、設定することが出来ます。
        /// </summary>
        public int ColumnIndex
        {
            set
            {
                if (colunIndex == value)
                {
                    ascending = !ascending;
                }
                else
                {
                    ascending = true;
                }
                colunIndex = value;
            }
            get
            {
                return colunIndex;
            }
        }

        /// <summary>
        /// ソートする順番を取得、設定することが出来ます。
        /// </summary>
        public bool Ascending
        {
            get
            {
                return ascending;
            }
            set
            {
                ascending = value;
            }
        }
        #endregion

        #region IComparer メンバ
        /// <summary>
        /// 比較を行います。
        /// </summary>
        /// <param name="x">比較元</param>
        /// <param name="y">比較先</param>
        /// <returns></returns>
        public int Compare(object x, object y)
        {
            int signed = 1;
            if (ascending == false)
            {
                signed *= -1;
            }

            ListViewItem firstItem = x as ListViewItem;
            ListViewItem secondItem = y as ListViewItem;
            if (firstItem != null && secondItem != null)
            {
                return CompareTo(firstItem, secondItem) * signed;
            }

            return 0;
        }

        /// <summary>
        /// リストビューアイテムの比較を行います。
        /// </summary>
        /// <param name="firstItem"></param>
        /// <param name="secondItem"></param>
        /// <returns></returns>
        private int CompareTo(ListViewItem firstItem, ListViewItem secondItem)
        {
            string firstItemText = firstItem.SubItems[colunIndex].Text;
            string secondItemText = secondItem.SubItems[colunIndex].Text;

            Type type = firstItem.ListView.Columns[colunIndex].Tag as Type;
            if (type == null || type == typeof(string) || type == typeof(bool))
            {
                return firstItemText.CompareTo(secondItemText);
            }
            else if (type == typeof(long))
            {
                return Math.Sign(Convert.ToInt64(firstItemText.Replace(",", string.Empty)) - Convert.ToInt64(secondItemText.Replace(",", string.Empty)));
            }
            else if (type == typeof(DateTime))
            {
                return Convert.ToDateTime(firstItemText).CompareTo(Convert.ToDateTime(secondItemText));
            }
            else if (type == typeof(DataSize))
            {
                return Math.Sign(DataSize.ToInt64(firstItemText) - DataSize.ToInt64(secondItemText));
            }

            return 0;
        }
        #endregion
    }
}
