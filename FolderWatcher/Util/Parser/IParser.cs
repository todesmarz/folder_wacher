using System;
using System.Collections.Generic;
using System.Text;
using Rui.FolderWatcher.Data;

namespace Rui.FolderWatcher.Util.Parser
{
    public interface IParser
    {
        /// <summary>
        /// 文字列配列を変換します、
        /// </summary>
        /// <param name="srcData">変換前文字列配列</param>
        /// <param name="isAllData">内部のファイルも変換対象</param>
        /// <returns></returns>
        string[] Parse(DirectoryAnalyzeData srcData, bool isAllData);

        string GetHeader(DirectoryAnalyzeData srcData);

        string GetFooter(DirectoryAnalyzeData srcData);
    }
}
