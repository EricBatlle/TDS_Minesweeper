using NavigationSystem;
using UnityEngine;
using Utils;
using Action = Unity.Plastic.Antlr3.Runtime.Misc.Action;

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