using System.Windows.Forms;
using Rui.FolderWatcher.Data;
using System.IO;
using Rui.FolderWatcher.Util;
using System;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using Rui.FolderWatcher.Util.Parser;
using System.Reflection;

namespace Rui.FolderWatcher
{
    public partial class FolderListControl : UserControl
    {
        #region イベント
        /// <summary>
        /// アイテム選択イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        public delegate void SelectedItemEventHandler(object sender, DirectoryAnalyzeData[] data);

        /// <summary>
        /// フォルダを開いたときのイベント
        /// </summary>
        public event SelectedItemEventHandler SelectedItem;
        #endregion

        #region メンバー変数
        /// <summary>
        /// ディレクトリ情報
        /// </summary>
        private DirectoryAnalyzeData analyzeData;

        /// <summary>
        /// 検索
        /// </summary>
        private FileSearchData wildSearch;

        /// <summary>
        /// ソートアルゴリズム
        /// </summary>
        private FileListVewItemComparer comapre;

        /// <summary>
        /// ソートを行ったか
        /// </summary>
        private bool isSorted;

        #endregion

        #region プロパティ
        /// <summary>
        /// ディレクトリ情報を取得します。
        /// </summary>
        public DirectoryAnalyzeData AnalyzeData
        {
            get
            {
                return analyzeData;
            }
        }

        /// <summary>
        /// 検索
        /// </summary>
        public FileSearchData Search
        {
            get
            {
                return wildSearch;
            }
            set
            {
                wildSearch = value;
            }
        }

        /// <summary>
        /// 小さなイメージリストを取得、設定します。
        /// </summary>
        public ImageList SmallImageList
        {
            get
            {
                return fileListView.SmallImageList;
            }
        }

        /// <summary>
        /// 大きなイメージリストを取得、設定します。
        /// </summary>
        public ImageList LargeImageList
        {
            get
            {
                return fileListView.LargeImageList;
            }
        }

        /// <summary>
        /// ナビゲーションペイン表示状態を設定、取得することができます。
        /// </summary>
        public bool DisplayNavigatinPain
        {
            set
            {
                navigationTreeView.Visible = value;
            }

            get
            {
                return navigationTreeView.Visible;
            }
        }

        /// <summary>
        /// フォルダリストの表示モードを設定、取得することができます。
        /// </summary>
        public View FolderListViewMode
        {
            set
            {
                this.fileListView.View = value;
            }
            get
            {
                return this.fileListView.View;
            }
        }
        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FolderListControl()
        {
            InitializeComponent();

            navigationTreeView.ImageList = new ImageList();
            fileListView.SmallImageList = new ImageList();
            fileListView.LargeImageList = new ImageList();

            navigationTreeView.ImageList.ColorDepth = ColorDepth.Depth32Bit;
            fileListView.SmallImageList.ColorDepth = ColorDepth.Depth32Bit;
            fileListView.LargeImageList.ColorDepth = ColorDepth.Depth32Bit;
            fileListView.LargeImageList.ImageSize = new Size(48, 48);

            comapre = new FileListVewItemComparer();
        }

        /// <summary>
        /// 設定情報をクリアします。
        /// </summary>
        private void Clear()
        {
            navigationTreeView.Nodes.Clear();
            navigationTreeView.ImageList.Images.Clear();

            fileListView.Items.Clear();
            fileListView.Columns.Clear();
            fileListView.SmallImageList.Images.Clear();
            fileListView.LargeImageList.Images.Clear();

            fileListView.SelectedItems.Clear();
            OnSelectItem(fileListView);

            isSorted = false;
        }
        #endregion

