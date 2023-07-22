using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{


    #region 字属


    public float Progress = 0f;
    /// <summary> 是否销毁标记  </summary>
    public bool DeleteFlag = false;

    /// <summary> 回退到哪一个球后面 </summary>
    public Ball FallbackTarget;

    public BallType ballType;

    private GameManager m_GameManager;

    private Image img;

    public Ball Next { get; set; }
    public Ball Pre { get; set; }

    /// <summary>
    /// 获取当前这个球所在段的尾球
    /// </summary>
    public Ball Tail
    {
        get
        {
            Ball ball = this;
            do
            {
                if (ball.Next == null)
                { 
                    return ball;
                } 
                ball = ball.Next;
            } while (true);
        }
    }
    /// <summary>
    /// 头部球
    /// </summary>
    public Ball Head
    {
        get
        {
            Ball ball = this;
            do
            {
                if (ball.Pre == null)
                { 
                    return ball;
                } 
                ball = ball.Pre;
            } while (true);
        }
    }
    #endregion



    #region 生命



    /// <summary>  只有在生成的时候调用一次 </summary>
    public void SpawnInit()
    {
        img = GetComponent<Image>();
    }


    /// <summary>
    /// 每一次显示都需要调用此方法进行初始化
    /// </summary>
    /// <param name="gm"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public Ball Init(GameManager gm, BallType type)
    {
        ballType = type;
        m_GameManager = gm;
        Sprite sp = m_GameManager.GetSpriteByBallType(ballType);
        if (sp != null)
        { 
            img.sprite = sp;
        }

        transform.position = new Vector3(100, 100, 100);
        gameObject.Show();
        Progress = 0f;
        DeleteFlag = false;
        Next = null;
        Pre  = null;

        return this;
    }


    private void Update()
    {
        if (Progress >= 0)
        { 
            transform.localPosition = GameManager.Instance.MapConfig.GetPosition(Progress);
        }
    }



    public void Recovery()
    {
        m_GameManager.BallPool.AddObject(this);
        gameObject.Hide();
    }
    #endregion




    #region 辅助


    /// <summary>
    /// 获取当前这个球前后相同类型球的数量
    /// </summary>
    /// <returns></returns>
    public int SameColorCount(out List<Ball> list)
    {
        list = new List<Ball>();
        list.Add(this);
        int counter = 1;
        Ball curBall = this;
        do
        {
            if (curBall.Pre != null && curBall.Pre.ballType == ballType)
            {
                list.Add(curBall.Pre);
                counter++;
                curBall = curBall.Pre;
            }
            else
                break;
        } while (curBall != null);

        curBall = this;
        do
        {
            if (curBall.Next != null && curBall.Next.ballType == ballType)
            {
                list.Add(curBall.Next);
                counter++;
                curBall = curBall.Next;
            }
            else break;
        } while (curBall != null);
        return counter;
    }


    /// <summary>
    /// 是否离开开始洞口
    /// </summary>
    /// <returns></returns>
    public bool IsExitStartHole()
    {
        return Progress >= 1f;
    }


    /// <summary>
    /// 是否到达终点洞口
    /// </summary>
    /// <returns></returns>
    public bool IsArriveFailHole()
    {
        return Progress >= m_GameManager.MapConfig.EndPoint;
    }
    #endregion


}
