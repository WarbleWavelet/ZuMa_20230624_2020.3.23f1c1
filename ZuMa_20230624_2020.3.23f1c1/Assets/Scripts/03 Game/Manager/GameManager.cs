using QFramework;
using QFramework.Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




[System.Serializable]
public class BallTypeSprite
{
    public BallType ballType;
    public Sprite sprite;
}


public class GameManager : MonoSingleton<GameManager>
{


    #region 字属


    [SerializeField] Transform BallPoolTrans;

    public MapConfig MapConfig;
    public GameObject BallPrefab;
    public float MoveSpeed = 2f;
    /// <summary>复活时，回退的速度，快点才有视觉冲击</summary>
    private int     m_BackSpeed = 16;
    private float   m_BackTime = 3f;
    public ObjectPool<Ball> BallPool;

    private bool m_IsBomb = false;

    [SerializeField]
    private GameState m_GameState = GameState.Game;
    private MoveState m_MoveState = MoveState.Forword;
    private bool m_Init = false;
    QFramework.ResLoader m_Loader ;

    //

    [SerializeField]
    /// <summary>  球队 段 集合，存放的是首位球  </summary>
    private List<Ball> m_BallSegmentList = new List<Ball>();

    /// <summary>  新插入的球标记为搜索消除集合 </summary>
    private List<Ball> m_SearchDestroyList = new List<Ball>();
    /// <summary>  需要回退的球集合 </summary>
    private List<Ball> m_FallbackList = new List<Ball>();

    public List<BallTypeSprite> BallTypeSpriteList = new List<BallTypeSprite>();

    private Dictionary<BallType, Sprite> m_BallTypeSpriteDic=new Dictionary<BallType, Sprite>();

    #endregion  


    #region 生命


    public void Init(int p_LevelCount, Transform p_BallPoolTrans)
    {
        m_Init = false;
        this.BallPoolTrans = p_BallPoolTrans;
        ResKit.Init();
        m_Loader = QFramework.ResLoader.Allocate();
        //
        //Awake()
        {
            m_BallSegmentList = new List<Ball>();
            m_SearchDestroyList = new List<Ball>();
            m_FallbackList = new List<Ball>();
            Init(out BallTypeSpriteList);
            Init(out m_BallTypeSpriteDic);

            //
            m_BackSpeed = 16;
            m_BackTime = 1f;
            m_GameState = GameState.Game;
            MoveSpeed = 2f;
            BallPrefab = m_Loader.LoadSync<GameObject>("Ball");
            BallPool = new ObjectPool<Ball>(InstantiateBallFunc, 10);
        }

        ////////////////////////////////////////

        MapConfig = GameSceneConfig.Instance.MapConfigArr[p_LevelCount];
        MapConfig.InitMapConfig();
        SoundManager.PlayBg();


        m_Init = true;
    }




    private void Update()
    {
        if (m_Init == false)
        {
            return;
        }

        if (m_GameState == GameState.Game)
        {
            if (m_MoveState == MoveState.Forword)
            {
                FirstSegmentMove();
                CheckGameFail();
            }
            else if (m_MoveState == MoveState.Back)
            {
                SegmentBack();
            }
            ShootBallInsert();
            SearchDestroy();
            CheckFallbackBall();
            BallSegmentConnect();
        }
    }


    #endregion



    #region 辅助


    private void Init(out Dictionary<BallType, Sprite> p_BallTypeSpriteDic)
    {
        p_BallTypeSpriteDic = new Dictionary<BallType, Sprite>();
        foreach (var item in BallTypeSpriteList)
        {
            p_BallTypeSpriteDic.Add(item.ballType, item.sprite);
        }
    }


