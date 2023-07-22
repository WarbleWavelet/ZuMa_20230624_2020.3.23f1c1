/****************************************************
    文件：ExtendCamera.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/9 19:31:26
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public static class ExtendCamera
{


    #region 2Vector3
    // Screen (0-1920) x (0-1080)
    // View 0-1
    // World (0,0,0)
    // local


    public static Vector3 View2World( this Vector3 from)
    {
        return  Camera.main.ViewportToWorldPoint(from); 
    }


    public static Vector3 View2Screen(this Vector3 from)
    {
        return Camera.main.ViewportToScreenPoint(from);
    }


    public static Vector3 World2View(this Vector3 from)
    {
        return Camera.main.WorldToViewportPoint(from);
    }


    public static Vector3 World2Screen(this Vector3 from)
    {
        return Camera.main.WorldToScreenPoint(from);
    }

    public static Vector3 Screen2View(this Vector3 from)
    {
        return Camera.main.ScreenToViewportPoint(from);
    }

    public static Vector3 Screen2World(this Vector3 from)
    {
        return Camera.main.ScreenToWorldPoint(from);
    }

    /// ////////////////////////////////////////////////////////// <summary>
    public static Vector3 View2World(this Vector3 from,Camera camera)
    {
        return camera.ViewportToWorldPoint(from);
    }


    public static Vector3 View2Screen(this Vector3 from, Camera camera)
    {
        return camera.ViewportToScreenPoint(from);
    }


    public static Vector3 World2View(this Vector3 from, Camera camera)
    {
        return camera.WorldToViewportPoint(from);
    }


    public static Vector3 World2Screen(this Vector3 from, Camera camera)
    {
        return camera.WorldToScreenPoint(from);
    }

    public static Vector3 Screen2View(this Vector3 from, Camera camera)
    {
        return camera.ScreenToViewportPoint(from);
    }

    public static Vector3 Screen2World(this Vector3 from, Camera camera)
    {
        return camera.ScreenToWorldPoint(from);
    }




    #endregion



    #region 2Ray


    public static Ray Screen2Ray(this Vector3 from)
    {
        return Camera.main.ScreenPointToRay(from);
    }

    public static Ray View2Ray(this Vector3 from)
    {
        return Camera.main.ViewportPointToRay(from);
    }
    #endregion


    public static Camera UICamera(this GameObject go)
    {
        return go.FindComponentWithTag<Camera>("UI");
    }
}





