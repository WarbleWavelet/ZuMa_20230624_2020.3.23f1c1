/****************************************************
    文件：ExtendUnityOthers.cs
	作者：lenovo
    邮箱: 
    日期：2023/6/20 17:16:44
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using System.Runtime.CompilerServices;

public static class ExtendVector2
{
    public static Vector2Int LeftUp(this Vector2Int v)
    {
        return new Vector2Int(-1,1);
    }

    public static Vector2Int LeftDown(this Vector2Int v)
    {
        return new Vector2Int(-1, -1);
    }

    public static Vector2Int RightUp(this Vector2Int v)
    {
        return new Vector2Int(1, 1);
    }

    public static Vector2Int RightDown(this Vector2Int v)
    {
        return new Vector2Int(1, -1);
    }

}






