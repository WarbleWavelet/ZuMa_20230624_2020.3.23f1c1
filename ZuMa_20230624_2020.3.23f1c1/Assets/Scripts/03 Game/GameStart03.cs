/****************************************************
    文件：GameStart03.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/6 2:55:48
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using QFramework;
using QFramework.Example;
using QFramework.PointGame;
using QFramework.TimeExtend;
using GamePanel = QFramework.Example.GamePanel;

public class GameStart03 : MonoBehaviour
{
    #region 属性
    public int LevelIdx;
    /// <summary>没打AB包时再去拖预制体</summary>
    public QFramework.Example.GamePanel gamePanel;
    #endregion


    #region 生命

    /// <summary>首次载入</summary>
    void Awake()
    {
        if (true)
        {
            ResKit.Init();
            UIKit.OpenPanel<StartGamePanel>( );
            //UIKit.OpenPanel<GamePanel>(new GamePanelData() { LevelCount = LevelIdx });

         
        }
        else//没打AB包时 
        { 
             gamePanel.Init();
        }
    }
        

    /// <summary>Go激活</summary>
    void OnEnable ()
    {
            
    }

    /// <summary>首次载入且Go激活</summary>
    void Start()
    {
            
    }

        /// <summary>固定更新</summary>
    void FixedUpdate()
    {
            
    }

    void Update()
    {
            
    }

        /// <summary>延迟更新。适用于跟随逻辑</summary>
    void LateUpdae()
    {
            
    }

    /// <summary> 组件重设为默认值时（只用于编辑状态）</summary>
    void Reset()
    {
            
    }
      

    /// <summary>当对象设置为不可用时</summary>
    void OnDisable()
    {
            
    }


    /// <summary>组件销毁时调用</summary>
    void OnDestroy()
    {
            
    }
    #endregion 

    #region 系统

    #endregion 

    #region 辅助

    #endregion

}




