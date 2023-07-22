/****************************************************
    文件：NewBehaviourScript.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/18 6:35:7
	功能：
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
 

public static class ExtendSpriteRenderer
{

    /// <summary>水平滚动</summary>
    public static SpriteRenderer RollX(this SpriteRenderer p_SpriteRenderer, float speed=0.02f)
    {
        p_SpriteRenderer.material.mainTexture.wrapMode = TextureWrapMode.Repeat;
        p_SpriteRenderer.material.mainTextureOffset += new Vector2(Time.deltaTime * speed, 0);
        return p_SpriteRenderer;

    }


    /// <summary>竖直滚动</summary>
    public static SpriteRenderer RollY(this SpriteRenderer p_SpriteRenderer, float speed = 0.02f)
    {
        p_SpriteRenderer.material.mainTexture.wrapMode = TextureWrapMode.Repeat;
        p_SpriteRenderer.material.mainTextureOffset += new Vector2( 0, Time.deltaTime * speed);
        return p_SpriteRenderer;

    }

}



