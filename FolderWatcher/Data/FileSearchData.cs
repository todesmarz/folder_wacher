using System;
using System.Text;

namespace Rui.FolderWatcher.Data
{
    public class FileSearchData
    {
        #region 定数
        /// <summary>
        /// 複数文字マッチ文字
        /// </summary>
        private const char ASTERISK = '*';

        /// <summary>
        /// 一文字マッチ文字
        /// </summary>
        private const char QUESTION = '?';
        #endregion

        #region 対象の列挙体
        /// <summary>
        /// 対象の種類
        /// </summary>
        public enum TargetKind
        {
            /// <summary>
            /// すべて
            /// </summary>
            All,

            /// <summary>
            /// ファイル
            /// </summary>
            File,

            /// <summary>
            /// フォルダ
            /// </summary>
            Folder,
        }
        #endregion

        #region メンバ変数
        /// <summary>
        /// 検索文字
        /// </summary>
        private String searchWord;

        /// <summary>
        /// 大文字、小文字を区別する
        /// </summary>
        private bool ignoreCase;

        /// <summary>
        /// 曖昧検索
        /// </summary>
        private bool vague;

        /// <summary>
        /// 検索対象
        /// </summary>
        private TargetKind targetKind;
        #endregion

        #region プロパティ
        /// <summary>
        /// 検索文字を取得します。
        /// </summary>
        public string SearchWord
        {
            get
            {
                return searchWord;
            }
        }

        /// <summary>
        /// 大文字、小文字を区別するを取得します。
        /// </summary>
        public bool IgnoreCase
        {
            get
            {
                return ignoreCase;
            }
        }

        /// <summary>
        /// 曖昧検索を行うか取得します。
        /// </summary>
        public bool Vague
        {
            get
            {
                return vague;
            }
        }

        /// <summary>
        /// 検索対象を取得します。
        /// </summary>
        public TargetKind Target
        {
            get
            {
                return targetKind;
            }
        }
        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="searchWord">検索文字</param>
        /// <param name="ignoreCase">大文字小文字を区別するか</param>
        /// <param name="vague">曖昧検索を行うか</param>
        public FileSearchData(String searchWord, bool ignoreCase, bool vague, TargetKind targetKind)
        {
            this.searchWord = searchWord;
            this.ignoreCase = ignoreCase;
            this.vague = vague;
            this.targetKind = targetKind;
        }
        #endregion

        #region マッチ
        /// <summary>
        /// 検索対象とマッチするか判断します。
        /// </summary>
        /// <param name="targetWord">調べる文字</param>
        /// <returns>マッチした場合は true、それ以外の場合は false</returns>
        public bool Match(String targetWord)
        {
            // 検索文字が空文字の場合はtrue
            if (String.IsNullOrEmpty(searchWord) == true)
            {
                return true;
            }

            String tempSearchWord = searchWord;
            String tmpTragetWord = targetWord;
            // 曖昧検索の場合は前後に*を付加
            if (vague == true)
            {
                tempSearchWord = ASTERISK + tempSearchWord + ASTERISK;
            }

            // 大文字、小文字を区別しない場合は小文字に変換
            if (ignoreCase == false)
            {
                tmpTragetWord = tmpTragetWord.ToLower();
            }

            return Match(tempSearchWord, RegularWord(tmpTragetWord));
        }

        /// <summary>
        /// 検索対象とマッチするか判断します。
        /// </summary>
        /// <param name="targetWord">調べる文字</param>
        /// <returns>マッチした場合は true、それ以外の場合は false</returns>
        public bool Match(String targetWord, DirectoryAnalyzeData.FileTypeKind fileType)
        {
            if (targetKind != TargetKind.All)
            {
                if (fileType == DirectoryAnalyzeData.FileTypeKind.File && targetKind != TargetKind.File)
                {
                    return true;
                }
                else if (fileType == DirectoryAnalyzeData.FileTypeKind.Directory && targetKind != TargetKind.Folder)
                {
                    return true;
                }
            }

            return Match(targetWord);
        }

        /// <summary>
        /// 検索対象とマッチするか判断します。
        /// </summary>
        /// <param name="searchWord">検索文字</param>
        /// <param name="targetWord">調べる文字</param>
        /// <returns>マッチした場合は true、それ以外の場合は false</returns>
        private static bool Match(String searchWord, String targetWord)
        {
            if (String.IsNullOrEmpty(searchWord) == true)
            {
                return String.IsNullOrEmpty(targetWord);
            }

            char searchChar = searchWord[0];
            if (searchChar == ASTERISK)
            {
                return Match(searchWord.Substring(1), targetWord) || 
                    String.IsNullOrEmpty(targetWord) == false && 
                    Match(searchWord, targetWord.Substring(1));
            }
            else if (searchChar == QUESTION)
            {
                return String.IsNullOrEmpty(targetWord) == false && Match(searchWord.Substring(1), targetWord.Substring(1));
            }
            else if (String.IsNullOrEmpty(targetWord) == true)
            {
                return false;
            }

            char targetChar = targetWord[0];
            return searchChar == targetChar && Match(searchWord.Substring(1), targetWord.Substring(1));
        }
        #endregion

        #region 検索文字の正規化
        /// <summary>
        /// 連続した*を一つにまとめます。
        /// </summary>
        /// <param name="word">対象文字</param>
        /// <returns>正規化した文字</returns>
        public static String RegularWord(String word)
        {
            StringBuilder result = new StringBuilder();
            bool isPreAsterisk = false;

            // 正規化した文字列の作成
            for (int wordIndex = 0; wordIndex < word.Length; wordIndex++)
            {
                bool isAsterisk = ASTERISK.Equals(word[wordIndex]);
                if (isPreAsterisk == true && isAsterisk == true)
                {
                    continue;
                }

                result.Append(word[wordIndex]);
                isPreAsterisk = isAsterisk;
            }

            return result.ToString();
        }
        #endregion
    }
}
