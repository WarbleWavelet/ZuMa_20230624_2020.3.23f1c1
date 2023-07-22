/****************************************************
    文件：UniRxExtensionsMonoBehaviour.Scene.cs
	作者：lenovo
    邮箱: 
    日期：2023/6/6 18:6:33
	功能：
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;
 

public partial  class UniRxExtensionsMonoBehaviour : MonoBehaviour
{
    //意思是一部分放Start，一部分放Update，单用Dic好像搞错了，而且也不每关
    Dictionary<Action,Action> startUpdateDic=new Dictionary<Action,Action>(); 
    ReactiveProperty<int> numRp=new ReactiveProperty<int>(0);
    //
    IDisposable disposable;
    CompositeDisposable compositeDisposable;


    /// <summary>有场景交互部分</summary>
    private void Example_Scene()
    {


        gameObject.FindTop("Go").ExampleGameObject(this );
        numRp.BindInt(this);
        //
        GameObject Cube1 = gameObject.FindTop("Cube1");
        Cube1.DoWhenCollide();
        Cube1.DoWhenMouseDown();
        //
        disposable = gameObject.DoUpdateDispose();
        //
        disposable = gameObject.DoUpdateDispose(compositeDisposable);
        //
        disposable = gameObject.DoUpdateDispose(compositeDisposable);
        compositeDisposable.AddTo(gameObject);



    }

    private void Example_SceneUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            numRp.Value++;
            //
            disposable.Dispose();
            compositeDisposable.Dispose();
        }
    }


}




