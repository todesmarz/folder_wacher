using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Rui.FolderWatcher.Data;
using Rui.FolderWatcher.Util;
using System.IO;
using Rui.FolderWatcher.Util.Parser;
using System.Threading;

namespace Rui.FolderWatcher
{
    public partial class FolderViewControl : UserControl
    {
        #region イベント
        /// <summary>
        /// フォルダを開いたときのイベント
        /// </summary>
        public event FolderListControl.SelectedItemEventHandler OpenFolderEvent;
        #endregion

        #region メンバー変数
        /// <summary>
        /// 選択されている解析データリスト
        /// </summary>
        private DirectoryAnalyzeData[] selectionAnalyzeDataList;
        #endregion

        #region プロパティ
        /// <summary>
        /// ナビゲーションペイン表示状態を取得、設定することができます。
        /// </summary>
        public bool DisplayNavigatinPain
        {
            get
            {
                return folderListControl.DisplayNavigatinPain;
            }
            set
            {
                folderListControl.DisplayNavigatinPain = value;
            }
        }

        /// <summary>
        /// ナビゲーションペイン表示状態を取得、設定することができます。
        /// </summary>
        public bool DisplayDetailPain
        {
            get
            {
                return detailPanel.Visible;
            }
            set
            {

                this.detailPanel.Visible = value;
            }
        }

        /// <summary>
        /// フォルダリストの表示モードを取得、設定することができます。
        /// </summary>
        public View FolderListViewMode
        {
            get
            {
                return folderListControl.FolderListViewMode;
            }
            set
            {
                folderListControl.FolderListViewMode = value;
            }
        }

        [Browsable(false)]
        /// <summary>
        /// 選択状態になっているアイテムのデータを取得することができます。
        /// </summary>
        public DirectoryAnalyzeData[] SelectedItems
        {
            get
            {
                return selectionAnalyzeDataList;
            }
        }
        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FolderViewControl()
        {
            InitializeComponent();
        }
        #endregion

        #region フォルダー操作
        /// <summary>
        /// 開く
        /// </summary>
        public void Open()
        {
            if (folderListControl.AnalyzeData == null)
            {
                return;
            }

            Open(folderListControl.AnalyzeData.FullName);
        }

