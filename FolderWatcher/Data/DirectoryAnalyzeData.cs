using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using Rui.FolderWatcher.Util;
using System.Windows.Forms;
using System.Security.AccessControl;
using System.Security.Principal;
using Rui.FolderWatcher.Util.Parser;
using System.Reflection;
using System.Security.Cryptography;

namespace Rui.FolderWatcher.Data
{
    public class DirectoryAnalyzeData
    {
        #region 列挙体
        /// <summary>
        /// ファイルの種類
        /// </summary>
        public enum FileTypeKind {
            /// <summary>
            /// ファイル
            /// </summary>
            File,

            /// <summary>
            /// ディレクトリ
            /// </summary>
            Directory,
        }
        #endregion

        #region メンバ変数
        /// <summary>
        /// ルートディレクトリデータ
        /// </summary>
        private DirectoryAnalyzeData rootData;

        /// <summary>
        /// ファイルパス
        /// </summary>
        private string path;

        /// <summary>
        /// 名前
        /// </summary>
        private string name;

        /// <summary>
        /// 拡張子
        /// </summary>
        private string extention;

        /// <summary>
        /// ファイル作成日時
        /// </summary>
        private DateTime creationTime;

        /// <summary>
        /// 最終更新日時
        /// </summary>
        private DateTime lastModifyDate;

        /// <summary>
        /// アクセス日時
        /// </summary>
        private DateTime lastAccessTime;

        /// <summary>
        /// サイズ
        /// </summary>
        private long size;

        /// <summary>
        /// 読み取り専用
        /// </summary>
        private bool isReadOnly;

        /// <summary>
        /// 隠しファイル
        /// </summary>
        private bool isHidden;

        /// <summary>
        /// アクセス拒否
        /// </summary>
        private bool isAccessDeny = false;

        /// <summary>
        /// ファイル
        /// </summary>
        private IList<DirectoryAnalyzeData> files;

        /// <summary>
        /// フォルダ
        /// </summary>
        private IList<DirectoryAnalyzeData> directories;

        /// <summary>
        /// 所有者
        /// </summary>
        private string owerName;

        /// <summary>
        /// ファイルに関連づけられたデータ
        /// </summary>
        private FileAttachData attachData;

        /// <summary>
        /// 検索
        /// </summary>
        private static FileSearchData search;

        /// <summary>
        /// 処理の中断
        /// </summary>
        private static bool isCancel = false;

        /// <summary>
        /// ファイルタイプ
        /// </summary>
        private FileTypeKind fileType;

        /// <summary>
        /// ハッシュの種類
        /// </summary>
        private static FolderWatcherSetting.HashKind hashKind;

        /// <summary>
        /// 相対パス表示
        /// </summary>
        private static bool isRelativePath;
        #endregion

        #region プロパティ
        /// <summary>
        /// 名前を取得することができます。
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// パスを取得することができます。
        /// </summary>
        public string DirectoryName
        {
            get
            {
                string parentDirPath = Path.GetDirectoryName(path);

                if (isRelativePath == false || this == rootData)
                {
                    return parentDirPath;
                }
                else
                {
                    string rootPath = rootData.FullName;
                    if (parentDirPath.StartsWith(rootPath) == false)
                    {
                        return parentDirPath;
                    }

                    parentDirPath = parentDirPath.Substring(rootPath.Length);
                    if (string.IsNullOrEmpty(parentDirPath) == true)
                    {
                        parentDirPath = Path.DirectorySeparatorChar.ToString();
                    }

                    return "." + parentDirPath;
                }
            }
        }

        /// <summary>
        /// フルパスを取得することが出来ます。
        /// </summary>
        public string FullName
        {
            get
            {
                return path;
            }
        }

        /// <summary>
        /// 拡張子を取得することができます。
        /// </summary>
        public string Extention
        {
            get
            {
                return extention;
            }
        }

        /// <summary>
        /// ファイル作成日時を取得することができます。
        /// </summary>
        public DateTime CreationDate
        {
            get
            {
                return creationTime;
            }
        }

        /// <summary>
        /// 最終更新日時を取得することができます。
        /// </summary>
        public DateTime LastModifyDate
        {
            get
            {
                return lastModifyDate;
            }
        }