    private void Init(out List<BallTypeSprite> p_BallTypeSpriteList )
    {
        p_BallTypeSpriteList = new List<BallTypeSprite>();
        p_BallTypeSpriteList.Add(new BallTypeSprite() { ballType = BallType.Bomb,   sprite = LoadSprite("bomb2") });
        p_BallTypeSpriteList.Add(new BallTypeSprite() { ballType = BallType.Red,    sprite = LoadSprite("qiu-red") });
        p_BallTypeSpriteList.Add(new BallTypeSprite() { ballType = BallType.Yellow, sprite = LoadSprite("qiu_01") });
        p_BallTypeSpriteList.Add(new BallTypeSprite() { ballType = BallType.Green,  sprite = LoadSprite("qiu_02") });
        p_BallTypeSpriteList.Add(new BallTypeSprite() { ballType = BallType.Blue,   sprite = LoadSprite("qiu_03") });

    }

    /// <summary>球的名字</summary>
    Sprite LoadSprite(string p_BallName)
    {
        return m_Loader.LoadSync<Sprite>(p_BallName);
    }

    public Sprite GetSpriteByBallType(BallType type)
    {
        if (m_BallTypeSpriteDic.ContainsKey(type) == false)
        {
            return null;
        }
        return m_BallTypeSpriteDic[type];   
    }


    private Ball InstantiateBallFunc()
    {
        GameObject go = Instantiate(BallPrefab, BallPoolTrans);
        go.Hide();
        Ball ball = go.AddComponent<Ball>();
        ball.SpawnInit();
        return ball;
    }






    public GameState GetGameState()
    {
        return m_GameState;
    }

    /// <summary>
    /// 第一段移动
    /// </summary>
    private void FirstSegmentMove()
    {
        int maxCnt = GameStrategy.SpawnBallCount(SceneManager.GetActiveScene().buildIndex);
        //场景中一段球都没有，先初始化一段
        if (BallPool.counter < maxCnt && m_BallSegmentList.Count == 0)
        {
            BallPool.counter++;
            Ball ball = SpawnBall();
            m_BallSegmentList.Add(ball);
            return;
        }

        if (m_BallSegmentList.Count <= 0)
        {
            GamePass();
            return;
        }

        Ball ball0 = m_BallSegmentList[0];
        //如果第一段第一个球已经出了洞口，填充新的球在洞口
        if (BallPool.counter < maxCnt && ball0.IsExitStartHole())
        {
            BallPool.counter++;
            Ball ball = SpawnBall();
            ball.Next = ball0;
            ball0.Pre = ball;

            m_BallSegmentList[0] = ball;
            ball0 = ball;
        }
        ball0.Progress += Time.deltaTime * MoveSpeed;

        while (ball0.Next != null)
        {
            if (ball0.Next.Progress < ball0.Progress + 1)
            {
                ball0.Next.Progress = ball0.Progress + 1;
            }

            ball0 = ball0.Next;
        }
    }





    /// <summary>
    /// 检查游戏结束
    /// 最后一段的最后一个球是否到达终点
    /// </summary>
    private void CheckGameFail()
    {
        int count = m_BallSegmentList.Count;
        if (count == 0)
        {
            return;
        }
        Ball l_Ball = m_BallSegmentList[count - 1];
        if (l_Ball.Tail.IsArriveFailHole())
        {
            GameOver();
        }
    }
    /// <summary>
    /// 发射球的插入
    /// </summary>
    private void ShootBallInsert()
    {
        float l_Distance = 0.3f;
        List<ShootBall> shootBallList = ShootBallManager.Instance.ShootBallList;
        int l_I = shootBallList.Count;
        while (l_I-- > 0)
        {
            bool isHit = false;
            ShootBall shootBall = shootBallList[l_I];

            int j = m_BallSegmentList.Count;
            while (j-- > 0)
            {
                Ball fb = m_BallSegmentList[j];
                do
                {
                    if (shootBall.IsCross(fb.transform.position, l_Distance))
                    {
                        //代表找到了距离射击球最近的点
                        if (shootBall.ballType != BallType.Bomb)
                        {

                            Ball insert = SpawnBall(shootBall.ballType);
                            Ball next = fb.Next;
                            fb.Next = insert;
                            insert.Pre = fb;
                            insert.Next = next;
                            if (next != null) next.Pre = insert;
                            insert.Progress = fb.Progress + 1;
                            m_SearchDestroyList.Add(insert);
                            isHit = true;
                        }
                        else//是炸弹球
                        {
                            fb.DeleteFlag = true;
                            Ball ball = fb.Pre;
                            int count = GameStrategy.BombDestroyCount / 2;
                            while (ball != null && count-- > 0)
                            {
                                ball.DeleteFlag = true;
                                ball = ball.Pre;
                            }
                            count = GameStrategy.BombDestroyCount / 2;
                            ball = fb.Next;
                            while (ball != null && count-- > 0)
                            {
                                ball.DeleteFlag = true;
                                ball = ball.Next;
                            }
                            m_IsBomb = true;
                            SoundManager.PlayBomb();
                        }

                        shootBallList.RemoveAt(l_I);
                        ShootBallManager.Instance.Recovery(shootBall);
                        break;
                    }
                    fb = fb.Next;
                } while (fb != null);

                if (isHit)
                {
                    UpdateBallProgress(m_BallSegmentList[j]);
                    break;
                }
                if (m_IsBomb) break;
            }
        }
    }


