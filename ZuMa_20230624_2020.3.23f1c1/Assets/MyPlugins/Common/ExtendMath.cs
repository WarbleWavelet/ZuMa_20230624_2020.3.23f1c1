/****************************************************
    文件：ExtendMath.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/21 22:42:47
	功能：
*****************************************************/

using log4net.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 

public static class ExtendMath
{


    /// <summary>以某点为圆心，沿着它一圈圆形地实例物体</summary>
    public static void InstantiateByCircle(this GameObject prefab
        , UnityEngine.Transform centerTrans
        , int numberOfObjects = 20
        , float radius = 200f)//200UGUI,5world
    {

        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            Vector3 pos = centerTrans.position + new Vector3(x, 0, z);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
           GameObject.Instantiate(prefab, pos, rot).SetParent(centerTrans);
        }

    }

}


