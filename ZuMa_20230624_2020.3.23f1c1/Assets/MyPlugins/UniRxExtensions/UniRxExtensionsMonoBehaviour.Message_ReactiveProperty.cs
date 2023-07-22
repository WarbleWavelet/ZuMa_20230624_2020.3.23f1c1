/****************************************************
    文件：UniRxExtensionsMonoBehaviour.Message_ReactiveProperty.cs
	作者：lenovo
    邮箱: 
    日期：2023/6/7 17:48:10
	功能：消息机制
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Random = UnityEngine.Random;


public  partial class UniRxExtensionsMonoBehaviour : MonoBehaviour
{

    int msgId = 0;
    ReactiveProperty<int> mReactiveProperty_Int = new ReactiveProperty<int>();
    ReactiveProperty<string> mReactiveProperty_String = new ReactiveProperty<string>();
    static int CurTextID = 3;
    
    
    public void ExampleStart_Message()
    {
        if (CurTextID == 1)
        { 
            MainThreadingMsg_Receive();//注册
            MainThreadingMsg_Publish();//调用
            MainThreadingMsg_Publish();
            MainThreadingMsg_Publish();        
        }

        //
        if (CurTextID == 2)
        {
            ReactivePropertyChangeValue_Int();
            mReactiveProperty_Int.Value++;//调用
            mReactiveProperty_Int.Value++;
            mReactiveProperty_Int.Value++;
        }
        //
        if (CurTextID == 3)
        {
            //ReactivePropertyChangeValue_Int();//如果前面没注册
            ReactivePropertyChangeValue_String();
            mReactiveProperty_String.Value = Guid.NewGuid().ToString();
            mReactiveProperty_Int.Value++;
            mReactiveProperty_String.Value = Guid.NewGuid().ToString();
        }
    }




    #region 辅助

    void ReactivePropertyChangeValue_Int()
    {

        mReactiveProperty_Int
            .Skip(1)//跳过初始化
            .Subscribe(_=>Debug.Log("ReactivePropertyChangeValue_Int的Subscribe"))
            .AddTo(this);
    }


    void ReactivePropertyChangeValue_String()
    {

        mReactiveProperty_String
            .Skip(1)//跳过初始化
            .Subscribe(_ => Debug.Log("ReactivePropertyChangeValue_String的Subscribe"))
            .AddTo(this);
    }

    void ReactiveProperty_Merge()
    {
        mReactiveProperty_Int
           .Select(_ => Unit.Default)
           .Merge(mReactiveProperty_String.Select(_ => Unit.Default))
           .Subscribe(_=>Debug.Log("ReactiveProperty_Merge的Subscribe"))
           .AddTo(this);
    }

        /// <summary>多线程的消息，主线程下一帧Update收</summary>
        void MultiThreadingMsg_Receive()
    {
        MessageBroker.Default.Receive<MsgTmp>()
            .SubscribeOnMainThread()
            .Subscribe(msgTmp =>
                Debug.Log("MainThreadingMsg_Receive的Subscribe" + msgTmp.ToString()))
            .AddTo(this);

    }


    /// <summary>单线程的消息，即发即收</summary>
    void MainThreadingMsg_Publish()
    {
        MessageBroker.Default.Publish(new MsgTmp()
        {
            Idx = msgId,
            Val = "绝区零"+ msgId.ToString(),
           
        }) ;
        msgId++;
    }


     void MainThreadingMsg_Receive()
    {
        MessageBroker.Default.Receive<MsgTmp>()
           .Subscribe(msgTmp => 
            Debug.Log("MainThreadingMsg_Receive的Subscribe"+msgTmp.ToString()))
           .AddTo(this);
   
    }
    #endregion


    #region 内部类

    #endregion
    class MsgTmp
    { 
        public int Idx { get; set; }
        public string Val { get; set; }


        public override string ToString()
        {
            string str = "MsgTmp";
            str += "\tIdx=" + Idx;
            str += "\tVal=" + Val;

            return str;
        }
    }
}




