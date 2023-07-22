using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using System;

public class GameData
{
    private static int m_LevelIndex;
    public static int MaxLevelIndex = 30;
    private static bool m_Settlement = false;


    public static int GetLevelIndex()
    {
        return m_LevelIndex;
    }


    public static void SetLevelIndex(int p_Idx)
    {
        if (p_Idx >= MaxLevelIndex)
        {
            return;
        }
        m_LevelIndex = p_Idx;
    }


    public static void UpLevelIndex()
    {
        if (m_LevelIndex + 1 >= MaxLevelIndex)
        {
            return;
        }
        m_LevelIndex++;
    }


    /// <summary>是否结算中</summary>
    internal static bool IsSettlement()
    {

        return m_Settlement;


    }
    public static void SetSettlement(bool p_Settlement)
    {
        m_Settlement= p_Settlement;
    }
}

#region 改写成这样


public interface IStorage : IUtility
{
    void SaveInt(string key, int value);
    int LoadInt(string key, int defaultValue = 0);
}



public interface IGameModel : IModel
{
    BindableProperty<int> m_LevelIndex { get; }

}

public class GameModel : AbstractModel, IGameModel
{
    public BindableProperty<int> m_LevelIndex { get; } = new BindableProperty<int>()
    {
        Value = 0
    };


    protected override void OnInit()
    {
        var storage = this.GetUtility<IStorage>();

        m_LevelIndex.Value = storage.LoadInt(nameof(m_LevelIndex), 0);
        m_LevelIndex.Register(v => storage.SaveInt(nameof(m_LevelIndex), v));

    }
}

#endregion  