        #region データの取得
        /// <summary>
        /// ディレクトリ内の情報を取得
        /// </summary>
        /// <param name="path">取得対象のフォルダパス</param>
        public bool GetDirectoryInnerInfo(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            this.Cursor = Cursors.WaitCursor;

            if (File.Exists(path) == true)
            {
                // ショートカットファイルの場合はその先を参照
                if (ShellLink.LinkExtention.Equals(directory.Extension, StringComparison.OrdinalIgnoreCase) == true)
                {
                    ShellLink shellLink = new ShellLink(path);

                    if (String.IsNullOrEmpty(shellLink.Target) == false)
                    {
                        directory = new DirectoryInfo(shellLink.Target);
                    }
                }
            }

            if (directory.Exists == false)
            {
                // フォルダが存在しない場合
                this.Cursor = Cursors.Default;

                MessageBox.Show(Properties.Resources.NotFoundOpenFolder + Environment.NewLine + path, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            Clear();

            // フォルダの解析
            try
            {
                DirectoryAnalyzer analyzer = new DirectoryAnalyzer(directory);
                analyzer.ReadDirectoryInnerInfo(this);

                this.analyzeData = analyzer.AnalyzeData;


                // リストビューにヘッダーを登録
                CreateListViewHeader();

                // ツリービューにデータを登録
                TreeNode rootNode = CreateFolderTree(navigationTreeView, analyzeData);

                if (rootNode != null)
                {
                    // 先頭のノードを選択
                    navigationTreeView.SelectedNode = rootNode;
                }
            }
            catch
            {

            }

            this.Cursor = Cursors.Default;

            return this.analyzeData != null;
        }
        #endregion

        #region フォルダーツリーの操作
        /// <summary>
        /// フォルダツリーを作成します。
        /// </summary>
        /// <param name="treeView">登録するツリー</param>
        /// <param name="analyzeData">解析データ</param>
        private static TreeNode CreateFolderTree(TreeView treeView, DirectoryAnalyzeData analyzeData)
        {
            treeView.Nodes.Clear();
            treeView.ImageList.Images.Clear();

            if (analyzeData == null)
            {
                // データがない場合　中止した場合
                return null;
            }

            List<Image> imageList = new List<Image>();

            TreeNode rootNode = GetFolderNode(analyzeData, ref imageList);

            treeView.BeginUpdate();
            treeView.Nodes.Add(rootNode);
            treeView.ImageList.Images.AddRange(imageList.ToArray());

            treeView.ExpandAll();
            treeView.EndUpdate();

            return rootNode;
        }

        /// <summary>
        /// フォルダのノードを取得します。
        /// </summary>
        /// <param name="analyzeData">解析データ</param>
        /// <param name="imageList">アイコンリスト</param>
        /// <returns></returns>
        private static TreeNode GetFolderNode(DirectoryAnalyzeData analyzeData, ref List<Image> imageList)
        {
            TreeNode node = new TreeNode(analyzeData.Name);

            if (analyzeData.SmallIcon != null)
            {
                // アイコンリストのキーを取得
                int imageListIndex = GetImageListIndex(analyzeData, imageList, analyzeData.SmallIcon);
                if (imageListIndex < 0)
                {
                    imageList.Add(analyzeData.SmallIcon);

                    imageListIndex = imageList.Count - 1;
                }
                node.ImageIndex = imageListIndex;
                node.SelectedImageIndex = imageListIndex;
            }

            // サブフォルダを処理
            foreach (DirectoryAnalyzeData subAnalyzeData in analyzeData.SubDirectoryData)
            {
                TreeNode childNode = GetFolderNode(subAnalyzeData, ref imageList);
                node.Nodes.Add(childNode);
            }

            node.Tag = analyzeData;

            return node;
        }



        /// <summary>
        /// 指定されたデータのノードを選択します。
        /// </summary>
        /// <param name="analyzeData">解析データ</param>
        public void SelectNode(DirectoryAnalyzeData analyzeData)
        {
            TreeNode preSelectNode = GetTargetPathNode(navigationTreeView.TopNode, analyzeData);
            if (preSelectNode != null)
            {
                navigationTreeView.SelectedNode = preSelectNode;
            }
        }

        /// <summary>
        /// 指定されたデータのノードを取得します。
        /// </summary>
        /// <param name="node">親ノード</param>
        /// <param name="analyzeData">解析データ</param>
        /// <returns></returns>
        public static TreeNode GetTargetPathNode(TreeNode node, DirectoryAnalyzeData analyzeData)
        {
            if (node.Tag.Equals(analyzeData) == true)
            {
                return node;
            }

            foreach (TreeNode subNode in node.Nodes)
            {
                TreeNode foundNode = GetTargetPathNode(subNode, analyzeData);

                if (foundNode != null)
                {
                    return foundNode;
                }
            }

            return null;
        }
        #endregion

        #region リストビュー操作
        /// <summary>
        /// リストビューにデータを登録
        /// </summary>
        /// <param name="listView">リストビュー</param>
        /// <param name="analyzeData">解析データ</param>
        private static void SetDirecotryInnerInfoListView(ListView listView, DirectoryAnalyzeData analyzeData)
        {
            // リストを初期化
            listView.Items.Clear();
            listView.SmallImageList.Images.Clear();
            listView.LargeImageList.Images.Clear();

            List<Image> smallIconList = new List<Image>();
            List<Image> largeIconList = new List<Image>();

            // 解析データをリストビューに登録
            listView.BeginUpdate();
            SetDirecotryInnerInfoListView(listView, analyzeData, ref smallIconList, ref largeIconList);

            listView.SmallImageList.Images.AddRange(smallIconList.ToArray());
            listView.LargeImageList.Images.AddRange(largeIconList.ToArray());

            // 自動幅調整
            foreach (ColumnHeader columnHeader in listView.Columns)
            {
                columnHeader.Width = -2;
            }

            listView.EndUpdate();
        }

        /// <summary>
        /// リストビューにデータを登録
        /// </summary>
        /// <param name="listView">リストビュー</param>
        /// <param name="innerDataList">登録するデータ</param>
        private static void SetDirecotryInnerInfoListView(ListView listView, DirectoryAnalyzeData analyzeData,
            ref List<Image> smallIconList, ref List<Image> largeIconList)
        {
            // サブディレクトリを登録
            foreach (DirectoryAnalyzeData subAnalyzeData in analyzeData.SubDirectoryData)
            {
                if (subAnalyzeData.FileType == DirectoryAnalyzeData.FileTypeKind.Directory)
                {
                    ListViewItem item = GetChildViewItem(subAnalyzeData, ref smallIconList, ref largeIconList);
                    listView.Items.Add(item);
                }
            }

            // ファイルを登録
            foreach (DirectoryAnalyzeData fileAnalyzeData in analyzeData.FileData)
            {
                if (fileAnalyzeData.FileType == DirectoryAnalyzeData.FileTypeKind.File)
                {
                    ListViewItem item = GetChildViewItem(fileAnalyzeData, ref smallIconList, ref largeIconList);
                    listView.Items.Add(item);

                }
            }
        }

        /// <summary>
        /// リストビューにアイテムを追加します。
        /// </summary>
        /// <param name="iconList"></param>
        /// <param name="analyzeData">追加対象のデータ</param>
        private static ListViewItem GetChildViewItem(DirectoryAnalyzeData analyzeData, ref List<Image> smallIconList, ref List<Image> largeIconList)
        {
            ListViewItem item = new ListViewItem();

            // アイコンリストのキーを取得
            if (analyzeData.SmallIcon != null)
            {
                int imageListIndex = GetImageListIndex(analyzeData, smallIconList, analyzeData.SmallIcon);
                if (imageListIndex < 0)
                {
                    smallIconList.Add(analyzeData.SmallIcon);
                    largeIconList.Add(analyzeData.LargeIcon);

                    imageListIndex = smallIconList.Count - 1;
                }
                item.ImageIndex = imageListIndex;
            }

            // 列に合うデータを作成
            FolderWatcherSetting setting = FolderWatcherSetting.Instance;
            item.Text = analyzeData.Name;

            FolderWatcherSetting.FileItemKind[] itemKinds = setting.FileItemList;
            int index = Array.IndexOf(itemKinds, FolderWatcherSetting.FileItemKind.Name);
            if (index >= 0)
            {
                List < FolderWatcherSetting.FileItemKind > tempList = new List<FolderWatcherSetting.FileItemKind>(itemKinds);
                tempList.RemoveAt(index);
                itemKinds = tempList.ToArray();
            }
            List<String> itemDataList = analyzeData.ToArray(itemKinds);
            item.SubItems.AddRange(itemDataList.ToArray());

            return item;
        }

        /// <summary>
        /// リストビューのヘッダーを追加
        /// </summary>
        public void CreateListViewHeader()
        {
            fileListView.Columns.Clear();

            FolderWatcherSetting setting = FolderWatcherSetting.Instance;
            foreach (FolderWatcherSetting.FileItemKind fileItem in setting.FileItemList)
            {

                Type typeFileInfo = typeof(DirectoryAnalyzeData);
                PropertyInfo pi = typeFileInfo.GetProperty(fileItem.ToString());

                ColumnHeader header = new ColumnHeader();
                header.Text = FolderWatcherSetting.GetString(fileItem);
                header.Tag = pi.PropertyType;

                if (fileItem == FolderWatcherSetting.FileItemKind.DataSize)
                {
                    header.Tag = typeof(DataSize);
                }

                fileListView.Columns.Add(header);
            }
        }

        /// <summary>
        /// アイテムを全選択します。
        /// </summary>
        public void SeletionAll()
        {
            fileListView.Focus();

            foreach (ListViewItem item in fileListView.Items)
            {
                item.Selected = true;
            }
        }
        #endregion

        #region イメージリスト操作
        /// <summary>
        /// ディレクトリのアイコンインデックスを取得します。
        /// 登録されていない場合は -1を返します。
        /// </summary>
        /// <param name="innerData"></param>
        /// <param name="iconList"></param>
        /// <param name="searchIcon"></param>
        /// <returns></returns>
        private static int GetImageListIndex(DirectoryAnalyzeData innerData, List<Image> iconList, Image searchIcon)
        {
            // イメージリストに登録するIndexを取得
            int iconIndex = -1;
            if (iconList.Contains(searchIcon) == true)
            {
                iconIndex = iconList.IndexOf(searchIcon);
            }

            return iconIndex;
        }
        #endregion

        #region 操作イベント
        /// <summary>
        /// ノード選択イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void folderTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null)
            {
                return;
            }

            SetDirecotryInnerInfoListView(fileListView, (DirectoryAnalyzeData) e.Node.Tag);
            if (isSorted == true)
            {
                fileListView.ListViewItemSorter = comapre;
                fileListView.Sort();
                fileListView.ListViewItemSorter = null;
            }

            if (SelectedItem != null)
            {
                SelectedItem(this, new DirectoryAnalyzeData[]{(DirectoryAnalyzeData)e.Node.Tag});
            }
        }

        /// <summary>
        /// リストビューダブルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView listView = sender as ListView;
            if (listView == null)
            {
                return;
            }

            ListViewItem item = listView.HitTest(e.Location).Item;
            if (item == null)
            {
                return;
            }

            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            // クリックしたアイテムがノードに存在するか探す
            // 存在したら選択する
            foreach (TreeNode node in navigationTreeView.SelectedNode.Nodes)
            {
                if (node.Text.Equals(item.Text) == true)
                {
                    navigationTreeView.SelectedNode = node;
                    break;
                }
            }
        }