        /// <summary>
        /// 最終アクセス日時を取得することができます。
        /// </summary>
        public DateTime LastAccessDate
        {
            get
            {
                return lastAccessTime;
            }
        }

        /// <summary>
        /// 読み取り専用か取得することが出来ます。
        /// </summary>
        public bool ReadOnly
        {
            get
            {
                return isReadOnly;
            }
        }

        /// <summary>
        /// 隠しファイルか取得することが出来ます。
        /// </summary>
        public bool Hidden
        {
            get
            {
                return isHidden;
            }
        }

        /// <summary>
        /// アクセス拒否か取得することが出来ます。
        /// </summary>
        public bool AccessDeny
        {
            get
            {
                return isAccessDeny;
            }
        }

        /// <summary>
        /// サイズを取得することができます。
        /// </summary>
        public long Size
        {
            get
            {
                long totalSize = this.size;
                // 内部ファイルのサイズ
                foreach (DirectoryAnalyzeData file in FileData)
                {
                    totalSize += file.Size;
                }

                // サブフォルダのサイズ
                foreach (DirectoryAnalyzeData directory in SubDirectoryData)
                {
                    totalSize += directory.Size;
                }

                return totalSize;
            }
        }

        /// <summary>
        /// サイズを取得することができます。
        /// </summary>
        public string DataSize
        {
            get
            {
                return Util.DataSize.GetRegularSize(Size);
            }
        }

        /// <summary>
        /// ファイル数をを取得することができます。
        /// </summary>
        public long FileCount
        {
            get
            {
                long fileCount = 0; 
                // 内部ファイルのファイル数
                foreach (DirectoryAnalyzeData file in FileData)
                {
                    fileCount += file.FileCount;
                }

                // サブフォルダのファイル数
                foreach (DirectoryAnalyzeData directory in SubDirectoryData)
                {
                    fileCount += directory.FileCount;
                }

                if (FileType == FileTypeKind.File)
                {
                    fileCount++;
                }

                return fileCount;
            }
        }

        /// <summary>
        /// 内部に存在するファイル情報を取得します。
        /// </summary>
        public IList<DirectoryAnalyzeData> FileData
        {
            get
            {
                IList<DirectoryAnalyzeData> targetFiles = new List<DirectoryAnalyzeData>();

                foreach (DirectoryAnalyzeData fileData in files)
                {
                    if (fileData.Target == true)
                    {
                        targetFiles.Add(fileData);
                    }
                }

                return targetFiles;
            }
        }

        /// <summary>
        /// ディレクトリ数を取得することができます。
        /// </summary>
        public long DirectoryCount
        {
            get
            {
                long directoryCount = 0;
                foreach (DirectoryAnalyzeData directory in SubDirectoryData)
                {
                    directoryCount += directory.DirectoryCount;
                }

                if (FileType == FileTypeKind.Directory)
                {
                    directoryCount++;
                }

                return directoryCount;
            }
        }

        /// <summary>
        /// 内部に存在するディレクトリ情報を取得します。
        /// </summary>
        public IList<DirectoryAnalyzeData> SubDirectoryData
        {
            get
            {
                IList<DirectoryAnalyzeData> targetFiles = new List<DirectoryAnalyzeData>();

                foreach (DirectoryAnalyzeData fileData in directories)
                {
                    if (fileData.Target == true || 1 < fileData.DirectoryCount )
                    {
                        targetFiles.Add(fileData);
                    }
                }

                return targetFiles;
            }
        }

        /// <summary>
        /// 小アイコンを取得することができます。
        /// </summary>
        public Image SmallIcon
        {
            get
            {
                if (attachData == null)
                {
                    return Properties.Resources.AccessDeny.ToBitmap();
                }

                return attachData.SmallIcon;
            }
        }

        /// <summary>
        /// 大アイコンを取得することができます。
        /// </summary>
        public Image LargeIcon
        {
            get
            {
                if (attachData == null)
                {
                    return Properties.Resources.AccessDeny.ToBitmap();
                }

                return attachData.LargeIcon;
            }
        }

