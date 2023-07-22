using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;
using QFramework.PointGame;


namespace QFramework.Example
{
	public class SuccPanelData : UIPanelData
	{
	}
	public partial class SuccPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as SuccPanelData ?? new SuccPanelData();
			// please add init code here


			BtnNext.onClick.AddListener(() => {
				
				UIKit.OpenPanel<GamePanel>(
					new GamePanelData() { LevelCount=GameData.GetLevelIndex() }
				);
				CloseSelf();
			});

			BtnHome.onClick.AddListener(() => {
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
