using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏策略类
/// </summary>
public class GameStrategy : MonoBehaviour
{
    static int m_TypeNumber = 0;
    static int m_BallType;
    public static int BombDestroyCount = 5;



    public static BallType SpawnBallStrategy()
    {
        if (m_TypeNumber <= 0)
        {
            //2
            m_TypeNumber = Random.Range(1, 3);
            m_BallType = Random.Range(0, 4);
        }
        m_TypeNumber--;
        return (BallType)m_BallType;
    }
    public static BallType SpawnShootBallStrategy()
    {
        return (BallType)Random.Range(0, 5);
    }



    public static int SpawnBallCount(int sceneIndex)
    {
        return GameData.GetLevelIndex() * 10 + 50;
    }
}
