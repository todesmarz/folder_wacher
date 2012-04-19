using System;
using System.Windows.Forms;
using Rui.FolderWatcher.Data;
using System.Text;

namespace Rui.FolderWatcher.Util
{
    public partial class ProgressForm : Form
    {
        #region �f���Q�[�g
        /// <summary>
        /// �i���l�擾�f���Q�[�g
        /// </summary>
        public delegate ProgressData GetProggressValue();
        #endregion

        #region �f���Q�[�g�p�\����
        /// <summary>
        /// �v���O���X�o�[�ɕ\������f�[�^
        /// </summary>
        public struct ProgressData
        {
            #region �����o�[�ϐ�
            /// <summary>
            /// �i����
            /// </summary>
            public int progress;

            /// <summary>
            /// �i���I���l
            /// </summary>
            public int maxProgress;

            /// <summary>
            /// �Ώۂ̃t�H���_��
            /// </summary>
            public string targetFolderName;

            /// <summary>
            /// �������̃t�@�C����
            /// </summary>
            public string currentFileName;
            #endregion

            #region ��r
            /// <summary>
            /// ��r
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
            /// �n�b�V���R�[�h�̎擾
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

        #region �����o�[�ϐ�
        /// <summary>
        /// �ő�l
        /// </summary>
        private int maxValue = 100;

        /// <summary>
        /// �i���l�擾�f���Q�[�g
        /// </summary>
        private GetProggressValue GetProgressValue;

        /// <summary>
        /// ��������
        /// </summary>
        private int transactionTime;

        /// <summary>
        /// �O��̏����f�[�^
        /// </summary>
        private ProgressData preProgressData;

        /// <summary>
        /// �L�����Z�����ꂽ�����f
        /// </summary>
        private bool isCancel;
        #endregion

        #region �v���p�e�B
        /// <summary>
        /// �ő�l���擾�A�ݒ�����邱�Ƃ��ł��܂��B
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
        /// �L�����Z�����ꂽ���擾���邱�Ƃ��o���܂��B
        /// </summary>
        public bool Canceled
        {
            get
            {
                return isCancel;
            }
        }
        #endregion

        #region �R���X�g���N�^
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="getProgressValue">�i���l�擾�f���Q�[�g</param>
        public ProgressForm(GetProggressValue getProgressValue)
        {
            InitializeComponent();

            // null�͋����Ȃ�
            if (getProgressValue == null)
            {
                throw new NullReferenceException();
            }
            this.GetProgressValue = getProgressValue;

            FolderWatcherSetting setting = FolderWatcherSetting.Instance;
            this.Opacity = 1 - setting.Opacity * 0.05;
        }
        #endregion

        #region �i���l�̐ݒ�
        /// <summary>
        /// �i���l�̐ݒ�
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
        /// �^�C���X�p������t�H�[�}�b�g������������擾
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

            // ����
            if (timeSpan.Hours > 0)
            {
                timeSpan.Add(new TimeSpan(0, 60 - timeSpan.Minutes, 60 - timeSpan.Seconds));

                result.AppendFormat(Properties.Resources.HoursFormat, timeSpan.Hours);
            }

            // ��
            if (timeSpan.Minutes > 0)
            {
                timeSpan.Add(new TimeSpan(0, 0, 60 - timeSpan.Seconds));

                result.AppendFormat(Properties.Resources.MinutesFormat, timeSpan.Minutes);
            }            

            // �b
            if (timeSpan.Seconds > 0)
            {
                int second = (int) (Math.Floor(timeSpan.Seconds % 5.0) * 5);
                result.AppendFormat(Properties.Resources.SecondsFormat, second);
            }

            // �~���b
            if (timeSpan.Seconds == 0 && timeSpan.Milliseconds >= 0)
            {
                int second = timeSpan.Milliseconds / 200;
                result.AppendFormat(Properties.Resources.SecondsFormat, second);
            }

            return result.ToString();
        }

        #region �^�C�}�[����
        /// <summary>
        /// �^�C�}�[�C�x���g
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateTimer_Tick(object sender, EventArgs e)
        {
            SetProgress();
        }
        #endregion

        #region �{�^���C�x���g
        /// <summary>
        /// �L�����Z���{�^�������C�x���g
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

        #region �t�H�[���C�x���g
        /// <summary>
        /// �t�H�[�����[�h�C�x���g
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