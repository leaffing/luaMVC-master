
using System.Collections.Generic;
using System.IO;
using System.Text;
using PureMVC.Patterns;

namespace LuaMVC
{
    using UnityEngine;
    using PureMVC.Patterns.Lua;
    using XLua;
    using System;
    using System.Collections;

    [LuaCallCSharp]
    public class LuaApplicationFacade : LuaFacade
    {
        public static LuaEnv luaEnv = new LuaEnv();
        private LuaTable scriptEnv = null;
        private Action ondestroy = null;
        /// <summary>
        /// lua文件的Assetbundle，注意加载路径
        /// </summary>
        private AssetBundle luaAssetbundle = null;

        public LuaApplicationFacade()
        {
             //StartUp();
        }
        public IEnumerator StartUp()
        {
            yield return LoadLuaAssetbundle(FilePath.DataPath+"Data/lua.unity3d");
            //根据是否开启自动更新来设置加载方式
            if (LuaMVCConfig.AutomaticUpdateLuaScriptsFromServer)
                luaEnv.AddLoader(LuaAssetLoader);
            else
                luaEnv.AddLoader(Loader);
            this.scriptEnv = luaEnv.NewTable();
            LuaTable meta = luaEnv.NewTable();
            meta.Set("__index", luaEnv.Global);
            scriptEnv.SetMetaTable(meta);
            meta.Dispose();
            scriptEnv.Set("self", this);
            luaEnv.DoString(LoadLua("LuaFacade"), "LuaFacade", scriptEnv); 
            Action awake = scriptEnv.Get<Action>("awake");
            ondestroy = scriptEnv.Get<Action>("ondestroy");
            Debug.Log("LuaApplicationFacade:StartUp");

            if (null != awake)
            {
                awake();
                Debug.Log("LuaFacade.awake()");
            }
        }

        public void ShutDown()
        {
            if (null != ondestroy)
                ondestroy();
            scriptEnv.Dispose();
        } 

        /// <summary>
        /// 从指定路径加载Assetbundle
        /// </summary>
        /// <param name="assetPath">加载文件路径</param>
        /// <returns></returns>
        private IEnumerator LoadLuaAssetbundle(string assetPath)
        {
            WWW www = new WWW(assetPath);            
            yield return www;
            luaAssetbundle = www.assetBundle;
        }

        /// <summary>
        /// 从Assetbundle里读取lua内容
        /// </summary>
        /// <param name="filePath">lua文件名（不需后缀）</param>
        /// <returns>读取的lua文本内容</returns>
        private string LoadAssetLua(string filePath)
        {
            TextAsset text = luaAssetbundle.LoadAsset(filePath + ".lua.txt") as TextAsset;
            return text.text;
        }
        /// <summary>
        /// 从数据存放目录里读取lua内容
        /// </summary>
        /// <param name="filePath">lua文件名（不需后缀）</param>
        /// <returns>读取的lua文本内容</returns>
        private string LoadLua(string filePath)
        {
            string fullPath = FilePath.DataPath + "Data/" + filePath + ".lua.txt";
            return File.ReadAllText(fullPath);
        }

        private byte[] Loader(ref string filePath)
        { 
            string fullPath = null;
            RecursionFilePath(FilePath.DataPath + "Data/", filePath + ".lua.txt",out fullPath);
            if (string.IsNullOrEmpty(fullPath))
                LuaMVCDebug.DebugError("Load " + filePath + ".lua.txt" +" failed.Please check the file path.");
            return Encoding.UTF8.GetBytes(File.ReadAllText(fullPath));
        } 
        private byte[] LoaderLuaFromAssetbundle( ref string filePath)
        {
            // todo 可以拓展一个直接从asetbundle中读取lua文件的接口 
            TextAsset text = luaAssetbundle.LoadAsset(filePath + ".lua.txt") as TextAsset;
            return Encoding.UTF8.GetBytes(text.text);
        }
        /// <summary>
        /// 从Assetbundle里面读取lua的loader
        /// </summary>
        /// <param name="filePath">lua文件名</param>
        /// <returns></returns>
        private byte[] LuaAssetLoader(ref string filePath)
        {
            TextAsset text = luaAssetbundle.LoadAsset(filePath+".lua.txt") as TextAsset;
            return Encoding.UTF8.GetBytes(text.text);
        }
        /// <summary>
        /// 从特定目录里读取lua的loader
        /// </summary>
        /// <param name="filePath">lua文件名</param>
        /// <returns></returns>
        private byte[] LuaPathLoader( ref string filePath )
        {   
            string fullPath = null;
            RecursionFilePath(Application.streamingAssetsPath + "/Data/", filePath + ".lua.txt", out fullPath); 
            if (string.IsNullOrEmpty(fullPath))
                LuaMVCDebug.DebugError("Load " + filePath + ".lua.txt" + " failed.Please check the file path.");
            return Encoding.UTF8.GetBytes(File.ReadAllText(fullPath));
        }

