/****************************************************
    文件：TestBackPanel.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/23 2:13:29
	功能：
*****************************************************/

using QFramework.Example;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 
public class TestBackPanel : MonoBehaviour
{
    #region 属性

    #endregion

    #region 生命

    /// <summary>首次载入</summary>
    void Awake()
    {

    }
        

    /// <summary>Go激活</summary>
    void OnEnable ()
    {
            
    }

    /// <summary>首次载入且Go激活</summary>
    void Start()
    {
        ResKit.Init();
        UIKit.Stack.Push(UIKit.OpenPanel<GameOverPanel>()); //压进去没打开
        var succ = UIKit.OpenPanel<SuccPanel>(); //先打开
        this.DoAfterTime(5f, () => UIKit.Back(succ));//返回之前压的面板            
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




