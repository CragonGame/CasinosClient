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
    public class UnityEngineMeshWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UnityEngine.Mesh);
			Utils.BeginObjectRegister(type, L, translator, 0, 37, 23, 19);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetNativeVertexBufferPtr", _m_GetNativeVertexBufferPtr);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetNativeIndexBufferPtr", _m_GetNativeIndexBufferPtr);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ClearBlendShapes", _m_ClearBlendShapes);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetBlendShapeName", _m_GetBlendShapeName);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetBlendShapeIndex", _m_GetBlendShapeIndex);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetBlendShapeFrameCount", _m_GetBlendShapeFrameCount);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetBlendShapeFrameWeight", _m_GetBlendShapeFrameWeight);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetBlendShapeFrameVertices", _m_GetBlendShapeFrameVertices);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddBlendShapeFrame", _m_AddBlendShapeFrame);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetUVDistributionMetric", _m_GetUVDistributionMetric);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetVertices", _m_GetVertices);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetVertices", _m_SetVertices);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetNormals", _m_GetNormals);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetNormals", _m_SetNormals);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetTangents", _m_GetTangents);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetTangents", _m_SetTangents);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetColors", _m_GetColors);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetColors", _m_SetColors);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetUVs", _m_SetUVs);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetUVs", _m_GetUVs);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetTriangles", _m_GetTriangles);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetIndices", _m_GetIndices);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetIndexStart", _m_GetIndexStart);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetIndexCount", _m_GetIndexCount);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetBaseVertex", _m_GetBaseVertex);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetTriangles", _m_SetTriangles);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetIndices", _m_SetIndices);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetBindposes", _m_GetBindposes);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetBoneWeights", _m_GetBoneWeights);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Clear", _m_Clear);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RecalculateBounds", _m_RecalculateBounds);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RecalculateNormals", _m_RecalculateNormals);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RecalculateTangents", _m_RecalculateTangents);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "MarkDynamic", _m_MarkDynamic);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "UploadMeshData", _m_UploadMeshData);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetTopology", _m_GetTopology);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CombineMeshes", _m_CombineMeshes);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "indexFormat", _g_get_indexFormat);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "vertexBufferCount", _g_get_vertexBufferCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "blendShapeCount", _g_get_blendShapeCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "boneWeights", _g_get_boneWeights);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "bindposes", _g_get_bindposes);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "isReadable", _g_get_isReadable);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "vertexCount", _g_get_vertexCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "subMeshCount", _g_get_subMeshCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "bounds", _g_get_bounds);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "vertices", _g_get_vertices);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "normals", _g_get_normals);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "tangents", _g_get_tangents);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uv", _g_get_uv);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uv2", _g_get_uv2);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uv3", _g_get_uv3);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uv4", _g_get_uv4);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uv5", _g_get_uv5);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uv6", _g_get_uv6);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uv7", _g_get_uv7);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uv8", _g_get_uv8);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "colors", _g_get_colors);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "colors32", _g_get_colors32);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "triangles", _g_get_triangles);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "indexFormat", _s_set_indexFormat);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "boneWeights", _s_set_boneWeights);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "bindposes", _s_set_bindposes);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "subMeshCount", _s_set_subMeshCount);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "bounds", _s_set_bounds);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "vertices", _s_set_vertices);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "normals", _s_set_normals);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "tangents", _s_set_tangents);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uv", _s_set_uv);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uv2", _s_set_uv2);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uv3", _s_set_uv3);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uv4", _s_set_uv4);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uv5", _s_set_uv5);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uv6", _s_set_uv6);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uv7", _s_set_uv7);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uv8", _s_set_uv8);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "colors", _s_set_colors);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "colors32", _s_set_colors32);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "triangles", _s_set_triangles);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					UnityEngine.Mesh gen_ret = new UnityEngine.Mesh();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Mesh constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetNativeVertexBufferPtr(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                        System.IntPtr gen_ret = gen_to_be_invoked.GetNativeVertexBufferPtr( _index );
                        LuaAPI.lua_pushlightuserdata(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetNativeIndexBufferPtr(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        System.IntPtr gen_ret = gen_to_be_invoked.GetNativeIndexBufferPtr(  );
                        LuaAPI.lua_pushlightuserdata(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ClearBlendShapes(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ClearBlendShapes(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetBlendShapeName(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _shapeIndex = LuaAPI.xlua_tointeger(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.GetBlendShapeName( _shapeIndex );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetBlendShapeIndex(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _blendShapeName = LuaAPI.lua_tostring(L, 2);
                    
                        int gen_ret = gen_to_be_invoked.GetBlendShapeIndex( _blendShapeName );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetBlendShapeFrameCount(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _shapeIndex = LuaAPI.xlua_tointeger(L, 2);
                    
                        int gen_ret = gen_to_be_invoked.GetBlendShapeFrameCount( _shapeIndex );
                        LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetBlendShapeFrameWeight(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _shapeIndex = LuaAPI.xlua_tointeger(L, 2);
                    int _frameIndex = LuaAPI.xlua_tointeger(L, 3);
                    
                        float gen_ret = gen_to_be_invoked.GetBlendShapeFrameWeight( _shapeIndex, _frameIndex );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetBlendShapeFrameVertices(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _shapeIndex = LuaAPI.xlua_tointeger(L, 2);
                    int _frameIndex = LuaAPI.xlua_tointeger(L, 3);
                    UnityEngine.Vector3[] _deltaVertices = (UnityEngine.Vector3[])translator.GetObject(L, 4, typeof(UnityEngine.Vector3[]));
                    UnityEngine.Vector3[] _deltaNormals = (UnityEngine.Vector3[])translator.GetObject(L, 5, typeof(UnityEngine.Vector3[]));
                    UnityEngine.Vector3[] _deltaTangents = (UnityEngine.Vector3[])translator.GetObject(L, 6, typeof(UnityEngine.Vector3[]));
                    
                    gen_to_be_invoked.GetBlendShapeFrameVertices( _shapeIndex, _frameIndex, _deltaVertices, _deltaNormals, _deltaTangents );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddBlendShapeFrame(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _shapeName = LuaAPI.lua_tostring(L, 2);
                    float _frameWeight = (float)LuaAPI.lua_tonumber(L, 3);
                    UnityEngine.Vector3[] _deltaVertices = (UnityEngine.Vector3[])translator.GetObject(L, 4, typeof(UnityEngine.Vector3[]));
                    UnityEngine.Vector3[] _deltaNormals = (UnityEngine.Vector3[])translator.GetObject(L, 5, typeof(UnityEngine.Vector3[]));
                    UnityEngine.Vector3[] _deltaTangents = (UnityEngine.Vector3[])translator.GetObject(L, 6, typeof(UnityEngine.Vector3[]));
                    
                    gen_to_be_invoked.AddBlendShapeFrame( _shapeName, _frameWeight, _deltaVertices, _deltaNormals, _deltaTangents );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetUVDistributionMetric(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _uvSetIndex = LuaAPI.xlua_tointeger(L, 2);
                    
                        float gen_ret = gen_to_be_invoked.GetUVDistributionMetric( _uvSetIndex );
                        LuaAPI.lua_pushnumber(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetVertices(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Generic.List<UnityEngine.Vector3> _vertices = (System.Collections.Generic.List<UnityEngine.Vector3>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Vector3>));
                    
                    gen_to_be_invoked.GetVertices( _vertices );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetVertices(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Generic.List<UnityEngine.Vector3> _inVertices = (System.Collections.Generic.List<UnityEngine.Vector3>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Vector3>));
                    
                    gen_to_be_invoked.SetVertices( _inVertices );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetNormals(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Generic.List<UnityEngine.Vector3> _normals = (System.Collections.Generic.List<UnityEngine.Vector3>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Vector3>));
                    
                    gen_to_be_invoked.GetNormals( _normals );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetNormals(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Generic.List<UnityEngine.Vector3> _inNormals = (System.Collections.Generic.List<UnityEngine.Vector3>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Vector3>));
                    
                    gen_to_be_invoked.SetNormals( _inNormals );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTangents(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Generic.List<UnityEngine.Vector4> _tangents = (System.Collections.Generic.List<UnityEngine.Vector4>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Vector4>));
                    
                    gen_to_be_invoked.GetTangents( _tangents );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetTangents(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Generic.List<UnityEngine.Vector4> _inTangents = (System.Collections.Generic.List<UnityEngine.Vector4>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Vector4>));
                    
                    gen_to_be_invoked.SetTangents( _inTangents );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetColors(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Collections.Generic.List<UnityEngine.Color>>(L, 2)) 
                {
                    System.Collections.Generic.List<UnityEngine.Color> _colors = (System.Collections.Generic.List<UnityEngine.Color>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Color>));
                    
                    gen_to_be_invoked.GetColors( _colors );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Collections.Generic.List<UnityEngine.Color32>>(L, 2)) 
                {
                    System.Collections.Generic.List<UnityEngine.Color32> _colors = (System.Collections.Generic.List<UnityEngine.Color32>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Color32>));
                    
                    gen_to_be_invoked.GetColors( _colors );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Mesh.GetColors!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetColors(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.Collections.Generic.List<UnityEngine.Color>>(L, 2)) 
                {
                    System.Collections.Generic.List<UnityEngine.Color> _inColors = (System.Collections.Generic.List<UnityEngine.Color>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Color>));
                    
                    gen_to_be_invoked.SetColors( _inColors );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Collections.Generic.List<UnityEngine.Color32>>(L, 2)) 
                {
                    System.Collections.Generic.List<UnityEngine.Color32> _inColors = (System.Collections.Generic.List<UnityEngine.Color32>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Color32>));
                    
                    gen_to_be_invoked.SetColors( _inColors );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Mesh.SetColors!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetUVs(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Collections.Generic.List<UnityEngine.Vector2>>(L, 3)) 
                {
                    int _channel = LuaAPI.xlua_tointeger(L, 2);
                    System.Collections.Generic.List<UnityEngine.Vector2> _uvs = (System.Collections.Generic.List<UnityEngine.Vector2>)translator.GetObject(L, 3, typeof(System.Collections.Generic.List<UnityEngine.Vector2>));
                    
                    gen_to_be_invoked.SetUVs( _channel, _uvs );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Collections.Generic.List<UnityEngine.Vector3>>(L, 3)) 
                {
                    int _channel = LuaAPI.xlua_tointeger(L, 2);
                    System.Collections.Generic.List<UnityEngine.Vector3> _uvs = (System.Collections.Generic.List<UnityEngine.Vector3>)translator.GetObject(L, 3, typeof(System.Collections.Generic.List<UnityEngine.Vector3>));
                    
                    gen_to_be_invoked.SetUVs( _channel, _uvs );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Collections.Generic.List<UnityEngine.Vector4>>(L, 3)) 
                {
                    int _channel = LuaAPI.xlua_tointeger(L, 2);
                    System.Collections.Generic.List<UnityEngine.Vector4> _uvs = (System.Collections.Generic.List<UnityEngine.Vector4>)translator.GetObject(L, 3, typeof(System.Collections.Generic.List<UnityEngine.Vector4>));
                    
                    gen_to_be_invoked.SetUVs( _channel, _uvs );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Mesh.SetUVs!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetUVs(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Collections.Generic.List<UnityEngine.Vector2>>(L, 3)) 
                {
                    int _channel = LuaAPI.xlua_tointeger(L, 2);
                    System.Collections.Generic.List<UnityEngine.Vector2> _uvs = (System.Collections.Generic.List<UnityEngine.Vector2>)translator.GetObject(L, 3, typeof(System.Collections.Generic.List<UnityEngine.Vector2>));
                    
                    gen_to_be_invoked.GetUVs( _channel, _uvs );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Collections.Generic.List<UnityEngine.Vector3>>(L, 3)) 
                {
                    int _channel = LuaAPI.xlua_tointeger(L, 2);
                    System.Collections.Generic.List<UnityEngine.Vector3> _uvs = (System.Collections.Generic.List<UnityEngine.Vector3>)translator.GetObject(L, 3, typeof(System.Collections.Generic.List<UnityEngine.Vector3>));
                    
                    gen_to_be_invoked.GetUVs( _channel, _uvs );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Collections.Generic.List<UnityEngine.Vector4>>(L, 3)) 
                {
                    int _channel = LuaAPI.xlua_tointeger(L, 2);
                    System.Collections.Generic.List<UnityEngine.Vector4> _uvs = (System.Collections.Generic.List<UnityEngine.Vector4>)translator.GetObject(L, 3, typeof(System.Collections.Generic.List<UnityEngine.Vector4>));
                    
                    gen_to_be_invoked.GetUVs( _channel, _uvs );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Mesh.GetUVs!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTriangles(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _submesh = LuaAPI.xlua_tointeger(L, 2);
                    
                        int[] gen_ret = gen_to_be_invoked.GetTriangles( _submesh );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    int _submesh = LuaAPI.xlua_tointeger(L, 2);
                    bool _applyBaseVertex = LuaAPI.lua_toboolean(L, 3);
                    
                        int[] gen_ret = gen_to_be_invoked.GetTriangles( _submesh, _applyBaseVertex );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Collections.Generic.List<int>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    System.Collections.Generic.List<int> _triangles = (System.Collections.Generic.List<int>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<int>));
                    int _submesh = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.GetTriangles( _triangles, _submesh );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<System.Collections.Generic.List<int>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    System.Collections.Generic.List<int> _triangles = (System.Collections.Generic.List<int>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<int>));
                    int _submesh = LuaAPI.xlua_tointeger(L, 3);
                    bool _applyBaseVertex = LuaAPI.lua_toboolean(L, 4);
                    
                    gen_to_be_invoked.GetTriangles( _triangles, _submesh, _applyBaseVertex );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Mesh.GetTriangles!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetIndices(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _submesh = LuaAPI.xlua_tointeger(L, 2);
                    
                        int[] gen_ret = gen_to_be_invoked.GetIndices( _submesh );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    int _submesh = LuaAPI.xlua_tointeger(L, 2);
                    bool _applyBaseVertex = LuaAPI.lua_toboolean(L, 3);
                    
                        int[] gen_ret = gen_to_be_invoked.GetIndices( _submesh, _applyBaseVertex );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Collections.Generic.List<int>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    System.Collections.Generic.List<int> _indices = (System.Collections.Generic.List<int>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<int>));
                    int _submesh = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.GetIndices( _indices, _submesh );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<System.Collections.Generic.List<int>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    System.Collections.Generic.List<int> _indices = (System.Collections.Generic.List<int>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<int>));
                    int _submesh = LuaAPI.xlua_tointeger(L, 3);
                    bool _applyBaseVertex = LuaAPI.lua_toboolean(L, 4);
                    
                    gen_to_be_invoked.GetIndices( _indices, _submesh, _applyBaseVertex );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Mesh.GetIndices!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetIndexStart(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _submesh = LuaAPI.xlua_tointeger(L, 2);
                    
                        uint gen_ret = gen_to_be_invoked.GetIndexStart( _submesh );
                        LuaAPI.xlua_pushuint(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetIndexCount(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _submesh = LuaAPI.xlua_tointeger(L, 2);
                    
                        uint gen_ret = gen_to_be_invoked.GetIndexCount( _submesh );
                        LuaAPI.xlua_pushuint(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetBaseVertex(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _submesh = LuaAPI.xlua_tointeger(L, 2);
                    
                        uint gen_ret = gen_to_be_invoked.GetBaseVertex( _submesh );
                        LuaAPI.xlua_pushuint(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetTriangles(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<int[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int[] _triangles = (int[])translator.GetObject(L, 2, typeof(int[]));
                    int _submesh = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.SetTriangles( _triangles, _submesh );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<System.Collections.Generic.List<int>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    System.Collections.Generic.List<int> _triangles = (System.Collections.Generic.List<int>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<int>));
                    int _submesh = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.SetTriangles( _triangles, _submesh );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<int[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    int[] _triangles = (int[])translator.GetObject(L, 2, typeof(int[]));
                    int _submesh = LuaAPI.xlua_tointeger(L, 3);
                    bool _calculateBounds = LuaAPI.lua_toboolean(L, 4);
                    
                    gen_to_be_invoked.SetTriangles( _triangles, _submesh, _calculateBounds );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<System.Collections.Generic.List<int>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    System.Collections.Generic.List<int> _triangles = (System.Collections.Generic.List<int>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<int>));
                    int _submesh = LuaAPI.xlua_tointeger(L, 3);
                    bool _calculateBounds = LuaAPI.lua_toboolean(L, 4);
                    
                    gen_to_be_invoked.SetTriangles( _triangles, _submesh, _calculateBounds );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& translator.Assignable<int[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    int[] _triangles = (int[])translator.GetObject(L, 2, typeof(int[]));
                    int _submesh = LuaAPI.xlua_tointeger(L, 3);
                    bool _calculateBounds = LuaAPI.lua_toboolean(L, 4);
                    int _baseVertex = LuaAPI.xlua_tointeger(L, 5);
                    
                    gen_to_be_invoked.SetTriangles( _triangles, _submesh, _calculateBounds, _baseVertex );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& translator.Assignable<System.Collections.Generic.List<int>>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    System.Collections.Generic.List<int> _triangles = (System.Collections.Generic.List<int>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<int>));
                    int _submesh = LuaAPI.xlua_tointeger(L, 3);
                    bool _calculateBounds = LuaAPI.lua_toboolean(L, 4);
                    int _baseVertex = LuaAPI.xlua_tointeger(L, 5);
                    
                    gen_to_be_invoked.SetTriangles( _triangles, _submesh, _calculateBounds, _baseVertex );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Mesh.SetTriangles!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetIndices(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& translator.Assignable<int[]>(L, 2)&& translator.Assignable<UnityEngine.MeshTopology>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    int[] _indices = (int[])translator.GetObject(L, 2, typeof(int[]));
                    UnityEngine.MeshTopology _topology;translator.Get(L, 3, out _topology);
                    int _submesh = LuaAPI.xlua_tointeger(L, 4);
                    
                    gen_to_be_invoked.SetIndices( _indices, _topology, _submesh );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& translator.Assignable<int[]>(L, 2)&& translator.Assignable<UnityEngine.MeshTopology>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)) 
                {
                    int[] _indices = (int[])translator.GetObject(L, 2, typeof(int[]));
                    UnityEngine.MeshTopology _topology;translator.Get(L, 3, out _topology);
                    int _submesh = LuaAPI.xlua_tointeger(L, 4);
                    bool _calculateBounds = LuaAPI.lua_toboolean(L, 5);
                    
                    gen_to_be_invoked.SetIndices( _indices, _topology, _submesh, _calculateBounds );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 6&& translator.Assignable<int[]>(L, 2)&& translator.Assignable<UnityEngine.MeshTopology>(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 6)) 
                {
                    int[] _indices = (int[])translator.GetObject(L, 2, typeof(int[]));
                    UnityEngine.MeshTopology _topology;translator.Get(L, 3, out _topology);
                    int _submesh = LuaAPI.xlua_tointeger(L, 4);
                    bool _calculateBounds = LuaAPI.lua_toboolean(L, 5);
                    int _baseVertex = LuaAPI.xlua_tointeger(L, 6);
                    
                    gen_to_be_invoked.SetIndices( _indices, _topology, _submesh, _calculateBounds, _baseVertex );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Mesh.SetIndices!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetBindposes(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Generic.List<UnityEngine.Matrix4x4> _bindposes = (System.Collections.Generic.List<UnityEngine.Matrix4x4>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Matrix4x4>));
                    
                    gen_to_be_invoked.GetBindposes( _bindposes );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetBoneWeights(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Collections.Generic.List<UnityEngine.BoneWeight> _boneWeights = (System.Collections.Generic.List<UnityEngine.BoneWeight>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.BoneWeight>));
                    
                    gen_to_be_invoked.GetBoneWeights( _boneWeights );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Clear(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                    gen_to_be_invoked.Clear(  );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 2)) 
                {
                    bool _keepVertexLayout = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.Clear( _keepVertexLayout );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Mesh.Clear!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RecalculateBounds(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.RecalculateBounds(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RecalculateNormals(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.RecalculateNormals(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RecalculateTangents(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.RecalculateTangents(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_MarkDynamic(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.MarkDynamic(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_UploadMeshData(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    bool _markNoLongerReadable = LuaAPI.lua_toboolean(L, 2);
                    
                    gen_to_be_invoked.UploadMeshData( _markNoLongerReadable );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTopology(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _submesh = LuaAPI.xlua_tointeger(L, 2);
                    
                        UnityEngine.MeshTopology gen_ret = gen_to_be_invoked.GetTopology( _submesh );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CombineMeshes(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<UnityEngine.CombineInstance[]>(L, 2)) 
                {
                    UnityEngine.CombineInstance[] _combine = (UnityEngine.CombineInstance[])translator.GetObject(L, 2, typeof(UnityEngine.CombineInstance[]));
                    
                    gen_to_be_invoked.CombineMeshes( _combine );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& translator.Assignable<UnityEngine.CombineInstance[]>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    UnityEngine.CombineInstance[] _combine = (UnityEngine.CombineInstance[])translator.GetObject(L, 2, typeof(UnityEngine.CombineInstance[]));
                    bool _mergeSubMeshes = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.CombineMeshes( _combine, _mergeSubMeshes );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 4&& translator.Assignable<UnityEngine.CombineInstance[]>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    UnityEngine.CombineInstance[] _combine = (UnityEngine.CombineInstance[])translator.GetObject(L, 2, typeof(UnityEngine.CombineInstance[]));
                    bool _mergeSubMeshes = LuaAPI.lua_toboolean(L, 3);
                    bool _useMatrices = LuaAPI.lua_toboolean(L, 4);
                    
                    gen_to_be_invoked.CombineMeshes( _combine, _mergeSubMeshes, _useMatrices );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 5&& translator.Assignable<UnityEngine.CombineInstance[]>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)) 
                {
                    UnityEngine.CombineInstance[] _combine = (UnityEngine.CombineInstance[])translator.GetObject(L, 2, typeof(UnityEngine.CombineInstance[]));
                    bool _mergeSubMeshes = LuaAPI.lua_toboolean(L, 3);
                    bool _useMatrices = LuaAPI.lua_toboolean(L, 4);
                    bool _hasLightmapData = LuaAPI.lua_toboolean(L, 5);
                    
                    gen_to_be_invoked.CombineMeshes( _combine, _mergeSubMeshes, _useMatrices, _hasLightmapData );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Mesh.CombineMeshes!");
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_indexFormat(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.indexFormat);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_vertexBufferCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.vertexBufferCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_blendShapeCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.blendShapeCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_boneWeights(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.boneWeights);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_bindposes(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.bindposes);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isReadable(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.isReadable);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_vertexCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.vertexCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_subMeshCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.subMeshCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_bounds(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                translator.PushUnityEngineBounds(L, gen_to_be_invoked.bounds);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_vertices(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.vertices);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_normals(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.normals);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_tangents(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.tangents);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uv(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uv);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uv2(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uv2);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uv3(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uv3);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uv4(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uv4);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uv5(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uv5);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uv6(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uv6);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uv7(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uv7);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uv8(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uv8);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_colors(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.colors);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_colors32(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.colors32);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_triangles(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.triangles);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_indexFormat(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                UnityEngine.Rendering.IndexFormat gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.indexFormat = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_boneWeights(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.boneWeights = (UnityEngine.BoneWeight[])translator.GetObject(L, 2, typeof(UnityEngine.BoneWeight[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_bindposes(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.bindposes = (UnityEngine.Matrix4x4[])translator.GetObject(L, 2, typeof(UnityEngine.Matrix4x4[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_subMeshCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.subMeshCount = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_bounds(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                UnityEngine.Bounds gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.bounds = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_vertices(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.vertices = (UnityEngine.Vector3[])translator.GetObject(L, 2, typeof(UnityEngine.Vector3[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_normals(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.normals = (UnityEngine.Vector3[])translator.GetObject(L, 2, typeof(UnityEngine.Vector3[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_tangents(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.tangents = (UnityEngine.Vector4[])translator.GetObject(L, 2, typeof(UnityEngine.Vector4[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uv(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uv = (UnityEngine.Vector2[])translator.GetObject(L, 2, typeof(UnityEngine.Vector2[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uv2(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uv2 = (UnityEngine.Vector2[])translator.GetObject(L, 2, typeof(UnityEngine.Vector2[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uv3(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uv3 = (UnityEngine.Vector2[])translator.GetObject(L, 2, typeof(UnityEngine.Vector2[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uv4(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uv4 = (UnityEngine.Vector2[])translator.GetObject(L, 2, typeof(UnityEngine.Vector2[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uv5(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uv5 = (UnityEngine.Vector2[])translator.GetObject(L, 2, typeof(UnityEngine.Vector2[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uv6(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uv6 = (UnityEngine.Vector2[])translator.GetObject(L, 2, typeof(UnityEngine.Vector2[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uv7(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uv7 = (UnityEngine.Vector2[])translator.GetObject(L, 2, typeof(UnityEngine.Vector2[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uv8(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uv8 = (UnityEngine.Vector2[])translator.GetObject(L, 2, typeof(UnityEngine.Vector2[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_colors(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.colors = (UnityEngine.Color[])translator.GetObject(L, 2, typeof(UnityEngine.Color[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_colors32(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.colors32 = (UnityEngine.Color32[])translator.GetObject(L, 2, typeof(UnityEngine.Color32[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_triangles(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Mesh gen_to_be_invoked = (UnityEngine.Mesh)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.triangles = (int[])translator.GetObject(L, 2, typeof(int[]));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
