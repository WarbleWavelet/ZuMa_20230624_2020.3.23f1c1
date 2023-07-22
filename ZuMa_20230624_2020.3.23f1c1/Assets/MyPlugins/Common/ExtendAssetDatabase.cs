/****************************************************
    文件：ExtendAssetDatabase.cs
	作者：lenovo
    邮箱: 
    日期：2023/6/24 22:13:11
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;
 

public static partial class ExtendAssetDatabase 
{

/***
01 编辑Asset里面的数据，直接引用改就行了
       MapConfig fromAsset= AssetDatabase.LoadAssetAtPath<MapConfig>("Assets/Map/shooter.asset");
       int idx = int.Parse( gameObject.name);
       Transform shooter = GameObject.Find("ShooterTrans").transform;
       fromAsset.pathPointList[idx] = shooter.localPosition;
       AssetDatabase.SaveAssets();
       AssetDatabase.Refresh();
*/

}


/// <summary>收集阶段，还不能直接用</summary>
public static partial class ExtendAssetDatabase
{


#if UNITY_EDITOR
    public static T CreateAsset<T>(string assetPath = "Assets/Map/map.asset") 
        where T:UnityEngine.ScriptableObject,new()
    {
        T cfg = new T(); 
        //写如数据
        //foreach (var item in pathPointList)
        //{
        //    cfg.pathPointList.Add(item);
        //}
        AssetDatabase.CreateAsset(cfg, assetPath);
        AssetDatabase.SaveAssets();

        return cfg;
    }

     /// <summary>
     /// 
     /// </summary>
     /// <param name="fromPath">"Assets/Asset1.txt"</param>
     /// <param name="toPath">"Assets/Text/Asset1.txt"</param>
    public static void CopyAsset(string fromPath, string toPath)
    {
        AssetDatabase.CopyAsset(fromPath,  toPath);
        AssetDatabase.Refresh();
    }

    public static void MoveAsset(string fromPath, string toPath)
    {
        AssetDatabase.MoveAsset(fromPath, toPath);
        AssetDatabase.Refresh();
    }

    public static void DeleteAsset(string path)
    {
        AssetDatabase.DeleteAsset(path);
        AssetDatabase.Refresh();
    }


    public static void AddObjectToAsset(Object obj, string path)
    {
        AssetDatabase.AddObjectToAsset(obj, path);
        AssetDatabase.Refresh();
    }   
    
    
    public static void EditAsset(Object obj, string path)
    {
        try
        {
            //将资源数据库置于大多数 API
            //都暂停导入的状态
            AssetDatabase.StartAssetEditing();
            AssetDatabase.CopyAsset("Assets/Asset1.txt", "Assets/Text/Asset1.txt");
            AssetDatabase.MoveAsset("Assets/Asset2.txt", "Assets/Text/Asset2.txt");
            AssetDatabase.DeleteAsset("Assets/Asset3.txt");
        }
        finally
        {
            //在 "finally" 代码块中添加
            //对 StopAssetEditing 的调用可确保
            //在离开此函数时重置 AssetDatabase 状态
            AssetDatabase.StopAssetEditing();
        }
    }
#endif


}




