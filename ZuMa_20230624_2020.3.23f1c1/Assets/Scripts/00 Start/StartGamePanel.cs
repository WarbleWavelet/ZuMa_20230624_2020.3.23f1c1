using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace QFramework.Example
{
	public class StartGamePanelData : UIPanelData
	{
	}
	public partial class StartGamePanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as StartGamePanelData ?? new StartGamePanelData();
			// please add init code here

			Screen.SetResolution(640, 1136, false);//宽，高，不可修改
			BtnStart.onClick.AddListener(() => {
				Debug.Log("StartGamePanel");
				SoundManager.PlayClick();
				//
				UIKit.OpenPanel<WorldSelectPanel>();
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
