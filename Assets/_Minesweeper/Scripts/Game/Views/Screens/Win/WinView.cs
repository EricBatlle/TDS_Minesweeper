using System;
using NavigationSystem;
using UnityEngine;
using Utils;

namespace Game
{
	public class WinView : BaseView
	{
		public event Action ContinuePlayingClicked;

		[SerializeField] 
		private CustomButton continuePlayingButton;

		private void Awake()
		{
			continuePlayingButton.onLeftClick.AddListener(OnContinuePlaying);
		}

		private void OnContinuePlaying()
		{
			ContinuePlayingClicked?.Invoke();
		}
	}
}