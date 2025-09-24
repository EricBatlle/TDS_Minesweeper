using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Game
{
	public class WinViewPresenter : IInitializable
	{
		private readonly WinView view;
		private readonly NavigationSystem.NavigationSystem navigationSystem;

		public WinViewPresenter(WinView view, NavigationSystem.NavigationSystem navigationSystem)
		{
			this.view = view;
			this.navigationSystem = navigationSystem;
		}

		public void Initialize()
		{
			view.ContinuePlayingClicked += OnContinuePlayingClicked;
		}

		private void OnContinuePlayingClicked()
		{
			navigationSystem.Close(view).Forget();
		}
	}
}