        /// <summary>
        /// 開く
        /// </summary>
        /// <param name="path">開くフォルダパス</param>
        public void Open(String path)
        {
            DirectoryAnalyzeData[] openDirectoryData = null;

            // スレッドを発生させる
            // D&D時にフォルダが固まるため
            Thread process = new Thread(
            delegate()
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate()
                    {
                        if (folderListControl.GetDirectoryInnerInfo(path) == true)
                        {
                            openDirectoryData = new DirectoryAnalyzeData[] { folderListControl.AnalyzeData };
                        }

                        if (OpenFolderEvent != null)
                        {
                            OpenFolderEvent(this, openDirectoryData);
                        }
                    });
                }
            });
            process.Start();
        }
        #endregion

        #region ドラックドロップ処理
        /// <summary>
        /// ドロップされたときの処理
        /// </summary>
        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);

            //ドロップされたデータがファイルドロップか調べる
            if (drgevent.Data.GetDataPresent(DataFormats.FileDrop))
            {
                String[] filePaths = (String[])drgevent.Data.GetData(DataFormats.FileDrop);

                if (filePaths == null || filePaths.Length <= 0)
                {
                    return;
                }

                Open(filePaths[0]);
            }
        }

        /// <summary>
        /// ドラックされたときの処理
        /// </summary>

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            base.OnDragEnter(drgevent);

            //// ドラッグされているデータがファイルドロップ型か調べ、
            //// そうであればドロップ効果をMoveにする
            if (drgevent.Data.GetDataPresent(DataFormats.FileDrop) && this.Enabled == true)
            {
                drgevent.Effect = DragDropEffects.Copy;
            }
            else
            {
                // それ以外は、受け入れない
                drgevent.Effect = DragDropEffects.None;
            }
        }
        #endregion

        #region リストビュー操作
        /// <summary>
        /// リストの全選択
        /// </summary>
        public void SeletionAll()
        {
            folderListControl.SeletionAll();
        }

        /// <summary>
        /// ヒットテスト
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public DirectoryAnalyzeData HitTest(Point point)
        {
            return folderListControl.HitTest(point);
        }

        /// <summary>
        /// 指定されたデータのノードを選択します。
        /// </summary>
        /// <param name="analyzeData">解析データ</param>
        public void SelectNode(DirectoryAnalyzeData analyzeData)
        {
            folderListControl.SelectNode(analyzeData);
        }

        /// <summary>
        /// 親フォルダのノードへ移動します。
        /// </summary>
        public void MoveToParent()
        {
            folderListControl.MoveToParent();
        }
        #endregion

        #region リストビュー操作イベント
        /// <summary>
        /// アイテム選択イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        private void folderListControl1_SelectedItem(object sender, DirectoryAnalyzeData[] dataArray)
        {
            selectionAnalyzeDataList = dataArray;

            if (dataArray == null || dataArray.Length <= 0)
            {
                // 何も選択されていない場合
                fileIconpictureBox.Image = null;
                flowLayoutPanel1.Visible = false;
                toolTip.SetToolTip(fileIconpictureBox, null);
                return;
            }

            fileIconpictureBox.Image = dataArray[0].LargeIcon;

            if (dataArray.Length <= 1)
            {
                // 一つのファイルが選択されている場合は、
                // そのファイルの情報を表示
                fileNameLabel.Text = dataArray[0].Name;
                fileTypeLabel.Text = dataArray[0].Kind;
                toolTip.SetToolTip(fileIconpictureBox, dataArray[0].FullName);
            }
            else
            {
                // 複数ファイルが選択されている場合は、
                // 選択されたファイル数を表示
                fileNameLabel.Text = string.Format(Properties.Resources.SelectItemsMessage, dataArray.Length);
                toolTip.SetToolTip(fileIconpictureBox, Path.GetDirectoryName(dataArray[0].FullName));
            }

            // それぞれの最大最小値を取得して表示
            DateTime minLMD = dataArray[0].LastModifyDate;
            DateTime maxLMD = dataArray[0].LastModifyDate;
            DateTime minCreate = dataArray[0].CreationDate;
            DateTime maxCreate = dataArray[0].CreationDate;
            long totalSize = 0;
            foreach (DirectoryAnalyzeData data in dataArray)
            {
                // 最終更新日付
                if (minLMD > data.LastModifyDate)
                {
                    minLMD = data.LastModifyDate;
                }
                if (maxLMD < data.LastModifyDate)
                {
                    maxLMD = data.LastModifyDate;
                }

                // サイズ
                totalSize += data.Size;

                // 作成日付
                if (minCreate > data.CreationDate)
                {
                    minCreate = data.CreationDate;
                }
                if (maxCreate < data.CreationDate)
                {
                    maxCreate = data.CreationDate;
                }
            }

            // 最大最小が同じならそのままの値を表示    
            if (minLMD == maxLMD)
            {
                lastWriteDateLabel.Text = minLMD.ToString();
            }
            else
            {
                lastWriteDateLabel.Text = string.Format(Properties.Resources.FromToDate, minLMD, maxLMD);
            }

            sizeLabel.Text = DataSize.GetRegularSize(totalSize);

            if (minCreate == maxCreate)
            {
                createLabel.Text = minCreate.ToString();
            }
            else
            {
                createLabel.Text = string.Format(Properties.Resources.FromToDate, minCreate, maxCreate);
            }

            flowLayoutPanel1.Visible = true;
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="parser">保存形式</param>
        /// <param name="path">保存ファイルパス</param>
        public void Save(IParser parser, string path)
        {
            if (folderListControl.AnalyzeData == null)
            {
                // データを読み込んでいない場合
                return;
            }

            List<string> dataList = new List<string>();

            // ヘッダー
            dataList.Add(parser.GetHeader(folderListControl.AnalyzeData));

            // データ
            dataList.AddRange(folderListControl.AnalyzeData.Parse(parser, true));

            // フッター
            dataList.Add(parser.GetFooter(folderListControl.AnalyzeData));

            File.WriteAllLines(path, dataList.ToArray());
        }
        #endregion

        #region 更新
        /// <summary>
        /// 描画更新処理
        /// </summary>
        public override void Refresh()
        {
            folderListControl.Refresh();

            base.Refresh();
        }
        #endregion
    }
}
