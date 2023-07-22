/****************************************************
    文件：UniRxExtensions.ToObservable.cs
	作者：lenovo
    邮箱: 
    日期：2023/6/7 17:48:10
	功能：各种协程转Observable
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


public partial  class UniRxExtensionsMonoBehaviour:MonoBehaviour
{
    void Example_ToObservable()
    {
        //IEnumeratorBody2Observable(enumerator());
        IEnumeratorBody2Observable_TryCatch(enumerator_TryCatch());

        //
        //IEnumerator2ObservableBody();
        //IEnumerator2ObservableBody_TryCatch();

    }



    #region 辅助

    /// <summary>转，但方法块在IEnumeratorBody</summary>
    void IEnumeratorBody2Observable( IEnumerator from)
    {
        from.ToObservable()
            .DoOnCompleted(()=>  Debug.Log("ConvertToObservable")) //这里_（报错）与()(不报错)是不同的
            .Subscribe( _ =>  Debug.Log("ConvertToObservable的Subscribe"))
            .AddTo(this);
    }

    /// <summary>转，但方法块在IEnumeratorBody</summary>
    void IEnumeratorBody2Observable_TryCatch(IEnumerator from)
    {
        from.ToObservable()
            .Catch<Unit, Exception>(expcetion =>
            {
                Debug.LogError("IEnumeratorBody2Observable_TryCatch的Catch<Unit, Exception>");
                return Observable.ReturnUnit();
            })
            .DoOnCompleted(() => Debug.Log("IEnumeratorBody2Observable_TryCatch的DoOnCompleted（该异常不会阻碍DoOnCompleted的打印）")) //这里_（报错）与()(不报错)是不同的
            .Subscribe(_ => Debug.Log("IEnumeratorBody2Observable_TryCatch的Subscribe"))
            .AddTo(this);
    }

    IEnumerator enumerator(Action onComplted = null)
    {

        Debug.Log("enumerator的前");
        yield return null;
        Debug.Log("enumerator的后");
        onComplted?.Invoke();
    }

    IEnumerator enumerator_TryCatch(Action onComplted = null)
    {
        int index = 0;
        Debug.Log("enumerator的前");
        yield return null;
        Debug.Log("enumerator的后");
        int[] arr = new int[0];
         index = arr[0];
    }


    IObservable<Unit> Obs()
    {
        IObservable<Unit> res = Observable.ReturnUnit();
        res.Do(_ => Debug.Log("IObservable<Unit>的前"))
            .DelayFrame(5)
            .Do(_ => Debug.Log("IObservable<Unit>的后"));

        return res;
    }


    /// <summary>IObservable<Unit>解决IEnumerator的异常捕捉</summary>
    IObservable<Unit> ObsTryCatch()
    {
        int index = 0;
        IObservable<Unit> res = Observable.ReturnUnit();
        res=res
            .Do(_ => Debug.Log("IObservable<Unit>的前"))
            .DelayFrame(5)
            .Do(_ => Debug.Log("IObservable<Unit>的后"))
            .Do( _=>
            {
                int[] arr = new int[0];
                index = arr[0];
            })
            .Catch<Unit,Exception>( expcetion=>               
            {

                Debug.LogError(" IObservable<Unit>的异常");
                return Observable.ReturnUnit();
            });

        return res;
    }


    /// <summary>转，但方法块在Observable</summary>
    void IEnumerator2ObservableBody()
    {
        Obs()
            .DoOnCompleted(() => Debug.Log("IObservable<Unit>的DoOnCompleted"))
            .Subscribe()
            .AddTo(this);

        StartCoroutine(Obs()
            .DoOnCompleted(() => 
                Debug.Log("IObservable<Unit>的StartCoroutine的DoOnCompleted"))
            .ToYieldInstruction());
    }

    /// <summary>转，但方法块在Observable</summary>
    void IEnumerator2ObservableBody_TryCatch()
    {
        ObsTryCatch()
            .DoOnCompleted(() => Debug.Log("IObservable<Unit>的TryCatch的DoOnCompleted"))
            .Subscribe()
            .AddTo(this);

        StartCoroutine(ObsTryCatch()
            .DoOnCompleted(() =>
                Debug.Log("IObservable<Unit>的TryCatch的StartCoroutine的DoOnCompleted"))
            .ToYieldInstruction());
    }
    #endregion



}



