using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Collections.Generic;
using System.Drawing;
using Rui.FolderWatcher.Util.Parser;
using System.Xml.Serialization;

namespace Rui.FolderWatcher.Data
{
    [XmlRoot("Setting")]
    public class FolderWatcherSetting
    {
        #region 定数
        /// <summary>
        /// ファイル名
        /// </summary>
        private const String SETTING_FILE_NAME = "Setting.xml";
        #endregion

        #region 列挙体
        /// <summary>
        /// サイズ列挙体
        /// </summary>
        public enum SizeUnitKind
        {
            /// <summary>
            /// 自動
            /// </summary>
            Auto,

            /// <summary>
            /// バイト
            /// </summary>
            Byte,

            /// <summary>
            /// キロバイト
            /// </summary>
            Kb,

            /// <summary>
            /// メガバイト
            /// </summary>
            Mb,
        }

        /// <summary>
        /// ファイルアイテム列挙体
        /// </summary>
        public enum FileItemKind
        {
            /// <summary>
            /// ファイル名
            /// </summary>
            Name,

            /// <summary>
            /// ファイルパス
            /// </summary>
            DirectoryName,

            /// <summary>
            /// 拡張子
            /// </summary>
            Extention,

            /// <summary>
            /// 作成日時
            /// </summary>
            CreationDate,

            /// <summary>
            /// 変更日時
            /// </summary>
            LastModifyDate,

            /// <summary>
            /// アクセス日時
            /// </summary>
            LastAccessDate,

            /// <summary>
            /// 読み取り専用
            /// </summary>
            ReadOnly,

            /// <summary>
            /// 隠しファイル
            /// </summary>
            Hidden,

            /// <summary>
            /// アクセス拒否
            /// </summary>
            AccessDeny,

            /// <summary>
            /// サイズ
            /// </summary>
            DataSize,

            /// <summary>
            /// ファイル数
            /// </summary>
            FileCount,

            /// <summary>
            /// ディレクトリ数
            /// </summary>
            DirectoryCount,

            /// <summary>
            /// 種類
            /// </summary>
            Kind,

            /// <summary>
            /// 所有者
            /// </summary>
            Owner,

            /// <summary>
            /// ハッシュ値
            /// </summary>
            Hash
        }

        /// <summary>
        /// ハッシュの種類
        /// </summary>
        public enum HashKind
        {
            None,

            MD5,

            SHA1
        }
        #endregion

        #region メンバ変数
        /// <summary>
        /// 自身
        /// </summary>
        private static FolderWatcherSetting instance = null;

        /// <summary>
        /// 自動サイズ単位演算を行うか
        /// </summary>
        private SizeUnitKind sizeUnit;

        /// <summary>
        /// 設定の保存をする
        /// </summary>
        private Boolean isSaveSettingFile;

        /// <summary>
        /// フォームの表示位置
        /// </summary>
        private System.Drawing.Rectangle formRectangle;

        /// <summary>
        /// ナビゲーションペイン表示状態
        /// </summary>
        private bool isDisplayNavigatinPain;

        /// <summary>
        /// 詳細ペイン表示状態
        /// </summary>
        private bool isDisplayDetailPain;

        /// <summary>
        /// フォルダリストの表示モード
        /// </summary>
        private View folderListViewMode;

        /// <summary>
        /// 表示ファイルアイテムリスト
        /// </summary>
        private FileItemKind[] fileItemList = new FileItemKind[0];

        /// <summary>
        /// 最後に開いたフォルダ
        /// </summary>
        private string lastOpenFolder;

        /// <summary>
        /// ハッシュの種類
        /// </summary>
        private HashKind hashKind;

        /// <summary>
        /// 相対パス表示
        /// </summary>
        private bool isRelativePath;

        /// <summary>
        /// 透過値
        /// </summary>
        private int opacity;
        #endregion

        #region プロパティ
        /// <summary>
        /// 自身のインスタンスを取得することができます。
        /// </summary>
        public static FolderWatcherSetting Instance
        {
            get
            {
                

                return instance;
            }
        }

        /// <summary>
        /// 自動サイズ単位演算を行うかを設定、取得することができます。
        /// </summary>
        public SizeUnitKind SizeUnit
        {
            set
            {
                sizeUnit = value;
            }

            get
            {
                return sizeUnit;
            }
        }

        /// <summary>
        /// 設定をファイルへ保存するかを設定、取得することができます。
        /// </summary>
        public Boolean SaveSettingFile
        {
            set
            {
                isSaveSettingFile = value;
            }

            get
            {
                return isSaveSettingFile;
            }
        }