        /// <summary>
        /// アイテム選択変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView listView = sender as ListView;
            if (listView == null)
            {
                return;
            }

            OnSelectItem(listView);
        }

        /// <summary>
        /// 選択アイテムイベントを発生させます。
        /// </summary>
        /// <param name="listView"></param>
        private void OnSelectItem(ListView listView)
        {
            TreeNode node = navigationTreeView.SelectedNode;

            if (node == null)
            {
                if (SelectedItem != null)
                {
                    SelectedItem(this, null);
                }
                return;
            }

            DirectoryAnalyzeData parentData = (DirectoryAnalyzeData)node.Tag;

            if (listView.SelectedItems.Count <= 0)
            {
                if (SelectedItem != null)
                {
                    SelectedItem(this, new DirectoryAnalyzeData[] { parentData });
                }
                return;
            }

            List<DirectoryAnalyzeData> innerDataList = new List<DirectoryAnalyzeData>();
            innerDataList.AddRange(parentData.SubDirectoryData);
            innerDataList.AddRange(parentData.FileData);

            List<DirectoryAnalyzeData> selectItemList = new List<DirectoryAnalyzeData>();
            foreach (DirectoryAnalyzeData fileData in innerDataList)
            {
                foreach (ListViewItem item in listView.SelectedItems)
                {
                    if (fileData.Name.Equals(item.Text) == true)
                    {
                        selectItemList.Add(fileData);
                    }
                }
            }

            if (selectItemList.Count > 0)
            {
                if (SelectedItem != null)
                {
                    SelectedItem(this, selectItemList.ToArray());
                }
            }
        }