        /// <summary>
        /// ファイルの種類を取得することができます。
        /// </summary>
        public string Kind
        {
            get {
                if (attachData == null)
                {
                    return Properties.Resources.UnKown;
                }

                return attachData.TypeName;
            }
        }

        /// <summary>
        /// 所有者を取得することができます。
        /// </summary>
        public string Owner
        {
            get
            {
                return owerName;
            }
        }

        /// <summary>
        /// ファイルタイプを取得することができます。
        /// </summary>
        public FileTypeKind FileType
        {
            get
            {
                return fileType;
            }
        }

        /// <summary>
        /// 検索方法を設定、取得します。
        /// </summary>
        public static FileSearchData Search
        {
            set
            {
                search = value;
            }
            get
            {
                return search;
            }
        }

        /// <summary>
        /// ハッシュの種類を設定、取得します。
        /// </summary>
        public static FolderWatcherSetting.HashKind HashType
        {
            set
            {
                hashKind = value;
            }
            get
            {
                return hashKind;
            }
        }

        /// <summary>
        /// ハッシュ値を取得します。
        /// </summary>
        public string Hash
        {
            get
            {
                if (fileType != FileTypeKind.File || hashKind == FolderWatcherSetting.HashKind.None)
                {
                    return string.Empty;
                }

                //MD5CryptoServiceProviderオブジェクトを作成
                HashAlgorithm hashAlgorithm = null;
                if (hashKind == FolderWatcherSetting.HashKind.MD5)
                {
                    hashAlgorithm = new MD5CryptoServiceProvider();
                }
                else if (hashKind == FolderWatcherSetting.HashKind.SHA1)
                {
                    hashAlgorithm = new SHA1CryptoServiceProvider();
                }


                //ハッシュ値を計算する
                byte[] bs = hashAlgorithm.ComputeHash(new FileInfo(this.path).OpenRead());

                //byte型配列を16進数の文字列に変換
                System.Text.StringBuilder result = new System.Text.StringBuilder();
                foreach (byte b in bs)
                {
                    result.Append(b.ToString("x2"));
                }

                //結果を表示
                return result.ToString();
            }
        }

        /// <summary>
        /// 相対パス表示かを設定、取得することができます。
        /// </summary>
        public static bool RelativePath
        {
            set
            {
                isRelativePath = value;
            }
            get
            {
                return isRelativePath;
            }
        }

        /// <summary>
        /// 処理対象のファイルか取得します。
        /// </summary>
        protected bool Target
        {
            get
            {
                if (Search == null)
                {
                    return true;
                }

                return Search.Match(Name, FileType);
            }
        }
        #endregion

