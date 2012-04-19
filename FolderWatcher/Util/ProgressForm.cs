using System;
using System.Windows.Forms;
using Rui.FolderWatcher.Data;
using System.Text;

namespace Rui.FolderWatcher.Util
{
    public partial class ProgressForm : Form
    {
        #region デリゲート
        /// <summary>
        /// 進捗値取得デリゲート
        /// </summary>
        public delegate ProgressData GetProggressValue();
        #endregion

        #region デリゲート用構造体
        /// <summary>
        /// プログレスバーに表示するデータ
        /// </summary>
        public struct ProgressData
        {
            #region メンバー変数
            /// <summary>
            /// 進捗状況
            /// </summary>
            public int progress;

            /// <summary>
            /// 進捗終了値
            /// </summary>
            public int maxProgress;

            /// <summary>
            /// 対象のフォルダ名
            /// </summary>
            public string targetFolderName;

            /// <summary>
            /// 処理中のファイル名
            /// </summary>
            public string currentFileName;
            #endregion

            #region 比較
            /// <summary>
            /// 比較
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public override bool Equals(object obj)
            {
                if (base.Equals(obj) == true)
                {
                    return true;
                }

                if (obj is ProgressData == false)
                {
                    return false;
                }

                ProgressData data = (ProgressData)obj;
                if (progress != data.progress)
                {
                    return false;
                }

                if (maxProgress != data.maxProgress)
                {
                    return false;
                }

                if (targetFolderName.Equals(data.targetFolderName) == false)
                {
                    return false;
                }

                if (currentFileName.Equals(data.currentFileName) == false)
                {
                    return false;
                }

                return true;
            }

            /// <summary>
            /// ハッシュコードの取得
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                int hashCode = 17;
                hashCode = hashCode * 31 + progress;
                hashCode = hashCode * 31 + maxProgress;
                hashCode = hashCode * 31 + targetFolderName.GetHashCode();
                hashCode = hashCode * 31 + currentFileName.GetHashCode();

                return hashCode;
            }
            #endregion
        }
        #endregion

        #region メンバー変数
        /// <summary>
        /// 最大値
        /// </summary>
        private int maxValue = 100;

        /// <summary>
        /// 進捗値取得デリゲート
        /// </summary>
        private GetProggressValue GetProgressValue;

        /// <summary>
        /// 処理時間
        /// </summary>
        private int transactionTime;

        /// <summary>
        /// 前回の処理データ
        /// </summary>
        private ProgressData preProgressData;

        /// <summary>
        /// キャンセルされたか判断
        /// </summary>
        private bool isCancel;
        #endregion

        #region プロパティ
        /// <summary>
        /// 最大値を取得、設定をすることができます。
        /// </summary>
        public int MaxValue
        {
            set
            {
                if (value > 0)
                {
                    maxValue = value;
                }
            }

            get
            {
                return maxValue;
            }
        }

        /// <summary>
        /// キャンセルされたか取得することが出来ます。
        /// </summary>
        public bool Canceled
        {
            get
            {
                return isCancel;
            }
        }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="getProgressValue">進捗値取得デリゲート</param>
        public ProgressForm(GetProggressValue getProgressValue)
        {
            InitializeComponent();

            // nullは許さない
            if (getProgressValue == null)
            {
                throw new NullReferenceException();
            }
            this.GetProgressValue = getProgressValue;

            FolderWatcherSetting setting = FolderWatcherSetting.Instance;
            this.Opacity = 1 - setting.Opacity * 0.05;
        }
        #endregion

        #region 進捗値の設定
        /// <summary>
        /// 進捗値の設定
        /// </summary>
        private void SetProgress()
        {
            ProgressData data = GetProgressValue();
            int progress = data.progress;

            transactionTime += updateTimer.Interval;

            if (0 < progress)
            {
                double remaining = (data.maxProgress - progress) / (double) progress;
                TimeSpan restTransactionTime = TimeSpan.FromMilliseconds(transactionTime * remaining);
                string restTransValue = GetTimeSpanToFormat(restTransactionTime);

                this.Text = string.Format(Properties.Resources.RestTimeTitle, restTransValue);
                restTimeLabel.Text = restTransValue;
            }

            if (preProgressData.Equals(data) == false)
            {
                progressBar.Value = (progress * 100) / data.maxProgress;

                targetFolderLabel.Text = string.Format(Properties.Resources.FolderInfoMessage, data.maxProgress);
                targetLabel.Text = data.targetFolderName;
                currentLabel.Text = data.currentFileName;

                if (progress == data.maxProgress)
                {
                    this.Close();
                }

                preProgressData = data;
            }
        }
        #endregion

        /// <summary>
        /// タイムスパンからフォーマットした文字列を取得
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        private String GetTimeSpanToFormat(TimeSpan timeSpan)
        {
            if (timeSpan.Equals(TimeSpan.Zero) == true)
            {
                return Properties.Resources.UnKown;
            }

            StringBuilder result = new StringBuilder();
            result.Append(Properties.Resources.AlsoTime);

            // 時間
            if (timeSpan.Hours > 0)
            {
                timeSpan.Add(new TimeSpan(0, 60 - timeSpan.Minutes, 60 - timeSpan.Seconds));

                result.AppendFormat(Properties.Resources.HoursFormat, timeSpan.Hours);
            }

            // 分
            if (timeSpan.Minutes > 0)
            {
                timeSpan.Add(new TimeSpan(0, 0, 60 - timeSpan.Seconds));

                result.AppendFormat(Properties.Resources.MinutesFormat, timeSpan.Minutes);
            }            

            // 秒
            if (timeSpan.Seconds > 0)
            {
                int second = (int) (Math.Floor(timeSpan.Seconds % 5.0) * 5);
                result.AppendFormat(Properties.Resources.SecondsFormat, second);
            }

            // ミリ秒
            if (timeSpan.Seconds == 0 && timeSpan.Milliseconds >= 0)
            {
                int second = timeSpan.Milliseconds / 200;
                result.AppendFormat(Properties.Resources.SecondsFormat, second);
            }

            return result.ToString();
        }

        #region タイマー処理
        /// <summary>
        /// タイマーイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateTimer_Tick(object sender, EventArgs e)
        {
            SetProgress();
        }
        #endregion

        #region ボタンイベント
        /// <summary>
        /// キャンセルボタン押下イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control != null)
            {
                control.Enabled = false;
            }

            isCancel = true;
            DirectoryAnalyzeData.Cancel();
        }
        #endregion

        #region フォームイベント
        /// <summary>
        /// フォームロードイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressForm_Load(object sender, EventArgs e)
        {
            this.isCancel = false;
            this.transactionTime = 0;
            this.targetFolderLabel.Text = string.Empty;
            this.targetLabel.Text = string.Empty;
            this.restTimeLabel.Text = string.Empty;

            this.preProgressData.currentFileName = string.Empty;
            this.preProgressData.targetFolderName = string.Empty;
            this.preProgressData.progress = 0;

            this.updateTimer.Enabled = true;
        }
        #endregion
    }
}