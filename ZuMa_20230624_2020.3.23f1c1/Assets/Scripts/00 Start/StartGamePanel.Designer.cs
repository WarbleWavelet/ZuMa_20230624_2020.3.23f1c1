using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:29cfe4a1-ad1e-4350-a490-3bdf8cf34278
	public partial class StartGamePanel
	{
		public const string Name = "StartGamePanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnStart;
		
		private StartGamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnStart = null;
			
			mData = null;
		}
		
		public StartGamePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		StartGamePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new StartGamePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
