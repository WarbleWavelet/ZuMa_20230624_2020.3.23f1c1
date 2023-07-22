using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:3d179405-a8c2-4781-b9b0-f1103e65996b
	public partial class LevelSelectPanel
	{
		public const string Name = "LevelSelectPanel";
		
		[SerializeField]
		public RectTransform LevelListGo;
		[SerializeField]
		public UnityEngine.UI.Button BtnBack;
		
		private LevelSelectPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			LevelListGo = null;
			BtnBack = null;
			
			mData = null;
		}
		
		public LevelSelectPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		LevelSelectPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new LevelSelectPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
