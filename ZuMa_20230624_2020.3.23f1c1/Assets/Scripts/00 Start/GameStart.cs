/****************************************************
    文件：GameStart.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/2 22:59:41
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using QFramework;


namespace QFramework.Example
{
    public class GameStart : MonoBehaviour
    {
        #region 属性

        #endregion

        #region 生命

        /// <summary>首次载入</summary>
        void Awake()
        {
            ResKit.Init();
            UIKit.OpenPanel<StartGamePanel>();

        }
        

        #endregion 


    }

}



