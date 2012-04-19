using System;
using System.Collections.Generic;
using System.Text;
using Rui.FolderWatcher.Data;
using System.IO;

namespace Rui.FolderWatcher.Util.Parser
{
    public class HtmlParser :IParser
    {
        #region 定数
        /// <summary>
        /// 拡張子
        /// </summary>
        public static readonly string[] Extentions = { ".html", ".htm" };

        /// <summary>
        /// フィルタ
        /// </summary>
        public const string Filter = "Hyper Text Markup Language (*.html,*.htm)|*.html;*.htm";

        #endregion

        #region ヘッダーフッター
        /// <summary>
        /// ヘッダーデータを取得します。
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public string GetHeader(DirectoryAnalyzeData srcData)
        {
            StringBuilder header = new StringBuilder();
            header.Append("<html>");
            header.Append(Environment.NewLine);
            header.Append("<head>");
            header.Append(Environment.NewLine);
            header.Append("<title>");
            header.Append(srcData.Name);
            header.Append("</title>");
            header.Append(Environment.NewLine);
            header.Append("</head>");
            header.Append(Environment.NewLine);
            header.Append("<body>");
            header.Append(Environment.NewLine);

            return header.ToString();
        }

        /// <summary>
        /// フッターデータを取得します。
        /// </summary>
        /// <returns></returns>
        public string GetFooter(DirectoryAnalyzeData srcData)
        {
            StringBuilder header = new StringBuilder();
            header.Append("</body>");
            header.Append(Environment.NewLine);
            header.Append("</html>");
            header.Append(Environment.NewLine);

            return header.ToString();
        }
        #endregion

        #region 変換
        /// <summary>
        /// HTMLへ変換
        /// </summary>
        /// <param name="srcData">変換元データ</param>
        /// <returns></returns>
        public string[] Parse(DirectoryAnalyzeData srcData, bool isAllData)
        {
            List<string> parseData = new List<string>();

            if (isAllData == true)
            {
                string path = srcData.FullName;
                parseData.Add("<a name='" + path.Replace('\\', '/') + "'>");
                parseData.Add(path);
                parseData.Add("</a>");
                parseData.Add("<table border='1px'>");

                List<string> header = new List<string>();

                foreach (FolderWatcherSetting.FileItemKind fileItem in FolderWatcherSetting.Instance.FileItemList)
                {
                    header.Add(FolderWatcherSetting.GetString(fileItem));
                }
                parseData.Add(Parse(header, null, " bgcolor='#cccccc'"));

                // 内部フォルダ
                foreach (DirectoryAnalyzeData data in srcData.SubDirectoryData)
                {
                    parseData.AddRange(Parse(data, false));
                }

                // 内部ファイル
                foreach (DirectoryAnalyzeData data in srcData.FileData)
                {
                    parseData.AddRange(Parse(data, false));
                }
                parseData.Add("</table>");
                parseData.Add("<br>");

                foreach (DirectoryAnalyzeData data in srcData.SubDirectoryData)
                {
                    parseData.AddRange(Parse(data, isAllData));
                }
            }
            else
            {
                parseData.Add(Parse(srcData.ToArray(FolderWatcherSetting.Instance.FileItemList), srcData, string.Empty));
            }

            return parseData.ToArray();
        }
                /// <summary>
        /// 文字列配列のデータをCSV形式に変換を行います。
        /// </summary>
        /// <param name="srcData">変換元のデータ</param>
        /// <param name="srcAnaData">変換元データ</param>
        /// <param name="headerOption">ヘッダー行オプション</param>
        public string Parse(List<string> srcData, DirectoryAnalyzeData srcAnaData, string headerOption)
        {
            StringBuilder destData = new StringBuilder();
            destData.Append("<tr" + headerOption + ">");
            destData.Append(Environment.NewLine);
            FolderWatcherSetting.FileItemKind[] itemList = FolderWatcherSetting.Instance.FileItemList;

            for (int dataIndex = 0; dataIndex < srcData.Count; dataIndex++)
            {
                destData.Append("<td>");

                bool isDirectoryLink = itemList[dataIndex] == FolderWatcherSetting.FileItemKind.Name
                        && srcAnaData != null && srcAnaData.FileType == DirectoryAnalyzeData.FileTypeKind.Directory;
                if (isDirectoryLink == true)
                {
                    destData.Append("<a href='#" + srcAnaData.FullName.Replace('\\', '/') + "'>");
                }
                destData.Append(srcData[dataIndex]);
                if (isDirectoryLink == true)
                {
                    destData.Append("</a>");
                }
                destData.Append("</td>");
                destData.Append(Environment.NewLine);
            }

            destData.Append("</tr>");

            return destData.ToString();
        }
        #endregion
    }
}
