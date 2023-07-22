using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool<T>
{
    /// <summary> 计数器 </summary>
    public  int counter = 0;
    private List<T> m_Pool = new List<T>();
    private Func<T> m_Func;

    public ObjectPool(Func<T> func, int count)
    {
        this.m_Func = func;
        InstantiateObject(count);
    }


    /// <summary>
    /// 从对象池里面取东西
    /// </summary>
    /// <returns></returns>
    public T GetObject()
    {
        int i = m_Pool.Count;
        while (i-- > 0)
        {
            T t = m_Pool[i];
            m_Pool.RemoveAt(i);
            return t;
        }

        InstantiateObject(3);
        return GetObject();
    }


    public void AddObject(T t)
    {
        m_Pool.Add(t);
    }


    /// <summary>
    /// 实例化对象
    /// </summary>
    /// <param name="count"></param>
    private void InstantiateObject(int count)
    {
        for (int i = 0; i < count; i++)
        {
            m_Pool.Add(m_Func());
        }
    }
}
