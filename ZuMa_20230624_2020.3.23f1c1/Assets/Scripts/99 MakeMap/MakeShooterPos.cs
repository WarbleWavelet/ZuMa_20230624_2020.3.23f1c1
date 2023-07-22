/****************************************************
    文件：MakeShooterPos.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/19 15:37:17
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;
 

public class MakeShooterPos : MonoBehaviour
{
#if UNITY_EDITOR
    /// <summary>
    /// pathPointList写入"Assets/Map/map.asset"
    /// 但没有覆盖功能，删掉再创建就看得见效果了
    /// </summary> 
    public void RecordeShooterPos()
    {
        MapConfig fromAsset= AssetDatabase.LoadAssetAtPath<MapConfig>("Assets/Map/ShooterPoses.asset");
        int idx = int.Parse( gameObject.name);
        Transform shooter = GameObject.Find("ShooterTrans").transform;
        fromAsset.pathPointList[idx] = shooter.localPosition;
        EditorUtility.SetDirty(fromAsset);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif

}

#if UNITY_EDITOR
[CustomEditor(typeof(MakeShooterPos))]
public class MakeShooterPosEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("生成Shooter位置"))//详情面板下的按钮
        {
            (target as MakeShooterPos).RecordeShooterPos();
        }
        AssetDatabase.Refresh();
    }
}
#endif