    /// <summary>销毁球？</summary>
    private void SearchDestroy()
    {
        bool isSame = false;
        int i = m_SearchDestroyList.Count;
        while (i-- > 0)
        {
            List<Ball> list;
            Ball ball = m_SearchDestroyList[i];
            if (ball.SameColorCount(out list) >= 3)
            {
                isSame = true;
                foreach (var item in list)
                {
                    item.DeleteFlag = true;
                }
                SoundManager.PlayDestroy();
            }
            else
            {
                SoundManager.PlayInsert();
            }
        }
        m_SearchDestroyList.Clear();

        if (isSame == false && m_IsBomb == false)
        {
            return;
        }
        m_IsBomb = false;

        Back();
    }


    void Back()
    { 
                 int x = m_BallSegmentList.Count;
        while (x-- > 0)
        {
            Ball fb = m_BallSegmentList[x];
            Ball head = fb.Head;
            Ball tail = fb.Tail;
            bool isDelete = false;

            do
            {
                if (fb.DeleteFlag)
                {
                    isDelete = true;
                    //切断链接
                    if (fb.Pre != null)
                    { 
                        fb.Pre.Next = null;
                    }
                    if (fb.Next != null)
                    { 
                         fb.Next.Pre = null;
                    }

                    //如果当前这个fb是头部球，就代表头部球被销毁了，就把head置为空
                    if (fb == head) head = null;
                    if (tail == fb) tail = null;

                    FXManager.Instance.ShowDestroyFX(fb.transform.localPosition);
                    fb.Recovery();
                }
                fb = fb.Next;
            } while (fb != null);

            if (isDelete == false)
            { 
               continue;
            } 

            //处理分裂
            if (head != null)
            {
                m_BallSegmentList[x] = head;
                if (tail != null && head != tail.Head)
                {
                    m_BallSegmentList.Insert(x + 1, tail.Head);
                }
            }
            else
            {
                if (tail != null)
                {
                    m_BallSegmentList[x] = tail.Head;
                }
                else
                {
                    m_BallSegmentList.RemoveAt(x);
                }
            }

            //回退
            Ball target = null;
            if (head != null)
            {
                target = head.Tail;
            }
            else if (x > 0)
            {
                target = m_BallSegmentList[x - 1].Tail;
            }


            if (target != null)
            {
                Ball p = null;
                if (tail != null)
                {
                    p = tail.Head;
                }
                else if (x + 1 <= m_BallSegmentList.Count - 1)
                {
                    p = m_BallSegmentList[x + 1];
                }

                if (p != null && p.ballType == target.ballType)
                {
                    p.FallbackTarget = target;
                    m_FallbackList.Add(p);
                }
            }
        }
    }
    /// <summary>
    /// 处理检测需要回退的球
    /// <para/>消灭一组后又能自动消灭一组就会快速会退来自动消灭</summary>
    private void CheckFallbackBall()
    {
        int i = m_FallbackList.Count;
        while (i-- > 0)
        {
            Ball ball = m_FallbackList[i];
            if (   ball.gameObject.activeSelf == false 
                || ball.FallbackTarget.gameObject.activeSelf == false)
            {
                m_FallbackList.RemoveAt(i);
                continue;
            }
            ball.Progress -= Time.deltaTime * 15;
            UpdateBallProgress(ball);
            //代表回退到FallbackTarget位置了
            if (ball.Progress <= ball.FallbackTarget.Progress + 1)
            {
                m_SearchDestroyList.Add(ball);
                m_FallbackList.RemoveAt(i);
            }
        }
    }
    private void UpdateBallProgress(Ball ball)
    {
        while (ball != null)
        {
            if (ball.Next != null)
                ball.Next.Progress = ball.Progress + 1;
            ball = ball.Next;
        }
    }
    /// <summary>
    /// 处理球段之间的链接
    /// </summary>
    private void BallSegmentConnect()
    {
        int i = m_BallSegmentList.Count;
        while (i-- > 1)
        {
            Ball nextSeg = m_BallSegmentList[i];
            Ball preSeg = m_BallSegmentList[i - 1];
            Ball tail = preSeg.Tail;
            if (tail.Progress >= nextSeg.Progress - 1)
            {
                nextSeg.Progress = tail.Progress + 1;
                UpdateBallProgress(nextSeg);
                tail.Next = nextSeg;
                nextSeg.Pre = tail;
                m_BallSegmentList.RemoveAt(i);
            }
        }
    }


