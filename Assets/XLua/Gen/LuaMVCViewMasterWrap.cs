#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class LuaMVCViewMasterWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(LuaMVC.ViewMaster);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 6, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "AddView", _m_AddView_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "OpenView", _m_OpenView_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CloseView", _m_CloseView_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "RemoveView", _m_RemoveView_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetView", _m_GetView_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "LuaMVC.ViewMaster does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddView_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& translator.Assignable<LuaMVC.IBaseView>(L, 1)) 
                {
                    LuaMVC.IBaseView view = (LuaMVC.IBaseView)translator.GetObject(L, 1, typeof(LuaMVC.IBaseView));
                    
                    LuaMVC.ViewMaster.AddView( view );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1&& translator.Assignable<LuaMVC.IBaseView[]>(L, 1)) 
                {
                    LuaMVC.IBaseView[] views = (LuaMVC.IBaseView[])translator.GetObject(L, 1, typeof(LuaMVC.IBaseView[]));
                    
                    LuaMVC.ViewMaster.AddView( views );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1&& translator.Assignable<LuaMVC.ILuaBaseView>(L, 1)) 
                {
                    LuaMVC.ILuaBaseView view = (LuaMVC.ILuaBaseView)translator.GetObject(L, 1, typeof(LuaMVC.ILuaBaseView));
                    
                    LuaMVC.ViewMaster.AddView( view );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to LuaMVC.ViewMaster.AddView!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenView_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string viewName = LuaAPI.lua_tostring(L, 1);
                    
                    LuaMVC.ViewMaster.OpenView( viewName );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1&& translator.Assignable<string[]>(L, 1)) 
                {
                    string[] viewNames = (string[])translator.GetObject(L, 1, typeof(string[]));
                    
                    LuaMVC.ViewMaster.OpenView( viewNames );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    string viewName = LuaAPI.lua_tostring(L, 1);
                    bool closeOthers = LuaAPI.lua_toboolean(L, 2);
                    
                    LuaMVC.ViewMaster.OpenView( viewName, closeOthers );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to LuaMVC.ViewMaster.OpenView!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CloseView_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string viewName = LuaAPI.lua_tostring(L, 1);
                    
                    LuaMVC.ViewMaster.CloseView( viewName );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1&& translator.Assignable<string[]>(L, 1)) 
                {
                    string[] viewNames = (string[])translator.GetObject(L, 1, typeof(string[]));
                    
                    LuaMVC.ViewMaster.CloseView( viewNames );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to LuaMVC.ViewMaster.CloseView!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveView_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int __gen_param_count = LuaAPI.lua_gettop(L);
            
                if(__gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string viewName = LuaAPI.lua_tostring(L, 1);
                    
                    LuaMVC.ViewMaster.RemoveView( viewName );
                    
                    
                    
                    return 0;
                }
                if(__gen_param_count == 1&& translator.Assignable<string[]>(L, 1)) 
                {
                    string[] viewNames = (string[])translator.GetObject(L, 1, typeof(string[]));
                    
                    LuaMVC.ViewMaster.RemoveView( viewNames );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to LuaMVC.ViewMaster.RemoveView!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetView_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    string viewName = LuaAPI.lua_tostring(L, 1);
                    
                        LuaMVC.IBaseView __cl_gen_ret = LuaMVC.ViewMaster.GetView( viewName );
                        translator.PushAny(L, __cl_gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception __gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + __gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
