using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UniRx.Async.Triggers;
using System.Collections;

namespace QFramework.Example
{
	public class GamePanelData : UIPanelData
	{
		/// <summary>��һ��ͼ����0���Դ�����</summary>
		public int LevelCount = 0;
	}


	public partial class GamePanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as GamePanelData ?? new GamePanelData();
			ResLoader loader =  ResLoader.Allocate();
            // please add init code here
			int levelCount = mData.LevelCount;

			GameSceneConfig.Instance.Init( levelCount,Bg, ShooterTrans);
		    GameManager.Instance.Init(levelCount, BallPoolTrans);

            FXManager.Instance.Init(FXs);
            ShootBallManager.Instance.Init(Balls);

            ShooterTrans.GetOrAddComponent<Shooter>().Init(Ball,ShooterTrans);
			Bg.GetComponent<Image>().sprite = loader.LoadSync<Sprite>(levelCount.ToString());
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
