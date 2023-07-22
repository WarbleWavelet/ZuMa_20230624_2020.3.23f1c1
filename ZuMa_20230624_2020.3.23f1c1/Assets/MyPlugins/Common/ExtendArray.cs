/****************************************************
    文件：ExtendArray.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/18 22:12:42
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 

    public static class ExtendArray 
    {
    #region 属性

    #endregion




    #region 生命

    public static void Example()
    {
        Example01();
        Example02();

    }
    public static void Example01()
    {
        int[] arr0 = new int[]{ };
        int[] arr1 = new int[5];
    }
    public static void Example02()
    { 
        int[,] arr0 = new int[2, 3] {
            {1,2,3}, //第0行的数据
            {4,5,6} //第1行的数据
        };


        int[,] arr1 = {
            {1,2,3},
            {4,5,6}
        };    
    }

    #endregion  


    #region 系统

    #endregion

    #region 辅助


    public static int[,] Foreach(this int[,] p_Array)
    {
        for (int i = 0; i < p_Array.GetLength(0); i++)
        {
            for (int j = 0; j < p_Array.GetLength(1); j++)
            {
                Debug.Log(p_Array[i, j]);
            }
        }

        return p_Array;
    }
    #endregion

}




