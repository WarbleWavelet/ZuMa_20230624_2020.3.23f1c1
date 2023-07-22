using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ResLoader = QFramework.ResLoader;

public class FXManager :MonoSingleton<FXManager>
{
       Transform m_FXs;
   //
    GameObject m_DestroyFXPrefab;
    ObjectPool<GameObject> m_DestroyFXPool;
 

                                                                
    public void Init(Transform p_FXs)
    {
        m_FXs = p_FXs;
        //
        ResKit.Init();
        QFramework.ResLoader loader =  QFramework.ResLoader.Allocate();
        m_DestroyFXPrefab = loader.LoadSync<GameObject>("DestroyFX"); 
        m_DestroyFXPool = new ObjectPool<GameObject>(InstantiateFX, 10);
    }


    private GameObject InstantiateFX()
    {
        GameObject fx = Instantiate(m_DestroyFXPrefab, m_FXs);
        fx.Hide();
        return fx;
    }


    public void ShowDestroyFX(Vector3 p_Pos)
    {
        GameObject fx = m_DestroyFXPool.GetObject();
        fx.Show();
        fx.transform.localPosition = p_Pos;

        //延时0.5f执行回收操作
        ScheduleOnce.Start(this, () =>
         {
             if (fx != null) //有时切得太快，报空
             { 
                 fx.Hide();
                 m_DestroyFXPool.AddObject(fx);
             
             }
         }, 0.5f);
    }
}
