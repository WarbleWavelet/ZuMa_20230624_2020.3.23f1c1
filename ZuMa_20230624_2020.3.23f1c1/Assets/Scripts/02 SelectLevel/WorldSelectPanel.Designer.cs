using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:a1a27bef-c42a-4236-8da2-a0bf2c250d21
	public partial class WorldSelectPanel
	{
		public const string Name = "WorldSelectPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnWorld1;
		[SerializeField]
		public UnityEngine.UI.Button BtnWorld2;
		[SerializeField]
		public UnityEngine.UI.Button BtnWorld3;
		
		private WorldSelectPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnWorld1 = null;
			BtnWorld2 = null;
			BtnWorld3 = null;
			
			mData = null;
		}
		
		public WorldSelectPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		WorldSelectPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new WorldSelectPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
