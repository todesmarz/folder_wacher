using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Rui.FolderWatcher.Util
{
    class FileAttachData
    {
        #region 構造体
        /// <summary>
        /// ファイルに関連づけられたアイコンデータ
        /// </summary>
        private class FileIconInfo
        {
            /// <summary>
            /// 小さいアイコン
            /// </summary>
            private Image smallIcon;

            public Image SmallIcon
            {
                get {
                    Image image = smallIcon;

                    return image;
                }
            }

            /// <summary>
            /// 大きなアイコン
            /// </summary>
            private Image largeIcon;

            public Image LargeIcon
            {
                get
                {
                    Image image = largeIcon;

                    return image;
                }
            }
            
            /// <summary>
            /// アイコンファイルの位置
            /// </summary>
            private string location = string.Empty;

            public string Location
            {
                get { return location; }
                set { location = value; }
            }

            /// <summary>
            /// Locationのアイコンファイルのインデックス
            /// </summary>
            private IntPtr iconIndex = (IntPtr) 0;

            public IntPtr IconIndex
            {
                get { return iconIndex; }
                set { iconIndex = value; }
            }

            /// <summary>
            /// 隠しファイル
            /// </summary>
            private bool isHidden;

            public bool Hidden
            {
                get { return isHidden; }
                set { isHidden = value; }
            }

            /// <summary>
            /// アクセス拒否
            /// </summary>
            private bool isAccessDeny;

            public bool AccessDeny
            {
                get { return isAccessDeny; }
                set { isAccessDeny = value; }
            }

            public void setLargeIcon(Icon icon)
            {
                if (icon != null)
                {
                    largeIcon = icon.ToBitmap();
                }
                else
                {
                    largeIcon = new Bitmap(32, 32);
                }

                if (Hidden == true)
                {
                    largeIcon = convertHiddenImage(largeIcon);
                }

                if (AccessDeny == true)
                {
                    largeIcon = convertAccessDenyImage(largeIcon);
                }
            }

            public void setSmallIcon(Icon icon)
            {
                if (icon != null)
                {
                    smallIcon = icon.ToBitmap();
                }
                else
                {
                    smallIcon = new Bitmap(16, 16);
                }

                if (Hidden == true)
                {
                    smallIcon = convertHiddenImage(smallIcon);
                }

                if (AccessDeny == true)
                {
                    smallIcon = convertAccessDenyImage(smallIcon);
                }
            }

            /// <summary>
            /// 隠しファイル用アイコンに変換します。
            /// </summary>
            /// <param name="icon"></param>
            /// <returns></returns>
            private Image convertHiddenImage(Image icon)
            {
                System.Drawing.Imaging.ColorMatrix cm = new System.Drawing.Imaging.ColorMatrix();
                //ColorMatrixの行列の値を変更して、アルファ値が0.5に変更されるようにする
                cm.Matrix00 = 1;
                cm.Matrix11 = 1;
                cm.Matrix22 = 1;
                cm.Matrix33 = 0.5F;
                cm.Matrix44 = 1;

                //ImageAttributesオブジェクトの作成
                System.Drawing.Imaging.ImageAttributes ia =
                    new System.Drawing.Imaging.ImageAttributes();
                //ColorMatrixを設定する
                ia.SetColorMatrix(cm);

                Image image = new Bitmap(icon.Width, icon.Height);
                Graphics g = Graphics.FromImage(image);

                g.DrawImage(icon, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, ia);

                return image;
            }

            /// <summary>
            /// 隠しファイル用アイコンに変換します。
            /// </summary>
            /// <param name="icon"></param>
            /// <returns></returns>
            private Image convertAccessDenyImage(Image icon)
            {
                Graphics g = Graphics.FromImage(icon);

                g.DrawImage(new Icon(Properties.Resources.AccessDeny, icon.Size).ToBitmap(), Point.Empty);

                return icon;
            }

            /// <summary>
            /// 比較を行います。
            /// </summary>
            /// <param name="obj">比較対象</param>
            /// <returns>比較と同じ場合は true</returns>
            public override bool Equals(object obj)
            {
                if (base.Equals(obj) == true)
                {
                    return true;
                }

                if (obj is FileIconInfo == false)
                {
                    return false;
                }

                FileIconInfo iconInfo = (FileIconInfo)obj;

                if (location.Equals(iconInfo.location) == false)
                {
                    return false;
                }

                if (iconIndex != iconInfo.iconIndex)
                {
                    return false;
                }

                if (isHidden != iconInfo.isHidden)
                {
                    return false;
                }

                if (isAccessDeny != iconInfo.isAccessDeny)
                {
                    return false;
                }

                return true;
            }

            /// <summary>
            /// ハッシュコードを取得します。
            /// </summary>
            /// <returns></returns>
            public override int GetHashCode()
            {
                int hashCode = 17;
                hashCode += Convert.ToInt32(isHidden) * 13;
                hashCode += Convert.ToInt32(isAccessDeny) * 17;
                hashCode += location.GetHashCode();
                hashCode = hashCode * 7 + (int) iconIndex;

                return hashCode;
            }
        }
        #endregion

        #region メンバ変数
        /// <summary>
        /// ファイルパス
        /// </summary>
        private string path;

        /// <summary>
        /// 隠しファイル
        /// </summary>
        private bool isHidden;

        /// <summary>
        /// アクセス拒否
        /// </summary>
        private bool isAccessDeny;

        /// <summary>
        /// アイコン番号
        /// </summary>
        private int iconIndex;

        /// <summary>
        /// 拡張子
        /// </summary>
        private string extensionName;

        /// <summary>
        /// アイコン一覧
        /// </summary>
        private static IList<FileIconInfo> iconList = new List<FileIconInfo>();

        /// <summary>
        /// 説明一覧
        /// </summary>
        private static IDictionary<string, string> descriptionDictinary = new Dictionary<string, string>();
        #endregion

        #region Dll読み込み
        #region 定数
        enum SHGFI : int
        {
            /// <summary>get icon</summary>
            Icon = 0x000000100,
            /// <summary>get display name</summary>
            DisplayName = 0x000000200,
            /// <summary>get type name</summary>
            TypeName = 0x000000400,
            /// <summary>get attributes</summary>
            Attributes = 0x000000800,
            /// <summary>get icon location</summary>
            IconLocation = 0x000001000,
            /// <summary>return exe type</summary>
            ExeType = 0x000002000,
            /// <summary>get system icon index</summary>
            SysIconIndex = 0x000004000,
            /// <summary>put a link overlay on icon</summary>
            LinkOverlay = 0x000008000,
            /// <summary>show icon in selected state</summary>
            Selected = 0x000010000,
            /// <summary>get only specified attributes</summary>
            Attr_Specified = 0x000020000,
            /// <summary>get large icon</summary>
            LargeIcon = 0x000000000,
            /// <summary>get small icon</summary>
            SmallIcon = 0x000000001,
            /// <summary>get open icon</summary>
            OpenIcon = 0x000000002,
            /// <summary>get shell size icon</summary>
            ShellIconSize = 0x000000004,
            /// <summary>pszPath is a pidl</summary>
            PIDL = 0x000000008,
            /// <summary>use passed dwFileAttribute</summary>
            UseFileAttributes = 0x000000010,
            /// <summary>apply the appropriate overlays</summary>
            AddOverlays = 0x000000020,
            /// <summary>Get the index of the overlay in the upper 8 bits of the iIcon</summary>
            OverlayIndex = 0x000000040,
        }   
        #endregion

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        [DllImport("shell32.dll")]
        private static extern bool SHObjectProperties(uint hwnd, uint shopObjectType, [MarshalAs(UnmanagedType.LPWStr)] string pszObjectName, [MarshalAs(UnmanagedType.LPWStr)] string pszPropertyPage);

        [DllImport("User32.dll")]
        public static extern int DestroyIcon(IntPtr hIcon);

        #endregion

        #region プロパティ
        /// <summary>
        /// 小さいアイコンを取得することができます。
        /// </summary>
        public Image SmallIcon
        {
            get
            {
                if (iconIndex == -1)
                {
                    iconIndex = GetIconInfo(path, isHidden, isAccessDeny);
                }

                return iconList[iconIndex].SmallIcon;

            }
        }

        /// <summary>
        /// 大きいアイコンを取得することができます。
        /// </summary>
        public Image LargeIcon
        {
            get
            {
                if (iconIndex == -1)
                {
                    iconIndex = GetIconInfo(path, isHidden, isAccessDeny);
                }

                return iconList[iconIndex].LargeIcon;
            }
        }

        /// <summary>
        /// タイプ名を取得することができます。
        /// </summary>
        public string TypeName
        {
            get
            {
                if (descriptionDictinary.ContainsKey(extensionName) == false)
                {
                    SHFILEINFO shinfo = new SHFILEINFO();
                    uint shInfoSize = (uint)Marshal.SizeOf(shinfo);

                    descriptionDictinary.Add(extensionName, GetTypeName(shinfo, shInfoSize, path));
                }

                return descriptionDictinary[extensionName];
            }
        }
        #endregion

        #region 初期化
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="path">対象ファイルパス</param>
        /// <param name="isHidden">隠しファイル</param>
        /// <param name="isAccessDeny">アクセス拒否</param>
        public FileAttachData(string path, bool isHidden, bool isAccessDeny)
        {
            this.path = path;
            this.isHidden = isHidden;
            this.isAccessDeny = isAccessDeny;

            this.extensionName = System.IO.Path.GetExtension(path);
            this.iconIndex = -1;
        }
        #endregion

        #region データの取得
        /// <summary>
        /// ファイルに関連づけられたアイコンを取得
        /// </summary>
        /// <param name="path">対象ファイルパス</param>
        /// <returns>アイコンインデックス</returns>
        private static int GetIconInfo(string path, bool isHidden, bool isAccessDeny)
        {
            SHFILEINFO shinfo = new SHFILEINFO();
            uint shInfoSize = (uint)Marshal.SizeOf(shinfo);
            FileIconInfo iconInfo = GetIconLocation(shinfo, shInfoSize, path);

            iconInfo.Hidden = isHidden;
            iconInfo.AccessDeny = isAccessDeny;

            if (iconList.Contains(iconInfo) == false)
            {
                iconInfo.setSmallIcon(GetIcon(shinfo, shInfoSize, path, (uint)(SHGFI.Icon | SHGFI.SmallIcon)));
                iconInfo.setLargeIcon(GetIcon(shinfo, shInfoSize, path, (uint)(SHGFI.Icon | SHGFI.LargeIcon)));

                iconList.Add(iconInfo);
            }

            return iconList.IndexOf(iconInfo);
        }

        /// <summary>
        /// ファイルに関連づけられたアイコンを取得
        /// </summary>
        /// <param name="path">対象ファイルパス</param>
        /// <param name="uFlag">ふらぐ</param>
        /// <returns>アイコン</returns>
        private static Icon GetIcon(SHFILEINFO shinfo, uint shOnfoSize, string path, uint uFlag)
        {
            IntPtr hSuccess = SHGetFileInfo(path, 0, ref shinfo, shOnfoSize, uFlag);

            if (hSuccess == IntPtr.Zero)
            {
                return null;
            }

            Icon icon = (Icon)Icon.FromHandle(shinfo.hIcon).Clone();
            DestroyIcon(shinfo.hIcon);
            return icon;
        }

        /// <summary>
        /// ファイルに関連づけられたタイプ名を取得
        /// </summary>
        /// <param name="path">対象ファイルパス</param>
        /// <returns>タイプ名</returns>
        private static String GetTypeName(SHFILEINFO shinfo , uint shInfoSize, string path)
        {
            IntPtr hSuccess = SHGetFileInfo(path, 0, ref shinfo, shInfoSize, (uint)SHGFI.TypeName);

            if (hSuccess == IntPtr.Zero)
            {
                return null;
            }

            return shinfo.szTypeName;
        }

        /// <summary>
        /// ファイルに関連づけられたアイコンの格納場所を取得
        /// </summary>
        /// <param name="path">対象ファイルパス</param>
        /// <returns>格納場所</returns>
        private static FileIconInfo GetIconLocation(SHFILEINFO shinfo , uint shInfoSize, string path)
        {
            FileIconInfo iconInfo = new FileIconInfo();
            IntPtr hSuccess = SHGetFileInfo(path, 0, ref shinfo, shInfoSize, (uint)SHGFI.IconLocation);

            if (hSuccess == IntPtr.Zero)
            {
                return iconInfo;
            }
            iconInfo.Location = shinfo.szDisplayName;
            iconInfo.IconIndex = shinfo.iIcon;

            return iconInfo;
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
            return SHObjectProperties((uint)parent.Handle, (uint)0x02, path, null);
        }
        #endregion
    }
}
