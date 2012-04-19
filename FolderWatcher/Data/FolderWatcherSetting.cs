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
        #region �萔
        /// <summary>
        /// �t�@�C����
        /// </summary>
        private const String SETTING_FILE_NAME = "Setting.xml";
        #endregion

        #region �񋓑�
        /// <summary>
        /// �T�C�Y�񋓑�
        /// </summary>
        public enum SizeUnitKind
        {
            /// <summary>
            /// ����
            /// </summary>
            Auto,

            /// <summary>
            /// �o�C�g
            /// </summary>
            Byte,

            /// <summary>
            /// �L���o�C�g
            /// </summary>
            Kb,

            /// <summary>
            /// ���K�o�C�g
            /// </summary>
            Mb,
        }

        /// <summary>
        /// �t�@�C���A�C�e���񋓑�
        /// </summary>
        public enum FileItemKind
        {
            /// <summary>
            /// �t�@�C����
            /// </summary>
            Name,

            /// <summary>
            /// �t�@�C���p�X
            /// </summary>
            DirectoryName,

            /// <summary>
            /// �g���q
            /// </summary>
            Extention,

            /// <summary>
            /// �쐬����
            /// </summary>
            CreationDate,

            /// <summary>
            /// �ύX����
            /// </summary>
            LastModifyDate,

            /// <summary>
            /// �A�N�Z�X����
            /// </summary>
            LastAccessDate,

            /// <summary>
            /// �ǂݎ���p
            /// </summary>
            ReadOnly,

            /// <summary>
            /// �B���t�@�C��
            /// </summary>
            Hidden,

            /// <summary>
            /// �A�N�Z�X����
            /// </summary>
            AccessDeny,

            /// <summary>
            /// �T�C�Y
            /// </summary>
            DataSize,

            /// <summary>
            /// �t�@�C����
            /// </summary>
            FileCount,

            /// <summary>
            /// �f�B���N�g����
            /// </summary>
            DirectoryCount,

            /// <summary>
            /// ���
            /// </summary>
            Kind,

            /// <summary>
            /// ���L��
            /// </summary>
            Owner,

            /// <summary>
            /// �n�b�V���l
            /// </summary>
            Hash
        }

        /// <summary>
        /// �n�b�V���̎��
        /// </summary>
        public enum HashKind
        {
            None,

            MD5,

            SHA1
        }
        #endregion

        #region �����o�ϐ�
        /// <summary>
        /// ���g
        /// </summary>
        private static FolderWatcherSetting instance = null;

        /// <summary>
        /// �����T�C�Y�P�ʉ��Z���s����
        /// </summary>
        private SizeUnitKind sizeUnit;

        /// <summary>
        /// �ݒ�̕ۑ�������
        /// </summary>
        private Boolean isSaveSettingFile;

        /// <summary>
        /// �t�H�[���̕\���ʒu
        /// </summary>
        private System.Drawing.Rectangle formRectangle;

        /// <summary>
        /// �i�r�Q�[�V�����y�C���\�����
        /// </summary>
        private bool isDisplayNavigatinPain;

        /// <summary>
        /// �ڍ׃y�C���\�����
        /// </summary>
        private bool isDisplayDetailPain;

        /// <summary>
        /// �t�H���_���X�g�̕\�����[�h
        /// </summary>
        private View folderListViewMode;

        /// <summary>
        /// �\���t�@�C���A�C�e�����X�g
        /// </summary>
        private FileItemKind[] fileItemList = new FileItemKind[0];

        /// <summary>
        /// �Ō�ɊJ�����t�H���_
        /// </summary>
        private string lastOpenFolder;

        /// <summary>
        /// �n�b�V���̎��
        /// </summary>
        private HashKind hashKind;

        /// <summary>
        /// ���΃p�X�\��
        /// </summary>
        private bool isRelativePath;

        /// <summary>
        /// ���ߒl
        /// </summary>
        private int opacity;
        #endregion

        #region �v���p�e�B
        /// <summary>
        /// ���g�̃C���X�^���X���擾���邱�Ƃ��ł��܂��B
        /// </summary>
        public static FolderWatcherSetting Instance
        {
            get
            {
                

                return instance;
            }
        }

        /// <summary>
        /// �����T�C�Y�P�ʉ��Z���s������ݒ�A�擾���邱�Ƃ��ł��܂��B
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
        /// �ݒ���t�@�C���֕ۑ����邩��ݒ�A�擾���邱�Ƃ��ł��܂��B
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
        /// �t�H�[���̈ʒu��ݒ�A�擾���邱�Ƃ��ł��܂��B
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
        /// ���ߒl��ݒ�A�擾���邱�Ƃ��ł��܂��B
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
        /// �i�r�Q�[�V�����y�C���\����Ԃ�ݒ�A�擾���邱�Ƃ��ł��܂��B
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
        /// �i�r�Q�[�V�����y�C���\����Ԃ�ݒ�A�擾���邱�Ƃ��ł��܂��B
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
        /// �t�H���_���X�g�̕\�����[�h��ݒ�A�擾���邱�Ƃ��ł��܂��B
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
        /// �Ō�ɊJ�����t�H���_��ݒ�A�擾���邱�Ƃ��ł��܂��B
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
        /// �t�@�C���A�C�e���ꗗ��ݒ�A�擾���邱�Ƃ��ł��܂��B
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
        /// �n�b�V���̎�ނ�ݒ�A�擾���邱�Ƃ��ł��܂��B
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
        /// ���΃p�X�\������ݒ�A�擾���邱�Ƃ��ł��܂��B
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

        #region �R���X�g���N�^
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        static FolderWatcherSetting()
        {
            // Utilty�N���X�̂��߃R���X�g���N�^�� private
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

        #region �ݒ�
        /// <summary>
        /// �ݒ�̓ǂݍ���
        /// </summary>
        private static FolderWatcherSetting Load()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(FolderWatcherSetting));

            // ���s�p�X�̃t�@�C����ǂݍ���
            String execPath = Path.GetDirectoryName(Application.ExecutablePath);
            String filePath = Path.Combine(execPath, SETTING_FILE_NAME);

            if (File.Exists(filePath) == false)
            {
                // �t�@�C�����Ȃ��ꍇ�͉������Ȃ�
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
        /// �ݒ�̕ۑ�
        /// </summary>
        public void Save()
        {
            if (isSaveSettingFile == false)
            {
                return;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(FolderWatcherSetting));

            // ���s�p�X�ɕۑ�����B
            String execPath = Path.GetDirectoryName(Application.ExecutablePath);
            String filePath = Path.Combine(execPath, SETTING_FILE_NAME);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, this);
            }
         }
        #endregion

        #region ���\�[�X�̓ǂݍ���
        /// <summary>
        /// ���\�[�X�t�@�C�����當������擾���܂��B
        /// </summary>
        /// <param name="item">FileItemKind</param>
        /// <returns></returns>
        public static String GetString(FileItemKind item)
        {
            //���ݎ��s���Ă���Assembly���擾����
            System.Reflection.Assembly asm;
            asm = System.Reflection.Assembly.GetExecutingAssembly();

            //ResourceManager�I�u�W�F�N�g�̍쐬
            //���\�[�X�t�@�C������"Resource1.resources"���Ƃ���
            System.Resources.ResourceManager rm =
                new System.Resources.ResourceManager(
                    typeof(FolderWatcherSetting).Namespace + ".ListItemResource", asm);

            return rm.GetString(item.ToString());
        }
        #endregion
    }
}
