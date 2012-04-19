using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Rui.FolderWatcher.Util;
using Rui.FolderWatcher.Data;

namespace Rui.FolderWatcher
{
    class DirectoryAnalyzer
    {

        #region メンバー変数
        /// <summary>
        /// 処理対象のディレクトリ
        /// </summary>
        DirectoryInfo currentDirectory;

        /// <summary>
        /// ディレクトリ内の情報
        /// </summary>
        private DirectoryAnalyzeData analyzeData;

        /// <summary>
        /// 進捗状況
        /// </summary>
        ProgressForm.ProgressData progressData;

        /// <summary>
        /// 進捗バーフォーム
        /// </summary>
        private ProgressForm progressForm;
        #endregion

        #region プロパティ
        /// <summary>
        /// ディレクトリ内の情報を取得できます。
        /// </summary>
        public DirectoryAnalyzeData AnalyzeData
        {
            get
            {
                return analyzeData;
            }
        }
        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="directory">対象ディレクトリ</param>
        public DirectoryAnalyzer(DirectoryInfo directory)
        {
            this.currentDirectory = directory;

            this.progressData = new ProgressForm.ProgressData();
            this.progressForm = new ProgressForm(GetProgressValue);
        }
        #endregion

        #region リスト操作処理
        /// <summary>
        /// ディレクトリ内の情報を取得します。
        /// </summary>
        public void ReadDirectoryInnerInfo(IWin32Window owner)
        {
            analyzeData = null;

            Thread thread = new Thread(Execute);
            thread.Start();

            progressForm.ShowDialog(owner);
            thread.Join();
            progressForm.Dispose();
        }

        /// <summary>
        /// フォルダの情報を取得
        /// </summary>
        private void Execute()
        {
            if (currentDirectory.Exists == false)
            {
                return;
            }

            progressData.maxProgress = 1;

            DirectoryAnalyzeData.Clear();
            analyzeData = new DirectoryAnalyzeData(currentDirectory, ref progressData);

            if (progressForm.Canceled == true)
            {
                analyzeData = null;
            }

            progressData.progress = progressData.maxProgress;
        }
        #endregion

        #region 進捗状況
        /// <summary>
        /// 進捗状況を取得します。
        /// </summary>
        /// <returns>進捗状況</returns>
        private ProgressForm.ProgressData GetProgressValue()
        {
            return progressData;
        }
        #endregion
    }
}
