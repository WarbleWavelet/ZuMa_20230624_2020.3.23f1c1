/****************************************************
    文件：UniRxExtensionsMonoBehaviour.Start.cs
	作者：lenovo
    邮箱: 
    日期：2023/6/8 22:40:42
	功能：生命函数放这里
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    using Random = UnityEngine.Random;
 

    public partial class UniRxExtensionsMonoBehaviour : MonoBehaviour
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
            
            //UniRxExtensions.ExampleMonoBehaviour(this);
            //UniRxExtensions.ExampleGameObject(gameObject,this);
            //UniRxExtensions.ExampleStart_Keyword(gameObject);
            //Example_Scene();
            //Example_ToObservable();
            ExampleStart_Message();

        }

         /// <summary>固定更新</summary>
        void FixedUpdate()
        {
            
        }

        void Update()
        {
            Example_SceneUpdate();
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