    /// <summary> 球回退 </summary>     
    private void SegmentBack()
    {
        int i = m_BallSegmentList.Count;
        if (i <= 0)
        { 
            return;
        }
        Ball ball = m_BallSegmentList[i - 1].Tail;
        ball.Progress -= Time.deltaTime * m_BackSpeed;
        while (ball.Pre != null)
        {
            if (ball.Pre.Progress > ball.Progress - 1)
            { 
                  ball.Pre.Progress = ball.Progress - 1;
            }
            ball = ball.Pre;

            
            if (ball.Progress < 0)//退出出洞口，就会隐藏
            {
                m_BallSegmentList[i - 1] = ball.Next;
                ball.Next.Pre = null;

                do
                {
                    BallPool.counter--;
                    ball.Recovery();
                    ball = ball.Pre;
                } while (ball != null);
                break;
            }
        }
    }
    #endregion


    #region 游戏结果

    public  void GameOver()
    {
        SoundManager.PlayFail();
        m_GameState = GameState.Fail;
        //
       // UIKit.HidePanel<GamePanel>();  //一些Manager挂在GamePanel上，需要修改
        UIKit.HidePanel<GamePanel>(); //复活时不该关掉这个，所以这里强制不控制，给后面的来控制
      //
        UIKit.OpenPanel<GameOverPanel>(UILevel.PopUI);

    }


    /// <summary>
    /// 开始回退，复活
    /// </summary>
    public void GameRevive()
    {       
        UIKit.ShowPanel<GamePanel>();
        m_GameState = GameState.Game;
        m_MoveState = MoveState.Back;
   
        ScheduleOnce.Start(this, () =>
        {
            m_MoveState = MoveState.Forword;
          
        }, m_BackTime);
    }

    public void GamePass()
    {
        m_GameState = GameState.Succ;
        GameData.UpLevelIndex();
        UIKit.ClosePanel<GamePanel>();
        UIKit.OpenPanel<SuccPanel>(UILevel.PopUI);
    }
    #endregion


    #region 辅助


    Ball SpawnBall()
    {
        return BallPool.GetObject().Init(this, GameStrategy.SpawnBallStrategy());
    }

    Ball SpawnBall(BallType ballType)
    {
        return BallPool.GetObject().Init(this, ballType);
    }
    #endregion  

}
