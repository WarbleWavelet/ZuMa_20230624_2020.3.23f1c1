/****************************************************
    文件：ExtendComponent.Transform.RectTransform.cs
	作者：lenovo
    邮箱: 
    日期：2023/5/4 16:2:51
	功能：
*****************************************************/

using UnityEngine;

public static partial class ExtendRectTransform
{

    public static RectTransform Rect(this Transform trans)
    {
        return trans.GetComponent<RectTransform>();
    }

    public static RectTransform Rect(this GameObject go)
    {
        return go.GetComponent<RectTransform>();
    }
    public static void Reset(this RectTransform rect)
    {
        rect.localPosition = Vector3.zero;
        rect.anchoredPosition = Vector3.zero;
        rect.sizeDelta = Vector3.zero;
    }


    public static Vector2 GetPosInRootTrans(
        this RectTransform selfRectTransform
        , Transform rootTrans)
    {
        return RectTransformUtility
            .CalculateRelativeRectTransformBounds(rootTrans, selfRectTransform)
            .center;
    }

    public static RectTransform AnchorPosX(
        this RectTransform selfRectTrans
        , float anchorPosX)
    {
        var anchorPos = selfRectTrans.anchoredPosition;
        anchorPos.x = anchorPosX;
        selfRectTrans.anchoredPosition = anchorPos;
        return selfRectTrans;
    }

    public static RectTransform AnchorPosY(
        this RectTransform selfRectTrans
        , float anchorPosY)
    {
        var anchorPos = selfRectTrans.anchoredPosition;
        anchorPos.y = anchorPosY;
        selfRectTrans.anchoredPosition = anchorPos;
        return selfRectTrans;
    }

    public static RectTransform SetSizeWidth(
        this RectTransform selfRectTrans
        , float sizeWidth)
    {
        var sizeDelta = selfRectTrans.sizeDelta;
        sizeDelta.x = sizeWidth;
        selfRectTrans.sizeDelta = sizeDelta;
        return selfRectTrans;
    }

    public static RectTransform SetSizeHeight(
        this RectTransform selfRectTrans
        , float sizeHeight)
    {
        var sizeDelta = selfRectTrans.sizeDelta;
        sizeDelta.y = sizeHeight;
        selfRectTrans.sizeDelta = sizeDelta;
        return selfRectTrans;
    }

    public static Vector2 GetWorldSize(this RectTransform selfRectTrans)
    {
        return RectTransformUtility
            .CalculateRelativeRectTransformBounds(selfRectTrans)
            .size;
    }

    /// <summary>平铺开</summary>
    public static  RectTransform Stretch(this RectTransform rect)
    {
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.sizeDelta = Vector2.zero;

        return rect;
    }


}