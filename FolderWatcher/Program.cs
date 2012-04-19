using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Rui.FolderWatcher
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 引数が存在する場合は、そのパスで処理を行う
            String folderPath = "";
            if (args.Length >= 1)
            {
                folderPath = args[0];
            }

            Application.Run(new MainForm(folderPath));
        }
    }
}
