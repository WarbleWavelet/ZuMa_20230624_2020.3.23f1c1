using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/// <summary>Balls下的3个</summary>
public class ShootBall : MonoBehaviour
{
    public BallType ballType;

    public void Init(BallType type, Sprite sprite, Transform shooterTrans)
    {
        ballType = type;
        GetComponent<Image>().sprite = sprite;
        transform.localPosition = shooterTrans.localPosition;
        transform.rotation = shooterTrans.localRotation;
        gameObject.Show();
    }
    public void Move()
    {
        transform.localPosition += transform.up * Time.deltaTime * 2000; //原来在世界坐标下100
    }
    /// <summary>
    /// 是否超出边界
    /// </summary>
    /// <returns></returns>
    public bool IsOutOfBounds()
    {
        //if (transform.localPosition.x > 3 || transform.localPosition.x < -3
        // || transform.localPosition.y > 5 || transform.localPosition.y < -5)//原来在世界坐标下
        if (transform.localPosition.x > 700 || transform.localPosition.x < -700
         || transform.localPosition.y > 1200 || transform.localPosition.y <-1200)
        { 
            return true;
        }
        return false;
    }
    public bool IsCross(Vector3 targetPos,float dis)
    {
        return Vector3.Distance(transform.position, targetPos) <= dis;
    }
}
