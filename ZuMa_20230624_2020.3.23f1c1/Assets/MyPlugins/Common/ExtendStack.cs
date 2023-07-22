/****************************************************
    文件：ExtendStack.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/17 20:50:23
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public static class ExtendStack
{
    public static Stack Add(this Stack stack,object obj)
    {
        stack.Push(obj);
        return stack;
    }

    public static Stack Remove(this Stack stack)
    {
        stack.Pop();
        return stack;
    }

    public static object Get(this Stack stack)
    {
       return stack.Pop();
    }

}




