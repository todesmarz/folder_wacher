using System.Windows.Forms;
using Rui.FolderWatcher.Data;

namespace Rui.FolderWatcher.Util
{
    class FormSetting
    {
        #region フォーム設定
        /// <summary>
        /// フォームの透過設定を行います。
        /// </summary>
        public static void SetOpacity(Form form)
        {
            FolderWatcherSetting setting = FolderWatcherSetting.Instance;
            form.Opacity = 1 - setting.Opacity * 0.05;
        }
        #endregion
    }
}
