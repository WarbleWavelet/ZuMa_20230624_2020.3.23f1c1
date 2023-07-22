using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;
using System.Reflection;

namespace QFramework.Example
{
	public class LevelSelectPanelData : UIPanelData
	{
        public int worldIndex = 0;


    }
	public partial class LevelSelectPanel : UIPanel
	{
         int m_WorldIndex = 0;
         int m_LevelIndex = 0;
         int m_WorldBgCount_1 = 10;
         int m_WorldBgCount_2 = 10;
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as LevelSelectPanelData ?? new LevelSelectPanelData();
            // please add init code here

            this.m_WorldIndex = mData.worldIndex;
            BtnBack.onClick.AddListener(() => {
               
                SoundManager.PlayClick();
                UIKit.OpenPanel<WorldSelectPanel>();
                CloseSelf();
            });

            //LevelIndex
            {
                Transform list = LevelListGo;
                for (int i = 0; i < list.childCount; i++)
                {
                    int curLevelIdx = i+1;
                    list.GetChild(i).GetComponent<Button>().onClick.AddListener(() =>
                    {
                        SoundManager.PlayClick();
                        SetLevelIndexAndLoadGame(curLevelIdx);
                        CloseSelf();                    
                    }); 
                }

            }
        }


        void SetLevelIndexAndLoadGame(int p_LevelIndex)
        {
            if (m_WorldIndex == 0)                                                        //测试过，分别是
            {
                GameData.SetLevelIndex(p_LevelIndex)  ;                                       // 0-1
            }
            if (m_WorldIndex == 1)
            {
                GameData.SetLevelIndex( p_LevelIndex + m_WorldIndex * m_WorldBgCount_1);    // 10-19
            }                         
            if (m_WorldIndex == 2)
            {
                GameData.SetLevelIndex(p_LevelIndex + m_WorldBgCount_1 + m_WorldBgCount_2);    //20-19
            }

            Debug.LogFormat("******worldIndex：{0}\nlevelIndex：{1}\nGameData.LevelIndex：{2}"
                , m_WorldIndex
                , p_LevelIndex
                , GameData.GetLevelIndex());
            // TODO
            UIKit.OpenPanel <GamePanel>(
                new GamePanelData { LevelCount = GameData.GetLevelIndex() }
            ); 
        }


        protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
