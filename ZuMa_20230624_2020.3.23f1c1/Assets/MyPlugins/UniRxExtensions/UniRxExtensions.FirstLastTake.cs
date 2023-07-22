/****************************************************
    文件：UniRxExtensions.FirstLastTake.cs
	作者：lenovo
    邮箱: 
    日期：2023/6/7 17:48:10
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


public static partial class UniRxExtensions
{
    public static void ExampleStart_Keyword(GameObject go)
    {
        //go.DoFirst();
        //go.DoLast();
        //go.DoTake(3);
        //go.DoBuffer(60 * 25);
        //go.DoMerge();
        //go.DoReturn();
        go.Do();
    }

    public static void DoFirst(this GameObject go)
    {
        go
            .UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            .First(unit => true)
            .Subscribe(_ => Debug.Log("DoFirst"))
            .AddTo(go);
    }


    /// <summary>
    /// 返回List集合序列中的第一个符合条件的元素，如果没有查找到
    /// <para/>则返回对应默认值，如引用类型对象的话则返回null。
    /// </summary>
    public static void DoFirstOrDefault(this GameObject go)
    {
        go
            .UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            .FirstOrDefault(unit => true)
            .Subscribe(_ => Debug.Log("DoFirstOrDefault"))
            .AddTo(go);
    }

    public static void DoLast(this GameObject go)
    {
        go
            .UpdateAsObservable()
            .Where(unit => Input.GetMouseButtonDown(0))
            .Last(unit => true)
            .Subscribe(_ => Debug.Log("DoLast"))
            .AddTo(go);
    }

    public static void DoTake(this GameObject go, int cnt)
    {
        go
            .UpdateAsObservable()
            .Where(unit => Input.GetMouseButtonDown(0))
            .Take(cnt)
            .Subscribe(_ => Debug.Log("DoTake"))
            .AddTo(go);
    }


    /// <summary>每cnt帧的时间输出一次</summary>
    public static void DoBuffer(this GameObject go, int cnt)
    {
        go
            .UpdateAsObservable()
            .Buffer(cnt)
            .Subscribe(_ => Debug.Log("DoBuffer"))
            .AddTo(go);

    }

    public static void DoMerge(this GameObject go)
    {
        IObservable<Unit> disposable0 = go.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0));

        IObservable<Unit> disposable1 = go.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(1));

        disposable0.Merge(disposable1)
            .Subscribe(_ => Debug.Log("Mereg单击"))
            .AddTo(go);

        //相当于
        //IObservable<Unit> disposable3 = go.UpdateAsObservable()
        //  .Where(_ => Input.GetMouseButtonDown(0)  || Input.GetMouseButtonDown(1));

    }
    public static void DoReturn(this GameObject obj)
    {

        Debug.Log("DoReturn的前面");
        Observable
            .Return("DoReturn")
            .Subscribe(Debug.Log)
            .AddTo(obj);


    }

    /// <summary>提前筛选</summary> 
    public static void Do(this GameObject go)
    {
        go.UpdateAsObservable()
            .Do(_ => Debug.Log("Do"))
            .Subscribe(_ => Debug.Log("Do的Subscribe"))//不订阅就不会带引Do
            .AddTo(go);

    }


    /// <summary>与Where、Delay等结合使用</summary>
    public static void DoWithDo(this GameObject go)
    {
        go.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
            .Do(_ => Debug.Log("DoWhereDo按下了鼠标"))
            .Delay(TimeSpan.FromSeconds(2f))
            .Do(_ => Debug.Log("DoWhereDo延时2秒"))
            .Subscribe(_ => Debug.Log("DoWhereDo的Subscribe"))//不订阅就不会带引Do
            .AddTo(go);

    }


    public static void DoStartWith(this GameObject go)
    {
        Observable
            .Return("baidu.com")
            .StartWith("https://")
            .Subscribe(_ => Debug.Log("DoStartWith的Subscribe"))
            .AddTo(go);
    }

}



