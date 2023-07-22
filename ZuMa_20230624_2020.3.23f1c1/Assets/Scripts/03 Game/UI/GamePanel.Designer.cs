using System;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	// Generate Id:c6f661e5-ca1b-44f4-99f5-6ac5d6505cd2
	public partial class GamePanel
	{
		public const string Name = "GamePanel";
		
		[SerializeField]
		public Transform Bg;
		[SerializeField]
		public Transform BallPoolTrans;
		[SerializeField]
		public Transform ShooterTrans;
		[SerializeField]
		public Transform Ball;
		[SerializeField]
		public Transform Balls;
		[SerializeField]
		public Transform FXs;
		
		private GamePanelData mPrivateData = null;
		
		protected override void ClearUIComponents()
		{
			Bg = null;
			BallPoolTrans = null;
			ShooterTrans = null;
			Ball = null;
			Balls = null;
			FXs = null;
			
			mData = null;
		}
		
		public GamePanelData Data
		{
			get
			{
				return mData;
			}
		}
		
		GamePanelData mData
		{
			get
			{
				return mPrivateData ?? (mPrivateData = new GamePanelData());
			}
			set
			{
				mUIData = value;
				mPrivateData = value;
			}
		}
	}
}
