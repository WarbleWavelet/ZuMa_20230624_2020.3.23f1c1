/****************************************************
    文件：ExtendClass.cs
	作者：lenovo
    邮箱: 
    日期：2023/5/24 20:9:6
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 

public static partial class ExtendClass 
{
    public static void Example()
    {
        var simpleClass = new object();

        if (simpleClass.IsNull()) // simpleClass == null
        {
            // do sth
        }
        else if (simpleClass.IsNotNull()) // simpleClasss != null
        {
            // do sth
        }
    }

    public static bool IsNull<T>(this T selfObj) where T : class
    {
        return null == selfObj;
    }

    public static bool IsNotNull<T>(this T selfObj) where T : class
    {
        return null != selfObj;
    }

}




