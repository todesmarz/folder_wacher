using System;
using Rui.FolderWatcher.Data;

namespace Rui.FolderWatcher.Util
{
    class DataSize
    {
        #region �萔
        #region �T�C�Y�P��
        private enum DataSizeUnit : long{
            /// <summary>
            /// �o�C�g
            /// </summary>
            Byte = 1,

            /// <summary>
            /// �L���o�C�g
            /// </summary>
            KB = 1024,

            /// <summary>
            /// ���K�o�C�g
            /// </summary>
            MB = 1024 * KB,

            /// <summary>
            /// �M�K�o�C�g
            /// </summary>
            GB = 1024 * MB,

            /// <summary>
            /// �e���o�C�g
            /// </summary>
            TB = 1024 * GB,

            /// <summary>
            /// �y�^�o�C�g
            /// </summary>
            PB = 1024 * TB,

            /// <summary>
            /// ���^�o�C�g
            /// </summary>
            YB = 1024 * PB
        }
        #endregion

        /// <summary>
        /// �\���t�H�[�}�b�g
        /// </summary>
        private const String SIZE_FORMAT = "{0:#,##0.00}{1}";
        #endregion

        #region �T�C�Y�P�ʂ̉��Z
        /// <summary>
        /// �K�؂ȃT�C�Y�̒P�ʂɂȂ����܂��B
        /// </summary>
        /// <param name="size">�T�C�Y</param>
        /// <returns>�K�؂ȒP�ʂ̃T�C�Y</returns>
        public static String GetRegularSize(long size)
        {
            double detailSize = size;
            FolderWatcherSetting setting = FolderWatcherSetting.Instance;

            DataSizeUnit unit = DataSizeUnit.Byte;
            if (setting.SizeUnit == FolderWatcherSetting.SizeUnitKind.Auto)
            {
                foreach (DataSizeUnit checkUnit in Enum.GetValues(typeof(DataSizeUnit))) {
                    if (size < (long)checkUnit)
                    {
                        break;
                    }
                    unit = checkUnit;
                }
            }
            else  
            {
                try
                {
                    unit = (DataSizeUnit)Enum.Parse(typeof(DataSizeUnit), setting.SizeUnit.ToString(), true);
                }
                catch
                {

                }
            }

            return String.Format(SIZE_FORMAT, detailSize / (long) unit, unit.ToString());
        }
        #endregion

        #region �ϊ�
        /// <summary>
        /// �K�؂ȃT�C�Y�����񂩂�T�C�Y�ɕϊ����܂��B
        /// </summary>
        /// <param name="regularSize"></param>
        /// <returns></returns>
        public static long ToInt64(string regularSize)
        {
            FolderWatcherSetting setting = FolderWatcherSetting.Instance;
            long unit = 1;
            if (setting.SizeUnit == FolderWatcherSetting.SizeUnitKind.Auto)
            {
                foreach (DataSizeUnit checkUnit in Enum.GetValues(typeof(DataSizeUnit)))
                {
                    string unitName = checkUnit.ToString();
                    if (regularSize.EndsWith(unitName) == true)
                    {
                        regularSize = regularSize.Replace(unitName, string.Empty);
                        unit = (long) checkUnit;
                        break;
                    }
                }
            }
            long size = 0;
            try
            {
                size = (long) (Convert.ToDouble(regularSize) * unit);
            }
            catch
            {
                return 0;
            }

            return size;
        }
        #endregion
    }
}