        /// <summary>
        /// リストのヘッダクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            comapre.ColumnIndex = e.Column;
            this.fileListView.ListViewItemSorter = comapre;
            this.fileListView.Sort();
            this.fileListView.ListViewItemSorter = null;

            isSorted = true;
        }

        /// <summary>
        /// キー入力イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileListView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ListView listView = sender as ListView;
                if (listView == null)
                {
                    return;
                }

                if (listView.SelectedItems.Count != 1)
                {
                    return;
                }

                ListViewItem item = listView.SelectedItems[0];
                // クリックしたアイテムがノードに存在するか探す
                // 存在したら選択する
                foreach (TreeNode node in navigationTreeView.SelectedNode.Nodes)
                {
                    if (node.Text.Equals(item.Text) == true)
                    {
                        navigationTreeView.SelectedNode = node;
                        break;
                    }
                }
            }
        }
        #endregion

        #region 操作
        /// <summary>
        /// ヒットテスト
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public DirectoryAnalyzeData HitTest(Point point)
        {
            point = fileListView.PointToClient(point);

           ListViewHitTestInfo listHitInfo = fileListView.HitTest(point);
            if (listHitInfo.Item != null)
            {
                // クリックしたアイテムがノードに存在するか探す
                // 存在したら選択する
                DirectoryAnalyzeData parentData = (DirectoryAnalyzeData)navigationTreeView.SelectedNode.Tag;
                foreach (DirectoryAnalyzeData data in parentData.SubDirectoryData)
                {
                    if (data.Name.Equals(listHitInfo.Item.Text) == true)
                    {
                        return data;
                    }
                }

                foreach (DirectoryAnalyzeData data in parentData.FileData)
                {
                    if (data.Name.Equals(listHitInfo.Item.Text) == true)
                    {
                        return data;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 親フォルダのノードへ移動します。
        /// </summary>
        public void MoveToParent()
        {
            if (navigationTreeView.SelectedNode == null)
            {
                return;
            }

            navigationTreeView.SelectedNode = navigationTreeView.SelectedNode.Parent;
        }
        #endregion

        #region 更新
        /// <summary>
        /// 描画更新処理
        /// </summary>
        public override void Refresh()
        {
            this.Cursor = Cursors.WaitCursor;

            if (navigationTreeView.SelectedNode == null)
            {
                // 何も選択されてない場合は、何もしない
                this.Cursor = Cursors.Default;
                return;
            }

            DirectoryAnalyzeData analyzeData = (DirectoryAnalyzeData)navigationTreeView.SelectedNode.Tag;
            
            Clear();

            CreateListViewHeader();

            TreeNode rootNode = CreateFolderTree(navigationTreeView, this.analyzeData);

            TreeNode selectNode = GetTargetPathNode(rootNode, analyzeData);

            if (selectNode != null)
            {
                navigationTreeView.SelectedNode = selectNode;
            }

            base.Refresh();
            this.Cursor = Cursors.Default;
        }
        #endregion
    }
}
