using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:0e5972b2-0ec8-42a0-b744-4c6150100b51
	public partial class SuccPanel
	{
		public const string Name = "SuccPanel";
		
		[SerializeField]
		public UnityEngine.UI.Button BtnNext;
		[SerializeField]
		public UnityEngine.UI.Button BtnHome;
		
		private SuccPanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			BtnNext = null;
			BtnHome = null;
			
			mData = null;
		}
		
		public SuccPanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		SuccPanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new SuccPanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
