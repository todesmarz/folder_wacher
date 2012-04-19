using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Rui.FolderWatcher.Data;

namespace Rui.FolderWatcher.Util.Parser
{
    public class CsvParser : IParser
    {
        #region 定数
        /// <summary>
        /// 区切り文字
        /// </summary>
        private const char DefaultDelimiter = ',';

        /// <summary>
        /// エスケープ文字
        /// </summary>
        private const char Escape = '\\';

        /// <summary>
        /// ダブルクォート
        /// </summary>
        private const char DoubleQuart = '\"';

        /// <summary>
        /// 拡張子
        /// </summary>
        public static readonly string[] Extentions = { ".csv" };

        /// <summary>
        /// フィルタ
        /// </summary>
        public const string Filter = "Comma Separated Values(*.csv)|*.csv";
        #endregion

        #region メンバー変数
        /// <summary>
        /// 区切り文字
        /// </summary>
        private char separeter;
        #endregion

        #region 初期化
                /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="separeter">区切り文字</param>
        public CsvParser() : this(DefaultDelimiter)
        {

        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="separeter">区切り文字</param>
        public CsvParser(char separeter)
        {
            this.separeter = separeter;
        }
        #endregion

        #region ヘッダーフッター
        /// <summary>
        /// ヘッダーデータを取得します。
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public string GetHeader(DirectoryAnalyzeData srcData)
        {
            return string.Empty;
        }

        /// <summary>
        /// フッターデータを取得します。
        /// </summary>
        /// <returns></returns>
        public string GetFooter(DirectoryAnalyzeData srcData)
        {
            return string.Empty;
        }
        #endregion


        #region 変換
        /// <summary>
        /// 文字列配列のデータをCSV形式に変換を行います。
        /// </summary>
        /// <param name="srcData">変換元のデータ</param>
        public string[] Parse(DirectoryAnalyzeData srcData, bool isAllData)
        {
            List<string> parseData = new List<string>();

            parseData.Add(Parse(srcData.ToArray(FolderWatcherSetting.Instance.FileItemList)));

            if (isAllData == true)
            {
                foreach (DirectoryAnalyzeData data in srcData.SubDirectoryData)
                {
                    parseData.AddRange(Parse(data, isAllData));
                }

                foreach (DirectoryAnalyzeData data in srcData.FileData)
                {
                    parseData.AddRange(Parse(data, isAllData));
                }
            }

            return parseData.ToArray();
        }

        /// <summary>
        /// 文字列配列のデータをCSV形式に変換を行います。
        /// </summary>
        /// <param name="srcData">変換元のデータ</param>
        public string Parse(List<string> srcData)
        {
            string replaceWord = DoubleQuart.ToString() + DoubleQuart;
            StringBuilder csvData = new StringBuilder();
            foreach (string data in srcData)
            {
                if (0 <= data.IndexOf(separeter))
                {
                    csvData.Append(DoubleQuart);
                    string value = data;
                    if (0 <= data.IndexOf(separeter))
                    {
                        value = value.Replace(DoubleQuart.ToString(), replaceWord);
                    }
                    csvData.Append(value);

                    csvData.Append(DoubleQuart);
                }
                else
                {
                    csvData.Append(data);
                }

                csvData.Append(separeter);
            }

            csvData.Remove(csvData.Length - 1, 1);

            return csvData.ToString();
        }
        #endregion
    }
}