        /// <summary>
        /// フォームの位置を設定、取得することができます。
        /// </summary>
        public System.Drawing.Rectangle FromRectangle
        {
            set
            {
                formRectangle = value;
            }
            get
            {
                return formRectangle;
            }
        }

        /// <summary>
        /// 透過値を設定、取得することができます。
        /// </summary>
        public int Opacity
        {
            set
            {
                opacity = value;
            }

            get
            {
                return opacity;
            }
        }

        /// <summary>
        /// ナビゲーションペイン表示状態を設定、取得することができます。
        /// </summary>
        public bool DisplayNavigatinPain
        {
            set
            {
                isDisplayNavigatinPain = value;
            }

            get
            {
                return isDisplayNavigatinPain;
            }
        }

        /// <summary>
        /// ナビゲーションペイン表示状態を設定、取得することができます。
        /// </summary>
        public bool DisplayDetailPain
        {
            set
            {
                isDisplayDetailPain = value;
            }

            get
            {
                return isDisplayDetailPain;
            }
        }

        /// <summary>
        /// フォルダリストの表示モードを設定、取得することができます。
        /// </summary>
        public View FolderListViewMode
        {
            set
            {
                folderListViewMode = value;
            }
            get
            {
                return folderListViewMode;
            }
        }

        /// <summary>
        /// 最後に開いたフォルダを設定、取得することができます。
        /// </summary>
        public String LastOpenFolder
        {
            set
            {
                lastOpenFolder = value;
            }
            get
            {
                return lastOpenFolder;
            }
        }

        /// <summary>
        /// ファイルアイテム一覧を設定、取得することができます。
        /// </summary>
        public FileItemKind[] FileItemList
        {
            set
            {
                if (value == null)
                {
                    return;
                }
                fileItemList = value;
            }
            get
            {
                return (FileItemKind[]) fileItemList.Clone();
            }
        }

        /// <summary>
        /// ハッシュの種類を設定、取得することができます。
        /// </summary>
        public HashKind Hash
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
        /// 相対パス表示かを設定、取得することができます。
        /// </summary>
        public bool RelativePath
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
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        static FolderWatcherSetting()
        {
            // Utiltyクラスのためコンストラクタは private
            try
            {
                instance = FolderWatcherSetting.Load();
            }
            catch
            {

            }
        }

        public FolderWatcherSetting()
        {
            sizeUnit = SizeUnitKind.Auto;

            isSaveSettingFile = false;

            fileItemList = new FileItemKind[6];
            fileItemList[0] = FileItemKind.Name;
            fileItemList[1] = FileItemKind.LastModifyDate;
            fileItemList[2] = FileItemKind.DataSize;
            fileItemList[3] = FileItemKind.Kind;
            fileItemList[4] = FileItemKind.FileCount;
            fileItemList[5] = FileItemKind.DirectoryCount;

            folderListViewMode = View.Details;

            isDisplayDetailPain = true;

            isDisplayNavigatinPain = true;

            hashKind = HashKind.None;

            opacity = 0;
        }
        #endregion

        #region 設定
        /// <summary>
        /// 設定の読み込み
        /// </summary>
        private static FolderWatcherSetting Load()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(FolderWatcherSetting));

            // 実行パスのファイルを読み込む
            String execPath = Path.GetDirectoryName(Application.ExecutablePath);
            String filePath = Path.Combine(execPath, SETTING_FILE_NAME);

            if (File.Exists(filePath) == false)
            {
                // ファイルがない場合は何もしない
                return new FolderWatcherSetting();
            }

            FolderWatcherSetting setting = null;
            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                setting = serializer.Deserialize(stream) as FolderWatcherSetting;
            }

            return setting;
        }

        /// <summary>
        /// 設定の保存
        /// </summary>
        public void Save()
        {
            if (isSaveSettingFile == false)
            {
                return;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(FolderWatcherSetting));

            // 実行パスに保存する。
            String execPath = Path.GetDirectoryName(Application.ExecutablePath);
            String filePath = Path.Combine(execPath, SETTING_FILE_NAME);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
         }
        #endregion

        #region リソースの読み込み
        /// <summary>
        /// リソースファイルから文字列を取得します。
        /// </summary>
        /// <param name="item">FileItemKind</param>
        /// <returns></returns>
        public static String GetString(FileItemKind item)
        {
            //現在実行しているAssemblyを取得する
            System.Reflection.Assembly asm;
            asm = System.Reflection.Assembly.GetExecutingAssembly();

            //ResourceManagerオブジェクトの作成
            //リソースファイル名が"Resource1.resources"だとする
            System.Resources.ResourceManager rm =
                new System.Resources.ResourceManager(
                    typeof(FolderWatcherSetting).Namespace + ".ListItemResource", asm);

            return rm.GetString(item.ToString());
        }
        #endregion
    }
}