        #region 初期化
                /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="path">ファイルパス</param>
        /// <param name="progressData">進捗データ</param>
        public DirectoryAnalyzeData(FileSystemInfo file, ref ProgressForm.ProgressData progressData) : this(file, ref progressData, null)
        {
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="file">解析ファイル</param>
        /// <param name="progressData">進捗データ</param>
        /// <param name="rootData">大本の親フォルダの解析データ</param>
        public DirectoryAnalyzeData(FileSystemInfo file, ref ProgressForm.ProgressData progressData, DirectoryAnalyzeData rootData)
        {
            if (isCancel == true)
            {
                return;
            }

            this.rootData = rootData;
            if (rootData == null)
            {
                this.rootData = this;
            }

            FileAttributes attribute = file.Attributes;
            if ((attribute & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint)
            {
                // リパースポイントの場合は無視
                return;
            }

            FileSystemSecurity fileSecurity = null;
            // ファイルタイプの判別
            try
            {
                this.path = file.FullName;

                if ((attribute & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    this.fileType = FileTypeKind.Directory;

                    fileSecurity = ((DirectoryInfo)file).GetAccessControl();
                }
                else
                {
                    this.fileType = FileTypeKind.File;
                    this.size = ((FileInfo)file).Length;

                    fileSecurity = ((FileInfo)file).GetAccessControl();
                }
            }
            catch
            {
                // アクセス拒否の場合
                isAccessDeny = true;
            }

            try
            {
                this.owerName = string.Empty;
                if (fileSecurity != null)
                {
                    NTAccount account = (NTAccount)fileSecurity.GetOwner(typeof(NTAccount));
                    this.owerName = account.ToString();
                }
            }
            catch
            {
            }

            this.name = file.Name;
            this.extention = Path.GetExtension(this.path).ToLower();
            this.creationTime = file.CreationTime;
            this.lastModifyDate = file.LastWriteTime;
            this.lastAccessTime = file.LastAccessTime;
            this.isReadOnly = (attribute & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
            this.isHidden = (attribute & FileAttributes.Hidden) == FileAttributes.Hidden;

            this.files = new List<DirectoryAnalyzeData>();
            this.directories = new List<DirectoryAnalyzeData>();

            progressData.currentFileName = this.name;
            progressData.targetFolderName = Path.GetDirectoryName(path);
            progressData.progress++;

            if (this.FileType == FileTypeKind.Directory)
            {
                // ディレクトリの場合
                // 中のファイルの情報を取得
                try {
                    progressData.maxProgress += ((DirectoryInfo)file).GetFileSystemInfos().Length;

                    foreach (DirectoryInfo subDir in ((DirectoryInfo)file).GetDirectories())
                    {
                        DirectoryAnalyzeData data = new DirectoryAnalyzeData(subDir, ref progressData, this.rootData);

                        if (string.IsNullOrEmpty(data.Name) == false)
                        {
                            directories.Add(data);
                        }
                    }

                    foreach (FileInfo subFile in ((DirectoryInfo)file).GetFiles())
                    {
                        DirectoryAnalyzeData data = new DirectoryAnalyzeData(subFile, ref progressData, this.rootData);

                        if (string.IsNullOrEmpty(data.Name) == false)
                        {
                            files.Add(data);
                        }
                    }
                }
                catch
                {
                    // アクセス拒否の場合
                    isAccessDeny = true;
                }
            }

            this.attachData = new FileAttachData(path, isHidden, isAccessDeny);
        }

        /// <summary>
        /// データのクリア
        /// </summary>
        public static void Clear()
        {
            isCancel = false;
        }
        #endregion

        #region 中断処理
        /// <summary>
        /// 解析処理を中断します。
        /// </summary>
        public static void Cancel()
        {
            isCancel = true;
        }
        #endregion

        #region 変換
        /// <summary>
        /// 指定した形式に変換します。
        /// </summary>
        /// <param name="parser"></param>
        /// <returns></returns>
        public string[] Parse(IParser parser)
        {
            return Parse(parser, false);
        }

        /// <summary>
        /// 指定した形式に変換します。
        /// </summary>
        /// <param name="parser"></param>
        /// <returns></returns>
        public string[] Parse(IParser parser, bool isAllFiles)
        {
            List<string> parseData = new List<string>();

            parseData.AddRange(parser.Parse(this, isAllFiles));

            return parseData.ToArray();
        }

        /// <summary>
        /// 指定された項目のデータリストを取得します。
        /// </summary>
        /// <param name="itemKinds"></param>
        /// <returns></returns>
        public List<string> ToArray(FolderWatcherSetting.FileItemKind[] itemKinds)
        {
            List<String> itemDatas = new List<string>();

            Type typeFileInfo = this.GetType();
            foreach (FolderWatcherSetting.FileItemKind itemKind in itemKinds)
            {
                PropertyInfo pi = typeFileInfo.GetProperty(itemKind.ToString());
                if (pi != null)
                {
                    // プロパティのゲッターの呼び出し
                    // 以下は「newct = fi.CreationTimeUtc;」と同じ意味
                    object propertyData = pi.GetValue(this, null);

                    if (propertyData == null)
                    {
                        itemDatas.Add(string.Empty);
                    }
                    else
                    {
                        itemDatas.Add(propertyData.ToString());
                    }
                }
            }

            return itemDatas;
        }
        #endregion

        #region ファイルプロパティ
        /// <summary>
        /// ファイルのプロパティを表示します。
        /// </summary>
        /// <param name="parent">親フォーム</param>
        /// <returns></returns>
        public bool ShowFilePropertiesDialog(Form parent)
        {
            return attachData.ShowFilePropertiesDialog(parent);
        }
        #endregion
    }
}
