using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:57317d29-dc54-4f0c-a070-b23eb3d18848
	public partial class GameOverPanel
	{
		public const string Name = "GameOverPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnReset;
		[SerializeField]
		public UnityEngine.UI.Button BtnReplay;
		[SerializeField]
		public UnityEngine.UI.Button BtnHome;
		
		private GameOverPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnReset = null;
			BtnReplay = null;
			BtnHome = null;
			
			mData = null;
		}
		
		public GameOverPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		GameOverPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new GameOverPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
