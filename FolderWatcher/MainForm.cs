using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Rui.FolderWatcher.Util;
using Rui.FolderWatcher.Data;
using Rui.FolderWatcher.Util.Parser;

namespace Rui.FolderWatcher
{
    public partial class MainForm : Form
    {
        #region 定数
        /// <summary>
        /// 親の階層
        /// </summary>
        public const String PARENT_DIRECTORY = "..";

        /// <summary>
        /// 自身の階層
        /// </summary>
        public const String OWN_DIRECTORY = ".";
        #endregion

        #region メンバ変数
        /// <summary>
        /// フォーム名
        /// </summary>
        private static String formTextFormat = " [{0}]";

        /// <summary>
        /// 表示フォルダーパス
        /// </summary>
        private String directoryPath;
        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm():this("")
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="directoryPath">表示するフォルダのパス</param>
        public MainForm(String directoryPath)
        {
            InitializeComponent();

            // 透過設定
            SetOpacity(this);

            // ウィンドウタイトル
            formTextFormat = this.Text + formTextFormat;

            this.directoryPath = directoryPath;
        }

        /// <summary>
        /// フォームが読み込まれたときのイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FolderWatcher_Load(object sender, EventArgs e)
        {
            FolderWatcherSetting setting = FolderWatcherSetting.Instance;
            if (setting.FromRectangle.IsEmpty == false)
            {
                // フォーム位置の復元
                this.StartPosition = FormStartPosition.Manual;
                this.Bounds = setting.FromRectangle;
            }

            // ペインの表示モード
            this.VisibleDetailPainMenuItem.Checked = setting.DisplayDetailPain;
            this.VisibleNavigationPainMenuItem.Checked = setting.DisplayNavigatinPain;

            // リストの表示モード
            this.folderViewControl.FolderListViewMode = setting.FolderListViewMode;
            VisibleDetailMenuItem.Checked = false;
            VisibleIconMenuItem.Checked = false;
            if (setting.FolderListViewMode == View.Details)
            {
                VisibleDetailMenuItem.Checked = true;
            }
            else if (setting.FolderListViewMode == View.Tile)
            {
                VisibleIconMenuItem.Checked = true;
            }

            if (string.IsNullOrEmpty(this.directoryPath) == false)
            {
                folderViewControl.Open(this.directoryPath);
            }
            else
            {
                this.directoryPath = setting.LastOpenFolder;
            }

            DirectoryAnalyzeData.HashType = setting.Hash;
            DirectoryAnalyzeData.RelativePath = setting.RelativePath;
        }
        #endregion

