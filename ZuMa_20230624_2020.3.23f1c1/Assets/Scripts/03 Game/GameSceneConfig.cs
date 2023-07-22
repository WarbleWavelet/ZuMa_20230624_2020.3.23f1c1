using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEditor;

public class GameSceneConfig : MonoSingleton<GameSceneConfig>
{
    /// <summary>背景</summary>
    public Sprite[] BgSpriteArr;
    /// <summary>每一关的轨道数据</summary>
    public MapConfig[] MapConfigArr;
    /// <summary>发射器青蛙的位置</summary>
    public Vector3[] ShooterPosArr;
    //
    private Image m_Bg;
    private Transform m_ShooterTrans;
    /// <summary>最多30关，索引是29</summary>
    public const int MaxLevel = 30;
    QFramework.ResLoader m_Loader;

    public void Init(int p_LevelIdx
        ,Transform p_Bg
        ,Transform p_ShooterTrans)
    {
        //
        m_Bg = p_Bg.GetComponent<Image>();
        m_ShooterTrans = p_ShooterTrans;
        //
        Debug.LogFormat("****GameSceneConfig初始化，Level={0}", p_LevelIdx);
        InitData();


        m_Bg.sprite = BgSpriteArr[p_LevelIdx];
        m_ShooterTrans.GetComponent<RectTransform>().localPosition = ShooterPosArr[p_LevelIdx];
    }


    void InitData()
    {           
        BgSpriteArr = new Sprite[MaxLevel];
        MapConfigArr = new MapConfig[MaxLevel];
        ShooterPosArr = new Vector3[MaxLevel];
       m_Loader = QFramework.ResLoader.Allocate();
        //
        MapConfig shooterPoses = LoadMapConfig("ShooterPoses");
        ShooterPosArr = shooterPoses.pathPointList.ToArray() ;
        //
        for (int i = 0; i < 3; i++)
        {
            MapConfigArr[i] = LoadMapConfig(i.ToString());
            BgSpriteArr[i]  = m_Loader.LoadSync<Sprite>(i.ToString());//1,2,3命名的图片
        }
    }



    MapConfig LoadMapConfig(string p_ABName)
    {
#if UNITY_EDITOR
        //MapConfig mapConfig = m_Loader.LoadSync<MapConfig>(p_ABName+"_asset") ;
        //MapConfig mapConfig = m_Loader.LoadSync<ScriptableObject>(p_ABName+"_asset") as MapConfig;
         MapConfig mapConfig = AssetDatabase.LoadAssetAtPath<MapConfig>("Assets/Map/" + p_ABName+".asset");
        if (mapConfig == null)
        {
            throw new System.Exception("异常LoadMapConfig："+ p_ABName);   
        }
        return mapConfig;
#endif
        return null;
    }



}
