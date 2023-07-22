using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace QFramework.Example
{
	public class GameOverPanelData : UIPanelData
	{
	}


	public partial class GameOverPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as GameOverPanelData ?? new GameOverPanelData();
			// please add init code here

			BtnReset.onClick.AddListener(()=>{ //复活
                GameManager.Instance.GameRevive();
                CloseSelf();

            });

            BtnReplay.onClick.AddListener(() => { //再来一次
                UIKit.ClosePanel<GamePanel>();
                UIKit.OpenPanel<GamePanel>( new GamePanelData() { LevelCount=GameData.GetLevelIndex() });
                CloseSelf();
            });

			BtnHome.onClick.AddListener(() => {	 //主页
                UIKit.ClosePanel<GamePanel>();
                UIKit.OpenPanel<StartGamePanel>();
                CloseSelf();
            });
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
