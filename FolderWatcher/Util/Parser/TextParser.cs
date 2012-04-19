using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Rui.FolderWatcher.Data;

namespace Rui.FolderWatcher.Util.Parser
{
    public class TextParser : IParser
    {
        #region 定数
        /// <summary>
        /// 拡張子
        /// </summary>
        public static readonly string[] Extentions = { ".txt" };

        /// <summary>
        /// フィルタ
        /// </summary>
        public const string Filter = "Text File(*.txt)|*.txt";
        #endregion

        #region ヘッダーフッター
        /// <summary>
        /// ヘッダーデータを取得します。
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public string GetHeader(DirectoryAnalyzeData srcData)
        {
            return srcData.FullName;
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

            if (isAllData == true)
            {
                // 内部ファイル
                List<DirectoryAnalyzeData> subFileList = new List<DirectoryAnalyzeData>();
                subFileList.AddRange(srcData.FileData);
                subFileList.AddRange(srcData.SubDirectoryData);
                for (int subIndex = 0; subIndex < subFileList.Count; subIndex++)
                {
                    string line = string.Empty;
                    if (subIndex < subFileList.Count -1)
                    {
                        line = " |-";
                    }
                    else
                    {
                        line = " L ";
                    }
                    parseData.Add(line + Parse(subFileList[subIndex], false)[0]);

                    string[] subFileData = Parse(subFileList[subIndex], true);
                    foreach (string subData in subFileData)
                    {
                        line = "   ";
                        if (subIndex < subFileList.Count - 1)
                        {
                            line = " | ";
                        }
                        parseData.Add(line + subData);
                    }
                }
            }
            else
            {
                CsvParser csvParser = new CsvParser('\t');
                parseData.AddRange(csvParser.Parse(srcData, false));
            }

            return parseData.ToArray();
        }
        #endregion
    }
}
