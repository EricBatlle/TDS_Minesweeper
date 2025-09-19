using VContainer.Unity;

namespace Game
{
	public class LoseViewPresenter : IInitializable
	{
		private readonly LoseView view;
		private readonly NavigationSystem.NavigationSystem navigationSystem;

		public LoseViewPresenter(LoseView view, NavigationSystem.NavigationSystem navigationSystem)
		{
			this.view = view;
			this.navigationSystem = navigationSystem;
		}

		public void Initialize()
		{
			view.UserNameConfirmed += OnUserNameConfirmed;
		}

		private void OnUserNameConfirmed()
		{
			navigationSystem.Close(view.gameObject);
		}
	}
}