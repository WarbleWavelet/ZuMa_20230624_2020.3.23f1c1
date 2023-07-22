/****************************************************
    文件：UniRxExtensions.Subject.cs
	作者：lenovo
    邮箱: 
    日期：2023/6/7 17:48:10
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;
 

public static partial class UniRxExtensions
{
    #region 属性
    static Subject<int> mSubject = new Subject<int>();
    static Subject<Tuple<int, int, int>> mTupleSubject = new Subject<Tuple<int, int, int>>();
    static Action<int> mAction;
    static UnityEvent mUnityEvent;
    static event Action mEvent;
    #endregion


    static void Example(MonoBehaviour mono)
    {
        mAction = index => { };
        mAction = null;
        //
        mSubject.Subscribe(index => { }).AddTo(mono)  ;
        mSubject.OnNext(0);
        //
        mUnityEvent.AddListener(Call);//常规
        mUnityEvent.AsObservable() //引入UniRx
            .Subscribe(unit => Call())
            .AddTo(mono);
        //
        Observable.FromEvent(action => mEvent += action ,
        action => mEvent -= action)
            .Subscribe(unit => { 
            
            })
            .AddTo(mono);
        mEvent.Invoke();
        //
        mTupleSubject.Subscribe(tuple => { (int a, int b, int c) = tuple; });
        mTupleSubject.OnNext(new Tuple<int, int, int>(1,2,3));
      
    }

    private static void DestroyUnityEvent()
    {
        mUnityEvent.RemoveListener(Call);
    }

    private static void Call()
    {

        Debug.Log("Call");
    }

    #region 系统

    #endregion

    #region 辅助

    #endregion

}




