using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConfig : ScriptableObject
{
    public float EndPoint { get; private set; }

    public List<Vector3> pathPointList = new List<Vector3>();

    public void InitMapConfig()
    {
        EndPoint = pathPointList.Count - 2;
    }

    public Vector3 GetPosition(float progress)
    {
        Camera ui=Camera.main.gameObject.FindComponentWithTag<Camera>("UI");
        int index = Mathf.FloorToInt(progress);
         //return Vector3.Lerp(pathPointList[index], pathPointList[index + 1], progress - index);

        Vector3 v1 = Vector3.Lerp(pathPointList[index], pathPointList[index + 1], progress - index);

        return v1*230f;
    }

}
