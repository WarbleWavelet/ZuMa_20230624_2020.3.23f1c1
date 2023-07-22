/****************************************************
    文件：ShowLevelSelectPanelCommand.cs
	作者：lenovo
    邮箱: 
    日期：2023/7/3 18:0:2
	功能：
*****************************************************/

using QFramework;
using QFramework.Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BuyLifeCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        UIKit.OpenPanel<LevelSelectPanel>(new LevelSelectPanelData()
        {
           
        });
    }
}



