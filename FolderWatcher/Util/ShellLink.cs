using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;                // コンソールプロジェクトの場合は、参照から追加します

namespace Rui.FolderWatcher.Util
{
    /*
  
      Copyright
      c 2003 Steve McMahon steve@vbaccelerator.com.


      アイコン機能を削除、#region やら namespace
      コメント（テキトウに訳してます）を変更して転載。
      日本語のコメントはあまり信用しないでください (^^;
      オリジナルソースに関してはサイト（vbaccelerator）を参照。

    */
    /// <summary>
    /// IShellLinkインターフェイスから、オブジェクトを生成する。
    /// IShellLink インターフェースは、シェルリンクを作成、変更するための機能を提供します。
    /// </summary>
    public class ShellLink : IDisposable
    {
        /// <summary>
        /// ショートカットファイルの拡張子
        /// </summary>
        public const String LinkExtention = ".lnk";

        #region InterfaceShellLink
        [ComImportAttribute()]
        [GuidAttribute("0000010C-0000-0000-C000-000000000046")]
        [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IPersist
        {
            [PreserveSig]
            //[helpstring("Returns the class identifier for the component object")]
            void GetClassID(out Guid pClassID);
        }

        [ComImportAttribute()]
        [GuidAttribute("0000010B-0000-0000-C000-000000000046")]
        [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IPersistFile
        {
            // can't get this to go if I extend IPersist, so put it here:
            [PreserveSig]
            void GetClassID(out Guid pClassID);

            //[helpstring("Checks for changes since last file write")]        
            void IsDirty();

            //[helpstring("Opens the specified file and initializes the object from its contents")]        
            void Load(
                [MarshalAs(UnmanagedType.LPWStr)] string pszFileName,
                uint dwMode);

            //[helpstring("Saves the object into the specified file")]        
            void Save(
                [MarshalAs(UnmanagedType.LPWStr)] string pszFileName,
                [MarshalAs(UnmanagedType.Bool)] bool fRemember);

            //[helpstring("Notifies the object that save is completed")]        
            void SaveCompleted(
                [MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

            //[helpstring("Gets the current name of the file associated with the object")]        
            void GetCurFile(
                [MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
        }

        [ComImportAttribute()]
        [GuidAttribute("000214EE-0000-0000-C000-000000000046")]
        [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IShellLinkA
        {
            //[helpstring("Retrieves the path and filename of a shell link object")]
            void GetPath(
                [Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszFile,
                int cchMaxPath,
                ref _WIN32_FIND_DATAA pfd,
                uint fFlags);

            //[helpstring("Retrieves the list of shell link item identifiers")]
            void GetIDList(out IntPtr ppidl);

            //[helpstring("Sets the list of shell link item identifiers")]
            void SetIDList(IntPtr pidl);

            //[helpstring("Retrieves the shell link description string")]
            void GetDescription(
                [Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszFile,
                int cchMaxName);

            //[helpstring("Sets the shell link description string")]
            void SetDescription(
                [MarshalAs(UnmanagedType.LPStr)] string pszName);

            //[helpstring("Retrieves the name of the shell link working directory")]
            void GetWorkingDirectory(
                [Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszDir,
                int cchMaxPath);

            //[helpstring("Sets the name of the shell link working directory")]
            void SetWorkingDirectory(
                [MarshalAs(UnmanagedType.LPStr)] string pszDir);

            //[helpstring("Retrieves the shell link command-line arguments")]
            void GetArguments(
                [Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszArgs,
                int cchMaxPath);

            //[helpstring("Sets the shell link command-line arguments")]
            void SetArguments(
                [MarshalAs(UnmanagedType.LPStr)] string pszArgs);

            //[propget, helpstring("Retrieves or sets the shell link hot key")]
            void GetHotkey(out short pwHotkey);
            //[propput, helpstring("Retrieves or sets the shell link hot key")]
            void SetHotkey(short pwHotkey);

            //[propget, helpstring("Retrieves or sets the shell link show command")]
            void GetShowCmd(out uint piShowCmd);
            //[propput, helpstring("Retrieves or sets the shell link show command")]
            void SetShowCmd(uint piShowCmd);

            //[helpstring("Retrieves the location (path and index) of the shell link icon")]
            void GetIconLocation(
                [Out(), MarshalAs(UnmanagedType.LPStr)] StringBuilder pszIconPath,
                int cchIconPath,
                out int piIcon);

            //[helpstring("Sets the location (path and index) of the shell link icon")]
            void SetIconLocation(
                [MarshalAs(UnmanagedType.LPStr)] string pszIconPath,
                int iIcon);

            //[helpstring("Sets the shell link relative path")]
            void SetRelativePath(
                [MarshalAs(UnmanagedType.LPStr)] string pszPathRel,
                uint dwReserved);

            //[helpstring("Resolves a shell link. The system searches for the shell link object and updates the shelnk path and its list of identifiers (if necessary)")]
            void Resolve(
                IntPtr hWnd,
                uint fFlags);

            //[helpstring("Sets the shell link path and filename")]
            void SetPath(
                [MarshalAs(UnmanagedType.LPStr)] string pszFile);
        }


        [ComImportAttribute()]
        [GuidAttribute("000214F9-0000-0000-C000-000000000046")]
        [InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IShellLinkW
        {
            //[helpstring("Retrieves the path and filename of a shell link object")]
            void GetPath(
                [Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile,
                int cchMaxPath,
                ref _WIN32_FIND_DATAW pfd,
                uint fFlags);

            //[helpstring("Retrieves the list of shell link item identifiers")]
            void GetIDList(out IntPtr ppidl);

            //[helpstring("Sets the list of shell link item identifiers")]
            void SetIDList(IntPtr pidl);

            //[helpstring("Retrieves the shell link description string")]
            void GetDescription(
                [Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile,
                int cchMaxName);

            //[helpstring("Sets the shell link description string")]
            void SetDescription(
                [MarshalAs(UnmanagedType.LPWStr)] string pszName);

            //[helpstring("Retrieves the name of the shell link working directory")]
            void GetWorkingDirectory(
                [Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir,
                int cchMaxPath);

            //[helpstring("Sets the name of the shell link working directory")]
            void SetWorkingDirectory(
                [MarshalAs(UnmanagedType.LPWStr)] string pszDir);

            //[helpstring("Retrieves the shell link command-line arguments")]
            void GetArguments(
                [Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs,
                int cchMaxPath);

            //[helpstring("Sets the shell link command-line arguments")]
            void SetArguments(
                [MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

            //[propget, helpstring("Retrieves or sets the shell link hot key")]
            void GetHotkey(out short pwHotkey);
            //[propput, helpstring("Retrieves or sets the shell link hot key")]
            void SetHotkey(short pwHotkey);

            //[propget, helpstring("Retrieves or sets the shell link show command")]
            void GetShowCmd(out uint piShowCmd);
            //[propput, helpstring("Retrieves or sets the shell link show command")]
            void SetShowCmd(uint piShowCmd);

            //[helpstring("Retrieves the location (path and index) of the shell link icon")]
            void GetIconLocation(
                [Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath,
                int cchIconPath,
                out int piIcon);

            //[helpstring("Sets the location (path and index) of the shell link icon")]
            void SetIconLocation(
                [MarshalAs(UnmanagedType.LPWStr)] string pszIconPath,
                int iIcon);

            //[helpstring("Sets the shell link relative path")]
            void SetRelativePath(
                [MarshalAs(UnmanagedType.LPWStr)] string pszPathRel,
                uint dwReserved);

            //[helpstring("Resolves a shell link. The system searches for the shell link object and updates the shelnk path and its list of identifiers (if necessary)")]
            void Resolve(
                IntPtr hWnd,
                uint fFlags);

            //[helpstring("Sets the shell link path and filename")]
            void SetPath(
                [MarshalAs(UnmanagedType.LPWStr)] string pszFile);
        }

        [GuidAttribute("00021401-0000-0000-C000-000000000046")]
        [ClassInterfaceAttribute(ClassInterfaceType.None)]
        [ComImportAttribute()]
        private class CShellLink { }
        #endregion

        #region enumAndStructShellLink
        private enum EShellLinkGP : uint
        {
            SLGP_SHORTPATH = 1,
            SLGP_UNCPRIORITY = 2
        }

        [Flags]
        private enum EShowWindowFlags : uint
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_MAX = 10
        }


        [StructLayoutAttribute(LayoutKind.Sequential, Pack = 4, Size = 0, CharSet = CharSet.Unicode)]
        private struct _WIN32_FIND_DATAW
        {
            public uint dwFileAttributes;
            public _FILETIME ftCreationTime;
            public _FILETIME ftLastAccessTime;
            public _FILETIME ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            public uint dwReserved0;
            public uint dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] // MAX_PATH
            public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, Pack = 4, Size = 0, CharSet = CharSet.Ansi)]
        private struct _WIN32_FIND_DATAA
        {
            public uint dwFileAttributes;
            public _FILETIME ftCreationTime;
            public _FILETIME ftLastAccessTime;
            public _FILETIME ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            public uint dwReserved0;
            public uint dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] // MAX_PATH
            public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
        }

        [StructLayoutAttribute(LayoutKind.Sequential, Pack = 4, Size = 0)]
        private struct _FILETIME
        {
            public uint dwLowDateTime;
            public uint dwHighDateTime;
        }

        private class UnManagedMethods
        {
            [DllImport("Shell32", CharSet = CharSet.Auto)]
            internal extern static int ExtractIconEx(
                [MarshalAs(UnmanagedType.LPTStr)] 
                string lpszFile,
                int nIconIndex,
                IntPtr[] phIconLarge,
                IntPtr[] phIconSmall,
                int nIcons);

            [DllImport("user32")]
            internal static extern int DestroyIcon(IntPtr hIcon);
        }

        /// <summary>
        /// ターゲットが失われた場合、リンクがどのように解決されるか決めるフラグ。
        /// </summary>
        [Flags]
        public enum EShellLinkResolveFlags : uint
        {
            /// <summary>
            /// Allow any match during resolution.  Has no effect
            /// on ME/2000 or above, use the other flags instead.
            /// 任意の組み合わせを許可する。
            /// Win ME/2000 もしくはそれ以降のOSでは効果がない
            /// 他のフラグを代わり使ってください。
            /// </summary>
            SLR_ANY_MATCH = 0x2,

            /// <summary>
            /// Call the Microsoft Windows Installer. 
            /// </summary>
            SLR_INVOKE_MSI = 0x80,

            /// <summary>
            /// distributed(分散) link tracking(追跡)を不能にしてください。
            /// デフォルトでは、distributed link trackingは、
            /// ボリューム名に基づいた多数の装置やリムーバブル・メディアを
            /// 追跡します。そのドライブレターが変わった遠隔のファイル・システムを
            /// 追跡するためにUNCのパスを使用します。
            /// SLR_NOLINKINFOのセットは両方のタイプのトラッキングを不能にします。
            /// </summary>
            SLR_NOLINKINFO = 0x40,

            /// <summary>
            /// リンクを解決することができない場合に、ダイアログ・ボックスを表示しない。
            /// SLR_NO_UIをセットする場合、リンク解決に費やされる最大の時間を指定します。
            /// タイムアウト値はミリセカンドで指定することができます。
            /// タイムアウト持続内にリンクを解決することができない場合、制御が返ります。
            /// タイムアウトがセットされない場合、デフォルト値の 3,000 ミリセカンド(3秒)がセットされます。
            /// </summary>                                            
            SLR_NO_UI = 0x1,

            /// <summary>
            /// SDKの中でドキュメント化されてない(なんじゃそりゃ)。
            /// SLR_NO_UIと同じであると仮定するが、hWndのない適用のためと推測。
            /// </summary>
            SLR_NO_UI_WITH_MSG_PUMP = 0x101,

            /// <summary>
            /// リンク情報を更新しない。
            /// </summary>
            SLR_NOUPDATE = 0x8,

            /// <summary>
            /// 探索ヒューリスティックスを実行しない。
            /// </summary>
            SLR_NOSEARCH = 0x10,

            /// <summary>
            /// distributed(分散?) link tracking使用しない。
            /// </summary>
            SLR_NOTRACK = 0x20,

            /// <summary>
            /// リンク・オブジェクトが変わった場合は、対象のパスおよびリストを更新してください。
            /// SLR_UPDATE がセットされる場合、リンク・オブジェクトが変わったかどうかを
            /// 調べるために IPersistFile::IsDirty を呼び出す必要がありません。
            /// </summary>
            SLR_UPDATE = 0x4
        }

        public enum LinkDisplayMode : uint
        {
            edmNormal = EShowWindowFlags.SW_NORMAL,
            edmMinimized = EShowWindowFlags.SW_SHOWMINNOACTIVE,
            edmMaximized = EShowWindowFlags.SW_MAXIMIZE
        }

        #endregion

        #region maincode
        // Use Unicode (W) under NT, otherwise use ANSI
        private IShellLinkW linkW;
        private IShellLinkA linkA;
        private string shortcutFile = "";

        /// <summary>
        /// シェル・リンク・オブジェクトのインスタンスを作成します。
        /// </summary>
        public ShellLink()
        {
            if (System.Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                linkW = (IShellLinkW)new CShellLink();
            }
            else
            {
                linkA = (IShellLinkA)new CShellLink();
            }
        }

        /// <summary>
        /// 指定されたリンクファイルから
        /// シェル・リンク・オブジェクトのインスタンスを作成します。
        /// </summary>
        /// <param name="linkFile">オープンするリンクファイル</param>
        public ShellLink(string linkFile)
            : this()
        {
            Open(linkFile);
        }

        /// <summary>
        /// Dispose()が呼び出されていない場合、それを呼び出します。
        /// </summary>
        ~ShellLink()
        {
            Dispose();
        }

        /// <summary>
        /// COM ShellLinkオブジェクトをリリースして、オブジェクトを処分します
        /// </summary>
        public void Dispose()
        {
            if (linkW != null)
            {
                Marshal.ReleaseComObject(linkW);
                linkW = null;
            }
            if (linkA != null)
            {
                Marshal.ReleaseComObject(linkA);
                linkA = null;
            }
        }

        public string ShortCutFile
        {
            get
            {
                return this.shortcutFile;
            }
            set
            {
                this.shortcutFile = value;
            }
        }

        /// <summary>
        /// リンク先を設定、取得
        /// </summary>
        public string Target
        {
            get
            {
                StringBuilder target = new StringBuilder(260, 260);
                if (linkA == null)
                {
                    _WIN32_FIND_DATAW fd = new _WIN32_FIND_DATAW();
                    linkW.GetPath(target, target.Capacity, ref fd, (uint)EShellLinkGP.SLGP_UNCPRIORITY);
                }
                else
                {
                    _WIN32_FIND_DATAA fd = new _WIN32_FIND_DATAA();
                    linkA.GetPath(target, target.Capacity, ref fd, (uint)EShellLinkGP.SLGP_UNCPRIORITY);
                }
                return target.ToString();
            }
            set
            {
                if (linkA == null)
                {
                    linkW.SetPath(value);
                }
                else
                {
                    linkA.SetPath(value);
                }
            }
        }

        /// <summary>
        /// 作業ディレクトリを設定、取得
        /// </summary>
        public string WorkingDirectory
        {
            get
            {
                StringBuilder path = new StringBuilder(260, 260);
                if (linkA == null)
                {
                    linkW.GetWorkingDirectory(path, path.Capacity);
                }
                else
                {
                    linkA.GetWorkingDirectory(path, path.Capacity);
                }
                return path.ToString();
            }
            set
            {
                if (linkA == null)
                {
                    linkW.SetWorkingDirectory(value);
                }
                else
                {
                    linkA.SetWorkingDirectory(value);
                }
            }
        }

        /// <summary>
        /// リンクの記述（プロパティーで見ると、コメント）を設定、取得
        /// </summary>
        public string Description
        {
            get
            {
                StringBuilder description = new StringBuilder(1024, 1024);
                if (linkA == null)
                {
                    linkW.GetDescription(description, description.Capacity);
                }
                else
                {
                    linkA.GetDescription(description, description.Capacity);
                }
                return description.ToString();
            }
            set
            {
                if (linkA == null)
                {
                    linkW.SetDescription(value);
                }
                else
                {
                    linkA.SetDescription(value);
                }
            }
        }

        /// <summary>
        /// コマンドライン引き数を設定、取得
        /// </summary>
        public string Arguments
        {
            get
            {
                StringBuilder arguments = new StringBuilder(260, 260);
                if (linkA == null)
                {
                    linkW.GetArguments(arguments, arguments.Capacity);
                }
                else
                {
                    linkA.GetArguments(arguments, arguments.Capacity);
                }
                return arguments.ToString();
            }
            set
            {
                if (linkA == null)
                {
                    linkW.SetArguments(value);
                }
                else
                {
                    linkA.SetArguments(value);
                }
            }
        }

        /// <summary>
        /// 近道が実行される場合、初期のディスプレイ・モードを設定、取得
        /// </summary>
        public LinkDisplayMode DisplayMode
        {
            get
            {
                uint cmd = 0;
                if (linkA == null)
                {
                    linkW.GetShowCmd(out cmd);
                }
                else
                {
                    linkA.GetShowCmd(out cmd);
                }
                return (LinkDisplayMode)cmd;
            }
            set
            {
                if (linkA == null)
                {
                    linkW.SetShowCmd((uint)value);
                }
                else
                {
                    linkA.SetShowCmd((uint)value);
                }
            }
        }

        /// <summary>
        /// ショートカットのホットキーを設定、取得
        /// [仮想キーコード=>System.Windows.Forms.Keys]
        /// </summary>
        public Keys HotKey
        {
            get
            {
                short key = 0;
                if (linkA == null)
                {
                    linkW.GetHotkey(out key);
                }
                else
                {
                    linkA.GetHotkey(out key);
                }
                return (Keys)key;
            }
            set
            {
                if (linkA == null)
                {
                    linkW.SetHotkey((short)value);
                }
                else
                {
                    linkA.SetHotkey((short)value);
                }
            }
        }

        /// <summary>
        /// ShortCutFile(プロパティー) にショートカットファイルを保存する
        /// </summary>
        public void Save()
        {
            Save(shortcutFile);
        }

        /// <summary>
        /// ショートカットファイルを保存する
        /// </summary>
        /// <param name="linkFile">ショートカットの保存先(.lnk)</param>
        public void Save(string linkFile)
        {
            // Save the object to disk
            if (linkA == null)
            {
                ((IPersistFile)linkW).Save(linkFile, true);
                shortcutFile = linkFile;
            }
            else
            {
                ((IPersistFile)linkA).Save(linkFile, true);
                shortcutFile = linkFile;
            }
        }

        /// <summary>
        /// 指定されたショートカットファイルをロードする
        /// </summary>
        /// <param name="linkFile">読み込みショートカットファイル(.lnk)</param>
        public void Open(string linkFile)
        {
            Open(linkFile, IntPtr.Zero, (EShellLinkResolveFlags.SLR_ANY_MATCH | EShellLinkResolveFlags.SLR_NO_UI), 1);
        }

        /// <summary>
        /// 指定されたショートカットファイルをロードし、
        /// ショートカットのターゲットがセットに見つからない場合、
        /// UI 動作をコントロールするフラグを許可する。
        /// </summary>
        /// <param name="linkFile">読み込みショートカットファイル(.lnk)</param>
        /// <param name="hWnd">(もしあれば)適用のUIのウィンドウハンドル</param>
        /// <param name="resolveFlags">動作を指定するフラグ</param>
        public void Open(string linkFile, IntPtr hWnd, EShellLinkResolveFlags resolveFlags)
        {
            Open(linkFile, hWnd, resolveFlags, 1);
        }

        /// <summary>
        /// 指定されたショートカットファイルをロードし、
        /// ショートカットのターゲットがセットに見つからない場合、
        /// UI 動作をコントロールするフラグを許可する。
        /// SLR_NO_UIが指定されない場合、
        /// さらに、タイムアウトを指定することができる。
        /// </summary>
        /// <param name="linkFile">読み込みショートカットファイル(.lnk)</param>
        /// <param name="hWnd">(もしあれば)適用のUIのウィンドウハンドル</param>
        /// <param name="resolveFlags">動作を指定するフラグ</param>
        /// <param name="timeOut">SLR_NO_UIがミリセカンドで指定される場合、タイムアウト</param>
        public void Open(string linkFile, IntPtr hWnd, EShellLinkResolveFlags resolveFlags, ushort timeOut)
        {
            uint flags;

            if ((resolveFlags & EShellLinkResolveFlags.SLR_NO_UI) == EShellLinkResolveFlags.SLR_NO_UI)
            {
                flags = (uint)((int)resolveFlags | (timeOut << 16));
            }
            else
            {
                flags = (uint)resolveFlags;
            }

            if (linkA == null)
            {
                ((IPersistFile)linkW).Load(linkFile, 0); //STGM_DIRECT)
                linkW.Resolve(hWnd, flags);
                this.shortcutFile = linkFile;
            }
            else
            {
                ((IPersistFile)linkA).Load(linkFile, 0); //STGM_DIRECT)
                linkA.Resolve(hWnd, flags);
                this.shortcutFile = linkFile;
            }
        }
        #endregion
    }

}