        #region 終了処理
        /// <summary>
        /// フォームクローズイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FolderWatcherForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            FolderWatcherSetting setting = FolderWatcherSetting.Instance;
            if (setting.SaveSettingFile == true)
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    setting.FromRectangle = this.Bounds;
                }
                setting.Save();
            }
        }
        #endregion

        #region メニューボタンクリックイベント
        /// <summary>
        /// 対象のファイルオープンボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileOpenMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = directoryPath;
            dialog.Description = Properties.Resources.SelectDisplayDirectory;
            dialog.ShowNewFolderButton = false;

            DialogResult result = dialog.ShowDialog();

            // OKの時は、指定されたフォルダの情報を表示する。
            if (result == DialogResult.OK)
            {
                DirectoryAnalyzeData.Search = null;
                folderViewControl.Open(dialog.SelectedPath);
            }
        }

        /// <summary>
        /// ファイル保存ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileSaveAsMenuItem_Click(object sender, EventArgs e)
        {
            FileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = CsvParser.Filter + "|" + HtmlParser.Filter + "|" + TextParser.Filter;

            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            IParser parser = null;
            String extension = Path.GetExtension(fileDialog.FileName).ToLower();
            if (0 <= Array.IndexOf(CsvParser.Extentions, extension))
            {
                parser = new CsvParser();
            }
            else if (0 <= Array.IndexOf(HtmlParser.Extentions, extension))
            {
                parser = new HtmlParser();
            }
            else
            {
                parser = new TextParser();
            }

            folderViewControl.Save(parser, fileDialog.FileName);
        }

        /// <summary>
        /// アプリケーションクローズボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 検索ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditSearchMenuItem_Click(object sender, EventArgs e)
        {
            // ウィンドウの透過設定
            SearchForm searchForm = new SearchForm(DirectoryAnalyzeData.Search);
            SetOpacity(searchForm);
            searchForm.ShowDialog();

            if (searchForm.DialogResult == DialogResult.OK)
            {
                DirectoryAnalyzeData.Search = searchForm.SearchData;

                folderViewControl.Refresh();
            }

            searchForm.Dispose();
        }

        /// <summary>
        /// すべて選択ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditSelectAllMenuItem_Click(object sender, EventArgs e)
        {
            folderViewControl.SeletionAll();
        }

        /// <summary>
        /// 再読み込みボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditRefreshMenuItem_Click(object sender, EventArgs e)
        {
            folderViewControl.Open();
        }


        /// <summary>
        /// ナビゲーションペインメニューのチェック変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VisibleNavigationPainMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item == null)
            {
                return;
            }

            FolderWatcherSetting setting = FolderWatcherSetting.Instance;
            setting.DisplayNavigatinPain = item.Checked;
            this.folderViewControl.DisplayNavigatinPain = item.Checked;
        }

        /// <summary>
        /// 詳細ペインメニューのチェック変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VisibleDetailPainMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item == null)
            {
                return;
            }

            FolderWatcherSetting setting = FolderWatcherSetting.Instance;
            setting.DisplayDetailPain = item.Checked;
            this.folderViewControl.DisplayDetailPain = item.Checked;
        }

        /// <summary>
        /// アイコン表示ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VisibleLargeIconMenuItem_Click(object sender, EventArgs e)
        {
            FolderWatcherSetting setting = FolderWatcherSetting.Instance;
            setting.FolderListViewMode = View.Tile;

            this.folderViewControl.FolderListViewMode = View.Tile;

            VisibleIconMenuItem.Checked = true;
            VisibleDetailMenuItem.Checked = false;
        }

        /// <summary>
        /// 詳細表示ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VisibleDetailMenuItem_Click(object sender, EventArgs e)
        {
            FolderWatcherSetting setting = FolderWatcherSetting.Instance;
            setting.FolderListViewMode = View.Details;

            this.folderViewControl.FolderListViewMode = View.Details;

            VisibleIconMenuItem.Checked = false;
            VisibleDetailMenuItem.Checked = true;
        }

        /// <summary>
        /// 一つ上の階層へ移動ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VisibleUpMenuItem_Click(object sender, EventArgs e)
        {
            folderViewControl.MoveToParent();
        }

        /// <summary>
        /// 列の詳細設定ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VisibleColDetailMenuItem_Click(object sender, EventArgs e)
        {
            ColumnDetailForm form = new ColumnDetailForm();

            // ウィンドウの透過設定
            SetOpacity(form);

            form.Icon = this.Icon;

            if (form.ShowDialog() == DialogResult.OK)
            {
                folderViewControl.Refresh();
            }
            form.Dispose();
        }

        /// <summary>
        /// 設定ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolOptionMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new FolderWatcherSettingForm();

            // ウィンドウの透過設定
            SetOpacity(form);

            form.Icon = this.Icon;

            form.ShowDialog();
            form.Dispose();

            // ウィンドウの透過設定
            SetOpacity(this);

            folderViewControl.Refresh();
        }

        /// <summary>
        /// バージョン情報ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HelpVersionMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();

            // ウィンドウの透過設定
            SetOpacity(form);

            form.Icon = this.Icon;

            form.ShowDialog();
            form.Dispose();
        }
        #endregion

        #region 右クリックメニュークリックイベント
        /// <summary>
        /// 右クリックメニューオープンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

            DirectoryAnalyzeData analyzeData = folderViewControl.HitTest(MousePosition);
            e.Cancel = true;

            if (analyzeData != null)
            {
                e.Cancel = false;

                OpenSubDirMenuItem.Enabled = (analyzeData.FileType == DirectoryAnalyzeData.FileTypeKind.Directory);
                ExplorerOpenMenuItem.Enabled = (analyzeData.FileType == DirectoryAnalyzeData.FileTypeKind.Directory);
            }
        }

        /// <summary>
        /// メニューオープンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void EditMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            EditCopyMenuItem.Enabled = true;

            if (folderViewControl.SelectedItems == null || folderViewControl.SelectedItems.Length < 0)
            {
                EditCopyMenuItem.Enabled = false;
            }
        }

        /// <summary>
        /// サブフォルダを開くメニューボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenSubDirMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem == null)
            {
                return;
            }

            DirectoryAnalyzeData analyzeData = folderViewControl.HitTest(menuItem.Owner.Location);
            if (analyzeData != null)
            {
                folderViewControl.SelectNode(analyzeData);
            }
        }

        /// <summary>
        /// エクスプローラで開くメニューボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExplorerOpenMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem == null)
            {
                return;
            }

            DirectoryAnalyzeData analyzeData = folderViewControl.HitTest(menuItem.Owner.Location);
            System.Diagnostics.Process.Start(analyzeData.FullName);
        }

        /// <summary>
        /// コピーメニューボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryAnalyzeData[] analyzeDataList = folderViewControl.SelectedItems;

            StringBuilder copyData = new StringBuilder();

            IParser parser = new CsvParser('\t');
            // 現在選択されている行データをコピー
            foreach (DirectoryAnalyzeData analyzeData in analyzeDataList)
            {
                // 各セルのデータはタブで区切る
                foreach (string parseData in analyzeData.Parse(parser))
                {
                    copyData.AppendLine(parseData);
                }
            }

            // クリップボードに登録する。
            if (copyData.Length > 0)
            {
                Clipboard.Clear();
                Clipboard.SetText(copyData.ToString(), new TextDataFormat());
            }
        }

        /// <summary>
        /// プロパティ表示メニューボタンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertyMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem == null)
            {
                return;
            }

            DirectoryAnalyzeData analyzeData = folderViewControl.HitTest(menuItem.Owner.Location);
            analyzeData.ShowFilePropertiesDialog(this);
        }
        #endregion

        #region リストイベント
        /// <summary>
        /// リストのクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_MouseClick(object sender, MouseEventArgs e)
        {

        }

        /// <summary>
        /// リストのダブルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left && typeof(ListView) == sender.GetType())
            //{
            //    ListViewItem clickListViewItem = ((ListView)sender).GetItemAt(e.X, e.Y);
            //    String clickDirectoryName = clickListViewItem.Text;
            //    String clickDirectoryPath = directoryPath;
            //    if (PathColumnHeader.Index != -1)
            //    {
            //        clickDirectoryPath = clickListViewItem.SubItems[PathColumnHeader.Index].Text;
            //    }

            //    // クリックした項目の中の情報を表示する。
            //    if (clickDirectoryName.Equals(OWN_DIRECTORY) == true && Path.GetPathRoot(directoryPath).Equals(directoryPath) == false)
            //    {
            //        clickDirectoryName = PARENT_DIRECTORY;
            //    }

            //    if (string.IsNullOrEmpty(clickDirectoryName) == false)
            //    {
            //        GetDirectoryInnerInfo(Path.Combine(directoryPath, clickDirectoryName));
            //    }
            //}
        }

        /// <summary>
        /// リストのヘッダクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            //comapre.ColumnIndex = e.Column;
            //this.fileListView.ListViewItemSorter = comapre;
            //this.fileListView.Sort();
            //this.fileListView.ListViewItemSorter = null;
        }

        /// <summary>
        /// リストの選択範囲変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //bool isSelectItem = (0 < fileListView.SelectedIndices.Count);

            //EditCopyMenuItem.Enabled = isSelectItem;
            //ContextCopyMenuItem.Enabled = isSelectItem;
        }
        #endregion

        #region フォーム設定
        /// <summary>
        /// フォームの透過設定を行います。
        /// </summary>
        private void SetOpacity(Form form)
        {
            FolderWatcherSetting setting = FolderWatcherSetting.Instance;
            form.Opacity = 1 - setting.Opacity * 0.05;
        }

        /// <summary>
        /// 指定列の表示/非表示設定
        /// </summary>
        /// <param name="columnHeader"></param>
        /// <param name="isVisibled"></param>
        /// <param name="index"></param>
        private void SetCloumnVisible(ColumnHeader columnHeader, bool isVisibled, int index)
        {
            //if (isVisibled == true)
            //{
            //    if (columnHeader.Index == -1)
            //    {
            //        fileListView.Columns.Insert(index, columnHeader);
            //    }
            //}
            //else
            //{
            //    if (columnHeader.Index != -1)
            //    {

            //        //パス列を削除
            //        fileListView.Columns.Remove(PathColumnHeader);
            //    }
            //}
        }

        /// <summary>
        /// ディレクトリ情報を開いたときに有効/無効化するメニュー
        /// </summary>
        /// <param name="isEnabled">有効/無効設定</param>
        private void SetOpenDirectoryMenu(bool isEnabled)
        {
            EditMenuItem.Enabled = isEnabled;
            EditSearchMenuItem.Enabled = isEnabled;
            EditSelectAllMenuItem.Enabled = isEnabled;
            EditRefreshMenuItem.Enabled = isEnabled;

            VisibleUpMenuItem.Enabled = isEnabled;
            FileSaveAsMenuItem.Enabled = isEnabled;
        }
        #endregion

        #region フォームイベント
        /// <summary>
        /// フォームリサイズイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FolderWatcherForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                FolderWatcherSetting setting = FolderWatcherSetting.Instance;
                setting.FromRectangle = this.Bounds;
            }
        }
        #endregion

        #region フォルダ操作イベント
        /// <summary>
        /// フォルダオープンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void folderViewControl1_OpenFolderEvent(object sender, DirectoryAnalyzeData[] data)
        {
            if (data == null || data.Length <= 0)
            {
                SetOpenDirectoryMenu(false);

                this.Text = Application.ProductName;
            }
            else
            {
                SetOpenDirectoryMenu(true);

                this.directoryPath = data[0].FullName;
                FolderWatcherSetting setting = FolderWatcherSetting.Instance;
                setting.LastOpenFolder = directoryPath;

                this.Text = String.Format(formTextFormat, this.directoryPath);
            }
        }
        #endregion
    }
}
