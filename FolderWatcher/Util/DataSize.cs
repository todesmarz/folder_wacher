using System;
using Rui.FolderWatcher.Data;

namespace Rui.FolderWatcher.Util
{
    class DataSize
    {
        #region 定数
        #region サイズ単位
        private enum DataSizeUnit : long{
            /// <summary>
            /// バイト
            /// </summary>
            Byte = 1,

            /// <summary>
            /// キロバイト
            /// </summary>
            KB = 1024,

            /// <summary>
            /// メガバイト
            /// </summary>
            MB = 1024 * KB,

            /// <summary>
            /// ギガバイト
            /// </summary>
            GB = 1024 * MB,

            /// <summary>
            /// テラバイト
            /// </summary>
            TB = 1024 * GB,

            /// <summary>
            /// ペタバイト
            /// </summary>
            PB = 1024 * TB,

            /// <summary>
            /// ヨタバイト
            /// </summary>
            YB = 1024 * PB
        }
        #endregion

        /// <summary>
        /// 表示フォーマット
        /// </summary>
        private const String SIZE_FORMAT = "{0:#,##0.00}{1}";
        #endregion

        #region サイズ単位の演算
        /// <summary>
        /// 適切なサイズの単位になおします。
        /// </summary>
        /// <param name="size">サイズ</param>
        /// <returns>適切な単位のサイズ</returns>
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

        #region 変換
        /// <summary>
        /// 適切なサイズ文字列からサイズに変換します。
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
