using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
using System;
using UnityEngine.UI;

public class BezierPathController : MonoBehaviour
{

    #region 字属



    public int segmentsPerCurve = 3000;
    /// <summary>连线上求和球之间的举例，也就是比直径大一点</summary>
    public float BallAndBallDis = 0.3f;
    public bool Debug = true;
    public GameObject ballPrefab;

    /// <summary>贝塞尔曲线的节点。控制弯曲度的白球</summary>
    public List<GameObject> ControlPointList = new List<GameObject>();
    /// <summary>贝塞尔曲线的的线段。连线的蓝球的坐标</summary>
    public List<Vector3> pathPointList = new List<Vector3>();
    #endregion


    private void Awake()
    {
        Debug = true;
        foreach (var item in pathPointList)
        {
            GameObject ball = Instantiate(ballPrefab, GameObject.Find("Map").transform);
            ball.transform.position = item;
        }
    }


    private void OnDrawGizmos()
    {
        //节点
        ControlPointList.Clear();
        foreach (Transform item in transform)//没错，就是遍历子节点
        {
            ControlPointList.Add(item.gameObject);
        }


        //线段
        List<Vector3> controlPointPos 
            = ControlPointList.Select(point => point.transform.position).ToList();
        var points = GetDrawingPoints(controlPointPos, segmentsPerCurve);

        Vector3 startPos = points[0];
        pathPointList.Clear();
        pathPointList.Add(startPos);
        for (int i = 1; i < points.Count; i++)
        {
            if (Vector3.Distance(startPos, points[i]) >= BallAndBallDis)
            {
                startPos = points[i];
                pathPointList.Add(startPos);
            }
        }

        foreach (var item in ControlPointList)
        {
            item.GetComponent<Image>().enabled = Debug;//相当于将物体隐身，并不会影响物体的脚本运行，物体的碰撞体也依然存在。
        }

        if (Debug == false)
        { 
            return;
        } 


        //01 画连线球的球
        Gizmos.color = Color.blue;
        foreach (var pos in pathPointList)
        {
            Gizmos.DrawSphere(pos, BallAndBallDis / 2);
        }

        //02 画连线球的线
        Gizmos.color = Color.yellow;
        for (int i = 0; i < points.Count - 1; i++)
        {
            Gizmos.DrawLine(points[i], points[i + 1]);
        }

        //03 画连线球的的弯曲度控制线
        //绘制贝塞尔曲线控制点连线，红，色
        Gizmos.color = Color.red;
        for (int i = 0; i < controlPointPos.Count - 1; i++)
        {
            Gizmos.DrawLine(controlPointPos[i], controlPointPos[i + 1]);
        }

    }



    #region 辅助


    /// <summary>贝塞尔线段</summary>
    List<Vector3> GetDrawingPoints(List<Vector3> controlPoints, int segmentsPerCurve)
    {
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < controlPoints.Count - 3; i += 3)
        {
            var p0 = controlPoints[i];
            var p1 = controlPoints[i + 1];
            var p2 = controlPoints[i + 2];
            var p3 = controlPoints[i + 3];

            for (int j = 0; j <= segmentsPerCurve; j++)
            {
                var t = j / (float)segmentsPerCurve;
                points.Add(CalculateBezierPoint(t, p0, p1, p2, p3));
            }
        }
        return points;
    }

    /// <summary>
    /// <summary>贝塞尔曲线的三次方公式</summary>
    /// </summary>
    /// <param name="t"></param>
    /// <param name="p0">起点</param>
    /// <param name="p1">一侧的平滑度调节点</param>
    /// <param name="p2">另一侧的平滑度调节点</param>
    /// <param name="p3">终点</param>
    /// <returns></returns>
    Vector3 CalculateBezierPoint(float t
        , Vector3 p0
        , Vector3 p1, Vector3 p2
        , Vector3 p3)
    {
        var x   = 1 - t;
        var xx  = x * x;
        var xxx = x * x * x;
        var tt  = t * t;
        var ttt = t * t * t;
        return p0 * xxx 
            +   3 * p1 * t * xx 
            +   3 * p2 * tt * x 
            +  p3 * ttt;
    }


#if UNITY_EDITOR
    /// <summary>
    /// pathPointList写入"Assets/Map/map.asset"
    /// 但没有覆盖功能，删掉再创建就看得见效果了
    /// </summary> 
    public void CreateMapAsset()
    {
        string assetPath =String.Format(  "Assets/Map/{0}.asset",gameObject.name);  //写这Vector3数据的
        MapConfig mapConfig = new MapConfig();
        foreach (Vector3 item in pathPointList)
        {
            mapConfig.pathPointList.Add(item);
        }
        AssetDatabase.CreateAsset(mapConfig, assetPath);
        AssetDatabase.SaveAssets();
    }
#endif


    #endregion


}


#if UNITY_EDITOR
[CustomEditor(typeof(BezierPathController))]
public class BezierEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("生成地图文件"))//详情面板下的按钮
        {
            (target as BezierPathController).CreateMapAsset();
        }
        AssetDatabase.Refresh();
    }
}
#endif


