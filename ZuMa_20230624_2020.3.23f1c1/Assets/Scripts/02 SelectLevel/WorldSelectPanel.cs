using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{




    public class WorldSelectPanelData : UIPanelData
	{
        public int worldIndex = 0;


    }
	public partial class WorldSelectPanel : UIPanel
	{

        protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as WorldSelectPanelData ?? new WorldSelectPanelData();
            // please add init code here
            BtnWorld1.onClick.AddListener( ()=> {  OpenLevelSelectPanel(0); } );
            BtnWorld2.onClick.AddListener( ()=> {  OpenLevelSelectPanel(1); } );
            BtnWorld3.onClick.AddListener( ()=> {  OpenLevelSelectPanel(2); } );
        }

		void OpenLevelSelectPanel(int worldIdx=0)
		{
            SoundManager.PlayClick();
            UIKit.OpenPanel<LevelSelectPanel>( 
				new LevelSelectPanelData(){ worldIndex = worldIdx }
			);
			CloseSelf();
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
