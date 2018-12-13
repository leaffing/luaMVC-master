
using UnityEngine;

namespace LuaMVC
{
    /// <summary>
    /// 文件路径类：用于获取各种文件路径、目录
    /// </summary>
    public class FilePath
    {
        public static string prePath
        {
            get
            {
#if UNITY_EDITOR 
                return "file://" + Application.dataPath + "/StreamingAssets/";
#elif UNITY_STANDALONE_WIN
                return "file://" + Application.dataPath + "/StreamingAssets/";
#elif UNITY_ANDROUD
                return "jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IPHONE
                return Application.dataPath + "/Raw/";
#endif
                return "";
            }
        }

        public static string normalPath 
        {
            get
            {
#if UNITY_EDITOR 
                return  Application.dataPath + "/StreamingAssets/";
#elif UNITY_STANDALONE_WIN
                return "file://" + Application.dataPath + "/StreamingAssets/";
#elif UNITY_ANDROUD
                return "jar:file://" + Application.dataPath + "!/assets/";
#elif UNITY_IPHONE
                return Application.dataPath + "/Raw/";
#endif 
                return "";
            }
        }

        public static string fullPath( string fileName )
        {
            return prePath + fileName;
        }

        /// <summary>
        /// 取得数据存放目录(persistentDataPath)
        /// </summary>
        public static string DataPath
        {
            get
            {
                string game = LuaMVCConfig.AppName.ToLower();
                if (Application.isMobilePlatform)
                {
                    return Application.persistentDataPath + "/" + game + "/";
                }
                if (Application.isEditor)
                {
                    return Application.dataPath + "/StreamingAssets/";
                }
                if (Application.platform == RuntimePlatform.OSXEditor)
                {
                    int i = Application.dataPath.LastIndexOf('/');
                    return Application.dataPath.Substring(0, i + 1) + game + "/";
                }
                return "c:/" + game + "/";
            }
        }

        public static string GetRelativePath()
        {
            if (Application.isEditor)
                return "file://" + System.Environment.CurrentDirectory.Replace("\\", "/") + "/Assets/StreamingAssets/";
            else if (Application.isMobilePlatform || Application.isConsolePlatform)
                return "file:///" + DataPath;
            else // For standalone player.
                 return "file://" + DataPath;
                //return "file://" + Application.streamingAssetsPath + "/";
        }

        /// <summary>
        /// 应用程序内容路径
        /// </summary>
        public static string AppContentPath()
        {
            string path = string.Empty;
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    path = "jar:file://" + Application.dataPath + "!/assets/";
                    break;
                case RuntimePlatform.IPhonePlayer:
                    path = Application.dataPath + "/Raw/";
                    break;
                default:
                    path = Application.dataPath + "/StreamingAssets/";
                    break;
            }
            return path;
        }

        //todo 这是在pc端，可以保持正常，需要在安卓机进行调试
        // configuration file path
        public static string JsonConfigFilePath( string fileName )
        {
            return normalPath + "Configuration/Json/" + fileName;
        }
        public static string XmlConfigFilePath( string fileName )
        {
            return normalPath + "Configuration/Xml/" + fileName;
        }
        public static string ProtobufConfigFilePath( string fileName )
        {
            return normalPath + "Configuration/Protobuf/" + fileName;
        }
    }
}