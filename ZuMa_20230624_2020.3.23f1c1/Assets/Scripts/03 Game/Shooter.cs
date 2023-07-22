//using Markdig.Extensions.Tables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{

    [SerializeField] private Image BallImg;
    [SerializeField] private Transform ShooterTrans;
    //
    private BallType curBallType;
    bool init = false;



    #region 生命

    /// <summary>背景，发射球的位置由GameSceneConfig控制</summary>
    public void Init(Transform Ball,Transform ShooterTrans)
    {
        init = false;
        this.ShooterTrans = ShooterTrans;
        this.BallImg = Ball.GetComponent<Image>();
        //
        RefreshBallType();
        init = true;
    }


    private void Update()                       
    {
        if (init==false)
        {
            return;
        }
        if (EventSystem.current.IsPointerOverGameObject() )
        { 
            return;
        } 

        if (Input.GetMouseButton(0))
        {
            ShooterTrans.RotateByMouse();
        }
        if (Input.GetMouseButtonUp(0))
        {
            Shoot();
        }
    }
    #endregion  




    /// <summary>
    /// 刷新球的类型
    /// </summary>
    private void RefreshBallType()
    {
        curBallType = GameStrategy.SpawnShootBallStrategy();
        BallImg.sprite = GameManager.Instance.GetSpriteByBallType(curBallType);
        BallImg.Show();
    }


    /// <summary>发射一个球
    /// </summary>
    private void Shoot()
    {
        if (BallImg.gameObject.activeSelf == false)
        { 
             return;
        }
        SoundManager.PlayShoot();
        ShootBallManager.Instance.Shoot(curBallType, BallImg.sprite, ShooterTrans);
        //
        BallImg.Hide();
        ScheduleOnce.Start(this, RefreshBallType, 0.2f);//0.2s后刷新下一次球的图片
    }
}










