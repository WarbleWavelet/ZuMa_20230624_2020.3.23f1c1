using QAssetBundle;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBallManager : MonoSingleton<ShootBallManager>
{

    private Transform m_Balls;//Balls
                              //
    private GameObject m_BallPrefab;
    private ObjectPool<ShootBall> m_Pool;
    /// <summary> 存放当前发射的球集合 </summary>

   public List<ShootBall> ShootBallList = new List<ShootBall>();



    #region 生命


    public void Init(Transform p_Balls)
    {
        this.m_Balls = p_Balls;
        //
        ShootBallList = new List<ShootBall>();
        ResKit.Init();
        QFramework.ResLoader loader = QFramework.ResLoader.Allocate();
        m_BallPrefab = loader.LoadSync<GameObject>("Ball");
        m_Pool = new ObjectPool<ShootBall>(InstantiateBall, 3);
    }


     void Update()
    {

        if (GameManager.Instance.GetGameState()!=GameState.Game)
        {
            return;
        }
        for (int i = ShootBallList.Count - 1; i >= 0; i--)
        {
            ShootBallList[i].Move();
            if (ShootBallList[i].IsOutOfBounds())
            {
                Recovery(ShootBallList[i]);
                ShootBallList.RemoveAt(i);
            }
        }
    }
    #endregion



    #region 辅助


    private ShootBall InstantiateBall()
    {
        GameObject ball = Instantiate(m_BallPrefab, m_Balls);
        ball.Hide();
        ShootBall shootBall = ball.AddComponent<ShootBall>();
        return shootBall;
    }



    /// <summary>
    /// 回收到对象池
    /// </summary>
    /// <param name="ball"></param>
    public void Recovery(ShootBall ball)
    {
        ball.Hide();
        m_Pool.AddObject(ball);
    }


    /// <summary>
    /// 发射一个球
    /// </summary>
    /// <param name="type"></param>
    public void Shoot(BallType type, Sprite sp, Transform shooterTrans)
    {
        ShootBall shootBall = m_Pool.GetObject();
        shootBall.Init(type, sp, shooterTrans);
        ShootBallList.Add(shootBall);
    }
    #endregion  



}