        /// <summary>
        /// 遍历路径下文件，获取完整路径（第一个同名的文件）
        /// </summary>
        /// <param name="dirPath">路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="fileFullPath">输出完整路径</param>
        private void RecursionFilePath( string dirPath , string fileName , out string fileFullPath )
        {
            fileFullPath = null;
            string[] filesPath = Directory.GetFiles(dirPath);
            for (int i = 0; i < filesPath.Length; i++)
            {
                FileInfo fileInfo = new FileInfo(filesPath[i]); 
                if (fileInfo.Name.Equals(fileName))
                {
                    
                    fileFullPath = fileInfo.FullName; 
                    return;
                }
            } 
            string[] childrenPath = Directory.GetDirectories(dirPath);
            for (int i = 0; i < childrenPath.Length; i++)
            {
                // 这里避免取到值以后的重复递归  也可以使用抛出异常的方式直接退出 ？？？
                if( !string.IsNullOrEmpty(fileFullPath) )
                    return;
                RecursionFilePath(childrenPath[i], fileName,out fileFullPath); 
            }
        }

        #region Lua table map to CSharp class 注册MVC各模块
        public void RegisterLuaMediator(string mediatorName)
        {
            luaEnv.DoString("require '" + mediatorName + "'"); 
            ILuaMediator mediator = new LuaMediator();
            mediator.NAME = luaEnv.Global.GetInPath<string>(mediatorName + ".NAME");
            mediator.ListNotificationInterests = luaEnv.Global.GetInPath<List<string>>(mediatorName + ".ListNotificationInterests");
            mediator.OnRegister = luaEnv.Global.GetInPath<Action>(mediatorName + ".OnRegister");
            mediator.OnRemove = luaEnv.Global.GetInPath<Action>(mediatorName + ".OnRemove");
            mediator.HandleNotification = luaEnv.Global.GetInPath<HandleNotification>(mediatorName + ".HandleNotification");
            base.RegisterLuaMediator(mediator);
        }
        public void RegisterLuaCommand( string commandName ) 
        {
            luaEnv.DoString("require '" + commandName + "'");
            ILuaCommnad command = new LuaCommnad();
            command.CommandName = luaEnv.Global.GetInPath<string>(commandName + ".NAME");
            command.Execute = luaEnv.Global.GetInPath<HandleNotification>(commandName+".Execute");
            base.RegisterLuaCommand(command);
        }
        public void RegisterLuaHandler( string handlerName )
        {
            luaEnv.DoString("require '" + handlerName + "'");
            ILuaHandler handler = new LuaHandler();
            handler.NAME = luaEnv.Global.GetInPath<string>(handlerName + ".NAME");
            handler.ListNotificationInterests = luaEnv.Global.GetInPath<List<string>>(handlerName + ".ListNotificationInterests");
            handler.OnRegister = luaEnv.Global.GetInPath<Action>(handlerName + ".OnRegister");
            handler.OnRemove = luaEnv.Global.GetInPath<Action>(handlerName + ".OnRemove");
            handler.Request = luaEnv.Global.GetInPath<HandleNotification>(handlerName + ".HandleNotification");
            base.RegisterLuaHandler(handler);
        }
        public void RegisterLuaProxy( string proxyName )
        {
            luaEnv.DoString("require '" + proxyName + "'");
            ILuaProxy proxy = new LuaProxy();
            proxy.NAME = luaEnv.Global.GetInPath<string>(proxyName + ".NAME");
            proxy.OnRegister = luaEnv.Global.GetInPath<Action>(proxyName + ".OnRegister");
            proxy.OnRemove = luaEnv.Global.GetInPath<Action>(proxyName + ".OnRemove");
            proxy.DoSomething = luaEnv.Global.GetInPath<HandleModle>(proxyName + ".DoSomething");
            base.RegisterLuaProxy(proxy);
        }
        #endregion
    }
}